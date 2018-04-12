using System;
using System.Collections.Generic;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001C0 RID: 448
	public class KeyValueTableTypeWriterRegistry
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x000602AC File Offset: 0x0005E6AC
		public static void write<T>(IFormattedFileWriter output, T value)
		{
			IFormattedTypeWriter formattedTypeWriter;
			if (KeyValueTableTypeWriterRegistry.writers.TryGetValue(typeof(T), out formattedTypeWriter))
			{
				formattedTypeWriter.write(output, value);
			}
			else
			{
				output.writeValue(value.ToString());
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000602FC File Offset: 0x0005E6FC
		public static void write(IFormattedFileWriter output, object value)
		{
			IFormattedTypeWriter formattedTypeWriter;
			if (KeyValueTableTypeWriterRegistry.writers.TryGetValue(value.GetType(), out formattedTypeWriter))
			{
				formattedTypeWriter.write(output, value);
			}
			else
			{
				output.writeValue(value.ToString());
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00060339 File Offset: 0x0005E739
		public static void add<T>(IFormattedTypeWriter writer)
		{
			KeyValueTableTypeWriterRegistry.add(typeof(T), writer);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0006034B File Offset: 0x0005E74B
		public static void add(Type type, IFormattedTypeWriter writer)
		{
			KeyValueTableTypeWriterRegistry.writers.Add(type, writer);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00060359 File Offset: 0x0005E759
		public static void remove<T>()
		{
			KeyValueTableTypeWriterRegistry.remove(typeof(T));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0006036A File Offset: 0x0005E76A
		public static void remove(Type type)
		{
			KeyValueTableTypeWriterRegistry.writers.Remove(type);
		}

		// Token: 0x040008E6 RID: 2278
		private static Dictionary<Type, IFormattedTypeWriter> writers = new Dictionary<Type, IFormattedTypeWriter>();
	}
}
