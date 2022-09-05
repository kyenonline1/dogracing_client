using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRLeaveTablePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR6";
        }

        public PKRLeaveTablePush()
        {
        }

        public PKRLeaveTablePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Silver, IsOptional = true)]
        public long Silver { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }
    }
}
