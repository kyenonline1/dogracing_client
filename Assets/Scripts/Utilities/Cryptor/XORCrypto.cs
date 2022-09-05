using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Cryptor
{
    public class XORCrypto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_encrypt(byte[] key, byte[] data)
        {
            byte[] ret = new byte[data.Length];
            int keylength = key.Length;
            int val = 0;
            for (int i = 0; i < data.Length; i++)
            {
                int k_i = i % keylength;
                val = (val ^ data[i] ^ key[k_i]);
                ret[i] = (byte)val;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">key must be ASCII encoding</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_encrypt(string key, byte[] data)
        {
            byte[] bkey = ASCIIEncoding.GetBytes(key);
            return xor_encrypt(bkey, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] xor_encrypt(string key, string data, Encoding encoding)
        {
            byte[] bkey = ASCIIEncoding.GetBytes(key);
            byte[] bdata = encoding.GetBytes(data);
            return xor_encrypt(bkey, bdata);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_encrypt(string key, string data)
        {
            return xor_encrypt(key, data, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_decrypt(byte[] key, byte[] data)
        {
            byte[] ret = new byte[data.Length];
            int keylength = key.Length;
            int val_i = 0, val_i_1 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                val_i_1 = val_i;
                val_i = data[i];
                int k_i = i % keylength;
                int val = (val_i_1 ^ data[i] ^ key[k_i]);
                ret[i] = (byte)val;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"> key must be ASCII encoding</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_decrypt(string key, byte[] data)
        {
            byte[] bkey = ASCIIEncoding.GetBytes(key);
            return xor_decrypt(bkey, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] xor_decrypt(string key, string data, Encoding encoding)
        {
            byte[] bkey = ASCIIEncoding.GetBytes(key);
            byte[] bdata = encoding.GetBytes(data);
            return xor_decrypt(bkey, bdata);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] xor_decrypt(string key, string data)
        {
            return xor_decrypt(key, data, Encoding.UTF8);
        }
    }
}
