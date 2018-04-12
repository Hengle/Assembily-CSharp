using System;

namespace SDG.Unturned
{
	// Token: 0x020006A7 RID: 1703
	public class BarricadesConfigData
	{
		// Token: 0x060031C6 RID: 12742 RVA: 0x001439D3 File Offset: 0x00141DD3
		public BarricadesConfigData(EGameMode mode)
		{
			this.Decay_Time = 604800u;
			this.Armor_Multiplier = 1f;
		}

		// Token: 0x0400215C RID: 8540
		public uint Decay_Time;

		// Token: 0x0400215D RID: 8541
		public float Armor_Multiplier;
	}
}
