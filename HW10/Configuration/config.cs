using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Configuration
{
    public static class config
    {
       public static string  ConnectionString { get; set; }
        static config()
        {
            ConnectionString = @"Data Source=DESKTOP-7648UU0\SQLEXPRESS;Initial Catalog=HW10; Integrated Security=true;";
        }
    }
}
