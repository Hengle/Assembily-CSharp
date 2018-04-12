using System;

namespace SDG.Unturned
{
	// Token: 0x0200060C RID: 1548
	public class HotkeyInfo
	{
		// Token: 0x06002B8D RID: 11149 RVA: 0x00110980 File Offset: 0x0010ED80
		public HotkeyInfo()
		{
			this.id = 0;
			this.page = byte.MaxValue;
			this.x = byte.MaxValue;
			this.y = byte.MaxValue;
		}

		// Token: 0x04001BEE RID: 7150
		public ushort id;

		// Token: 0x04001BEF RID: 7151
		public byte page;

		// Token: 0x04001BF0 RID: 7152
		public byte x;

		// Token: 0x04001BF1 RID: 7153
		public byte y;
	}
}
