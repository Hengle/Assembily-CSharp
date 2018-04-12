using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000807 RID: 2055
	[Serializable]
	public class AntialiasingModel : PostProcessingModel
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x001CB682 File Offset: 0x001C9A82
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x001CB68A File Offset: 0x001C9A8A
		public AntialiasingModel.Settings settings
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

		// Token: 0x06003B4D RID: 15181 RVA: 0x001CB693 File Offset: 0x001C9A93
		public override void Reset()
		{
			this.m_Settings = AntialiasingModel.Settings.defaultSettings;
		}

		// Token: 0x04002F69 RID: 12137
		[SerializeField]
		private AntialiasingModel.Settings m_Settings = AntialiasingModel.Settings.defaultSettings;

		// Token: 0x02000808 RID: 2056
		public enum Method
		{
			// Token: 0x04002F6B RID: 12139
			Fxaa,
			// Token: 0x04002F6C RID: 12140
			Taa
		}

		// Token: 0x02000809 RID: 2057
		public enum FxaaPreset
		{
			// Token: 0x04002F6E RID: 12142
			ExtremePerformance,
			// Token: 0x04002F6F RID: 12143
			Performance,
			// Token: 0x04002F70 RID: 12144
			Default,
			// Token: 0x04002F71 RID: 12145
			Quality,
			// Token: 0x04002F72 RID: 12146
			ExtremeQuality
		}

		// Token: 0x0200080A RID: 2058
		[Serializable]
		public struct FxaaQualitySettings
		{
			// Token: 0x04002F73 RID: 12147
			[Tooltip("The amount of desired sub-pixel aliasing removal. Effects the sharpeness of the output.")]
			[Range(0f, 1f)]
			public float subpixelAliasingRemovalAmount;

			// Token: 0x04002F74 RID: 12148
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.063f, 0.333f)]
			public float edgeDetectionThreshold;

			// Token: 0x04002F75 RID: 12149
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0f, 0.0833f)]
			public float minimumRequiredLuminance;

			// Token: 0x04002F76 RID: 12150
			public static AntialiasingModel.FxaaQualitySettings[] presets = new AntialiasingModel.FxaaQualitySettings[]
			{
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0f,
					edgeDetectionThreshold = 0.333f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.25f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 0.75f,
					edgeDetectionThreshold = 0.166f,
					minimumRequiredLuminance = 0.0833f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.0625f
				},
				new AntialiasingModel.FxaaQualitySettings
				{
					subpixelAliasingRemovalAmount = 1f,
					edgeDetectionThreshold = 0.063f,
					minimumRequiredLuminance = 0.0312f
				}
			};
		}

		// Token: 0x0200080B RID: 2059
		[Serializable]
		public struct FxaaConsoleSettings
		{
			// Token: 0x04002F77 RID: 12151
			[Tooltip("The amount of spread applied to the sampling coordinates while sampling for subpixel information.")]
			[Range(0.33f, 0.5f)]
			public float subpixelSpreadAmount;

			// Token: 0x04002F78 RID: 12152
			[Tooltip("This value dictates how sharp the edges in the image are kept; a higher value implies sharper edges.")]
			[Range(2f, 8f)]
			public float edgeSharpnessAmount;

			// Token: 0x04002F79 RID: 12153
			[Tooltip("The minimum amount of local contrast required to qualify a region as containing an edge.")]
			[Range(0.125f, 0.25f)]
			public float edgeDetectionThreshold;

			// Token: 0x04002F7A RID: 12154
			[Tooltip("Local contrast adaptation value to disallow the algorithm from executing on the darker regions.")]
			[Range(0.04f, 0.06f)]
			public float minimumRequiredLuminance;

			// Token: 0x04002F7B RID: 12155
			public static AntialiasingModel.FxaaConsoleSettings[] presets = new AntialiasingModel.FxaaConsoleSettings[]
			{
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.25f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.33f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.06f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 8f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.05f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 4f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				},
				new AntialiasingModel.FxaaConsoleSettings
				{
					subpixelSpreadAmount = 0.5f,
					edgeSharpnessAmount = 2f,
					edgeDetectionThreshold = 0.125f,
					minimumRequiredLuminance = 0.04f
				}
			};
		}

		// Token: 0x0200080C RID: 2060
		[Serializable]
		public struct FxaaSettings
		{
			// Token: 0x17000AA1 RID: 2721
			// (get) Token: 0x06003B50 RID: 15184 RVA: 0x001CB94C File Offset: 0x001C9D4C
			public static AntialiasingModel.FxaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.FxaaSettings
					{
						preset = AntialiasingModel.FxaaPreset.Default
					};
				}
			}

			// Token: 0x04002F7C RID: 12156
			public AntialiasingModel.FxaaPreset preset;
		}

		// Token: 0x0200080D RID: 2061
		[Serializable]
		public struct TaaSettings
		{
			// Token: 0x17000AA2 RID: 2722
			// (get) Token: 0x06003B51 RID: 15185 RVA: 0x001CB96C File Offset: 0x001C9D6C
			public static AntialiasingModel.TaaSettings defaultSettings
			{
				get
				{
					return new AntialiasingModel.TaaSettings
					{
						jitterSpread = 0.75f,
						sharpen = 0.3f,
						stationaryBlending = 0.95f,
						motionBlending = 0.85f
					};
				}
			}

			// Token: 0x04002F7D RID: 12157
			[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable but blurrier output.")]
			[Range(0.1f, 1f)]
			public float jitterSpread;

			// Token: 0x04002F7E RID: 12158
			[Tooltip("Controls the amount of sharpening applied to the color buffer.")]
			[Range(0f, 3f)]
			public float sharpen;

			// Token: 0x04002F7F RID: 12159
			[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 1f)]
			public float stationaryBlending;

			// Token: 0x04002F80 RID: 12160
			[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
			[Range(0f, 1f)]
			public float motionBlending;
		}

		// Token: 0x0200080E RID: 2062
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AA3 RID: 2723
			// (get) Token: 0x06003B52 RID: 15186 RVA: 0x001CB9B4 File Offset: 0x001C9DB4
			public static AntialiasingModel.Settings defaultSettings
			{
				get
				{
					return new AntialiasingModel.Settings
					{
						method = AntialiasingModel.Method.Fxaa,
						fxaaSettings = AntialiasingModel.FxaaSettings.defaultSettings,
						taaSettings = AntialiasingModel.TaaSettings.defaultSettings
					};
				}
			}

			// Token: 0x04002F81 RID: 12161
			public AntialiasingModel.Method method;

			// Token: 0x04002F82 RID: 12162
			public AntialiasingModel.FxaaSettings fxaaSettings;

			// Token: 0x04002F83 RID: 12163
			public AntialiasingModel.TaaSettings taaSettings;
		}
	}
}
