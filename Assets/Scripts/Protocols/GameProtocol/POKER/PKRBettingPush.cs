using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using GameProtocol.Base;
using GameProtocol.Protocol;

namespace GameProtocol.PKR
{
    public class PKRBettingPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR5";
        }
        public PKRBettingPush()
        {
        }

        public PKRBettingPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CashBet, IsOptional = true)]
        public long CashBet { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.ActionId, IsOptional = true)]
        public short ActionId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.ActionName, IsOptional = true)]
        public string ActionName { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.IsEndRound, IsOptional = true)]
        public bool IsEndRound { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextPlayer, IsOptional = true)]
        public long PlayerNow { get; set; }

    }
}
