
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.PAY
{

    public class PAY2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY2";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Type, IsOptional = true)]
        public string Type { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Seri, IsOptional = true)]
        public string Seri { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.CardNumber, IsOptional = true)]
        public string CardNumber { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Gold, IsOptional = true)]
        public int Amount { get; set; }

        /// <summary>
        /// TransId thẻ đã đổi/ dành cho nạp lại
        /// </summary>
        [DataMember(Code = (byte)PAY_ParameterCode.Transaction, IsOptional = true)]
        public int TransId { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Capcha, IsOptional = true)]
        public string Capcha { get; set; }

        public PAY2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public PAY2_Request()
        {
        }
    }
}
