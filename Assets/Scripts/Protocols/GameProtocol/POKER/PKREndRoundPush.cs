using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKREndRoundPush : PushBase
    {
        public PKREndRoundPush()
        {
        }

        public PKREndRoundPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PKR9";
        }
        [DataMember(Code = (byte)PKR_ParameterCode.RoundName, IsOptional = true)]
        public string RoundName { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CardsRank, IsOptional = true)]
        public int CardsRank { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CommuniCard, IsOptional = true)]
        public int[] CommonCards { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.NextPlayer, IsOptional = true)]
        public long PlayerNow { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CenterCash, IsOptional = true)]
        public long[] CenterCash { get; set; }
    }
}
