using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace GameProtocol.Protocol
{
    public static class BinaryWriterEx
    {
        private delegate void BuildinWriterDelegate(BinaryWriter writer, object obj);

        //build-in writer
        private static readonly Dictionary<Type, BuildinWriterDelegate> BuildinWriters
            = new Dictionary<Type, BuildinWriterDelegate>()
        {
           
            { typeof(bool),(writer, obj)=>writer.Write((bool)obj)},
        
            { typeof(byte),(writer, obj)=>writer.Write((byte)obj)},
       
            { typeof(char),(writer, obj)=>writer.Write((char)obj)},
        
            { typeof(decimal),(writer, obj)=>writer.Write((decimal)obj)},
        
            { typeof(double),(writer, obj)=>writer.Write((double)obj)},
        
            { typeof(short),(writer, obj)=>writer.Write((short)obj)},
        
            { typeof(int),(writer, obj)=>writer.Write((int)obj)},
        
            { typeof(long),(writer, obj)=>writer.Write((long)obj)},
        
            { typeof(sbyte),(writer, obj)=>writer.Write((sbyte)obj)},
        
            { typeof(float),(writer, obj)=>writer.Write((float)obj)},
        
            { typeof(string),(writer, obj)=>writer.Write((string)obj)},
        
            { typeof(ushort),(writer, obj)=>writer.Write((ushort)obj)},
        
            { typeof(uint),(writer, obj)=>writer.Write((uint)obj)},
        
            { typeof(ulong),(writer, obj)=>writer.Write((ulong)obj)},
        };

        public static void Write7BitEncodedInt(BinaryWriter writer, int val)
        {
            if (val < 0)
                throw new ArgumentException("Number cannot be less than zero.");

            int t = val;
            do {
                byte b = (byte)(t & 0x7f);
                t >>= 7;
                if (t > 0)
                    b |= 0x80;

                writer.Write(b);
            } while (t > 0);
        }

        public static void WriteArray<T>(this BinaryWriter writer, T[] obj)
        {
            int length = obj != null ? obj.Length : 0;
            Write7BitEncodedInt(writer, length);
            BuildinWriterDelegate buildin = BuildinWriters[typeof(T)];
            for (int i = 0; i < length; i++) {
                buildin(writer, obj[i]);
            }
        }

        public static void WriteArray2<T>(this BinaryWriter writer, T[][] obj)
        {
            int length = obj != null ? obj.Length : 0;
            Write7BitEncodedInt(writer, length);
            
            for (int i = 0; i < length; i++) {
                writer.WriteArray<T>(obj[i]);
            }
        }


        public static void WriteArray3<T>(this BinaryWriter writer, T[][][] obj)
        {
            int length = obj != null ? obj.Length : 0;
            Write7BitEncodedInt(writer, length);

            for (int i = 0; i < length; i++) {
                writer.WriteArray2<T>(obj[i]);
            }
        }
    }
}
