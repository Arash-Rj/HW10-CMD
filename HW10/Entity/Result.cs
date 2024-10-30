using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Entity
{
    public class Result
    {
        public bool _IsDone { get; set; }
        public string _Messege { get; set; }
        public Result(bool isDone,string messege=null)
        {
            _IsDone = isDone;
            _Messege = messege;
        }

    }
}
