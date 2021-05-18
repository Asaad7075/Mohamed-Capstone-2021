using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LogicLayer
{
    public static class StringHelpers
    {
        /// <summary>
        /// Richard Schroeder
        /// Created: 2021/02/16
        /// 
        /// string method to encrypt a user's password with SHA256 encryption
        /// </summary>
        /// <param name="source"></param>
        public static string hashSHA256(this string source)
        {
            string result = "";

            byte[] data;

            using (SHA256 sha256hash = SHA256.Create())
            {
                data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();

            return result;
        }
    }
}
