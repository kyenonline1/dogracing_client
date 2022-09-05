using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.ATH
{
    public class ATH9_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH9";
        }

        [DataMember(Code = (byte)ATH_ParameterCode.GiftCode, IsOptional = true)]
        public string GiftCode { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Captcha, IsOptional = true)]
        public string Captcha { get; set; }

        public ATH9_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH9_Request()
        {
        }
    }
}
