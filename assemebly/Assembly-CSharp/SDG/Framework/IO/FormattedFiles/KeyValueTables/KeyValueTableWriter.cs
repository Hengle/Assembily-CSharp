using System;
using System.IO;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001C1 RID: 449
	public class KeyValueTableWriter : IFormattedFileWriter
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x00060384 File Offset: 0x0005E784
		public KeyValueTableWriter(StreamWriter writer)
		{
			this.writer = writer;
			this.indentationCount = 0;
			this.hasWritten = false;
			this.wroteKey = false;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000603A8 File Offset: 0x0005E7A8
		public virtual void writeKey(string key)
		{
			if (this.hasWritten)
			{
				this.writer.WriteLine();
			}
			this.writeIndents();
			this.writer.Write('"');
			this.writer.Write(key);
			this.writer.Write('"');
			this.hasWritten = true;
			this.wroteKey = true;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00060405 File Offset: 0x0005E805
		public virtual void writeValue(string key, string value)
		{
			this.writeKey(key);
			this.writeValue(value);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00060418 File Offset: 0x0005E818
		public virtual void writeValue(string value)
		{
			if (this.wroteKey)
			{
				this.writer.Write(' ');
			}
			else
			{
				if (this.hasWritten)
				{
					this.writer.WriteLine();
				}
				this.writeIndents();
			}
			this.writer.Write('"');
			this.writer.Write(value);
			this.writer.Write('"');
			this.wroteKey = false;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0006048B File Offset: 0x0005E88B
		public virtual void writeValue(string key, object value)
		{
			this.writeKey(key);
			this.writeValue(value);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0006049C File Offset: 0x0005E89C
		public virtual void writeValue(object value)
		{
			if (value is IFormattedFileWritable)
			{
				IFormattedFileWritable formattedFileWritable = value as IFormattedFileWritable;
				formattedFileWritable.write(this);
			}
			else
			{
				KeyValueTableTypeWriterRegistry.write(this, value);
			}
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000604CE File Offset: 0x0005E8CE
		public virtual void writeValue<T>(string key, T value)
		{
			this.writeKey(key);
			this.writeValue<T>(value);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000604E0 File Offset: 0x0005E8E0
		public virtual void writeValue<T>(T value)
		{
			if (value is IFormattedFileWritable)
			{
				IFormattedFileWritable formattedFileWritable = value as IFormattedFileWritable;
				formattedFileWritable.write(this);
			}
			else
			{
				KeyValueTableTypeWriterRegistry.write<T>(this, value);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0006051C File Offset: 0x0005E91C
		public virtual void beginObject(string key)
		{
			this.writeKey(key);
			this.beginObject();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0006052C File Offset: 0x0005E92C
		public virtual void beginObject()
		{
			if (this.hasWritten)
			{
				this.writer.WriteLine();
			}
			this.writeIndents();
			this.writer.Write('{');
			this.indentationCount++;
			this.hasWritten = true;
			this.wroteKey = false;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0006057E File Offset: 0x0005E97E
		public virtual void endObject()
		{
			if (this.hasWritten)
			{
				this.writer.WriteLine();
			}
			this.indentationCount--;
			this.writeIndents();
			this.writer.Write('}');
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000605B7 File Offset: 0x0005E9B7
		public virtual void beginArray(string key)
		{
			this.writeKey(key);
			this.beginArray();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000605C8 File Offset: 0x0005E9C8
		public virtual void beginArray()
		{
			if (this.hasWritten)
			{
				this.writer.WriteLine();
			}
			this.writeIndents();
			this.writer.Write('[');
			this.indentationCount++;
			this.hasWritten = true;
			this.wroteKey = false;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0006061A File Offset: 0x0005EA1A
		public virtual void endArray()
		{
			if (this.hasWritten)
			{
				this.writer.WriteLine();
			}
			this.indentationCount--;
			this.writeIndents();
			this.writer.Write(']');
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00060654 File Offset: 0x0005EA54
		protected virtual void writeIndents()
		{
			for (int i = 0; i < this.indentationCount; i++)
			{
				this.writer.Write('\t');
			}
		}

		// Token: 0x040008E7 RID: 2279
		protected StreamWriter writer;

		// Token: 0x040008E8 RID: 2280
		protected int indentationCount;

		// Token: 0x040008E9 RID: 2281
		protected bool hasWritten;

		// Token: 0x040008EA RID: 2282
		protected bool wroteKey;
	}
}
