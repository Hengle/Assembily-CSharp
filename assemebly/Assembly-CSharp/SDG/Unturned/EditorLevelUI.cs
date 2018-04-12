using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000756 RID: 1878
	public class EditorLevelUI
	{
		// Token: 0x06003503 RID: 13571 RVA: 0x0015ECC8 File Offset: 0x0015D0C8
		public EditorLevelUI()
		{
			Local local = Localization.read("/Editor/EditorLevel.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorLevel/EditorLevel.unity3d");
			EditorLevelUI.container = new Sleek();
			EditorLevelUI.container.positionOffset_X = 10;
			EditorLevelUI.container.positionOffset_Y = 10;
			EditorLevelUI.container.positionScale_X = 1f;
			EditorLevelUI.container.sizeOffset_X = -20;
			EditorLevelUI.container.sizeOffset_Y = -20;
			EditorLevelUI.container.sizeScale_X = 1f;
			EditorLevelUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorLevelUI.container);
			EditorLevelUI.active = false;
			EditorLevelUI.objectsButton = new SleekButtonIcon((Texture2D)bundle.load("Objects"));
			EditorLevelUI.objectsButton.positionOffset_Y = 40;
			EditorLevelUI.objectsButton.sizeOffset_X = -5;
			EditorLevelUI.objectsButton.sizeOffset_Y = 30;
			EditorLevelUI.objectsButton.sizeScale_X = 0.25f;
			EditorLevelUI.objectsButton.text = local.format("ObjectsButtonText");
			EditorLevelUI.objectsButton.tooltip = local.format("ObjectsButtonTooltip");
			SleekButton sleekButton = EditorLevelUI.objectsButton;
			if (EditorLevelUI.<>f__mg$cache0 == null)
			{
				EditorLevelUI.<>f__mg$cache0 = new ClickedButton(EditorLevelUI.onClickedObjectsButton);
			}
			sleekButton.onClickedButton = EditorLevelUI.<>f__mg$cache0;
			EditorLevelUI.container.add(EditorLevelUI.objectsButton);
			EditorLevelUI.visibilityButton = new SleekButtonIcon((Texture2D)bundle.load("Visibility"));
			EditorLevelUI.visibilityButton.positionOffset_X = 5;
			EditorLevelUI.visibilityButton.positionOffset_Y = 40;
			EditorLevelUI.visibilityButton.positionScale_X = 0.25f;
			EditorLevelUI.visibilityButton.sizeOffset_X = -10;
			EditorLevelUI.visibilityButton.sizeOffset_Y = 30;
			EditorLevelUI.visibilityButton.sizeScale_X = 0.25f;
			EditorLevelUI.visibilityButton.text = local.format("VisibilityButtonText");
			EditorLevelUI.visibilityButton.tooltip = local.format("VisibilityButtonTooltip");
			SleekButton sleekButton2 = EditorLevelUI.visibilityButton;
			if (EditorLevelUI.<>f__mg$cache1 == null)
			{
				EditorLevelUI.<>f__mg$cache1 = new ClickedButton(EditorLevelUI.onClickedVisibilityButton);
			}
			sleekButton2.onClickedButton = EditorLevelUI.<>f__mg$cache1;
			EditorLevelUI.container.add(EditorLevelUI.visibilityButton);
			EditorLevelUI.playersButton = new SleekButtonIcon((Texture2D)bundle.load("Players"));
			EditorLevelUI.playersButton.positionOffset_Y = 40;
			EditorLevelUI.playersButton.positionOffset_X = 5;
			EditorLevelUI.playersButton.positionScale_X = 0.5f;
			EditorLevelUI.playersButton.sizeOffset_X = -10;
			EditorLevelUI.playersButton.sizeOffset_Y = 30;
			EditorLevelUI.playersButton.sizeScale_X = 0.25f;
			EditorLevelUI.playersButton.text = local.format("PlayersButtonText");
			EditorLevelUI.playersButton.tooltip = local.format("PlayersButtonTooltip");
			SleekButton sleekButton3 = EditorLevelUI.playersButton;
			if (EditorLevelUI.<>f__mg$cache2 == null)
			{
				EditorLevelUI.<>f__mg$cache2 = new ClickedButton(EditorLevelUI.onClickedPlayersButton);
			}
			sleekButton3.onClickedButton = EditorLevelUI.<>f__mg$cache2;
			EditorLevelUI.container.add(EditorLevelUI.playersButton);
			bundle.unload();
			new EditorLevelObjectsUI();
			new EditorLevelVisibilityUI();
			new EditorLevelPlayersUI();
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0015EFC0 File Offset: 0x0015D3C0
		public static void open()
		{
			if (EditorLevelUI.active)
			{
				return;
			}
			EditorLevelUI.active = true;
			EditorLevelUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x0015EFED File Offset: 0x0015D3ED
		public static void close()
		{
			if (!EditorLevelUI.active)
			{
				return;
			}
			EditorLevelUI.active = false;
			EditorLevelObjectsUI.close();
			EditorLevelVisibilityUI.close();
			EditorLevelPlayersUI.close();
			EditorLevelUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x0015F029 File Offset: 0x0015D429
		private static void onClickedObjectsButton(SleekButton button)
		{
			EditorLevelObjectsUI.open();
			EditorLevelVisibilityUI.close();
			EditorLevelPlayersUI.close();
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0015F03A File Offset: 0x0015D43A
		private static void onClickedVisibilityButton(SleekButton button)
		{
			EditorLevelObjectsUI.close();
			EditorLevelVisibilityUI.open();
			EditorLevelPlayersUI.close();
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0015F04B File Offset: 0x0015D44B
		private static void onClickedPlayersButton(SleekButton button)
		{
			EditorLevelObjectsUI.close();
			EditorLevelVisibilityUI.close();
			EditorLevelPlayersUI.open();
		}

		// Token: 0x0400246D RID: 9325
		private static Sleek container;

		// Token: 0x0400246E RID: 9326
		public static bool active;

		// Token: 0x0400246F RID: 9327
		private static SleekButtonIcon objectsButton;

		// Token: 0x04002470 RID: 9328
		private static SleekButtonIcon visibilityButton;

		// Token: 0x04002471 RID: 9329
		private static SleekButtonIcon playersButton;

		// Token: 0x04002472 RID: 9330
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002473 RID: 9331
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002474 RID: 9332
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;
	}
}
