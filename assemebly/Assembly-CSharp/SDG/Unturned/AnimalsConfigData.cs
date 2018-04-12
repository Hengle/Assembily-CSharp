using System;

namespace SDG.Unturned
{
	// Token: 0x020006A6 RID: 1702
	public class AnimalsConfigData
	{
		// Token: 0x060031C5 RID: 12741 RVA: 0x0014392C File Offset: 0x00141D2C
		public AnimalsConfigData(EGameMode mode)
		{
			this.Respawn_Time = 180f;
			if (mode != EGameMode.EASY)
			{
				if (mode != EGameMode.HARD)
				{
					this.Damage_Multiplier = 1f;
					this.Armor_Multiplier = 1f;
				}
				else
				{
					this.Damage_Multiplier = 1.5f;
					this.Armor_Multiplier = 0.75f;
				}
			}
			else
			{
				this.Damage_Multiplier = 0.75f;
				this.Armor_Multiplier = 1.25f;
			}
			this.Max_Instances_Tiny = 4u;
			this.Max_Instances_Small = 8u;
			this.Max_Instances_Medium = 16u;
			this.Max_Instances_Large = 32u;
			this.Max_Instances_Insane = 64u;
		}

		// Token: 0x04002154 RID: 8532
		public float Respawn_Time;

		// Token: 0x04002155 RID: 8533
		public float Damage_Multiplier;

		// Token: 0x04002156 RID: 8534
		public float Armor_Multiplier;

		// Token: 0x04002157 RID: 8535
		public uint Max_Instances_Tiny;

		// Token: 0x04002158 RID: 8536
		public uint Max_Instances_Small;

		// Token: 0x04002159 RID: 8537
		public uint Max_Instances_Medium;

		// Token: 0x0400215A RID: 8538
		public uint Max_Instances_Large;

		// Token: 0x0400215B RID: 8539
		public uint Max_Instances_Insane;
	}
}
