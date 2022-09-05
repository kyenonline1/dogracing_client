using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.XIT
{

    public class XITOCashInPush : PushBase
    {
        public XITOCashInPush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "XIT0";
        }
        [DataMember(Code = (byte)XIT_ParameterCode.CashIn, IsOptional = true)]
        public long CashIn { get; set; }

        [DataMember(Code = (byte)XIT_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }
    }
}
