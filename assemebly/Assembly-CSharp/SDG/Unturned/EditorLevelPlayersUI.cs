using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200075B RID: 1883
	public class EditorLevelPlayersUI
	{
		// Token: 0x0600354E RID: 13646 RVA: 0x00163654 File Offset: 0x00161A54
		public EditorLevelPlayersUI()
		{
			Local local = Localization.read("/Editor/EditorLevelPlayers.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorLevelPlayers/EditorLevelPlayers.unity3d");
			EditorLevelPlayersUI.container = new Sleek();
			EditorLevelPlayersUI.container.positionOffset_X = 10;
			EditorLevelPlayersUI.container.positionOffset_Y = 10;
			EditorLevelPlayersUI.container.positionScale_X = 1f;
			EditorLevelPlayersUI.container.sizeOffset_X = -20;
			EditorLevelPlayersUI.container.sizeOffset_Y = -20;
			EditorLevelPlayersUI.container.sizeScale_X = 1f;
			EditorLevelPlayersUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorLevelPlayersUI.container);
			EditorLevelPlayersUI.active = false;
			EditorLevelPlayersUI.altToggle = new SleekToggle();
			EditorLevelPlayersUI.altToggle.positionOffset_Y = -180;
			EditorLevelPlayersUI.altToggle.positionScale_Y = 1f;
			EditorLevelPlayersUI.altToggle.sizeOffset_X = 40;
			EditorLevelPlayersUI.altToggle.sizeOffset_Y = 40;
			EditorLevelPlayersUI.altToggle.state = EditorSpawns.selectedAlt;
			EditorLevelPlayersUI.altToggle.addLabel(local.format("AltLabelText"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = EditorLevelPlayersUI.altToggle;
			if (EditorLevelPlayersUI.<>f__mg$cache0 == null)
			{
				EditorLevelPlayersUI.<>f__mg$cache0 = new Toggled(EditorLevelPlayersUI.onToggledAltToggle);
			}
			sleekToggle.onToggled = EditorLevelPlayersUI.<>f__mg$cache0;
			EditorLevelPlayersUI.container.add(EditorLevelPlayersUI.altToggle);
			EditorLevelPlayersUI.radiusSlider = new SleekSlider();
			EditorLevelPlayersUI.radiusSlider.positionOffset_Y = -130;
			EditorLevelPlayersUI.radiusSlider.positionScale_Y = 1f;
			EditorLevelPlayersUI.radiusSlider.sizeOffset_X = 200;
			EditorLevelPlayersUI.radiusSlider.sizeOffset_Y = 20;
			EditorLevelPlayersUI.radiusSlider.state = (float)(EditorSpawns.radius - EditorSpawns.MIN_REMOVE_SIZE) / (float)EditorSpawns.MAX_REMOVE_SIZE;
			EditorLevelPlayersUI.radiusSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorLevelPlayersUI.radiusSlider.addLabel(local.format("RadiusSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider = EditorLevelPlayersUI.radiusSlider;
			if (EditorLevelPlayersUI.<>f__mg$cache1 == null)
			{
				EditorLevelPlayersUI.<>f__mg$cache1 = new Dragged(EditorLevelPlayersUI.onDraggedRadiusSlider);
			}
			sleekSlider.onDragged = EditorLevelPlayersUI.<>f__mg$cache1;
			EditorLevelPlayersUI.container.add(EditorLevelPlayersUI.radiusSlider);
			EditorLevelPlayersUI.rotationSlider = new SleekSlider();
			EditorLevelPlayersUI.rotationSlider.positionOffset_Y = -100;
			EditorLevelPlayersUI.rotationSlider.positionScale_Y = 1f;
			EditorLevelPlayersUI.rotationSlider.sizeOffset_X = 200;
			EditorLevelPlayersUI.rotationSlider.sizeOffset_Y = 20;
			EditorLevelPlayersUI.rotationSlider.state = EditorSpawns.rotation / 360f;
			EditorLevelPlayersUI.rotationSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorLevelPlayersUI.rotationSlider.addLabel(local.format("RotationSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider2 = EditorLevelPlayersUI.rotationSlider;
			if (EditorLevelPlayersUI.<>f__mg$cache2 == null)
			{
				EditorLevelPlayersUI.<>f__mg$cache2 = new Dragged(EditorLevelPlayersUI.onDraggedRotationSlider);
			}
			sleekSlider2.onDragged = EditorLevelPlayersUI.<>f__mg$cache2;
			EditorLevelPlayersUI.container.add(EditorLevelPlayersUI.rotationSlider);
			EditorLevelPlayersUI.addButton = new SleekButtonIcon((Texture2D)bundle.load("Add"));
			EditorLevelPlayersUI.addButton.positionOffset_Y = -70;
			EditorLevelPlayersUI.addButton.positionScale_Y = 1f;
			EditorLevelPlayersUI.addButton.sizeOffset_X = 200;
			EditorLevelPlayersUI.addButton.sizeOffset_Y = 30;
			EditorLevelPlayersUI.addButton.text = local.format("AddButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorLevelPlayersUI.addButton.tooltip = local.format("AddButtonTooltip");
			SleekButton sleekButton = EditorLevelPlayersUI.addButton;
			if (EditorLevelPlayersUI.<>f__mg$cache3 == null)
			{
				EditorLevelPlayersUI.<>f__mg$cache3 = new ClickedButton(EditorLevelPlayersUI.onClickedAddButton);
			}
			sleekButton.onClickedButton = EditorLevelPlayersUI.<>f__mg$cache3;
			EditorLevelPlayersUI.container.add(EditorLevelPlayersUI.addButton);
			EditorLevelPlayersUI.removeButton = new SleekButtonIcon((Texture2D)bundle.load("Remove"));
			EditorLevelPlayersUI.removeButton.positionOffset_Y = -30;
			EditorLevelPlayersUI.removeButton.positionScale_Y = 1f;
			EditorLevelPlayersUI.removeButton.sizeOffset_X = 200;
			EditorLevelPlayersUI.removeButton.sizeOffset_Y = 30;
			EditorLevelPlayersUI.removeButton.text = local.format("RemoveButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			EditorLevelPlayersUI.removeButton.tooltip = local.format("RemoveButtonTooltip");
			SleekButton sleekButton2 = EditorLevelPlayersUI.removeButton;
			if (EditorLevelPlayersUI.<>f__mg$cache4 == null)
			{
				EditorLevelPlayersUI.<>f__mg$cache4 = new ClickedButton(EditorLevelPlayersUI.onClickedRemoveButton);
			}
			sleekButton2.onClickedButton = EditorLevelPlayersUI.<>f__mg$cache4;
			EditorLevelPlayersUI.container.add(EditorLevelPlayersUI.removeButton);
			bundle.unload();
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x00163A83 File Offset: 0x00161E83
		public static void open()
		{
			if (EditorLevelPlayersUI.active)
			{
				return;
			}
			EditorLevelPlayersUI.active = true;
			EditorSpawns.isSpawning = true;
			EditorSpawns.spawnMode = ESpawnMode.ADD_PLAYER;
			EditorLevelPlayersUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x00163ABC File Offset: 0x00161EBC
		public static void close()
		{
			if (!EditorLevelPlayersUI.active)
			{
				return;
			}
			EditorLevelPlayersUI.active = false;
			EditorSpawns.isSpawning = false;
			EditorLevelPlayersUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x00163AEF File Offset: 0x00161EEF
		private static void onToggledAltToggle(SleekToggle toggle, bool state)
		{
			EditorSpawns.selectedAlt = state;
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x00163AF7 File Offset: 0x00161EF7
		private static void onDraggedRadiusSlider(SleekSlider slider, float state)
		{
			EditorSpawns.radius = (byte)((float)EditorSpawns.MIN_REMOVE_SIZE + state * (float)EditorSpawns.MAX_REMOVE_SIZE);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x00163B0E File Offset: 0x00161F0E
		private static void onDraggedRotationSlider(SleekSlider slider, float state)
		{
			EditorSpawns.rotation = state * 360f;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x00163B1C File Offset: 0x00161F1C
		private static void onClickedAddButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.ADD_PLAYER;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x00163B24 File Offset: 0x00161F24
		private static void onClickedRemoveButton(SleekButton button)
		{
			EditorSpawns.spawnMode = ESpawnMode.REMOVE_PLAYER;
		}

		// Token: 0x040024F0 RID: 9456
		private static Sleek container;

		// Token: 0x040024F1 RID: 9457
		public static bool active;

		// Token: 0x040024F2 RID: 9458
		private static SleekToggle altToggle;

		// Token: 0x040024F3 RID: 9459
		private static SleekSlider radiusSlider;

		// Token: 0x040024F4 RID: 9460
		private static SleekSlider rotationSlider;

		// Token: 0x040024F5 RID: 9461
		private static SleekButtonIcon addButton;

		// Token: 0x040024F6 RID: 9462
		private static SleekButtonIcon removeButton;

		// Token: 0x040024F7 RID: 9463
		[CompilerGenerated]
		private static Toggled <>f__mg$cache0;

		// Token: 0x040024F8 RID: 9464
		[CompilerGenerated]
		private static Dragged <>f__mg$cache1;

		// Token: 0x040024F9 RID: 9465
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x040024FA RID: 9466
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x040024FB RID: 9467
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;
	}
}
