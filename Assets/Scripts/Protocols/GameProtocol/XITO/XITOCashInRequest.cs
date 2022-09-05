using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.XIT
{
    public class XITOCashInRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT0";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        public XITOCashInRequest(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public XITOCashInRequest()
        {
        }
    }
}
