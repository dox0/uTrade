using System;

namespace Alveo.UserCode
{
	[Flags]
	public enum OpenFileMode
	{
		FILE_BIN = 2,
		FILE_CSV = 4,
		FILE_READ = 8,
		FILE_WRITE = 16
	}
}
