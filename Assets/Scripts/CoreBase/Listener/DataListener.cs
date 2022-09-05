using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Network;

namespace Listener
{
    [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
    public delegate IEnumerator HandleResponse(string CodeRun, Dictionary<byte, object> Data);

    [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
    public class DataListener
    {
        private HandleResponse Delegate;

        public DataListener(HandleResponse Delegate)
        {
            this.Delegate = Delegate;
        }
        [System.Reflection.Obfuscation(Feature = "renaming", Exclude = true)]
        public IEnumerator Invoke(string CodeRun, Dictionary<byte, object> Data)
        {
            //Debug.LogError("InVoke: " + CodeRun);
            IEnumerator IEnumHandler = Delegate.Invoke(CodeRun, Data);
            while (IEnumHandler.MoveNext())
                yield return IEnumHandler.Current;

            yield return new WaitForEndOfFrame();
        }
    }
}
