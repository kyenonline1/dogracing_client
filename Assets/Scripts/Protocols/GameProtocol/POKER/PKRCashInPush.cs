using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRCashInPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR0";
        }
        public PKRCashInPush()
        {
        }

        public PKRCashInPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
    }
}
