using System;

namespace SDG.Unturned
{
	// Token: 0x020004C5 RID: 1221
	public class Interactable2SalvageStructure : Interactable2
	{
		// Token: 0x06002097 RID: 8343 RVA: 0x000B2FD0 File Offset: 0x000B13D0
		public override bool checkHint(out EPlayerMessage message, out float data)
		{
			message = EPlayerMessage.SALVAGE;
			if (this.hp != null)
			{
				data = (float)this.hp.hp / 100f;
			}
			else
			{
				data = 0f;
			}
			return base.hasOwnership;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000B3020 File Offset: 0x000B1420
		public override void use()
		{
			StructureManager.salvageStructure(base.transform);
		}

		// Token: 0x0400136D RID: 4973
		public Interactable2HP hp;
	}
}
