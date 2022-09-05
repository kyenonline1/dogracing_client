

using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace GameProtocol.OTP
{
    public class OTP1_Request : RequestBase
    {

        public override void SetCodeRun()
        {
            CodeRun = "OTP1";
        }
        [DataMember(Code = (byte)OTP_ParameterCode.UserName, IsOptional = true)]
        public string UserName { get; set; }
        [DataMember(Code = (byte)OTP_ParameterCode.NumberPhone, IsOptional = true)]
        public string NumberPhone { get; set; }

        public OTP1_Request(IRpcProtocol protocol, OperationRequest operationRequest) :
           base(protocol, operationRequest)
        {
        }

        public OTP1_Request()
        {
        }
    }
}
