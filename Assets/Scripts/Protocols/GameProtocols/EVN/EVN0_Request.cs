using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.EVN
{
    public class EVN0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN0";
        }

        public EVN0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN0_Request()
        {
        }
    }
}
