using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUStartCountDownPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU9";
        }

        public MAUStartCountDownPush()
        {

        }

        public MAUStartCountDownPush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.CountDownTime, IsOptional = true)]
        public int CountDownTime { get; set; }
    }
}
