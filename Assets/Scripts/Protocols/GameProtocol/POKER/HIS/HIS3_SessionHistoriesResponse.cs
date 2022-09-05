using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
namespace GameProtocol.HIS
{
    public class HIS3_SessionHistoriesResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS3";
        }

        public HIS3_SessionHistoriesResponse()
        {
        }

        public HIS3_SessionHistoriesResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        [DataMember(Code = (byte)HIS_ParameterCode.TableId, IsOptional = true)]
        public int TableId { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.GameId, IsOptional = true)]
        public string GameId { get; set; }

        [DataMember(Code = (byte)HIS_ParameterCode.Gamesession, IsOptional = true)]
        public int[] Sessions { get; set; }
    }
}