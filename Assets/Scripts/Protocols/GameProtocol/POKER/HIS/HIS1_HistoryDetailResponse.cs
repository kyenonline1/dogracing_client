using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
namespace GameProtocol.HIS
{
    public class HIS1_HistoryDetailResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS1";
        }

        public HIS1_HistoryDetailResponse()
        {
        }

        public HIS1_HistoryDetailResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)HIS_ParameterCode.Histories, IsOptional = true)]
        public TurnHistory[] Histories { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.NumberSlot, IsOptional = true)]
        public byte NumberSlot { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.Blind, IsOptional = true)]
        public int Blind { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.StartTime, IsOptional = true)]
        public string StartTime { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.Ante, IsOptional = true)]
        public int Ante { get; set; }
    }
}