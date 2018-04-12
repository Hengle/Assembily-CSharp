using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020003CD RID: 973
	public class ItemFisherAsset : ItemAsset
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x00094D3C File Offset: 0x0009313C
		public ItemFisherAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this._cast = (AudioClip)bundle.load("Cast");
			this._reel = (AudioClip)bundle.load("Reel");
			this._tug = (AudioClip)bundle.load("Tug");
			this._rewardID = data.readUInt16("Reward_ID");
			bundle.unload();
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00094DAD File Offset: 0x000931AD
		public AudioClip cast
		{
			get
			{
				return this._cast;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00094DB5 File Offset: 0x000931B5
		public AudioClip reel
		{
			get
			{
				return this._reel;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00094DBD File Offset: 0x000931BD
		public AudioClip tug
		{
			get
			{
				return this._tug;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00094DC5 File Offset: 0x000931C5
		public ushort rewardID
		{
			get
			{
				return this._rewardID;
			}
		}

		// Token: 0x04000F5D RID: 3933
		private AudioClip _cast;

		// Token: 0x04000F5E RID: 3934
		private AudioClip _reel;

		// Token: 0x04000F5F RID: 3935
		private AudioClip _tug;

		// Token: 0x04000F60 RID: 3936
		private ushort _rewardID;
	}
}
