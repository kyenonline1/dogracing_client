using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.PKR
{
    public class PKROpenCardRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR6";
        }
        public PKROpenCardRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKROpenCardRequest()
        {
        }
    }
}
