using System;

namespace SDG.Unturned
{
	// Token: 0x0200052F RID: 1327
	public class FlagData
	{
		// Token: 0x060023CD RID: 9165 RVA: 0x000C6BFC File Offset: 0x000C4FFC
		public FlagData(string newDifficultyGUID = "", byte newMaxZombies = 64, bool newSpawnZombies = true)
		{
			this.difficultyGUID = newDifficultyGUID;
			this.maxZombies = newMaxZombies;
			this.spawnZombies = newSpawnZombies;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000C6C19 File Offset: 0x000C5019
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x000C6C24 File Offset: 0x000C5024
		public string difficultyGUID
		{
			get
			{
				return this._difficultyGUID;
			}
			set
			{
				this._difficultyGUID = value;
				try
				{
					this.difficulty = new AssetReference<ZombieDifficultyAsset>(new Guid(this.difficultyGUID));
				}
				catch
				{
					this.difficulty = AssetReference<ZombieDifficultyAsset>.invalid;
				}
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000C6C74 File Offset: 0x000C5074
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x000C6C7C File Offset: 0x000C507C
		public AssetReference<ZombieDifficultyAsset> difficulty { get; private set; }

		// Token: 0x040015DD RID: 5597
		private string _difficultyGUID;

		// Token: 0x040015DF RID: 5599
		public byte maxZombies;

		// Token: 0x040015E0 RID: 5600
		public bool spawnZombies;
	}
}
