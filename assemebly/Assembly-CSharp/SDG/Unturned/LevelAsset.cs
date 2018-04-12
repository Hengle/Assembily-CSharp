using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Unturned
{
	// Token: 0x020003F6 RID: 1014
	public class LevelAsset : Asset
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x00097731 File Offset: 0x00095B31
		public LevelAsset()
		{
			this.supportedGameModes = new InspectableList<TypeReference<GameMode>>();
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00097744 File Offset: 0x00095B44
		public LevelAsset(Bundle bundle, Local localization, byte[] hash) : base(bundle, localization, hash)
		{
			this.supportedGameModes = new InspectableList<TypeReference<GameMode>>();
			bundle.unload();
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00097760 File Offset: 0x00095B60
		public LevelAsset(Bundle bundle, Data data, Local localization, ushort id) : base(bundle, data, localization, id)
		{
			this.supportedGameModes = new InspectableList<TypeReference<GameMode>>();
			bundle.unload();
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00097780 File Offset: 0x00095B80
		protected override void readAsset(IFormattedFileReader reader)
		{
			base.readAsset(reader);
			this.defaultGameMode = reader.readValue<TypeReference<GameMode>>("Default_Game_Mode");
			int num = reader.readArrayLength("Supported_Game_Modes");
			for (int i = 0; i < num; i++)
			{
				this.supportedGameModes.Add(reader.readValue<TypeReference<GameMode>>(i));
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000977D8 File Offset: 0x00095BD8
		protected override void writeAsset(IFormattedFileWriter writer)
		{
			base.writeAsset(writer);
			writer.writeValue<TypeReference<GameMode>>("Default_Game_Mode", this.defaultGameMode);
			writer.beginArray("Supported_Game_Modes");
			foreach (TypeReference<GameMode> value in this.supportedGameModes)
			{
				writer.writeValue<TypeReference<GameMode>>(value);
			}
			writer.endArray();
		}

		// Token: 0x0400103B RID: 4155
		[Inspectable("#SDG::Asset.Level.Default_Game_Mode.Name", null)]
		public TypeReference<GameMode> defaultGameMode;

		// Token: 0x0400103C RID: 4156
		[Inspectable("#SDG::Asset.Level.Supported_Game_Modes.Name", null)]
		public InspectableList<TypeReference<GameMode>> supportedGameModes;
	}
}
