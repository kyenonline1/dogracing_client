using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.XIT
{
    public class XITOBettingRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT5";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.CashBet, IsOptional = true)]
        public long CashBet { get; set; } //-1 : Fail

        public XITOBettingRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOBettingRequest()
        {
        }
    }
}
