

using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.OTP
{
    public class OTP0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "OTP0";
        }

        public OTP0_Response(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)OTP_ParameterCode.CurGold, IsOptional = true)]
        public long CurGold { get; set; }

        [DataMember(Code = (byte)OTP_ParameterCode.CurGoldSafe, IsOptional = true)]
        public long CurGoldSafe { get; set; }

    }
}
