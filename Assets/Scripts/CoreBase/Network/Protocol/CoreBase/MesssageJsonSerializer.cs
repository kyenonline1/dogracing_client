using Newtonsoft.Json;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GameProtocol.Protocol
{
    /// <summary>
    /// Json parser to log only
    /// </summary>
    class MesssageJsonSerializer : JsonConverter
    {
         
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            MessageBase message = (MessageBase)value;

            //get properties has DataMemberAttribute or CustomMemberAttribute
            var dict = message.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => (p.GetCustomAttributes(typeof(DataMember), false) != null ))
                    /*
                .Where(p => (p.GetCustomAttribute(typeof(DataMemberAttribute), false) != null 
                        || p.GetCustomAttribute(typeof(CustomMemberAttribute), false) != null)
                        && p.GetCustomAttribute(typeof(JsonIgnoreAttribute), false) == null)*/

                .ToDictionary(p => p.Name, p => p.GetValue(message, null));
            
            
            serializer.Serialize(writer, dict);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new Exception("not use Serialize for MesssageBase");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(MessageBase));
        }
       
    }
}
