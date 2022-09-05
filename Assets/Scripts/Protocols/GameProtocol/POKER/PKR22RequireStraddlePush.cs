using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameProtocol.PKR
{
    public class PKR22RequireStraddlePush : PushBase
    {
        public PKR22RequireStraddlePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "PKR22";
        }

        [DataMember(Code = (byte)PKR_ParameterCode.CashBet, IsOptional = true)]
        public long CashBet { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.TurnTime, IsOptional = true)]
        public long TurnTime { get; set; }

    }
}
