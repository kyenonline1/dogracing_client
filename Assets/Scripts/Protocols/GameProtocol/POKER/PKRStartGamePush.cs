using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.PKR
{

    public class PKRStartGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR2";
        }

        public PKRStartGamePush()
        {
        }

        public PKRStartGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Players, IsOptional = true)]
        public PokerPlayer[] Players { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Dealer, IsOptional = true)]
        public long Dealer { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextPlayer, IsOptional = true)]
        public long PlayerNow { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.SmallBlind, IsOptional = true)]
        public long SmallBlind { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.BigBlind, IsOptional = true)]
        public long BigBlind { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Ante, IsOptional = true)]
        public int Ante { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TotalAnte, IsOptional = true)]
        public int TotalAnte { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TotalFreePot, IsOptional = true)]
        public long TotalFreePot { get; set; }
    }
}
