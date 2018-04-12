using System;

namespace SDG.Unturned
{
	// Token: 0x02000736 RID: 1846
	public class ItemIconInfo
	{
		// Token: 0x0400237F RID: 9087
		public ushort id;

		// Token: 0x04002380 RID: 9088
		public ushort skin;

		// Token: 0x04002381 RID: 9089
		public byte quality;

		// Token: 0x04002382 RID: 9090
		public byte[] state;

		// Token: 0x04002383 RID: 9091
		public ItemAsset itemAsset;

		// Token: 0x04002384 RID: 9092
		public SkinAsset skinAsset;

		// Token: 0x04002385 RID: 9093
		public string tags;

		// Token: 0x04002386 RID: 9094
		public string dynamic_props;

		// Token: 0x04002387 RID: 9095
		public int x;

		// Token: 0x04002388 RID: 9096
		public int y;

		// Token: 0x04002389 RID: 9097
		public bool scale;

		// Token: 0x0400238A RID: 9098
		public ItemIconReady callback;
	}
}
