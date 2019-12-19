using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IssueTrackingSystemApi.CommonTools
{
    public class ObjectToSql
    {
        //static IEnumerable<T> Query<T>()
        //{

        //}

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
