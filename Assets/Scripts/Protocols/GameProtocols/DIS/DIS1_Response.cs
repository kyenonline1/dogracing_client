using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.DIS
{
    public class DIS1_Response : ResponseBase
    {
        public DIS1_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "DIS1";
        }

        [DataMember(Code = (byte)DIS_ParameterCode.Gold, IsOptional = true)]
        public long CurrentGold { get; set; }
    }
}
