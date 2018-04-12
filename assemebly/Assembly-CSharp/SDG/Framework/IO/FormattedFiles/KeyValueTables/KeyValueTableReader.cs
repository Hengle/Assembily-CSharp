using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001BD RID: 445
	public class KeyValueTableReader : IFormattedFileReader
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x0005FAAC File Offset: 0x0005DEAC
		public KeyValueTableReader()
		{
			this.table = new Dictionary<string, object>();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0005FABF File Offset: 0x0005DEBF
		public KeyValueTableReader(StreamReader input)
		{
			this.table = new Dictionary<string, object>();
			this.readDictionary(input, this.table);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0005FADF File Offset: 0x0005DEDF
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0005FAE7 File Offset: 0x0005DEE7
		public Dictionary<string, object> table { get; protected set; }

		// Token: 0x06000D3B RID: 3387 RVA: 0x0005FAF0 File Offset: 0x0005DEF0
		public virtual IEnumerable<string> getKeys()
		{
			return this.table.Keys;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0005FAFD File Offset: 0x0005DEFD
		public virtual bool containsKey(string key)
		{
			return this.table.ContainsKey(key);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0005FB0B File Offset: 0x0005DF0B
		public virtual void readKey(string key)
		{
			this.key = key;
			this.index = -1;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0005FB1B File Offset: 0x0005DF1B
		public virtual int readArrayLength(string key)
		{
			this.readKey(key);
			return this.readArrayLength();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0005FB2C File Offset: 0x0005DF2C
		public virtual int readArrayLength()
		{
			object obj;
			if (this.table.TryGetValue(this.key, out obj))
			{
				return (obj as List<object>).Count;
			}
			return 0;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0005FB5E File Offset: 0x0005DF5E
		public virtual void readArrayIndex(string key, int index)
		{
			this.readKey(key);
			this.readArrayIndex(index);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0005FB6E File Offset: 0x0005DF6E
		public virtual void readArrayIndex(int index)
		{
			this.index = index;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0005FB77 File Offset: 0x0005DF77
		public virtual string readValue(string key)
		{
			this.readKey(key);
			return this.readValue();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0005FB86 File Offset: 0x0005DF86
		public virtual string readValue(int index)
		{
			this.readArrayIndex(index);
			return this.readValue();
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0005FB95 File Offset: 0x0005DF95
		public virtual string readValue(string key, int index)
		{
			this.readKey(key);
			this.readArrayIndex(index);
			return this.readValue();
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0005FBAC File Offset: 0x0005DFAC
		public virtual string readValue()
		{
			if (this.index == -1)
			{
				object obj;
				if (!this.table.TryGetValue(this.key, out obj))
				{
					return null;
				}
				return (string)obj;
			}
			else
			{
				object obj2;
				if (this.table.TryGetValue(this.key, out obj2))
				{
					object obj3 = (obj2 as List<object>)[this.index];
					return (string)obj3;
				}
				return null;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0005FC18 File Offset: 0x0005E018
		public virtual object readValue(Type type, string key)
		{
			this.readKey(key);
			return this.readValue(type);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0005FC28 File Offset: 0x0005E028
		public virtual object readValue(Type type, int index)
		{
			this.readArrayIndex(index);
			return this.readValue(type);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0005FC38 File Offset: 0x0005E038
		public virtual object readValue(Type type, string key, int index)
		{
			this.readKey(key);
			this.readArrayIndex(index);
			return this.readValue(type);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0005FC50 File Offset: 0x0005E050
		public virtual object readValue(Type type)
		{
			if (typeof(IFormattedFileReadable).IsAssignableFrom(type))
			{
				IFormattedFileReadable formattedFileReadable = Activator.CreateInstance(type) as IFormattedFileReadable;
				formattedFileReadable.read(this);
				return formattedFileReadable;
			}
			return KeyValueTableTypeReaderRegistry.read(this, type);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0005FC8E File Offset: 0x0005E08E
		public virtual T readValue<T>(string key)
		{
			this.readKey(key);
			return this.readValue<T>();
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0005FC9D File Offset: 0x0005E09D
		public virtual T readValue<T>(int index)
		{
			this.readArrayIndex(index);
			return this.readValue<T>();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0005FCAC File Offset: 0x0005E0AC
		public virtual T readValue<T>(string key, int index)
		{
			this.readKey(key);
			this.readArrayIndex(index);
			return this.readValue<T>();
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0005FCC4 File Offset: 0x0005E0C4
		public virtual T readValue<T>()
		{
			if (typeof(IFormattedFileReadable).IsAssignableFrom(typeof(T)))
			{
				IFormattedFileReadable formattedFileReadable = Activator.CreateInstance<T>() as IFormattedFileReadable;
				formattedFileReadable.read(this);
				return (T)((object)formattedFileReadable);
			}
			return KeyValueTableTypeReaderRegistry.read<T>(this);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0005FD13 File Offset: 0x0005E113
		public virtual IFormattedFileReader readObject(string key)
		{
			this.readKey(key);
			return this.readObject();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0005FD22 File Offset: 0x0005E122
		public virtual IFormattedFileReader readObject(int index)
		{
			this.readArrayIndex(index);
			return this.readObject();
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0005FD31 File Offset: 0x0005E131
		public virtual IFormattedFileReader readObject(string key, int index)
		{
			this.readKey(key);
			this.readArrayIndex(index);
			return this.readObject();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0005FD48 File Offset: 0x0005E148
		public virtual IFormattedFileReader readObject()
		{
			if (this.index == -1)
			{
				object obj;
				if (this.table.TryGetValue(this.key, out obj))
				{
					return obj as IFormattedFileReader;
				}
				return null;
			}
			else
			{
				object obj2;
				if (this.table.TryGetValue(this.key, out obj2))
				{
					return (obj2 as List<object>)[this.index] as IFormattedFileReader;
				}
				return null;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0005FDB2 File Offset: 0x0005E1B2
		protected virtual bool canContinueReadDictionary(StreamReader input, Dictionary<string, object> scope)
		{
			return true;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0005FDB8 File Offset: 0x0005E1B8
		public virtual void readDictionary(StreamReader input, Dictionary<string, object> scope)
		{
			this.dictionaryKey = null;
			this.dictionaryInQuotes = false;
			this.dictionaryIgnoreNextChar = false;
			while (!input.EndOfStream)
			{
				char c = (char)input.Read();
				if (this.dictionaryIgnoreNextChar)
				{
					KeyValueTableReader.builder.Append(c);
					this.dictionaryIgnoreNextChar = false;
				}
				else if (c == '\\')
				{
					this.dictionaryIgnoreNextChar = true;
				}
				else if (c == '"')
				{
					if (this.dictionaryInQuotes)
					{
						this.dictionaryInQuotes = false;
						if (string.IsNullOrEmpty(this.dictionaryKey))
						{
							this.dictionaryKey = KeyValueTableReader.builder.ToString();
						}
						else
						{
							string value = KeyValueTableReader.builder.ToString();
							if (!scope.ContainsKey(this.dictionaryKey))
							{
								scope.Add(this.dictionaryKey, value);
							}
							if (!this.canContinueReadDictionary(input, scope))
							{
								return;
							}
							this.dictionaryKey = null;
						}
					}
					else
					{
						this.dictionaryInQuotes = true;
						KeyValueTableReader.builder.Length = 0;
					}
				}
				else if (this.dictionaryInQuotes)
				{
					KeyValueTableReader.builder.Append(c);
				}
				else if (c == '{')
				{
					object obj;
					if (scope.TryGetValue(this.dictionaryKey, out obj))
					{
						KeyValueTableReader keyValueTableReader = (KeyValueTableReader)obj;
						keyValueTableReader.readDictionary(input, keyValueTableReader.table);
					}
					else
					{
						KeyValueTableReader keyValueTableReader2 = new KeyValueTableReader(input);
						obj = keyValueTableReader2;
						scope.Add(this.dictionaryKey, keyValueTableReader2);
					}
					if (!this.canContinueReadDictionary(input, scope))
					{
						return;
					}
					this.dictionaryKey = null;
				}
				else
				{
					if (c == '}')
					{
						return;
					}
					if (c == '[')
					{
						object obj2;
						if (!scope.TryGetValue(this.dictionaryKey, out obj2))
						{
							obj2 = new List<object>();
							scope.Add(this.dictionaryKey, obj2);
						}
						this.readList(input, (List<object>)obj2);
						if (!this.canContinueReadDictionary(input, scope))
						{
							return;
						}
						this.dictionaryKey = null;
					}
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0005FFA4 File Offset: 0x0005E3A4
		public virtual void readList(StreamReader input, List<object> scope)
		{
			this.listInQuotes = false;
			this.listIgnoreNextChar = false;
			while (!input.EndOfStream)
			{
				char c = (char)input.Read();
				if (this.listIgnoreNextChar)
				{
					KeyValueTableReader.builder.Append(c);
					this.listIgnoreNextChar = false;
				}
				else if (c == '\\')
				{
					this.listIgnoreNextChar = true;
				}
				else if (c == '"')
				{
					if (this.listInQuotes)
					{
						this.listInQuotes = false;
						string item = KeyValueTableReader.builder.ToString();
						scope.Add(item);
					}
					else
					{
						this.listInQuotes = true;
						KeyValueTableReader.builder.Length = 0;
					}
				}
				else if (this.listInQuotes)
				{
					KeyValueTableReader.builder.Append(c);
				}
				else if (c == '{')
				{
					KeyValueTableReader item2 = new KeyValueTableReader(input);
					scope.Add(item2);
				}
				else if (c == ']')
				{
					return;
				}
			}
		}

		// Token: 0x040008DB RID: 2267
		protected static StringBuilder builder = new StringBuilder();

		// Token: 0x040008DD RID: 2269
		protected string key;

		// Token: 0x040008DE RID: 2270
		protected int index;

		// Token: 0x040008DF RID: 2271
		protected string dictionaryKey;

		// Token: 0x040008E0 RID: 2272
		protected bool dictionaryInQuotes;

		// Token: 0x040008E1 RID: 2273
		protected bool dictionaryIgnoreNextChar;

		// Token: 0x040008E2 RID: 2274
		protected bool listInQuotes;

		// Token: 0x040008E3 RID: 2275
		protected bool listIgnoreNextChar;
	}
}
