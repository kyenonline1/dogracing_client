using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProtocol.MOM
{
    public class MOM0_Response : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HOL0";
        }

        public MOM0_Response(Dictionary<byte, object> data) : base(data)
        {
            CodeRun = "HOL0";
        }

        [DataMember(Code = (byte)MOM_ParameterCode.NumberPhone, IsOptional = true)]
        public string NumberPhone { get; set; }

        [DataMember(Code = (byte)MOM_ParameterCode.UserName, IsOptional = true)]
        public string UserName { get; set; }


        [DataMember(Code = (byte)MOM_ParameterCode.Rate, IsOptional = true)]
        public float Rate { get; set; }

        [DataMember(Code = (byte)MOM_ParameterCode.Content, IsOptional = true)]
        public string Content { get; set; }

        [DataMember(Code = (byte)MOM_ParameterCode.MinCashIn, IsOptional = true)]
        public int MinCashIn { get; set; }

        [DataMember(Code = (byte)MOM_ParameterCode.MaxCashIn, IsOptional = true)]
        public int MaxCashIn { get; set; }


    }
}
