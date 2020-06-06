using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogin
{
    public class User
    {
        public System.Int32 UserId { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String fakNum { get; set; }
        public int role { get; set; }
        public DateTime created { get; set; }
        public DateTime isActive { get; set; }
    }
}