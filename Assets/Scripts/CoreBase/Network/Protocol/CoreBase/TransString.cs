using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameProtocol.Protocol
{
    
    [JsonConverter(typeof(TransStringJsonSerializer))]
    public sealed class TransString : MessageParameter
    {
        //format of string
        public readonly string Format;

        //parameters of string. Can rescuse by TransString
        public readonly object[] Parameters;

        /*
        public static byte GetByteRegister()
        {
            return (byte)222;
        }*/

        public TransString():this("")
        {

        }

        public TransString(string format)
        {
            this.Format = format;
            this.Parameters = null;
        }

        public TransString(string format, params object[] parameters)
        {
            this.Format = format;
            this.Parameters = parameters;
        }
        /*
        public static byte[] Serialize(object obj)
        {
            
            var transObject = (TransString)obj;
            using (MemoryStream m = new MemoryStream()) {
                using (BinaryWriter writer = new BinaryWriter(m)) {
                    writer.Write(transObject.Translate(transObject.Language));
                }
                return m.ToArray();
            };
        }*/
        /*
        public static object Deserialize(byte[] bytes)
        {
            return null;
        }*/

        public string Translate(string langue)
        {
            if (string.IsNullOrEmpty(Format)) return string.Empty;
            if (string.IsNullOrEmpty(langue)) return Format;

            string transFormat = ITranslator.Translator.Loc(Format, langue);

            transFormat = string.IsNullOrEmpty(transFormat) ? Format : transFormat;

            if (Parameters == null || Parameters.Length == 0) 
                return transFormat;
            

            object[] translateParameters = new object[Parameters.Length];

            for (int i = 0; i < translateParameters.Length; i++) {
                if (Parameters[i] is TransString)
                    translateParameters[i] = ((TransString)Parameters[i]).Translate(langue);
                else
                    translateParameters[i] = Parameters[i];
            }
            try {
                return string.Format(transFormat, translateParameters);
            } catch (Exception e) {
                Utilities.LogMng.Log("a", e.StackTrace);
                return transFormat;
            }
        }

        /// <summary>
        /// convert string to TransString fast
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static implicit operator TransString(string format)
        {
            return new TransString(format);
        }

        /// <summary>
        /// convert TransString to string fast
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static implicit operator string(TransString trans)
        {
            return trans.Translate(trans.Language);
        } 
    }

    class TransStringJsonSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            TransString trans = (TransString)value;

            //lấy ngôn ngữ hiện tại
            var language = (string)serializer.Context.Context;

            serializer.Serialize(writer, trans.Translate(language));
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new Exception("not use Serialize for TransString");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(TransString));
        }
    }
}
