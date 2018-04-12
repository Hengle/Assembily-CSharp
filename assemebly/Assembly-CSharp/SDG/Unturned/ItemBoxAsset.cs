using System;

namespace SDG.Unturned
{
	// Token: 0x020003DE RID: 990
	public class ItemBoxAsset : ItemAsset
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x000969DC File Offset: 0x00094DDC
		public ItemBoxAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._generate = data.readInt32("Generate");
			this._destroy = data.readInt32("Destroy");
			this._drops = new int[data.readInt32("Drops")];
			for (int i = 0; i < this.drops.Length; i++)
			{
				this.drops[i] = data.readInt32("Drop_" + i);
			}
			bundle.unload();
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00096A69 File Offset: 0x00094E69
		public int generate
		{
			get
			{
				return this._generate;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00096A71 File Offset: 0x00094E71
		public int destroy
		{
			get
			{
				return this._destroy;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x00096A79 File Offset: 0x00094E79
		public int[] drops
		{
			get
			{
				return this._drops;
			}
		}

		// Token: 0x04000FD2 RID: 4050
		protected int _generate;

		// Token: 0x04000FD3 RID: 4051
		protected int _destroy;

		// Token: 0x04000FD4 RID: 4052
		protected int[] _drops;
	}
}
