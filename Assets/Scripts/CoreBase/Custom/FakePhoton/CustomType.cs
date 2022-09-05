using System;
using ExitGames.Client.Photon;

namespace Client.Photon
{
    internal class CustomType
    {
        public readonly byte Code;
        public readonly Type Type;
        public readonly SerializeMethod SerializeFunction;
        public readonly DeserializeMethod DeserializeFunction;

        public CustomType(Type type, byte code, SerializeMethod serializeFunction, DeserializeMethod deserializeFunction)
        {
            this.Type = type;
            this.Code = code;
            this.SerializeFunction = serializeFunction;
            this.DeserializeFunction = deserializeFunction;
        }
    }
}