using HW10.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public StatusEnum Status { get; set; }
        public User(string userName, string password) 
        {
            UserName = userName;
            Password = password;
            Status = StatusEnum.UnAvailable;
        }
        public bool changepassword(string newpass, string oldpass)
        {
            if(Password == oldpass)
            {
                Password = newpass;
                return true;
            }
           return false;
        }
        public bool CheckPass(string Pass)
        {
            if(Password == Pass)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{UserName} | {Status.ToString()}";
        }
    }
}
