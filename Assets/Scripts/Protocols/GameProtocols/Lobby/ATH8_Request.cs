using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class ATH8_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH8";
        }

        public ATH8_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH8_Request()
        {
        }
    }
}
