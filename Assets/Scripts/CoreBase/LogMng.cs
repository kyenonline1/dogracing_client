using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

namespace Utilities
{
	public class LogMng
	{

        public static readonly StringBuilder LogString = new StringBuilder();
#if UNITY_EDITOR
        private static readonly bool DEBUG = true;
#else
        private static readonly bool DEBUG = false;
#endif

        public static string GetLog()
        {
            return LogString.ToString();
        }

        public static void ClearLog()
        {
            LogString.Remove(0, LogString.Length);
        }

		// Update is called once per frame
		public static void Log (string TAG, object message)
		{
            if (!DEBUG) return;
            
			Debug.Log (TAG + " - " + message);
            LogString.Append("\r\n\r\n").Append("Log - " + TAG + " - " + message);
		}

		public static void Log (string TAG, object message, Object context)
		{
            if (!DEBUG) return;

            Debug.Log (TAG + " - " + message, context);
            LogString.Append("\r\n\r\n").Append("Log - " + TAG + " - " + message);
		}

		public static void LogError (string TAG, object message)
		{
            if (!DEBUG) return;

            Debug.LogError (TAG + " - " + message);
            LogString.Append("\r\n\r\n").Append("LogError - " + TAG + " - " + message);
		}

		public static void LogError (string TAG, object message, Object context)
		{
            if (!DEBUG) return;

            Debug.LogError (TAG + " - " + message, context);
            LogString.Append("\r\n\r\n").Append("LogError - " + TAG + " - " + message);
		}

		public static void LogException (System.Exception exception)
		{
            if (!DEBUG) return;

            Debug.LogException (exception);
            LogString.Append("\r\n\r\n").Append("LogException - " + GetString(exception));
		}

		public static void LogException (System.Exception exception, Object context)
		{
            if (!DEBUG) return;

            Debug.LogException (exception, context);
            LogString.Append("\r\n\r\n").Append("LogException - " + GetString(exception));
		}

		public static void LogWarning (string TAG, object message)
		{
            if (!DEBUG) return;

            Debug.LogWarning (TAG + " - " + message);
            LogString.Append("\r\n\r\n").Append("LogWarning - " + TAG + " - " + message);
		}

        public static void LogWarning(string TAG, object message, Object context)
		{
            if (!DEBUG) return;

            Debug.LogWarning (TAG + " - " + message, context);
            LogString.Append("\r\n\r\n").Append("LogWarning - " + TAG + " - " + message);
		}

        private static string GetString(System.Exception e)
        {
            if (!DEBUG) return string.Empty;

            StringBuilder exception = new StringBuilder();
            exception.Append(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " ***");
            exception.Append("\r\n");
            exception.Append("TYPE: " + exception.GetType());
            exception.Append("\r\n");
            exception.Append("Message: " + e.Message);
            exception.Append("\r\n");
            exception.Append("STACK TRACE: " + e.StackTrace);
            return exception.ToString();
        }

		public static void LogExceptionToSDCard (System.Exception exception)
		{
            //string filePath = Application.persistentDataPath + "/" + "sdcardlog.txt";
            //try {
            //    if (System.IO.File.Exists (filePath)) {
            //        //Debug.Log ("Logger: LogExceptionToSDCard exist: " + true);
            //    } else {
            //        //Debug.Log ("Logger: LogExceptionToSDCard exist: " + false);
            //    }
				
            //    System.IO.StreamWriter file = new System.IO.StreamWriter (filePath, true);
            //    file.WriteLine ();
            //    file.WriteLine ();
            //    file.Write (" " + System.DateTime.Now.ToString ("dd/MM/yyyy HH:mm:ss") + " ***");
            //    file.WriteLine ();
            //    file.Write ("TYPE: " + exception.GetType ());
            //    file.WriteLine ();
            //    file.Write ("Message: " + exception.Message);				
            //    file.WriteLine ();
            //    file.Write ("STACK TRACE: " + exception.StackTrace);	
				
            //    file.Close ();
				
            //} catch (IOException e) {
            //    Logger.LogError ("LOGGER", "Error@logToSdcard:" + e.Message, null);
            //}
		}

		public static void LogExceptionToSDCard (string condition, string stackTrace, LogType type)
		{
            //string filePath = Application.persistentDataPath + "/" + "sdcardlog.txt";
            //try {
            //    if (System.IO.File.Exists (filePath)) {
            //        //Debug.Log ("Logger: LogExceptionToSDCard exist: " + true);
            //    } else {
            //        //Debug.Log ("Logger: LogExceptionToSDCard exist: " + false);
            //    }

            //    System.IO.StreamWriter file = new System.IO.StreamWriter (filePath, true);
            //    file.WriteLine ();
            //    file.WriteLine ();
            //    file.Write (" " + System.DateTime.Now.ToString ("dd/MM/yyyy HH:mm:ss") + " ***");
            //    file.WriteLine ();
            //    file.Write ("TYPE: " + type);
            //    file.WriteLine ();
            //    file.Write ("CONDITION: " + condition);				
            //    file.WriteLine ();
            //    file.Write ("STACK TRACE: " + stackTrace);	

            //    file.Close ();

            //} catch (IOException e) {
            //    Logger.LogError ("LOGGER", "Error@logToSdcard:" + e.Message, null);
            //}
		}
	}
}
