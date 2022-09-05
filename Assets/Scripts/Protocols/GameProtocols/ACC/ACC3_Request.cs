
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.ACC
{
    public class ACC3_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ACC3";
        }

        public ACC3_Request(Dictionary<byte, object> data) : base(data)
        {
        }

        [DataMember(Code = (byte)ACC_ParameterCode.Gold, IsOptional = true)]
        public long Chip { get; set; }

        public ACC3_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ACC3_Request()
        {
        }
    }
}
