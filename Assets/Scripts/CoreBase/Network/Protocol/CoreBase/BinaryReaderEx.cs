using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.Protocol
{
    public static class BinaryReaderEx
    {
        private delegate object BuildinReaderDelegate(BinaryReader reader);

        //build-in readers
        private static readonly Dictionary<Type, BuildinReaderDelegate> BuildinReaders
            = new Dictionary<Type, BuildinReaderDelegate>()
        { 
            { typeof(bool),reader=>reader.ReadBoolean()},
        
            { typeof(byte),reader=>reader.ReadByte()},
       
            { typeof(char),reader=>reader.ReadChar()},
        
            { typeof(decimal),reader=>reader.ReadDecimal()},
        
            { typeof(double),reader=>reader.ReadDouble()},
        
            { typeof(short),reader=>reader.ReadInt16()},
        
            { typeof(int),reader=>reader.ReadInt32()},
        
            { typeof(long),reader=>reader.ReadInt64()},
        
            { typeof(sbyte),reader=>reader.ReadSByte()},
        
            { typeof(float),reader=>reader.ReadSingle()},
        
            { typeof(string),reader=>reader.ReadString()},
        
            { typeof(ushort),reader=>reader.ReadUInt16()},
        
            { typeof(uint),reader=>reader.ReadUInt32()},
        
            { typeof(ulong),reader=>reader.ReadUInt64()},
        };


        public static int Read7BitEncodedInt(BinaryReader reader)
        {
            int val = 0;
            int shift = 0;
            byte b;
            do {
                b = reader.ReadByte();
                val += (b & 0x7f) << shift;
                shift += 7;
            } while ((b & 0x80) != 0);
            return val;
        }

        /// <summary>
        /// read one element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T Read<T>(this BinaryReader reader)
        {
            BuildinReaderDelegate buildin = BuildinReaders[typeof(T)];
            return (T)buildin(reader);
        }

        /// <summary>
        /// read array of element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T[] ReadArray<T>(this BinaryReader reader)
        {
            int length = Read7BitEncodedInt(reader);
            T[] ret = new T[length];
            BuildinReaderDelegate buildin = BuildinReaders[typeof(T)];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = reader.Read<T>();
            }
            return ret;
        }

        /// <summary>
        /// reader two-demension array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T[][] ReadArray2<T>(this BinaryReader reader)
        {
            int length = Read7BitEncodedInt(reader);
            T[][] ret = new T[length][];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = reader.ReadArray<T>();
            }
            return ret;
        }

        /// <summary>
        /// read three-demension array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T[][][] ReadArray3<T>(this BinaryReader reader)
        {
            int length = Read7BitEncodedInt(reader);
            T[][][] ret = new T[length][][];
            for (int i = 0; i < ret.Length; i++) {
                ret[i] = reader.ReadArray2<T>();
            }
            return ret;
        }

    }
}
