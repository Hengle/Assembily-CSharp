using System;

namespace SDG.Unturned
{
	// Token: 0x020003E7 RID: 999
	public class ItemStorageAsset : ItemBarricadeAsset
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x00096C48 File Offset: 0x00095048
		public ItemStorageAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._storage_x = data.readByte("Storage_X");
			if (this.storage_x < 1)
			{
				this._storage_x = 1;
			}
			this._storage_y = data.readByte("Storage_Y");
			if (this.storage_y < 1)
			{
				this._storage_y = 1;
			}
			this._isDisplay = data.has("Display");
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00096CB9 File Offset: 0x000950B9
		public byte storage_x
		{
			get
			{
				return this._storage_x;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00096CC1 File Offset: 0x000950C1
		public byte storage_y
		{
			get
			{
				return this._storage_y;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x00096CC9 File Offset: 0x000950C9
		public bool isDisplay
		{
			get
			{
				return this._isDisplay;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00096CD1 File Offset: 0x000950D1
		public override byte[] getState(EItemOrigin origin)
		{
			if (this.isDisplay)
			{
				return new byte[21];
			}
			return new byte[17];
		}

		// Token: 0x04000FED RID: 4077
		protected byte _storage_x;

		// Token: 0x04000FEE RID: 4078
		protected byte _storage_y;

		// Token: 0x04000FEF RID: 4079
		protected bool _isDisplay;
	}
}
