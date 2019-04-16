using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Cryptography;

namespace ClassLibrary1
{
   public class EncryptClass
    {
        /// <summary>
        /// MD5Encrypt
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="ToUpper">是否轉大寫</param>
        /// <returns></returns>
        public string MD5Encrypt(string inputString, bool ToUpper = false)
        {

            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            if (ToUpper)
            {
                return sBuilder.ToString().ToUpper();
            }
            else
            {
                return sBuilder.ToString();
            }
        }
    }
}
