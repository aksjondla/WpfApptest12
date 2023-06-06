using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApptest1.model
{
    public static class UserInfo
    {
        public static bool IsAdmin { get; set; }
        public static Users TrueUserInfo { get; set; }

        public static string Hashis { get; }
        static UserInfo()
        {
            IsAdmin = false;
            TrueUserInfo = new Users();
            TrueUserInfo = null;
            Hashis = "250CF8B51C773F3F8DC8B4BE867A9A02";
        }
    }
}
