using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200075C RID: 1884
	public class EditorSpawnsUI
	{
		// Token: 0x06003556 RID: 13654 RVA: 0x00163B2C File Offset: 0x00161F2C
		public EditorSpawnsUI()
		{
			Local local = Localization.read("/Editor/EditorSpawns.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorSpawns/EditorSpawns.unity3d");
			EditorSpawnsUI.container = new Sleek();
			EditorSpawnsUI.container.positionOffset_X = 10;
			EditorSpawnsUI.container.positionOffset_Y = 10;
			EditorSpawnsUI.container.positionScale_X = 1f;
			EditorSpawnsUI.container.sizeOffset_X = -20;
			EditorSpawnsUI.container.sizeOffset_Y = -20;
			EditorSpawnsUI.container.sizeScale_X = 1f;
			EditorSpawnsUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorSpawnsUI.container);
			EditorSpawnsUI.active = false;
			EditorSpawnsUI.animalsButton = new SleekButtonIcon((Texture2D)bundle.load("Animals"));
			EditorSpawnsUI.animalsButton.positionOffset_Y = 40;
			EditorSpawnsUI.animalsButton.sizeOffset_X = -5;
			EditorSpawnsUI.animalsButton.sizeOffset_Y = 30;
			EditorSpawnsUI.animalsButton.sizeScale_X = 0.25f;
			EditorSpawnsUI.animalsButton.text = local.format("AnimalsButtonText");
			EditorSpawnsUI.animalsButton.tooltip = local.format("AnimalsButtonTooltip");
			SleekButton sleekButton = EditorSpawnsUI.animalsButton;
			if (EditorSpawnsUI.<>f__mg$cache0 == null)
			{
				EditorSpawnsUI.<>f__mg$cache0 = new ClickedButton(EditorSpawnsUI.onClickedAnimalsButton);
			}
			sleekButton.onClickedButton = EditorSpawnsUI.<>f__mg$cache0;
			EditorSpawnsUI.container.add(EditorSpawnsUI.animalsButton);
			EditorSpawnsUI.itemsButton = new SleekButtonIcon((Texture2D)bundle.load("Items"));
			EditorSpawnsUI.itemsButton.positionOffset_X = 5;
			EditorSpawnsUI.itemsButton.positionOffset_Y = 40;
			EditorSpawnsUI.itemsButton.positionScale_X = 0.25f;
			EditorSpawnsUI.itemsButton.sizeOffset_X = -10;
			EditorSpawnsUI.itemsButton.sizeOffset_Y = 30;
			EditorSpawnsUI.itemsButton.sizeScale_X = 0.25f;
			EditorSpawnsUI.itemsButton.text = local.format("ItemsButtonText");
			EditorSpawnsUI.itemsButton.tooltip = local.format("ItemsButtonTooltip");
			SleekButton sleekButton2 = EditorSpawnsUI.itemsButton;
			if (EditorSpawnsUI.<>f__mg$cache1 == null)
			{
				EditorSpawnsUI.<>f__mg$cache1 = new ClickedButton(EditorSpawnsUI.onClickItemsButton);
			}
			sleekButton2.onClickedButton = EditorSpawnsUI.<>f__mg$cache1;
			EditorSpawnsUI.container.add(EditorSpawnsUI.itemsButton);
			EditorSpawnsUI.zombiesButton = new SleekButtonIcon((Texture2D)bundle.load("Zombies"));
			EditorSpawnsUI.zombiesButton.positionOffset_X = 5;
			EditorSpawnsUI.zombiesButton.positionOffset_Y = 40;
			EditorSpawnsUI.zombiesButton.positionScale_X = 0.5f;
			EditorSpawnsUI.zombiesButton.sizeOffset_X = -10;
			EditorSpawnsUI.zombiesButton.sizeOffset_Y = 30;
			EditorSpawnsUI.zombiesButton.sizeScale_X = 0.25f;
			EditorSpawnsUI.zombiesButton.text = local.format("ZombiesButtonText");
			EditorSpawnsUI.zombiesButton.tooltip = local.format("ZombiesButtonTooltip");
			SleekButton sleekButton3 = EditorSpawnsUI.zombiesButton;
			if (EditorSpawnsUI.<>f__mg$cache2 == null)
			{
				EditorSpawnsUI.<>f__mg$cache2 = new ClickedButton(EditorSpawnsUI.onClickedZombiesButton);
			}
			sleekButton3.onClickedButton = EditorSpawnsUI.<>f__mg$cache2;
			EditorSpawnsUI.container.add(EditorSpawnsUI.zombiesButton);
			EditorSpawnsUI.vehiclesButton = new SleekButtonIcon((Texture2D)bundle.load("Vehicles"));
			EditorSpawnsUI.vehiclesButton.positionOffset_X = 5;
			EditorSpawnsUI.vehiclesButton.positionOffset_Y = 40;
			EditorSpawnsUI.vehiclesButton.positionScale_X = 0.75f;
			EditorSpawnsUI.vehiclesButton.sizeOffset_X = -5;
			EditorSpawnsUI.vehiclesButton.sizeOffset_Y = 30;
			EditorSpawnsUI.vehiclesButton.sizeScale_X = 0.25f;
			EditorSpawnsUI.vehiclesButton.text = local.format("VehiclesButtonText");
			EditorSpawnsUI.vehiclesButton.tooltip = local.format("VehiclesButtonTooltip");
			SleekButton sleekButton4 = EditorSpawnsUI.vehiclesButton;
			if (EditorSpawnsUI.<>f__mg$cache3 == null)
			{
				EditorSpawnsUI.<>f__mg$cache3 = new ClickedButton(EditorSpawnsUI.onClickedVehiclesButton);
			}
			sleekButton4.onClickedButton = EditorSpawnsUI.<>f__mg$cache3;
			EditorSpawnsUI.container.add(EditorSpawnsUI.vehiclesButton);
			bundle.unload();
			new EditorSpawnsAnimalsUI();
			new EditorSpawnsItemsUI();
			new EditorSpawnsZombiesUI();
			new EditorSpawnsVehiclesUI();
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x00163EF1 File Offset: 0x001622F1
		public static void open()
		{
			if (EditorSpawnsUI.active)
			{
				return;
			}
			EditorSpawnsUI.active = true;
			EditorSpawnsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x00163F20 File Offset: 0x00162320
		public static void close()
		{
			if (!EditorSpawnsUI.active)
			{
				return;
			}
			EditorSpawnsUI.active = false;
			EditorSpawnsItemsUI.close();
			EditorSpawnsAnimalsUI.close();
			EditorSpawnsZombiesUI.close();
			EditorSpawnsVehiclesUI.close();
			EditorSpawnsUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x00163F6C File Offset: 0x0016236C
		private static void onClickedAnimalsButton(SleekButton button)
		{
			EditorSpawnsItemsUI.close();
			EditorSpawnsZombiesUI.close();
			EditorSpawnsVehiclesUI.close();
			EditorSpawnsAnimalsUI.open();
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x00163F82 File Offset: 0x00162382
		private static void onClickItemsButton(SleekButton button)
		{
			EditorSpawnsAnimalsUI.close();
			EditorSpawnsZombiesUI.close();
			EditorSpawnsVehiclesUI.close();
			EditorSpawnsItemsUI.open();
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x00163F98 File Offset: 0x00162398
		private static void onClickedZombiesButton(SleekButton button)
		{
			EditorSpawnsAnimalsUI.close();
			EditorSpawnsItemsUI.close();
			EditorSpawnsVehiclesUI.close();
			EditorSpawnsZombiesUI.open();
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x00163FAE File Offset: 0x001623AE
		private static void onClickedVehiclesButton(SleekButton button)
		{
			EditorSpawnsAnimalsUI.close();
			EditorSpawnsItemsUI.close();
			EditorSpawnsZombiesUI.close();
			EditorSpawnsVehiclesUI.open();
		}

		// Token: 0x040024FC RID: 9468
		private static Sleek container;

		// Token: 0x040024FD RID: 9469
		public static bool active;

		// Token: 0x040024FE RID: 9470
		private static SleekButtonIcon animalsButton;

		// Token: 0x040024FF RID: 9471
		private static SleekButtonIcon itemsButton;

		// Token: 0x04002500 RID: 9472
		private static SleekButtonIcon zombiesButton;

		// Token: 0x04002501 RID: 9473
		private static SleekButtonIcon vehiclesButton;

		// Token: 0x04002502 RID: 9474
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002503 RID: 9475
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x04002504 RID: 9476
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x04002505 RID: 9477
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
