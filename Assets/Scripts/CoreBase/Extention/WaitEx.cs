using Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CoreBase.Extention
{
    public static class WaitEx
    {
        public static IEnumerator WaitForSeconds(this object controller, float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        public static IEnumerator WaitUntilNotify(this object controller, WaitNotify notify)
        {
            while (!notify.FinishWait)
                yield return new WaitForSeconds(0.5f);
            yield return new WaitForEndOfFrame();
        }

        public static IEnumerator WaitUntilNotify(this object controller, WaitNotify notify, float interval)
        {
            while (!notify.FinishWait)
                yield return new WaitForSeconds(interval);
            yield return new WaitForEndOfFrame();
        }
    }
}
