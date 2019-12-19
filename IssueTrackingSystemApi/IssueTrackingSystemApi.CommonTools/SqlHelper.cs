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
        private string ConnectString = @"Data Source=REX-LIN\COURSE_SQL;Initial Catalog=ITS;User ID=sqlLogin;Password=password1123";

        public enum SqlAction
        {
            SELECT,

        }
        public static string ObjectToString<T>(T conition, SqlAction action = SqlAction.SELECT)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder("");

            string tableName = (type.GetCustomAttributes().FirstOrDefault(i => i is DBAttribute) as DBAttribute).TableName;

            switch (action)
            {
                case SqlAction.SELECT:
                    PropertyInfo[] properties = type.GetProperties();

                    sql.AppendLine("SELECT ");
                    foreach (PropertyInfo pi in properties) // [DBAttribute.ColumnName] AS [PropertyName]
                    {
                        sql.AppendLine($"[{GetColumnName(pi)}] AS [{pi.Name}]");
                    }
                    sql.AppendLine($"FROM {tableName}");

                    break;
                default:
                    return null;
            }
            return null;
        }

        // 取得DBAttribute的ColumnName
        private static string GetColumnName(PropertyInfo property)
        {
            var attrs = property.GetCustomAttributes();

            foreach (Attribute attr in attrs)
            {
                if (attr is DBAttribute)
                {
                    return (attr as DBAttribute).ColumnName;
                }
            }
            return null;
        }


        //把從資料庫得到的DataTable 轉換成class List
        private List<T> GetItemListFromDataTable<T>(DataTable dataTable) where T : new()
        {
            string tableStr = JsonConvert.SerializeObject(dataTable);
            List<T> itemList = JsonConvert.DeserializeObject<List<T>>(tableStr);

            return itemList;
        }

        public enum TransactionResultType
        {
            EffectNum,
            EffectId
        };

        //根據 SQL 指令取得 DataTable
        public DataTable QueryWithNolock(SqlConnection connection, SqlCommand command, Dictionary<string, object> sqlParmDic = null)
        {
            command.Connection = connection;

            if (sqlParmDic != null)
            {
                foreach (KeyValuePair<string, object> item in sqlParmDic)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
            }

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        //加入Transaction 並指定回傳的資料(預設影響個數)
        public int QueryWithTransaction(SqlConnection connection, SqlCommand command, Dictionary<string, object> sqlParmDic, TransactionResultType resultType = TransactionResultType.EffectNum)
        {
            int result = 0;

            command.Connection = connection;

            foreach (KeyValuePair<string, object> item in sqlParmDic)
            {
                command.Parameters.AddWithValue(item.Key, item.Value);
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
                        result = Convert.ToInt32(command.ExecuteScalar());
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
