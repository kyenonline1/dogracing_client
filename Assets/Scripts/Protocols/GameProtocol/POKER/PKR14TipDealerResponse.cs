using GameProtocol.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProtocol.PKR
{
    public class PKR14TipDealerResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "PKR14";
        }

        public PKR14TipDealerResponse()
        {
        }

        public PKR14TipDealerResponse(Dictionary<byte, object> dict) : base(dict)
        {
        }
    }
}