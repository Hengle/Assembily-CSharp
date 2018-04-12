using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200082D RID: 2093
	[Serializable]
	public class MotionBlurModel : PostProcessingModel
	{
		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x001CC381 File Offset: 0x001CA781
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x001CC389 File Offset: 0x001CA789
		public MotionBlurModel.Settings settings
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

		// Token: 0x06003B8D RID: 15245 RVA: 0x001CC392 File Offset: 0x001CA792
		public override void Reset()
		{
			this.m_Settings = MotionBlurModel.Settings.defaultSettings;
		}

		// Token: 0x04002FFB RID: 12283
		[SerializeField]
		private MotionBlurModel.Settings m_Settings = MotionBlurModel.Settings.defaultSettings;

		// Token: 0x0200082E RID: 2094
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06003B8E RID: 15246 RVA: 0x001CC3A0 File Offset: 0x001CA7A0
			public static MotionBlurModel.Settings defaultSettings
			{
				get
				{
					return new MotionBlurModel.Settings
					{
						shutterAngle = 270f,
						sampleCount = 10,
						frameBlending = 0f
					};
				}
			}

			// Token: 0x04002FFC RID: 12284
			[Range(0f, 360f)]
			[Tooltip("The angle of rotary shutter. Larger values give longer exposure.")]
			public float shutterAngle;

			// Token: 0x04002FFD RID: 12285
			[Range(4f, 32f)]
			[Tooltip("The amount of sample points, which affects quality and performances.")]
			public int sampleCount;

			// Token: 0x04002FFE RID: 12286
			[Range(0f, 1f)]
			[Tooltip("The strength of multiple frame blending. The opacity of preceding frames are determined from this coefficient and time differences.")]
			public float frameBlending;
		}
	}
}
