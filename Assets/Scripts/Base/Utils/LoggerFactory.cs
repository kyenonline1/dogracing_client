using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils
{
	public class LoggerFactory {
		protected static SimpleLogger simpleLogger;
		public static ILogger CreateLogger()
		{
			if(simpleLogger == null)
				simpleLogger = new SimpleLogger();
			return simpleLogger;
		}
	}

}
