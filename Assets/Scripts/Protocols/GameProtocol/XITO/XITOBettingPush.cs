using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{
    public class XITOBettingPush : PushBase
    {
        public XITOBettingPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT5";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CashBet, IsOptional = true)]
        public long CashBet { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.ActionId, IsOptional = true)]
        public short ActionId { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.ActionName, IsOptional = true)]
        public string ActionName { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.IsEndRound, IsOptional = true)]
        public bool IsEndRound { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.NextPlayer, IsOptional = true)]
        public long NextPlayer { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CenterCash, IsOptional = true)]
        public long CenterCash { get; set; }
    }
}
