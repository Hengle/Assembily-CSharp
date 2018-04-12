using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200074E RID: 1870
	public class EditorDashboardUI
	{
		// Token: 0x0600349A RID: 13466 RVA: 0x00159870 File Offset: 0x00157C70
		public EditorDashboardUI()
		{
			EditorDashboardUI.localization = Localization.read("/Editor/EditorDashboard.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorDashboard/EditorDashboard.unity3d");
			EditorDashboardUI.container = new Sleek();
			EditorDashboardUI.container.positionOffset_X = 10;
			EditorDashboardUI.container.positionOffset_Y = 10;
			EditorDashboardUI.container.sizeOffset_X = -20;
			EditorDashboardUI.container.sizeOffset_Y = -20;
			EditorDashboardUI.container.sizeScale_X = 1f;
			EditorDashboardUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorDashboardUI.container);
			EditorDashboardUI.terrainButton = new SleekButtonIcon((Texture2D)bundle.load("Terrain"));
			EditorDashboardUI.terrainButton.sizeOffset_X = -5;
			EditorDashboardUI.terrainButton.sizeOffset_Y = 30;
			EditorDashboardUI.terrainButton.sizeScale_X = 0.25f;
			EditorDashboardUI.terrainButton.text = EditorDashboardUI.localization.format("TerrainButtonText");
			EditorDashboardUI.terrainButton.tooltip = EditorDashboardUI.localization.format("TerrainButtonTooltip");
			SleekButton sleekButton = EditorDashboardUI.terrainButton;
			if (EditorDashboardUI.<>f__mg$cache0 == null)
			{
				EditorDashboardUI.<>f__mg$cache0 = new ClickedButton(EditorDashboardUI.onClickedTerrainButton);
			}
			sleekButton.onClickedButton = EditorDashboardUI.<>f__mg$cache0;
			EditorDashboardUI.container.add(EditorDashboardUI.terrainButton);
			EditorDashboardUI.environmentButton = new SleekButtonIcon((Texture2D)bundle.load("Environment"));
			EditorDashboardUI.environmentButton.positionOffset_X = 5;
			EditorDashboardUI.environmentButton.positionScale_X = 0.25f;
			EditorDashboardUI.environmentButton.sizeOffset_X = -10;
			EditorDashboardUI.environmentButton.sizeOffset_Y = 30;
			EditorDashboardUI.environmentButton.sizeScale_X = 0.25f;
			EditorDashboardUI.environmentButton.text = EditorDashboardUI.localization.format("EnvironmentButtonText");
			EditorDashboardUI.environmentButton.tooltip = EditorDashboardUI.localization.format("EnvironmentButtonTooltip");
			SleekButton sleekButton2 = EditorDashboardUI.environmentButton;
			if (EditorDashboardUI.<>f__mg$cache1 == null)
			{
				EditorDashboardUI.<>f__mg$cache1 = new ClickedButton(EditorDashboardUI.onClickedEnvironmentButton);
			}
			sleekButton2.onClickedButton = EditorDashboardUI.<>f__mg$cache1;
			EditorDashboardUI.container.add(EditorDashboardUI.environmentButton);
			EditorDashboardUI.spawnsButton = new SleekButtonIcon((Texture2D)bundle.load("Spawns"));
			EditorDashboardUI.spawnsButton.positionOffset_X = 5;
			EditorDashboardUI.spawnsButton.positionScale_X = 0.5f;
			EditorDashboardUI.spawnsButton.sizeOffset_X = -10;
			EditorDashboardUI.spawnsButton.sizeOffset_Y = 30;
			EditorDashboardUI.spawnsButton.sizeScale_X = 0.25f;
			EditorDashboardUI.spawnsButton.text = EditorDashboardUI.localization.format("SpawnsButtonText");
			EditorDashboardUI.spawnsButton.tooltip = EditorDashboardUI.localization.format("SpawnsButtonTooltip");
			SleekButton sleekButton3 = EditorDashboardUI.spawnsButton;
			if (EditorDashboardUI.<>f__mg$cache2 == null)
			{
				EditorDashboardUI.<>f__mg$cache2 = new ClickedButton(EditorDashboardUI.onClickedSpawnsButton);
			}
			sleekButton3.onClickedButton = EditorDashboardUI.<>f__mg$cache2;
			EditorDashboardUI.container.add(EditorDashboardUI.spawnsButton);
			EditorDashboardUI.levelButton = new SleekButtonIcon((Texture2D)bundle.load("Level"));
			EditorDashboardUI.levelButton.positionOffset_X = 5;
			EditorDashboardUI.levelButton.positionScale_X = 0.75f;
			EditorDashboardUI.levelButton.sizeOffset_X = -5;
			EditorDashboardUI.levelButton.sizeOffset_Y = 30;
			EditorDashboardUI.levelButton.sizeScale_X = 0.25f;
			EditorDashboardUI.levelButton.text = EditorDashboardUI.localization.format("LevelButtonText");
			EditorDashboardUI.levelButton.tooltip = EditorDashboardUI.localization.format("LevelButtonTooltip");
			SleekButton sleekButton4 = EditorDashboardUI.levelButton;
			if (EditorDashboardUI.<>f__mg$cache3 == null)
			{
				EditorDashboardUI.<>f__mg$cache3 = new ClickedButton(EditorDashboardUI.onClickedLevelButton);
			}
			sleekButton4.onClickedButton = EditorDashboardUI.<>f__mg$cache3;
			EditorDashboardUI.container.add(EditorDashboardUI.levelButton);
			bundle.unload();
			new EditorPauseUI();
			new EditorTerrainUI();
			new EditorEnvironmentUI();
			new EditorSpawnsUI();
			new EditorLevelUI();
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x00159C1A File Offset: 0x0015801A
		private static void onClickedTerrainButton(SleekButton button)
		{
			EditorTerrainUI.open();
			EditorEnvironmentUI.close();
			EditorSpawnsUI.close();
			EditorLevelUI.close();
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x00159C30 File Offset: 0x00158030
		private static void onClickedEnvironmentButton(SleekButton button)
		{
			EditorTerrainUI.close();
			EditorEnvironmentUI.open();
			EditorSpawnsUI.close();
			EditorLevelUI.close();
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x00159C46 File Offset: 0x00158046
		private static void onClickedSpawnsButton(SleekButton button)
		{
			EditorTerrainUI.close();
			EditorEnvironmentUI.close();
			EditorSpawnsUI.open();
			EditorLevelUI.close();
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x00159C5C File Offset: 0x0015805C
		private static void onClickedLevelButton(SleekButton button)
		{
			EditorTerrainUI.close();
			EditorEnvironmentUI.close();
			EditorSpawnsUI.close();
			EditorLevelUI.open();
		}

		// Token: 0x040023BE RID: 9150
		private static Sleek container;

		// Token: 0x040023BF RID: 9151
		public static Local localization;

		// Token: 0x040023C0 RID: 9152
		private static SleekButtonIcon terrainButton;

		// Token: 0x040023C1 RID: 9153
		private static SleekButtonIcon environmentButton;

		// Token: 0x040023C2 RID: 9154
		private static SleekButtonIcon spawnsButton;

		// Token: 0x040023C3 RID: 9155
		private static SleekButtonIcon levelButton;

		// Token: 0x040023C4 RID: 9156
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040023C5 RID: 9157
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040023C6 RID: 9158
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040023C7 RID: 9159
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
