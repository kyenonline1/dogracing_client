using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ATH
{
    public class ATH0_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH0";
        }
        [DataMember(Code = (byte)ATH_ParameterCode.Session, IsOptional = true)]
        public string Session { get; set; }
        [DataMember(Code = (byte)ATH_ParameterCode.Username, IsOptional = true)]
        public string Username { get; set; }
        [DataMember(Code = (byte)ATH_ParameterCode.Password, IsOptional = true)]
        public string Password { get; set; }

        public ATH0_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ATH0_Request()
        {
        }
    }
}
