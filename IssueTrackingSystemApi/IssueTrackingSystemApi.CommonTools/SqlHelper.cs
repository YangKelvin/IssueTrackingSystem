using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Linq;
using IssueTrackingSystemApi.Models.Entity;
using System.Collections.Generic;

namespace IssueTrackingSystemApi.CommonTools
{
    public class SqlHelper
    {
        private static string ConnectString = @"Data Source=REX-LIN\COURSE_SQL;Initial Catalog=ITS;User ID=sqlLogin;Password=password1123";

        private static string GetDataBaseConnectString { get => ConnectString; }

        public static IEnumerable<T> Select<T>(T conition) where T : new()
        {
            IEnumerable<T> result;
            using (SqlConnection conn = new SqlConnection(GetDataBaseConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToString(conition, SqlAction.SELECT));
                var dataTable = QueryWithNolock(conn, sql, ObjectToParm(conition));
                result = GetItemListFromDataTable<T>(dataTable);
            }

            return result;
        }

        public static int Insert<T>(T insertData) where T : new()
        {
            int id = -1;
            using (SqlConnection conn = new SqlConnection(GetDataBaseConnectString))
            {
                SqlCommand sql = new SqlCommand(ObjectToString(insertData, SqlAction.INSERT));
                id = QueryWithTransaction(conn, sql, ObjectToParm(insertData), TransactionResultType.EffectId);
            }

            return id;
        }

        public enum SqlAction
        {
            SELECT,
            INSERT
        }
        public static string ObjectToString<T>(T conition, SqlAction action = SqlAction.SELECT)
        {

            switch (action)
            {
                case SqlAction.SELECT:
                    return SelectSql(conition);
                case SqlAction.INSERT:
                    return InsertSql(conition);
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

                if (pi.GetValue(conition) != null)
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
        /// <param name="conition"></param>
        /// <returns></returns>
        private static string InsertSql<T>(T conition)
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
                if (pi.GetValue(conition) != null || attribute.Nullable)
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

        #endregion

        public static Dictionary<string, object> ObjectToParm<T>(T conition)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                DBAttribute attribute = GetColumnName(pi);
                if(pi.GetValue(conition) == null)
                {
                    if (!attribute.Nullable) { continue; }
                    result.Add($"@{pi.Name}", System.DBNull.Value);
                    continue;
                }
                result.Add($"@{pi.Name}", pi.GetValue(conition));
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
        public static int QueryWithTransaction(SqlConnection connection, SqlCommand command, Dictionary<string, object> sqlParmDic, TransactionResultType resultType = TransactionResultType.EffectNum)
        {
            int result = 0;

            command.Connection = connection;

            foreach (KeyValuePair<string, object> item in sqlParmDic)
            {
                command.Parameters.AddWithValue(item.Key, item.Value);
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
            catch
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
