using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOAutoOpenCardPush : PushBase
    {
        public XITOAutoOpenCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT12";
        }

        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerCard[] Players { get; set; }
    }
}
