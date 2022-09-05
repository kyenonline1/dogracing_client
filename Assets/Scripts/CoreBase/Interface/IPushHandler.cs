using System.Collections;
using System.Collections.Generic;

namespace Interface
{
    public interface IPushHandler
    {

        IEnumerator HandlePush(string coderun, Dictionary<byte, object> data);
    }
}