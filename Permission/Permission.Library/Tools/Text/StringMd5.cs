using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Tools.Text
{
    public class StringMd5
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Hash32(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// md5加盐
        /// </summary>
        /// <param name="input">需要加密文本</param>
        /// <param name="salt">盐</param>
        /// <returns></returns>
        public static string Md5Hash32Salt(string input, string salt)
        {
            return Md5Hash32(input + "_" + salt);
        }

        public static string Md5Hash32Salt(string input)
        {
            return Md5Hash32Salt(input, "bsaid#$%$ASA");
        }
    }
}
