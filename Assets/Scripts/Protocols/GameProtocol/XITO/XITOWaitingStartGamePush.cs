using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;

namespace GameProtocol.XIT
{
    public class XITOWaitingStartGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT3";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }

}
