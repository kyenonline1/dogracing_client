using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class ATH7_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH7";
        }

        public ATH7_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH7_Request()
        {
        }
    }
}
