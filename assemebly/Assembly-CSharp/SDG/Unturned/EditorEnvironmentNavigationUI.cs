using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000750 RID: 1872
	public class EditorEnvironmentNavigationUI
	{
		// Token: 0x060034B5 RID: 13493 RVA: 0x0015AD6C File Offset: 0x0015916C
		public EditorEnvironmentNavigationUI()
		{
			Local local = Localization.read("/Editor/EditorEnvironmentNavigation.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorEnvironmentNavigation/EditorEnvironmentNavigation.unity3d");
			EditorEnvironmentNavigationUI.container = new Sleek();
			EditorEnvironmentNavigationUI.container.positionOffset_X = 10;
			EditorEnvironmentNavigationUI.container.positionOffset_Y = 10;
			EditorEnvironmentNavigationUI.container.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.container.sizeOffset_X = -20;
			EditorEnvironmentNavigationUI.container.sizeOffset_Y = -20;
			EditorEnvironmentNavigationUI.container.sizeScale_X = 1f;
			EditorEnvironmentNavigationUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorEnvironmentNavigationUI.container);
			EditorEnvironmentNavigationUI.active = false;
			EditorEnvironmentNavigationUI.widthSlider = new SleekSlider();
			EditorEnvironmentNavigationUI.widthSlider.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.widthSlider.positionOffset_Y = 80;
			EditorEnvironmentNavigationUI.widthSlider.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.widthSlider.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.widthSlider.sizeOffset_Y = 20;
			EditorEnvironmentNavigationUI.widthSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentNavigationUI.widthSlider.addLabel(local.format("Width_Label"), ESleekSide.LEFT);
			SleekSlider sleekSlider = EditorEnvironmentNavigationUI.widthSlider;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache0 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache0 = new Dragged(EditorEnvironmentNavigationUI.onDraggedWidthSlider);
			}
			sleekSlider.onDragged = EditorEnvironmentNavigationUI.<>f__mg$cache0;
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.widthSlider);
			EditorEnvironmentNavigationUI.widthSlider.isVisible = false;
			EditorEnvironmentNavigationUI.heightSlider = new SleekSlider();
			EditorEnvironmentNavigationUI.heightSlider.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.heightSlider.positionOffset_Y = 110;
			EditorEnvironmentNavigationUI.heightSlider.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.heightSlider.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.heightSlider.sizeOffset_Y = 20;
			EditorEnvironmentNavigationUI.heightSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentNavigationUI.heightSlider.addLabel(local.format("Height_Label"), ESleekSide.LEFT);
			SleekSlider sleekSlider2 = EditorEnvironmentNavigationUI.heightSlider;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache1 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache1 = new Dragged(EditorEnvironmentNavigationUI.onDraggedHeightSlider);
			}
			sleekSlider2.onDragged = EditorEnvironmentNavigationUI.<>f__mg$cache1;
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.heightSlider);
			EditorEnvironmentNavigationUI.heightSlider.isVisible = false;
			EditorEnvironmentNavigationUI.navBox = new SleekBox();
			EditorEnvironmentNavigationUI.navBox.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.navBox.positionOffset_Y = 140;
			EditorEnvironmentNavigationUI.navBox.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.navBox.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.navBox.sizeOffset_Y = 30;
			EditorEnvironmentNavigationUI.navBox.addLabel(local.format("Nav_Label"), ESleekSide.LEFT);
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.navBox);
			EditorEnvironmentNavigationUI.navBox.isVisible = false;
			EditorEnvironmentNavigationUI.difficultyGUIDField = new SleekField();
			EditorEnvironmentNavigationUI.difficultyGUIDField.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.difficultyGUIDField.positionOffset_Y = 180;
			EditorEnvironmentNavigationUI.difficultyGUIDField.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.difficultyGUIDField.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.difficultyGUIDField.sizeOffset_Y = 30;
			EditorEnvironmentNavigationUI.difficultyGUIDField.maxLength = 32;
			SleekField sleekField = EditorEnvironmentNavigationUI.difficultyGUIDField;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache2 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache2 = new Typed(EditorEnvironmentNavigationUI.onDifficultyGUIDFieldTyped);
			}
			sleekField.onTyped = EditorEnvironmentNavigationUI.<>f__mg$cache2;
			EditorEnvironmentNavigationUI.difficultyGUIDField.addLabel(local.format("Difficulty_GUID_Field_Label"), ESleekSide.LEFT);
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.difficultyGUIDField);
			EditorEnvironmentNavigationUI.difficultyGUIDField.isVisible = false;
			EditorEnvironmentNavigationUI.maxZombiesField = new SleekByteField();
			EditorEnvironmentNavigationUI.maxZombiesField.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.maxZombiesField.positionOffset_Y = 220;
			EditorEnvironmentNavigationUI.maxZombiesField.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.maxZombiesField.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.maxZombiesField.sizeOffset_Y = 30;
			SleekByteField sleekByteField = EditorEnvironmentNavigationUI.maxZombiesField;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache3 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache3 = new TypedByte(EditorEnvironmentNavigationUI.onMaxZombiesFieldTyped);
			}
			sleekByteField.onTypedByte = EditorEnvironmentNavigationUI.<>f__mg$cache3;
			EditorEnvironmentNavigationUI.maxZombiesField.addLabel(local.format("Max_Zombies_Field_Label"), ESleekSide.LEFT);
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.maxZombiesField);
			EditorEnvironmentNavigationUI.maxZombiesField.isVisible = false;
			EditorEnvironmentNavigationUI.spawnZombiesToggle = new SleekToggle();
			EditorEnvironmentNavigationUI.spawnZombiesToggle.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.spawnZombiesToggle.positionOffset_Y = 260;
			EditorEnvironmentNavigationUI.spawnZombiesToggle.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.spawnZombiesToggle.sizeOffset_X = 40;
			EditorEnvironmentNavigationUI.spawnZombiesToggle.sizeOffset_Y = 40;
			SleekToggle sleekToggle = EditorEnvironmentNavigationUI.spawnZombiesToggle;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache4 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache4 = new Toggled(EditorEnvironmentNavigationUI.onToggledSpawnZombiesToggle);
			}
			sleekToggle.onToggled = EditorEnvironmentNavigationUI.<>f__mg$cache4;
			EditorEnvironmentNavigationUI.spawnZombiesToggle.addLabel(local.format("Spawn_Zombies_Toggle_Label"), ESleekSide.RIGHT);
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.spawnZombiesToggle);
			EditorEnvironmentNavigationUI.spawnZombiesToggle.isVisible = false;
			EditorEnvironmentNavigationUI.bakeNavigationButton = new SleekButtonIcon((Texture2D)bundle.load("Navigation"));
			EditorEnvironmentNavigationUI.bakeNavigationButton.positionOffset_X = -200;
			EditorEnvironmentNavigationUI.bakeNavigationButton.positionOffset_Y = -30;
			EditorEnvironmentNavigationUI.bakeNavigationButton.positionScale_X = 1f;
			EditorEnvironmentNavigationUI.bakeNavigationButton.positionScale_Y = 1f;
			EditorEnvironmentNavigationUI.bakeNavigationButton.sizeOffset_X = 200;
			EditorEnvironmentNavigationUI.bakeNavigationButton.sizeOffset_Y = 30;
			EditorEnvironmentNavigationUI.bakeNavigationButton.text = local.format("Bake_Navigation");
			EditorEnvironmentNavigationUI.bakeNavigationButton.tooltip = local.format("Bake_Navigation_Tooltip");
			SleekButton sleekButton = EditorEnvironmentNavigationUI.bakeNavigationButton;
			if (EditorEnvironmentNavigationUI.<>f__mg$cache5 == null)
			{
				EditorEnvironmentNavigationUI.<>f__mg$cache5 = new ClickedButton(EditorEnvironmentNavigationUI.onClickedBakeNavigationButton);
			}
			sleekButton.onClickedButton = EditorEnvironmentNavigationUI.<>f__mg$cache5;
			EditorEnvironmentNavigationUI.container.add(EditorEnvironmentNavigationUI.bakeNavigationButton);
			EditorEnvironmentNavigationUI.bakeNavigationButton.isVisible = false;
			bundle.unload();
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x0015B2D8 File Offset: 0x001596D8
		public static void open()
		{
			if (EditorEnvironmentNavigationUI.active)
			{
				return;
			}
			EditorEnvironmentNavigationUI.active = true;
			EditorNavigation.isPathfinding = true;
			EditorUI.message(EEditorMessage.NAVIGATION);
			EditorEnvironmentNavigationUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x0015B311 File Offset: 0x00159711
		public static void close()
		{
			if (!EditorEnvironmentNavigationUI.active)
			{
				return;
			}
			EditorEnvironmentNavigationUI.active = false;
			EditorNavigation.isPathfinding = false;
			EditorEnvironmentNavigationUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x0015B344 File Offset: 0x00159744
		public static void updateSelection(Flag flag)
		{
			if (flag != null)
			{
				EditorEnvironmentNavigationUI.widthSlider.state = flag.width;
				EditorEnvironmentNavigationUI.heightSlider.state = flag.height;
				EditorEnvironmentNavigationUI.navBox.text = flag.graph.graphIndex.ToString();
				EditorEnvironmentNavigationUI.difficultyGUIDField.text = flag.data.difficultyGUID;
				EditorEnvironmentNavigationUI.maxZombiesField.state = flag.data.maxZombies;
				EditorEnvironmentNavigationUI.spawnZombiesToggle.state = flag.data.spawnZombies;
			}
			EditorEnvironmentNavigationUI.widthSlider.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.heightSlider.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.navBox.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.difficultyGUIDField.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.maxZombiesField.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.spawnZombiesToggle.isVisible = (flag != null);
			EditorEnvironmentNavigationUI.bakeNavigationButton.isVisible = (flag != null);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x0015B44D File Offset: 0x0015984D
		private static void onDraggedWidthSlider(SleekSlider slider, float state)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.width = state;
				EditorNavigation.flag.buildMesh();
			}
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x0015B46E File Offset: 0x0015986E
		private static void onDraggedHeightSlider(SleekSlider slider, float state)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.height = state;
				EditorNavigation.flag.buildMesh();
			}
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x0015B48F File Offset: 0x0015988F
		private static void onDifficultyGUIDFieldTyped(SleekField field, string state)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.data.difficultyGUID = state;
			}
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x0015B4AB File Offset: 0x001598AB
		private static void onMaxZombiesFieldTyped(SleekByteField field, byte state)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.data.maxZombies = state;
			}
		}

		// Token: 0x060034BD RID: 13501 RVA: 0x0015B4C7 File Offset: 0x001598C7
		private static void onToggledSpawnZombiesToggle(SleekToggle toggle, bool state)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.data.spawnZombies = state;
			}
		}

		// Token: 0x060034BE RID: 13502 RVA: 0x0015B4E3 File Offset: 0x001598E3
		private static void onClickedBakeNavigationButton(SleekButton button)
		{
			if (EditorNavigation.flag != null)
			{
				EditorNavigation.flag.bakeNavigation();
			}
		}

		// Token: 0x040023F1 RID: 9201
		private static Sleek container;

		// Token: 0x040023F2 RID: 9202
		public static bool active;

		// Token: 0x040023F3 RID: 9203
		private static SleekSlider widthSlider;

		// Token: 0x040023F4 RID: 9204
		private static SleekSlider heightSlider;

		// Token: 0x040023F5 RID: 9205
		private static SleekBox navBox;

		// Token: 0x040023F6 RID: 9206
		private static SleekField difficultyGUIDField;

		// Token: 0x040023F7 RID: 9207
		private static SleekByteField maxZombiesField;

		// Token: 0x040023F8 RID: 9208
		private static SleekToggle spawnZombiesToggle;

		// Token: 0x040023F9 RID: 9209
		private static SleekButtonIcon bakeNavigationButton;

		// Token: 0x040023FA RID: 9210
		[CompilerGenerated]
		private static Dragged <>f__mg$cache0;

		// Token: 0x040023FB RID: 9211
		[CompilerGenerated]
		private static Dragged <>f__mg$cache1;

		// Token: 0x040023FC RID: 9212
		[CompilerGenerated]
		private static Typed <>f__mg$cache2;

		// Token: 0x040023FD RID: 9213
		[CompilerGenerated]
		private static TypedByte <>f__mg$cache3;

		// Token: 0x040023FE RID: 9214
		[CompilerGenerated]
		private static Toggled <>f__mg$cache4;

		// Token: 0x040023FF RID: 9215
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;
	}
}
