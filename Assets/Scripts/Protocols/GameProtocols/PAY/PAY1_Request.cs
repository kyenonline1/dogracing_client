
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.PAY
{
    [Serializable]
    public class PAY1_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY1";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Type, IsOptional = true)]
        public string Type { get; set; }

        public PAY1_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY1_Request()
        {
        }
    }
}
