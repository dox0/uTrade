using Alveo.Interfaces.UserCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alveo.UserCode
{
	internal class MqlFileHelper
	{
		private readonly Dictionary<int, FileData> _openFiles = new Dictionary<int, FileData>();

		private int _fileHandle = 1000;

		internal MqlResult<int> FileOpen(string filename, int mode, char delimiter = ';')
		{
			bool flag = (mode & 8) == 8 && (mode & 16) == 16;
			FileMode fileMode;
			FileAccess fileAccess;
			MqlResult<int> result;
			if (flag)
			{
				fileMode = FileMode.OpenOrCreate;
				fileAccess = FileAccess.ReadWrite;
			}
			else
			{
				bool flag2 = (mode & 8) == 8;
				if (flag2)
				{
					fileMode = FileMode.Open;
					fileAccess = FileAccess.Read;
				}
				else
				{
					bool flag3 = (mode & 16) == 16;
					if (!flag3)
					{
						result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
						return result;
					}
					fileMode = FileMode.Create;
					fileAccess = FileAccess.Write;
				}
			}
			bool flag4 = string.IsNullOrEmpty(filename) || (!File.Exists(filename) && fileMode == FileMode.Open);
			if (flag4)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_WRONG_FILE_NAME);
			}
			else
			{
				try
				{
					FileStream fs = File.Open(filename, fileMode, fileAccess);
					Dictionary<int, FileData> arg_B8_0 = this._openFiles;
					int num = this._fileHandle + 1;
					this._fileHandle = num;
					arg_B8_0.Add(num, new FileData(filename, (OpenFileMode)mode, delimiter, fileAccess, fs));
					result = new MqlResult<int>(this._fileHandle);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_CANNOT_OPEN_FILE);
				}
			}
			return result;
		}

		internal MqlResult FileClose(int handle)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult result;
			if (flag)
			{
				fileData.Close();
				result = MqlResult.Empty;
			}
			else
			{
				result = new MqlResult(RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult FileDelete(string filename)
		{
			bool flag = string.IsNullOrEmpty(filename) || !File.Exists(filename);
			MqlResult result;
			if (flag)
			{
				result = new MqlResult(RunTimeErrors.ERR_WRONG_FILE_NAME);
			}
			else
			{
				try
				{
					File.Delete(filename);
				}
				catch
				{
					result = new MqlResult(RunTimeErrors.ERR_SOME_FILE_ERROR);
					return result;
				}
				result = MqlResult.Empty;
			}
			return result;
		}

		internal MqlResult FileFlush(int handle)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult result;
			if (flag)
			{
				fileData.FileStream.Flush();
				result = MqlResult.Empty;
			}
			else
			{
				result = new MqlResult(RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult<bool> FileIsEnding(int handle)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<bool> result;
			if (flag)
			{
				result = new MqlResult<bool>(fileData.FileStream.Position == fileData.FileStream.Length);
			}
			else
			{
				result = new MqlResult<bool>(false, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult<int> FileOpenHistory(string filename, int mode, char delimiter = ';')
		{
			bool flag = (mode & 8) == 8 && (mode & 16) == 16;
			FileMode fileMode;
			FileAccess fileAccess;
			MqlResult<int> result;
			if (flag)
			{
				fileMode = FileMode.OpenOrCreate;
				fileAccess = FileAccess.ReadWrite;
			}
			else
			{
				bool flag2 = (mode & 8) == 8;
				if (flag2)
				{
					fileMode = FileMode.Open;
					fileAccess = FileAccess.Read;
				}
				else
				{
					bool flag3 = (mode & 16) == 16;
					if (!flag3)
					{
						result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
						return result;
					}
					fileMode = FileMode.Create;
					fileAccess = FileAccess.Write;
				}
			}
			try
			{
				bool flag4 = string.IsNullOrEmpty(filename) || (!File.Exists(filename) && fileMode != FileMode.Create);
				if (flag4)
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_WRONG_FILE_NAME);
				}
				else
				{
					FileStream fs = File.Open(filename, fileMode, fileAccess);
					Dictionary<int, FileData> arg_BB_0 = this._openFiles;
					int num = this._fileHandle + 1;
					this._fileHandle = num;
					arg_BB_0.Add(num, new FileData(filename, (OpenFileMode)mode, delimiter, fileAccess, fs));
					result = new MqlResult<int>(this._fileHandle);
				}
			}
			catch
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
			}
			return result;
		}

		internal MqlResult<bool> FileIsLineEnding(int handle)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData) || fileData.IsBinary;
			MqlResult<bool> result;
			if (flag)
			{
				result = new MqlResult<bool>(false, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				bool flag2 = fileData.FileStream.Position >= fileData.FileStream.Length;
				if (flag2)
				{
					result = new MqlResult<bool>(false);
				}
				else
				{
					byte[] array = new byte[2];
					fileData.FileStream.Read(array, 0, 2);
					fileData.FileStream.Position -= 2L;
					result = new MqlResult<bool>(array[0] == 13 && array[1] == 10);
				}
			}
			return result;
		}

		internal MqlResult<int> FileReadArray(int handle, double[] array, int start, int count)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData) || !fileData.IsBinary;
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				BinaryReader reader = fileData.Reader;
				List<byte> list = new List<byte>();
				int num = 0;
				try
				{
					for (int i = 0; i < count; i++)
					{
						byte b = reader.ReadByte();
						while (!fileData.Delimiter.Equals((char)b))
						{
							list.Add(b);
							b = reader.ReadByte();
						}
						bool flag2 = list.Count > 0;
						if (flag2)
						{
							num++;
							array[start++] = BitConverter.ToDouble(list.ToArray(), 0);
						}
						list.Clear();
					}
					result = new MqlResult<int>(num);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_INCOMPATIBLE_FILEACCESS);
				}
			}
			return result;
		}

		internal MqlResult<double> FileReadDouble(int handle, int size = 8)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<double> result;
			if (flag)
			{
				result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					BinaryReader reader = fileData.Reader;
					if (size != 4)
					{
						if (size != 8)
						{
							result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
						}
						else
						{
							result = new MqlResult<double>(reader.ReadDouble());
						}
					}
					else
					{
						result = new MqlResult<double>((double)reader.ReadSingle());
					}
				}
				catch
				{
					result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INCOMPATIBLE_FILEACCESS);
				}
			}
			return result;
		}

		internal MqlResult<int> FileReadInteger(int handle, int size = 4)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					BinaryReader reader = fileData.Reader;
					switch (size)
					{
					case 1:
						result = new MqlResult<int>((int)reader.ReadSByte());
						return result;
					case 2:
						result = new MqlResult<int>((int)reader.ReadInt16());
						return result;
					case 4:
						result = new MqlResult<int>(reader.ReadInt32());
						return result;
					}
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_INCOMPATIBLE_FILEACCESS);
				}
			}
			return result;
		}

		internal MqlResult<double> FileReadNumber(int handle)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData) || fileData.IsBinary;
			MqlResult<double> result;
			if (flag)
			{
				result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				StreamReader sReader = fileData.SReader;
				string text = string.Empty;
				try
				{
					char[] array = new char[1];
					sReader.Read(array, 0, 1);
					while (!fileData.Delimiter.Equals(array[0]) && !sReader.EndOfStream)
					{
						text += array[0].ToString();
						bool flag2 = text.Equals("\r\n");
						if (flag2)
						{
							text = string.Empty;
						}
						sReader.Read(array, 0, 1);
					}
					bool flag3 = !string.IsNullOrEmpty(text);
					if (flag3)
					{
						result = new MqlResult<double>(double.Parse(text));
					}
					else
					{
						result = new MqlResult<double>(double.NaN);
					}
				}
				catch (FormatException)
				{
					result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
				}
				catch
				{
					result = new MqlResult<double>(-1.0, RunTimeErrors.ERR_INCOMPATIBLE_FILEACCESS);
				}
			}
			return result;
		}

		internal MqlResult<string> FileReadString(int handle, int length = 0)
		{
			string text = string.Empty;
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<string> result;
			if (flag)
			{
				result = new MqlResult<string>(string.Empty, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					bool isBinary = fileData.IsBinary;
					if (isBinary)
					{
						BinaryReader reader = fileData.Reader;
						while (fileData.FileStream.Position < fileData.FileStream.Length && text.Length < length)
						{
							text += reader.ReadChar().ToString();
						}
						result = new MqlResult<string>(text);
					}
					else
					{
						bool flag2 = length == 0;
						if (flag2)
						{
							result = new MqlResult<string>(string.Empty);
						}
						else
						{
							StreamReader sReader = fileData.SReader;
							char[] array = new char[length];
							long position = fileData.FileStream.Position;
							sReader.Read(array, 0, length);
							fileData.FileStream.Position = position + (long)length;
							result = new MqlResult<string>(new string(array));
						}
					}
				}
				catch
				{
					result = new MqlResult<string>(string.Empty, RunTimeErrors.ERR_INCOMPATIBLE_FILEACCESS);
				}
			}
			return result;
		}

		internal MqlResult<bool> FileSeek(int handle, int offset, int origin)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<bool> result;
			if (flag)
			{
				fileData.FileStream.Seek((long)offset, (SeekOrigin)origin);
				result = new MqlResult<bool>(true);
			}
			else
			{
				result = new MqlResult<bool>(false, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult<int> FileSize(int handle)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>((int)fileData.FileStream.Length);
			}
			else
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult<int> FileTell(int handle)
		{
			FileData fileData;
			bool flag = this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>((int)fileData.FileStream.Position);
			}
			else
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			return result;
		}

		internal MqlResult<int> FileWrite(int handle, params object[] values)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					fileData.Write(true, values);
					fileData.Write(false, new object[]
					{
						"\r\n"
					});
					result = new MqlResult<int>(0);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
				}
			}
			return result;
		}

		internal MqlResult<int> FileWriteArray(int handle, object[] array, int start, int count)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					List<object> list = new List<object>();
					int num = start;
					while (num < array.Count<object>() && count > 0)
					{
						list.Add(array[num]);
						bool flag2 = array[num] is string;
						if (flag2)
						{
							list.Add("\r\n");
						}
						num++;
						count--;
					}
					fileData.Write(false, list.ToArray());
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
					return result;
				}
				result = new MqlResult<int>(0);
			}
			return result;
		}

		internal MqlResult<int> FileWriteDouble(int handle, double value, int size = 8)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					if (size != 4)
					{
						if (size != 8)
						{
							result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
						}
						else
						{
							fileData.Write(false, new object[]
							{
								value
							});
							result = new MqlResult<int>(8);
						}
					}
					else
					{
						fileData.Write(false, new object[]
						{
							(float)value
						});
						result = new MqlResult<int>(4);
					}
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
				}
			}
			return result;
		}

		internal MqlResult<int> FileWriteInteger(int handle, int value, int size = 4)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					switch (size)
					{
					case 1:
						fileData.Write(false, new object[]
						{
							(sbyte)value
						});
						result = new MqlResult<int>(1);
						return result;
					case 2:
						fileData.Write(false, new object[]
						{
							(short)value
						});
						result = new MqlResult<int>(2);
						return result;
					case 4:
						fileData.Write(false, new object[]
						{
							value
						});
						result = new MqlResult<int>(4);
						return result;
					}
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
				}
			}
			return result;
		}

		internal MqlResult<int> FileWriteString(int handle, string value, int size)
		{
			FileData fileData;
			bool flag = !this._openFiles.TryGetValue(handle, out fileData);
			MqlResult<int> result;
			if (flag)
			{
				result = new MqlResult<int>(-1, RunTimeErrors.ERR_INVALID_FUNCTION_PARAMVALUE);
			}
			else
			{
				try
				{
					bool flag2 = value.Length > size;
					if (flag2)
					{
						value = value.Substring(0, size);
					}
					fileData.Write(false, new object[]
					{
						value
					});
					result = new MqlResult<int>(0);
				}
				catch
				{
					result = new MqlResult<int>(-1, RunTimeErrors.ERR_SOME_FILE_ERROR);
				}
			}
			return result;
		}
	}
}
