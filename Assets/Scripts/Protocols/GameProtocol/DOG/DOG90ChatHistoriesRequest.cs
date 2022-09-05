using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DOG
{
    public class DOG90ChatHistoriesRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "AIG90";
        }

        public DOG90ChatHistoriesRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG90ChatHistoriesRequest() { }
    }
}
