
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.SLC
{
    public class SLC4_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "SLC4";
        }
        [DataMember(Code = (byte)SLC_ParameterCode.GameId, IsOptional = true)]
        public string GameId { get; set; }

        [DataMember(Code = (byte)SLC_ParameterCode.TopGame, IsOptional = true)]
        public TopGame[] Tops { get; set; }

        public SLC4_Response(Dictionary<byte, object> data) : base(data)
        {

        }
    }
}
