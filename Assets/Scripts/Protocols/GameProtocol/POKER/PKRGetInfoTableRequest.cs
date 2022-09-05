using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.PKR
{
    public class PKRGetInfoTableRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR5";
        }

        public PKRGetInfoTableRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKRGetInfoTableRequest()
        {
        }
    }
}
