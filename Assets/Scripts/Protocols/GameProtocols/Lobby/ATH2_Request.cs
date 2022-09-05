using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.ATH
{
    public class ATH2_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH2";
        }

        [DataMember(Code = (byte)ATH_ParameterCode.Username, IsOptional = true)]
        public string Username { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Password, IsOptional = true)]
        public string Password { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Captcha, IsOptional = true)]
        public string Captcha { get; set; }

        public ATH2_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH2_Request()
        {
        }
    }
}
