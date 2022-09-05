using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.TOP
{
    public class TOP0CateResponse : ResponseBase
    {
        public TOP0CateResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "TOP0";
        }

        [DataMember(Code = (byte)TOP_ParameterCode.Cates, IsOptional = true)]
        public TopCate[] Cates { get; set; }

        [DataMember(Code = (byte)TOP_ParameterCode.Seasons, IsOptional = true)]
        public string[] Seasons { get; set; }
    }
}
