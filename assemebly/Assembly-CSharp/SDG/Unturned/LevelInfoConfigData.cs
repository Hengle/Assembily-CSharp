using System;
using System.Collections.Generic;
using System.Globalization;

namespace SDG.Unturned
{
	// Token: 0x0200054A RID: 1354
	public class LevelInfoConfigData
	{
		// Token: 0x060024CC RID: 9420 RVA: 0x000D2D5C File Offset: 0x000D115C
		public LevelInfoConfigData()
		{
			this.Creators = new string[0];
			this.Collaborators = new string[0];
			this.Thanks = new string[0];
			this.Item = 0;
			this.Feedback = null;
			this.Status = EMapStatus.NONE;
			this.Load_From_Resources = false;
			this.Asset = AssetReference<LevelAsset>.invalid;
			this.Trains = new List<LevelTrainAssociation>();
			this.Mode_Config_Overrides = new Dictionary<string, object>();
			this.Allow_Underwater_Features = false;
			this.Terrain_Snow_Sparkle = false;
			this.Use_Legacy_Clip_Borders = true;
			this.Use_Legacy_Ground = true;
			this.Use_Legacy_Water = true;
			this.Use_Legacy_Objects = true;
			this.Use_Legacy_Snow_Height = true;
			this.Use_Legacy_Fog_Height = true;
			this.Use_Legacy_Oxygen_Height = true;
			this.Use_Rain_Volumes = false;
			this.Use_Snow_Volumes = false;
			this.Is_Aurora_Borealis_Visible = false;
			this.Snow_Affects_Temperature = true;
			this.Has_Atmosphere = true;
			this.Has_Discord_Rich_Presence = false;
			this.Can_Use_Bundles = true;
			this.Gravity = -9.81f;
			this.Blimp_Altitude = 150f;
			this.Category = ESingleplayerMapCategory.MISC;
			this.Visible_In_Matchmaking = false;
			this.Use_Arena_Compactor = true;
			this.Arena_Loadouts = new List<ArenaLoadout>();
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000D2E78 File Offset: 0x000D1278
		public DateTime getCuratedMapTimestamp()
		{
			DateTime result;
			if (DateTime.TryParseExact(this.CuratedMapTimestamp, "yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out result))
			{
				return result;
			}
			return DateTime.MinValue;
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x000D2EAC File Offset: 0x000D12AC
		public bool isNowBeforeCuratedMapTimestamp
		{
			get
			{
				DateTime curatedMapTimestamp = this.getCuratedMapTimestamp();
				return DateTime.UtcNow < curatedMapTimestamp;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x000D2ECB File Offset: 0x000D12CB
		public bool isAvailableToPlay
		{
			get
			{
				return this.CuratedMapMode != ECuratedMapMode.TIMED || this.isNowBeforeCuratedMapTimestamp;
			}
		}

		// Token: 0x040016AA RID: 5802
		public string[] Creators;

		// Token: 0x040016AB RID: 5803
		public string[] Collaborators;

		// Token: 0x040016AC RID: 5804
		public string[] Thanks;

		// Token: 0x040016AD RID: 5805
		public int Item;

		// Token: 0x040016AE RID: 5806
		public string Feedback;

		// Token: 0x040016AF RID: 5807
		public EMapStatus Status;

		// Token: 0x040016B0 RID: 5808
		public bool Load_From_Resources;

		// Token: 0x040016B1 RID: 5809
		public AssetReference<LevelAsset> Asset;

		// Token: 0x040016B2 RID: 5810
		public List<LevelTrainAssociation> Trains;

		// Token: 0x040016B3 RID: 5811
		public Dictionary<string, object> Mode_Config_Overrides;

		// Token: 0x040016B4 RID: 5812
		public bool Allow_Underwater_Features;

		// Token: 0x040016B5 RID: 5813
		public bool Terrain_Snow_Sparkle;

		// Token: 0x040016B6 RID: 5814
		public bool Use_Legacy_Clip_Borders;

		// Token: 0x040016B7 RID: 5815
		public bool Use_Legacy_Ground;

		// Token: 0x040016B8 RID: 5816
		public bool Use_Legacy_Water;

		// Token: 0x040016B9 RID: 5817
		public bool Use_Legacy_Objects;

		// Token: 0x040016BA RID: 5818
		public bool Use_Legacy_Snow_Height;

		// Token: 0x040016BB RID: 5819
		public bool Use_Legacy_Fog_Height;

		// Token: 0x040016BC RID: 5820
		public bool Use_Legacy_Oxygen_Height;

		// Token: 0x040016BD RID: 5821
		public bool Use_Rain_Volumes;

		// Token: 0x040016BE RID: 5822
		public bool Use_Snow_Volumes;

		// Token: 0x040016BF RID: 5823
		public bool Is_Aurora_Borealis_Visible;

		// Token: 0x040016C0 RID: 5824
		public bool Snow_Affects_Temperature;

		// Token: 0x040016C1 RID: 5825
		public bool Has_Atmosphere;

		// Token: 0x040016C2 RID: 5826
		public bool Has_Discord_Rich_Presence;

		// Token: 0x040016C3 RID: 5827
		public bool Can_Use_Bundles;

		// Token: 0x040016C4 RID: 5828
		public float Gravity;

		// Token: 0x040016C5 RID: 5829
		public float Blimp_Altitude;

		// Token: 0x040016C6 RID: 5830
		public ESingleplayerMapCategory Category;

		// Token: 0x040016C7 RID: 5831
		public bool Visible_In_Matchmaking;

		// Token: 0x040016C8 RID: 5832
		public ECuratedMapMode CuratedMapMode;

		// Token: 0x040016C9 RID: 5833
		public string CuratedMapTimestamp;

		// Token: 0x040016CA RID: 5834
		public bool Use_Arena_Compactor;

		// Token: 0x040016CB RID: 5835
		public List<ArenaLoadout> Arena_Loadouts;
	}
}
