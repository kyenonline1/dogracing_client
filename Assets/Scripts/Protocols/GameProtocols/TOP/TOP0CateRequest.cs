using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.TOP
{

    public class TOP0CateRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "TOP0";
        }

        public TOP0CateRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public TOP0CateRequest()
        {
        }
    }
}
