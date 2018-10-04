using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APICRUD.Models
{
    public class SQLExecutor
    {
        public static async Task<DataSet> GetDataSetAsync(SqlConnection myConnection, SqlCommand sqlCmd)
        {
            using (myConnection)
            {
                myConnection.Open();
                using (var mySQLAdapter = new SqlDataAdapter(sqlCmd))
                {
                    DataSet myDataSet = new DataSet();
                    mySQLAdapter.Fill(myDataSet);
                    return myDataSet;
                }                 
            }
        }
        public static async Task<int> ExecuteAsync(SqlConnection myConnection, SqlCommand cmd)
        {
            using (myConnection)
            using (cmd)
            {
                await myConnection.OpenAsync().ConfigureAwait(false);
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}