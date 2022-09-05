
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.ACC
{
    public class ACC0_Response : ResponseBase
    {
        public ACC0_Response(Dictionary<byte, object> data) : base(data)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "ACC0";
        }
        [DataMember(Code = (byte)ACC_ParameterCode.UserId, IsOptional = true)]
        public long UserId { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Silver, IsOptional = true)]
        public long Silver { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.GoldSafe, IsOptional = true)]
        public long GoldSafe { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Level, IsOptional = true)]
        public int Level { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Exp, IsOptional = true)]
        public int Exp { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.CurrentVip, IsOptional = true)]
        public int CurrentVip { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.MaxVip, IsOptional = true)]
        public int MaxVip { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.PhoneNumber, IsOptional = true)]
        public string PhoneNumber { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.AvatarConfigs, IsOptional = true)]
        public int[] AvatarConfigs { get; set; }

        [DataMember(Code = (byte)ACC_ParameterCode.Email, IsOptional = true)]
        public string Email { get; set; }


    }
}
