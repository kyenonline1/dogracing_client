using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.PNG
{
    public class PNG0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PNG0";
        }
        
        public PNG0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PNG0_Request()
        {
            CodeRun = "PNG0";
        }
    }
}
