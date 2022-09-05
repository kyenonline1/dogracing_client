using GameProtocol.Base;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using System.Reflection;

namespace GameProtocol.Protocol
{

    public class ResponseMessage : MessageBase {
        public ResponseMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest, SendParameters sendParameters)
            : base(rpcProtocol, operationRequest, sendParameters)
        {
            Flag = 0;
        }

        public ResponseMessage(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
            Flag = 0;
        }
        public ResponseMessage(Dictionary<byte, object> dict)
            : base(dict) 
        {
            Flag = 0;
        }
        public ResponseMessage()
            : base() 
        {
            Flag = 0;
        }


        [DataMember(Code = (byte)ParameterCode.ErrorCode, IsOptional = true)]
        public short ErrorCode { get; set; }

#if SERVER
        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
      // public TransString ErrorMsg { get; set; }
        public string ErrorMsg { get; set; }
#else//if CLIENT
        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
        public string ErrorMsg { get; set; }
#endif
    }
}
