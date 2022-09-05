using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOStartGamePush : PushBase
    {
        public XITOStartGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT2";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerInfo[] Players { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.NextPlayer, IsOptional = true)]
        public long NextPlayer { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CenterCash, IsOptional = true)]
        public long CenterCash { get; set; }
    }

    
    public class Player
    {
        public long UserId { get; set; }
        public long CashBet { get; set; }
        public long CashIn { get; set; }
        public short ActionId { get; set; }
        public string ActionName { get; set; }
        public int[] HandCards { get; set; }
    }
}
