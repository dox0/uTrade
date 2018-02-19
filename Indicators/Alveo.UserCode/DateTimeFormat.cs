using System;

namespace Alveo.UserCode
{
	[Flags]
	public enum DateTimeFormat
	{
		TIME_DATE = 2,
		TIME_MINUTES = 4,
		TIME_SECONDS = 8
	}
}
