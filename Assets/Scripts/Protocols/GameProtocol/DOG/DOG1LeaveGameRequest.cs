using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DOG
{
    public class DOG1LeaveGameRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG1";
        }

        public DOG1LeaveGameRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG1LeaveGameRequest() { }
    }
}
