using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class ATH3_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH3";
        }
        public ATH3_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH3_Request()
        {
        }
    }
}
