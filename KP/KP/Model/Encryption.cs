using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KP.Model
{
    internal class Encryption
    {
        public static string hashPassword(String password)
        {
            MD5 md5 = MD5.Create();

            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
                sb.Append(a.ToString("X2"));

            return sb.ToString();
        }

    }
}
