
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG0_Push : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG0";
        }

        public MSG0_Push(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MSG_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.Type, IsOptional = true)]
        public byte Type { get; set; }
    }
}
