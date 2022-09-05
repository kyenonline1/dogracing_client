using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.EVN
{
    public class EVN5_DailyReceiverRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN5";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Mission, IsOptional = false)]
        public int MissionId { get; set; }

        public EVN5_DailyReceiverRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN5_DailyReceiverRequest()
        {
        }
    }
}
