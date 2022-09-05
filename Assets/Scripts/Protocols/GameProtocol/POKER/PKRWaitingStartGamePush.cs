using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRWaitingStartGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR3";
        }

        public PKRWaitingStartGamePush()
        {
        }

        public PKRWaitingStartGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.RemainTime, IsOptional = true)]
        public int CountDownTime { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }
    
}
