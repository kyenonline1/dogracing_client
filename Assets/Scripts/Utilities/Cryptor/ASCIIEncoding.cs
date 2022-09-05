using System.Text;

namespace Utilities.Cryptor
{
    public class ASCIIEncoding
    {
        public static byte[] GetBytes(string s)
        {
            var retval = new byte[s.Length];
            for (int ix = 0; ix < s.Length; ++ix)
            {
                char ch = s[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }

        public static string GetString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length); 
            foreach(byte b in bytes) {
                sb.Append(b<=0x7f ? (char)b : '?'); 
            } 
            return sb.ToString(); 
        }
    }
}
