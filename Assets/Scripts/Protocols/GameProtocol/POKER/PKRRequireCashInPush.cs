using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRRequireCashInPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR11";
        }

        public PKRRequireCashInPush()
        {
        }

        public PKRRequireCashInPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.Cash, IsOptional = true)]
        public long Cash { get; set; }

        //[DataMember(Code = (byte)PKR_ParameterCode.CashIn, IsOptional = true)]
        //public long Cashin { get; set; }
        
        [DataMember(Code = (byte)PKR_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.CashinRequired, IsOptional = true)]
        public int CashinRequired { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.RemainTime, IsOptional = true)]
        public int RemainTime { get; set; }
    }
}
