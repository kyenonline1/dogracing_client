

using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.ACC
{
    public class ACC3_Response : ResponseBase
    {

        public ACC3_Response(Dictionary<byte, object> data) : base(data)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "ACC3";
        }

        [DataMember(Code = (byte)ACC_ParameterCode.Gold, IsOptional = true)]
        public long GoldChip { get; set; }
        [DataMember(Code = (byte)ACC_ParameterCode.GoldSafe, IsOptional = true)]
        public long GoldSafe { get; set; }

    }
}
