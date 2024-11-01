using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Services
{
    public static class UserQueries
    {
        public static string _create = "INSERT INTO Users (Username,Password,Status) values (@Username,@Password,@status);";
        public static string _GetById = "SELECT * FROM Users WHERE Id = @Id";
        public static string _GetAll = "SELECT * FROM dbo.Users";
        public static string _Update = "UPDATE Users SET Username=@Username , Password = @Password , Status = @Status  WHERE Id = @Id";
    }

}
