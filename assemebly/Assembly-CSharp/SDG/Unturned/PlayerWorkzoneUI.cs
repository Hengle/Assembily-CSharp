using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007A4 RID: 1956
	public class PlayerWorkzoneUI
	{
		// Token: 0x060038F8 RID: 14584 RVA: 0x001A28D8 File Offset: 0x001A0CD8
		public PlayerWorkzoneUI()
		{
			Local local = Localization.read("/Editor/EditorLevelObjects.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorLevelObjects/EditorLevelObjects.unity3d");
			PlayerWorkzoneUI.container = new Sleek();
			PlayerWorkzoneUI.container.positionOffset_X = 10;
			PlayerWorkzoneUI.container.positionOffset_Y = 10;
			PlayerWorkzoneUI.container.positionScale_X = 1f;
			PlayerWorkzoneUI.container.sizeOffset_X = -20;
			PlayerWorkzoneUI.container.sizeOffset_Y = -20;
			PlayerWorkzoneUI.container.sizeScale_X = 1f;
			PlayerWorkzoneUI.container.sizeScale_Y = 1f;
			PlayerUI.window.add(PlayerWorkzoneUI.container);
			PlayerWorkzoneUI.active = false;
			PlayerWorkzone workzone = Player.player.workzone;
			if (PlayerWorkzoneUI.<>f__mg$cache0 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache0 = new DragStarted(PlayerWorkzoneUI.onDragStarted);
			}
			workzone.onDragStarted = PlayerWorkzoneUI.<>f__mg$cache0;
			PlayerWorkzone workzone2 = Player.player.workzone;
			if (PlayerWorkzoneUI.<>f__mg$cache1 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache1 = new DragStopped(PlayerWorkzoneUI.onDragStopped);
			}
			workzone2.onDragStopped = PlayerWorkzoneUI.<>f__mg$cache1;
			PlayerWorkzoneUI.dragBox = new SleekBox();
			PlayerUI.window.add(PlayerWorkzoneUI.dragBox);
			PlayerWorkzoneUI.dragBox.isVisible = false;
			PlayerWorkzoneUI.snapTransformField = new SleekSingleField();
			PlayerWorkzoneUI.snapTransformField.positionOffset_Y = -190;
			PlayerWorkzoneUI.snapTransformField.positionScale_Y = 1f;
			PlayerWorkzoneUI.snapTransformField.sizeOffset_X = 200;
			PlayerWorkzoneUI.snapTransformField.sizeOffset_Y = 30;
			PlayerWorkzoneUI.snapTransformField.text = (Mathf.Floor(Player.player.workzone.snapTransform * 100f) / 100f).ToString();
			PlayerWorkzoneUI.snapTransformField.addLabel(local.format("SnapTransformLabelText"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField = PlayerWorkzoneUI.snapTransformField;
			if (PlayerWorkzoneUI.<>f__mg$cache2 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache2 = new TypedSingle(PlayerWorkzoneUI.onTypedSnapTransformField);
			}
			sleekSingleField.onTypedSingle = PlayerWorkzoneUI.<>f__mg$cache2;
			PlayerWorkzoneUI.container.add(PlayerWorkzoneUI.snapTransformField);
			PlayerWorkzoneUI.snapRotationField = new SleekSingleField();
			PlayerWorkzoneUI.snapRotationField.positionOffset_Y = -150;
			PlayerWorkzoneUI.snapRotationField.positionScale_Y = 1f;
			PlayerWorkzoneUI.snapRotationField.sizeOffset_X = 200;
			PlayerWorkzoneUI.snapRotationField.sizeOffset_Y = 30;
			PlayerWorkzoneUI.snapRotationField.text = (Mathf.Floor(Player.player.workzone.snapRotation * 100f) / 100f).ToString();
			PlayerWorkzoneUI.snapRotationField.addLabel(local.format("SnapRotationLabelText"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField2 = PlayerWorkzoneUI.snapRotationField;
			if (PlayerWorkzoneUI.<>f__mg$cache3 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache3 = new TypedSingle(PlayerWorkzoneUI.onTypedSnapRotationField);
			}
			sleekSingleField2.onTypedSingle = PlayerWorkzoneUI.<>f__mg$cache3;
			PlayerWorkzoneUI.container.add(PlayerWorkzoneUI.snapRotationField);
			PlayerWorkzoneUI.transformButton = new SleekButtonIcon((Texture2D)bundle.load("Transform"));
			PlayerWorkzoneUI.transformButton.positionOffset_Y = -110;
			PlayerWorkzoneUI.transformButton.positionScale_Y = 1f;
			PlayerWorkzoneUI.transformButton.sizeOffset_X = 200;
			PlayerWorkzoneUI.transformButton.sizeOffset_Y = 30;
			PlayerWorkzoneUI.transformButton.text = local.format("TransformButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			PlayerWorkzoneUI.transformButton.tooltip = local.format("TransformButtonTooltip");
			SleekButton sleekButton = PlayerWorkzoneUI.transformButton;
			if (PlayerWorkzoneUI.<>f__mg$cache4 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache4 = new ClickedButton(PlayerWorkzoneUI.onClickedTransformButton);
			}
			sleekButton.onClickedButton = PlayerWorkzoneUI.<>f__mg$cache4;
			PlayerWorkzoneUI.container.add(PlayerWorkzoneUI.transformButton);
			PlayerWorkzoneUI.rotateButton = new SleekButtonIcon((Texture2D)bundle.load("Rotate"));
			PlayerWorkzoneUI.rotateButton.positionOffset_Y = -70;
			PlayerWorkzoneUI.rotateButton.positionScale_Y = 1f;
			PlayerWorkzoneUI.rotateButton.sizeOffset_X = 200;
			PlayerWorkzoneUI.rotateButton.sizeOffset_Y = 30;
			PlayerWorkzoneUI.rotateButton.text = local.format("RotateButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			PlayerWorkzoneUI.rotateButton.tooltip = local.format("RotateButtonTooltip");
			SleekButton sleekButton2 = PlayerWorkzoneUI.rotateButton;
			if (PlayerWorkzoneUI.<>f__mg$cache5 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache5 = new ClickedButton(PlayerWorkzoneUI.onClickedRotateButton);
			}
			sleekButton2.onClickedButton = PlayerWorkzoneUI.<>f__mg$cache5;
			PlayerWorkzoneUI.container.add(PlayerWorkzoneUI.rotateButton);
			PlayerWorkzoneUI.coordinateButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(local.format("CoordinateButtonTextGlobal"), (Texture)bundle.load("Global")),
				new GUIContent(local.format("CoordinateButtonTextLocal"), (Texture)bundle.load("Local"))
			});
			PlayerWorkzoneUI.coordinateButton.positionOffset_Y = -30;
			PlayerWorkzoneUI.coordinateButton.positionScale_Y = 1f;
			PlayerWorkzoneUI.coordinateButton.sizeOffset_X = 200;
			PlayerWorkzoneUI.coordinateButton.sizeOffset_Y = 30;
			PlayerWorkzoneUI.coordinateButton.tooltip = local.format("CoordinateButtonTooltip");
			SleekButtonState sleekButtonState = PlayerWorkzoneUI.coordinateButton;
			if (PlayerWorkzoneUI.<>f__mg$cache6 == null)
			{
				PlayerWorkzoneUI.<>f__mg$cache6 = new SwappedState(PlayerWorkzoneUI.onSwappedStateCoordinate);
			}
			sleekButtonState.onSwappedState = PlayerWorkzoneUI.<>f__mg$cache6;
			PlayerWorkzoneUI.container.add(PlayerWorkzoneUI.coordinateButton);
			bundle.unload();
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x001A2DEA File Offset: 0x001A11EA
		public static void open()
		{
			if (PlayerWorkzoneUI.active)
			{
				return;
			}
			PlayerWorkzoneUI.active = true;
			Player.player.workzone.isBuilding = true;
			PlayerWorkzoneUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x001A2E27 File Offset: 0x001A1227
		public static void close()
		{
			if (!PlayerWorkzoneUI.active)
			{
				return;
			}
			PlayerWorkzoneUI.active = false;
			Player.player.workzone.isBuilding = false;
			PlayerWorkzoneUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x001A2E64 File Offset: 0x001A1264
		private static void onDragStarted(int min_x, int min_y, int max_x, int max_y)
		{
			PlayerWorkzoneUI.dragBox.positionOffset_X = min_x;
			PlayerWorkzoneUI.dragBox.positionOffset_Y = min_y;
			PlayerWorkzoneUI.dragBox.sizeOffset_X = max_x - min_x;
			PlayerWorkzoneUI.dragBox.sizeOffset_Y = max_y - min_y;
			PlayerWorkzoneUI.dragBox.isVisible = true;
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x001A2EA1 File Offset: 0x001A12A1
		private static void onDragStopped()
		{
			PlayerWorkzoneUI.dragBox.isVisible = false;
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x001A2EAE File Offset: 0x001A12AE
		private static void onTypedSnapTransformField(SleekSingleField field, float value)
		{
			Player.player.workzone.snapTransform = value;
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x001A2EC0 File Offset: 0x001A12C0
		private static void onTypedSnapRotationField(SleekSingleField field, float value)
		{
			Player.player.workzone.snapRotation = value;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x001A2ED2 File Offset: 0x001A12D2
		private static void onClickedTransformButton(SleekButton button)
		{
			Player.player.workzone.dragMode = EDragMode.TRANSFORM;
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x001A2EE4 File Offset: 0x001A12E4
		private static void onClickedRotateButton(SleekButton button)
		{
			Player.player.workzone.dragMode = EDragMode.ROTATE;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x001A2EF6 File Offset: 0x001A12F6
		private static void onSwappedStateCoordinate(SleekButtonState button, int index)
		{
			Player.player.workzone.dragCoordinate = (EDragCoordinate)index;
		}

		// Token: 0x04002BCD RID: 11213
		private static Sleek container;

		// Token: 0x04002BCE RID: 11214
		public static bool active;

		// Token: 0x04002BCF RID: 11215
		private static SleekBox dragBox;

		// Token: 0x04002BD0 RID: 11216
		private static SleekSingleField snapTransformField;

		// Token: 0x04002BD1 RID: 11217
		private static SleekSingleField snapRotationField;

		// Token: 0x04002BD2 RID: 11218
		private static SleekButtonIcon transformButton;

		// Token: 0x04002BD3 RID: 11219
		private static SleekButtonIcon rotateButton;

		// Token: 0x04002BD4 RID: 11220
		public static SleekButtonState coordinateButton;

		// Token: 0x04002BD5 RID: 11221
		[CompilerGenerated]
		private static DragStarted <>f__mg$cache0;

		// Token: 0x04002BD6 RID: 11222
		[CompilerGenerated]
		private static DragStopped <>f__mg$cache1;

		// Token: 0x04002BD7 RID: 11223
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache2;

		// Token: 0x04002BD8 RID: 11224
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache3;

		// Token: 0x04002BD9 RID: 11225
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache4;

		// Token: 0x04002BDA RID: 11226
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache5;

		// Token: 0x04002BDB RID: 11227
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache6;
	}
}
