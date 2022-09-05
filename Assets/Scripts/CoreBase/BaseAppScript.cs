using UnityEngine;
using System.Collections;

namespace CoreBase
{
    /// <summary>
    /// class base cho cac man hinh sau khi login
    /// </summary>
    public class BaseAppScript : ScreenScript
    {

        [HandleUpdateEvent(EventType = HandlerType.EVN_PUSH_HANDLER)]
        void GotoLoginScreen(params object[] parameters)
        {

        }
    }
}
