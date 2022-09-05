
using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameProtocol.PAY
{
    public class PAY0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PAY0";
        }
        [DataMember(Code = (byte)PAY_ParameterCode.Cate, IsOptional = true)]
        public CateCharging[] Cates { get; set; }

        [DataMember(Code = (byte)PAY_ParameterCode.CardRate, IsOptional = true)]
        public float CardRate { get; set; }

        public PAY0_Response(Dictionary<byte, object> data) : base(data)
        {
             
        }
    }
}
