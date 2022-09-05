using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{

    public class XITOGetInfoTableResponse : ResponseBase
    {
        public XITOGetInfoTableResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "SBI_XIT5";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.Players, IsOptional = true)]
        public XitoPlayerInfo[] Players { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.CenterCash, IsOptional = true)]
        public long CenterCash { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Blind, IsOptional = true)]
        public long Blind { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.MinCashIn, IsOptional = true)]
        public long MinCashIn { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.MaxCashIn, IsOptional = true)]
        public long MaxCashIn { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.PlayerNow, IsOptional = true)]
        public long PlayerNow { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.TurnTime, IsOptional = true)]
        public long TurnTime { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.TableState, IsOptional = true)]
        public int TableState { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.Gamesession, IsOptional = true)]
        public long Gamesession { get; set; }
    }
}
