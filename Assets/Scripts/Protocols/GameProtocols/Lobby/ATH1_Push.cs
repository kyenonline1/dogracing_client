
using System;
using System.Collections.Generic;
using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;

namespace GameProtocol.ATH
{
    
    public class ATH1_Push : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH1";
        }

        public ATH1_Push(Dictionary<byte, object> dict) : base(dict)
        {

        }


        [DataMember(Code = (byte)ATH_ParameterCode.Message, IsOptional = true)]
        public string Message { get; set; }
    }
}
