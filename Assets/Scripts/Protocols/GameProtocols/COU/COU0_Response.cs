using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.COU
{
    public class COU0_Response : ResponseBase
    {
        public COU0_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "COU0";
        }

        [DataMember(Code = (byte)COU_ParameterCode.Items, IsOptional = true)]
        public TelcoDetail[] telcoDetails { get; set; }

        [DataMember(Code = (byte)COU_ParameterCode.Rate, IsOptional = true)]
        public float Rate { get; set; }
    }
}
