using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.DOG
{
    public class DOG20ClearBettingRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG20";
        }
        public DOG20ClearBettingRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG20ClearBettingRequest() { }
    }
}
