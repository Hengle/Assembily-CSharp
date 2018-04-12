using System;

namespace SDG.Unturned
{
	// Token: 0x0200042D RID: 1069
	public class VendorElement
	{
		// Token: 0x06001D3D RID: 7485 RVA: 0x0009D550 File Offset: 0x0009B950
		public VendorElement(byte newIndex, ushort newID, uint newCost, INPCCondition[] newConditions)
		{
			this.index = newIndex;
			this.id = newID;
			this.cost = newCost;
			this.conditions = newConditions;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0009D575 File Offset: 0x0009B975
		// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0009D57D File Offset: 0x0009B97D
		public byte index { get; protected set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0009D586 File Offset: 0x0009B986
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0009D58E File Offset: 0x0009B98E
		public ushort id { get; protected set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0009D597 File Offset: 0x0009B997
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0009D59F File Offset: 0x0009B99F
		public uint cost { get; protected set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0009D5A8 File Offset: 0x0009B9A8
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0009D5B0 File Offset: 0x0009B9B0
		public INPCCondition[] conditions { get; protected set; }

		// Token: 0x06001D46 RID: 7494 RVA: 0x0009D5BC File Offset: 0x0009B9BC
		public bool areConditionsMet(Player player)
		{
			if (this.conditions != null)
			{
				for (int i = 0; i < this.conditions.Length; i++)
				{
					if (!this.conditions[i].isConditionMet(player))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0009D604 File Offset: 0x0009BA04
		public void applyConditions(Player player, bool shouldSend)
		{
			if (this.conditions != null)
			{
				for (int i = 0; i < this.conditions.Length; i++)
				{
					this.conditions[i].applyCondition(player, shouldSend);
				}
			}
		}
	}
}
