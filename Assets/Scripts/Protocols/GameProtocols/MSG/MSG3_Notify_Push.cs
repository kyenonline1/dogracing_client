
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG3_Notify_Push : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG3";
        }

        [DataMember(Code = (byte)MSG_ParameterCode.Message, IsOptional = false)]
        public string Message { get; set; }

        [DataMember(Code = (byte)MSG_ParameterCode.Type, IsOptional = false)]
        public byte Type { get; set; }//0 : Cộng tiền, 1 : Khóa user

        [DataMember(Code = (byte)MSG_ParameterCode.CurrentCash, IsOptional = false)]
        public long CurrentCash { get; set; }

        public MSG3_Notify_Push(Dictionary<byte, object> data) : base(data)
        {
            CodeRun = "MSG3";
        }
    }
}
