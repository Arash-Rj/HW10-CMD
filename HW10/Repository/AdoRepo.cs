using HW10.Configuration;
using HW10.Entity;
using HW10.Enum;
using HW10.Interface;
using HW10.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Repository
{
    public class AdoRepo : IUserRepo
    {
        public User _OnlineUser;
        public void Add(User user)
        {
            using(SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = UserQueries._create;
                command.Parameters.AddWithValue(@"Username", user.UserName);
                command.Parameters.AddWithValue(@"Password", user.Password);
                command.Parameters.AddWithValue(@"Status", (int)user.Status);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                
            }
        }

        public User Get(int id)
        {
            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = UserQueries._GetById;
                command.Parameters.AddWithValue("Id", id);
                connection.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        return new User
                        {
                            UserName = (string)reader["Uername"],
                            Password = (string)reader["Password"],
                            Status = (StatusEnum)reader["Status"],
                            Id = (int)reader["Id"]
                        };
                    }
                }
            }
            return null;
        }
        public List<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = UserQueries._GetAll;
                List<User> users = new List<User>();
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        User user = new User();
                        user.UserName = (string)reader["Username"];
                        user.Password = (string)reader["Password"];
                        user.Status = (StatusEnum)reader["Status"];
                        user.Id = (int)reader["Id"];
                        users.Add(user);
                    }
                }
                connection.Close();
                return users;
            }

        }

        public void Update(User user)
        {
            using (SqlConnection connection = new SqlConnection(config.ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = UserQueries._Update;
                command.Parameters.AddWithValue(@"Username", user.UserName);
                command.Parameters.AddWithValue(@"Password", user.Password);
                command.Parameters.AddWithValue(@"Status", (int)user.Status);
                command.Parameters.AddWithValue(@"Id", user.Id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }
    }
}
