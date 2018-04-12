using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000838 RID: 2104
	[Serializable]
	public class VignetteModel : PostProcessingModel
	{
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x001CC549 File Offset: 0x001CA949
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x001CC551 File Offset: 0x001CA951
		public VignetteModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x001CC55A File Offset: 0x001CA95A
		public override void Reset()
		{
			this.m_Settings = VignetteModel.Settings.defaultSettings;
		}

		// Token: 0x04003019 RID: 12313
		[SerializeField]
		private VignetteModel.Settings m_Settings = VignetteModel.Settings.defaultSettings;

		// Token: 0x02000839 RID: 2105
		public enum Mode
		{
			// Token: 0x0400301B RID: 12315
			Classic,
			// Token: 0x0400301C RID: 12316
			Round,
			// Token: 0x0400301D RID: 12317
			Masked
		}

		// Token: 0x0200083A RID: 2106
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AC8 RID: 2760
			// (get) Token: 0x06003B9D RID: 15261 RVA: 0x001CC568 File Offset: 0x001CA968
			public static VignetteModel.Settings defaultSettings
			{
				get
				{
					return new VignetteModel.Settings
					{
						mode = VignetteModel.Mode.Classic,
						color = new Color(0f, 0f, 0f, 1f),
						center = new Vector2(0.5f, 0.5f),
						intensity = 0.45f,
						smoothness = 0.2f,
						roundness = 1f,
						mask = null,
						opacity = 1f
					};
				}
			}

			// Token: 0x0400301E RID: 12318
			[Tooltip("Use the \"Classic\" mode for parametric controls. Use \"Round\" to get a perfectly round vignette no matter what the aspect ratio is. Use the \"Masked\" mode to use your own texture mask.")]
			public VignetteModel.Mode mode;

			// Token: 0x0400301F RID: 12319
			[ColorUsage(false)]
			[Tooltip("Vignette color. Use the alpha channel for transparency.")]
			public Color color;

			// Token: 0x04003020 RID: 12320
			[Tooltip("Sets the vignette center point (screen center is [0.5,0.5]).")]
			public Vector2 center;

			// Token: 0x04003021 RID: 12321
			[Range(0f, 1f)]
			[Tooltip("Amount of vignetting on screen.")]
			public float intensity;

			// Token: 0x04003022 RID: 12322
			[Range(0.01f, 1f)]
			[Tooltip("Smoothness of the vignette borders.")]
			public float smoothness;

			// Token: 0x04003023 RID: 12323
			[Range(0f, 1f)]
			[Tooltip("Lower values will make a square-ish vignette.")]
			public float roundness;

			// Token: 0x04003024 RID: 12324
			[Tooltip("A black and white mask to use as a vignette.")]
			public Texture mask;

			// Token: 0x04003025 RID: 12325
			[Range(0f, 1f)]
			[Tooltip("Mask opacity.")]
			public float opacity;
		}
	}
}
