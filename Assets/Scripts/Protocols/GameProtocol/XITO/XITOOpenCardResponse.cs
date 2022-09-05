﻿using GameProtocol.Base;
using GameProtocol.Protocol;

namespace GameProtocol.XIT
{
    public class XITOOpenCardResponse : ResponseBase
    {
        public override void SetCodeRun()
        {
            CodeRun = "XIT6";
        }
    }
}
