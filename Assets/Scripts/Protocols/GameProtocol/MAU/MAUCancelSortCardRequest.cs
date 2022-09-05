using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.MAU
{
    public class MAUCancelSortCardRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU14";
        }

        public MAUCancelSortCardRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MAUCancelSortCardRequest()
        {
        }
    }
}
