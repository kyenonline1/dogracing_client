using GameProtocol.Base;
using GameProtocol.Protocol;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
namespace GameProtocol.PKR
{
    public class PKR20BuyInsuranceResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR20";
        }

        public PKR20BuyInsuranceResponse()
        {
        }

        public PKR20BuyInsuranceResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }

        
    }
}