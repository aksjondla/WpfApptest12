using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace WpfApptest1.model
{
    class md5
    {
        public static string HashPassword(string password)
        {
            if (password != null)
            {
                MD5 md5 = MD5.Create();
                byte[] b = Encoding.UTF8.GetBytes(password);
                byte[] hash = md5.ComputeHash(b);
                StringBuilder sb = new StringBuilder();
                foreach (var c in hash)
                    sb.Append(c.ToString("X2"));
                return sb.ToString();
            }
            else
                return password;
        }
    }
}
