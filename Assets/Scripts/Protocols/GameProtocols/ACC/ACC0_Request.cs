
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ACC
{
    public class ACC0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ACC0";
        }
        [DataMember(Code = (byte)ACC_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        public ACC0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ACC0_Request()
        {
        }
    }
}
