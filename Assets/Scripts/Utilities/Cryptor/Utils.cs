using UnityEngine;
using System.Text;
using System.Security.Cryptography;

namespace Utilities
{
    public class Utils
    {
        public class Crypto
        {

            public static readonly string TAG = "Utils.Crypto";

            public static string MD5Hash(string input)
            {
                string hash = string.Empty;
                using (MD5 md5Hash = MD5.Create())
                {
                    // Convert the input string to a byte array and compute the hash. 
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

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
                    hash = sBuilder.ToString();
                }
                return hash;
            }
        }

        
    }
}
