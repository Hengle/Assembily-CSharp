using System;

namespace SDG.Unturned
{
	// Token: 0x020006AC RID: 1708
	public class EventsConfigData
	{
		// Token: 0x060031CB RID: 12747 RVA: 0x00143C80 File Offset: 0x00142080
		public EventsConfigData(EGameMode mode)
		{
			this.Rain_Frequency_Min = 2.3f;
			this.Rain_Frequency_Max = 5.6f;
			this.Rain_Duration_Min = 0.05f;
			this.Rain_Duration_Max = 0.15f;
			this.Snow_Frequency_Min = 1.3f;
			this.Snow_Frequency_Max = 4.6f;
			this.Snow_Duration_Min = 0.2f;
			this.Snow_Duration_Max = 0.5f;
			this.Airdrop_Frequency_Min = 0.8f;
			this.Airdrop_Frequency_Max = 6.5f;
			this.Airdrop_Speed = 128f;
			this.Airdrop_Force = 9.5f;
			this.Arena_Clear_Timer = 5u;
			this.Arena_Finale_Timer = 10u;
			this.Arena_Restart_Timer = 15u;
			this.Arena_Compactor_Delay_Timer = 1u;
			this.Arena_Compactor_Pause_Timer = 5u;
			this.Arena_Min_Players = 2u;
			this.Arena_Compactor_Damage = 10u;
			this.Arena_Use_Airdrops = true;
			this.Arena_Use_Compactor_Pause = true;
			this.Arena_Compactor_Speed_Tiny = 0.5f;
			this.Arena_Compactor_Speed_Small = 1.5f;
			this.Arena_Compactor_Speed_Medium = 3f;
			this.Arena_Compactor_Speed_Large = 4.5f;
			this.Arena_Compactor_Speed_Insane = 6f;
			this.Arena_Compactor_Shrink_Factor = 0.5f;
		}

		// Token: 0x04002183 RID: 8579
		public float Rain_Frequency_Min;

		// Token: 0x04002184 RID: 8580
		public float Rain_Frequency_Max;

		// Token: 0x04002185 RID: 8581
		public float Rain_Duration_Min;

		// Token: 0x04002186 RID: 8582
		public float Rain_Duration_Max;

		// Token: 0x04002187 RID: 8583
		public float Snow_Frequency_Min;

		// Token: 0x04002188 RID: 8584
		public float Snow_Frequency_Max;

		// Token: 0x04002189 RID: 8585
		public float Snow_Duration_Min;

		// Token: 0x0400218A RID: 8586
		public float Snow_Duration_Max;

		// Token: 0x0400218B RID: 8587
		public float Airdrop_Frequency_Min;

		// Token: 0x0400218C RID: 8588
		public float Airdrop_Frequency_Max;

		// Token: 0x0400218D RID: 8589
		public float Airdrop_Speed;

		// Token: 0x0400218E RID: 8590
		public float Airdrop_Force;

		// Token: 0x0400218F RID: 8591
		public uint Arena_Min_Players;

		// Token: 0x04002190 RID: 8592
		public uint Arena_Compactor_Damage;

		// Token: 0x04002191 RID: 8593
		public uint Arena_Clear_Timer;

		// Token: 0x04002192 RID: 8594
		public uint Arena_Finale_Timer;

		// Token: 0x04002193 RID: 8595
		public uint Arena_Restart_Timer;

		// Token: 0x04002194 RID: 8596
		public uint Arena_Compactor_Delay_Timer;

		// Token: 0x04002195 RID: 8597
		public uint Arena_Compactor_Pause_Timer;

		// Token: 0x04002196 RID: 8598
		public bool Arena_Use_Airdrops;

		// Token: 0x04002197 RID: 8599
		public bool Arena_Use_Compactor_Pause;

		// Token: 0x04002198 RID: 8600
		public float Arena_Compactor_Speed_Tiny;

		// Token: 0x04002199 RID: 8601
		public float Arena_Compactor_Speed_Small;

		// Token: 0x0400219A RID: 8602
		public float Arena_Compactor_Speed_Medium;

		// Token: 0x0400219B RID: 8603
		public float Arena_Compactor_Speed_Large;

		// Token: 0x0400219C RID: 8604
		public float Arena_Compactor_Speed_Insane;

		// Token: 0x0400219D RID: 8605
		public float Arena_Compactor_Shrink_Factor;
	}
}
