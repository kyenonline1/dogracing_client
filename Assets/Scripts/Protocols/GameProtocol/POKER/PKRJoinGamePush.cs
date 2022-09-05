using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRJoinGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR2";
        }

        public PKRJoinGamePush()
        {
        }

        public PKRJoinGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.TableId, IsOptional = true)]
        public long TableId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        [DataMember(Code = (byte)ParameterCode.ErrorCode, IsOptional = true)]
        public short ErrorCode { get; set; }

        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
        public string ErrorMsg { get; set; }

    }
}
