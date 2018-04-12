using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200081A RID: 2074
	[Serializable]
	public class ColorGradingModel : PostProcessingModel
	{
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x001CBC89 File Offset: 0x001CA089
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x001CBC91 File Offset: 0x001CA091
		public ColorGradingModel.Settings settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
				this.OnValidate();
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x001CBCA0 File Offset: 0x001CA0A0
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x001CBCA8 File Offset: 0x001CA0A8
		public bool isDirty { get; internal set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x001CBCB1 File Offset: 0x001CA0B1
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x001CBCB9 File Offset: 0x001CA0B9
		public RenderTexture bakedLut { get; internal set; }

		// Token: 0x06003B71 RID: 15217 RVA: 0x001CBCC2 File Offset: 0x001CA0C2
		public override void Reset()
		{
			this.m_Settings = ColorGradingModel.Settings.defaultSettings;
			this.OnValidate();
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x001CBCD5 File Offset: 0x001CA0D5
		public override void OnValidate()
		{
			this.isDirty = true;
		}

		// Token: 0x04002FA7 RID: 12199
		[SerializeField]
		private ColorGradingModel.Settings m_Settings = ColorGradingModel.Settings.defaultSettings;

		// Token: 0x0200081B RID: 2075
		public enum Tonemapper
		{
			// Token: 0x04002FAB RID: 12203
			None,
			// Token: 0x04002FAC RID: 12204
			ACES,
			// Token: 0x04002FAD RID: 12205
			Neutral
		}

		// Token: 0x0200081C RID: 2076
		[Serializable]
		public struct TonemappingSettings
		{
			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x06003B73 RID: 15219 RVA: 0x001CBCE0 File Offset: 0x001CA0E0
			public static ColorGradingModel.TonemappingSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.TonemappingSettings
					{
						tonemapper = ColorGradingModel.Tonemapper.Neutral,
						neutralBlackIn = 0.02f,
						neutralWhiteIn = 10f,
						neutralBlackOut = 0f,
						neutralWhiteOut = 10f,
						neutralWhiteLevel = 5.3f,
						neutralWhiteClip = 10f
					};
				}
			}

			// Token: 0x04002FAE RID: 12206
			[Tooltip("Tonemapping algorithm to use at the end of the color grading process. Use \"Neutral\" if you need a customizable tonemapper or \"Filmic\" to give a standard filmic look to your scenes.")]
			public ColorGradingModel.Tonemapper tonemapper;

			// Token: 0x04002FAF RID: 12207
			[Range(-0.1f, 0.1f)]
			public float neutralBlackIn;

			// Token: 0x04002FB0 RID: 12208
			[Range(1f, 20f)]
			public float neutralWhiteIn;

			// Token: 0x04002FB1 RID: 12209
			[Range(-0.09f, 0.1f)]
			public float neutralBlackOut;

			// Token: 0x04002FB2 RID: 12210
			[Range(1f, 19f)]
			public float neutralWhiteOut;

			// Token: 0x04002FB3 RID: 12211
			[Range(0.1f, 20f)]
			public float neutralWhiteLevel;

			// Token: 0x04002FB4 RID: 12212
			[Range(1f, 10f)]
			public float neutralWhiteClip;
		}

		// Token: 0x0200081D RID: 2077
		[Serializable]
		public struct BasicSettings
		{
			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06003B74 RID: 15220 RVA: 0x001CBD48 File Offset: 0x001CA148
			public static ColorGradingModel.BasicSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.BasicSettings
					{
						postExposure = 0f,
						temperature = 0f,
						tint = 0f,
						hueShift = 0f,
						saturation = 1f,
						contrast = 1f
					};
				}
			}

			// Token: 0x04002FB5 RID: 12213
			[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
			public float postExposure;

			// Token: 0x04002FB6 RID: 12214
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to a custom color temperature.")]
			public float temperature;

			// Token: 0x04002FB7 RID: 12215
			[Range(-100f, 100f)]
			[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
			public float tint;

			// Token: 0x04002FB8 RID: 12216
			[Range(-180f, 180f)]
			[Tooltip("Shift the hue of all colors.")]
			public float hueShift;

			// Token: 0x04002FB9 RID: 12217
			[Range(0f, 2f)]
			[Tooltip("Pushes the intensity of all colors.")]
			public float saturation;

			// Token: 0x04002FBA RID: 12218
			[Range(0f, 2f)]
			[Tooltip("Expands or shrinks the overall range of tonal values.")]
			public float contrast;
		}

		// Token: 0x0200081E RID: 2078
		[Serializable]
		public struct ChannelMixerSettings
		{
			// Token: 0x17000AB5 RID: 2741
			// (get) Token: 0x06003B75 RID: 15221 RVA: 0x001CBDA8 File Offset: 0x001CA1A8
			public static ColorGradingModel.ChannelMixerSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ChannelMixerSettings
					{
						red = new Vector3(1f, 0f, 0f),
						green = new Vector3(0f, 1f, 0f),
						blue = new Vector3(0f, 0f, 1f),
						currentEditingChannel = 0
					};
				}
			}

			// Token: 0x04002FBB RID: 12219
			public Vector3 red;

			// Token: 0x04002FBC RID: 12220
			public Vector3 green;

			// Token: 0x04002FBD RID: 12221
			public Vector3 blue;

			// Token: 0x04002FBE RID: 12222
			[HideInInspector]
			public int currentEditingChannel;
		}

		// Token: 0x0200081F RID: 2079
		[Serializable]
		public struct LogWheelsSettings
		{
			// Token: 0x17000AB6 RID: 2742
			// (get) Token: 0x06003B76 RID: 15222 RVA: 0x001CBE18 File Offset: 0x001CA218
			public static ColorGradingModel.LogWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LogWheelsSettings
					{
						slope = Color.clear,
						power = Color.clear,
						offset = Color.clear
					};
				}
			}

			// Token: 0x04002FBF RID: 12223
			[Trackball("GetSlopeValue")]
			public Color slope;

			// Token: 0x04002FC0 RID: 12224
			[Trackball("GetPowerValue")]
			public Color power;

			// Token: 0x04002FC1 RID: 12225
			[Trackball("GetOffsetValue")]
			public Color offset;
		}

		// Token: 0x02000820 RID: 2080
		[Serializable]
		public struct LinearWheelsSettings
		{
			// Token: 0x17000AB7 RID: 2743
			// (get) Token: 0x06003B77 RID: 15223 RVA: 0x001CBE54 File Offset: 0x001CA254
			public static ColorGradingModel.LinearWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.LinearWheelsSettings
					{
						lift = Color.clear,
						gamma = Color.clear,
						gain = Color.clear
					};
				}
			}

			// Token: 0x04002FC2 RID: 12226
			[Trackball("GetLiftValue")]
			public Color lift;

			// Token: 0x04002FC3 RID: 12227
			[Trackball("GetGammaValue")]
			public Color gamma;

			// Token: 0x04002FC4 RID: 12228
			[Trackball("GetGainValue")]
			public Color gain;
		}

		// Token: 0x02000821 RID: 2081
		public enum ColorWheelMode
		{
			// Token: 0x04002FC6 RID: 12230
			Linear,
			// Token: 0x04002FC7 RID: 12231
			Log
		}

		// Token: 0x02000822 RID: 2082
		[Serializable]
		public struct ColorWheelsSettings
		{
			// Token: 0x17000AB8 RID: 2744
			// (get) Token: 0x06003B78 RID: 15224 RVA: 0x001CBE90 File Offset: 0x001CA290
			public static ColorGradingModel.ColorWheelsSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.ColorWheelsSettings
					{
						mode = ColorGradingModel.ColorWheelMode.Log,
						log = ColorGradingModel.LogWheelsSettings.defaultSettings,
						linear = ColorGradingModel.LinearWheelsSettings.defaultSettings
					};
				}
			}

			// Token: 0x04002FC8 RID: 12232
			public ColorGradingModel.ColorWheelMode mode;

			// Token: 0x04002FC9 RID: 12233
			[TrackballGroup]
			public ColorGradingModel.LogWheelsSettings log;

			// Token: 0x04002FCA RID: 12234
			[TrackballGroup]
			public ColorGradingModel.LinearWheelsSettings linear;
		}

		// Token: 0x02000823 RID: 2083
		[Serializable]
		public struct CurvesSettings
		{
			// Token: 0x17000AB9 RID: 2745
			// (get) Token: 0x06003B79 RID: 15225 RVA: 0x001CBEC8 File Offset: 0x001CA2C8
			public static ColorGradingModel.CurvesSettings defaultSettings
			{
				get
				{
					return new ColorGradingModel.CurvesSettings
					{
						master = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						red = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						green = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						blue = new ColorGradingCurve(new AnimationCurve(new Keyframe[]
						{
							new Keyframe(0f, 0f, 1f, 1f),
							new Keyframe(1f, 1f, 1f, 1f)
						}), 0f, false, new Vector2(0f, 1f)),
						hueVShue = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						hueVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f)),
						satVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						lumVSsat = new ColorGradingCurve(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f)),
						e_CurrentEditingCurve = 0,
						e_CurveY = true,
						e_CurveR = false,
						e_CurveG = false,
						e_CurveB = false
					};
				}
			}

			// Token: 0x04002FCB RID: 12235
			public ColorGradingCurve master;

			// Token: 0x04002FCC RID: 12236
			public ColorGradingCurve red;

			// Token: 0x04002FCD RID: 12237
			public ColorGradingCurve green;

			// Token: 0x04002FCE RID: 12238
			public ColorGradingCurve blue;

			// Token: 0x04002FCF RID: 12239
			public ColorGradingCurve hueVShue;

			// Token: 0x04002FD0 RID: 12240
			public ColorGradingCurve hueVSsat;

			// Token: 0x04002FD1 RID: 12241
			public ColorGradingCurve satVSsat;

			// Token: 0x04002FD2 RID: 12242
			public ColorGradingCurve lumVSsat;

			// Token: 0x04002FD3 RID: 12243
			[HideInInspector]
			public int e_CurrentEditingCurve;

			// Token: 0x04002FD4 RID: 12244
			[HideInInspector]
			public bool e_CurveY;

			// Token: 0x04002FD5 RID: 12245
			[HideInInspector]
			public bool e_CurveR;

			// Token: 0x04002FD6 RID: 12246
			[HideInInspector]
			public bool e_CurveG;

			// Token: 0x04002FD7 RID: 12247
			[HideInInspector]
			public bool e_CurveB;
		}

		// Token: 0x02000824 RID: 2084
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000ABA RID: 2746
			// (get) Token: 0x06003B7A RID: 15226 RVA: 0x001CC178 File Offset: 0x001CA578
			public static ColorGradingModel.Settings defaultSettings
			{
				get
				{
					return new ColorGradingModel.Settings
					{
						tonemapping = ColorGradingModel.TonemappingSettings.defaultSettings,
						basic = ColorGradingModel.BasicSettings.defaultSettings,
						channelMixer = ColorGradingModel.ChannelMixerSettings.defaultSettings,
						colorWheels = ColorGradingModel.ColorWheelsSettings.defaultSettings,
						curves = ColorGradingModel.CurvesSettings.defaultSettings
					};
				}
			}

			// Token: 0x04002FD8 RID: 12248
			public ColorGradingModel.TonemappingSettings tonemapping;

			// Token: 0x04002FD9 RID: 12249
			public ColorGradingModel.BasicSettings basic;

			// Token: 0x04002FDA RID: 12250
			public ColorGradingModel.ChannelMixerSettings channelMixer;

			// Token: 0x04002FDB RID: 12251
			public ColorGradingModel.ColorWheelsSettings colorWheels;

			// Token: 0x04002FDC RID: 12252
			public ColorGradingModel.CurvesSettings curves;
		}
	}
}
