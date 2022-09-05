using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUReadyPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU8";
        }

        public MAUReadyPush()
        {

        }

        public MAUReadyPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.IsReady, IsOptional = true)]
        public bool IsReady { get; set; }
    }
}
