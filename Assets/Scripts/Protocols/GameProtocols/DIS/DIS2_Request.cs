using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.DIS
{
    public class DIS2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DIS2";
        }

        public DIS2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DIS2_Request()
        {
        }
    }
}
