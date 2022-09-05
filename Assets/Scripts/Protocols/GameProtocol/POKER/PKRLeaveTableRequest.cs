using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PKR
{
    public class PKRLeaveTableRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR6";
        }

        public PKRLeaveTableRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKRLeaveTableRequest()
        {
        }
    }
}
