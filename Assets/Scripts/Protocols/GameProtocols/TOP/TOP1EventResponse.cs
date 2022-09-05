using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.TOP
{
    public class TOP1EventResponse : ResponseBase
    {
        public TOP1EventResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "TOP1";
        }

        [DataMember(Code = (byte)TOP_ParameterCode.BETs, IsOptional = true)]
        public TopEvent[] BETs { get; set; }

        [DataMember(Code = (byte)TOP_ParameterCode.NLHs, IsOptional = true)]
        public TopEvent[] NLHs { get; set; }

        [DataMember(Code = (byte)TOP_ParameterCode.MTTs, IsOptional = true)]
        public TopEvent[] MTTs { get; set; }

        [DataMember(Code = (byte)TOP_ParameterCode.CLUBs, IsOptional = true)]
        public TopEvent[] CLUBs { get; set; }

        [DataMember(Code = (byte)TOP_ParameterCode.TimeRemain, IsOptional = true)]
        public string TimeRemain { get; set; }
    }
}
