using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.PKR
{
    public class PKR1StandUpRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR1";
        }
        public PKR1StandUpRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PKR1StandUpRequest()
        {
        }
    }
}
