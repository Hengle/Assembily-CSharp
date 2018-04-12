using System;

namespace SDG.Unturned
{
	// Token: 0x020006A8 RID: 1704
	public class StructuresConfigData
	{
		// Token: 0x060031C7 RID: 12743 RVA: 0x001439F1 File Offset: 0x00141DF1
		public StructuresConfigData(EGameMode mode)
		{
			this.Decay_Time = 604800u;
			this.Armor_Multiplier = 1f;
		}

		// Token: 0x0400215E RID: 8542
		public uint Decay_Time;

		// Token: 0x0400215F RID: 8543
		public float Armor_Multiplier;
	}
}
