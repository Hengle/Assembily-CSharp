using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Framework.IO.FormattedFiles.KeyValueTables
{
	// Token: 0x020001BE RID: 446
	public class KeyValueTableTypeReaderRegistry
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x000600AC File Offset: 0x0005E4AC
		public static T read<T>(IFormattedFileReader input)
		{
			IFormattedTypeReader formattedTypeReader;
			if (KeyValueTableTypeReaderRegistry.readers.TryGetValue(typeof(T), out formattedTypeReader))
			{
				object obj = formattedTypeReader.read(input);
				if (obj == null)
				{
					return default(T);
				}
				return (T)((object)obj);
			}
			else
			{
				if (!typeof(T).IsEnum)
				{
					Debug.LogError("Failed to find reader for: " + typeof(T));
					return default(T);
				}
				string value = input.readValue();
				if (string.IsNullOrEmpty(value))
				{
					return default(T);
				}
				return (T)((object)Enum.Parse(typeof(T), value, true));
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0006015C File Offset: 0x0005E55C
		public static object read(IFormattedFileReader input, Type type)
		{
			IFormattedTypeReader formattedTypeReader;
			if (KeyValueTableTypeReaderRegistry.readers.TryGetValue(type, out formattedTypeReader))
			{
				object obj = formattedTypeReader.read(input);
				if (obj == null)
				{
					return type.getDefaultValue();
				}
				return obj;
			}
			else
			{
				if (!type.IsEnum)
				{
					Debug.LogError("Failed to find reader for: " + type);
					return type.getDefaultValue();
				}
				string value = input.readValue();
				if (string.IsNullOrEmpty(value))
				{
					return type.getDefaultValue();
				}
				return Enum.Parse(type, value, true);
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000601D5 File Offset: 0x0005E5D5
		public static void add<T>(IFormattedTypeReader reader)
		{
			KeyValueTableTypeReaderRegistry.add(typeof(T), reader);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000601E7 File Offset: 0x0005E5E7
		public static void add(Type type, IFormattedTypeReader reader)
		{
			KeyValueTableTypeReaderRegistry.readers.Add(type, reader);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000601F5 File Offset: 0x0005E5F5
		public static void remove<T>()
		{
			KeyValueTableTypeReaderRegistry.remove(typeof(T));
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00060206 File Offset: 0x0005E606
		public static void remove(Type type)
		{
			KeyValueTableTypeReaderRegistry.readers.Remove(type);
		}

		// Token: 0x040008E4 RID: 2276
		private static Dictionary<Type, IFormattedTypeReader> readers = new Dictionary<Type, IFormattedTypeReader>();
	}
}
