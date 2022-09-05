using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DIS
{
    public class DIS0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DIS0";
        }

        public DIS0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DIS0_Request()
        {
        }
    }
}
