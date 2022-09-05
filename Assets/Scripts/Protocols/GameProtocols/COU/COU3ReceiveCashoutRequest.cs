
using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.COU
{
    /// <summary>
    /// Lấy thông tin thẻ đổi thưởng
    /// </summary>
    public class COU3ReceiveCashoutRequest : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "COU3";
        }
        public COU3ReceiveCashoutRequest(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public COU3ReceiveCashoutRequest()
        {
        }

        [DataMember(Code = (byte)COU_ParameterCode.TransactionId, IsOptional = true)]
        public int TransactionId { get; set; }
    }
}
