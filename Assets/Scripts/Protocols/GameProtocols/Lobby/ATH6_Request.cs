using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class ATH6_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH6";
        }

        public ATH6_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH6_Request()
        {
        }
    }
}
