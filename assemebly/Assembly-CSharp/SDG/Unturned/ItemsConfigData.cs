using System;

namespace SDG.Unturned
{
	// Token: 0x020006A3 RID: 1699
	public class ItemsConfigData
	{
		// Token: 0x060031C2 RID: 12738 RVA: 0x00143328 File Offset: 0x00141728
		public ItemsConfigData(EGameMode mode)
		{
			this.Despawn_Dropped_Time = 600f;
			this.Despawn_Natural_Time = 900f;
			switch (mode)
			{
			case EGameMode.EASY:
				this.Spawn_Chance = 0.35f;
				this.Respawn_Time = 30f;
				this.Quality_Full_Chance = 0.1f;
				this.Quality_Multiplier = 1f;
				this.Gun_Bullets_Full_Chance = 0.1f;
				this.Gun_Bullets_Multiplier = 1f;
				this.Magazine_Bullets_Full_Chance = 0.1f;
				this.Magazine_Bullets_Multiplier = 1f;
				this.Crate_Bullets_Full_Chance = 0.1f;
				this.Crate_Bullets_Multiplier = 1f;
				break;
			case EGameMode.NORMAL:
				this.Spawn_Chance = 0.35f;
				this.Respawn_Time = 45f;
				this.Quality_Full_Chance = 0.1f;
				this.Quality_Multiplier = 1f;
				this.Gun_Bullets_Full_Chance = 0.05f;
				this.Gun_Bullets_Multiplier = 0.25f;
				this.Magazine_Bullets_Full_Chance = 0.05f;
				this.Magazine_Bullets_Multiplier = 0.5f;
				this.Crate_Bullets_Full_Chance = 0.05f;
				this.Crate_Bullets_Multiplier = 1f;
				break;
			case EGameMode.HARD:
				this.Spawn_Chance = 0.15f;
				this.Respawn_Time = 60f;
				this.Quality_Full_Chance = 0.01f;
				this.Quality_Multiplier = 1f;
				this.Gun_Bullets_Full_Chance = 0.025f;
				this.Gun_Bullets_Multiplier = 0.1f;
				this.Magazine_Bullets_Full_Chance = 0.025f;
				this.Magazine_Bullets_Multiplier = 0.25f;
				this.Crate_Bullets_Full_Chance = 0.025f;
				this.Crate_Bullets_Multiplier = 0.75f;
				break;
			default:
				this.Spawn_Chance = 1f;
				this.Respawn_Time = 1000000f;
				this.Quality_Full_Chance = 1f;
				this.Quality_Multiplier = 1f;
				this.Gun_Bullets_Full_Chance = 1f;
				this.Gun_Bullets_Multiplier = 1f;
				this.Magazine_Bullets_Full_Chance = 1f;
				this.Magazine_Bullets_Multiplier = 1f;
				this.Crate_Bullets_Full_Chance = 1f;
				this.Crate_Bullets_Multiplier = 1f;
				break;
			}
			if (mode != EGameMode.EASY)
			{
				this.Has_Durability = true;
			}
			else
			{
				this.Has_Durability = false;
			}
		}

		// Token: 0x04002121 RID: 8481
		public float Spawn_Chance;

		// Token: 0x04002122 RID: 8482
		public float Despawn_Dropped_Time;

		// Token: 0x04002123 RID: 8483
		public float Despawn_Natural_Time;

		// Token: 0x04002124 RID: 8484
		public float Respawn_Time;

		// Token: 0x04002125 RID: 8485
		public float Quality_Full_Chance;

		// Token: 0x04002126 RID: 8486
		public float Quality_Multiplier;

		// Token: 0x04002127 RID: 8487
		public float Gun_Bullets_Full_Chance;

		// Token: 0x04002128 RID: 8488
		public float Gun_Bullets_Multiplier;

		// Token: 0x04002129 RID: 8489
		public float Magazine_Bullets_Full_Chance;

		// Token: 0x0400212A RID: 8490
		public float Magazine_Bullets_Multiplier;

		// Token: 0x0400212B RID: 8491
		public float Crate_Bullets_Full_Chance;

		// Token: 0x0400212C RID: 8492
		public float Crate_Bullets_Multiplier;

		// Token: 0x0400212D RID: 8493
		public bool Has_Durability;
	}
}
