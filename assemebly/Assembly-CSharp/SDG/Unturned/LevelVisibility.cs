using System;

namespace SDG.Unturned
{
	// Token: 0x0200055D RID: 1373
	public class LevelVisibility
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000DE219 File Offset: 0x000DC619
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x000DE220 File Offset: 0x000DC620
		public static bool roadsVisible
		{
			get
			{
				return LevelVisibility._roadsVisible;
			}
			set
			{
				LevelVisibility._roadsVisible = value;
				LevelRoads.setEnabled(LevelVisibility.roadsVisible);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000DE232 File Offset: 0x000DC632
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x000DE239 File Offset: 0x000DC639
		public static bool navigationVisible
		{
			get
			{
				return LevelVisibility._navigationVisible;
			}
			set
			{
				LevelVisibility._navigationVisible = value;
				LevelNavigation.setEnabled(LevelVisibility.navigationVisible);
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000DE24B File Offset: 0x000DC64B
		// (set) Token: 0x060025E5 RID: 9701 RVA: 0x000DE252 File Offset: 0x000DC652
		public static bool nodesVisible
		{
			get
			{
				return LevelVisibility._nodesVisible;
			}
			set
			{
				LevelVisibility._nodesVisible = value;
				LevelNodes.setEnabled(LevelVisibility.nodesVisible);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000DE264 File Offset: 0x000DC664
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000DE26B File Offset: 0x000DC66B
		public static bool itemsVisible
		{
			get
			{
				return LevelVisibility._itemsVisible;
			}
			set
			{
				LevelVisibility._itemsVisible = value;
				LevelItems.setEnabled(LevelVisibility.itemsVisible);
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000DE27D File Offset: 0x000DC67D
		// (set) Token: 0x060025E9 RID: 9705 RVA: 0x000DE284 File Offset: 0x000DC684
		public static bool playersVisible
		{
			get
			{
				return LevelVisibility._playersVisible;
			}
			set
			{
				LevelVisibility._playersVisible = value;
				LevelPlayers.setEnabled(LevelVisibility.playersVisible);
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x000DE296 File Offset: 0x000DC696
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x000DE29D File Offset: 0x000DC69D
		public static bool zombiesVisible
		{
			get
			{
				return LevelVisibility._zombiesVisible;
			}
			set
			{
				LevelVisibility._zombiesVisible = value;
				LevelZombies.setEnabled(LevelVisibility.zombiesVisible);
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000DE2AF File Offset: 0x000DC6AF
		// (set) Token: 0x060025ED RID: 9709 RVA: 0x000DE2B6 File Offset: 0x000DC6B6
		public static bool vehiclesVisible
		{
			get
			{
				return LevelVisibility._vehiclesVisible;
			}
			set
			{
				LevelVisibility._vehiclesVisible = value;
				LevelVehicles.setEnabled(LevelVisibility.vehiclesVisible);
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000DE2C8 File Offset: 0x000DC6C8
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x000DE2CF File Offset: 0x000DC6CF
		public static bool borderVisible
		{
			get
			{
				return LevelVisibility._borderVisible;
			}
			set
			{
				LevelVisibility._borderVisible = value;
				Level.setEnabled(LevelVisibility.borderVisible);
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000DE2E1 File Offset: 0x000DC6E1
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000DE2E8 File Offset: 0x000DC6E8
		public static bool animalsVisible
		{
			get
			{
				return LevelVisibility._animalsVisible;
			}
			set
			{
				LevelVisibility._animalsVisible = value;
				LevelAnimals.setEnabled(LevelVisibility.animalsVisible);
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000DE2FC File Offset: 0x000DC6FC
		public static void load()
		{
			if (Level.isVR)
			{
				LevelVisibility.roadsVisible = false;
				LevelVisibility._navigationVisible = false;
				LevelVisibility._nodesVisible = false;
				LevelVisibility._itemsVisible = false;
				LevelVisibility.playersVisible = false;
				LevelVisibility._zombiesVisible = false;
				LevelVisibility._vehiclesVisible = false;
				LevelVisibility.borderVisible = false;
				LevelVisibility._animalsVisible = false;
				return;
			}
			if (Level.isEditor)
			{
				if (ReadWrite.fileExists(Level.info.path + "/Level/Visibility.dat", false, false))
				{
					River river = new River(Level.info.path + "/Level/Visibility.dat", false);
					byte b = river.readByte();
					if (b > 0)
					{
						LevelVisibility.roadsVisible = river.readBoolean();
						LevelVisibility.navigationVisible = river.readBoolean();
						LevelVisibility.nodesVisible = river.readBoolean();
						LevelVisibility.itemsVisible = river.readBoolean();
						LevelVisibility.playersVisible = river.readBoolean();
						LevelVisibility.zombiesVisible = river.readBoolean();
						LevelVisibility.vehiclesVisible = river.readBoolean();
						LevelVisibility.borderVisible = river.readBoolean();
						if (b > 1)
						{
							LevelVisibility.animalsVisible = river.readBoolean();
						}
						else
						{
							LevelVisibility._animalsVisible = true;
						}
						river.closeRiver();
					}
				}
				else
				{
					LevelVisibility._roadsVisible = true;
					LevelVisibility._navigationVisible = true;
					LevelVisibility._nodesVisible = true;
					LevelVisibility._itemsVisible = true;
					LevelVisibility._playersVisible = true;
					LevelVisibility._zombiesVisible = true;
					LevelVisibility._vehiclesVisible = true;
					LevelVisibility._borderVisible = true;
					LevelVisibility._animalsVisible = true;
				}
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000DE454 File Offset: 0x000DC854
		public static void save()
		{
			River river = new River(Level.info.path + "/Level/Visibility.dat", false);
			river.writeByte(LevelVisibility.SAVEDATA_VERSION);
			river.writeBoolean(LevelVisibility.roadsVisible);
			river.writeBoolean(LevelVisibility.navigationVisible);
			river.writeBoolean(LevelVisibility.nodesVisible);
			river.writeBoolean(LevelVisibility.itemsVisible);
			river.writeBoolean(LevelVisibility.playersVisible);
			river.writeBoolean(LevelVisibility.zombiesVisible);
			river.writeBoolean(LevelVisibility.vehiclesVisible);
			river.writeBoolean(LevelVisibility.borderVisible);
			river.writeBoolean(LevelVisibility.animalsVisible);
			river.closeRiver();
		}

		// Token: 0x04001797 RID: 6039
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x04001798 RID: 6040
		public static readonly byte OBJECT_REGIONS = 4;

		// Token: 0x04001799 RID: 6041
		private static bool _roadsVisible;

		// Token: 0x0400179A RID: 6042
		private static bool _navigationVisible;

		// Token: 0x0400179B RID: 6043
		private static bool _nodesVisible;

		// Token: 0x0400179C RID: 6044
		private static bool _itemsVisible;

		// Token: 0x0400179D RID: 6045
		private static bool _playersVisible;

		// Token: 0x0400179E RID: 6046
		private static bool _zombiesVisible;

		// Token: 0x0400179F RID: 6047
		private static bool _vehiclesVisible;

		// Token: 0x040017A0 RID: 6048
		private static bool _borderVisible;

		// Token: 0x040017A1 RID: 6049
		private static bool _animalsVisible;
	}
}
