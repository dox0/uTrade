using System;
using System.IO;
using System.Text;

namespace Alveo.UserCode
{
	public class FileData
	{
		public char Delimiter
		{
			get;
			private set;
		}

		public string FileName
		{
			get;
			private set;
		}

		public OpenFileMode Mode
		{
			get;
			private set;
		}

		public FileAccess FileAccess
		{
			get;
			private set;
		}

		public FileStream FileStream
		{
			get;
			private set;
		}

		public BinaryReader Reader
		{
			get;
			private set;
		}

		public BinaryWriter Writer
		{
			get;
			private set;
		}

		public StreamReader SReader
		{
			get;
			private set;
		}

		public StreamWriter SWriter
		{
			get;
			private set;
		}

		public bool IsBinary
		{
			get;
			private set;
		}

		public FileData(string name, OpenFileMode mode, char delimiter, FileAccess fileAccess, FileStream fs)
		{
			this.FileName = name;
			this.Mode = mode;
			this.Delimiter = delimiter;
			this.FileAccess = fileAccess;
			this.FileStream = fs;
			bool flag = (this.Mode & OpenFileMode.FILE_CSV) == OpenFileMode.FILE_CSV;
			if (flag)
			{
				bool canRead = fs.CanRead;
				if (canRead)
				{
					this.SReader = new StreamReader(fs);
				}
				bool canWrite = fs.CanWrite;
				if (canWrite)
				{
					this.SWriter = new StreamWriter(fs);
				}
				this.IsBinary = false;
			}
			else
			{
				bool canRead2 = fs.CanRead;
				if (canRead2)
				{
					this.Reader = new BinaryReader(fs);
				}
				bool canWrite2 = fs.CanWrite;
				if (canWrite2)
				{
					this.Writer = new BinaryWriter(fs);
				}
				this.IsBinary = true;
			}
		}

		public void Close()
		{
			bool flag = this.Reader != null;
			if (flag)
			{
				this.Reader.Close();
			}
			bool flag2 = this.Writer != null;
			if (flag2)
			{
				this.Writer.Close();
			}
			bool flag3 = this.SWriter != null;
			if (flag3)
			{
				this.SWriter.Close();
			}
			bool flag4 = this.SReader != null;
			if (flag4)
			{
				this.SReader.Close();
			}
		}

		public void Write(bool useDelimiter, params object[] array)
		{
			bool isBinary = this.IsBinary;
			if (isBinary)
			{
				for (int i = 0; i < array.Length; i++)
				{
					object obj = array[i];
					bool flag = obj is bool;
					if (flag)
					{
						this.Writer.Write((bool)obj);
					}
					else
					{
						bool flag2 = obj is byte;
						if (flag2)
						{
							this.Writer.Write((byte)obj);
						}
						else
						{
							bool flag3 = obj is char;
							if (flag3)
							{
								this.Writer.Write((char)obj);
							}
							else
							{
								bool flag4 = obj is short;
								if (flag4)
								{
									this.Writer.Write((short)obj);
								}
								else
								{
									bool flag5 = obj is int;
									if (flag5)
									{
										this.Writer.Write((int)obj);
									}
									else
									{
										bool flag6 = obj is long;
										if (flag6)
										{
											this.Writer.Write((long)obj);
										}
										else
										{
											bool flag7 = obj is double;
											if (flag7)
											{
												this.Writer.Write((double)obj);
											}
											else
											{
												bool flag8 = obj is float;
												if (flag8)
												{
													this.Writer.Write((float)obj);
												}
												else
												{
													this.Writer.Write(obj.ToString());
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < array.Length; j++)
				{
					object value = array[j];
					stringBuilder.Append(value);
					if (useDelimiter)
					{
						stringBuilder.Append(this.Delimiter);
					}
				}
				this.SWriter.Write(stringBuilder.ToString());
			}
		}
	}
}
