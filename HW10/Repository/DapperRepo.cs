using Dapper;
using HW10.Configuration;
using HW10.Entity;
using HW10.Interface;
using HW10.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Repository        
{           
    internal class DapperRepo : IUserRepo
    {
        public User _OnlineUser;
        public void Add(User user)
        {
            using (IDbConnection DataBase = new SqlConnection(config.ConnectionString))
            {
                DataBase.Execute(UserQueries._create, new { user.UserName,user.Password,user.Status });
            }
        }

        public User Get(int id)
        {
            using (IDbConnection DataBase = new SqlConnection(config.ConnectionString))
            {
               return DataBase.QueryFirstOrDefault<User>(UserQueries._GetById,new {Id = id});
            }
        }

        public List<User> GetAll()
        {
            //using (IDbConnection DataBase = new SqlConnection(config.ConnectionString))
            //{
            //    return DataBase.Query<User>(UserQueries._GetAll).ToList();
            //}
            using IDbConnection cm = new SqlConnection(config.ConnectionString);
            
                var query = "SELECT * FROM dbo.Users";
                var cmd = new CommandDefinition(query);
                var result = cm.Query<User>(cmd).ToList();
                return result;            
        }

        public void Update(User user)
        {
            using (IDbConnection DataBase = new SqlConnection(config.ConnectionString))
            {
                DataBase.Execute("UPDATE Users SET Username=@Username , Password = @Password , Status = @Status  WHERE Id = @Id",
                    new { Id =user.Id, Username = user.UserName, Password = user.Password ,Status = (int)user.Status });
            }
        }
    }
}
