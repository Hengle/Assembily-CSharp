using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004EB RID: 1259
	public class InteractableTank : Interactable
	{
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000BB6B8 File Offset: 0x000B9AB8
		public ETankSource source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000BB6C0 File Offset: 0x000B9AC0
		public ushort amount
		{
			get
			{
				return this._amount;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x000BB6C8 File Offset: 0x000B9AC8
		public ushort capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000BB6D0 File Offset: 0x000B9AD0
		public bool isRefillable
		{
			get
			{
				return this.amount < this.capacity;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x000BB6E0 File Offset: 0x000B9AE0
		public bool isSiphonable
		{
			get
			{
				return this.amount > 0;
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000BB6EB File Offset: 0x000B9AEB
		public void updateAmount(ushort newAmount)
		{
			this._amount = newAmount;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000BB6F4 File Offset: 0x000B9AF4
		public override void updateState(Asset asset, byte[] state)
		{
			this._amount = BitConverter.ToUInt16(state, 0);
			this._capacity = ((ItemTankAsset)asset).resource;
			this._source = ((ItemTankAsset)asset).source;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000BB725 File Offset: 0x000B9B25
		public override bool checkUseable()
		{
			return this.amount > 0;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000BB730 File Offset: 0x000B9B30
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.source == ETankSource.WATER)
			{
				message = EPlayerMessage.VOLUME_WATER;
				text = this.amount + "/" + this.capacity;
			}
			else
			{
				message = EPlayerMessage.VOLUME_FUEL;
				text = string.Empty;
			}
			color = Color.white;
			return true;
		}

		// Token: 0x04001458 RID: 5208
		private ETankSource _source;

		// Token: 0x04001459 RID: 5209
		private ushort _amount;

		// Token: 0x0400145A RID: 5210
		private ushort _capacity;
	}
}
