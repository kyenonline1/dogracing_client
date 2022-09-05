using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUSortCardResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU11";
        }

        public MAUSortCardResponse()
        {

        }

        public MAUSortCardResponse(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.RankId, IsOptional = true)]
        public int RankId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Rank, IsOptional = true)]
        public string Rank { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Sortable, IsOptional = true)]
        public bool Sortable { get; set; }
    }
}
