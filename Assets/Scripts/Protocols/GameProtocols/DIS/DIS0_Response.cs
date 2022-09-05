using System.Collections.Generic;
using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;

namespace GameProtocol.DIS
{
    public class DIS0_Response : ResponseBase
    {
        public DIS0_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "DIS0";
        }
        [DataMember(Code = (byte)DIS_ParameterCode.Distributor, IsOptional = true)]
        public Distributor[] Data { get; set; }

        [DataMember(Code = (byte)DIS_ParameterCode.Rate, IsOptional = true)]
        public float Rate { get; set; }
    }
}
