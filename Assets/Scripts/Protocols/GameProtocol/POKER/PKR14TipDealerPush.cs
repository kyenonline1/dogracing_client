using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProtocol.PKR
{
    public class PKR14TipDealerPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR14";
        }
        public PKR14TipDealerPush()
        {
        }

        public PKR14TipDealerPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Gold, IsOptional = true)]
        public long CurrentGold { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.ActionId, IsOptional = true)]
        public int MoneyTip { get; set; }

    }
}
