using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PAY
{
    public class PAY6BankInfoResponse : ResponseBase
    {
        public PAY6BankInfoResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PAY6";
        }

        [DataMember(Code = (byte)PAY_ParameterCode.AccountId, IsOptional = true)]
        public string AccountId { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.AccountName, IsOptional = true)]
        public string AccountName { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.BankName, IsOptional = true)]
        public string BankName { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.BankFullName, IsOptional = true)]
        public string BankFullName { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.Branch, IsOptional = true)]
        public string Branch { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.TransferContent, IsOptional = true)]
        public string TransferContent { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.CardRate, IsOptional = true)]
        public float CardRate { get; set; }
    }
}
