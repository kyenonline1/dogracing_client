
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System;
using System.Collections.Generic;

namespace GameProtocol.UDT
{
    [Serializable]
    public class UDT0_Response : ResponseBase
    {
        public UDT0_Response(Dictionary<byte, object> data) : base(data)
        {
        }

        public override void SetCodeRun()
        {
            CodeRun = "UDT0";
        }
        [DataMember(Code = (byte)UDT_ParameterCode.BundleUrl, IsOptional = true)]
        public string BundleUrl { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.FacebookUrl, IsOptional = true)]
        public string FacebookUrl { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.IsAppFullFunction, IsOptional = true)]
        public bool IsAppFullFunction { get; set; }

        [DataMember(Code = (byte)UDT_ParameterCode.IsAppFullFunction, IsOptional = true)]
        public bool IsAppCharing { get; set; }


        [DataMember(Code = (byte)UDT_ParameterCode.MessengerUrl, IsOptional = true)]
        public string MessengerUrl { get; set; }


        [DataMember(Code = (byte)UDT_ParameterCode.TelegramUrl, IsOptional = true)]
        public string TelegramUrl { get; set; }


        [DataMember(Code = (byte)UDT_ParameterCode.TelegramBotUrl, IsOptional = true)]
        public string TelegramBotUrl { get; set; }


        [DataMember(Code = (byte)UDT_ParameterCode.Hotline, IsOptional = true)]
        public string Hotline { get; set; }

    }
}
