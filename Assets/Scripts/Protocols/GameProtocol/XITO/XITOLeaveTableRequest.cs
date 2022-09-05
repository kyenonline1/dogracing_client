using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.XIT
{
    public class XITOLeaveTableRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT6";
        }

        public XITOLeaveTableRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOLeaveTableRequest()
        {
        }
    }
}
