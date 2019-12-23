using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Linq;
using IssueTrackingSystemApi.Models.Entity;

namespace IssueTrackingSystemApi.CommonTools
{
    public class SqlHelper
    {
        public static string DBConnectString { get; set; }

        /// <summary>
        /// 取得Entity Model 查詢結果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition">查詢條建</param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(T conition) where T : class, new()
        {
            if (conition == null) conition = new T();

            IEnumerable<T> result;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToSql(SqlAction.SELECT, conition: conition));
                var dataTable = QueryWithNolock(conn, sql, ObjectToParm(conition));
                result = GetItemListFromDataTable<T>(dataTable);
            }

            return result;
        }

        /// <summary>
        /// 取得sql 查詢結果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr">sql語法</param>
        /// <param name="sqlParmDic">參數字典</param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(string sqlStr, Dictionary<string, object> sqlParmDic = null) where T : class, new()
        {
            IEnumerable<T> result;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(sqlStr);
                var dataTable = QueryWithNolock(conn, sql, sqlParmDic);
                result = GetItemListFromDataTable<T>(dataTable);
            }

            return result;
        }

        /// <summary>
        /// 新增Entity Model資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="insertData">新資料</param>
        /// <returns></returns>
        public static int Insert<T>(T insertData) where T : class, new()
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToSql(SqlAction.INSERT, newData: insertData));

                var tranResult = TransactionResultType.EffectNum;
                PropertyInfo[] properties = typeof(T).GetProperties();
                if (properties.Where(pi => GetColumnName(pi).AutoGenerate).Any())
                    tranResult = TransactionResultType.EffectId;

                id = QueryWithTransaction(conn, sql, ObjectToParm(insertData), tranResult);
            }

            return id;
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition">刪除條件</param>
        /// <returns></returns>
        public static int Delete<T>(T conition) where T : class, new()
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToSql(SqlAction.DELETE, conition: conition));
                id = QueryWithTransaction(conn, sql, ObjectToParm(conition), TransactionResultType.EffectNum);
            }

            return id;
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition">更新的地方</param>
        /// <param name="newData">更新成什麼資料</param>
        /// <returns></returns>
        public static int Update<T>(T conition, T newData) where T : class, new()
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToSql(SqlAction.UPDATE, conition: conition, newData: newData));
                id = QueryWithTransaction(conn, sql, ObjectToParm(conition, "Con_").Union(
                                                     ObjectToParm(newData, "Col_")).ToDictionary(i => i.Key, i => i.Value)
                    , TransactionResultType.EffectNum);
            }

            return id;
        }

        public static int Execute(string sqlStr, Dictionary<string, object> sqlParmDic = null, TransactionResultType resultType = TransactionResultType.EffectNum)
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(DBConnectString))
            {
                SqlCommand sql = new SqlCommand(sqlStr);
                id = QueryWithTransaction(conn, sql, sqlParmDic, resultType);
            }

            return id;
        }

        public enum SqlAction
        {
            SELECT,
            INSERT,
            DELETE,
            UPDATE
        }
        public static string ObjectToSql<T>(SqlAction action = SqlAction.SELECT, T conition = null , T newData = null) where T: class
        {
            if (conition is null && newData is null)
                return null;

            switch (action)
            {
                case SqlAction.SELECT:
                    return SelectSql(conition);
                case SqlAction.INSERT:
                    return InsertSql(newData);
                case SqlAction.DELETE:
                    return DeleteSql(conition);
                case SqlAction.UPDATE:
                    return UpdateSql(conition, newData);
                default:
                    return null;
            }
            return null;
        }

        #region SQL
        /// <summary>
        /// 取得SELECT語法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition"></param>
        /// <returns></returns>
        private static string SelectSql<T>(T conition)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder("");

            string tableName = (type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableName;

            PropertyInfo[] properties = type.GetProperties();

            List<string> columnList = new List<string>();
            List<string> conitionList = new List<string>();

            foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
            {
                DBAttribute attribute = GetColumnName(pi);
                columnList.Add($"[{attribute.ColumnName}] AS [{pi.Name}]");

                if (pi.GetValue(conition) != null || attribute.Nullable)
                {
                    string operation = "";
                    if (conitionList.Any())
                    {
                        operation = attribute.Operation.ToString();
                    }
                    conitionList.Add($"{operation} [{attribute.ColumnName}] = @{pi.Name} ");
                }
            }
            // Column
            sql.AppendLine($"SELECT {string.Join(", ", columnList)} ");
            // From
            sql.AppendLine($"FROM [{tableName}] ");
            // Where
            if (!conitionList.Any())
            {
                return sql.ToString();
            }
            sql.AppendLine($"WHERE {string.Join("", conitionList)} ");
            return sql.ToString();
        }

        /// <summary>
        /// 取得Insert語法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newData"></param>
        /// <returns></returns>
        private static string InsertSql<T>(T newData)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder("");

            string tableName = (type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableName;

            PropertyInfo[] properties = type.GetProperties();

            List<string> columnList = new List<string>();
            List<string> conitionList = new List<string>();

            foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
            {
                DBAttribute attribute = GetColumnName(pi);

                if (attribute.AutoGenerate) continue; // 自動產生的值不要動

                if (pi.GetValue(newData) != null || attribute.Nullable)
                {
                    columnList.Add($"[{attribute.ColumnName}]");

                    conitionList.Add($"@{pi.Name}");
                }
            }
            // 沒資料不用Insert
            if (!columnList.Any())
            {
                return null;
            }

            // Insert
            sql.AppendLine($"INSERT INTO [{tableName}] ({string.Join(", ", columnList)}) ");
            // Value
            sql.AppendLine($"VALUES ({string.Join(", ", conitionList)})");
            return sql.ToString();
        }

        /// <summary>
        /// 取得Delete語法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition"></param>
        /// <returns></returns>
        private static string DeleteSql<T>(T conition)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder("");

            string tableName = (type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableName;

            PropertyInfo[] properties = type.GetProperties();

            List<string> conitionList = new List<string>();

            foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
            {
                DBAttribute attribute = GetColumnName(pi);

                if (pi.GetValue(conition) != null || attribute.Nullable)
                {
                    string operation = "";
                    if (conitionList.Any())
                    {
                        operation = attribute.Operation.ToString();
                    }
                    conitionList.Add($"{operation} [{attribute.ColumnName}] = @{pi.Name} ");
                }
            }
            // Column
            sql.AppendLine($"DELETE [{tableName}] ");
            // Where
            if (!conitionList.Any())
            {
                if((type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableDeleteAble)
                {
                    return sql.ToString();
                }
                return null;
            }
            sql.AppendLine($"WHERE {string.Join("", conitionList)} ");
            return sql.ToString();
        }
        
        /// <summary>
        /// 取得Updata語法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition"></param>
        /// <param name="newData"></param>
        /// <returns></returns>
        private static string UpdateSql<T>(T conition, T newData)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder("");

            string tableName = (type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableName;

            PropertyInfo[] properties = type.GetProperties();

            List<string> columnList = new List<string>();
            List<string> conitionList = new List<string>();

            //columnList
            foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
            {
                DBAttribute attribute = GetColumnName(pi);

                if (attribute.AutoGenerate) continue; // 自動產生的值不要動

                if (pi.GetValue(newData) != null || attribute.Nullable)
                {
                    columnList.Add($"[{attribute.ColumnName}] = @Col_{pi.Name} ");
                }
            }
            //conitionList
            foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
            {
                DBAttribute attribute = GetColumnName(pi);

                if (pi.GetValue(conition) != null || attribute.Nullable)
                {
                    string operation = "";
                    if (conitionList.Any())
                    {
                        operation = attribute.Operation.ToString();
                    }
                    conitionList.Add($"{operation} [{attribute.ColumnName}] = @Con_{pi.Name} ");
                }
            }
            // Column
            sql.AppendLine($"UPDATE [{tableName}] ");
            // SET
            if (!columnList.Any())
            {
                return null;
            }
            sql.AppendLine($"SET {string.Join(", ", columnList)} ");
            // Where
            if (!conitionList.Any())
            {
                return sql.ToString();
            }
            sql.AppendLine($"WHERE {string.Join("", conitionList)} ");
            return sql.ToString();
        }
        #endregion

        /// <summary>
        /// 把class根據標籤轉成含有前綴的字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conition"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObjectToParm<T>(T conition, string prefix = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                DBAttribute attribute = GetColumnName(pi);
                if(pi.GetValue(conition) == null)
                {
                    if (!attribute.Nullable) { continue; }
                    result.Add($"@{prefix}{pi.Name}", System.DBNull.Value);
                    continue;
                }
                result.Add($"@{prefix}{pi.Name}", pi.GetValue(conition));
            }
            return result;
        }

        // 取得DBAttribute
        private static DBAttribute GetColumnName(PropertyInfo property)
        {
            var attrs = property.GetCustomAttributes();

            foreach (Attribute attr in attrs)
            {
                if (attr is DBAttribute)
                {
                    return (attr as DBAttribute);
                }
            }
            return null;
        }

        //把從資料庫得到的DataTable 轉換成class List
        private static IEnumerable<T> GetItemListFromDataTable<T>(DataTable dataTable) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            IList<T> result = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                //var item = CreateItemFromRow<T>((DataRow)row, properties);
                T item = new T();
                foreach (var property in properties)
                {
                    var value = row[property.Name];
                    if (value is System.DBNull)
                    {
                        property.SetValue(item, null, null);
                        continue;
                    }
                    property.SetValue(item, row[property.Name], null);
                }
                result.Add(item);
            }

            return result;
        }

        public enum TransactionResultType
        {
            EffectNum,
            EffectId
        };

        //根據 SQL 指令取得 DataTable
        private static DataTable QueryWithNolock(SqlConnection connection, SqlCommand command, Dictionary<string, object> sqlParmDic = null)
        {
            command.Connection = connection;

            if (sqlParmDic != null)
            {
                foreach (KeyValuePair<string, object> item in sqlParmDic)
                {
                    if (item.Value == null) continue;
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        //加入Transaction 並指定回傳的資料(預設影響個數)
        private static int QueryWithTransaction(SqlConnection connection, SqlCommand command, Dictionary<string, object> sqlParmDic, TransactionResultType resultType = TransactionResultType.EffectNum)
        {
            int result = 0;

            command.Connection = connection;

            foreach (KeyValuePair<string, object> item in sqlParmDic)
            {
                command.Parameters.AddWithValue(item.Key, item.Value ?? DBNull.Value);
            }

            SqlParameter pmtLogId = new SqlParameter("@LogId", SqlDbType.Int);
            if (resultType == TransactionResultType.EffectId)
            {
                command.CommandText += " SET @LogId = SCOPE_IDENTITY() ";
                pmtLogId.Direction = ParameterDirection.Output;
                command.Parameters.Add(pmtLogId);
            }

            connection.Open();
            SqlTransaction tran = connection.BeginTransaction();
            command.Transaction = tran;
            try
            {
                switch (resultType)
                {
                    case TransactionResultType.EffectNum:
                        result = command.ExecuteNonQuery();
                        break;
                    case TransactionResultType.EffectId:
                        command.ExecuteScalar();
                        result = (int)pmtLogId.Value;
                        break;
                }
                tran.Commit();
            }
            catch(SqlException sqlException)
            {
                tran.Rollback();
                if (sqlException.Number == 2627) 
                    result = -1; // 違反 %ls 條件約束 '%.ls'。無法在物件 '%.ls' 中插入重複的索引鍵。
            }
            catch(Exception e)
            {
                tran.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }


    }
}
