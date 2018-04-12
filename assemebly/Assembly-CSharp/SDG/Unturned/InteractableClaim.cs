using System;

namespace SDG.Unturned
{
	// Token: 0x020004C9 RID: 1225
	public class InteractableClaim : Interactable
	{
		// Token: 0x060020C1 RID: 8385 RVA: 0x000B3853 File Offset: 0x000B1C53
		public void updateState(ItemBarricadeAsset asset)
		{
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000B3855 File Offset: 0x000B1C55
		public override bool checkInteractable()
		{
			return false;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000B3858 File Offset: 0x000B1C58
		private void registerBubble()
		{
			if (this.bubble != null)
			{
				return;
			}
			if (base.isPlant)
			{
				return;
			}
			this.bubble = ClaimManager.registerBubble(base.transform.position, 32f, this.owner, this.group);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000B38A4 File Offset: 0x000B1CA4
		private void deregisterBubble()
		{
			if (this.bubble == null)
			{
				return;
			}
			ClaimManager.deregisterBubble(this.bubble);
			this.bubble = null;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000B38C4 File Offset: 0x000B1CC4
		private void Start()
		{
			this.registerBubble();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000B38CC File Offset: 0x000B1CCC
		private void OnDestroy()
		{
			this.deregisterBubble();
		}

		// Token: 0x04001387 RID: 4999
		public ulong owner;

		// Token: 0x04001388 RID: 5000
		public ulong group;

		// Token: 0x04001389 RID: 5001
		private ClaimBubble bubble;
	}
}
