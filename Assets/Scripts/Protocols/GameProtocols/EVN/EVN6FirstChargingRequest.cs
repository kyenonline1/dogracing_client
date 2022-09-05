using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.EVN
{
    public class EVN6FirstChargingRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN6";
        }

        public EVN6FirstChargingRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN6FirstChargingRequest()
        {
        }
    }
}
