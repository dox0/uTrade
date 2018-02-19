using System;

namespace Alveo.UserCode
{
	public enum UninitializeReason
	{
		UNKNOWN = -1,
		NORMAL,
		REASON_REMOVE,
		REASON_RECOMPILE,
		REASON_CHARTCHANGE,
		REASON_CHARTCLOSE,
		REASON_PARAMETERS,
		REASON_ACCOUNT
	}
}
