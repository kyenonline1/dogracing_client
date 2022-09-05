
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG3_Push : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG3";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.CurrentCash, IsOptional = false)]
        public long CurrentCash { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.WinCash, IsOptional = false)]
        public long WinCash { get; set; }

        public DOG3_Push(Dictionary<byte, object> dict) : base(dict)
        {
            Flag = 0;
        }
        
    }
}
