using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.PKR
{
    public class PKR20StateInsurancePush : PushBase
    {
        public PKR20StateInsurancePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PKR20";
        }


        [DataMember(Code = (byte)PKR_ParameterCode.RemainTime, IsOptional = true)]
        public int RemainTime { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }
}
