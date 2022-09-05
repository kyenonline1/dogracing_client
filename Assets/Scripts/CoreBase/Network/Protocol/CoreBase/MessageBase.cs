using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using GameProtocol.Base;

namespace GameProtocol.Protocol
{

    public struct SendParameters
    {

        public byte ChannelId { get; set; }
        public bool Encrypted { get; set; }
        public bool Flush { get; set; }
        public bool Unreliable { get; set; }
    }

    /// <summary>
    /// for custom object in message base
    /// </summary>
    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CustomMemberAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class OldCustomMemberAttribute : Attribute
    {
        public string Name { get; set; }
    }

    [JsonConverter(typeof(MesssageJsonSerializer))]
    public class MessageBase : Operation
    {
        #region constructors
        [JsonIgnore]
        public SendParameters SendParameters;

        public MessageBase ShallowCopy()
        {
            return (MessageBase)this.MemberwiseClone();
        }

        public virtual void SetCodeRun() { CodeRun = "CodeRun"; }

        public MessageBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest)
        {
            SetCodeRun();
            SendParameters = sendParameters;
        }

        public MessageBase(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
            SetCodeRun();
            SendParameters = new SendParameters();
#if SERVER
            ParseJson(this.JsonContent);
#endif
        }

        public MessageBase()
            : base()
        {
            SetCodeRun();
            SendParameters = new SendParameters();

#if SERVER
            ParseJson(this.JsonContent);
#endif
        }

        public MessageBase(Dictionary<byte, object> dict)
        {
            SetCodeRun();
            SendParameters = new SendParameters();
            LoadFromDict(dict);

#if SERVER
            ParseJson(this.JsonContent);
#endif
        }
        #endregion constructors

        #region data contract
        [DataMember(Code = (byte)ParameterCode.CodeRun, IsOptional = false)]
        public string CodeRun { get; set; }

        [DataMember(Code = (byte)ParameterCode.SenderId, IsOptional = true)]
        public int SenderId { get; set; }

        [DataMember(Code = (byte)ParameterCode.Flag, IsOptional = true)]
        public byte Flag { get; set; }


        //[DataMember(Code = (byte)ParameterCode.LocalTime, IsOptional = false)]
        //public long LocalTime { get; set; }

        //[JsonIgnore]
        //[DataMember(Code = (byte)ParameterCode.JsonContent, IsOptional = true)]
        public string JsonContent {

#if SERVER
            get; set; 
#else
            get{
                return BuildJson();
            }
            set{
                ParseJson(value);
            }
#endif
        }

       

        #endregion data contract

        #region processing

        const string KEY_NAME = "Key";
        const string VALUE_NAME = "Value";

