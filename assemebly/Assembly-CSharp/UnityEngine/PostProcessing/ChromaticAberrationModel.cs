using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000818 RID: 2072
	[Serializable]
	public class ChromaticAberrationModel : PostProcessingModel
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x001CBC2D File Offset: 0x001CA02D
		// (set) Token: 0x06003B67 RID: 15207 RVA: 0x001CBC35 File Offset: 0x001CA035
		public ChromaticAberrationModel.Settings settings
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

		// Token: 0x06003B68 RID: 15208 RVA: 0x001CBC3E File Offset: 0x001CA03E
		public override void Reset()
		{
			this.m_Settings = ChromaticAberrationModel.Settings.defaultSettings;
		}

		// Token: 0x04002FA4 RID: 12196
		[SerializeField]
		private ChromaticAberrationModel.Settings m_Settings = ChromaticAberrationModel.Settings.defaultSettings;

		// Token: 0x02000819 RID: 2073
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AAF RID: 2735
			// (get) Token: 0x06003B69 RID: 15209 RVA: 0x001CBC4C File Offset: 0x001CA04C
			public static ChromaticAberrationModel.Settings defaultSettings
			{
				get
				{
					return new ChromaticAberrationModel.Settings
					{
						spectralTexture = null,
						intensity = 0.1f
					};
				}
			}

			// Token: 0x04002FA5 RID: 12197
			[Tooltip("Shift the hue of chromatic aberrations.")]
			public Texture2D spectralTexture;

			// Token: 0x04002FA6 RID: 12198
			[Range(0f, 1f)]
			[Tooltip("Amount of tangential distortion.")]
			public float intensity;
		}
	}
}
