
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PAY
{
    public class PAY3_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY3";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.ProductId, IsOptional = true)]
        public string ProductId { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.ReceiptData, IsOptional = true)]
        public string ReceiptData { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Transaction, IsOptional = true)]
        public long Transaction { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.PurchaseDate, IsOptional = true)]
        public string PurchaseDate { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Quantity, IsOptional = true)]
        public string Quantity { get; set; }

        public PAY3_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY3_Request()
        {
        }

        
    }
}