        /// <summary>
        /// Get all fields and properties 
        /// </summary>
        /// <returns></returns>
        public MemberInfo[] GetMemberInfos()
        {
            var type = this.GetType();
            return ((MemberInfo[])type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                     .Concat((MemberInfo[])type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                     .ToArray();
        }


        /// <summary>
        /// check type is KeyValuePair Class
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsKeyValuePair(Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        /// <summary>
        /// create KeyValuePair object from element
        /// </summary>
        /// <param name="keyValuePair"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private object CreateKeyValuePair(Type keyValuePair, object element)
        {
            PropertyInfo keyInfo = (PropertyInfo)(element.GetType().GetProperty(KEY_NAME));
            object key = keyInfo.GetValue(element, null);

            PropertyInfo valueInfo = (PropertyInfo)(element.GetType().GetProperty(VALUE_NAME));
            object value = valueInfo.GetValue(element, null);

            object instance = Activator.CreateInstance(keyValuePair, new object[] { key, value });

            return instance;
        }

        /// <summary>
        /// Get Collection type of type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Type GetCollectionType(Type type)
        {
            foreach (Type t in type.GetInterfaces()) {
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(ICollection<>))
                    return t;
            }
            return null;
        }

        /// <summary>
        /// Create Array object from object
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="propertyType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private object CreateArray(MemberInfo memberInfo, Type propertyType, object value)
        {
            //UnityEngine.Debug.Log("CreateArray: " + memberInfo.Name + " - type: " + propertyType.Name + " - value: " + value.ToString());
            Array arr = value as Array;
            var instance = Array.CreateInstance(propertyType.GetElementType(), arr.Length);
            Array.Copy((Array)value, instance, arr.Length);
            return instance;
        }

        /// <summary>
        /// Create Collection object from object
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="propertyType"></param>
        /// <param name="collectionType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private object CreateCollection(MemberInfo memberInfo, Type propertyType, Type collectionType, object value)
        {
            var instance = Activator.CreateInstance(propertyType);

            var elementType = collectionType.GetGenericArguments()[0];

            MethodInfo addMethod = collectionType.GetMethod("Add");

            IEnumerable collection = (IEnumerable)value;

            foreach (var element in collection) {
                var newElement = IsKeyValuePair(element.GetType()) ?
                    CreateKeyValuePair(elementType, element) : element;
                addMethod.Invoke(instance, new object[] { newElement });
            }
            return instance;
        }

        /// <summary>
        /// get all fields and properties with CustomMember Attribute
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetCustomObjectsByName()
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            var memberInfos = GetMemberInfos();

            foreach(var m in memberInfos){
                var attributes = (CustomMemberAttribute[])m.GetCustomAttributes(typeof(CustomMemberAttribute), false);
                if (attributes == null || attributes.Length == 0) continue;
                var attribute = attributes[0];
                string name = string.IsNullOrEmpty(attribute.Name) ? m.Name : attribute.Name;
                if (m is FieldInfo) {
                    ret[name] = ((FieldInfo)m).GetValue(this);
                } else if(m is PropertyInfo) {
                    ret[name] = ((PropertyInfo)m).GetValue(this, null);
                }
            }
            return ret;
        }

        /// <summary>
        /// parse json to all object with attribute CustomMemberAttribute
        /// </summary>
        protected virtual void ParseJson(string content)
        {
            if (string.IsNullOrEmpty(content)) return;
            JObject dict = JObject.Parse(content);

            var memberInfos = GetMemberInfos();

            foreach (var m in memberInfos)
            {
                var attributes = (CustomMemberAttribute[])m.GetCustomAttributes(typeof(CustomMemberAttribute), false);
                if (attributes == null || attributes.Length == 0) continue;
                JToken jValue = null;
                var attribute = attributes[0];
                string name = string.IsNullOrEmpty(attribute.Name) ? m.Name : attribute.Name;

                if (attribute == null || !dict.TryGetValue(name, out jValue))
                    continue;

                var type = (m is PropertyInfo) ? ((PropertyInfo)m).PropertyType : ((FieldInfo)m).FieldType;
                var value = jValue.ToObject(type);
                //var method = m.GetGetMethod();

                var collectionType = GetCollectionType(type);

                if (type != value.GetType())
                {
                    var instance = type.IsArray ? CreateArray(m, type, value) :
                        collectionType != null ? CreateCollection(m, type, collectionType, value) : value;
                    if (m is PropertyInfo)
                        ((PropertyInfo)m).SetValue(this, instance, null);
                    else
                        ((FieldInfo)m).SetValue(this, instance);
                }
                else
                {
                    if (m is PropertyInfo)
                        ((PropertyInfo)m).SetValue(this, value, null);
                    else
                        ((FieldInfo)m).SetValue(this, value);
                }
            }
        }

        /// <summary>
        /// translate all parameters to language 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private Dictionary<byte, object> TranslateParameters(Dictionary<byte, object> parameters, string language)
        {
            return parameters.ToDictionary(pair => pair.Key, pair => Translate(pair.Value, language));
        }

        /// <summary>
        /// Translate object to language
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private object Translate(object obj, string language)
        {
            if (obj is IList) {
                IList list = (IList)obj;

                IList ret = new List<object>();
                foreach (var item in list)
                    ret.Add(Translate(item, language));

                return ret;
            }

            if (obj is Dictionary<byte, object>) {
                Dictionary<byte, object> dict = (Dictionary<byte, object>)obj;
                return TranslateParameters(dict, language);
            }

            if (obj is object[]) {
                object[] arr = (object[])obj;
                return (arr).Select(item => Translate(item, language)).ToArray();
            }

            if (obj is IDictionary) {
                IDictionary dict = (IDictionary)obj;
                IDictionary ret = new Dictionary<object, object>();
                foreach (DictionaryEntry pair in dict) {
                    ret.Add(pair.Key, pair.Value);
                }
                return ret;
            }

            if (obj is TransString) {
                return ((TransString)obj).Translate(language);
            }

            if (obj is MessageParameter) {
                MessageParameter customObjectMessage = ((MessageParameter)obj).ShallowCopy();
                customObjectMessage.Language = language;
                return customObjectMessage;
            }
            return obj;
        }

        /// <summary>
        /// cache message for each language
        /// </summary>
        private Dictionary<string, Dictionary<byte, object>> MessageLanguages
            = new Dictionary<string,Dictionary<byte, object>>();

        /// <summary>
        /// translate the message into the language
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public Dictionary<byte, object> Translate(string language)
        {
            Dictionary<byte, object> ret = null;
            if (!MessageLanguages.TryGetValue(language, out ret)) {
                this.JsonContent = this.BuildJson(language);
                /*
                ret = this
                    .ToDictionary()
                    .ToDictionary(
                        pair => pair.Key,
                        pair => (pair.Value is TransString) ? ((TransString)pair.Value).Translate(language) : pair.Value
                    );*/
                ret = TranslateParameters(this.ToDictionary(), language);
                MessageLanguages[language] = ret;
            }
            return ret;
        }

        /// <summary>
        /// all language setting for json serializer
        /// </summary>
        private static Dictionary<string, JsonSerializerSettings> languageSettings 
            = new Dictionary<string, JsonSerializerSettings>();



        public static JsonSerializerSettings GetJsonLanguageSetting(string language)
        {
            JsonSerializerSettings setting = null;
            if (!languageSettings.TryGetValue(language, out setting)) {
                setting = new JsonSerializerSettings {
                    Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.All, language)
                };
                languageSettings[language] = setting;
            };
            return setting;
            
        }

