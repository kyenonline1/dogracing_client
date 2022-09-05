using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DOG
{
    public class DOG7DogRacingHistoriesRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG7";
        }

        public DOG7DogRacingHistoriesRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG7DogRacingHistoriesRequest() { }
    }
}
