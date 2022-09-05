using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.HIS
{
    public class HIS0_HistoriesResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS0";
        }

        public HIS0_HistoriesResponse()
        {
        }

        public HIS0_HistoriesResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)HIS_ParameterCode.Histories, IsOptional = true)]
        public PokerHistory[] Histories { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.TotalPage, IsOptional = true)]
        public byte TotalPage { get; set; }
    }
}