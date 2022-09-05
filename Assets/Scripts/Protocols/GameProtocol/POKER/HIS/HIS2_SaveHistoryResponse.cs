using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;

namespace GameProtocol.HIS
{
    public class HIS2_SaveHistoryResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "HIS2";
        }

        public HIS2_SaveHistoryResponse()
        {
        }

        public HIS2_SaveHistoryResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

    }
}