        /// <summary>
        /// build all Fields and Properties with CustomMember Attribute to json string
        /// </summary>
        /// <param name="language"></param>
        public string BuildJson(string language = null)
        {
            var parameters = this.GetCustomObjectsByName();

            if (parameters != null && parameters.Count !=0) {
                

                if (language == null)
                    return JsonConvert.SerializeObject(parameters);

                //JsonSerializerSettings setting = GetJsonLanguageSetting(language);

                return JsonConvert.SerializeObject(parameters);//, setting);
            }
            return string.Empty;
        }

        /// <summary>
        /// Load Fields and Properties (with DataMember of CustomMember Attribute) from dict
        /// </summary>
        /// <param name="dict"></param>
        private void LoadFromDict(Dictionary<byte, object> dict)
        {
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in properties) {

                var attribute = (DataMember[])p.GetCustomAttributes(typeof(DataMember), false);
                if (attribute == null || attribute.Length ==0 || !dict.ContainsKey(attribute[0].Code))
                    continue;


                var value = dict[attribute[0].Code];
                var type = p.PropertyType;
                var collectionType = GetCollectionType(type);
                //var method = p.GetGetMethod();

                if (type != value.GetType()) {
                    var instance = type.IsArray ? CreateArray(p, type, value) :
                        collectionType != null ? CreateCollection(p, type, collectionType, value) : value;
                    //method.Invoke(this, new object[] { instance });
                    p.SetValue(this, instance, null);
                } else {
                    //method.Invoke(this, new object[] { value });
                    p.SetValue(this, value, null);
                }
            }
        }


        public Dictionary<byte, object> ToDictionary()
        {
            var dict = new Dictionary<byte, object>();

            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in properties)
            {
                DataMember[] attributes = (DataMember[])p.GetCustomAttributes(typeof(DataMember), false);

                if (attributes.Length == 0)
                {
                    continue;
                }
                var attribute = attributes[0];
                var keyCode = attribute.Code;

                var method = p.GetGetMethod();
                var value = method.Invoke(this, null);

                dict[keyCode] = value;
            }
            return dict;
        }
        #endregion
    }
}
