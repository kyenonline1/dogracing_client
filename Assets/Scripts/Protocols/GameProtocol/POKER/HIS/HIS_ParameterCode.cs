using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameProtocol.HIS
{
    public enum HIS_ParameterCode : byte
    {
        TableId = 10,
        Gamesession,
        GameId,
        Blind,
        Ante,
        Histories,
        IsSave,
        StartTime,
        CurrentPage,
        TotalPage,
        NumberSlot,
    }
}
