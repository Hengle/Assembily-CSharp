using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000763 RID: 1891
	public class EditorTerrainUI
	{
		// Token: 0x060035D9 RID: 13785 RVA: 0x0016AC40 File Offset: 0x00169040
		public EditorTerrainUI()
		{
			Local local = Localization.read("/Editor/EditorTerrain.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorTerrain/EditorTerrain.unity3d");
			EditorTerrainUI.container = new Sleek();
			EditorTerrainUI.container.positionOffset_X = 10;
			EditorTerrainUI.container.positionOffset_Y = 10;
			EditorTerrainUI.container.positionScale_X = 1f;
			EditorTerrainUI.container.sizeOffset_X = -20;
			EditorTerrainUI.container.sizeOffset_Y = -20;
			EditorTerrainUI.container.sizeScale_X = 1f;
			EditorTerrainUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorTerrainUI.container);
			EditorTerrainUI.active = false;
			EditorTerrainUI.heightButton = new SleekButtonIcon((Texture2D)bundle.load("Height"));
			EditorTerrainUI.heightButton.positionOffset_Y = 40;
			EditorTerrainUI.heightButton.sizeOffset_X = -5;
			EditorTerrainUI.heightButton.sizeOffset_Y = 30;
			EditorTerrainUI.heightButton.sizeScale_X = 0.25f;
			EditorTerrainUI.heightButton.text = local.format("HeightButtonText");
			EditorTerrainUI.heightButton.tooltip = local.format("HeightButtonTooltip");
			SleekButton sleekButton = EditorTerrainUI.heightButton;
			if (EditorTerrainUI.<>f__mg$cache0 == null)
			{
				EditorTerrainUI.<>f__mg$cache0 = new ClickedButton(EditorTerrainUI.onClickedHeightButton);
			}
			sleekButton.onClickedButton = EditorTerrainUI.<>f__mg$cache0;
			EditorTerrainUI.container.add(EditorTerrainUI.heightButton);
			EditorTerrainUI.materialsButton = new SleekButtonIcon((Texture2D)bundle.load("Materials"));
			EditorTerrainUI.materialsButton.positionOffset_X = 5;
			EditorTerrainUI.materialsButton.positionOffset_Y = 40;
			EditorTerrainUI.materialsButton.positionScale_X = 0.25f;
			EditorTerrainUI.materialsButton.sizeOffset_X = -10;
			EditorTerrainUI.materialsButton.sizeOffset_Y = 30;
			EditorTerrainUI.materialsButton.sizeScale_X = 0.25f;
			EditorTerrainUI.materialsButton.text = local.format("MaterialsButtonText");
			EditorTerrainUI.materialsButton.tooltip = local.format("MaterialsButtonTooltip");
			SleekButton sleekButton2 = EditorTerrainUI.materialsButton;
			if (EditorTerrainUI.<>f__mg$cache1 == null)
			{
				EditorTerrainUI.<>f__mg$cache1 = new ClickedButton(EditorTerrainUI.onClickedMaterialsButton);
			}
			sleekButton2.onClickedButton = EditorTerrainUI.<>f__mg$cache1;
			EditorTerrainUI.container.add(EditorTerrainUI.materialsButton);
			EditorTerrainUI.detailsButton = new SleekButtonIcon((Texture2D)bundle.load("Details"));
			EditorTerrainUI.detailsButton.positionOffset_X = 5;
			EditorTerrainUI.detailsButton.positionOffset_Y = 40;
			EditorTerrainUI.detailsButton.positionScale_X = 0.5f;
			EditorTerrainUI.detailsButton.sizeOffset_X = -10;
			EditorTerrainUI.detailsButton.sizeOffset_Y = 30;
			EditorTerrainUI.detailsButton.sizeScale_X = 0.25f;
			EditorTerrainUI.detailsButton.text = local.format("DetailsButtonText");
			EditorTerrainUI.detailsButton.tooltip = local.format("DetailsButtonTooltip");
			SleekButton sleekButton3 = EditorTerrainUI.detailsButton;
			if (EditorTerrainUI.<>f__mg$cache2 == null)
			{
				EditorTerrainUI.<>f__mg$cache2 = new ClickedButton(EditorTerrainUI.onClickedDetailsButton);
			}
			sleekButton3.onClickedButton = EditorTerrainUI.<>f__mg$cache2;
			EditorTerrainUI.container.add(EditorTerrainUI.detailsButton);
			EditorTerrainUI.resourcesButton = new SleekButtonIcon((Texture2D)bundle.load("Resources"));
			EditorTerrainUI.resourcesButton.positionOffset_X = 5;
			EditorTerrainUI.resourcesButton.positionOffset_Y = 40;
			EditorTerrainUI.resourcesButton.positionScale_X = 0.75f;
			EditorTerrainUI.resourcesButton.sizeOffset_X = -5;
			EditorTerrainUI.resourcesButton.sizeOffset_Y = 30;
			EditorTerrainUI.resourcesButton.sizeScale_X = 0.25f;
			EditorTerrainUI.resourcesButton.text = local.format("ResourcesButtonText");
			EditorTerrainUI.resourcesButton.tooltip = local.format("ResourcesButtonTooltip");
			SleekButton sleekButton4 = EditorTerrainUI.resourcesButton;
			if (EditorTerrainUI.<>f__mg$cache3 == null)
			{
				EditorTerrainUI.<>f__mg$cache3 = new ClickedButton(EditorTerrainUI.onClickedResourcesButton);
			}
			sleekButton4.onClickedButton = EditorTerrainUI.<>f__mg$cache3;
			EditorTerrainUI.container.add(EditorTerrainUI.resourcesButton);
			bundle.unload();
			new EditorTerrainHeightUI();
			new EditorTerrainMaterialsUI();
			new EditorTerrainDetailsUI();
			new EditorTerrainResourcesUI();
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x0016B005 File Offset: 0x00169405
		public static void open()
		{
			if (EditorTerrainUI.active)
			{
				return;
			}
			EditorTerrainUI.active = true;
			EditorTerrainUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x0016B034 File Offset: 0x00169434
		public static void close()
		{
			if (!EditorTerrainUI.active)
			{
				return;
			}
			EditorTerrainUI.active = false;
			EditorTerrainHeightUI.close();
			EditorTerrainMaterialsUI.close();
			EditorTerrainDetailsUI.close();
			EditorTerrainResourcesUI.close();
			EditorTerrainUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x0016B080 File Offset: 0x00169480
		private static void onClickedHeightButton(SleekButton button)
		{
			EditorTerrainMaterialsUI.close();
			EditorTerrainDetailsUI.close();
			EditorTerrainResourcesUI.close();
			EditorTerrainHeightUI.open();
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x0016B096 File Offset: 0x00169496
		private static void onClickedMaterialsButton(SleekButton button)
		{
			EditorTerrainHeightUI.close();
			EditorTerrainDetailsUI.close();
			EditorTerrainResourcesUI.close();
			EditorTerrainMaterialsUI.open();
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x0016B0AC File Offset: 0x001694AC
		private static void onClickedDetailsButton(SleekButton button)
		{
			EditorTerrainHeightUI.close();
			EditorTerrainMaterialsUI.close();
			EditorTerrainResourcesUI.close();
			EditorTerrainDetailsUI.open();
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x0016B0C2 File Offset: 0x001694C2
		private static void onClickedResourcesButton(SleekButton button)
		{
			EditorTerrainHeightUI.close();
			EditorTerrainMaterialsUI.close();
			EditorTerrainDetailsUI.close();
			EditorTerrainResourcesUI.open();
		}

		// Token: 0x040025E8 RID: 9704
		private static Sleek container;

		// Token: 0x040025E9 RID: 9705
		public static bool active;

		// Token: 0x040025EA RID: 9706
		private static SleekButtonIcon heightButton;

		// Token: 0x040025EB RID: 9707
		private static SleekButtonIcon materialsButton;

		// Token: 0x040025EC RID: 9708
		private static SleekButtonIcon detailsButton;

		// Token: 0x040025ED RID: 9709
		private static SleekButtonIcon resourcesButton;

		// Token: 0x040025EE RID: 9710
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x040025EF RID: 9711
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x040025F0 RID: 9712
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x040025F1 RID: 9713
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;
	}
}
