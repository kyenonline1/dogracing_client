using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOShowHandPush : PushBase
    {
        public XITOShowHandPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT8";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerCard[] Players { get; set; }
    }
}
