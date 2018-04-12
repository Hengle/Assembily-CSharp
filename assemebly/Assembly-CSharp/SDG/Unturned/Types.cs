using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000688 RID: 1672
	public class Types
	{
		// Token: 0x0400201A RID: 8218
		public static readonly Type STRING_TYPE = typeof(string);

		// Token: 0x0400201B RID: 8219
		public static readonly Type STRING_ARRAY_TYPE = typeof(string[]);

		// Token: 0x0400201C RID: 8220
		public static readonly Type BOOLEAN_TYPE = typeof(bool);

		// Token: 0x0400201D RID: 8221
		public static readonly Type BOOLEAN_ARRAY_TYPE = typeof(bool[]);

		// Token: 0x0400201E RID: 8222
		public static readonly Type BYTE_ARRAY_TYPE = typeof(byte[]);

		// Token: 0x0400201F RID: 8223
		public static readonly Type BYTE_TYPE = typeof(byte);

		// Token: 0x04002020 RID: 8224
		public static readonly Type INT16_TYPE = typeof(short);

		// Token: 0x04002021 RID: 8225
		public static readonly Type UINT16_TYPE = typeof(ushort);

		// Token: 0x04002022 RID: 8226
		public static readonly Type INT32_ARRAY_TYPE = typeof(int[]);

		// Token: 0x04002023 RID: 8227
		public static readonly Type INT32_TYPE = typeof(int);

		// Token: 0x04002024 RID: 8228
		public static readonly Type UINT32_TYPE = typeof(uint);

		// Token: 0x04002025 RID: 8229
		public static readonly Type SINGLE_TYPE = typeof(float);

		// Token: 0x04002026 RID: 8230
		public static readonly Type INT64_TYPE = typeof(long);

		// Token: 0x04002027 RID: 8231
		public static readonly Type UINT64_ARRAY_TYPE = typeof(ulong[]);

		// Token: 0x04002028 RID: 8232
		public static readonly Type UINT64_TYPE = typeof(ulong);

		// Token: 0x04002029 RID: 8233
		public static readonly Type STEAM_ID_TYPE = typeof(CSteamID);

		// Token: 0x0400202A RID: 8234
		public static readonly Type GUID_TYPE = typeof(Guid);

		// Token: 0x0400202B RID: 8235
		public static readonly Type VECTOR3_TYPE = typeof(Vector3);

		// Token: 0x0400202C RID: 8236
		public static readonly Type COLOR_TYPE = typeof(Color);

		// Token: 0x0400202D RID: 8237
		public static readonly byte[] SHIFTS = new byte[]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128
		};
	}
}
