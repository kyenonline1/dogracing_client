using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils
{
	public class SuperLogger {
		public static void Log(string message)
		{
			LoggerFactory.CreateLogger().LogInfo(message);
		}

		public static void LogError(string message)
		{
			LoggerFactory.CreateLogger().LogError(message);
		}

		public static void LogException(string message, System.Exception exception)
		{
			LoggerFactory.CreateLogger().LogException(message, exception);
		}

		public static void LogWarning(string message)
		{
			LoggerFactory.CreateLogger().LogWarning(message);
		}
	}

}
