using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;

namespace GameProtocol.EVN
{
    public class EVN4_DailyMissionRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN4";
        }

        public EVN4_DailyMissionRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN4_DailyMissionRequest()
        {
        }
    }
}
