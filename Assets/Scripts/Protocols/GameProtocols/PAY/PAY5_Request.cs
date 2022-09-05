using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PAY
{
    public class PAY5_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY5";
        }
        
        public PAY5_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY5_Request()
        {
        }
    }
}
