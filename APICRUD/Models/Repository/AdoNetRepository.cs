using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace APICRUD.Models
{
    public class AdoNetEmployeeRepository : IRepository<User>
    {
        private SqlDataReader reader;
        private SqlCommand sqlCmd = new SqlCommand();
        private SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
        private User user = new User();

        public async Task<User> GetAsync(int userId)
        {

            sqlCmd.CommandText = "Select * from Users where Id=" + userId + "";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = myConnection;

            var results = await SQLExecutor.GetDataSetAsync(myConnection, sqlCmd);

            user.Id = Convert.ToInt32(results.Tables[0].Rows[0][0]);
            user.FirstName = Convert.ToString(results.Tables[0].Rows[0][1]);
            user.LastName = Convert.ToString(results.Tables[0].Rows[0][2]);
            return user;
        }

        public async Task AddAsync(User user)
        {

            sqlCmd.CommandText = "INSERT INTO Users (Id,FirstName,LastName) Values (@Id,@FirstName,@LastName)";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@Id", user.Id);
            sqlCmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", user.LastName);

            await SQLExecutor.ExecuteAsync(myConnection, sqlCmd);
        }
       
        public async Task UpdateAsync(User employee)
        {
            sqlCmd.CommandText = "Update Users set Id=@Id, FirstName=@FirstName where LastName=@LastName;";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@Id", employee.Id);
            sqlCmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", employee.LastName);

            await SQLExecutor.ExecuteAsync(myConnection, sqlCmd);
        }

        public async Task DeleteAsync(int id)
        {
            sqlCmd.Connection = myConnection;
            sqlCmd.CommandText = "delete from Users where Id=" + id + "";
            await SQLExecutor.ExecuteAsync(myConnection, sqlCmd);
        }       
    }
}