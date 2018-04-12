using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005A2 RID: 1442
	public class ItemRegion
	{
		// Token: 0x06002853 RID: 10323 RVA: 0x000F4EA3 File Offset: 0x000F32A3
		public ItemRegion()
		{
			this._drops = new List<ItemDrop>();
			this.items = new List<ItemData>();
			this.isNetworked = false;
			this.lastRespawn = Time.realtimeSinceStartup;
			this.despawnItemIndex = 0;
			this.respawnItemIndex = 0;
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000F4EE1 File Offset: 0x000F32E1
		public List<ItemDrop> drops
		{
			get
			{
				return this._drops;
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000F4EEC File Offset: 0x000F32EC
		public void destroy()
		{
			ushort num = 0;
			while ((int)num < this.drops.Count)
			{
				UnityEngine.Object.Destroy(this.drops[(int)num].model.gameObject);
				num += 1;
			}
			this.drops.Clear();
		}

		// Token: 0x04001923 RID: 6435
		private List<ItemDrop> _drops;

		// Token: 0x04001924 RID: 6436
		public List<ItemData> items;

		// Token: 0x04001925 RID: 6437
		public bool isNetworked;

		// Token: 0x04001926 RID: 6438
		public ushort despawnItemIndex;

		// Token: 0x04001927 RID: 6439
		public ushort respawnItemIndex;

		// Token: 0x04001928 RID: 6440
		public float lastRespawn;
	}
}
