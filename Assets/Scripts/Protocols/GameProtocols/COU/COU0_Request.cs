using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.COU
{
    public class COU0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU0";
        }

        public COU0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public COU0_Request()
        {
        }
    }
}
