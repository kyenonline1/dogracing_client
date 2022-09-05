using System;

namespace Base
{
	public class HeaderInvalidException : Exception {
		public HeaderInvalidException()
		{

		}

		public HeaderInvalidException(string message) : base(message)
		{

		}
	}

}
