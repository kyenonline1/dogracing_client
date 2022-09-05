using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils
{
	public interface ILogger {
		void LogInfo(string message);
		void LogWarning(string message);
		void LogError(string message);
		void LogException(string message, System.Exception ex);
	}

}
