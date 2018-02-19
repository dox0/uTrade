using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Alveo.UserCode
{
	public class ExpertAdvisorBase : CodeBase, IExpertAdvisor, ICode, ICloneable, IManageableObject
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class c
		{
			public static readonly ExpertAdvisorBase.c __9 = new ExpertAdvisorBase.c();

			public static Action<IndicatorBase> __9__7_0;

			public static Action<IndicatorBase> __9__8_0;

			internal void <BaseInit>b__7_0(IndicatorBase i)
			{
				i.BaseInit();
			}

			internal void <BaseStart>b__8_0(IndicatorBase i)
			{
				i.BaseStart();
			}
		}

		public bool IsEaStopped
		{
			get;
			set;
		}

		protected ExpertAdvisorBase()
		{
			base.ID = Guid.NewGuid().ToString();
		}

		public override object Clone()
		{
			return base.MemberwiseClone();
		}

		void ICode.UpdateProperties(ICode code)
		{
			this.IsEaStopped = ((ExpertAdvisorBase)code).isStopped;
		}

		public override void BaseInit()
		{
			List<IndicatorBase> arg_26_0 = this.IndicatorCache;
			Action<IndicatorBase> arg_26_1;
			if ((arg_26_1 = ExpertAdvisorBase.c.__9__7_0) == null)
			{
				arg_26_1 = (ExpertAdvisorBase.c.__9__7_0 = new Action<IndicatorBase>(ExpertAdvisorBase.c.__9.<BaseInit>b__7_0));
			}
			arg_26_0.ForEach(arg_26_1);
			base.BaseInit();
		}

		public override void BaseStart()
		{
			List<IndicatorBase> arg_26_0 = this.IndicatorCache;
			Action<IndicatorBase> arg_26_1;
			if ((arg_26_1 = ExpertAdvisorBase.c.__9__8_0) == null)
			{
				arg_26_1 = (ExpertAdvisorBase.c.__9__8_0 = new Action<IndicatorBase>(ExpertAdvisorBase.c.__9.<BaseStart>b__8_0));
			}
			arg_26_0.ForEach(arg_26_1);
			base.BaseStart();
		}

		public void AddEmptyValueToAllSeries()
		{
			foreach (IndicatorBase current in this.IndicatorCache)
			{
				current.AddEmptyValueToAllSeries();
			}
		}

		public override void UpdateProperties(ICode code)
		{
		}
	}
}
