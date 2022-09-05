using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DOG
{
    public class DOG8BettingHistoriesRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG8";
        }

        public DOG8BettingHistoriesRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG8BettingHistoriesRequest() { }
    }
}
