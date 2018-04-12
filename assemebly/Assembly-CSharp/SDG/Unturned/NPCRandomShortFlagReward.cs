using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200040F RID: 1039
	public class NPCRandomShortFlagReward : NPCShortFlagReward
	{
		// Token: 0x06001C03 RID: 7171 RVA: 0x0009918F File Offset: 0x0009758F
		public NPCRandomShortFlagReward(ushort newID, short newMinValue, short newMaxValue, ENPCModificationType newModificationType, string newText) : base(newID, 0, newModificationType, newText)
		{
			base.id = newID;
			this.minValue = newMinValue;
			this.maxValue = newMaxValue;
			base.modificationType = newModificationType;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000991BA File Offset: 0x000975BA
		// (set) Token: 0x06001C05 RID: 7173 RVA: 0x000991C2 File Offset: 0x000975C2
		public short minValue { get; protected set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x000991CB File Offset: 0x000975CB
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x000991D3 File Offset: 0x000975D3
		public short maxValue { get; protected set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000991DC File Offset: 0x000975DC
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x000991F2 File Offset: 0x000975F2
		public override short value
		{
			get
			{
				return (short)UnityEngine.Random.Range((int)this.minValue, (int)(this.maxValue + 1));
			}
			protected set
			{
			}
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000991F4 File Offset: 0x000975F4
		public override void grantReward(Player player, bool shouldSend)
		{
			if (!Provider.isServer)
			{
				return;
			}
			base.grantReward(player, true);
		}
	}
}
