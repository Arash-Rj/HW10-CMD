using HW10.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Interface
{
    public interface IUserService
    {
        Result Login(string username, string password);
        Result Register(string username,string password);
        Result Logout();
        Result ChangeStat(string Newstatus);
        List<User> Search(string username);
        Result ChangePass(string newpass,string oldpass);
    }
}
