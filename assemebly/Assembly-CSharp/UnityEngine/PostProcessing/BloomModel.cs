using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200080F RID: 2063
	[Serializable]
	public class BloomModel : PostProcessingModel
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06003B54 RID: 15188 RVA: 0x001CB9FD File Offset: 0x001C9DFD
		// (set) Token: 0x06003B55 RID: 15189 RVA: 0x001CBA05 File Offset: 0x001C9E05
		public BloomModel.Settings settings
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

		// Token: 0x06003B56 RID: 15190 RVA: 0x001CBA0E File Offset: 0x001C9E0E
		public override void Reset()
		{
			this.m_Settings = BloomModel.Settings.defaultSettings;
		}

		// Token: 0x04002F84 RID: 12164
		[SerializeField]
		private BloomModel.Settings m_Settings = BloomModel.Settings.defaultSettings;

		// Token: 0x02000810 RID: 2064
		[Serializable]
		public struct BloomSettings
		{
			// Token: 0x17000AA5 RID: 2725
			// (get) Token: 0x06003B58 RID: 15192 RVA: 0x001CBA29 File Offset: 0x001C9E29
			// (set) Token: 0x06003B57 RID: 15191 RVA: 0x001CBA1B File Offset: 0x001C9E1B
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.threshold);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06003B59 RID: 15193 RVA: 0x001CBA38 File Offset: 0x001C9E38
			public static BloomModel.BloomSettings defaultSettings
			{
				get
				{
					return new BloomModel.BloomSettings
					{
						intensity = 0.5f,
						threshold = 1.1f,
						softKnee = 0.5f,
						radius = 4f,
						antiFlicker = false
					};
				}
			}

			// Token: 0x04002F85 RID: 12165
			[Min(0f)]
			[Tooltip("Blend factor of the result image.")]
			public float intensity;

			// Token: 0x04002F86 RID: 12166
			[Min(0f)]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x04002F87 RID: 12167
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual (0 = hard threshold, 1 = soft threshold).")]
			public float softKnee;

			// Token: 0x04002F88 RID: 12168
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x04002F89 RID: 12169
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;
		}

		// Token: 0x02000811 RID: 2065
		[Serializable]
		public struct LensDirtSettings
		{
			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06003B5A RID: 15194 RVA: 0x001CBA88 File Offset: 0x001C9E88
			public static BloomModel.LensDirtSettings defaultSettings
			{
				get
				{
					return new BloomModel.LensDirtSettings
					{
						texture = null,
						intensity = 3f
					};
				}
			}

			// Token: 0x04002F8A RID: 12170
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture texture;

			// Token: 0x04002F8B RID: 12171
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float intensity;
		}

		// Token: 0x02000812 RID: 2066
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06003B5B RID: 15195 RVA: 0x001CBAB4 File Offset: 0x001C9EB4
			public static BloomModel.Settings defaultSettings
			{
				get
				{
					return new BloomModel.Settings
					{
						bloom = BloomModel.BloomSettings.defaultSettings,
						lensDirt = BloomModel.LensDirtSettings.defaultSettings
					};
				}
			}

			// Token: 0x04002F8C RID: 12172
			public BloomModel.BloomSettings bloom;

			// Token: 0x04002F8D RID: 12173
			public BloomModel.LensDirtSettings lensDirt;
		}
	}
}
