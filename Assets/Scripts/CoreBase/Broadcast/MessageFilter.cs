using UnityEngine;
using System.Collections;

namespace Broadcast
{
    public class MessageFilter
    {

        private string code = string.Empty;
        private string type = string.Empty;

        public MessageFilter(string code, string type)
        {
            this.code = code;
            this.type = type;
        }

        public string Code { get { return code;} private set{code = value;} }
        public string Type { get { return type;} private set{type = value;} }

        public bool Equals(MessageFilter other)
        {
            return this.Code.Equals(other.Code) && this.Type.Equals(other.Type);
        }
    }
}