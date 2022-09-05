
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG1_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG1";
        }

        [DataMember(Code = (byte)MSG_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.Type, IsOptional = true)]
        public byte Type { get; set; }

        public MSG1_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MSG1_Request()
        {
        }

        public MSG1_Request(Dictionary<byte, object> data) : base(data)
        {
        }
    }
}
