using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DIS
{
    public class DIS2_Response : ResponseBase
    {
        public DIS2_Response()
        {

        }
        public DIS2_Response(Dictionary<byte, object> dict) : base(dict)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "DIS2";
        }

        [DataMember(Code = (byte)DIS_ParameterCode.Histories, IsOptional = true)]
        public TranferHistory[] Data { get; set; }
    }
}
