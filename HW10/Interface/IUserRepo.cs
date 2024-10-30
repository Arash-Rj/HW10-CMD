using HW10.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Interface
{
    public interface IUserRepo
    {
        void Add(User item);
        User Get(int id);
        List<User> GetAll();
    }
}
