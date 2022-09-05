using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN5_DailyReceiverResponse : ResponseBase
    {
        public EVN5_DailyReceiverResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "EVN5";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Gold, IsOptional = false)]
        public long CurrentRuby { get; set; }
    }
}
