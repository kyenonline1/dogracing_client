
using GameProtocol.Protocol;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.ACC
{
    [Serializable]
    public class ACC1_Request : RequestBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ACC1";
        }

        [DataMember(Code = (byte)ACC_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Username, IsOptional = true)]
        public string Username { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Password, IsOptional = true)]
        public string Password { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.NumberPhone, IsOptional = true)]
        public string NumberPhone { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Email, IsOptional = true)]
        public string Email { get; set; }

        public ACC1_Request(IRpcProtocol protocol, OperationRequest operationRequest) : base(protocol, operationRequest)
        {
        }

        public ACC1_Request()
        {
        }
    }
}
