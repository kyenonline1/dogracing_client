using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRGetInfoTableResponse : ResponseBase
    {
        public PKRGetInfoTableResponse()
        {
        }

        public PKRGetInfoTableResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR5";
        }
        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayer[] Players { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CommuniCard, IsOptional = true)]
        public int[] CommuniCard { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CenterCash, IsOptional = true)]
        public long[] CenterCash { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Blind, IsOptional = true)]
        public long Blind { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.MinCashIn, IsOptional = true)]
        public long MinCashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.MaxCashIn, IsOptional = true)]
        public long MaxCashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Dealer, IsOptional = true)]
        public long Delear { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextPlayer, IsOptional = true)]
        public long PlayerNow { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TurnTime, IsOptional = true)]
        public long TurnTime { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TableState, IsOptional = true)]
        public int TableState { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Gamesession, IsOptional = true)]
        public long Gamesession { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.BlindUp, IsOptional = true)]
        public long BlindUp { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.SlotNumber, IsOptional = true)]
        public int SlotNumber { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TimeIncrBlind, IsOptional = true)]
        public int TimeIncrBlind { get; set; }

        /// <summary>
        /// -1: normal, 0: Spin, 1: Tour
        /// </summary>
        [DataMember(Code = (byte)PKR_ParameterCode.Cate, IsOptional = true)]
        public int Cate { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.RemainTimeIncrBlind, IsOptional = true)]
        public long RemainTimeIncrBlind { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TotalAnte, IsOptional = true)]
        public int TotalAnte { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Ante, IsOptional = true)]
        public int CurrentAnte { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextAnte, IsOptional = true)]
        public int NextAnte { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TourId, IsOptional = true)]
        public int TourId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TableId, IsOptional = true)]
        public int TableId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TotalFreePot, IsOptional = true)]
        public long TotalFreePot { get; set; }

    }
}
