using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.PKR
{
    public class PKR21RefundInsurancePush : PushBase
    {
        public PKR21RefundInsurancePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PKR21";
        }

        [DataMember(Code = (byte)PKR_ParameterCode.MoneyInsurance, IsOptional = true)]
        public long MoneyInsurance { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

    }
}
