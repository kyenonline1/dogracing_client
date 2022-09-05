using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Utilities.Cryptor
{
    public class BigIntFactory
    {
        public static BigInteger Create(String hexInt)
        {
            if (hexInt.StartsWith("0x"))
            {
                hexInt = hexInt.Substring(2);
            }
            if (hexInt.EndsWith("L"))
                hexInt = hexInt.Substring(0, hexInt.Length - 1);

            if (hexInt[0] != '0')
                hexInt = "0" + hexInt;
            return new BigInteger(hexInt, 16);
        }

        public static byte[] ToByteArray(BigInteger number)
        {
            String hexInt = number.ToString(16);

            if (hexInt.StartsWith("0x"))
            {
                hexInt = hexInt.Substring(2);
            }
            if (hexInt.EndsWith("L"))
                hexInt = hexInt.Substring(0, hexInt.Length - 1);

            if ((hexInt.Length & 0x1) == 1)
                hexInt = "0" + hexInt;

            int len = hexInt.Length;

            byte[] stream = new byte[len / 2];
            for (int i = 0, k = stream.Length - 1; i < len; i += 2, k--)
            {
                String hex = hexInt.Substring(i, 2);
                stream[k] = (byte)int.Parse(hex, NumberStyles.HexNumber);
            }
            return stream;
        }

        static public BigInteger Attach(byte[] stream)
        {
            String hexInt = string.Empty;
            for (int i = 0; i < stream.Length; i++)
            {
                int x = (stream[i] & 0xff);
                hexInt = x.ToString("x2") + hexInt;
            }
            return new BigInteger(hexInt, 16);
        }
    }
}
