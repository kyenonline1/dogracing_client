using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.COU
{
    /// <summary>
    /// Nạp lại
    /// </summary>
    public class COU4ReChargingRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU4";
        }
        public COU4ReChargingRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public COU4ReChargingRequest()
        {
        }

        [DataMember(Code = (byte)COU_ParameterCode.TransactionId, IsOptional = true)]
        public int TransactionId { get; set; }
    }
}
