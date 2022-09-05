using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Custom
{
    public sealed class DataDispatcher
    {
        private Dictionary<string, Extra> extras = new Dictionary<string, Extra>();

        private static readonly DataDispatcher instance = new DataDispatcher();

        public static DataDispatcher Instance()
        {
            return instance;
        }
        public Extra SetExtras(string SceneName)
        {
            Extra extra = new Extra();
            if (extras.ContainsKey(SceneName))
            {
                extras.Remove(SceneName);
                //extra[SceneName] = extra;
                //return extra;
            }
                
                
            extras.Add(SceneName, extra);
            return extra;
        }

        public Extra GetExtras(string SceneName)
        {
            if (extras.ContainsKey(SceneName))
            {
                Extra ex = extras[SceneName];
                return ex;
            }
            return null;
        }

        public object GetExtra(string SceneName, string key, object defValue = null)
        {
            if (extras.ContainsKey(SceneName))
            {
                Extra ex = extras[SceneName];
                if (ex.ContainsKey(key))
                    return ex[key];
                return defValue;
            }
            return defValue;
        }
        

        public class Extra: Dictionary<string, object> { }

        void test()
        {
            DataDispatcher.Instance().SetExtras("TestScene").
                PutExtra("username", "johnroid").
                PutExtra("password", "abcdef123").
                PutExtra("logintype", 0);
        }
    }
    static class ExtraEx
    {
        public static DataDispatcher.Extra PutExtra(this DataDispatcher.Extra extra, string name, object value)
        {
            if (extra.ContainsKey(name))
            {
                extra.Remove(name);
            }
            extra.Add(name, value);
            return extra;
        }
    }

}


