using HW10.Entity;
using HW10.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Repository
{
    public class UserRepo : IUserRepo
    {
        string _path = "ListOFUsers.txt";
        public User _OnlineUser;
        public void Add(User item)
        {
            string data;
            var objects = GetAll();
            if (objects is null)
            {
                objects = new List<User>();
            }
            if(objects.Count != 0)
            {
                User Last = objects.Last();
                item.Id = Last.Id + 1;
            }
            else
            {
                item.Id = 1;
            }
            objects.Add(item);
            data = JsonConvert.SerializeObject(objects);
            File.WriteAllText(_path, data);
        }

        public User Get(int id)
        {
          return _OnlineUser;
        }

        public List<User> GetAll()
        {
            var data = File.ReadAllText(_path);
            var objects = JsonConvert.DeserializeObject<List<User>>(data);
            return objects;
        }
    }
}
