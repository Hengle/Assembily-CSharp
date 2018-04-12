using System;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007E0 RID: 2016
	public sealed class AmbientOcclusionComponent : PostProcessingComponentCommandBuffer<AmbientOcclusionModel>
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x001C629C File Offset: 0x001C469C
		private AmbientOcclusionComponent.OcclusionSource occlusionSource
		{
			get
			{
				if (this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility)
				{
					return AmbientOcclusionComponent.OcclusionSource.GBuffer;
				}
				if (base.model.settings.highPrecision && !this.context.isGBufferAvailable)
				{
					return AmbientOcclusionComponent.OcclusionSource.DepthTexture;
				}
				return AmbientOcclusionComponent.OcclusionSource.DepthNormalsTexture;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x001C6300 File Offset: 0x001C4700
		private bool ambientOnlySupported
		{
			get
			{
				return this.context.isHdr && base.model.settings.ambientOnly && this.context.isGBufferAvailable && !base.model.settings.forceForwardCompatibility;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x001C6360 File Offset: 0x001C4760
		public override bool active
		{
			get
			{
				return base.model.enabled && base.model.settings.intensity > 0f && !this.context.interrupted;
			}
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x001C63AC File Offset: 0x001C47AC
		public override DepthTextureMode GetCameraFlags()
		{
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			if (this.occlusionSource == AmbientOcclusionComponent.OcclusionSource.DepthTexture)
			{
				depthTextureMode |= DepthTextureMode.Depth;
			}
			if (this.occlusionSource != AmbientOcclusionComponent.OcclusionSource.GBuffer)
			{
				depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			return depthTextureMode;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x001C63DB File Offset: 0x001C47DB
		public override string GetName()
		{
			return "Ambient Occlusion";
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x001C63E2 File Offset: 0x001C47E2
		public override CameraEvent GetCameraEvent()
		{
			return (!this.ambientOnlySupported || this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion)) ? CameraEvent.BeforeImageEffectsOpaque : CameraEvent.BeforeReflections;
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x001C6414 File Offset: 0x001C4814
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			AmbientOcclusionModel.Settings settings = base.model.settings;
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Blit");
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Ambient Occlusion");
			material.shaderKeywords = null;
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Intensity, settings.intensity);
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Radius, settings.radius);
			material.SetFloat(AmbientOcclusionComponent.Uniforms._Downsample, (!settings.downsampling) ? 1f : 0.5f);
			material.SetInt(AmbientOcclusionComponent.Uniforms._SampleCount, (int)settings.sampleCount);
			int width = this.context.width;
			int height = this.context.height;
			int num = (!settings.downsampling) ? 1 : 2;
			int nameID = AmbientOcclusionComponent.Uniforms._OcclusionTexture1;
			cb.GetTemporaryRT(nameID, width / num, height / num, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.Blit(null, nameID, material, (int)this.occlusionSource);
			int occlusionTexture = AmbientOcclusionComponent.Uniforms._OcclusionTexture2;
			cb.GetTemporaryRT(occlusionTexture, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, occlusionTexture, material, (this.occlusionSource != AmbientOcclusionComponent.OcclusionSource.GBuffer) ? 3 : 4);
			cb.ReleaseTemporaryRT(nameID);
			nameID = AmbientOcclusionComponent.Uniforms._OcclusionTexture;
			cb.GetTemporaryRT(nameID, width, height, 0, FilterMode.Bilinear, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, occlusionTexture);
			cb.Blit(occlusionTexture, nameID, material, 5);
			cb.ReleaseTemporaryRT(occlusionTexture);
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.AmbientOcclusion))
			{
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget, material, 8);
				this.context.Interrupt();
			}
			else if (this.ambientOnlySupported)
			{
				cb.SetRenderTarget(this.m_MRT, BuiltinRenderTextureType.CameraTarget);
				cb.DrawMesh(GraphicsUtils.quad, Matrix4x4.identity, material, 0, 7);
			}
			else
			{
				RenderTextureFormat format = (!this.context.isHdr) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
				int tempRT = AmbientOcclusionComponent.Uniforms._TempRT;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear, format);
				cb.Blit(BuiltinRenderTextureType.CameraTarget, tempRT, mat, 0);
				cb.SetGlobalTexture(AmbientOcclusionComponent.Uniforms._MainTex, tempRT);
				cb.Blit(tempRT, BuiltinRenderTextureType.CameraTarget, material, 6);
				cb.ReleaseTemporaryRT(tempRT);
			}
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x04002E78 RID: 11896
		private const string k_BlitShaderString = "Hidden/Post FX/Blit";

		// Token: 0x04002E79 RID: 11897
		private const string k_ShaderString = "Hidden/Post FX/Ambient Occlusion";

		// Token: 0x04002E7A RID: 11898
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x020007E1 RID: 2017
		private static class Uniforms
		{
			// Token: 0x04002E7B RID: 11899
			internal static readonly int _Intensity = Shader.PropertyToID("_Intensity");

			// Token: 0x04002E7C RID: 11900
			internal static readonly int _Radius = Shader.PropertyToID("_Radius");

			// Token: 0x04002E7D RID: 11901
			internal static readonly int _Downsample = Shader.PropertyToID("_Downsample");

			// Token: 0x04002E7E RID: 11902
			internal static readonly int _SampleCount = Shader.PropertyToID("_SampleCount");

			// Token: 0x04002E7F RID: 11903
			internal static readonly int _OcclusionTexture1 = Shader.PropertyToID("_OcclusionTexture1");

			// Token: 0x04002E80 RID: 11904
			internal static readonly int _OcclusionTexture2 = Shader.PropertyToID("_OcclusionTexture2");

			// Token: 0x04002E81 RID: 11905
			internal static readonly int _OcclusionTexture = Shader.PropertyToID("_OcclusionTexture");

			// Token: 0x04002E82 RID: 11906
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04002E83 RID: 11907
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");
		}

		// Token: 0x020007E2 RID: 2018
		private enum OcclusionSource
		{
			// Token: 0x04002E85 RID: 11909
			DepthTexture,
			// Token: 0x04002E86 RID: 11910
			DepthNormalsTexture,
			// Token: 0x04002E87 RID: 11911
			GBuffer
		}
	}
}
