using System;

namespace SDG.Unturned
{
	// Token: 0x0200069C RID: 1692
	public class GraphicsSettingsData
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x00142690 File Offset: 0x00140A90
		public GraphicsSettingsData()
		{
			this.Resolution = new GraphicsSettingsResolution();
			this.IsFullscreenEnabled = false;
			this.IsVSyncEnabled = false;
			this.IsBloomEnabled = false;
			this.IsChromaticAberrationEnabled = false;
			this.IsFilmGrainEnabled = false;
			this.IsCloudEnabled = true;
			this.IsNiceBlendEnabled = true;
			this.IsFogEnabled = true;
			this.IsGrassDisplacementEnabled = false;
			this.IsFoliageFocusEnabled = false;
			this.IsRagdollsEnabled = true;
			this.IsDebrisEnabled = true;
			this.IsBlastEnabled = true;
			this.IsPuddleEnabled = true;
			this.IsGlitterEnabled = true;
			this.IsTriplanarMappingEnabled = true;
			this.IsSkyboxReflectionEnabled = false;
			this.DrawDistance = 1f;
			this.LandmarkDistance = 0f;
			this.AntiAliasingType5 = EAntiAliasingType.OFF;
			this.AnisotropicFilteringMode = EAnisotropicFilteringMode.FORCED_ON;
			this.EffectQuality = EGraphicQuality.MEDIUM;
			this.FoliageQuality2 = EGraphicQuality.OFF;
			this.SunShaftsQuality = EGraphicQuality.OFF;
			this.LightingQuality = EGraphicQuality.LOW;
			this.ScreenSpaceAmbientOcclusionQuality = EGraphicQuality.OFF;
			this.ScreenSpaceReflectionQuality = EGraphicQuality.OFF;
			this.PlanarReflectionQuality = EGraphicQuality.MEDIUM;
			this.WaterQuality = EGraphicQuality.LOW;
			this.ScopeQuality2 = EGraphicQuality.OFF;
			this.OutlineQuality = EGraphicQuality.LOW;
			this.BoneQuality = EGraphicQuality.MEDIUM;
			this.TerrainQuality = EGraphicQuality.MEDIUM;
			this.WindQuality = EGraphicQuality.OFF;
			this.TreeMode = ETreeGraphicMode.LEGACY;
			this.RenderMode2 = ERenderMode.FORWARD;
			this.LandmarkQuality = EGraphicQuality.OFF;
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x001427B9 File Offset: 0x00140BB9
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x001427C1 File Offset: 0x00140BC1
		public GraphicsSettingsResolution Resolution { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x001427CA File Offset: 0x00140BCA
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x001427D2 File Offset: 0x00140BD2
		public bool IsFullscreenEnabled { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x001427DB File Offset: 0x00140BDB
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x001427E3 File Offset: 0x00140BE3
		public bool IsVSyncEnabled { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x001427EC File Offset: 0x00140BEC
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x001427F4 File Offset: 0x00140BF4
		public bool IsBloomEnabled { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x001427FD File Offset: 0x00140BFD
		// (set) Token: 0x06003182 RID: 12674 RVA: 0x00142805 File Offset: 0x00140C05
		public bool IsChromaticAberrationEnabled { get; set; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x0014280E File Offset: 0x00140C0E
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x00142816 File Offset: 0x00140C16
		public bool IsFilmGrainEnabled { get; set; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x0014281F File Offset: 0x00140C1F
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x00142827 File Offset: 0x00140C27
		public bool IsCloudEnabled { get; set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x00142830 File Offset: 0x00140C30
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x00142838 File Offset: 0x00140C38
		public bool IsNiceBlendEnabled { get; set; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x00142841 File Offset: 0x00140C41
		// (set) Token: 0x0600318A RID: 12682 RVA: 0x00142849 File Offset: 0x00140C49
		public bool IsFogEnabled { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x00142852 File Offset: 0x00140C52
		// (set) Token: 0x0600318C RID: 12684 RVA: 0x0014285A File Offset: 0x00140C5A
		public float DrawDistance { get; set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x00142863 File Offset: 0x00140C63
		// (set) Token: 0x0600318E RID: 12686 RVA: 0x0014286B File Offset: 0x00140C6B
		public float LandmarkDistance { get; set; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x00142874 File Offset: 0x00140C74
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x0014287C File Offset: 0x00140C7C
		public EAntiAliasingType AntiAliasingType5 { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x00142885 File Offset: 0x00140C85
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x0014288D File Offset: 0x00140C8D
		public EAnisotropicFilteringMode AnisotropicFilteringMode { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x00142896 File Offset: 0x00140C96
		// (set) Token: 0x06003194 RID: 12692 RVA: 0x0014289E File Offset: 0x00140C9E
		public EGraphicQuality EffectQuality { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06003195 RID: 12693 RVA: 0x001428A7 File Offset: 0x00140CA7
		// (set) Token: 0x06003196 RID: 12694 RVA: 0x001428AF File Offset: 0x00140CAF
		public EGraphicQuality FoliageQuality2 { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x001428B8 File Offset: 0x00140CB8
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x001428C0 File Offset: 0x00140CC0
		public EGraphicQuality SunShaftsQuality { get; set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x001428C9 File Offset: 0x00140CC9
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x001428D1 File Offset: 0x00140CD1
		public EGraphicQuality LightingQuality { get; set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x001428DA File Offset: 0x00140CDA
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x001428E2 File Offset: 0x00140CE2
		public EGraphicQuality ScreenSpaceAmbientOcclusionQuality { get; set; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x001428EB File Offset: 0x00140CEB
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x001428F3 File Offset: 0x00140CF3
		public EGraphicQuality ScreenSpaceReflectionQuality { get; set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x001428FC File Offset: 0x00140CFC
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x00142904 File Offset: 0x00140D04
		public EGraphicQuality PlanarReflectionQuality { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x0014290D File Offset: 0x00140D0D
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x00142915 File Offset: 0x00140D15
		public EGraphicQuality WaterQuality { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x0014291E File Offset: 0x00140D1E
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x00142926 File Offset: 0x00140D26
		public EGraphicQuality ScopeQuality2 { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x0014292F File Offset: 0x00140D2F
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x00142937 File Offset: 0x00140D37
		public EGraphicQuality OutlineQuality { get; set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x00142940 File Offset: 0x00140D40
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x00142948 File Offset: 0x00140D48
		public EGraphicQuality BoneQuality { get; set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x00142951 File Offset: 0x00140D51
		// (set) Token: 0x060031AA RID: 12714 RVA: 0x00142959 File Offset: 0x00140D59
		public EGraphicQuality TerrainQuality { get; set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x00142962 File Offset: 0x00140D62
		// (set) Token: 0x060031AC RID: 12716 RVA: 0x0014296A File Offset: 0x00140D6A
		public EGraphicQuality WindQuality { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x00142973 File Offset: 0x00140D73
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x0014297B File Offset: 0x00140D7B
		public ETreeGraphicMode TreeMode { get; set; }

		// Token: 0x040020C7 RID: 8391
		public bool IsGrassDisplacementEnabled;

		// Token: 0x040020C8 RID: 8392
		public bool IsFoliageFocusEnabled;

		// Token: 0x040020C9 RID: 8393
		public bool IsRagdollsEnabled;

		// Token: 0x040020CA RID: 8394
		public bool IsDebrisEnabled;

		// Token: 0x040020CB RID: 8395
		public bool IsBlastEnabled;

		// Token: 0x040020CC RID: 8396
		public bool IsPuddleEnabled;

		// Token: 0x040020CD RID: 8397
		public bool IsGlitterEnabled;

		// Token: 0x040020CE RID: 8398
		public bool IsTriplanarMappingEnabled;

		// Token: 0x040020CF RID: 8399
		public bool IsSkyboxReflectionEnabled;

		// Token: 0x040020E2 RID: 8418
		public ERenderMode RenderMode2;

		// Token: 0x040020E3 RID: 8419
		public EGraphicQuality LandmarkQuality;
	}
}
