using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.MAU
{
    public class MAUStartGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "MAU10";
        }

        public MAUStartGamePush()
        {

        }

        public MAUStartGamePush(Dictionary<byte, object> data) : base(data)
        {

        }

        [DataMember(Code = (byte)MAU_ParameterCode.Players, IsOptional = true)]
        public MauBinhPlayer[] Players { get; set; }
        
        [DataMember(Code = (byte)MAU_ParameterCode.RankId, IsOptional = true)]
        public int RankId { get; set; }

        [DataMember(Code = (byte)MAU_ParameterCode.Rank, IsOptional = true)]
        public string Rank { get; set; }
    }
}
