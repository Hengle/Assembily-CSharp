using System;
using System.Collections.Generic;
using HighlightingSystem;
using SDG.Framework.Debug;
using SDG.Framework.Foliage;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityStandardAssets.ImageEffects;

namespace SDG.Unturned
{
	// Token: 0x0200069B RID: 1691
	public class GraphicsSettings
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x00140826 File Offset: 0x0013EC26
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x0014082D File Offset: 0x0013EC2D
		[TerminalCommandProperty("gfx.uncap_landmarks", "unlimited landmark render distance", false)]
		public static bool uncapLandmarks
		{
			get
			{
				return GraphicsSettings._uncapLandmarks;
			}
			set
			{
				GraphicsSettings._uncapLandmarks = value;
				GraphicsSettings.apply();
				TerminalUtility.printCommandPass("Set uncap_landmarks to: " + GraphicsSettings.uncapLandmarks);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x00140853 File Offset: 0x0013EC53
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x0014085A File Offset: 0x0013EC5A
		[TerminalCommandProperty("ui.text_scale", "UI text size multiplier", 1f)]
		public static float uiTextScale
		{
			get
			{
				return GraphicsSettings._uiTextScale;
			}
			set
			{
				GraphicsSettings._uiTextScale = value;
				GraphicsSettings.apply();
				TerminalUtility.printCommandPass("Set ui_text_scale to: " + GraphicsSettings.uiTextScale);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x00140880 File Offset: 0x0013EC80
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x00140887 File Offset: 0x0013EC87
		[TerminalCommandProperty("ui.layout_scale", "UI layout spacing multiplier", 1f)]
		public static float uiLayoutScale
		{
			get
			{
				return GraphicsSettings._uiLayoutScale;
			}
			set
			{
				GraphicsSettings._uiLayoutScale = value;
				GraphicsSettings.changeResolution = true;
				GraphicsSettings.apply();
				TerminalUtility.printCommandPass("Set ui_layout_scale to: " + GraphicsSettings.uiLayoutScale);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x001408B3 File Offset: 0x0013ECB3
		public static PostProcessingProfile mainProfile
		{
			get
			{
				if (GraphicsSettings._mainProfile == null)
				{
					GraphicsSettings._mainProfile = (PostProcessingProfile)Resources.Load("Profiles/Profile_Main");
				}
				return GraphicsSettings._mainProfile;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x001408DE File Offset: 0x0013ECDE
		public static PostProcessingProfile viewProfile
		{
			get
			{
				if (GraphicsSettings._viewProfile == null)
				{
					GraphicsSettings._viewProfile = (PostProcessingProfile)Resources.Load("Profiles/Profile_View");
				}
				return GraphicsSettings._viewProfile;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x0014090C File Offset: 0x0013ED0C
		public static float effect
		{
			get
			{
				if (GraphicsSettings.effectQuality == EGraphicQuality.ULTRA)
				{
					return UnityEngine.Random.Range(GraphicsSettings.EFFECT_ULTRA - 16f, GraphicsSettings.EFFECT_ULTRA + 16f);
				}
				if (GraphicsSettings.effectQuality == EGraphicQuality.HIGH)
				{
					return UnityEngine.Random.Range(GraphicsSettings.EFFECT_HIGH - 8f, GraphicsSettings.EFFECT_HIGH + 8f);
				}
				if (GraphicsSettings.effectQuality == EGraphicQuality.MEDIUM)
				{
					return UnityEngine.Random.Range(GraphicsSettings.EFFECT_MEDIUM - 4f, GraphicsSettings.EFFECT_MEDIUM + 4f);
				}
				if (GraphicsSettings.effectQuality == EGraphicQuality.LOW)
				{
					return UnityEngine.Random.Range(GraphicsSettings.EFFECT_LOW - 2f, GraphicsSettings.EFFECT_LOW + 2f);
				}
				return 0f;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x001409BA File Offset: 0x0013EDBA
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x001409C6 File Offset: 0x0013EDC6
		public static GraphicsSettingsResolution resolution
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.Resolution;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.Resolution = value;
				GraphicsSettings.changeResolution = true;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x001409D9 File Offset: 0x0013EDD9
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x001409E5 File Offset: 0x0013EDE5
		public static bool fullscreen
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsFullscreenEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsFullscreenEnabled = value;
				GraphicsSettings.changeResolution = true;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x001409F8 File Offset: 0x0013EDF8
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x00140A04 File Offset: 0x0013EE04
		public static bool buffer
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsVSyncEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsVSyncEnabled = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x00140A11 File Offset: 0x0013EE11
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x00140A1D File Offset: 0x0013EE1D
		public static EAntiAliasingType antiAliasingType
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.AntiAliasingType5;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.AntiAliasingType5 = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x00140A2A File Offset: 0x0013EE2A
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x00140A36 File Offset: 0x0013EE36
		public static EAnisotropicFilteringMode anisotropicFilteringMode
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.AnisotropicFilteringMode;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.AnisotropicFilteringMode = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x00140A43 File Offset: 0x0013EE43
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x00140A4F File Offset: 0x0013EE4F
		public static bool bloom
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsBloomEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsBloomEnabled = value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x00140A5C File Offset: 0x0013EE5C
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x00140A68 File Offset: 0x0013EE68
		public static bool chromaticAberration
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsChromaticAberrationEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsChromaticAberrationEnabled = value;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00140A75 File Offset: 0x0013EE75
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x00140A81 File Offset: 0x0013EE81
		public static bool filmGrain
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsFilmGrainEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsFilmGrainEnabled = value;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00140A8E File Offset: 0x0013EE8E
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x00140AA6 File Offset: 0x0013EEA6
		public static bool clouds
		{
			get
			{
				return !Level.isVR && GraphicsSettings.graphicsSettingsData.IsCloudEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsCloudEnabled = value;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00140AB3 File Offset: 0x0013EEB3
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x00140ABF File Offset: 0x0013EEBF
		public static bool blend
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsNiceBlendEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsNiceBlendEnabled = value;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00140ACC File Offset: 0x0013EECC
		// (set) Token: 0x06003139 RID: 12601 RVA: 0x00140AD8 File Offset: 0x0013EED8
		public static bool fog
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsFogEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsFogEnabled = value;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x00140AE5 File Offset: 0x0013EEE5
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x00140AF1 File Offset: 0x0013EEF1
		public static bool grassDisplacement
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsGrassDisplacementEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsGrassDisplacementEnabled = value;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x00140AFE File Offset: 0x0013EEFE
		// (set) Token: 0x0600313D RID: 12605 RVA: 0x00140B0A File Offset: 0x0013EF0A
		public static bool foliageFocus
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsFoliageFocusEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsFoliageFocusEnabled = value;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x00140B17 File Offset: 0x0013EF17
		// (set) Token: 0x0600313F RID: 12607 RVA: 0x00140B23 File Offset: 0x0013EF23
		public static EGraphicQuality landmarkQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.LandmarkQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.LandmarkQuality = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x00140B30 File Offset: 0x0013EF30
		// (set) Token: 0x06003141 RID: 12609 RVA: 0x00140B3C File Offset: 0x0013EF3C
		public static bool ragdolls
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsRagdollsEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsRagdollsEnabled = value;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x00140B49 File Offset: 0x0013EF49
		// (set) Token: 0x06003143 RID: 12611 RVA: 0x00140B55 File Offset: 0x0013EF55
		public static bool debris
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsDebrisEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsDebrisEnabled = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x00140B62 File Offset: 0x0013EF62
		// (set) Token: 0x06003145 RID: 12613 RVA: 0x00140B6E File Offset: 0x0013EF6E
		public static bool blast
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsBlastEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsBlastEnabled = value;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x00140B7B File Offset: 0x0013EF7B
		// (set) Token: 0x06003147 RID: 12615 RVA: 0x00140B87 File Offset: 0x0013EF87
		public static bool puddle
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsPuddleEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsPuddleEnabled = value;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x00140B94 File Offset: 0x0013EF94
		// (set) Token: 0x06003149 RID: 12617 RVA: 0x00140BA0 File Offset: 0x0013EFA0
		public static bool glitter
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsGlitterEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsGlitterEnabled = value;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x00140BAD File Offset: 0x0013EFAD
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x00140BB9 File Offset: 0x0013EFB9
		public static bool triplanar
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsTriplanarMappingEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsTriplanarMappingEnabled = value;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x00140BC6 File Offset: 0x0013EFC6
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x00140BD2 File Offset: 0x0013EFD2
		public static bool skyboxReflection
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.IsSkyboxReflectionEnabled;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.IsSkyboxReflectionEnabled = value;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x00140BDF File Offset: 0x0013EFDF
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x00140BEB File Offset: 0x0013EFEB
		public static float distance
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.DrawDistance;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.DrawDistance = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x00140BF8 File Offset: 0x0013EFF8
		// (set) Token: 0x06003151 RID: 12625 RVA: 0x00140C04 File Offset: 0x0013F004
		public static float landmarkDistance
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.LandmarkDistance;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.LandmarkDistance = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x00140C11 File Offset: 0x0013F011
		// (set) Token: 0x06003153 RID: 12627 RVA: 0x00140C1D File Offset: 0x0013F01D
		public static EGraphicQuality effectQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.EffectQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.EffectQuality = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x00140C2A File Offset: 0x0013F02A
		// (set) Token: 0x06003155 RID: 12629 RVA: 0x00140C42 File Offset: 0x0013F042
		public static EGraphicQuality foliageQuality
		{
			get
			{
				if (Level.isVR)
				{
					return EGraphicQuality.OFF;
				}
				return GraphicsSettings.graphicsSettingsData.FoliageQuality2;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.FoliageQuality2 = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x00140C4F File Offset: 0x0013F04F
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x00140C5B File Offset: 0x0013F05B
		public static EGraphicQuality sunShaftsQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.SunShaftsQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.SunShaftsQuality = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x00140C68 File Offset: 0x0013F068
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x00140C90 File Offset: 0x0013F090
		public static EGraphicQuality lightingQuality
		{
			get
			{
				if (Level.isVR && GraphicsSettings.graphicsSettingsData.LightingQuality == EGraphicQuality.ULTRA)
				{
					return EGraphicQuality.HIGH;
				}
				return GraphicsSettings.graphicsSettingsData.LightingQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.LightingQuality = value;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x00140C9D File Offset: 0x0013F09D
		// (set) Token: 0x0600315B RID: 12635 RVA: 0x00140CA9 File Offset: 0x0013F0A9
		public static EGraphicQuality ambientOcclusionQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.ScreenSpaceAmbientOcclusionQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.ScreenSpaceAmbientOcclusionQuality = value;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x00140CB6 File Offset: 0x0013F0B6
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x00140CC2 File Offset: 0x0013F0C2
		public static EGraphicQuality reflectionQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.ScreenSpaceReflectionQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.ScreenSpaceReflectionQuality = value;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x00140CCF File Offset: 0x0013F0CF
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x00140CDB File Offset: 0x0013F0DB
		public static EGraphicQuality planarReflectionQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.PlanarReflectionQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.PlanarReflectionQuality = value;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x00140CE8 File Offset: 0x0013F0E8
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x00140D10 File Offset: 0x0013F110
		public static EGraphicQuality waterQuality
		{
			get
			{
				if (Level.isVR && GraphicsSettings.graphicsSettingsData.WaterQuality == EGraphicQuality.ULTRA)
				{
					return EGraphicQuality.HIGH;
				}
				return GraphicsSettings.graphicsSettingsData.WaterQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.WaterQuality = value;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x00140D1D File Offset: 0x0013F11D
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x00140D29 File Offset: 0x0013F129
		public static EGraphicQuality scopeQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.ScopeQuality2;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.ScopeQuality2 = value;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x00140D36 File Offset: 0x0013F136
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x00140D42 File Offset: 0x0013F142
		public static EGraphicQuality outlineQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.OutlineQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.OutlineQuality = value;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x00140D4F File Offset: 0x0013F14F
		// (set) Token: 0x06003167 RID: 12647 RVA: 0x00140D5B File Offset: 0x0013F15B
		public static EGraphicQuality boneQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.BoneQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.BoneQuality = value;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x00140D68 File Offset: 0x0013F168
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x00140D74 File Offset: 0x0013F174
		public static EGraphicQuality terrainQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.TerrainQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.TerrainQuality = value;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x00140D81 File Offset: 0x0013F181
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x00140D8D File Offset: 0x0013F18D
		public static EGraphicQuality windQuality
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.WindQuality;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.WindQuality = value;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x00140D9A File Offset: 0x0013F19A
		// (set) Token: 0x0600316D RID: 12653 RVA: 0x00140DA6 File Offset: 0x0013F1A6
		public static ETreeGraphicMode treeMode
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.TreeMode;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.TreeMode = value;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x00140DB3 File Offset: 0x0013F1B3
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x00140DBF File Offset: 0x0013F1BF
		public static ERenderMode renderMode
		{
			get
			{
				return GraphicsSettings.graphicsSettingsData.RenderMode2;
			}
			set
			{
				GraphicsSettings.graphicsSettingsData.RenderMode2 = value;
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06003170 RID: 12656 RVA: 0x00140DCC File Offset: 0x0013F1CC
		// (remove) Token: 0x06003171 RID: 12657 RVA: 0x00140E00 File Offset: 0x0013F200
		public static event GraphicsSettingsApplied graphicsSettingsApplied;

		// Token: 0x06003172 RID: 12658 RVA: 0x00140E34 File Offset: 0x0013F234
		public static void resize()
		{
			if (Application.isEditor)
			{
				return;
			}
			float num = (float)GraphicsSettings.resolution.Width / (float)GraphicsSettings.resolution.Height;
			if (num - 0.01f > GraphicsSettings.MAX_ASPECT_RATIO)
			{
				GraphicsSettings.resolution.Width = (int)((float)GraphicsSettings.resolution.Height * GraphicsSettings.MAX_ASPECT_RATIO);
			}
			if (GraphicsSettings.resolution.Width < 640 || GraphicsSettings.resolution.Height < 480)
			{
				GraphicsSettings.resolution = new GraphicsSettingsResolution(Screen.resolutions[Screen.resolutions.Length - 1]);
			}
			else if (GraphicsSettings.resolution.Width < Screen.resolutions[0].width || GraphicsSettings.resolution.Height < Screen.resolutions[0].height)
			{
				GraphicsSettings.resolution = new GraphicsSettingsResolution(Screen.resolutions[0]);
			}
			else if (GraphicsSettings.resolution.Width > Screen.resolutions[Screen.resolutions.Length - 1].width || GraphicsSettings.resolution.Height > Screen.resolutions[Screen.resolutions.Length - 1].height)
			{
				GraphicsSettings.resolution = new GraphicsSettingsResolution(Screen.resolutions[0]);
			}
			Screen.SetResolution(GraphicsSettings.resolution.Width, GraphicsSettings.resolution.Height, GraphicsSettings.fullscreen);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00140FC4 File Offset: 0x0013F3C4
		public static void apply()
		{
			if (GraphicsSettings.changeResolution)
			{
				GraphicsSettings.changeResolution = false;
				if (!Application.isEditor)
				{
					if (Provider.isConnected)
					{
						PlayerUI.rebuild();
					}
					else
					{
						MenuUI.rebuild();
					}
				}
			}
			if (LevelLighting.sun != null)
			{
				if (GraphicsSettings.lightingQuality == EGraphicQuality.ULTRA || GraphicsSettings.lightingQuality == EGraphicQuality.HIGH)
				{
					LevelLighting.sun.GetComponent<Light>().shadowNormalBias = 0f;
				}
				else
				{
					LevelLighting.sun.GetComponent<Light>().shadowNormalBias = 0.5f;
				}
			}
			QualitySettings.SetQualityLevel((int)((byte)GraphicsSettings.lightingQuality + 1), true);
			QualitySettings.vSyncCount = ((!GraphicsSettings.buffer) ? 0 : 1);
			EAnisotropicFilteringMode anisotropicFilteringMode = GraphicsSettings.anisotropicFilteringMode;
			if (anisotropicFilteringMode != EAnisotropicFilteringMode.DISABLED)
			{
				if (anisotropicFilteringMode != EAnisotropicFilteringMode.PER_TEXTURE)
				{
					if (anisotropicFilteringMode == EAnisotropicFilteringMode.FORCED_ON)
					{
						QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
					}
				}
				else
				{
					QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
				}
			}
			else
			{
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			}
			float[] array = new float[32];
			array[LayerMasks.DEFAULT] = 0f;
			array[LayerMasks.TRANSPARENT_FX] = 0f;
			array[LayerMasks.IGNORE_RAYCAST] = 0f;
			array[3] = 0f;
			array[LayerMasks.WATER] = 4096f;
			array[LayerMasks.UI] = 0f;
			array[6] = 0f;
			array[7] = 0f;
			array[LayerMasks.LOGIC] = ((!Level.isEditor) ? 0f : (256f + GraphicsSettings.distance * 256f));
			array[LayerMasks.PLAYER] = 0f;
			array[LayerMasks.ENEMY] = 512f;
			array[LayerMasks.VIEWMODEL] = 0f;
			array[LayerMasks.DEBRIS] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.ITEM] = 32f + GraphicsSettings.distance * 32f;
			array[LayerMasks.RESOURCE] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.LARGE] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.MEDIUM] = 128f + GraphicsSettings.distance * 128f;
			array[LayerMasks.SMALL] = 32f + GraphicsSettings.distance * 32f;
			array[LayerMasks.SKY] = 16384f;
			array[LayerMasks.ENVIRONMENT] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.GROUND] = 4096f;
			array[LayerMasks.CLIP] = 0f;
			array[LayerMasks.NAVMESH] = ((!Level.isEditor) ? 0f : (256f + GraphicsSettings.distance * 256f));
			array[LayerMasks.ENTITY] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.AGENT] = 0f;
			array[LayerMasks.LADDER] = 0f;
			array[LayerMasks.VEHICLE] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.BARRICADE] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.STRUCTURE] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.TIRE] = 0f;
			array[LayerMasks.TRAP] = 256f + GraphicsSettings.distance * 256f;
			array[LayerMasks.GROUND2] = 4096f;
			if (GraphicsSettings.landmarkQuality >= EGraphicQuality.LOW)
			{
				if (GraphicsSettings.uncapLandmarks)
				{
					array[LayerMasks.LARGE] = 4096f;
				}
				else
				{
					array[LayerMasks.LARGE] += GraphicsSettings.landmarkDistance * 1536f;
				}
			}
			if (GraphicsSettings.landmarkQuality >= EGraphicQuality.MEDIUM)
			{
				if (GraphicsSettings.uncapLandmarks)
				{
					array[LayerMasks.RESOURCE] = 4096f;
				}
				else
				{
					array[LayerMasks.RESOURCE] += GraphicsSettings.landmarkDistance * 1536f;
				}
			}
			if (GraphicsSettings.landmarkQuality >= EGraphicQuality.ULTRA)
			{
				if (GraphicsSettings.uncapLandmarks)
				{
					array[LayerMasks.ENVIRONMENT] = 4096f;
				}
				else
				{
					array[LayerMasks.ENVIRONMENT] += GraphicsSettings.landmarkDistance * 1536f;
				}
			}
			if (!LevelObjects.shouldInstantlyLoad && !LevelGround.shouldInstantlyLoad)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						if (LevelObjects.regions != null && !LevelObjects.regions[(int)b, (int)b2])
						{
							List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								LevelObject levelObject = list[i];
								if (levelObject != null)
								{
									if (levelObject.isLandmarkQualityMet)
									{
										levelObject.enableSkybox();
									}
									else
									{
										levelObject.disableSkybox();
									}
								}
							}
						}
						if (LevelGround.regions != null && !LevelGround.regions[(int)b, (int)b2])
						{
							List<ResourceSpawnpoint> list2 = LevelGround.trees[(int)b, (int)b2];
							for (int j = 0; j < list2.Count; j++)
							{
								ResourceSpawnpoint resourceSpawnpoint = list2[j];
								if (resourceSpawnpoint != null)
								{
									if (GraphicsSettings.landmarkQuality >= EGraphicQuality.MEDIUM)
									{
										resourceSpawnpoint.enableSkybox();
									}
									else
									{
										resourceSpawnpoint.disableSkybox();
									}
								}
							}
						}
					}
				}
			}
			QualitySettings.lodBias = 1f + GraphicsSettings.distance * 3f;
			switch (GraphicsSettings.boneQuality)
			{
			case EGraphicQuality.LOW:
				QualitySettings.blendWeights = BlendWeights.OneBone;
				break;
			case EGraphicQuality.MEDIUM:
				QualitySettings.blendWeights = BlendWeights.TwoBones;
				break;
			case EGraphicQuality.HIGH:
				QualitySettings.blendWeights = BlendWeights.FourBones;
				break;
			default:
				QualitySettings.blendWeights = BlendWeights.OneBone;
				break;
			}
			if (MainCamera.instance != null)
			{
				MainCamera.instance.renderingPath = ((GraphicsSettings.renderMode != ERenderMode.DEFERRED) ? RenderingPath.Forward : RenderingPath.DeferredShading);
				MainCamera.instance.hdr = (GraphicsSettings.renderMode == ERenderMode.DEFERRED);
				GlobalFog2 component = MainCamera.instance.GetComponent<GlobalFog2>();
				if (component != null)
				{
					component.enabled = (GraphicsSettings.renderMode == ERenderMode.DEFERRED);
				}
				AntialiasingModel.Settings settings = GraphicsSettings.mainProfile.antialiasing.settings;
				EAntiAliasingType antiAliasingType = GraphicsSettings.antiAliasingType;
				if (antiAliasingType != EAntiAliasingType.FXAA)
				{
					if (antiAliasingType != EAntiAliasingType.TAA)
					{
						GraphicsSettings.mainProfile.antialiasing.enabled = false;
						QualitySettings.antiAliasing = 0;
					}
					else
					{
						GraphicsSettings.mainProfile.antialiasing.enabled = true;
						settings.method = AntialiasingModel.Method.Taa;
						QualitySettings.antiAliasing = ((GraphicsSettings.renderMode != ERenderMode.DEFERRED) ? 0 : 8);
					}
				}
				else
				{
					GraphicsSettings.mainProfile.antialiasing.enabled = true;
					settings.method = AntialiasingModel.Method.Fxaa;
					QualitySettings.antiAliasing = ((GraphicsSettings.renderMode != ERenderMode.DEFERRED) ? 0 : 4);
				}
				GraphicsSettings.mainProfile.antialiasing.settings = settings;
				AmbientOcclusionModel.Settings settings2 = GraphicsSettings.mainProfile.ambientOcclusion.settings;
				GraphicsSettings.mainProfile.ambientOcclusion.enabled = (GraphicsSettings.renderMode == ERenderMode.DEFERRED && GraphicsSettings.ambientOcclusionQuality != EGraphicQuality.OFF);
				switch (GraphicsSettings.ambientOcclusionQuality)
				{
				case EGraphicQuality.LOW:
					settings2.sampleCount = AmbientOcclusionModel.SampleCount.Lowest;
					break;
				case EGraphicQuality.MEDIUM:
					settings2.sampleCount = AmbientOcclusionModel.SampleCount.Low;
					break;
				case EGraphicQuality.HIGH:
					settings2.sampleCount = AmbientOcclusionModel.SampleCount.Medium;
					break;
				case EGraphicQuality.ULTRA:
					settings2.sampleCount = AmbientOcclusionModel.SampleCount.High;
					break;
				}
				GraphicsSettings.mainProfile.ambientOcclusion.settings = settings2;
				ScreenSpaceReflectionModel.Settings settings3 = GraphicsSettings.mainProfile.screenSpaceReflection.settings;
				GraphicsSettings.mainProfile.screenSpaceReflection.enabled = (GraphicsSettings.reflectionQuality != EGraphicQuality.OFF);
				switch (GraphicsSettings.reflectionQuality)
				{
				case EGraphicQuality.LOW:
					settings3.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low;
					settings3.reflection.iterationCount = 100;
					settings3.reflection.stepSize = 4;
					break;
				case EGraphicQuality.MEDIUM:
					settings3.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low;
					settings3.reflection.iterationCount = 150;
					settings3.reflection.stepSize = 3;
					break;
				case EGraphicQuality.HIGH:
					settings3.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.High;
					settings3.reflection.iterationCount = 250;
					settings3.reflection.stepSize = 2;
					break;
				case EGraphicQuality.ULTRA:
					settings3.reflection.reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.High;
					settings3.reflection.iterationCount = 600;
					settings3.reflection.stepSize = 1;
					break;
				}
				GraphicsSettings.mainProfile.screenSpaceReflection.settings = settings3;
				GraphicsSettings.mainProfile.bloom.enabled = GraphicsSettings.bloom;
				BloomModel.Settings settings4 = GraphicsSettings.mainProfile.bloom.settings;
				settings4.lensDirt.intensity = (float)((!Provider.preferenceData.Graphics.Use_Lens_Dirt) ? 0 : 3);
				GraphicsSettings.mainProfile.bloom.settings = settings4;
				SunShafts component2 = MainCamera.instance.GetComponent<SunShafts>();
				if (component2 != null)
				{
					if (GraphicsSettings.sunShaftsQuality == EGraphicQuality.LOW)
					{
						component2.resolution = SunShaftsResolution.Low;
					}
					else if (GraphicsSettings.sunShaftsQuality == EGraphicQuality.MEDIUM)
					{
						component2.resolution = SunShaftsResolution.Normal;
					}
					else if (GraphicsSettings.sunShaftsQuality == EGraphicQuality.HIGH)
					{
						component2.resolution = SunShaftsResolution.High;
					}
					if (LevelLighting.areFXAllowed)
					{
						component2.enabled = (GraphicsSettings.sunShaftsQuality != EGraphicQuality.OFF);
					}
				}
				if (LevelLighting.areFXAllowed)
				{
					GlobalFog component3 = MainCamera.instance.GetComponent<GlobalFog>();
					if (component3 != null)
					{
						component3.enabled = GraphicsSettings.fog;
					}
				}
				HighlightingRenderer component4 = MainCamera.instance.GetComponent<HighlightingRenderer>();
				if (component4 != null)
				{
					if (Level.isDevkit)
					{
						component4.downsampleFactor = 1;
						component4.iterations = 1;
						component4.blurMinSpread = 1f;
						component4.blurSpread = 1f;
						component4.blurIntensity = 1f;
					}
					else if (GraphicsSettings.outlineQuality == EGraphicQuality.LOW)
					{
						component4.downsampleFactor = 4;
						component4.iterations = 1;
						component4.blurMinSpread = 0.75f;
						component4.blurSpread = 0f;
						component4.blurIntensity = 0.25f;
					}
					else if (GraphicsSettings.outlineQuality == EGraphicQuality.MEDIUM)
					{
						component4.downsampleFactor = 4;
						component4.iterations = 2;
						component4.blurMinSpread = 0.5f;
						component4.blurSpread = 0.25f;
						component4.blurIntensity = 0.25f;
					}
					else if (GraphicsSettings.outlineQuality == EGraphicQuality.HIGH)
					{
						component4.downsampleFactor = 2;
						component4.iterations = 2;
						component4.blurMinSpread = 1f;
						component4.blurSpread = 0.5f;
						component4.blurIntensity = 0.25f;
					}
					else if (GraphicsSettings.outlineQuality == EGraphicQuality.ULTRA)
					{
						component4.downsampleFactor = 1;
						component4.iterations = 3;
						component4.blurMinSpread = 0.5f;
						component4.blurSpread = 0.5f;
						component4.blurIntensity = 0.25f;
					}
				}
				MainCamera.instance.layerCullDistances = array;
				MainCamera.instance.layerCullSpherical = true;
				if (Player.player != null)
				{
					Player.player.look.scopeCamera.layerCullDistances = array;
					Player.player.look.scopeCamera.layerCullSpherical = true;
					Player.player.look.scopeCamera.depthTextureMode = DepthTextureMode.Depth;
					Player.player.look.updateScope(GraphicsSettings.scopeQuality);
					Player.player.look.scopeCamera.GetComponent<GlobalFog>().enabled = GraphicsSettings.fog;
					Player.player.look.scopeCamera.renderingPath = ((GraphicsSettings.renderMode != ERenderMode.DEFERRED) ? RenderingPath.Forward : RenderingPath.DeferredShading);
					Player.player.look.scopeCamera.hdr = (GraphicsSettings.renderMode == ERenderMode.DEFERRED);
					component = Player.player.look.scopeCamera.GetComponent<GlobalFog2>();
					if (component != null)
					{
						component.enabled = (GraphicsSettings.renderMode == ERenderMode.DEFERRED);
					}
					if (LevelLighting.areFXAllowed)
					{
						GlobalFog component5 = Player.player.look.scopeCamera.GetComponent<GlobalFog>();
						if (component5 != null)
						{
							component5.enabled = GraphicsSettings.fog;
						}
					}
					GraphicsSettings.mainProfile.chromaticAberration.enabled = (Player.player.look.perspective == EPlayerPerspective.THIRD && GraphicsSettings.chromaticAberration);
					GraphicsSettings.mainProfile.grain.enabled = (Player.player.look.perspective == EPlayerPerspective.THIRD && GraphicsSettings.filmGrain);
					GraphicsSettings.viewProfile.chromaticAberration.enabled = (Player.player.look.perspective == EPlayerPerspective.FIRST && GraphicsSettings.chromaticAberration);
					GraphicsSettings.viewProfile.grain.enabled = (Player.player.look.perspective == EPlayerPerspective.FIRST && GraphicsSettings.filmGrain);
				}
				else
				{
					GraphicsSettings.mainProfile.chromaticAberration.enabled = GraphicsSettings.chromaticAberration;
					GraphicsSettings.mainProfile.grain.enabled = GraphicsSettings.filmGrain;
					GraphicsSettings.viewProfile.chromaticAberration.enabled = false;
					GraphicsSettings.viewProfile.grain.enabled = false;
				}
			}
			if (LevelGround.terrain != null)
			{
				Terrain terrain = LevelGround.terrain;
				Terrain terrain2 = LevelGround.terrain2;
				if (GraphicsSettings.blend)
				{
					ERenderMode renderMode = GraphicsSettings.renderMode;
					if (renderMode != ERenderMode.FORWARD)
					{
						if (renderMode != ERenderMode.DEFERRED)
						{
							terrain.materialTemplate = null;
							terrain2.materialTemplate = null;
							Debug.LogError("Unknown render mode: " + GraphicsSettings.renderMode);
						}
						else
						{
							terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Deferred");
							terrain2.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Deferred");
						}
					}
					else
					{
						terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Forward");
						terrain2.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Forward");
					}
					terrain.basemapDistance = 512f;
					terrain2.basemapDistance = 512f;
				}
				else
				{
					terrain.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Classic");
					terrain2.materialTemplate = Resources.Load<Material>("Materials/Landscapes/Landscape_Classic");
					terrain.basemapDistance = 256f;
					terrain2.basemapDistance = 256f;
				}
				switch (GraphicsSettings.terrainQuality)
				{
				case EGraphicQuality.LOW:
					terrain.heightmapPixelError = 64f;
					break;
				case EGraphicQuality.MEDIUM:
					terrain.heightmapPixelError = 32f;
					break;
				case EGraphicQuality.HIGH:
					terrain.heightmapPixelError = 16f;
					break;
				case EGraphicQuality.ULTRA:
					terrain.heightmapPixelError = 8f;
					break;
				}
				terrain2.heightmapPixelError = terrain.heightmapPixelError * 2f;
				if (GraphicsSettings.foliageQuality == EGraphicQuality.OFF)
				{
					terrain.detailObjectDensity = 0f;
					terrain.detailObjectDistance = 0f;
				}
				else if (GraphicsSettings.foliageQuality == EGraphicQuality.LOW)
				{
					terrain.detailObjectDensity = 0.25f;
					terrain.detailObjectDistance = 60f;
				}
				else if (GraphicsSettings.foliageQuality == EGraphicQuality.MEDIUM)
				{
					terrain.detailObjectDensity = 0.5f;
					terrain.detailObjectDistance = 120f;
				}
				else if (GraphicsSettings.foliageQuality == EGraphicQuality.HIGH)
				{
					terrain.detailObjectDensity = 0.75f;
					terrain.detailObjectDistance = 180f;
				}
				else if (GraphicsSettings.foliageQuality == EGraphicQuality.ULTRA)
				{
					terrain.detailObjectDensity = 1f;
					terrain.detailObjectDistance = 250f;
				}
				terrain.terrainData.wavingGrassAmount = 0.25f;
				terrain.terrainData.wavingGrassSpeed = 0.5f;
				terrain.terrainData.wavingGrassStrength = 1f;
			}
			switch (GraphicsSettings.foliageQuality)
			{
			case EGraphicQuality.OFF:
				FoliageSettings.enabled = false;
				FoliageSettings.drawDistance = 0;
				FoliageSettings.instanceDensity = 0f;
				FoliageSettings.drawFocusDistance = 0;
				FoliageSettings.focusDistance = 0f;
				break;
			case EGraphicQuality.LOW:
				FoliageSettings.enabled = true;
				FoliageSettings.drawDistance = 2;
				FoliageSettings.instanceDensity = 0.25f;
				FoliageSettings.drawFocusDistance = 1;
				FoliageSettings.focusDistance = 1024f;
				break;
			case EGraphicQuality.MEDIUM:
				FoliageSettings.enabled = true;
				FoliageSettings.drawDistance = 3;
				FoliageSettings.instanceDensity = 0.5f;
				FoliageSettings.drawFocusDistance = 2;
				FoliageSettings.focusDistance = 1024f;
				break;
			case EGraphicQuality.HIGH:
				FoliageSettings.enabled = true;
				FoliageSettings.drawDistance = 4;
				FoliageSettings.instanceDensity = 0.75f;
				FoliageSettings.drawFocusDistance = 3;
				FoliageSettings.focusDistance = 2048f;
				break;
			case EGraphicQuality.ULTRA:
				FoliageSettings.enabled = true;
				FoliageSettings.drawDistance = 5;
				FoliageSettings.instanceDensity = 1f;
				FoliageSettings.drawFocusDistance = 4;
				FoliageSettings.focusDistance = 2048f;
				break;
			default:
				FoliageSettings.enabled = false;
				FoliageSettings.drawDistance = 0;
				FoliageSettings.instanceDensity = 0f;
				FoliageSettings.drawFocusDistance = 0;
				FoliageSettings.focusDistance = 0f;
				Debug.LogError("Unknown foliage quality: " + GraphicsSettings.foliageQuality);
				break;
			}
			FoliageSettings.drawFocus = GraphicsSettings.foliageFocus;
			if (LevelLighting.sea != null)
			{
				Material sea = LevelLighting.sea;
				PlanarReflection reflection = LevelLighting.reflection;
				if (sea != null && reflection != null)
				{
					if (GraphicsSettings.waterQuality == EGraphicQuality.LOW)
					{
						sea.shader.maximumLOD = 201;
						Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
						Shader.DisableKeyword("WATER_EDGEBLEND_ON");
						Shader.DisableKeyword("WATER_REFLECTIVE");
						Shader.EnableKeyword("WATER_SIMPLE");
						reflection.enabled = false;
					}
					else if (GraphicsSettings.waterQuality == EGraphicQuality.MEDIUM)
					{
						sea.shader.maximumLOD = 301;
						Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
						Shader.DisableKeyword("WATER_EDGEBLEND_ON");
						Shader.DisableKeyword("WATER_REFLECTIVE");
						Shader.EnableKeyword("WATER_SIMPLE");
						reflection.enabled = false;
					}
					else if (GraphicsSettings.waterQuality == EGraphicQuality.HIGH)
					{
						sea.shader.maximumLOD = 501;
						if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
						{
							Shader.EnableKeyword("WATER_EDGEBLEND_ON");
							Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
						}
						else
						{
							Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
							Shader.DisableKeyword("WATER_EDGEBLEND_ON");
						}
						Shader.DisableKeyword("WATER_REFLECTIVE");
						Shader.EnableKeyword("WATER_SIMPLE");
						reflection.enabled = false;
					}
					else if (GraphicsSettings.waterQuality == EGraphicQuality.ULTRA)
					{
						sea.shader.maximumLOD = 501;
						if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
						{
							Shader.EnableKeyword("WATER_EDGEBLEND_ON");
							Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
						}
						else
						{
							Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
							Shader.DisableKeyword("WATER_EDGEBLEND_ON");
						}
						reflection.enabled = true;
					}
				}
			}
			if (LevelLighting.clouds != null)
			{
				LevelLighting.clouds.gameObject.SetActive(GraphicsSettings.clouds);
				LevelLighting.clouds.GetComponent<ParticleSystem>().Play();
			}
			if (LevelLighting.rain != null)
			{
				LevelLighting.rain.GetComponent<ParticleSystem>().collision.collidesWith = ((!GraphicsSettings.puddle) ? (RayMasks.LARGE | RayMasks.STRUCTURE) : (RayMasks.LARGE | RayMasks.STRUCTURE | RayMasks.GROUND));
				LevelLighting.rain.GetComponent<ParticleSystem>().subEmitters.enabled = GraphicsSettings.puddle;
			}
			LevelLighting.isSkyboxReflectionEnabled = GraphicsSettings.skyboxReflection;
			if (GraphicsSettings.windQuality > EGraphicQuality.OFF)
			{
				Shader.EnableKeyword("NICE_FOLIAGE_ON");
				Shader.EnableKeyword("GRASS_WIND_ON");
			}
			else
			{
				Shader.DisableKeyword("NICE_FOLIAGE_ON");
				Shader.DisableKeyword("GRASS_WIND_ON");
			}
			if (GraphicsSettings.windQuality > EGraphicQuality.LOW)
			{
				Shader.EnableKeyword("ENABLE_WIND");
			}
			else
			{
				Shader.DisableKeyword("ENABLE_WIND");
			}
			switch (GraphicsSettings.windQuality)
			{
			case EGraphicQuality.OFF:
				Shader.SetGlobalInt("_MaxWindQuality", 0);
				break;
			case EGraphicQuality.LOW:
				Shader.SetGlobalInt("_MaxWindQuality", 1);
				break;
			case EGraphicQuality.MEDIUM:
				Shader.SetGlobalInt("_MaxWindQuality", 2);
				break;
			case EGraphicQuality.HIGH:
				Shader.SetGlobalInt("_MaxWindQuality", 3);
				break;
			case EGraphicQuality.ULTRA:
				Shader.SetGlobalInt("_MaxWindQuality", 4);
				break;
			}
			if (GraphicsSettings.grassDisplacement)
			{
				Shader.EnableKeyword("GRASS_DISPLACEMENT_ON");
			}
			else
			{
				Shader.DisableKeyword("GRASS_DISPLACEMENT_ON");
			}
			if (Level.info != null && Level.info.configData != null && Level.info.configData.Terrain_Snow_Sparkle && GraphicsSettings.glitter)
			{
				Shader.EnableKeyword("IS_SNOWING");
			}
			else
			{
				Shader.DisableKeyword("IS_SNOWING");
			}
			if (GraphicsSettings.triplanar)
			{
				Shader.EnableKeyword("TRIPLANAR_MAPPING_ON");
			}
			else
			{
				Shader.DisableKeyword("TRIPLANAR_MAPPING_ON");
			}
			GraphicsSettings.planarReflectionNeedsUpdate = true;
			if (GraphicsSettings.graphicsSettingsApplied != null)
			{
				GraphicsSettings.graphicsSettingsApplied();
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x001424A8 File Offset: 0x001408A8
		public static void restoreDefaults()
		{
			bool isFullscreenEnabled = false;
			bool isVSyncEnabled = false;
			GraphicsSettingsResolution resolution = new GraphicsSettingsResolution();
			if (GraphicsSettings.graphicsSettingsData != null)
			{
				isFullscreenEnabled = GraphicsSettings.graphicsSettingsData.IsFullscreenEnabled;
				isVSyncEnabled = GraphicsSettings.graphicsSettingsData.IsVSyncEnabled;
				resolution = GraphicsSettings.graphicsSettingsData.Resolution;
			}
			GraphicsSettings.graphicsSettingsData = new GraphicsSettingsData();
			GraphicsSettings.graphicsSettingsData.IsFullscreenEnabled = isFullscreenEnabled;
			GraphicsSettings.graphicsSettingsData.IsVSyncEnabled = isVSyncEnabled;
			GraphicsSettings.graphicsSettingsData.Resolution = resolution;
			GraphicsSettings.apply();
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x0014251C File Offset: 0x0014091C
		public static void load()
		{
			if (ReadWrite.fileExists("/Settings/Graphics.json", true))
			{
				try
				{
					GraphicsSettings.graphicsSettingsData = ReadWrite.deserializeJSON<GraphicsSettingsData>("/Settings/Graphics.json", true);
				}
				catch
				{
					GraphicsSettings.graphicsSettingsData = null;
				}
				if (GraphicsSettings.graphicsSettingsData == null)
				{
					GraphicsSettings.restoreDefaults();
				}
			}
			else
			{
				GraphicsSettings.restoreDefaults();
			}
			if (GraphicsSettings.graphicsSettingsData.EffectQuality == EGraphicQuality.OFF)
			{
				GraphicsSettings.graphicsSettingsData.EffectQuality = EGraphicQuality.MEDIUM;
			}
			if (!Application.isEditor && (GraphicsSettings.resolution.Width > Screen.resolutions[Screen.resolutions.Length - 1].width || GraphicsSettings.resolution.Height > Screen.resolutions[Screen.resolutions.Length - 1].height))
			{
				GraphicsSettings.resolution = new GraphicsSettingsResolution(Screen.resolutions[Screen.resolutions.Length - 1]);
			}
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x00142618 File Offset: 0x00140A18
		public static void save()
		{
			ReadWrite.serializeJSON<GraphicsSettingsData>("/Settings/Graphics.json", true, GraphicsSettings.graphicsSettingsData);
		}

		// Token: 0x040020B0 RID: 8368
		private static bool _uncapLandmarks = false;

		// Token: 0x040020B1 RID: 8369
		private static float _uiTextScale = 1f;

		// Token: 0x040020B2 RID: 8370
		private static float _uiLayoutScale = 1f;

		// Token: 0x040020B3 RID: 8371
		public static readonly float MAX_ASPECT_RATIO = 2.33333325f;

		// Token: 0x040020B4 RID: 8372
		private static readonly float EFFECT_ULTRA = 64f;

		// Token: 0x040020B5 RID: 8373
		private static readonly float EFFECT_HIGH = 48f;

		// Token: 0x040020B6 RID: 8374
		private static readonly float EFFECT_MEDIUM = 32f;

		// Token: 0x040020B7 RID: 8375
		private static readonly float EFFECT_LOW = 16f;

		// Token: 0x040020B8 RID: 8376
		protected static PostProcessingProfile _mainProfile;

		// Token: 0x040020B9 RID: 8377
		protected static PostProcessingProfile _viewProfile;

		// Token: 0x040020BA RID: 8378
		public static bool planarReflectionNeedsUpdate;

		// Token: 0x040020BB RID: 8379
		private static GraphicsSettingsData graphicsSettingsData = new GraphicsSettingsData();

		// Token: 0x040020BD RID: 8381
		private static bool changeResolution;
	}
}
