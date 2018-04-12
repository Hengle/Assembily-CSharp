using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000804 RID: 2052
	[Serializable]
	public class AmbientOcclusionModel : PostProcessingModel
	{
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x001CB5FA File Offset: 0x001C99FA
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x001CB602 File Offset: 0x001C9A02
		public AmbientOcclusionModel.Settings settings
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

		// Token: 0x06003B48 RID: 15176 RVA: 0x001CB60B File Offset: 0x001C9A0B
		public override void Reset()
		{
			this.m_Settings = AmbientOcclusionModel.Settings.defaultSettings;
		}

		// Token: 0x04002F5C RID: 12124
		[SerializeField]
		private AmbientOcclusionModel.Settings m_Settings = AmbientOcclusionModel.Settings.defaultSettings;

		// Token: 0x02000805 RID: 2053
		public enum SampleCount
		{
			// Token: 0x04002F5E RID: 12126
			Lowest = 3,
			// Token: 0x04002F5F RID: 12127
			Low = 6,
			// Token: 0x04002F60 RID: 12128
			Medium = 10,
			// Token: 0x04002F61 RID: 12129
			High = 16
		}

		// Token: 0x02000806 RID: 2054
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000A9F RID: 2719
			// (get) Token: 0x06003B49 RID: 15177 RVA: 0x001CB618 File Offset: 0x001C9A18
			public static AmbientOcclusionModel.Settings defaultSettings
			{
				get
				{
					return new AmbientOcclusionModel.Settings
					{
						intensity = 1f,
						radius = 0.3f,
						sampleCount = AmbientOcclusionModel.SampleCount.Medium,
						downsampling = true,
						forceForwardCompatibility = false,
						ambientOnly = false,
						highPrecision = false
					};
				}
			}

			// Token: 0x04002F62 RID: 12130
			[Range(0f, 4f)]
			[Tooltip("Degree of darkness produced by the effect.")]
			public float intensity;

			// Token: 0x04002F63 RID: 12131
			[Min(0.0001f)]
			[Tooltip("Radius of sample points, which affects extent of darkened areas.")]
			public float radius;

			// Token: 0x04002F64 RID: 12132
			[Tooltip("Number of sample points, which affects quality and performance.")]
			public AmbientOcclusionModel.SampleCount sampleCount;

			// Token: 0x04002F65 RID: 12133
			[Tooltip("Halves the resolution of the effect to increase performance.")]
			public bool downsampling;

			// Token: 0x04002F66 RID: 12134
			[Tooltip("Forces compatibility with Forward rendered objects when working with the Deferred rendering path.")]
			public bool forceForwardCompatibility;

			// Token: 0x04002F67 RID: 12135
			[Tooltip("Enables the ambient-only mode in that the effect only affects ambient lighting. This mode is only available with the Deferred rendering path and HDR rendering.")]
			public bool ambientOnly;

			// Token: 0x04002F68 RID: 12136
			[Tooltip("Toggles the use of a higher precision depth texture with the forward rendering path (may impact performances). Has no effect with the deferred rendering path.")]
			public bool highPrecision;
		}
	}
}
