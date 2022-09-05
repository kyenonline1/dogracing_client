using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MsgPack.Serialization;
using Base.Utils;
using Newtonsoft.Json;
using System.Text;

namespace Base
{
    public static class PackageCreator
    {
        public static byte[] SerializeToBytes<T>(this T source)
        {
            //// Creates serializer.
            //var serializer = MessagePackSerializer.Get<T>();
            //// Pack obj to stream.
            //return serializer.PackSingleObject(source);

            var pData = LitJson.JsonMapper.ToJson(source);
            //var pData = JsonConvert.SerializeObject(source);
            var package = Encoding.UTF8.GetBytes(pData);
            return package;
        }

        public static byte[] SerializeToBytes<T>(this T source, MessagePackSerializer serializer)
        {
            // Pack obj to stream.
            //return serializer.PackSingleObject(source);
            //var pData = JsonConvert.SerializeObject(source);
            var pData = LitJson.JsonMapper.ToJson(source);
            var package = Encoding.UTF8.GetBytes(pData);
            return package;
        }

        // Deerialize from bytes (BinaryFormatter)
        public static T DeserializeFromBytes<T>(this byte[] source)
        {
            //var serializer = MessagePackSerializer.Get<T>();

            //// Unpack from stream.
            //var unpackedObject = serializer.UnpackSingleObject(source);
            //return (T)unpackedObject;


            var package = Encoding.UTF8.GetString(source, 0, source.Length);
            var pData = LitJson.JsonMapper.ToObject<T>(package);
            //var pData = UnityEngine.JsonUtility.FromJson<T>(package);
            //var pData = JsonConvert.DeserializeObject<T>(package);
            return pData;
            //Encoding.UTF8.GetString(source)
           // return Newtonsoft.Json.Linq.JToken.Parse(package).ToObject<T>();

            //using (var memStream = new MemoryStream())
            //{
            //    var binForm = new BinaryFormatter();
            //    memStream.Write(source, 0, source.Length);
            //    memStream.Seek(8, SeekOrigin.Begin);
            //    var obj = binForm.Deserialize(memStream);
            //    UnityEngine.Debug.Log("DeserializeFromBytes: " + obj.GetType());
            //    var data = (T)obj;
            //    return data;
            //}
        }

        public static T DeserializeFromBytes<T>(this byte[] source, MessagePackSerializer serializer)
        {
            //return (T)serializer.UnpackSingleObject(source);
            //var package = Encoding.UTF8.GetString(source, 0, source.Length);
            //var pData = LitJson.JsonMapper.ToObject<T>(package);
            //var pData = UnityEngine.JsonUtility.FromJson<T>(package);
            //var pData = JsonConvert.DeserializeObject<T>(package);
            

            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(source, 8, source.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                var data = (T)obj;
                return data;
            }

            //return pData;
        }

        /// Convert a int array to a byte array
        public static byte[] SerializeToBytes(this int[] source)
        {
            var result = new byte[source.Length * sizeof(int)];
            Buffer.BlockCopy(source, 0, result, 0, result.Length);
            return result;
        }
    }
}
