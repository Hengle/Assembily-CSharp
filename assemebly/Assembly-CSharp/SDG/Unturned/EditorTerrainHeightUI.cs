using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000760 RID: 1888
	public class EditorTerrainHeightUI
	{
		// Token: 0x0600359D RID: 13725 RVA: 0x0016800C File Offset: 0x0016640C
		public EditorTerrainHeightUI()
		{
			Local local = Localization.read("/Editor/EditorTerrainHeight.dat");
			Bundle bundle = Bundles.getBundle("/Bundles/Textures/Edit/Icons/EditorTerrainHeight/EditorTerrainHeight.unity3d");
			EditorTerrainHeightUI.container = new Sleek();
			EditorTerrainHeightUI.container.positionOffset_X = 10;
			EditorTerrainHeightUI.container.positionOffset_Y = 10;
			EditorTerrainHeightUI.container.positionScale_X = 1f;
			EditorTerrainHeightUI.container.sizeOffset_X = -20;
			EditorTerrainHeightUI.container.sizeOffset_Y = -20;
			EditorTerrainHeightUI.container.sizeScale_X = 1f;
			EditorTerrainHeightUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorTerrainHeightUI.container);
			EditorTerrainHeightUI.active = false;
			EditorTerrainHeightUI.adjustUpButton = new SleekButtonIcon((Texture2D)bundle.load("Adjust_Up"));
			EditorTerrainHeightUI.adjustUpButton.positionOffset_Y = -190;
			EditorTerrainHeightUI.adjustUpButton.positionScale_Y = 1f;
			EditorTerrainHeightUI.adjustUpButton.sizeOffset_X = 200;
			EditorTerrainHeightUI.adjustUpButton.sizeOffset_Y = 30;
			EditorTerrainHeightUI.adjustUpButton.text = local.format("AdjustUpButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorTerrainHeightUI.adjustUpButton.tooltip = local.format("AdjustUpButtonTooltip");
			SleekButton sleekButton = EditorTerrainHeightUI.adjustUpButton;
			if (EditorTerrainHeightUI.<>f__mg$cache0 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache0 = new ClickedButton(EditorTerrainHeightUI.onClickedAdjustUpButton);
			}
			sleekButton.onClickedButton = EditorTerrainHeightUI.<>f__mg$cache0;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.adjustUpButton);
			EditorTerrainHeightUI.adjustDownButton = new SleekButtonIcon((Texture2D)bundle.load("Adjust_Down"));
			EditorTerrainHeightUI.adjustDownButton.positionOffset_Y = -150;
			EditorTerrainHeightUI.adjustDownButton.positionScale_Y = 1f;
			EditorTerrainHeightUI.adjustDownButton.sizeOffset_X = 200;
			EditorTerrainHeightUI.adjustDownButton.sizeOffset_Y = 30;
			EditorTerrainHeightUI.adjustDownButton.text = local.format("AdjustDownButtonText", new object[]
			{
				ControlsSettings.tool_0
			});
			EditorTerrainHeightUI.adjustDownButton.tooltip = local.format("AdjustDownButtonTooltip");
			SleekButton sleekButton2 = EditorTerrainHeightUI.adjustDownButton;
			if (EditorTerrainHeightUI.<>f__mg$cache1 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache1 = new ClickedButton(EditorTerrainHeightUI.onClickedAdjustDownButton);
			}
			sleekButton2.onClickedButton = EditorTerrainHeightUI.<>f__mg$cache1;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.adjustDownButton);
			EditorTerrainHeightUI.smoothButton = new SleekButtonIcon((Texture2D)bundle.load("Smooth"));
			EditorTerrainHeightUI.smoothButton.positionOffset_Y = -110;
			EditorTerrainHeightUI.smoothButton.positionScale_Y = 1f;
			EditorTerrainHeightUI.smoothButton.sizeOffset_X = 200;
			EditorTerrainHeightUI.smoothButton.sizeOffset_Y = 30;
			EditorTerrainHeightUI.smoothButton.text = local.format("SmoothButtonText", new object[]
			{
				ControlsSettings.tool_1
			});
			EditorTerrainHeightUI.smoothButton.tooltip = local.format("SmoothButtonTooltip");
			SleekButton sleekButton3 = EditorTerrainHeightUI.smoothButton;
			if (EditorTerrainHeightUI.<>f__mg$cache2 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache2 = new ClickedButton(EditorTerrainHeightUI.onClickedSmoothButton);
			}
			sleekButton3.onClickedButton = EditorTerrainHeightUI.<>f__mg$cache2;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.smoothButton);
			EditorTerrainHeightUI.flattenButton = new SleekButtonIcon((Texture2D)bundle.load("Flatten"));
			EditorTerrainHeightUI.flattenButton.positionOffset_Y = -70;
			EditorTerrainHeightUI.flattenButton.positionScale_Y = 1f;
			EditorTerrainHeightUI.flattenButton.sizeOffset_X = 200;
			EditorTerrainHeightUI.flattenButton.sizeOffset_Y = 30;
			EditorTerrainHeightUI.flattenButton.text = local.format("FlattenButtonText", new object[]
			{
				ControlsSettings.tool_2
			});
			EditorTerrainHeightUI.flattenButton.tooltip = local.format("FlattenButtonTooltip");
			SleekButton sleekButton4 = EditorTerrainHeightUI.flattenButton;
			if (EditorTerrainHeightUI.<>f__mg$cache3 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache3 = new ClickedButton(EditorTerrainHeightUI.onClickedFlattenButton);
			}
			sleekButton4.onClickedButton = EditorTerrainHeightUI.<>f__mg$cache3;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.flattenButton);
			EditorTerrainHeightUI.map2Button = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(local.format("Map2ButtonText1")),
				new GUIContent(local.format("Map2ButtonText2"))
			});
			EditorTerrainHeightUI.map2Button.positionOffset_Y = -30;
			EditorTerrainHeightUI.map2Button.positionScale_Y = 1f;
			EditorTerrainHeightUI.map2Button.sizeOffset_X = 200;
			EditorTerrainHeightUI.map2Button.sizeOffset_Y = 30;
			EditorTerrainHeightUI.map2Button.tooltip = local.format("Map2ButtonTooltip");
			SleekButtonState sleekButtonState = EditorTerrainHeightUI.map2Button;
			if (EditorTerrainHeightUI.<>f__mg$cache4 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache4 = new SwappedState(EditorTerrainHeightUI.onSwappedMap2);
			}
			sleekButtonState.onSwappedState = EditorTerrainHeightUI.<>f__mg$cache4;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.map2Button);
			EditorTerrainHeightUI.noiseSlider = new SleekSlider();
			EditorTerrainHeightUI.noiseSlider.positionOffset_Y = -320;
			EditorTerrainHeightUI.noiseSlider.positionScale_Y = 1f;
			EditorTerrainHeightUI.noiseSlider.sizeOffset_X = 200;
			EditorTerrainHeightUI.noiseSlider.sizeOffset_Y = 20;
			EditorTerrainHeightUI.noiseSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorTerrainHeightUI.noiseSlider.state = EditorTerrainHeight.brushNoise;
			EditorTerrainHeightUI.noiseSlider.addLabel(local.format("NoiseSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider = EditorTerrainHeightUI.noiseSlider;
			if (EditorTerrainHeightUI.<>f__mg$cache5 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache5 = new Dragged(EditorTerrainHeightUI.onDraggedNoiseSlider);
			}
			sleekSlider.onDragged = EditorTerrainHeightUI.<>f__mg$cache5;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.noiseSlider);
			EditorTerrainHeightUI.sizeSlider = new SleekSlider();
			EditorTerrainHeightUI.sizeSlider.positionOffset_Y = -290;
			EditorTerrainHeightUI.sizeSlider.positionScale_Y = 1f;
			EditorTerrainHeightUI.sizeSlider.sizeOffset_X = 200;
			EditorTerrainHeightUI.sizeSlider.sizeOffset_Y = 20;
			EditorTerrainHeightUI.sizeSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorTerrainHeightUI.sizeSlider.state = (float)(EditorTerrainHeight.brushSize - EditorTerrainHeight.MIN_BRUSH_SIZE) / (float)EditorTerrainHeight.MAX_BRUSH_SIZE;
			EditorTerrainHeightUI.sizeSlider.addLabel(local.format("SizeSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider2 = EditorTerrainHeightUI.sizeSlider;
			if (EditorTerrainHeightUI.<>f__mg$cache6 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache6 = new Dragged(EditorTerrainHeightUI.onDraggedSizeSlider);
			}
			sleekSlider2.onDragged = EditorTerrainHeightUI.<>f__mg$cache6;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.sizeSlider);
			EditorTerrainHeightUI.strengthSlider = new SleekSlider();
			EditorTerrainHeightUI.strengthSlider.positionOffset_Y = -260;
			EditorTerrainHeightUI.strengthSlider.positionScale_Y = 1f;
			EditorTerrainHeightUI.strengthSlider.sizeOffset_X = 200;
			EditorTerrainHeightUI.strengthSlider.sizeOffset_Y = 20;
			EditorTerrainHeightUI.strengthSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorTerrainHeightUI.strengthSlider.addLabel(local.format("StrengthSliderLabelText"), ESleekSide.RIGHT);
			EditorTerrainHeightUI.strengthSlider.state = EditorTerrainHeight.brushStrength;
			SleekSlider sleekSlider3 = EditorTerrainHeightUI.strengthSlider;
			if (EditorTerrainHeightUI.<>f__mg$cache7 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache7 = new Dragged(EditorTerrainHeightUI.onDraggedStrengthSlider);
			}
			sleekSlider3.onDragged = EditorTerrainHeightUI.<>f__mg$cache7;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.strengthSlider);
			EditorTerrainHeightUI.heightValue = new SleekValue();
			EditorTerrainHeightUI.heightValue.positionOffset_Y = -230;
			EditorTerrainHeightUI.heightValue.positionScale_Y = 1f;
			EditorTerrainHeightUI.heightValue.sizeOffset_X = 200;
			EditorTerrainHeightUI.heightValue.sizeOffset_Y = 30;
			EditorTerrainHeightUI.heightValue.addLabel(local.format("HeightValueLabelText"), ESleekSide.RIGHT);
			EditorTerrainHeightUI.heightValue.state = EditorTerrainHeight.brushHeight;
			SleekValue sleekValue = EditorTerrainHeightUI.heightValue;
			if (EditorTerrainHeightUI.<>f__mg$cache8 == null)
			{
				EditorTerrainHeightUI.<>f__mg$cache8 = new Valued(EditorTerrainHeightUI.onValuedHeightValue);
			}
			sleekValue.onValued = EditorTerrainHeightUI.<>f__mg$cache8;
			EditorTerrainHeightUI.container.add(EditorTerrainHeightUI.heightValue);
			bundle.unload();
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00168728 File Offset: 0x00166B28
		public static void open()
		{
			if (EditorTerrainHeightUI.active)
			{
				return;
			}
			EditorTerrainHeightUI.active = true;
			if (LevelGround.materials == null)
			{
				return;
			}
			EditorTerrainHeight.isTerraforming = true;
			EditorUI.message(EEditorMessage.HEIGHTS);
			EditorTerrainHeightUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00168777 File Offset: 0x00166B77
		public static void close()
		{
			if (!EditorTerrainHeightUI.active)
			{
				return;
			}
			EditorTerrainHeightUI.active = false;
			if (LevelGround.materials == null)
			{
				return;
			}
			EditorTerrainHeight.isTerraforming = false;
			EditorTerrainHeightUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x001687B5 File Offset: 0x00166BB5
		private static void onClickedAdjustUpButton(SleekButton button)
		{
			EditorTerrainHeight.brushMode = EPaintMode.ADJUST_UP;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x001687BD File Offset: 0x00166BBD
		private static void onClickedAdjustDownButton(SleekButton button)
		{
			EditorTerrainHeight.brushMode = EPaintMode.ADJUST_DOWN;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x001687C5 File Offset: 0x00166BC5
		private static void onClickedSmoothButton(SleekButton button)
		{
			EditorTerrainHeight.brushMode = EPaintMode.SMOOTH;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x001687CD File Offset: 0x00166BCD
		private static void onClickedFlattenButton(SleekButton button)
		{
			EditorTerrainHeight.brushMode = EPaintMode.FLATTEN;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x001687D5 File Offset: 0x00166BD5
		private static void onSwappedMap2(SleekButtonState button, int state)
		{
			EditorTerrainHeight.map2 = (state == 1);
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x001687E0 File Offset: 0x00166BE0
		private static void onDraggedSizeSlider(SleekSlider slider, float state)
		{
			EditorTerrainHeight.brushSize = (byte)((float)EditorTerrainHeight.MIN_BRUSH_SIZE + state * (float)EditorTerrainHeight.MAX_BRUSH_SIZE);
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x001687F7 File Offset: 0x00166BF7
		private static void onDraggedNoiseSlider(SleekSlider slider, float state)
		{
			EditorTerrainHeight.brushNoise = state;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x001687FF File Offset: 0x00166BFF
		private static void onDraggedStrengthSlider(SleekSlider slider, float state)
		{
			EditorTerrainHeight.brushStrength = state;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x00168807 File Offset: 0x00166C07
		private static void onValuedHeightValue(SleekValue value, float state)
		{
			EditorTerrainHeight.brushHeight = state;
		}

		// Token: 0x0400257D RID: 9597
		private static Sleek container;

		// Token: 0x0400257E RID: 9598
		public static bool active;

		// Token: 0x0400257F RID: 9599
		private static SleekButtonIcon adjustUpButton;

		// Token: 0x04002580 RID: 9600
		private static SleekButtonIcon adjustDownButton;

		// Token: 0x04002581 RID: 9601
		private static SleekButtonIcon smoothButton;

		// Token: 0x04002582 RID: 9602
		private static SleekButtonIcon flattenButton;

		// Token: 0x04002583 RID: 9603
		private static SleekButtonState map2Button;

		// Token: 0x04002584 RID: 9604
		private static SleekSlider sizeSlider;

		// Token: 0x04002585 RID: 9605
		private static SleekSlider noiseSlider;

		// Token: 0x04002586 RID: 9606
		private static SleekSlider strengthSlider;

		// Token: 0x04002587 RID: 9607
		public static SleekValue heightValue;

		// Token: 0x04002588 RID: 9608
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache0;

		// Token: 0x04002589 RID: 9609
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache1;

		// Token: 0x0400258A RID: 9610
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache2;

		// Token: 0x0400258B RID: 9611
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache3;

		// Token: 0x0400258C RID: 9612
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache4;

		// Token: 0x0400258D RID: 9613
		[CompilerGenerated]
		private static Dragged <>f__mg$cache5;

		// Token: 0x0400258E RID: 9614
		[CompilerGenerated]
		private static Dragged <>f__mg$cache6;

		// Token: 0x0400258F RID: 9615
		[CompilerGenerated]
		private static Dragged <>f__mg$cache7;

		// Token: 0x04002590 RID: 9616
		[CompilerGenerated]
		private static Valued <>f__mg$cache8;
	}
}
