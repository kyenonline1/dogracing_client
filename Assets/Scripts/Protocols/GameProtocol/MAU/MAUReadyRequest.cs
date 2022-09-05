using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.MAU
{
    public class MAUReadyRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU7";
        }

        public MAUReadyRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MAUReadyRequest()
        {
        }
    }
}
