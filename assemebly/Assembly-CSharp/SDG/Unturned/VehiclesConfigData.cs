using System;

namespace SDG.Unturned
{
	// Token: 0x020006A4 RID: 1700
	public class VehiclesConfigData
	{
		// Token: 0x060031C3 RID: 12739 RVA: 0x00143558 File Offset: 0x00141958
		public VehiclesConfigData(EGameMode mode)
		{
			this.Has_Battery_Chance = 0.8f;
			this.Min_Battery_Charge = 0.5f;
			this.Max_Battery_Charge = 0.75f;
			switch (mode)
			{
			case EGameMode.EASY:
				this.Has_Battery_Chance = 1f;
				this.Min_Battery_Charge = 0.8f;
				this.Max_Battery_Charge = 1f;
				this.Has_Tire_Chance = 1f;
				break;
			case EGameMode.NORMAL:
				this.Has_Battery_Chance = 0.8f;
				this.Min_Battery_Charge = 0.5f;
				this.Max_Battery_Charge = 0.75f;
				this.Has_Tire_Chance = 0.85f;
				break;
			case EGameMode.HARD:
				this.Has_Battery_Chance = 0.25f;
				this.Min_Battery_Charge = 0.1f;
				this.Max_Battery_Charge = 0.3f;
				this.Has_Tire_Chance = 0.7f;
				break;
			default:
				this.Has_Battery_Chance = 1f;
				this.Min_Battery_Charge = 1f;
				this.Max_Battery_Charge = 1f;
				this.Has_Tire_Chance = 1f;
				break;
			}
			this.Respawn_Time = 300f;
			this.Unlocked_After_Seconds_In_Safezone = 3600f;
			this.Armor_Multiplier = 1f;
			this.Max_Instances_Tiny = 4u;
			this.Max_Instances_Small = 8u;
			this.Max_Instances_Medium = 16u;
			this.Max_Instances_Large = 32u;
			this.Max_Instances_Insane = 64u;
		}

		// Token: 0x0400212E RID: 8494
		public float Has_Battery_Chance;

		// Token: 0x0400212F RID: 8495
		public float Min_Battery_Charge;

		// Token: 0x04002130 RID: 8496
		public float Max_Battery_Charge;

		// Token: 0x04002131 RID: 8497
		public float Has_Tire_Chance;

		// Token: 0x04002132 RID: 8498
		public float Respawn_Time;

		// Token: 0x04002133 RID: 8499
		public float Unlocked_After_Seconds_In_Safezone;

		// Token: 0x04002134 RID: 8500
		public float Armor_Multiplier;

		// Token: 0x04002135 RID: 8501
		public uint Max_Instances_Tiny;

		// Token: 0x04002136 RID: 8502
		public uint Max_Instances_Small;

		// Token: 0x04002137 RID: 8503
		public uint Max_Instances_Medium;

		// Token: 0x04002138 RID: 8504
		public uint Max_Instances_Large;

		// Token: 0x04002139 RID: 8505
		public uint Max_Instances_Insane;
	}
}
