using UnityEngine;
using System.Collections;
using GameProtocol.Protocol;
using System.IO;
using ExitGames.Client.Photon;

namespace Client.Photon
{
    public class FakeOperations
    {

        enum EgMessageType : byte
        {
            Init = (byte)0,
            InitResponse = (byte)1,
            Operation = (byte)2,
            OperationResponse = (byte)3,
            Event = (byte)4,
            InternalOperationRequest = (byte)6,
            InternalOperationResponse = (byte)7,
            Message = (byte)8,
            RawMessage = (byte)9,
        }

        //static readonly byte[] udpHeader0xF3 = new byte[2]
        //{
        //  (byte) 243,
        //  (byte) 2
        //};
        //static readonly byte[] messageHeader = udpHeader0xF3;

        public static byte[] SerializeOperationResponse(MessageBase response)
        {
            MemoryStream SerializeMemStream = new MemoryStream();
            //byte[] abc = SerializeOperationToMessage(0, push.ToDictionary(), EgMessageType.Message, false);
            Client.Photon.Protocol.SerializeOperationResponse(SerializeMemStream, new OperationResponse()
            {
                DebugMessage = "",
                OperationCode = 0,
                Parameters = response.ToDictionary(),
                ReturnCode = 0
            }, false);
            return SerializeMemStream.ToArray();
        }

        public static OperationResponse DeserializeToResponse(byte[] data)
        {
            return Client.Photon.Protocol.DeserializeOperationResponse(new MemoryStream(data));
        }

        public static byte[] SerializeEventData(MessageBase push)
        {
            MemoryStream SerializeMemStream = new MemoryStream();
            //byte[] abc = SerializeOperationToMessage(0, push.ToDictionary(), EgMessageType.Message, false);
            Client.Photon.Protocol.SerializeEventData(SerializeMemStream, new EventData()
            {
                Code = 0,
                Parameters = push.ToDictionary()
            }, false);
            return SerializeMemStream.ToArray();
        }

        public static EventData DeserializeToEventData(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            return Client.Photon.Protocol.DeserializeEventData(ms);
        }
    }
}