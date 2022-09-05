using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.XIT
{
    public class XITOGetInfoTableRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT5";
        }

        public XITOGetInfoTableRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOGetInfoTableRequest()
        {
        }
    }
}
