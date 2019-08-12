using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Tools.Text
{
   public class StringDes
    {
        private static readonly string key = "lzhpwd00";

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DesEncrypt(string encryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            //直接加密
            byte[] encrypted = provider.CreateEncryptor(keyBytes, keyIV).TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);
            return Convert.ToBase64String(encrypted);

        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string DesDecrypt(string decryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            //直接解密
            byte[] outputdata = provider.CreateDecryptor(keyBytes, keyIV).TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);
            return Encoding.UTF8.GetString(outputdata);
        }
    }
}
