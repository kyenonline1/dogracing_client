using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.EVN
{
    public class EVN9MissionReceiveRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN9";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Mission, IsOptional = false)]
        public int MissionId { get; set; }

        public EVN9MissionReceiveRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN9MissionReceiveRequest()
        {
        }
    }
}
