using System;

namespace SDG.Unturned
{
	// Token: 0x020003C4 RID: 964
	public class ItemBeaconAsset : ItemBarricadeAsset
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x000943AD File Offset: 0x000927AD
		public ItemBeaconAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._wave = data.readUInt16("Wave");
			this._rewards = data.readByte("Rewards");
			this._rewardID = data.readUInt16("Reward_ID");
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x000943ED File Offset: 0x000927ED
		public ushort wave
		{
			get
			{
				return this._wave;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x000943F5 File Offset: 0x000927F5
		public byte rewards
		{
			get
			{
				return this._rewards;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x000943FD File Offset: 0x000927FD
		public ushort rewardID
		{
			get
			{
				return this._rewardID;
			}
		}

		// Token: 0x04000F31 RID: 3889
		private ushort _wave;

		// Token: 0x04000F32 RID: 3890
		private byte _rewards;

		// Token: 0x04000F33 RID: 3891
		private ushort _rewardID;
	}
}
