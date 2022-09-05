using UnityEngine;
using System.Collections;
using Interface;

namespace CoreBase.Controller
{
    /// <summary>
    /// Base Controller dành cho game dạng Cờ.
    /// </summary>
    public class BaseChessGameController : BaseGameController
    {
        public BaseChessGameController(IView view) : base(view)
        {
        }
        public enum TableState
        {
            NONE = -2, EMPTY = -1, INIT = 0, PLAY = 1, END = 2
        }

    }
}

