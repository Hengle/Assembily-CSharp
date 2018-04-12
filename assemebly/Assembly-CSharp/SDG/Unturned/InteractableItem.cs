using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004D1 RID: 1233
	public class InteractableItem : Interactable
	{
		// Token: 0x0600210A RID: 8458 RVA: 0x000B47C1 File Offset: 0x000B2BC1
		public override void use()
		{
			ItemManager.takeItem(base.transform.parent, byte.MaxValue, byte.MaxValue, 0, byte.MaxValue);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000B47E3 File Offset: 0x000B2BE3
		public override bool checkHighlight(out Color color)
		{
			color = ItemTool.getRarityColorHighlight(this.asset.rarity);
			return true;
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000B47FC File Offset: 0x000B2BFC
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.ITEM;
			text = this.asset.itemName;
			color = ItemTool.getRarityColorUI(this.asset.rarity);
			return true;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000B4828 File Offset: 0x000B2C28
		public void clampRange()
		{
			if (this.wasReset)
			{
				return;
			}
			if ((base.transform.position - base.transform.parent.position).sqrMagnitude > 400f)
			{
				base.transform.position = base.transform.parent.position;
				this.wasReset = true;
				ItemManager.clampedItems.RemoveFast(this);
				UnityEngine.Object.Destroy(base.GetComponent<Rigidbody>());
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000B48AB File Offset: 0x000B2CAB
		private void OnEnable()
		{
			ItemManager.clampedItems.Add(this);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000B48B8 File Offset: 0x000B2CB8
		private void OnDisable()
		{
			if (this.wasReset)
			{
				return;
			}
			ItemManager.clampedItems.RemoveFast(this);
		}

		// Token: 0x040013A6 RID: 5030
		public Item item;

		// Token: 0x040013A7 RID: 5031
		public ItemJar jar;

		// Token: 0x040013A8 RID: 5032
		public ItemAsset asset;

		// Token: 0x040013A9 RID: 5033
		private bool wasReset;
	}
}
