using System;

namespace SDG.Unturned
{
	// Token: 0x020006A5 RID: 1701
	public class ZombiesConfigData
	{
		// Token: 0x060031C4 RID: 12740 RVA: 0x001436B0 File Offset: 0x00141AB0
		public ZombiesConfigData(EGameMode mode)
		{
			this.Respawn_Day_Time = 360f;
			this.Respawn_Night_Time = 30f;
			this.Respawn_Beacon_Time = 0f;
			switch (mode)
			{
			case EGameMode.EASY:
				this.Spawn_Chance = 0.2f;
				this.Loot_Chance = 0.55f;
				this.Crawler_Chance = 0f;
				this.Sprinter_Chance = 0f;
				this.Flanker_Chance = 0f;
				this.Burner_Chance = 0f;
				this.Acid_Chance = 0f;
				break;
			case EGameMode.NORMAL:
				this.Spawn_Chance = 0.25f;
				this.Loot_Chance = 0.5f;
				this.Crawler_Chance = 0.15f;
				this.Sprinter_Chance = 0.15f;
				this.Flanker_Chance = 0.025f;
				this.Burner_Chance = 0.025f;
				this.Acid_Chance = 0.025f;
				break;
			case EGameMode.HARD:
				this.Spawn_Chance = 0.3f;
				this.Loot_Chance = 0.3f;
				this.Crawler_Chance = 0.125f;
				this.Sprinter_Chance = 0.175f;
				this.Flanker_Chance = 0.05f;
				this.Burner_Chance = 0.05f;
				this.Acid_Chance = 0.05f;
				break;
			default:
				this.Spawn_Chance = 1f;
				this.Loot_Chance = 0f;
				this.Crawler_Chance = 0f;
				this.Sprinter_Chance = 0f;
				this.Flanker_Chance = 0f;
				this.Burner_Chance = 0f;
				this.Acid_Chance = 0f;
				break;
			}
			this.Boss_Electric_Chance = 0f;
			this.Boss_Wind_Chance = 0f;
			this.Boss_Fire_Chance = 0f;
			this.Spirit_Chance = 0f;
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
			this.Beacon_Experience_Multiplier = 1f;
			this.Full_Moon_Experience_Multiplier = 2f;
			this.Min_Drops = 1u;
			this.Max_Drops = 1u;
			this.Min_Mega_Drops = 5u;
			this.Max_Mega_Drops = 5u;
			this.Min_Boss_Drops = 8u;
			this.Max_Boss_Drops = 10u;
			this.Slow_Movement = (mode == EGameMode.EASY);
			this.Can_Stun = (mode != EGameMode.HARD);
		}

		// Token: 0x0400213A RID: 8506
		public float Spawn_Chance;

		// Token: 0x0400213B RID: 8507
		public float Loot_Chance;

		// Token: 0x0400213C RID: 8508
		public float Crawler_Chance;

		// Token: 0x0400213D RID: 8509
		public float Sprinter_Chance;

		// Token: 0x0400213E RID: 8510
		public float Flanker_Chance;

		// Token: 0x0400213F RID: 8511
		public float Burner_Chance;

		// Token: 0x04002140 RID: 8512
		public float Acid_Chance;

		// Token: 0x04002141 RID: 8513
		public float Boss_Electric_Chance;

		// Token: 0x04002142 RID: 8514
		public float Boss_Wind_Chance;

		// Token: 0x04002143 RID: 8515
		public float Boss_Fire_Chance;

		// Token: 0x04002144 RID: 8516
		public float Spirit_Chance;

		// Token: 0x04002145 RID: 8517
		public float Respawn_Day_Time;

		// Token: 0x04002146 RID: 8518
		public float Respawn_Night_Time;

		// Token: 0x04002147 RID: 8519
		public float Respawn_Beacon_Time;

		// Token: 0x04002148 RID: 8520
		public float Damage_Multiplier;

		// Token: 0x04002149 RID: 8521
		public float Armor_Multiplier;

		// Token: 0x0400214A RID: 8522
		public float Beacon_Experience_Multiplier;

		// Token: 0x0400214B RID: 8523
		public float Full_Moon_Experience_Multiplier;

		// Token: 0x0400214C RID: 8524
		public uint Min_Drops;

		// Token: 0x0400214D RID: 8525
		public uint Max_Drops;

		// Token: 0x0400214E RID: 8526
		public uint Min_Mega_Drops;

		// Token: 0x0400214F RID: 8527
		public uint Max_Mega_Drops;

		// Token: 0x04002150 RID: 8528
		public uint Min_Boss_Drops;

		// Token: 0x04002151 RID: 8529
		public uint Max_Boss_Drops;

		// Token: 0x04002152 RID: 8530
		public bool Slow_Movement;

		// Token: 0x04002153 RID: 8531
		public bool Can_Stun;
	}
}
