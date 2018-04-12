using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200082B RID: 2091
	[Serializable]
	public class GrainModel : PostProcessingModel
	{
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x001CC30E File Offset: 0x001CA70E
		// (set) Token: 0x06003B87 RID: 15239 RVA: 0x001CC316 File Offset: 0x001CA716
		public GrainModel.Settings settings
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

		// Token: 0x06003B88 RID: 15240 RVA: 0x001CC31F File Offset: 0x001CA71F
		public override void Reset()
		{
			this.m_Settings = GrainModel.Settings.defaultSettings;
		}

		// Token: 0x04002FF6 RID: 12278
		[SerializeField]
		private GrainModel.Settings m_Settings = GrainModel.Settings.defaultSettings;

		// Token: 0x0200082C RID: 2092
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AC0 RID: 2752
			// (get) Token: 0x06003B89 RID: 15241 RVA: 0x001CC32C File Offset: 0x001CA72C
			public static GrainModel.Settings defaultSettings
			{
				get
				{
					return new GrainModel.Settings
					{
						colored = true,
						intensity = 0.5f,
						size = 1f,
						luminanceContribution = 0.8f
					};
				}
			}

			// Token: 0x04002FF7 RID: 12279
			[Tooltip("Enable the use of colored grain.")]
			public bool colored;

			// Token: 0x04002FF8 RID: 12280
			[Range(0f, 1f)]
			[Tooltip("Grain strength. Higher means more visible grain.")]
			public float intensity;

			// Token: 0x04002FF9 RID: 12281
			[Range(1f, 3f)]
			[Tooltip("Grain particle size in \"Filmic\" mode.")]
			public float size;

			// Token: 0x04002FFA RID: 12282
			[Range(0f, 1f)]
			[Tooltip("Controls the noisiness response curve based on scene luminance. Lower values mean less noise in dark areas.")]
			public float luminanceContribution;
		}
	}
}
