using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG2_Push : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG2";
        }

        [DataMember(Code = (byte)MSG_ParameterCode.Message, IsOptional = false)]
        public string[] Messages { get; set; }

        public MSG2_Push(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
