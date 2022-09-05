
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MSG
{
    public class MSG2_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MSG2";
        }

        public MSG2_Response(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MSG_ParameterCode.ChatDetails, IsOptional = true)]
        public ChatDetail[] Chats { get; set; }
    }
}
