using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200082F RID: 2095
	[Serializable]
	public class ScreenSpaceReflectionModel : PostProcessingModel
	{
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x001CC3EA File Offset: 0x001CA7EA
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x001CC3F2 File Offset: 0x001CA7F2
		public ScreenSpaceReflectionModel.Settings settings
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

		// Token: 0x06003B92 RID: 15250 RVA: 0x001CC3FB File Offset: 0x001CA7FB
		public override void Reset()
		{
			this.m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;
		}

		// Token: 0x04002FFF RID: 12287
		[SerializeField]
		private ScreenSpaceReflectionModel.Settings m_Settings = ScreenSpaceReflectionModel.Settings.defaultSettings;

		// Token: 0x02000830 RID: 2096
		public enum SSRResolution
		{
			// Token: 0x04003001 RID: 12289
			High,
			// Token: 0x04003002 RID: 12290
			Low = 2
		}

		// Token: 0x02000831 RID: 2097
		public enum SSRReflectionBlendType
		{
			// Token: 0x04003004 RID: 12292
			PhysicallyBased,
			// Token: 0x04003005 RID: 12293
			Additive
		}

		// Token: 0x02000832 RID: 2098
		[Serializable]
		public struct IntensitySettings
		{
			// Token: 0x04003006 RID: 12294
			[Tooltip("Nonphysical multiplier for the SSR reflections. 1.0 is physically based.")]
			[Range(0f, 2f)]
			public float reflectionMultiplier;

			// Token: 0x04003007 RID: 12295
			[Tooltip("How far away from the maxDistance to begin fading SSR.")]
			[Range(0f, 1000f)]
			public float fadeDistance;

			// Token: 0x04003008 RID: 12296
			[Tooltip("Amplify Fresnel fade out. Increase if floor reflections look good close to the surface and bad farther 'under' the floor.")]
			[Range(0f, 1f)]
			public float fresnelFade;

			// Token: 0x04003009 RID: 12297
			[Tooltip("Higher values correspond to a faster Fresnel fade as the reflection changes from the grazing angle.")]
			[Range(0.1f, 10f)]
			public float fresnelFadePower;
		}

		// Token: 0x02000833 RID: 2099
		[Serializable]
		public struct ReflectionSettings
		{
			// Token: 0x0400300A RID: 12298
			[Tooltip("How the reflections are blended into the render.")]
			public ScreenSpaceReflectionModel.SSRReflectionBlendType blendType;

			// Token: 0x0400300B RID: 12299
			[Tooltip("Half resolution SSRR is much faster, but less accurate.")]
			public ScreenSpaceReflectionModel.SSRResolution reflectionQuality;

			// Token: 0x0400300C RID: 12300
			[Tooltip("Maximum reflection distance in world units.")]
			[Range(0.1f, 300f)]
			public float maxDistance;

			// Token: 0x0400300D RID: 12301
			[Tooltip("Max raytracing length.")]
			[Range(16f, 1024f)]
			public int iterationCount;

			// Token: 0x0400300E RID: 12302
			[Tooltip("Log base 2 of ray tracing coarse step size. Higher traces farther, lower gives better quality silhouettes.")]
			[Range(1f, 16f)]
			public int stepSize;

			// Token: 0x0400300F RID: 12303
			[Tooltip("Typical thickness of columns, walls, furniture, and other objects that reflection rays might pass behind.")]
			[Range(0.01f, 10f)]
			public float widthModifier;

			// Token: 0x04003010 RID: 12304
			[Tooltip("Blurriness of reflections.")]
			[Range(0.1f, 8f)]
			public float reflectionBlur;

			// Token: 0x04003011 RID: 12305
			[Tooltip("Enable for a performance gain in scenes where most glossy objects are horizontal, like floors, water, and tables. Leave on for scenes with glossy vertical objects.")]
			public bool reflectBackfaces;
		}

		// Token: 0x02000834 RID: 2100
		[Serializable]
		public struct ScreenEdgeMask
		{
			// Token: 0x04003012 RID: 12306
			[Tooltip("Higher = fade out SSRR near the edge of the screen so that reflections don't pop under camera motion.")]
			[Range(0f, 1f)]
			public float intensity;
		}

		// Token: 0x02000835 RID: 2101
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000AC4 RID: 2756
			// (get) Token: 0x06003B93 RID: 15251 RVA: 0x001CC408 File Offset: 0x001CA808
			public static ScreenSpaceReflectionModel.Settings defaultSettings
			{
				get
				{
					return new ScreenSpaceReflectionModel.Settings
					{
						reflection = new ScreenSpaceReflectionModel.ReflectionSettings
						{
							blendType = ScreenSpaceReflectionModel.SSRReflectionBlendType.PhysicallyBased,
							reflectionQuality = ScreenSpaceReflectionModel.SSRResolution.Low,
							maxDistance = 100f,
							iterationCount = 256,
							stepSize = 3,
							widthModifier = 0.5f,
							reflectionBlur = 1f,
							reflectBackfaces = false
						},
						intensity = new ScreenSpaceReflectionModel.IntensitySettings
						{
							reflectionMultiplier = 1f,
							fadeDistance = 100f,
							fresnelFade = 1f,
							fresnelFadePower = 1f
						},
						screenEdgeMask = new ScreenSpaceReflectionModel.ScreenEdgeMask
						{
							intensity = 0.03f
						}
					};
				}
			}

			// Token: 0x04003013 RID: 12307
			public ScreenSpaceReflectionModel.ReflectionSettings reflection;

			// Token: 0x04003014 RID: 12308
			public ScreenSpaceReflectionModel.IntensitySettings intensity;

			// Token: 0x04003015 RID: 12309
			public ScreenSpaceReflectionModel.ScreenEdgeMask screenEdgeMask;
		}
	}
}
