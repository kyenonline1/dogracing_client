using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.ATH
{
    public class ATH0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "ATH0";
        }

        public ATH0_Response(Dictionary<byte, object> dict) : base(dict)
        {

        }

        [DataMember(Code = (byte)ATH_ParameterCode.UserId, IsOptional = true)]
        public long UserID { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Nickname, IsOptional = true)]
        public string Nickname { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Silver, IsOptional = true)]
        public long Silver { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Gold, IsOptional = true)]
        public long Gold { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.GoldSafe, IsOptional = true)]
        public long GoldSafe { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.TableId, IsOptional = true)]
        public long TableId { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.GameId, IsOptional = true)]
        public string GameId { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Session, IsOptional = true)]
        public string Session { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.CurrentVip, IsOptional = true)]
        public int CurrentVip { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.MaxVip, IsOptional = true)]
        public int MaxVip { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.Avatar, IsOptional = true)]
        public string Avatar { get; set; }
        
        [DataMember(Code = (byte)ATH_ParameterCode.VipType, IsOptional = true)]
        public byte VipType { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.VipName, IsOptional = true)]
        public string VipName { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.PhoneNumber, IsOptional = true)]
        public string PhoneNumber { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.TotalCharging, IsOptional = true)]
        public int TotalCharging { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.TotalMomo, IsOptional = true)]
        public int TotalMomo { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.TotalSpin, IsOptional = true)]
        public int TotalSpin { get; set; }

        [DataMember(Code = (byte)ATH_ParameterCode.ClubId, IsOptional = true)]
        public int ClubId { get; set; }

        /// <summary>
        /// 0 : register 1: member 2 : admin 3 : owner
        /// </summary>
        [DataMember(Code = (byte)ATH_ParameterCode.ClubMemberType, IsOptional = true)]
        public int ClubMemberType { get; set; }// 0 : register 1: member 2 : admin 3 : owner
    }
}
