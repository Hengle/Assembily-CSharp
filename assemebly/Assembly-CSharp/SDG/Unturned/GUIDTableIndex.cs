using System;

namespace SDG.Unturned
{
	// Token: 0x020003B9 RID: 953
	public struct GUIDTableIndex
	{
		// Token: 0x06001A0D RID: 6669 RVA: 0x00091C5A File Offset: 0x0009005A
		public GUIDTableIndex(ushort index)
		{
			this.index = index;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00091C63 File Offset: 0x00090063
		public static implicit operator GUIDTableIndex(ushort value)
		{
			return new GUIDTableIndex(value);
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00091C6B File Offset: 0x0009006B
		public static explicit operator ushort(GUIDTableIndex value)
		{
			return value.index;
		}

		// Token: 0x04000EEF RID: 3823
		public static GUIDTableIndex invalid = new GUIDTableIndex(ushort.MaxValue);

		// Token: 0x04000EF0 RID: 3824
		public ushort index;
	}
}
