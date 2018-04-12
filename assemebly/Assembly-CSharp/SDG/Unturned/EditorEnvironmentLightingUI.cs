using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200074F RID: 1871
	public class EditorEnvironmentLightingUI
	{
		// Token: 0x0600349F RID: 13471 RVA: 0x00159C74 File Offset: 0x00158074
		public EditorEnvironmentLightingUI()
		{
			Local local = Localization.read("/Editor/EditorEnvironmentLighting.dat");
			EditorEnvironmentLightingUI.container = new Sleek();
			EditorEnvironmentLightingUI.container.positionOffset_X = 10;
			EditorEnvironmentLightingUI.container.positionOffset_Y = 10;
			EditorEnvironmentLightingUI.container.positionScale_X = 1f;
			EditorEnvironmentLightingUI.container.sizeOffset_X = -20;
			EditorEnvironmentLightingUI.container.sizeOffset_Y = -20;
			EditorEnvironmentLightingUI.container.sizeScale_X = 1f;
			EditorEnvironmentLightingUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorEnvironmentLightingUI.container);
			EditorEnvironmentLightingUI.active = false;
			EditorEnvironmentLightingUI.selectedTime = ELightingTime.DAWN;
			EditorEnvironmentLightingUI.azimuthSlider = new SleekSlider();
			EditorEnvironmentLightingUI.azimuthSlider.positionOffset_X = -230;
			EditorEnvironmentLightingUI.azimuthSlider.positionOffset_Y = 80;
			EditorEnvironmentLightingUI.azimuthSlider.positionScale_X = 1f;
			EditorEnvironmentLightingUI.azimuthSlider.sizeOffset_X = 230;
			EditorEnvironmentLightingUI.azimuthSlider.sizeOffset_Y = 20;
			EditorEnvironmentLightingUI.azimuthSlider.state = LevelLighting.azimuth / 360f;
			EditorEnvironmentLightingUI.azimuthSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentLightingUI.azimuthSlider.addLabel(local.format("AzimuthSliderLabelText"), ESleekSide.LEFT);
			SleekSlider sleekSlider = EditorEnvironmentLightingUI.azimuthSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cache0 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache0 = new Dragged(EditorEnvironmentLightingUI.onDraggedAzimuthSlider);
			}
			sleekSlider.onDragged = EditorEnvironmentLightingUI.<>f__mg$cache0;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.azimuthSlider);
			EditorEnvironmentLightingUI.biasSlider = new SleekSlider();
			EditorEnvironmentLightingUI.biasSlider.positionOffset_X = -230;
			EditorEnvironmentLightingUI.biasSlider.positionOffset_Y = 110;
			EditorEnvironmentLightingUI.biasSlider.positionScale_X = 1f;
			EditorEnvironmentLightingUI.biasSlider.sizeOffset_X = 230;
			EditorEnvironmentLightingUI.biasSlider.sizeOffset_Y = 20;
			EditorEnvironmentLightingUI.biasSlider.state = LevelLighting.bias;
			EditorEnvironmentLightingUI.biasSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentLightingUI.biasSlider.addLabel(local.format("BiasSliderLabelText"), ESleekSide.LEFT);
			SleekSlider sleekSlider2 = EditorEnvironmentLightingUI.biasSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cache1 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache1 = new Dragged(EditorEnvironmentLightingUI.onDraggedBiasSlider);
			}
			sleekSlider2.onDragged = EditorEnvironmentLightingUI.<>f__mg$cache1;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.biasSlider);
			EditorEnvironmentLightingUI.fadeSlider = new SleekSlider();
			EditorEnvironmentLightingUI.fadeSlider.positionOffset_X = -230;
			EditorEnvironmentLightingUI.fadeSlider.positionOffset_Y = 140;
			EditorEnvironmentLightingUI.fadeSlider.positionScale_X = 1f;
			EditorEnvironmentLightingUI.fadeSlider.sizeOffset_X = 230;
			EditorEnvironmentLightingUI.fadeSlider.sizeOffset_Y = 20;
			EditorEnvironmentLightingUI.fadeSlider.state = LevelLighting.fade;
			EditorEnvironmentLightingUI.fadeSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentLightingUI.fadeSlider.addLabel(local.format("FadeSliderLabelText"), ESleekSide.LEFT);
			SleekSlider sleekSlider3 = EditorEnvironmentLightingUI.fadeSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cache2 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache2 = new Dragged(EditorEnvironmentLightingUI.onDraggedFadeSlider);
			}
			sleekSlider3.onDragged = EditorEnvironmentLightingUI.<>f__mg$cache2;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.fadeSlider);
			EditorEnvironmentLightingUI.lightingScrollBox = new SleekScrollBox();
			EditorEnvironmentLightingUI.lightingScrollBox.positionOffset_X = -470;
			EditorEnvironmentLightingUI.lightingScrollBox.positionOffset_Y = 170;
			EditorEnvironmentLightingUI.lightingScrollBox.positionScale_X = 1f;
			EditorEnvironmentLightingUI.lightingScrollBox.sizeOffset_X = 470;
			EditorEnvironmentLightingUI.lightingScrollBox.sizeOffset_Y = -170;
			EditorEnvironmentLightingUI.lightingScrollBox.sizeScale_Y = 1f;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.lightingScrollBox);
			EditorEnvironmentLightingUI.seaLevelSlider = new SleekValue();
			EditorEnvironmentLightingUI.seaLevelSlider.positionOffset_Y = -130;
			EditorEnvironmentLightingUI.seaLevelSlider.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.seaLevelSlider.sizeOffset_X = 200;
			EditorEnvironmentLightingUI.seaLevelSlider.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.seaLevelSlider.state = LevelLighting.seaLevel;
			EditorEnvironmentLightingUI.seaLevelSlider.addLabel(local.format("Sea_Level_Slider_Label"), ESleekSide.RIGHT);
			SleekValue sleekValue = EditorEnvironmentLightingUI.seaLevelSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cache3 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache3 = new Valued(EditorEnvironmentLightingUI.onValuedSeaLevelSlider);
			}
			sleekValue.onValued = EditorEnvironmentLightingUI.<>f__mg$cache3;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.seaLevelSlider);
			EditorEnvironmentLightingUI.snowLevelSlider = new SleekValue();
			EditorEnvironmentLightingUI.snowLevelSlider.positionOffset_Y = -90;
			EditorEnvironmentLightingUI.snowLevelSlider.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.snowLevelSlider.sizeOffset_X = 200;
			EditorEnvironmentLightingUI.snowLevelSlider.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.snowLevelSlider.state = LevelLighting.snowLevel;
			EditorEnvironmentLightingUI.snowLevelSlider.addLabel(local.format("Snow_Level_Slider_Label"), ESleekSide.RIGHT);
			SleekValue sleekValue2 = EditorEnvironmentLightingUI.snowLevelSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cache4 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache4 = new Valued(EditorEnvironmentLightingUI.onValuedSnowLevelSlider);
			}
			sleekValue2.onValued = EditorEnvironmentLightingUI.<>f__mg$cache4;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.snowLevelSlider);
			EditorEnvironmentLightingUI.rainFreqField = new SleekSingleField();
			EditorEnvironmentLightingUI.rainFreqField.positionOffset_Y = -370;
			EditorEnvironmentLightingUI.rainFreqField.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.rainFreqField.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.rainFreqField.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.rainFreqField.state = LevelLighting.rainFreq;
			EditorEnvironmentLightingUI.rainFreqField.addLabel(local.format("Rain_Freq_Label"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField = EditorEnvironmentLightingUI.rainFreqField;
			if (EditorEnvironmentLightingUI.<>f__mg$cache5 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache5 = new TypedSingle(EditorEnvironmentLightingUI.onTypedRainFreqField);
			}
			sleekSingleField.onTypedSingle = EditorEnvironmentLightingUI.<>f__mg$cache5;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.rainFreqField);
			EditorEnvironmentLightingUI.rainDurField = new SleekSingleField();
			EditorEnvironmentLightingUI.rainDurField.positionOffset_Y = -330;
			EditorEnvironmentLightingUI.rainDurField.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.rainDurField.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.rainDurField.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.rainDurField.state = LevelLighting.rainDur;
			EditorEnvironmentLightingUI.rainDurField.addLabel(local.format("Rain_Dur_Label"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField2 = EditorEnvironmentLightingUI.rainDurField;
			if (EditorEnvironmentLightingUI.<>f__mg$cache6 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache6 = new TypedSingle(EditorEnvironmentLightingUI.onTypedRainDurField);
			}
			sleekSingleField2.onTypedSingle = EditorEnvironmentLightingUI.<>f__mg$cache6;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.rainDurField);
			EditorEnvironmentLightingUI.snowFreqField = new SleekSingleField();
			EditorEnvironmentLightingUI.snowFreqField.positionOffset_Y = -290;
			EditorEnvironmentLightingUI.snowFreqField.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.snowFreqField.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.snowFreqField.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.snowFreqField.state = LevelLighting.snowFreq;
			EditorEnvironmentLightingUI.snowFreqField.addLabel(local.format("Snow_Freq_Label"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField3 = EditorEnvironmentLightingUI.snowFreqField;
			if (EditorEnvironmentLightingUI.<>f__mg$cache7 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache7 = new TypedSingle(EditorEnvironmentLightingUI.onTypedSnowFreqField);
			}
			sleekSingleField3.onTypedSingle = EditorEnvironmentLightingUI.<>f__mg$cache7;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.snowFreqField);
			EditorEnvironmentLightingUI.snowDurField = new SleekSingleField();
			EditorEnvironmentLightingUI.snowDurField.positionOffset_Y = -250;
			EditorEnvironmentLightingUI.snowDurField.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.snowDurField.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.snowDurField.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.snowDurField.state = LevelLighting.snowDur;
			EditorEnvironmentLightingUI.snowDurField.addLabel(local.format("Snow_Dur_Label"), ESleekSide.RIGHT);
			SleekSingleField sleekSingleField4 = EditorEnvironmentLightingUI.snowDurField;
			if (EditorEnvironmentLightingUI.<>f__mg$cache8 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache8 = new TypedSingle(EditorEnvironmentLightingUI.onTypedSnowDurField);
			}
			sleekSingleField4.onTypedSingle = EditorEnvironmentLightingUI.<>f__mg$cache8;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.snowDurField);
			EditorEnvironmentLightingUI.stormButton = new SleekButton();
			EditorEnvironmentLightingUI.stormButton.positionOffset_Y = -210;
			EditorEnvironmentLightingUI.stormButton.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.stormButton.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.stormButton.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.stormButton.text = local.format("Storm");
			EditorEnvironmentLightingUI.stormButton.tooltip = local.format("Storm_Tooltip");
			SleekButton sleekButton = EditorEnvironmentLightingUI.stormButton;
			if (EditorEnvironmentLightingUI.<>f__mg$cache9 == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cache9 = new ClickedButton(EditorEnvironmentLightingUI.onClickedStormButton);
			}
			sleekButton.onClickedButton = EditorEnvironmentLightingUI.<>f__mg$cache9;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.stormButton);
			EditorEnvironmentLightingUI.rainToggle = new SleekToggle();
			EditorEnvironmentLightingUI.rainToggle.positionOffset_X = 110;
			EditorEnvironmentLightingUI.rainToggle.positionOffset_Y = -215;
			EditorEnvironmentLightingUI.rainToggle.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.rainToggle.sizeOffset_X = 40;
			EditorEnvironmentLightingUI.rainToggle.sizeOffset_Y = 40;
			EditorEnvironmentLightingUI.rainToggle.state = LevelLighting.canRain;
			EditorEnvironmentLightingUI.rainToggle.addLabel(local.format("Rain_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = EditorEnvironmentLightingUI.rainToggle;
			if (EditorEnvironmentLightingUI.<>f__mg$cacheA == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cacheA = new Toggled(EditorEnvironmentLightingUI.onToggledRainToggle);
			}
			sleekToggle.onToggled = EditorEnvironmentLightingUI.<>f__mg$cacheA;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.rainToggle);
			EditorEnvironmentLightingUI.blizzardButton = new SleekButton();
			EditorEnvironmentLightingUI.blizzardButton.positionOffset_Y = -170;
			EditorEnvironmentLightingUI.blizzardButton.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.blizzardButton.sizeOffset_X = 100;
			EditorEnvironmentLightingUI.blizzardButton.sizeOffset_Y = 30;
			EditorEnvironmentLightingUI.blizzardButton.text = local.format("Blizzard");
			EditorEnvironmentLightingUI.blizzardButton.tooltip = local.format("Blizzard_Tooltip");
			SleekButton sleekButton2 = EditorEnvironmentLightingUI.blizzardButton;
			if (EditorEnvironmentLightingUI.<>f__mg$cacheB == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cacheB = new ClickedButton(EditorEnvironmentLightingUI.onClickedBlizzardButton);
			}
			sleekButton2.onClickedButton = EditorEnvironmentLightingUI.<>f__mg$cacheB;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.blizzardButton);
			EditorEnvironmentLightingUI.snowToggle = new SleekToggle();
			EditorEnvironmentLightingUI.snowToggle.positionOffset_X = 110;
			EditorEnvironmentLightingUI.snowToggle.positionOffset_Y = -175;
			EditorEnvironmentLightingUI.snowToggle.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.snowToggle.sizeOffset_X = 40;
			EditorEnvironmentLightingUI.snowToggle.sizeOffset_Y = 40;
			EditorEnvironmentLightingUI.snowToggle.state = LevelLighting.canSnow;
			EditorEnvironmentLightingUI.snowToggle.addLabel(local.format("Snow_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle2 = EditorEnvironmentLightingUI.snowToggle;
			if (EditorEnvironmentLightingUI.<>f__mg$cacheC == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cacheC = new Toggled(EditorEnvironmentLightingUI.onToggledSnowToggle);
			}
			sleekToggle2.onToggled = EditorEnvironmentLightingUI.<>f__mg$cacheC;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.snowToggle);
			EditorEnvironmentLightingUI.moonSlider = new SleekSlider();
			EditorEnvironmentLightingUI.moonSlider.positionOffset_Y = -50;
			EditorEnvironmentLightingUI.moonSlider.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.moonSlider.sizeOffset_X = 200;
			EditorEnvironmentLightingUI.moonSlider.sizeOffset_Y = 20;
			EditorEnvironmentLightingUI.moonSlider.state = (float)LevelLighting.moon / (float)LevelLighting.MOON_CYCLES;
			EditorEnvironmentLightingUI.moonSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentLightingUI.moonSlider.addLabel(local.format("MoonSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider4 = EditorEnvironmentLightingUI.moonSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cacheD == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cacheD = new Dragged(EditorEnvironmentLightingUI.onDraggedMoonSlider);
			}
			sleekSlider4.onDragged = EditorEnvironmentLightingUI.<>f__mg$cacheD;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.moonSlider);
			EditorEnvironmentLightingUI.timeSlider = new SleekSlider();
			EditorEnvironmentLightingUI.timeSlider.positionOffset_Y = -20;
			EditorEnvironmentLightingUI.timeSlider.positionScale_Y = 1f;
			EditorEnvironmentLightingUI.timeSlider.sizeOffset_X = 200;
			EditorEnvironmentLightingUI.timeSlider.sizeOffset_Y = 20;
			EditorEnvironmentLightingUI.timeSlider.state = LevelLighting.time;
			EditorEnvironmentLightingUI.timeSlider.orientation = ESleekOrientation.HORIZONTAL;
			EditorEnvironmentLightingUI.timeSlider.addLabel(local.format("TimeSliderLabelText"), ESleekSide.RIGHT);
			SleekSlider sleekSlider5 = EditorEnvironmentLightingUI.timeSlider;
			if (EditorEnvironmentLightingUI.<>f__mg$cacheE == null)
			{
				EditorEnvironmentLightingUI.<>f__mg$cacheE = new Dragged(EditorEnvironmentLightingUI.onDraggedTimeSlider);
			}
			sleekSlider5.onDragged = EditorEnvironmentLightingUI.<>f__mg$cacheE;
			EditorEnvironmentLightingUI.container.add(EditorEnvironmentLightingUI.timeSlider);
			EditorEnvironmentLightingUI.timeButtons = new SleekButton[4];
			for (int i = 0; i < EditorEnvironmentLightingUI.timeButtons.Length; i++)
			{
				SleekButton sleekButton3 = new SleekButton();
				sleekButton3.positionOffset_X = 240;
				sleekButton3.positionOffset_Y = i * 40;
				sleekButton3.sizeOffset_X = 200;
				sleekButton3.sizeOffset_Y = 30;
				sleekButton3.text = local.format("Time_" + i);
				SleekButton sleekButton4 = sleekButton3;
				Delegate onClickedButton = sleekButton4.onClickedButton;
				if (EditorEnvironmentLightingUI.<>f__mg$cacheF == null)
				{
					EditorEnvironmentLightingUI.<>f__mg$cacheF = new ClickedButton(EditorEnvironmentLightingUI.onClickedTimeButton);
				}
				sleekButton4.onClickedButton = (ClickedButton)Delegate.Combine(onClickedButton, EditorEnvironmentLightingUI.<>f__mg$cacheF);
				EditorEnvironmentLightingUI.lightingScrollBox.add(sleekButton3);
				EditorEnvironmentLightingUI.timeButtons[i] = sleekButton3;
			}
			EditorEnvironmentLightingUI.infoBoxes = new SleekBox[12];
			EditorEnvironmentLightingUI.colorPickers = new SleekColorPicker[EditorEnvironmentLightingUI.infoBoxes.Length];
			EditorEnvironmentLightingUI.singleSliders = new SleekSlider[5];
			for (int j = 0; j < EditorEnvironmentLightingUI.colorPickers.Length; j++)
			{
				SleekBox sleekBox = new SleekBox();
				sleekBox.positionOffset_X = 240;
				sleekBox.positionOffset_Y = EditorEnvironmentLightingUI.timeButtons.Length * 40 + j * 170;
				sleekBox.sizeOffset_X = 200;
				sleekBox.sizeOffset_Y = 30;
				sleekBox.text = local.format("Color_" + j);
				EditorEnvironmentLightingUI.lightingScrollBox.add(sleekBox);
				EditorEnvironmentLightingUI.infoBoxes[j] = sleekBox;
				SleekColorPicker sleekColorPicker = new SleekColorPicker();
				sleekColorPicker.positionOffset_X = 200;
				sleekColorPicker.positionOffset_Y = EditorEnvironmentLightingUI.timeButtons.Length * 40 + j * 170 + 40;
				SleekColorPicker sleekColorPicker2 = sleekColorPicker;
				Delegate onColorPicked = sleekColorPicker2.onColorPicked;
				if (EditorEnvironmentLightingUI.<>f__mg$cache10 == null)
				{
					EditorEnvironmentLightingUI.<>f__mg$cache10 = new ColorPicked(EditorEnvironmentLightingUI.onPickedColorPicker);
				}
				sleekColorPicker2.onColorPicked = (ColorPicked)Delegate.Combine(onColorPicked, EditorEnvironmentLightingUI.<>f__mg$cache10);
				EditorEnvironmentLightingUI.lightingScrollBox.add(sleekColorPicker);
				EditorEnvironmentLightingUI.colorPickers[j] = sleekColorPicker;
			}
			for (int k = 0; k < EditorEnvironmentLightingUI.singleSliders.Length; k++)
			{
				SleekSlider sleekSlider6 = new SleekSlider();
				sleekSlider6.positionOffset_X = 240;
				sleekSlider6.positionOffset_Y = EditorEnvironmentLightingUI.timeButtons.Length * 40 + EditorEnvironmentLightingUI.colorPickers.Length * 170 + k * 30;
				sleekSlider6.sizeOffset_X = 200;
				sleekSlider6.sizeOffset_Y = 20;
				sleekSlider6.orientation = ESleekOrientation.HORIZONTAL;
				sleekSlider6.addLabel(local.format("Single_" + k), ESleekSide.LEFT);
				SleekSlider sleekSlider7 = sleekSlider6;
				Delegate onDragged = sleekSlider7.onDragged;
				if (EditorEnvironmentLightingUI.<>f__mg$cache11 == null)
				{
					EditorEnvironmentLightingUI.<>f__mg$cache11 = new Dragged(EditorEnvironmentLightingUI.onDraggedSingleSlider);
				}
				sleekSlider7.onDragged = (Dragged)Delegate.Combine(onDragged, EditorEnvironmentLightingUI.<>f__mg$cache11);
				EditorEnvironmentLightingUI.lightingScrollBox.add(sleekSlider6);
				EditorEnvironmentLightingUI.singleSliders[k] = sleekSlider6;
			}
			EditorEnvironmentLightingUI.lightingScrollBox.area = new Rect(0f, 0f, 5f, (float)(EditorEnvironmentLightingUI.timeButtons.Length * 40 + EditorEnvironmentLightingUI.colorPickers.Length * 170 + EditorEnvironmentLightingUI.singleSliders.Length * 30 - 10));
			EditorEnvironmentLightingUI.updateSelection();
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x0015AA38 File Offset: 0x00158E38
		public static void open()
		{
			if (EditorEnvironmentLightingUI.active)
			{
				return;
			}
			EditorEnvironmentLightingUI.active = true;
			EditorEnvironmentLightingUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x0015AA65 File Offset: 0x00158E65
		public static void close()
		{
			if (!EditorEnvironmentLightingUI.active)
			{
				return;
			}
			EditorEnvironmentLightingUI.active = false;
			EditorEnvironmentLightingUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x0015AA92 File Offset: 0x00158E92
		private static void onDraggedAzimuthSlider(SleekSlider slider, float state)
		{
			LevelLighting.azimuth = state * 360f;
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x0015AAA0 File Offset: 0x00158EA0
		private static void onDraggedBiasSlider(SleekSlider slider, float state)
		{
			LevelLighting.bias = state;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x0015AAA8 File Offset: 0x00158EA8
		private static void onDraggedFadeSlider(SleekSlider slider, float state)
		{
			LevelLighting.fade = state;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x0015AAB0 File Offset: 0x00158EB0
		private static void onValuedSeaLevelSlider(SleekValue slider, float state)
		{
			LevelLighting.seaLevel = state;
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x0015AAB8 File Offset: 0x00158EB8
		private static void onValuedSnowLevelSlider(SleekValue slider, float state)
		{
			LevelLighting.snowLevel = state;
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x0015AAC0 File Offset: 0x00158EC0
		private static void onToggledRainToggle(SleekToggle toggle, bool state)
		{
			LevelLighting.canRain = state;
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x0015AAC8 File Offset: 0x00158EC8
		private static void onToggledSnowToggle(SleekToggle toggle, bool state)
		{
			LevelLighting.canSnow = state;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x0015AAD0 File Offset: 0x00158ED0
		private static void onTypedRainFreqField(SleekSingleField field, float state)
		{
			LevelLighting.rainFreq = state;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x0015AAD8 File Offset: 0x00158ED8
		private static void onTypedRainDurField(SleekSingleField field, float state)
		{
			LevelLighting.rainDur = state;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x0015AAE0 File Offset: 0x00158EE0
		private static void onTypedSnowFreqField(SleekSingleField field, float state)
		{
			LevelLighting.snowFreq = state;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x0015AAE8 File Offset: 0x00158EE8
		private static void onTypedSnowDurField(SleekSingleField field, float state)
		{
			LevelLighting.snowDur = state;
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x0015AAF0 File Offset: 0x00158EF0
		private static void onDraggedMoonSlider(SleekSlider slider, float state)
		{
			byte b = (byte)(state * (float)LevelLighting.MOON_CYCLES);
			if (b >= LevelLighting.MOON_CYCLES)
			{
				b = LevelLighting.MOON_CYCLES - 1;
			}
			LevelLighting.moon = b;
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x0015AB21 File Offset: 0x00158F21
		private static void onDraggedTimeSlider(SleekSlider slider, float state)
		{
			LevelLighting.time = state;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x0015AB2C File Offset: 0x00158F2C
		private static void onClickedTimeButton(SleekButton button)
		{
			int i;
			for (i = 0; i < EditorEnvironmentLightingUI.timeButtons.Length; i++)
			{
				if (EditorEnvironmentLightingUI.timeButtons[i] == button)
				{
					break;
				}
			}
			EditorEnvironmentLightingUI.selectedTime = (ELightingTime)i;
			EditorEnvironmentLightingUI.updateSelection();
			switch (EditorEnvironmentLightingUI.selectedTime)
			{
			case ELightingTime.DAWN:
				LevelLighting.time = 0f;
				break;
			case ELightingTime.MIDDAY:
				LevelLighting.time = LevelLighting.bias / 2f;
				break;
			case ELightingTime.DUSK:
				LevelLighting.time = LevelLighting.bias;
				break;
			case ELightingTime.MIDNIGHT:
				LevelLighting.time = 1f - (1f - LevelLighting.bias) / 2f;
				break;
			}
			LevelLighting.updateClouds();
			EditorEnvironmentLightingUI.timeSlider.state = LevelLighting.time;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x0015ABF7 File Offset: 0x00158FF7
		private static void onClickedStormButton(SleekButton button)
		{
			if (LevelLighting.rainyness == ELightingRain.NONE)
			{
				LevelLighting.rainyness = ELightingRain.DRIZZLE;
			}
			else
			{
				LevelLighting.rainyness = ELightingRain.NONE;
			}
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x0015AC14 File Offset: 0x00159014
		private static void onClickedBlizzardButton(SleekButton button)
		{
			if (LevelLighting.snowyness == ELightingSnow.NONE)
			{
				LevelLighting.snowyness = ELightingSnow.BLIZZARD;
			}
			else
			{
				LevelLighting.snowyness = ELightingSnow.NONE;
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x0015AC34 File Offset: 0x00159034
		private static void onPickedColorPicker(SleekColorPicker picker, Color state)
		{
			int i;
			for (i = 0; i < EditorEnvironmentLightingUI.colorPickers.Length; i++)
			{
				if (EditorEnvironmentLightingUI.colorPickers[i] == picker)
				{
					break;
				}
			}
			LevelLighting.times[(int)EditorEnvironmentLightingUI.selectedTime].colors[i] = state;
			LevelLighting.updateLighting();
		}

		// Token: 0x060034B3 RID: 13491 RVA: 0x0015AC8C File Offset: 0x0015908C
		private static void onDraggedSingleSlider(SleekSlider slider, float state)
		{
			int i;
			for (i = 0; i < EditorEnvironmentLightingUI.singleSliders.Length; i++)
			{
				if (EditorEnvironmentLightingUI.singleSliders[i] == slider)
				{
					break;
				}
			}
			LevelLighting.times[(int)EditorEnvironmentLightingUI.selectedTime].singles[i] = state;
			LevelLighting.updateLighting();
			if (i == 2)
			{
				LevelLighting.updateClouds();
			}
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x0015ACE8 File Offset: 0x001590E8
		private static void updateSelection()
		{
			for (int i = 0; i < EditorEnvironmentLightingUI.colorPickers.Length; i++)
			{
				EditorEnvironmentLightingUI.colorPickers[i].state = LevelLighting.times[(int)EditorEnvironmentLightingUI.selectedTime].colors[i];
			}
			for (int j = 0; j < EditorEnvironmentLightingUI.singleSliders.Length; j++)
			{
				EditorEnvironmentLightingUI.singleSliders[j].state = LevelLighting.times[(int)EditorEnvironmentLightingUI.selectedTime].singles[j];
			}
		}

		// Token: 0x040023C8 RID: 9160
		private static Sleek container;

		// Token: 0x040023C9 RID: 9161
		public static bool active;

		// Token: 0x040023CA RID: 9162
		private static SleekScrollBox lightingScrollBox;

		// Token: 0x040023CB RID: 9163
		private static SleekSlider azimuthSlider;

		// Token: 0x040023CC RID: 9164
		private static SleekSlider biasSlider;

		// Token: 0x040023CD RID: 9165
		private static SleekSlider fadeSlider;

		// Token: 0x040023CE RID: 9166
		private static SleekButton[] timeButtons;

		// Token: 0x040023CF RID: 9167
		private static SleekBox[] infoBoxes;

		// Token: 0x040023D0 RID: 9168
		private static SleekColorPicker[] colorPickers;

		// Token: 0x040023D1 RID: 9169
		private static SleekSlider[] singleSliders;

		// Token: 0x040023D2 RID: 9170
		private static ELightingTime selectedTime;

		// Token: 0x040023D3 RID: 9171
		private static SleekValue seaLevelSlider;

		// Token: 0x040023D4 RID: 9172
		private static SleekValue snowLevelSlider;

		// Token: 0x040023D5 RID: 9173
		private static SleekSingleField rainFreqField;

		// Token: 0x040023D6 RID: 9174
		private static SleekSingleField rainDurField;

		// Token: 0x040023D7 RID: 9175
		private static SleekSingleField snowFreqField;

		// Token: 0x040023D8 RID: 9176
		private static SleekSingleField snowDurField;

		// Token: 0x040023D9 RID: 9177
		private static SleekButton stormButton;

		// Token: 0x040023DA RID: 9178
		private static SleekToggle rainToggle;

		// Token: 0x040023DB RID: 9179
		private static SleekButton blizzardButton;

		// Token: 0x040023DC RID: 9180
		private static SleekToggle snowToggle;

		// Token: 0x040023DD RID: 9181
		private static SleekSlider moonSlider;

		// Token: 0x040023DE RID: 9182
		private static SleekSlider timeSlider;

		// Token: 0x040023DF RID: 9183
		[CompilerGenerated]
		private static Dragged <>f__mg$cache0;

		// Token: 0x040023E0 RID: 9184
		[CompilerGenerated]
		private static Dragged <>f__mg$cache1;

		// Token: 0x040023E1 RID: 9185
		[CompilerGenerated]
		private static Dragged <>f__mg$cache2;

		// Token: 0x040023E2 RID: 9186
		[CompilerGenerated]
		private static Valued <>f__mg$cache3;

		// Token: 0x040023E3 RID: 9187
		[CompilerGenerated]
		private static Valued <>f__mg$cache4;

		// Token: 0x040023E4 RID: 9188
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache5;

		// Token: 0x040023E5 RID: 9189
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache6;

		// Token: 0x040023E6 RID: 9190
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache7;

		// Token: 0x040023E7 RID: 9191
		[CompilerGenerated]
		private static TypedSingle <>f__mg$cache8;

		// Token: 0x040023E8 RID: 9192
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache9;

		// Token: 0x040023E9 RID: 9193
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheA;

		// Token: 0x040023EA RID: 9194
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheB;

		// Token: 0x040023EB RID: 9195
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheC;

		// Token: 0x040023EC RID: 9196
		[CompilerGenerated]
		private static Dragged <>f__mg$cacheD;

		// Token: 0x040023ED RID: 9197
		[CompilerGenerated]
		private static Dragged <>f__mg$cacheE;

		// Token: 0x040023EE RID: 9198
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cacheF;

		// Token: 0x040023EF RID: 9199
		[CompilerGenerated]
		private static ColorPicked <>f__mg$cache10;

		// Token: 0x040023F0 RID: 9200
		[CompilerGenerated]
		private static Dragged <>f__mg$cache11;
	}
}
