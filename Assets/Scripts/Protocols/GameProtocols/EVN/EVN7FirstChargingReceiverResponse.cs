using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.EVN
{
    public class EVN7FirstChargingReceiverResponse : ResponseBase
    {
        public EVN7FirstChargingReceiverResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "EVN7";
        }

        [DataMember(Code = (byte)EVN_ParameterCode.Gold, IsOptional = false)]
        public long CurrentRuby { get; set; }
    }
}
