using Alveo.Interfaces.UserCode;
using System;

namespace Alveo.UserCode
{
	internal class MqlResult
	{
		public static readonly MqlResult Empty = new MqlResult();

		public int ErrorCode
		{
			get;
			set;
		}

		public MqlResult()
		{
			this.ErrorCode = -100;
		}

		public MqlResult(RunTimeErrors error)
		{
			this.ErrorCode = (int)error;
		}
	}
	internal class MqlResult<T> : MqlResult
	{
		public T Value
		{
			get;
			set;
		}

		public MqlResult(T value)
		{
			this.Value = value;
		}

		public MqlResult(T value, RunTimeErrors errors)
		{
			base.ErrorCode = (int)errors;
			this.Value = value;
		}
	}
}
