using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DOG
{
    public class DOG0GetInfoRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG0";
        }

        public DOG0GetInfoRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG0GetInfoRequest()
        {
        }
    }
}
