using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.XIT
{
    public class XITOOpenCardRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT6";
        }

        public XITOOpenCardRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOOpenCardRequest()
        {
        }
    }
}
