using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.LKC
{
    public class LKC0_Response : ResponseBase
    {
        public LKC0_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "LKC0";
        }

        [DataMember(Code = (byte)LKC_ParameterCode.Card1, IsOptional = true)]
        public int Card1 { get; set; }

        [DataMember(Code = (byte)LKC_ParameterCode.Card2, IsOptional = true)]
        public int Card2 { get; set; }


        [DataMember(Code = (byte)LKC_ParameterCode.Fund, IsOptional = true)]
        public long Fund { get; set; }

        [DataMember(Code = (byte)LKC_ParameterCode.Hits, IsOptional = true)]
        public LuckyCardHits[] Hits { get; set; }
    }
}