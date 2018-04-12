using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Unturned
{
	// Token: 0x02000430 RID: 1072
	public class ZombieDifficultyAsset : Asset, IDevkitAssetSpawnable
	{
		// Token: 0x06001D4F RID: 7503 RVA: 0x0009DA46 File Offset: 0x0009BE46
		public ZombieDifficultyAsset()
		{
			this.construct();
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x0009DA54 File Offset: 0x0009BE54
		public ZombieDifficultyAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.construct();
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x0009DA65 File Offset: 0x0009BE65
		public void devkitAssetSpawn()
		{
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x0009DA68 File Offset: 0x0009BE68
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.Crawler_Chance = reader.readValue<float>("Crawler_Chance");
			this.Sprinter_Chance = reader.readValue<float>("Sprinter_Chance");
			this.Flanker_Chance = reader.readValue<float>("Flanker_Chance");
			this.Burner_Chance = reader.readValue<float>("Burner_Chance");
			this.Acid_Chance = reader.readValue<float>("Acid_Chance");
			this.Boss_Electric_Chance = reader.readValue<float>("Boss_Electric_Chance");
			this.Boss_Wind_Chance = reader.readValue<float>("Boss_Wind_Chance");
			this.Boss_Fire_Chance = reader.readValue<float>("Boss_Fire_Chance");
			this.Spirit_Chance = reader.readValue<float>("Spirit_Chance");
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0009DB18 File Offset: 0x0009BF18
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<float>("Crawler_Chance", this.Crawler_Chance);
			writer.writeValue<float>("Sprinter_Chance", this.Sprinter_Chance);
			writer.writeValue<float>("Flanker_Chance", this.Flanker_Chance);
			writer.writeValue<float>("Burner_Chance", this.Burner_Chance);
			writer.writeValue<float>("Acid_Chance", this.Acid_Chance);
			writer.writeValue<float>("Boss_Electric_Chance", this.Boss_Electric_Chance);
			writer.writeValue<float>("Boss_Wind_Chance", this.Boss_Wind_Chance);
			writer.writeValue<float>("Boss_Fire_Chance", this.Boss_Fire_Chance);
			writer.writeValue<float>("Spirit_Chance", this.Spirit_Chance);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0009DBC5 File Offset: 0x0009BFC5
		protected virtual void construct()
		{
		}

		// Token: 0x0400115D RID: 4445
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Crawler_Chance.Name", null)]
		public float Crawler_Chance;

		// Token: 0x0400115E RID: 4446
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Sprinter_Chance.Name", null)]
		public float Sprinter_Chance;

		// Token: 0x0400115F RID: 4447
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Flanker_Chance.Name", null)]
		public float Flanker_Chance;

		// Token: 0x04001160 RID: 4448
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Burner_Chance.Name", null)]
		public float Burner_Chance;

		// Token: 0x04001161 RID: 4449
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Acid_Chance.Name", null)]
		public float Acid_Chance;

		// Token: 0x04001162 RID: 4450
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Boss_Electric_Chance.Name", null)]
		public float Boss_Electric_Chance;

		// Token: 0x04001163 RID: 4451
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Boss_Wind_Chance.Name", null)]
		public float Boss_Wind_Chance;

		// Token: 0x04001164 RID: 4452
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Boss_Fire_Chance.Name", null)]
		public float Boss_Fire_Chance;

		// Token: 0x04001165 RID: 4453
		[Inspectable("#SDG::Asset.Zombie_Difficulty.Spirit_Chance.Name", null)]
		public float Spirit_Chance;
	}
}
