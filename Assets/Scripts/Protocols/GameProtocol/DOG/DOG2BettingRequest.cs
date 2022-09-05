using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.DOG
{
    public class DOG2BettingRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG2";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.CashBet, IsOptional = false)]
        public int CashBet { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.SlotId, IsOptional = false)]
        public int SlotId { get; set; }

        public DOG2BettingRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public DOG2BettingRequest() { }
    }
}
