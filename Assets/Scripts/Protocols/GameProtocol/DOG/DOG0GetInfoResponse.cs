using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG0GetInfoResponse : ResponseBase
    {
        private Dictionary<byte, object> data;

        [DataMember(Code = (byte)DOG_ParameterCode.GameSession, IsOptional = true)]
        public long GameSession { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.GameState, IsOptional = true)]
        public byte GameState { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.CurrentBets, IsOptional = true)]
        public DogSlot[] CurrentBets { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.TotalBets, IsOptional = true)]
        public DogSlot[] TotalBets { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.DogRacings, IsOptional = true)]
        public DogRacing[] DogRacings { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.Players, IsOptional = true)]
        public PlayerDog[] Players { get; set; }

        public override void SetCodeRun()
        {
            CodeRun = "DOG0";
        }

        public DOG0GetInfoResponse()
        {
            Flag = 0;
        }

        public DOG0GetInfoResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
