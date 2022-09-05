using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System;

namespace GameProtocol.ATH
{
    public class PNG0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PNG0";
        }
        public PNG0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PNG0_Request()
        {
        }
    }
}
