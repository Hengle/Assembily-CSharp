using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000813 RID: 2067
	[Serializable]
	public class BuiltinDebugViewsModel : PostProcessingModel
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x001CBAF5 File Offset: 0x001C9EF5
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x001CBAFD File Offset: 0x001C9EFD
		public BuiltinDebugViewsModel.Settings settings
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

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x001CBB06 File Offset: 0x001C9F06
		public bool willInterrupt
		{
			get
			{
				return !this.IsModeActive(BuiltinDebugViewsModel.Mode.None) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.EyeAdaptation) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.PreGradingLog) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.LogLut) && !this.IsModeActive(BuiltinDebugViewsModel.Mode.UserLut);
			}
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x001CBB46 File Offset: 0x001C9F46
		public override void Reset()
		{
			this.settings = BuiltinDebugViewsModel.Settings.defaultSettings;
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x001CBB53 File Offset: 0x001C9F53
		public bool IsModeActive(BuiltinDebugViewsModel.Mode mode)
		{
			return this.m_Settings.mode == mode;
		}

		// Token: 0x04002F8E RID: 12174
		[SerializeField]
		private BuiltinDebugViewsModel.Settings m_Settings = BuiltinDebugViewsModel.Settings.defaultSettings;

		// Token: 0x02000814 RID: 2068
		[Serializable]
		public struct DepthSettings
		{
			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06003B62 RID: 15202 RVA: 0x001CBB64 File Offset: 0x001C9F64
			public static BuiltinDebugViewsModel.DepthSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.DepthSettings
					{
						scale = 1f
					};
				}
			}

			// Token: 0x04002F8F RID: 12175
			[Range(0f, 1f)]
			[Tooltip("Scales the camera far plane before displaying the depth map.")]
			public float scale;
		}

		// Token: 0x02000815 RID: 2069
		[Serializable]
		public struct MotionVectorsSettings
		{
			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x06003B63 RID: 15203 RVA: 0x001CBB88 File Offset: 0x001C9F88
			public static BuiltinDebugViewsModel.MotionVectorsSettings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.MotionVectorsSettings
					{
						sourceOpacity = 1f,
						motionImageOpacity = 0.9f,
						motionImageAmplitude = 16f,
						motionVectorsOpacity = 1f,
						motionVectorsResolution = 24,
						motionVectorsAmplitude = 64f
					};
				}
			}

			// Token: 0x04002F90 RID: 12176
			[Range(0f, 1f)]
			[Tooltip("Opacity of the source render.")]
			public float sourceOpacity;

			// Token: 0x04002F91 RID: 12177
			[Range(0f, 1f)]
			[Tooltip("Opacity of the per-pixel motion vector colors.")]
			public float motionImageOpacity;

			// Token: 0x04002F92 RID: 12178
			[Min(0f)]
			[Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
			public float motionImageAmplitude;

			// Token: 0x04002F93 RID: 12179
			[Range(0f, 1f)]
			[Tooltip("Opacity for the motion vector arrows.")]
			public float motionVectorsOpacity;

			// Token: 0x04002F94 RID: 12180
			[Range(8f, 64f)]
			[Tooltip("The arrow density on screen.")]
			public int motionVectorsResolution;

			// Token: 0x04002F95 RID: 12181
			[Min(0f)]
			[Tooltip("Tweaks the arrows length.")]
			public float motionVectorsAmplitude;
		}

		// Token: 0x02000816 RID: 2070
		public enum Mode
		{
			// Token: 0x04002F97 RID: 12183
			None,
			// Token: 0x04002F98 RID: 12184
			Depth,
			// Token: 0x04002F99 RID: 12185
			Normals,
			// Token: 0x04002F9A RID: 12186
			MotionVectors,
			// Token: 0x04002F9B RID: 12187
			AmbientOcclusion,
			// Token: 0x04002F9C RID: 12188
			EyeAdaptation,
			// Token: 0x04002F9D RID: 12189
			FocusPlane,
			// Token: 0x04002F9E RID: 12190
			PreGradingLog,
			// Token: 0x04002F9F RID: 12191
			LogLut,
			// Token: 0x04002FA0 RID: 12192
			UserLut
		}

		// Token: 0x02000817 RID: 2071
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AAD RID: 2733
			// (get) Token: 0x06003B64 RID: 15204 RVA: 0x001CBBE4 File Offset: 0x001C9FE4
			public static BuiltinDebugViewsModel.Settings defaultSettings
			{
				get
				{
					return new BuiltinDebugViewsModel.Settings
					{
						mode = BuiltinDebugViewsModel.Mode.None,
						depth = BuiltinDebugViewsModel.DepthSettings.defaultSettings,
						motionVectors = BuiltinDebugViewsModel.MotionVectorsSettings.defaultSettings
					};
				}
			}

			// Token: 0x04002FA1 RID: 12193
			public BuiltinDebugViewsModel.Mode mode;

			// Token: 0x04002FA2 RID: 12194
			public BuiltinDebugViewsModel.DepthSettings depth;

			// Token: 0x04002FA3 RID: 12195
			public BuiltinDebugViewsModel.MotionVectorsSettings motionVectors;
		}
	}
}
