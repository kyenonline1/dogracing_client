using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAULeaveTableRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU5";
        }

        public MAULeaveTableRequest()
        {

        }

        public MAULeaveTableRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }
    }
}
