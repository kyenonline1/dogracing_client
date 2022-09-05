using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.PKR
{
    public class PKRRegisterLeaveTablePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SBI_PKR7";
        }

        public PKRRegisterLeaveTablePush()
        {
        }

        public PKRRegisterLeaveTablePush(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)PKR_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

		[DataMember(Code = (byte)PKR_ParameterCode.IsRegisterLeaveTable, IsOptional = true)]
		 public bool isRegisterLeaveTable { get; set; }

        [DataMember(Code = (byte)PKR_ParameterCode.SittingOut, IsOptional = true)]
        public bool SittingOut { get; set; }
    }
}
