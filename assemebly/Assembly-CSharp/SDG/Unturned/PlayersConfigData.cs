using System;

namespace SDG.Unturned
{
	// Token: 0x020006A9 RID: 1705
	public class PlayersConfigData
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x00143A10 File Offset: 0x00141E10
		public PlayersConfigData(EGameMode mode)
		{
			this.Health_Regen_Min_Food = 90u;
			this.Health_Regen_Min_Water = 90u;
			this.Health_Regen_Ticks = 60u;
			this.Food_Damage_Ticks = 15u;
			this.Water_Damage_Ticks = 20u;
			this.Virus_Infect = 50u;
			this.Virus_Use_Ticks = 125u;
			this.Virus_Damage_Ticks = 25u;
			this.Leg_Regen_Ticks = 750u;
			this.Bleed_Damage_Ticks = 10u;
			this.Bleed_Regen_Ticks = 750u;
			if (mode != EGameMode.EASY)
			{
				if (mode != EGameMode.HARD)
				{
					this.Food_Use_Ticks = 300u;
					this.Water_Use_Ticks = 270u;
				}
				else
				{
					this.Food_Use_Ticks = 250u;
					this.Water_Use_Ticks = 220u;
				}
			}
			else
			{
				this.Food_Use_Ticks = 350u;
				this.Water_Use_Ticks = 320u;
			}
			switch (mode)
			{
			case EGameMode.EASY:
				this.Experience_Multiplier = 1.5f;
				break;
			case EGameMode.NORMAL:
				this.Experience_Multiplier = 1f;
				break;
			case EGameMode.HARD:
				this.Experience_Multiplier = 1.5f;
				break;
			default:
				this.Experience_Multiplier = 10f;
				break;
			}
			if (mode != EGameMode.EASY)
			{
				if (mode != EGameMode.HARD)
				{
					this.Detect_Radius_Multiplier = 1f;
				}
				else
				{
					this.Detect_Radius_Multiplier = 1.25f;
				}
			}
			else
			{
				this.Detect_Radius_Multiplier = 0.5f;
			}
			this.Ray_Aggressor_Distance = 8f;
			this.Armor_Multiplier = 1f;
			this.Lose_Skills_PvP = 0.75f;
			this.Lose_Skills_PvE = 0.75f;
			this.Lose_Items_PvP = 1f;
			this.Lose_Items_PvE = 1f;
			this.Lose_Clothes_PvP = true;
			this.Lose_Clothes_PvE = true;
			this.Can_Hurt_Legs = true;
			if (mode != EGameMode.EASY)
			{
				this.Can_Break_Legs = true;
				this.Can_Start_Bleeding = true;
			}
			else
			{
				this.Can_Break_Legs = false;
				this.Can_Start_Bleeding = false;
			}
			if (mode != EGameMode.HARD)
			{
				this.Can_Fix_Legs = true;
				this.Can_Stop_Bleeding = true;
			}
			else
			{
				this.Can_Fix_Legs = false;
				this.Can_Stop_Bleeding = false;
			}
		}

		// Token: 0x04002160 RID: 8544
		public uint Health_Regen_Min_Food;

		// Token: 0x04002161 RID: 8545
		public uint Health_Regen_Min_Water;

		// Token: 0x04002162 RID: 8546
		public uint Health_Regen_Ticks;

		// Token: 0x04002163 RID: 8547
		public uint Food_Use_Ticks;

		// Token: 0x04002164 RID: 8548
		public uint Food_Damage_Ticks;

		// Token: 0x04002165 RID: 8549
		public uint Water_Use_Ticks;

		// Token: 0x04002166 RID: 8550
		public uint Water_Damage_Ticks;

		// Token: 0x04002167 RID: 8551
		public uint Virus_Infect;

		// Token: 0x04002168 RID: 8552
		public uint Virus_Use_Ticks;

		// Token: 0x04002169 RID: 8553
		public uint Virus_Damage_Ticks;

		// Token: 0x0400216A RID: 8554
		public uint Leg_Regen_Ticks;

		// Token: 0x0400216B RID: 8555
		public uint Bleed_Damage_Ticks;

		// Token: 0x0400216C RID: 8556
		public uint Bleed_Regen_Ticks;

		// Token: 0x0400216D RID: 8557
		public float Armor_Multiplier;

		// Token: 0x0400216E RID: 8558
		public float Experience_Multiplier;

		// Token: 0x0400216F RID: 8559
		public float Detect_Radius_Multiplier;

		// Token: 0x04002170 RID: 8560
		public float Ray_Aggressor_Distance;

		// Token: 0x04002171 RID: 8561
		public float Lose_Skills_PvP;

		// Token: 0x04002172 RID: 8562
		public float Lose_Skills_PvE;

		// Token: 0x04002173 RID: 8563
		public float Lose_Items_PvP;

		// Token: 0x04002174 RID: 8564
		public float Lose_Items_PvE;

		// Token: 0x04002175 RID: 8565
		public bool Lose_Clothes_PvP;

		// Token: 0x04002176 RID: 8566
		public bool Lose_Clothes_PvE;

		// Token: 0x04002177 RID: 8567
		public bool Can_Hurt_Legs;

		// Token: 0x04002178 RID: 8568
		public bool Can_Break_Legs;

		// Token: 0x04002179 RID: 8569
		public bool Can_Fix_Legs;

		// Token: 0x0400217A RID: 8570
		public bool Can_Start_Bleeding;

		// Token: 0x0400217B RID: 8571
		public bool Can_Stop_Bleeding;
	}
}
