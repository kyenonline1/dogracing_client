using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOHandCardPush : PushBase
    {
        public XITOHandCardPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT4";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.RoundName, IsOptional = true)]
        public string RoundName { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.NextPlayer, IsOptional = true)]
        public long NextPlayer { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerCard[] Players { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CenterCash, IsOptional = true)]
        public long CenterCash { get; set; }
    }

}
