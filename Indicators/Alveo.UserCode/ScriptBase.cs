using Alveo.Interfaces.UserCode;
using System;

namespace Alveo.UserCode
{
	public abstract class ScriptBase : CodeBase, IScript, ICode, ICloneable, IManageableObject
	{
		public override void UpdateProperties(ICode code)
		{
			throw new NotImplementedException();
		}

		public override object Clone()
		{
			throw new NotImplementedException();
		}
	}
}
