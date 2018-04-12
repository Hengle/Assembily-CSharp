using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000766 RID: 1894
	public class MenuConfigurationGraphicsUI
	{
		// Token: 0x060035FD RID: 13821 RVA: 0x0016C6EC File Offset: 0x0016AAEC
		public MenuConfigurationGraphicsUI()
		{
			MenuConfigurationGraphicsUI.localization = Localization.read("/Menu/Configuration/MenuConfigurationGraphics.dat");
			MenuConfigurationGraphicsUI.container = new Sleek();
			MenuConfigurationGraphicsUI.container.positionOffset_X = 10;
			MenuConfigurationGraphicsUI.container.positionOffset_Y = 10;
			MenuConfigurationGraphicsUI.container.positionScale_Y = 1f;
			MenuConfigurationGraphicsUI.container.sizeOffset_X = -20;
			MenuConfigurationGraphicsUI.container.sizeOffset_Y = -20;
			MenuConfigurationGraphicsUI.container.sizeScale_X = 1f;
			MenuConfigurationGraphicsUI.container.sizeScale_Y = 1f;
			if (Provider.isConnected)
			{
				PlayerUI.container.add(MenuConfigurationGraphicsUI.container);
			}
			else
			{
				MenuUI.container.add(MenuConfigurationGraphicsUI.container);
			}
			MenuConfigurationGraphicsUI.active = false;
			MenuConfigurationGraphicsUI.graphicsBox = new SleekScrollBox();
			MenuConfigurationGraphicsUI.graphicsBox.positionOffset_X = -405;
			MenuConfigurationGraphicsUI.graphicsBox.positionOffset_Y = 100;
			MenuConfigurationGraphicsUI.graphicsBox.positionScale_X = 0.5f;
			MenuConfigurationGraphicsUI.graphicsBox.sizeOffset_X = 640;
			MenuConfigurationGraphicsUI.graphicsBox.sizeOffset_Y = -200;
			MenuConfigurationGraphicsUI.graphicsBox.sizeScale_Y = 1f;
			MenuConfigurationGraphicsUI.graphicsBox.area = new Rect(0f, 0f, 5f, 1510f);
			MenuConfigurationGraphicsUI.container.add(MenuConfigurationGraphicsUI.graphicsBox);
			MenuConfigurationGraphicsUI.landmarkButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.landmarkButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.landmarkButton.positionOffset_Y = 30;
			MenuConfigurationGraphicsUI.landmarkButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.landmarkButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.landmarkButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Landmark_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.landmarkButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Landmark_Button_Tooltip");
			SleekButtonState sleekButtonState = MenuConfigurationGraphicsUI.landmarkButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache0 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache0 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedLandmarkState);
			}
			sleekButtonState.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache0;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.landmarkButton);
			MenuConfigurationGraphicsUI.bloomToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.bloomToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.bloomToggle.positionOffset_Y = 100;
			MenuConfigurationGraphicsUI.bloomToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.bloomToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.bloomToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Bloom_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = MenuConfigurationGraphicsUI.bloomToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1 = new Toggled(MenuConfigurationGraphicsUI.onToggledBloomToggle);
			}
			sleekToggle.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache1;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.bloomToggle);
			MenuConfigurationGraphicsUI.chromaticAberrationToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.positionOffset_Y = 150;
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Chromatic_Aberration_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle2 = MenuConfigurationGraphicsUI.chromaticAberrationToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache2 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache2 = new Toggled(MenuConfigurationGraphicsUI.onToggledChromaticAberrationToggle);
			}
			sleekToggle2.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache2;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.chromaticAberrationToggle);
			MenuConfigurationGraphicsUI.filmGrainToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.filmGrainToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.filmGrainToggle.positionOffset_Y = 200;
			MenuConfigurationGraphicsUI.filmGrainToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.filmGrainToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.filmGrainToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Film_Grain_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle3 = MenuConfigurationGraphicsUI.filmGrainToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache3 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache3 = new Toggled(MenuConfigurationGraphicsUI.onToggledFilmGrainToggle);
			}
			sleekToggle3.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache3;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.filmGrainToggle);
			MenuConfigurationGraphicsUI.cloudsToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.cloudsToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.cloudsToggle.positionOffset_Y = 250;
			MenuConfigurationGraphicsUI.cloudsToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.cloudsToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.cloudsToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Clouds_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle4 = MenuConfigurationGraphicsUI.cloudsToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache4 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache4 = new Toggled(MenuConfigurationGraphicsUI.onToggledCloudsToggle);
			}
			sleekToggle4.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache4;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.cloudsToggle);
			MenuConfigurationGraphicsUI.blendToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.blendToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.blendToggle.positionOffset_Y = 300;
			MenuConfigurationGraphicsUI.blendToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.blendToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.blendToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Blend_Toggle_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.blendToggle.state = GraphicsSettings.blend;
			SleekToggle sleekToggle5 = MenuConfigurationGraphicsUI.blendToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache5 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache5 = new Toggled(MenuConfigurationGraphicsUI.onToggledBlendToggle);
			}
			sleekToggle5.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache5;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.blendToggle);
			MenuConfigurationGraphicsUI.fogToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.fogToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.fogToggle.positionOffset_Y = 350;
			MenuConfigurationGraphicsUI.fogToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.fogToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.fogToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Fog_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle6 = MenuConfigurationGraphicsUI.fogToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache6 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache6 = new Toggled(MenuConfigurationGraphicsUI.onToggledFogToggle);
			}
			sleekToggle6.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache6;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.fogToggle);
			MenuConfigurationGraphicsUI.grassDisplacementToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.grassDisplacementToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.grassDisplacementToggle.positionOffset_Y = 400;
			MenuConfigurationGraphicsUI.grassDisplacementToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.grassDisplacementToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.grassDisplacementToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Grass_Displacement_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle7 = MenuConfigurationGraphicsUI.grassDisplacementToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache7 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache7 = new Toggled(MenuConfigurationGraphicsUI.onToggledGrassDisplacementToggle);
			}
			sleekToggle7.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache7;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.grassDisplacementToggle);
			MenuConfigurationGraphicsUI.foliageFocusToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.foliageFocusToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.foliageFocusToggle.positionOffset_Y = 450;
			MenuConfigurationGraphicsUI.foliageFocusToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.foliageFocusToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.foliageFocusToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Foliage_Focus_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle8 = MenuConfigurationGraphicsUI.foliageFocusToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache8 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache8 = new Toggled(MenuConfigurationGraphicsUI.onToggledFoliageFocusToggle);
			}
			sleekToggle8.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache8;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.foliageFocusToggle);
			MenuConfigurationGraphicsUI.ragdollsToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.ragdollsToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.ragdollsToggle.positionOffset_Y = 500;
			MenuConfigurationGraphicsUI.ragdollsToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.ragdollsToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.ragdollsToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Ragdolls_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle9 = MenuConfigurationGraphicsUI.ragdollsToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache9 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache9 = new Toggled(MenuConfigurationGraphicsUI.onToggledRagdollsToggle);
			}
			sleekToggle9.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cache9;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.ragdollsToggle);
			MenuConfigurationGraphicsUI.debrisToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.debrisToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.debrisToggle.positionOffset_Y = 550;
			MenuConfigurationGraphicsUI.debrisToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.debrisToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.debrisToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Debris_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle10 = MenuConfigurationGraphicsUI.debrisToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheA == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheA = new Toggled(MenuConfigurationGraphicsUI.onToggledDebrisToggle);
			}
			sleekToggle10.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheA;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.debrisToggle);
			MenuConfigurationGraphicsUI.blastToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.blastToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.blastToggle.positionOffset_Y = 600;
			MenuConfigurationGraphicsUI.blastToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.blastToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.blastToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Blast_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle11 = MenuConfigurationGraphicsUI.blastToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheB == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheB = new Toggled(MenuConfigurationGraphicsUI.onToggledBlastToggle);
			}
			sleekToggle11.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheB;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.blastToggle);
			MenuConfigurationGraphicsUI.puddleToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.puddleToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.puddleToggle.positionOffset_Y = 650;
			MenuConfigurationGraphicsUI.puddleToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.puddleToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.puddleToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Puddle_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle12 = MenuConfigurationGraphicsUI.puddleToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheC == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheC = new Toggled(MenuConfigurationGraphicsUI.onToggledPuddleToggle);
			}
			sleekToggle12.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheC;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.puddleToggle);
			MenuConfigurationGraphicsUI.glitterToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.glitterToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.glitterToggle.positionOffset_Y = 700;
			MenuConfigurationGraphicsUI.glitterToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.glitterToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.glitterToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Glitter_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle13 = MenuConfigurationGraphicsUI.glitterToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheD == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheD = new Toggled(MenuConfigurationGraphicsUI.onToggledGlitterToggle);
			}
			sleekToggle13.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheD;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.glitterToggle);
			MenuConfigurationGraphicsUI.triplanarToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.triplanarToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.triplanarToggle.positionOffset_Y = 750;
			MenuConfigurationGraphicsUI.triplanarToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.triplanarToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.triplanarToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Triplanar_Toggle_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle14 = MenuConfigurationGraphicsUI.triplanarToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheE == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheE = new Toggled(MenuConfigurationGraphicsUI.onToggledTriplanarToggle);
			}
			sleekToggle14.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheE;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.triplanarToggle);
			MenuConfigurationGraphicsUI.skyboxReflectionToggle = new SleekToggle();
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.positionOffset_Y = 800;
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.sizeOffset_X = 40;
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.sizeOffset_Y = 40;
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.addLabel(MenuConfigurationGraphicsUI.localization.format("Skybox_Reflection_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle15 = MenuConfigurationGraphicsUI.skyboxReflectionToggle;
			if (MenuConfigurationGraphicsUI.<>f__mg$cacheF == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cacheF = new Toggled(MenuConfigurationGraphicsUI.onToggledSkyboxReflectionToggle);
			}
			sleekToggle15.onToggled = MenuConfigurationGraphicsUI.<>f__mg$cacheF;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.skyboxReflectionToggle);
			MenuConfigurationGraphicsUI.distanceSlider = new SleekSlider();
			MenuConfigurationGraphicsUI.distanceSlider.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.distanceSlider.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.distanceSlider.sizeOffset_Y = 20;
			MenuConfigurationGraphicsUI.distanceSlider.orientation = ESleekOrientation.HORIZONTAL;
			MenuConfigurationGraphicsUI.distanceSlider.addLabel(MenuConfigurationGraphicsUI.localization.format("Distance_Slider_Label", new object[]
			{
				25 + (int)(GraphicsSettings.distance * 75f)
			}), ESleekSide.RIGHT);
			SleekSlider sleekSlider = MenuConfigurationGraphicsUI.distanceSlider;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache10 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache10 = new Dragged(MenuConfigurationGraphicsUI.onDraggedDistanceSlider);
			}
			sleekSlider.onDragged = MenuConfigurationGraphicsUI.<>f__mg$cache10;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.distanceSlider);
			MenuConfigurationGraphicsUI.landmarkSlider = new SleekSlider();
			MenuConfigurationGraphicsUI.landmarkSlider.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.landmarkSlider.positionOffset_Y = 70;
			MenuConfigurationGraphicsUI.landmarkSlider.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.landmarkSlider.sizeOffset_Y = 20;
			MenuConfigurationGraphicsUI.landmarkSlider.orientation = ESleekOrientation.HORIZONTAL;
			MenuConfigurationGraphicsUI.landmarkSlider.addLabel(MenuConfigurationGraphicsUI.localization.format("Landmark_Slider_Label", new object[]
			{
				25 + (int)(GraphicsSettings.distance * 75f)
			}), ESleekSide.RIGHT);
			SleekSlider sleekSlider2 = MenuConfigurationGraphicsUI.landmarkSlider;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache11 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache11 = new Dragged(MenuConfigurationGraphicsUI.onDraggedLandmarkSlider);
			}
			sleekSlider2.onDragged = MenuConfigurationGraphicsUI.<>f__mg$cache11;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.landmarkSlider);
			MenuConfigurationGraphicsUI.landmarkSlider.isVisible = false;
			MenuConfigurationGraphicsUI.antiAliasingButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("FXAA")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("TAA"))
			});
			MenuConfigurationGraphicsUI.antiAliasingButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.antiAliasingButton.positionOffset_Y = 840;
			MenuConfigurationGraphicsUI.antiAliasingButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.antiAliasingButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.antiAliasingButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Anti_Aliasing_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.antiAliasingButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Anti_Aliasing_Button_Tooltip");
			SleekButtonState sleekButtonState2 = MenuConfigurationGraphicsUI.antiAliasingButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache12 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache12 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedAntiAliasingState);
			}
			sleekButtonState2.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache12;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.antiAliasingButton);
			MenuConfigurationGraphicsUI.anisotropicFilteringButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("AF_Disabled")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("AF_Per_Texture")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("AF_Forced_On"))
			});
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.positionOffset_Y = 880;
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Anisotropic_Filtering_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Anisotropic_Filtering_Button_Tooltip");
			SleekButtonState sleekButtonState3 = MenuConfigurationGraphicsUI.anisotropicFilteringButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache13 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache13 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedAnisotropicFilteringState);
			}
			sleekButtonState3.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache13;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.anisotropicFilteringButton);
			MenuConfigurationGraphicsUI.effectButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.effectButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.effectButton.positionOffset_Y = 920;
			MenuConfigurationGraphicsUI.effectButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.effectButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.effectButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Effect_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.effectButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Effect_Button_Tooltip");
			SleekButtonState sleekButtonState4 = MenuConfigurationGraphicsUI.effectButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache14 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache14 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedEffectState);
			}
			sleekButtonState4.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache14;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.effectButton);
			MenuConfigurationGraphicsUI.foliageButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.foliageButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.foliageButton.positionOffset_Y = 960;
			MenuConfigurationGraphicsUI.foliageButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.foliageButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.foliageButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Foliage_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.foliageButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Foliage_Button_Tooltip");
			SleekButtonState sleekButtonState5 = MenuConfigurationGraphicsUI.foliageButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache15 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache15 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedFoliageState);
			}
			sleekButtonState5.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache15;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.foliageButton);
			if (!SystemInfo.supportsInstancing)
			{
				MenuConfigurationGraphicsUI.foliageButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Warning_GPU_Instancing"), Color.red, ESleekSide.LEFT);
			}
			MenuConfigurationGraphicsUI.sunShaftsButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.sunShaftsButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.sunShaftsButton.positionOffset_Y = 1000;
			MenuConfigurationGraphicsUI.sunShaftsButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.sunShaftsButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.sunShaftsButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Sun_Shafts_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.sunShaftsButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Sun_Shafts_Button_Tooltip");
			SleekButtonState sleekButtonState6 = MenuConfigurationGraphicsUI.sunShaftsButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache16 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache16 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedSunShaftsState);
			}
			sleekButtonState6.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache16;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.sunShaftsButton);
			MenuConfigurationGraphicsUI.lightingButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.lightingButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.lightingButton.positionOffset_Y = 1040;
			MenuConfigurationGraphicsUI.lightingButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.lightingButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.lightingButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Lighting_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.lightingButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Lighting_Button_Tooltip");
			SleekButtonState sleekButtonState7 = MenuConfigurationGraphicsUI.lightingButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache17 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache17 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedLightingState);
			}
			sleekButtonState7.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache17;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.lightingButton);
			MenuConfigurationGraphicsUI.ambientOcclusionButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.ambientOcclusionButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.ambientOcclusionButton.positionOffset_Y = 1080;
			MenuConfigurationGraphicsUI.ambientOcclusionButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.ambientOcclusionButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.ambientOcclusionButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Ambient_Occlusion_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.ambientOcclusionButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Ambient_Occlusion_Button_Tooltip");
			SleekButtonState sleekButtonState8 = MenuConfigurationGraphicsUI.ambientOcclusionButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache18 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache18 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedAmbientOcclusionState);
			}
			sleekButtonState8.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache18;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.ambientOcclusionButton);
			MenuConfigurationGraphicsUI.reflectionButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.reflectionButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.reflectionButton.positionOffset_Y = 1120;
			MenuConfigurationGraphicsUI.reflectionButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.reflectionButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.reflectionButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Reflection_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.reflectionButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Reflection_Button_Tooltip");
			SleekButtonState sleekButtonState9 = MenuConfigurationGraphicsUI.reflectionButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache19 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache19 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedReflectionState);
			}
			sleekButtonState9.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache19;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.reflectionButton);
			MenuConfigurationGraphicsUI.planarReflectionButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.planarReflectionButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.planarReflectionButton.positionOffset_Y = 1160;
			MenuConfigurationGraphicsUI.planarReflectionButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.planarReflectionButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.planarReflectionButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Planar_Reflection_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.planarReflectionButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Planar_Reflection_Button_Tooltip");
			SleekButtonState sleekButtonState10 = MenuConfigurationGraphicsUI.planarReflectionButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1A == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1A = new SwappedState(MenuConfigurationGraphicsUI.onSwappedPlanarReflectionState);
			}
			sleekButtonState10.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1A;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.planarReflectionButton);
			MenuConfigurationGraphicsUI.waterButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.waterButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.waterButton.positionOffset_Y = 1200;
			MenuConfigurationGraphicsUI.waterButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.waterButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.waterButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Water_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.waterButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Water_Button_Tooltip");
			SleekButtonState sleekButtonState11 = MenuConfigurationGraphicsUI.waterButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1B == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1B = new SwappedState(MenuConfigurationGraphicsUI.onSwappedWaterState);
			}
			sleekButtonState11.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1B;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.waterButton);
			MenuConfigurationGraphicsUI.scopeButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.scopeButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.scopeButton.positionOffset_Y = 1240;
			MenuConfigurationGraphicsUI.scopeButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.scopeButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.scopeButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Scope_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.scopeButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Scope_Button_Tooltip");
			SleekButtonState sleekButtonState12 = MenuConfigurationGraphicsUI.scopeButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1C == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1C = new SwappedState(MenuConfigurationGraphicsUI.onSwappedScopeState);
			}
			sleekButtonState12.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1C;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.scopeButton);
			MenuConfigurationGraphicsUI.outlineButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.outlineButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.outlineButton.positionOffset_Y = 1280;
			MenuConfigurationGraphicsUI.outlineButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.outlineButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.outlineButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Outline_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.outlineButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Outline_Button_Tooltip");
			SleekButtonState sleekButtonState13 = MenuConfigurationGraphicsUI.outlineButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1D == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1D = new SwappedState(MenuConfigurationGraphicsUI.onSwappedOutlineState);
			}
			sleekButtonState13.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1D;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.outlineButton);
			MenuConfigurationGraphicsUI.boneButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.boneButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.boneButton.positionOffset_Y = 1320;
			MenuConfigurationGraphicsUI.boneButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.boneButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.boneButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Bone_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.boneButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Bone_Button_Tooltip");
			SleekButtonState sleekButtonState14 = MenuConfigurationGraphicsUI.boneButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1E == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1E = new SwappedState(MenuConfigurationGraphicsUI.onSwappedBoneState);
			}
			sleekButtonState14.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1E;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.boneButton);
			MenuConfigurationGraphicsUI.terrainButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.terrainButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.terrainButton.positionOffset_Y = 1360;
			MenuConfigurationGraphicsUI.terrainButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.terrainButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.terrainButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Terrain_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.terrainButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Terrain_Button_Tooltip");
			SleekButtonState sleekButtonState15 = MenuConfigurationGraphicsUI.terrainButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache1F == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache1F = new SwappedState(MenuConfigurationGraphicsUI.onSwappedTerrainState);
			}
			sleekButtonState15.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache1F;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.terrainButton);
			MenuConfigurationGraphicsUI.windButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Off")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Low")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Medium")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("High")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Ultra"))
			});
			MenuConfigurationGraphicsUI.windButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.windButton.positionOffset_Y = 1400;
			MenuConfigurationGraphicsUI.windButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.windButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.windButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Wind_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.windButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Wind_Button_Tooltip");
			SleekButtonState sleekButtonState16 = MenuConfigurationGraphicsUI.windButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache20 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache20 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedWindState);
			}
			sleekButtonState16.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache20;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.windButton);
			MenuConfigurationGraphicsUI.treeModeButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("TM_Legacy")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("TM_SpeedTree_Fade_None")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("TM_SpeedTree_Fade_SpeedTree"))
			});
			MenuConfigurationGraphicsUI.treeModeButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.treeModeButton.positionOffset_Y = 1440;
			MenuConfigurationGraphicsUI.treeModeButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.treeModeButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.treeModeButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Tree_Mode_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.treeModeButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Tree_Mode_Button_Tooltip");
			SleekButtonState sleekButtonState17 = MenuConfigurationGraphicsUI.treeModeButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache21 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache21 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedTreeModeState);
			}
			sleekButtonState17.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache21;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.treeModeButton);
			MenuConfigurationGraphicsUI.renderButton = new SleekButtonState(new GUIContent[]
			{
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Deferred")),
				new GUIContent(MenuConfigurationGraphicsUI.localization.format("Forward"))
			});
			MenuConfigurationGraphicsUI.renderButton.positionOffset_X = 205;
			MenuConfigurationGraphicsUI.renderButton.positionOffset_Y = 1480;
			MenuConfigurationGraphicsUI.renderButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.renderButton.sizeOffset_Y = 30;
			MenuConfigurationGraphicsUI.renderButton.addLabel(MenuConfigurationGraphicsUI.localization.format("Render_Mode_Button_Label"), ESleekSide.RIGHT);
			MenuConfigurationGraphicsUI.renderButton.tooltip = MenuConfigurationGraphicsUI.localization.format("Render_Mode_Button_Tooltip");
			SleekButtonState sleekButtonState18 = MenuConfigurationGraphicsUI.renderButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache22 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache22 = new SwappedState(MenuConfigurationGraphicsUI.onSwappedRenderState);
			}
			sleekButtonState18.onSwappedState = MenuConfigurationGraphicsUI.<>f__mg$cache22;
			MenuConfigurationGraphicsUI.graphicsBox.add(MenuConfigurationGraphicsUI.renderButton);
			MenuConfigurationGraphicsUI.backButton = new SleekButtonIcon((Texture2D)MenuDashboardUI.icons.load("Exit"));
			MenuConfigurationGraphicsUI.backButton.positionOffset_Y = -50;
			MenuConfigurationGraphicsUI.backButton.positionScale_Y = 1f;
			MenuConfigurationGraphicsUI.backButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.backButton.sizeOffset_Y = 50;
			MenuConfigurationGraphicsUI.backButton.text = MenuDashboardUI.localization.format("BackButtonText");
			MenuConfigurationGraphicsUI.backButton.tooltip = MenuDashboardUI.localization.format("BackButtonTooltip");
			SleekButton sleekButton = MenuConfigurationGraphicsUI.backButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache23 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache23 = new ClickedButton(MenuConfigurationGraphicsUI.onClickedBackButton);
			}
			sleekButton.onClickedButton = MenuConfigurationGraphicsUI.<>f__mg$cache23;
			MenuConfigurationGraphicsUI.backButton.fontSize = 14;
			MenuConfigurationGraphicsUI.backButton.iconImage.backgroundTint = ESleekTint.FOREGROUND;
			MenuConfigurationGraphicsUI.container.add(MenuConfigurationGraphicsUI.backButton);
			MenuConfigurationGraphicsUI.defaultButton = new SleekButton();
			MenuConfigurationGraphicsUI.defaultButton.positionOffset_X = -200;
			MenuConfigurationGraphicsUI.defaultButton.positionOffset_Y = -50;
			MenuConfigurationGraphicsUI.defaultButton.positionScale_X = 1f;
			MenuConfigurationGraphicsUI.defaultButton.positionScale_Y = 1f;
			MenuConfigurationGraphicsUI.defaultButton.sizeOffset_X = 200;
			MenuConfigurationGraphicsUI.defaultButton.sizeOffset_Y = 50;
			MenuConfigurationGraphicsUI.defaultButton.text = MenuPlayConfigUI.localization.format("Default");
			MenuConfigurationGraphicsUI.defaultButton.tooltip = MenuPlayConfigUI.localization.format("Default_Tooltip");
			SleekButton sleekButton2 = MenuConfigurationGraphicsUI.defaultButton;
			if (MenuConfigurationGraphicsUI.<>f__mg$cache24 == null)
			{
				MenuConfigurationGraphicsUI.<>f__mg$cache24 = new ClickedButton(MenuConfigurationGraphicsUI.onClickedDefaultButton);
			}
			sleekButton2.onClickedButton = MenuConfigurationGraphicsUI.<>f__mg$cache24;
			MenuConfigurationGraphicsUI.defaultButton.fontSize = 14;
			MenuConfigurationGraphicsUI.container.add(MenuConfigurationGraphicsUI.defaultButton);
			MenuConfigurationGraphicsUI.updateAll();
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x0016E70E File Offset: 0x0016CB0E
		public static void open()
		{
			if (MenuConfigurationGraphicsUI.active)
			{
				return;
			}
			MenuConfigurationGraphicsUI.active = true;
			MenuConfigurationGraphicsUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x0016E73B File Offset: 0x0016CB3B
		public static void close()
		{
			if (!MenuConfigurationGraphicsUI.active)
			{
				return;
			}
			MenuConfigurationGraphicsUI.active = false;
			MenuConfigurationGraphicsUI.container.lerpPositionScale(0f, 1f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x0016E768 File Offset: 0x0016CB68
		private static void onToggledBloomToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.bloom = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0016E775 File Offset: 0x0016CB75
		private static void onToggledChromaticAberrationToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.chromaticAberration = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x0016E782 File Offset: 0x0016CB82
		private static void onToggledFilmGrainToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.filmGrain = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0016E78F File Offset: 0x0016CB8F
		private static void onToggledCloudsToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.clouds = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0016E79C File Offset: 0x0016CB9C
		private static void onToggledBlendToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.blend = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0016E7A9 File Offset: 0x0016CBA9
		private static void onToggledFogToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.fog = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0016E7B6 File Offset: 0x0016CBB6
		private static void onToggledGrassDisplacementToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.grassDisplacement = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0016E7C3 File Offset: 0x0016CBC3
		private static void onToggledFoliageFocusToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.foliageFocus = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0016E7D0 File Offset: 0x0016CBD0
		private static void onSwappedLandmarkState(SleekButtonState button, int index)
		{
			GraphicsSettings.landmarkQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
			MenuConfigurationGraphicsUI.landmarkSlider.isVisible = (GraphicsSettings.landmarkQuality != EGraphicQuality.OFF);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0016E7F2 File Offset: 0x0016CBF2
		private static void onToggledRagdollsToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.ragdolls = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0016E7FF File Offset: 0x0016CBFF
		private static void onToggledDebrisToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.debris = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x0016E80C File Offset: 0x0016CC0C
		private static void onToggledBlastToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.blast = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0016E819 File Offset: 0x0016CC19
		private static void onToggledPuddleToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.puddle = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x0016E826 File Offset: 0x0016CC26
		private static void onToggledGlitterToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.glitter = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x0016E833 File Offset: 0x0016CC33
		private static void onToggledTriplanarToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.triplanar = state;
			GraphicsSettings.apply();
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x0016E840 File Offset: 0x0016CC40
		private static void onToggledSkyboxReflectionToggle(SleekToggle toggle, bool state)
		{
			GraphicsSettings.skyboxReflection = state;
			GraphicsSettings.apply();
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0016E850 File Offset: 0x0016CC50
		private static void onDraggedDistanceSlider(SleekSlider slider, float state)
		{
			GraphicsSettings.distance = state;
			GraphicsSettings.apply();
			MenuConfigurationGraphicsUI.distanceSlider.updateLabel(MenuConfigurationGraphicsUI.localization.format("Distance_Slider_Label", new object[]
			{
				25 + (int)(state * 75f)
			}));
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0016E89A File Offset: 0x0016CC9A
		private static void onDraggedLandmarkSlider(SleekSlider slider, float state)
		{
			GraphicsSettings.landmarkDistance = state;
			GraphicsSettings.apply();
			MenuConfigurationGraphicsUI.landmarkSlider.updateLabel(MenuConfigurationGraphicsUI.localization.format("Landmark_Slider_Label", new object[]
			{
				(int)(state * 100f)
			}));
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0016E8D6 File Offset: 0x0016CCD6
		private static void onSwappedAntiAliasingState(SleekButtonState button, int index)
		{
			GraphicsSettings.antiAliasingType = (EAntiAliasingType)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0016E8E3 File Offset: 0x0016CCE3
		private static void onSwappedAnisotropicFilteringState(SleekButtonState button, int index)
		{
			GraphicsSettings.anisotropicFilteringMode = (EAnisotropicFilteringMode)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0016E8F0 File Offset: 0x0016CCF0
		private static void onSwappedEffectState(SleekButtonState button, int index)
		{
			GraphicsSettings.effectQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0016E8FF File Offset: 0x0016CCFF
		private static void onSwappedFoliageState(SleekButtonState button, int index)
		{
			GraphicsSettings.foliageQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0016E90C File Offset: 0x0016CD0C
		private static void onSwappedSunShaftsState(SleekButtonState button, int index)
		{
			GraphicsSettings.sunShaftsQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0016E919 File Offset: 0x0016CD19
		private static void onSwappedLightingState(SleekButtonState button, int index)
		{
			GraphicsSettings.lightingQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0016E926 File Offset: 0x0016CD26
		private static void onSwappedAmbientOcclusionState(SleekButtonState button, int index)
		{
			GraphicsSettings.ambientOcclusionQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0016E933 File Offset: 0x0016CD33
		private static void onSwappedReflectionState(SleekButtonState button, int index)
		{
			GraphicsSettings.reflectionQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0016E940 File Offset: 0x0016CD40
		private static void onSwappedPlanarReflectionState(SleekButtonState button, int index)
		{
			GraphicsSettings.planarReflectionQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0016E94F File Offset: 0x0016CD4F
		private static void onSwappedWaterState(SleekButtonState button, int index)
		{
			GraphicsSettings.waterQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x0016E95E File Offset: 0x0016CD5E
		private static void onSwappedScopeState(SleekButtonState button, int index)
		{
			GraphicsSettings.scopeQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0016E96B File Offset: 0x0016CD6B
		private static void onSwappedOutlineState(SleekButtonState button, int index)
		{
			GraphicsSettings.outlineQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0016E97A File Offset: 0x0016CD7A
		private static void onSwappedBoneState(SleekButtonState button, int index)
		{
			GraphicsSettings.boneQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0016E989 File Offset: 0x0016CD89
		private static void onSwappedTerrainState(SleekButtonState button, int index)
		{
			GraphicsSettings.terrainQuality = index + EGraphicQuality.LOW;
			GraphicsSettings.apply();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x0016E998 File Offset: 0x0016CD98
		private static void onSwappedWindState(SleekButtonState button, int index)
		{
			GraphicsSettings.windQuality = (EGraphicQuality)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0016E9A5 File Offset: 0x0016CDA5
		private static void onSwappedTreeModeState(SleekButtonState button, int index)
		{
			GraphicsSettings.treeMode = (ETreeGraphicMode)index;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0016E9AD File Offset: 0x0016CDAD
		private static void onSwappedRenderState(SleekButtonState button, int index)
		{
			GraphicsSettings.renderMode = (ERenderMode)index;
			GraphicsSettings.apply();
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x0016E9BA File Offset: 0x0016CDBA
		private static void onClickedBackButton(SleekButton button)
		{
			if (Player.player != null)
			{
				PlayerPauseUI.open();
			}
			else
			{
				MenuConfigurationUI.open();
			}
			MenuConfigurationGraphicsUI.close();
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x0016E9E0 File Offset: 0x0016CDE0
		private static void onClickedDefaultButton(SleekButton button)
		{
			GraphicsSettings.restoreDefaults();
			MenuConfigurationGraphicsUI.updateAll();
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x0016E9EC File Offset: 0x0016CDEC
		private static void updateAll()
		{
			MenuConfigurationGraphicsUI.bloomToggle.state = GraphicsSettings.bloom;
			MenuConfigurationGraphicsUI.chromaticAberrationToggle.state = GraphicsSettings.chromaticAberration;
			MenuConfigurationGraphicsUI.filmGrainToggle.state = GraphicsSettings.filmGrain;
			MenuConfigurationGraphicsUI.cloudsToggle.state = GraphicsSettings.clouds;
			MenuConfigurationGraphicsUI.fogToggle.state = GraphicsSettings.fog;
			MenuConfigurationGraphicsUI.grassDisplacementToggle.state = GraphicsSettings.grassDisplacement;
			MenuConfigurationGraphicsUI.foliageFocusToggle.state = GraphicsSettings.foliageFocus;
			MenuConfigurationGraphicsUI.landmarkButton.state = (int)GraphicsSettings.landmarkQuality;
			MenuConfigurationGraphicsUI.ragdollsToggle.state = GraphicsSettings.ragdolls;
			MenuConfigurationGraphicsUI.debrisToggle.state = GraphicsSettings.debris;
			MenuConfigurationGraphicsUI.blastToggle.state = GraphicsSettings.blast;
			MenuConfigurationGraphicsUI.puddleToggle.state = GraphicsSettings.puddle;
			MenuConfigurationGraphicsUI.glitterToggle.state = GraphicsSettings.glitter;
			MenuConfigurationGraphicsUI.triplanarToggle.state = GraphicsSettings.triplanar;
			MenuConfigurationGraphicsUI.skyboxReflectionToggle.state = GraphicsSettings.skyboxReflection;
			MenuConfigurationGraphicsUI.distanceSlider.state = GraphicsSettings.distance;
			MenuConfigurationGraphicsUI.distanceSlider.updateLabel(MenuConfigurationGraphicsUI.localization.format("Distance_Slider_Label", new object[]
			{
				25 + (int)(GraphicsSettings.distance * 75f)
			}));
			MenuConfigurationGraphicsUI.landmarkSlider.state = GraphicsSettings.landmarkDistance;
			MenuConfigurationGraphicsUI.landmarkSlider.updateLabel(MenuConfigurationGraphicsUI.localization.format("Landmark_Slider_Label", new object[]
			{
				(int)(GraphicsSettings.landmarkDistance * 100f)
			}));
			MenuConfigurationGraphicsUI.landmarkSlider.isVisible = (GraphicsSettings.landmarkQuality != EGraphicQuality.OFF);
			MenuConfigurationGraphicsUI.antiAliasingButton.state = (int)GraphicsSettings.antiAliasingType;
			MenuConfigurationGraphicsUI.anisotropicFilteringButton.state = (int)GraphicsSettings.anisotropicFilteringMode;
			MenuConfigurationGraphicsUI.effectButton.state = GraphicsSettings.effectQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.foliageButton.state = (int)GraphicsSettings.foliageQuality;
			MenuConfigurationGraphicsUI.sunShaftsButton.state = (int)GraphicsSettings.sunShaftsQuality;
			MenuConfigurationGraphicsUI.lightingButton.state = (int)GraphicsSettings.lightingQuality;
			MenuConfigurationGraphicsUI.ambientOcclusionButton.state = (int)GraphicsSettings.ambientOcclusionQuality;
			MenuConfigurationGraphicsUI.reflectionButton.state = (int)GraphicsSettings.reflectionQuality;
			MenuConfigurationGraphicsUI.planarReflectionButton.state = GraphicsSettings.planarReflectionQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.waterButton.state = GraphicsSettings.waterQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.scopeButton.state = (int)GraphicsSettings.scopeQuality;
			MenuConfigurationGraphicsUI.outlineButton.state = GraphicsSettings.outlineQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.boneButton.state = GraphicsSettings.boneQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.terrainButton.state = GraphicsSettings.terrainQuality - EGraphicQuality.LOW;
			MenuConfigurationGraphicsUI.windButton.state = (int)GraphicsSettings.windQuality;
			MenuConfigurationGraphicsUI.treeModeButton.state = (int)GraphicsSettings.treeMode;
			MenuConfigurationGraphicsUI.renderButton.state = (int)GraphicsSettings.renderMode;
		}

		// Token: 0x0400261C RID: 9756
		private static Local localization;

		// Token: 0x0400261D RID: 9757
		private static Sleek container;

		// Token: 0x0400261E RID: 9758
		public static bool active;

		// Token: 0x0400261F RID: 9759
		private static SleekButtonIcon backButton;

		// Token: 0x04002620 RID: 9760
		private static SleekButton defaultButton;

		// Token: 0x04002621 RID: 9761
		private static SleekScrollBox graphicsBox;

		// Token: 0x04002622 RID: 9762
		private static SleekToggle motionBlurToggle;

		// Token: 0x04002623 RID: 9763
		private static SleekToggle bloomToggle;

		// Token: 0x04002624 RID: 9764
		private static SleekToggle chromaticAberrationToggle;

		// Token: 0x04002625 RID: 9765
		private static SleekToggle filmGrainToggle;

		// Token: 0x04002626 RID: 9766
		private static SleekToggle cloudsToggle;

		// Token: 0x04002627 RID: 9767
		private static SleekToggle blendToggle;

		// Token: 0x04002628 RID: 9768
		private static SleekToggle fogToggle;

		// Token: 0x04002629 RID: 9769
		private static SleekToggle grassDisplacementToggle;

		// Token: 0x0400262A RID: 9770
		private static SleekToggle foliageFocusToggle;

		// Token: 0x0400262B RID: 9771
		private static SleekToggle ragdollsToggle;

		// Token: 0x0400262C RID: 9772
		private static SleekToggle debrisToggle;

		// Token: 0x0400262D RID: 9773
		private static SleekToggle blastToggle;

		// Token: 0x0400262E RID: 9774
		private static SleekToggle puddleToggle;

		// Token: 0x0400262F RID: 9775
		private static SleekToggle glitterToggle;

		// Token: 0x04002630 RID: 9776
		private static SleekToggle triplanarToggle;

		// Token: 0x04002631 RID: 9777
		private static SleekToggle skyboxReflectionToggle;

		// Token: 0x04002632 RID: 9778
		private static SleekSlider distanceSlider;

		// Token: 0x04002633 RID: 9779
		private static SleekSlider landmarkSlider;

		// Token: 0x04002634 RID: 9780
		private static SleekButtonState landmarkButton;

		// Token: 0x04002635 RID: 9781
		public static SleekButtonState antiAliasingButton;

		// Token: 0x04002636 RID: 9782
		public static SleekButtonState anisotropicFilteringButton;

		// Token: 0x04002637 RID: 9783
		private static SleekButtonState effectButton;

		// Token: 0x04002638 RID: 9784
		private static SleekButtonState foliageButton;

		// Token: 0x04002639 RID: 9785
		private static SleekButtonState sunShaftsButton;

		// Token: 0x0400263A RID: 9786
		private static SleekButtonState lightingButton;

		// Token: 0x0400263B RID: 9787
		private static SleekButtonState ambientOcclusionButton;

		// Token: 0x0400263C RID: 9788
		private static SleekButtonState reflectionButton;

		// Token: 0x0400263D RID: 9789
		private static SleekButtonState planarReflectionButton;

		// Token: 0x0400263E RID: 9790
		private static SleekButtonState waterButton;

		// Token: 0x0400263F RID: 9791
		private static SleekButtonState scopeButton;

		// Token: 0x04002640 RID: 9792
		private static SleekButtonState outlineButton;

		// Token: 0x04002641 RID: 9793
		private static SleekButtonState boneButton;

		// Token: 0x04002642 RID: 9794
		private static SleekButtonState terrainButton;

		// Token: 0x04002643 RID: 9795
		private static SleekButtonState windButton;

		// Token: 0x04002644 RID: 9796
		private static SleekButtonState treeModeButton;

		// Token: 0x04002645 RID: 9797
		private static SleekButtonState renderButton;

		// Token: 0x04002646 RID: 9798
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache0;

		// Token: 0x04002647 RID: 9799
		[CompilerGenerated]
		private static Toggled <>f__mg$cache1;

		// Token: 0x04002648 RID: 9800
		[CompilerGenerated]
		private static Toggled <>f__mg$cache2;

		// Token: 0x04002649 RID: 9801
		[CompilerGenerated]
		private static Toggled <>f__mg$cache3;

		// Token: 0x0400264A RID: 9802
		[CompilerGenerated]
		private static Toggled <>f__mg$cache4;

		// Token: 0x0400264B RID: 9803
		[CompilerGenerated]
		private static Toggled <>f__mg$cache5;

		// Token: 0x0400264C RID: 9804
		[CompilerGenerated]
		private static Toggled <>f__mg$cache6;

		// Token: 0x0400264D RID: 9805
		[CompilerGenerated]
		private static Toggled <>f__mg$cache7;

		// Token: 0x0400264E RID: 9806
		[CompilerGenerated]
		private static Toggled <>f__mg$cache8;

		// Token: 0x0400264F RID: 9807
		[CompilerGenerated]
		private static Toggled <>f__mg$cache9;

		// Token: 0x04002650 RID: 9808
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheA;

		// Token: 0x04002651 RID: 9809
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheB;

		// Token: 0x04002652 RID: 9810
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheC;

		// Token: 0x04002653 RID: 9811
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheD;

		// Token: 0x04002654 RID: 9812
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheE;

		// Token: 0x04002655 RID: 9813
		[CompilerGenerated]
		private static Toggled <>f__mg$cacheF;

		// Token: 0x04002656 RID: 9814
		[CompilerGenerated]
		private static Dragged <>f__mg$cache10;

		// Token: 0x04002657 RID: 9815
		[CompilerGenerated]
		private static Dragged <>f__mg$cache11;

		// Token: 0x04002658 RID: 9816
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache12;

		// Token: 0x04002659 RID: 9817
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache13;

		// Token: 0x0400265A RID: 9818
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache14;

		// Token: 0x0400265B RID: 9819
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache15;

		// Token: 0x0400265C RID: 9820
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache16;

		// Token: 0x0400265D RID: 9821
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache17;

		// Token: 0x0400265E RID: 9822
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache18;

		// Token: 0x0400265F RID: 9823
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache19;

		// Token: 0x04002660 RID: 9824
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1A;

		// Token: 0x04002661 RID: 9825
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1B;

		// Token: 0x04002662 RID: 9826
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1C;

		// Token: 0x04002663 RID: 9827
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1D;

		// Token: 0x04002664 RID: 9828
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1E;

		// Token: 0x04002665 RID: 9829
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache1F;

		// Token: 0x04002666 RID: 9830
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache20;

		// Token: 0x04002667 RID: 9831
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache21;

		// Token: 0x04002668 RID: 9832
		[CompilerGenerated]
		private static SwappedState <>f__mg$cache22;

		// Token: 0x04002669 RID: 9833
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache23;

		// Token: 0x0400266A RID: 9834
		[CompilerGenerated]
		private static ClickedButton <>f__mg$cache24;
	}
}
