using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.MAU
{
    public class MAUTableInfoRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU4";
        }

        public MAUTableInfoRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public MAUTableInfoRequest()
        {
        }
    }
}
