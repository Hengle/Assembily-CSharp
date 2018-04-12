using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000828 RID: 2088
	[Serializable]
	public class EyeAdaptationModel : PostProcessingModel
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x001CC259 File Offset: 0x001CA659
		// (set) Token: 0x06003B82 RID: 15234 RVA: 0x001CC261 File Offset: 0x001CA661
		public EyeAdaptationModel.Settings settings
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

		// Token: 0x06003B83 RID: 15235 RVA: 0x001CC26A File Offset: 0x001CA66A
		public override void Reset()
		{
			this.m_Settings = EyeAdaptationModel.Settings.defaultSettings;
		}

		// Token: 0x04002FE8 RID: 12264
		[SerializeField]
		private EyeAdaptationModel.Settings m_Settings = EyeAdaptationModel.Settings.defaultSettings;

		// Token: 0x02000829 RID: 2089
		public enum EyeAdaptationType
		{
			// Token: 0x04002FEA RID: 12266
			Progressive,
			// Token: 0x04002FEB RID: 12267
			Fixed
		}

		// Token: 0x0200082A RID: 2090
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x06003B84 RID: 15236 RVA: 0x001CC278 File Offset: 0x001CA678
			public static EyeAdaptationModel.Settings defaultSettings
			{
				get
				{
					return new EyeAdaptationModel.Settings
					{
						lowPercent = 65f,
						highPercent = 95f,
						minLuminance = 0.03f,
						maxLuminance = 2f,
						exposureCompensation = 0.5f,
						adaptationType = EyeAdaptationModel.EyeAdaptationType.Progressive,
						speedUp = 2f,
						speedDown = 1f,
						logMin = -8,
						logMax = 4
					};
				}
			}

			// Token: 0x04002FEC RID: 12268
			[Range(1f, 99f)]
			[Tooltip("Filters the dark part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float lowPercent;

			// Token: 0x04002FED RID: 12269
			[Range(1f, 99f)]
			[Tooltip("Filters the bright part of the histogram when computing the average luminance to avoid very dark pixels from contributing to the auto exposure. Unit is in percent.")]
			public float highPercent;

			// Token: 0x04002FEE RID: 12270
			[Min(0f)]
			[Tooltip("Minimum average luminance to consider for auto exposure.")]
			public float minLuminance;

			// Token: 0x04002FEF RID: 12271
			[Min(0f)]
			[Tooltip("Maximum average luminance to consider for auto exposure.")]
			public float maxLuminance;

			// Token: 0x04002FF0 RID: 12272
			[Min(0f)]
			[Tooltip("Exposure bias. Use this to control the global exposure of the scene.")]
			public float exposureCompensation;

			// Token: 0x04002FF1 RID: 12273
			[Tooltip("Use \"Progressive\" if you want the auto exposure to be animated. Use \"Fixed\" otherwise.")]
			public EyeAdaptationModel.EyeAdaptationType adaptationType;

			// Token: 0x04002FF2 RID: 12274
			[Min(0f)]
			[Tooltip("Adaptation speed from a dark to a light environment.")]
			public float speedUp;

			// Token: 0x04002FF3 RID: 12275
			[Min(0f)]
			[Tooltip("Adaptation speed from a light to a dark environment.")]
			public float speedDown;

			// Token: 0x04002FF4 RID: 12276
			[Range(-16f, -1f)]
			[Tooltip("Lower bound for the brightness range of the generated histogram (Log2).")]
			public int logMin;

			// Token: 0x04002FF5 RID: 12277
			[Range(1f, 16f)]
			[Tooltip("Upper bound for the brightness range of the generated histogram (Log2).")]
			public int logMax;
		}
	}
}
