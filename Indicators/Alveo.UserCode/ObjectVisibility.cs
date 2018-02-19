using System;

namespace Alveo.UserCode
{
	public enum ObjectVisibility
	{
		EMPTY = -1,
		NULL,
		OBJ_PERIOD_M1,
		OBJ_PERIOD_M5,
		OBJ_PERIOD_M15 = 4,
		OBJ_PERIOD_M30 = 8,
		OBJ_PERIOD_H1 = 16,
		OBJ_PERIOD_H4 = 32,
		OBJ_PERIOD_D1 = 64,
		OBJ_PERIOD_W1 = 128,
		OBJ_PERIOD_MN1 = 256,
		OBJ_ALL_PERIODS = 511
	}
}
