using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomManagement.Domain;
using System.Data.SqlClient;
using System.Data.Entity;

namespace TelecomManagement.Data
{
    public class UserRepository : RepositoryBase<User>
    {

        private string _connectionString;

        public UserRepository(TelecomContext context) : base(context)
        {

        }

        public void AdaugaUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO User (Username, Password, LastLoggedIn) VALUES (@Username, @Password, @LastLoggedIn)";
                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@LastLoggedIn", user.LastLoggedIn);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

      
    }
}
