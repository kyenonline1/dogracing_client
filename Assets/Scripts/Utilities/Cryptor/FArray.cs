using System;
using System.Collections.Generic;
using System.Text;


namespace Utilities.Cryptor
{
    public class FArray
    {
        /// <summary>
        /// concat multi array
        /// </summary>
        /// <param name="multiArr"></param>
        /// <returns></returns>
        private static Array CoreConcat(Array[] multiArr)
        {
            int length = 0;
            foreach (Array arr in multiArr) length += arr.Length;
            Type itemType = multiArr[0].GetType().GetElementType();

            Array buf = Array.CreateInstance(itemType, length);

            int offset = 0;
            foreach (Array arr in multiArr)
            {
                Array.Copy(arr, 0, buf, offset, arr.Length);
                offset += arr.Length;
            }

            return buf;
        }

        
        public static Array Concat(params Array[] multiArr)
        {
            return CoreConcat(multiArr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="multiArr"></param>
        /// <returns></returns>
        public static T[] Concat<T>(params T[][] multiArr)
        {
            return (T[])CoreConcat((Array[])multiArr);
        }

        
        /// <summary>
        /// Clone an array
        /// </summary>
        /// <param name="src"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Array Clone(Array src, int start, int end)
        {
            Type itemType = src.GetType().GetElementType();

            if (end > src.Length) end = src.Length;
            if (start < 0) start = 0;
            if (start >= end)
                return Array.CreateInstance(itemType, 0);

            Array buf = Array.CreateInstance(itemType, end - start);
            Array.Copy(src, start, buf, 0, buf.Length);
            return buf;
        }

        public static Array Clone(Array src, int length)
        {
            return Clone(src, 0, length);
        }

        public static Array CloneStart(Array src, int start)
        {
            return Clone(src, start, src.Length);
        }

        public static Array CloneStart(Array src, int start, int end)
        {
            return Clone(src, start, end);
        }

        public static Array CloneToEnd(Array src, int end)
        {
            return Clone(src, 0, end);
        }

        public static Array CloneToEnd(Array src, int start, int end)
        {
            return Clone(src, start, end);
        }

        #region generic Methods
        public static T[] Clone<T>(T[] src, int start, int end)
        {
            return (T[])Clone((Array)src, start, end);
        }


        public static T[] Clone<T>(T[] src, int length)
        {
            return Clone(src, 0, length);
        }

        public static T[] CloneStart<T>(T[] src, int start)
        {
            return Clone(src, start, src.Length);
        }

        public static T[] CloneStart<T>(T[] src, int start, int end)
        {
            return Clone(src, start, end);
        }

        public static T[] CloneToEnd<T>(T[] src, int end)
        {
            return Clone(src, 0, end);
        }

        public static T[] CloneToEnd<T>(T[] src, int start, int end)
        {
            return Clone(src, start, end);
        }


        public static int SubIndex<T>(T[] subarr, T[] arr, int start)
        {
            if (arr.Length - start < subarr.Length) return -1;
            for (int i = start; i < arr.Length - subarr.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < subarr.Length; j++)
                {
                    if (!arr[i + j].Equals(subarr[j]))
                    {
                        found = false;
                        break;
                    }
                }
                if (found) return i;
            }
            return -1;
        }
        #endregion generic Methods
    }
}
