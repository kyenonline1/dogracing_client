using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.EVN
{
    public class EVN7FirstChargingReceiverRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "EVN7";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.FirstChargingId, IsOptional = false)]
        public int FirstChargingId { get; set; }

        public EVN7FirstChargingReceiverRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public EVN7FirstChargingReceiverRequest()
        {
        }
    }
}
