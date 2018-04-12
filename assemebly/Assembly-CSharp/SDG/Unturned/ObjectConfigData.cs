using System;

namespace SDG.Unturned
{
	// Token: 0x020006AA RID: 1706
	public class ObjectConfigData
	{
		// Token: 0x060031C9 RID: 12745 RVA: 0x00143C37 File Offset: 0x00142037
		public ObjectConfigData(EGameMode mode)
		{
			this.Binary_State_Reset_Multiplier = 1f;
			this.Fuel_Reset_Multiplier = 1f;
			this.Water_Reset_Multiplier = 1f;
			this.Resource_Reset_Multiplier = 1f;
			this.Rubble_Reset_Multiplier = 1f;
		}

		// Token: 0x0400217C RID: 8572
		public float Binary_State_Reset_Multiplier;

		// Token: 0x0400217D RID: 8573
		public float Fuel_Reset_Multiplier;

		// Token: 0x0400217E RID: 8574
		public float Water_Reset_Multiplier;

		// Token: 0x0400217F RID: 8575
		public float Resource_Reset_Multiplier;

		// Token: 0x04002180 RID: 8576
		public float Rubble_Reset_Multiplier;
	}
}
