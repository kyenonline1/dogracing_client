using UnityEngine;

namespace Base.Utils
{
	public class SimpleLogger : MonoBehaviour, ILogger {
        public void LogError(string message)
        {
            #if DEBUG
			Debug.LogErrorFormat("{0} - {1}", message, StackTraceUtility.ExtractStackTrace());
			#endif
        }

        public void LogException(string message, System.Exception ex)
        {
            #if DEBUG
			Debug.LogFormat("{0} - Exception: {1} - {2}", message, ex.Message, ex.StackTrace);
			#endif
        }

        public void LogInfo(string message)
        {
            #if DEBUG
			Debug.LogFormat("{0} - {1}", message, StackTraceUtility.ExtractStackTrace());
			#endif
        }

        public void LogWarning(string message)
        {
            #if DEBUG
			Debug.LogWarningFormat("{0} - {1}", message, StackTraceUtility.ExtractStackTrace());
			#endif
        }
	}

}
