using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Cryptor
{
    public class RSA
    {
        BigInteger e, d, N;
        public RSA(BigInteger e, BigInteger d, BigInteger N)
        {
            this.e = e;
            this.d = d;
            this.N = N;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public String encrypt(byte[] message)
        {
            byte[] bounder = new byte[message.Length + 2];
            Array.Copy(message, bounder, message.Length);

            bounder[message.Length] = 0x1;
            //BigInteger x = new BigInteger(bounder);
            //77873
            BigInteger x = BigIntFactory.Attach(bounder);

            //x.attach(bounder);
            x = x.modPow(e, N);

            //byte[] temp = BigIntFactory.ToByteArray(x);

            String hexInt = x.ToString(16);
            if (hexInt[0] == '0')
                hexInt = hexInt.Substring(1);
            return hexInt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public String encrypt(String message, Encoding encoding)
        {
            byte[] stream = encoding.GetBytes(message);
            return encrypt(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public String encrypt(String message)
        {
            return encrypt(message, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexInt"></param>
        /// <returns></returns>
        public byte[] decrypt(String hexInt)
        {
            BigInteger x = BigIntFactory.Create(hexInt);
            x = x.modPow(d, N);
            //byte[] original = BigIntFactory.ToByteArray(x);
            byte[] original = x.getBytes();
            byte[] ret = new byte[original.Length - 1];
       
            Array.Copy(original, 1, ret, 0, ret.Length);
            Array.Reverse(ret);
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexInt"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public String decrypt(String hexInt, Encoding encoding)
        {
            byte[] stream = decrypt(hexInt);
            return encoding.GetString(stream, 0, stream.Length);
        }
    }
}
