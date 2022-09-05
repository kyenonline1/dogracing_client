using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Common
{
    public class UtilsCommon
    {

        public static Coroutine delayTime(MonoBehaviour mb, float time, System.Action action)
        {
            if(mb.gameObject.activeInHierarchy)
                return mb.StartCoroutine(UtilsCommon._delayTime(time, action));
            return null;
        }

        private static IEnumerator _delayTime(float time, System.Action action)
        {
            yield return new WaitForSecondsRealtime(time);
            action();
        }

        public static string formatNumber(double numb)
        {
            return string.Format("{0:#.##0}", numb);
        }

        public static string abbreviateNumber(double numb, int _fixed = 2)
        {
            string format = "{0:#.##0.";
            for (int i = 0; i < _fixed; i++)
            {
                format += "#";
            }
            format += "}";
            if (numb >= 1000000000)
            {
                return string.Format(format, numb / 1000000000f) + "B";
            }
            else if (numb >= 1000000)
            {
                return string.Format(format, numb / 1000000f) + "M";
            }
            else if (numb >= 1000)
            {
                return string.Format(format, numb / 1000f) + "K";
            }
            return string.Format(format, numb);
        }

        public static string GetFormattedTime(long seconds, bool showHours = false)
        {
            if (showHours)
            {
                return string.Format("{0:00}:{1:00}:{2:00}", seconds / 3600, (seconds / 60) % 60, seconds % 60);
            }
            return string.Format("{0:00}", seconds % 60);
        }

        public static string EscapeXml(string _unsafe) {
            return _unsafe.Replace("<", "&lt;")
                          .Replace(">", "&gt;")
                          .Replace("&", "&amp;")
                          .Replace("\'", "&apos;")
                          .Replace("\"", "&quot;");
        }

        public static long StringToLong(string s)
        {
            return (long)System.Convert.ToDouble(new Regex(@"^-?[0-9,]*\.?[0-9]*").Match(s).Value.Replace(".", "").Trim());
        }
    }
}
