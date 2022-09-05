using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOJoinGamePush : PushBase
    {
        public XITOJoinGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT2";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.TableId, IsOptional = true)]
        public long TableId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Position, IsOptional = true)]
        public int Position { get; set; }

        [DataMember(Code = (byte)ParameterCode.ErrorCode, IsOptional = true)]
        public short ErrorCode { get; set; }

        [DataMember(Code = (byte)ParameterCode.ErrorMsg, IsOptional = true)]
        public string ErrorMsg { get; set; }
    }
}
