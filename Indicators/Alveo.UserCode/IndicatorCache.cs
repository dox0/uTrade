using System;
using System.Collections.Generic;

namespace Alveo.UserCode
{
	public class IndicatorCache : List<IndicatorBase>
	{
		public IndicatorBase GetCash(Type indicatorType, params object[] values)
		{
			IndicatorBase result;
			for (int i = 0; i < base.Count; i++)
			{
				bool flag = base[i].GetType() != indicatorType;
				if (!flag)
				{
					bool flag2 = base[i].IsSameParameters(values);
					if (flag2)
					{
						result = base[i];
						return result;
					}
				}
			}
			result = null;
			return result;
		}
	}
}
