using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.OTP
{
    public class OTP0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "OTP0";
        }

        [DataMember(Code = (byte)OTP_ParameterCode.NumberPhone, IsOptional = true)]
        public string NumberPhone { get; set; }
        

        public OTP0_Request(IRpcProtocol protocol, OperationRequest operationRequest) :
            base(protocol, operationRequest)
        {
        }

        public OTP0_Request()
        {
        }
    }
}
