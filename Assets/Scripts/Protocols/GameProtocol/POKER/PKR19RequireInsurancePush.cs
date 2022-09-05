using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.PKR
{
    public class PKR19RequireInsurancePush : PushBase
    {

        public PKR19RequireInsurancePush()
        {

        }

        public PKR19RequireInsurancePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PKR19";
        }
        [DataMember(Code = (byte)PKR_ParameterCode.TotalBet, IsOptional = true)]
        public long TotalBet { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.RemainTime, IsOptional = true)]
        public int RemainTime { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.HandCards, IsOptional = true)]
        public int[] HandCards { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CenterCards, IsOptional = true)]
        public int[] CenterCards { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Nickname, IsOptional = true)]
        public string OtherPlayer { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.OtherCards, IsOptional = true)]
        public int[] OtherCards { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.RankId, IsOptional = true)]
        public byte RankId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.OtherRankId, IsOptional = true)]
        public byte OtherRankId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.OutsCards, IsOptional = true)]
        public int[] OutsCards { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Odds, IsOptional = true)]
        public float Odds { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.BigBlind, IsOptional = true)]
        public int BigBlind { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Pot, IsOptional = true)]
        public int Pot { get; set; }

    }
}
