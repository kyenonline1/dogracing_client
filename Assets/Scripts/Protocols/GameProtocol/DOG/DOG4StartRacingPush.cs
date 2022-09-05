using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.DOG
{
    public class DOG4StartRacingPush : PushBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "DOG4";
        }

        [DataMember(Code = (byte)DOG_ParameterCode.DogRacings, IsOptional = true)]
        public DogRacing[] DogRacings { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.RemainTime, IsOptional = true)]
        public long RemainTime { get; set; }

        [DataMember(Code = (byte)DOG_ParameterCode.WinSlots, IsOptional = true)]
        public int[] WinSlots { get; set; }

        public DOG4StartRacingPush()
        {
            Flag = 0;
        }

        public DOG4StartRacingPush(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}
