using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000758 RID: 1880
	public class EditorPauseUI
	{
		// Token: 0x06003519 RID: 13593 RVA: 0x0015FDA8 File Offset: 0x0015E1A8
		public EditorPauseUI()
		{
			Local local = Localization.read("/Editor/EditorPause.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorPause/EditorPause.unity3d");
			EditorPauseUI.container = new Sleek();
			EditorPauseUI.container.positionOffset_X = 10;
			EditorPauseUI.container.positionOffset_Y = 10;
			EditorPauseUI.container.positionScale_X = 1f;
			EditorPauseUI.container.sizeOffset_X = -20;
			EditorPauseUI.container.sizeOffset_Y = -20;
			EditorPauseUI.container.sizeScale_X = 1f;
			EditorPauseUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorPauseUI.container);
			EditorPauseUI.active = false;
			EditorPauseUI.saveButton = new SleekButtonIcon((Texture2D)bundle.load("Save"));
			EditorPauseUI.saveButton.positionOffset_X = -100;
			EditorPauseUI.saveButton.positionOffset_Y = -115;
			EditorPauseUI.saveButton.positionScale_X = 0.5f;
			EditorPauseUI.saveButton.positionScale_Y = 0.5f;
			EditorPauseUI.saveButton.sizeOffset_X = 200;
			EditorPauseUI.saveButton.sizeOffset_Y = 30;
			EditorPauseUI.saveButton.text = local.format("Save_Button");
			EditorPauseUI.saveButton.tooltip = local.format("Save_Button_Tooltip");
			SleekButton sleekButton = EditorPauseUI.saveButton;
			if (EditorPauseUI.<>f__mg$cache0 == null)
			{
				EditorPauseUI.<>f__mg$cache0 = new ClickedButton(EditorPauseUI.onClickedSaveButton);
			}
			sleekButton.onClickedButton = EditorPauseUI.<>f__mg$cache0;
			EditorPauseUI.container.add(EditorPauseUI.saveButton);
			EditorPauseUI.mapButton = new SleekButtonIcon((Texture2D)bundle.load("Map"));
			EditorPauseUI.mapButton.positionOffset_X = -100;
			EditorPauseUI.mapButton.positionOffset_Y = -75;
			EditorPauseUI.mapButton.positionScale_X = 0.5f;
			EditorPauseUI.mapButton.positionScale_Y = 0.5f;
			EditorPauseUI.mapButton.sizeOffset_X = 200;
			EditorPauseUI.mapButton.sizeOffset_Y = 30;
			EditorPauseUI.mapButton.text = local.format("Map_Button");
			EditorPauseUI.mapButton.tooltip = local.format("Map_Button_Tooltip");
			SleekButton sleekButton2 = EditorPauseUI.mapButton;
			if (EditorPauseUI.<>f__mg$cache1 == null)
			{
				EditorPauseUI.<>f__mg$cache1 = new ClickedButton(EditorPauseUI.onClickedMapButton);
			}
			sleekButton2.onClickedButton = EditorPauseUI.<>f__mg$cache1;
			EditorPauseUI.container.add(EditorPauseUI.mapButton);
			EditorPauseUI.chartButton = new SleekButtonIcon((Texture2D)bundle.load("Chart"));
			EditorPauseUI.chartButton.positionOffset_X = -100;
			EditorPauseUI.chartButton.positionOffset_Y = -35;
			EditorPauseUI.chartButton.positionScale_X = 0.5f;
			EditorPauseUI.chartButton.positionScale_Y = 0.5f;
			EditorPauseUI.chartButton.sizeOffset_X = 200;
			EditorPauseUI.chartButton.sizeOffset_Y = 30;
			EditorPauseUI.chartButton.text = local.format("Chart_Button");
			EditorPauseUI.chartButton.tooltip = local.format("Chart_Button_Tooltip");
			SleekButton sleekButton3 = EditorPauseUI.chartButton;
			if (EditorPauseUI.<>f__mg$cache2 == null)
			{
				EditorPauseUI.<>f__mg$cache2 = new ClickedButton(EditorPauseUI.onClickedChartButton);
			}
			sleekButton3.onClickedButton = EditorPauseUI.<>f__mg$cache2;
			EditorPauseUI.container.add(EditorPauseUI.chartButton);
			EditorPauseUI.legacyIDField = new SleekUInt16Field();
			EditorPauseUI.legacyIDField.positionOffset_X = -100;
			EditorPauseUI.legacyIDField.positionOffset_Y = 5;
			EditorPauseUI.legacyIDField.positionScale_X = 0.5f;
			EditorPauseUI.legacyIDField.positionScale_Y = 0.5f;
			EditorPauseUI.legacyIDField.sizeOffset_X = 50;
			EditorPauseUI.legacyIDField.sizeOffset_Y = 30;
			EditorPauseUI.container.add(EditorPauseUI.legacyIDField);
			EditorPauseUI.legacyButton = new SleekButton();
			EditorPauseUI.legacyButton.positionOffset_X = -40;
			EditorPauseUI.legacyButton.positionOffset_Y = 5;
			EditorPauseUI.legacyButton.positionScale_X = 0.5f;
			EditorPauseUI.legacyButton.positionScale_Y = 0.5f;
			EditorPauseUI.legacyButton.sizeOffset_X = 140;
			EditorPauseUI.legacyButton.sizeOffset_Y = 30;
			EditorPauseUI.legacyButton.text = local.format("Legacy_Spawns");
			EditorPauseUI.legacyButton.tooltip = local.format("Legacy_Spawns_Tooltip");
			SleekButton sleekButton4 = EditorPauseUI.legacyButton;
			if (EditorPauseUI.<>f__mg$cache3 == null)
			{
				EditorPauseUI.<>f__mg$cache3 = new ClickedButton(EditorPauseUI.onClickedLegacyButton);
			}
			sleekButton4.onClickedButton = EditorPauseUI.<>f__mg$cache3;
			EditorPauseUI.container.add(EditorPauseUI.legacyButton);
			EditorPauseUI.proxyIDField = new SleekUInt16Field();
			EditorPauseUI.proxyIDField.positionOffset_X = -100;
			EditorPauseUI.proxyIDField.positionOffset_Y = 45;
			EditorPauseUI.proxyIDField.positionScale_X = 0.5f;
			EditorPauseUI.proxyIDField.positionScale_Y = 0.5f;
			EditorPauseUI.proxyIDField.sizeOffset_X = 50;
			EditorPauseUI.proxyIDField.sizeOffset_Y = 30;
			EditorPauseUI.container.add(EditorPauseUI.proxyIDField);
			EditorPauseUI.proxyButton = new SleekButton();
			EditorPauseUI.proxyButton.positionOffset_X = -40;
			EditorPauseUI.proxyButton.positionOffset_Y = 45;
			EditorPauseUI.proxyButton.positionScale_X = 0.5f;
			EditorPauseUI.proxyButton.positionScale_Y = 0.5f;
			EditorPauseUI.proxyButton.sizeOffset_X = 140;
			EditorPauseUI.proxyButton.sizeOffset_Y = 30;
			EditorPauseUI.proxyButton.text = local.format("Proxy_Spawns");
			EditorPauseUI.proxyButton.tooltip = local.format("Proxy_Spawns_Tooltip");
			SleekButton sleekButton5 = EditorPauseUI.proxyButton;
			if (EditorPauseUI.<>f__mg$cache4 == null)
			{
				EditorPauseUI.<>f__mg$cache4 = new ClickedButton(EditorPauseUI.onClickedProxyButton);
			}
			sleekButton5.onClickedButton = EditorPauseUI.<>f__mg$cache4;
			EditorPauseUI.container.add(EditorPauseUI.proxyButton);
			EditorPauseUI.exitButton = new SleekButtonIcon((Texture2D)bundle.load("Exit"));
			EditorPauseUI.exitButton.positionOffset_X = -100;
			EditorPauseUI.exitButton.positionOffset_Y = 85;
			EditorPauseUI.exitButton.positionScale_X = 0.5f;
			EditorPauseUI.exitButton.positionScale_Y = 0.5f;
			EditorPauseUI.exitButton.sizeOffset_X = 200;
			EditorPauseUI.exitButton.sizeOffset_Y = 30;
			EditorPauseUI.exitButton.text = local.format("Exit_Button");
			EditorPauseUI.exitButton.tooltip = local.format("Exit_Button_Tooltip");
			SleekButton sleekButton6 = EditorPauseUI.exitButton;
			if (EditorPauseUI.<>f__mg$cache5 == null)
			{
				EditorPauseUI.<>f__mg$cache5 = new ClickedButton(EditorPauseUI.onClickedExitButton);
			}
			sleekButton6.onClickedButton = EditorPauseUI.<>f__mg$cache5;
			EditorPauseUI.container.add(EditorPauseUI.exitButton);
			bundle.unload();
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x001603C1 File Offset: 0x0015E7C1
		public static void open()
		{
			if (EditorPauseUI.active)
			{
				return;
			}
			EditorPauseUI.active = true;
			EditorPauseUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x001603EE File Offset: 0x0015E7EE
		public static void close()
		{
			if (!EditorPauseUI.active)
			{
				return;
			}
			EditorPauseUI.active = false;
			EditorPauseUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0016041B File Offset: 0x0015E81B
		private static void onClickedSaveButton(SleekButton button)
		{
			Level.save();
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x00160422 File Offset: 0x0015E822
		private static void onClickedMapButton(SleekButton button)
		{
			Level.mapify();
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x00160429 File Offset: 0x0015E829
		private static void onClickedChartButton(SleekButton button)
		{
			Level.chartify();
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x00160430 File Offset: 0x0015E830
		private static void onClickedLegacyButton(SleekButton button)
		{
			ushort state = EditorPauseUI.legacyIDField.state;
			if (state == 0)
			{
				return;
			}
			SpawnTableTool.export(state, true);
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x00160458 File Offset: 0x0015E858
		private static void onClickedProxyButton(SleekButton button)
		{
			ushort state = EditorPauseUI.proxyIDField.state;
			if (state == 0)
			{
				return;
			}
			SpawnTableTool.export(state, false);
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0016047E File Offset: 0x0015E87E
		private static void onClickedExitButton(SleekButton button)
		{
			Level.exit();
		}

		// Token: 0x0400248E RID: 9358
		private static Sleek container;

		// Token: 0x0400248F RID: 9359
		public static bool active;

		// Token: 0x04002490 RID: 9360
		private static SleekButtonIcon saveButton;

		// Token: 0x04002491 RID: 9361
		private static SleekButtonIcon mapButton;

		// Token: 0x04002492 RID: 9362
		private static SleekButtonIcon exitButton;

		// Token: 0x04002493 RID: 9363
		private static SleekUInt16Field legacyIDField;

		// Token: 0x04002494 RID: 9364
		private static SleekButton legacyButton;

		// Token: 0x04002495 RID: 9365
		private static SleekUInt16Field proxyIDField;

		// Token: 0x04002496 RID: 9366
		private static SleekButton proxyButton;

		// Token: 0x04002497 RID: 9367
		private static SleekButtonIcon chartButton;

		// Token: 0x04002498 RID: 9368
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002499 RID: 9369
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x0400249A RID: 9370
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x0400249B RID: 9371
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x0400249C RID: 9372
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x0400249D RID: 9373
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;
	}
}
