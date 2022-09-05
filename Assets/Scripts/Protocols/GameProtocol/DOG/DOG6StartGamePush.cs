using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG6StartGamePush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG6";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.GameState, IsOptional = true)]
        public byte GameState { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.Players, IsOptional = true)]
        public PlayerDog[] Players { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.WinFactors, IsOptional = true)]
        public DogSlot[] WinFactors { get; set; }

        public DOG6StartGamePush()
        {
            Flag = 0;
        }

        public DOG6StartGamePush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
