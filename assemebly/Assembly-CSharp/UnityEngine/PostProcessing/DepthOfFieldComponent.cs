using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007ED RID: 2029
	public sealed class DepthOfFieldComponent : PostProcessingComponentRenderTexture<DepthOfFieldModel>
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x001C8559 File Offset: 0x001C6959
		public override bool active
		{
			get
			{
				return base.model.enabled && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf) && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RHalf) && !this.context.interrupted;
			}
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x001C8593 File Offset: 0x001C6993
		public override DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x001C8598 File Offset: 0x001C6998
		private float CalculateFocalLength()
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			if (!settings.useCameraFov)
			{
				return settings.focalLength / 1000f;
			}
			float num = this.context.camera.fieldOfView * 0.0174532924f;
			return 0.012f / Mathf.Tan(0.5f * num);
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x001C85F4 File Offset: 0x001C69F4
		private float CalculateMaxCoCRadius(int screenHeight)
		{
			float num = (float)base.model.settings.kernelSize * 4f + 10f;
			return Mathf.Min(0.05f, num / (float)screenHeight);
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x001C8630 File Offset: 0x001C6A30
		public void Prepare(RenderTexture source, Material uberMaterial, bool antialiasCoC)
		{
			DepthOfFieldModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Depth Of Field");
			material.shaderKeywords = null;
			float num = settings.focusDistance;
			float num2 = this.CalculateFocalLength();
			num = Mathf.Max(num, num2);
			material.SetFloat(DepthOfFieldComponent.Uniforms._Distance, num);
			float num3 = num2 * num2 / (settings.aperture * (num - num2) * 0.024f * 2f);
			material.SetFloat(DepthOfFieldComponent.Uniforms._LensCoeff, num3);
			float num4 = this.CalculateMaxCoCRadius(source.height);
			material.SetFloat(DepthOfFieldComponent.Uniforms._MaxCoC, num4);
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpMaxCoC, 1f / num4);
			float value = (float)source.height / (float)source.width;
			material.SetFloat(DepthOfFieldComponent.Uniforms._RcpAspect, value);
			RenderTexture renderTexture = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			source.filterMode = FilterMode.Point;
			Graphics.Blit(source, renderTexture, material, 0);
			RenderTexture renderTexture2 = renderTexture;
			if (antialiasCoC)
			{
				renderTexture2 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
				if (this.m_CoCHistory == null || !this.m_CoCHistory.IsCreated() || this.m_CoCHistory.width != this.context.width / 2 || this.m_CoCHistory.height != this.context.height / 2)
				{
					this.m_CoCHistory = RenderTexture.GetTemporary(this.context.width / 2, this.context.height / 2, 0, RenderTextureFormat.RHalf);
					this.m_CoCHistory.filterMode = FilterMode.Point;
					this.m_CoCHistory.name = "CoC History";
					Graphics.Blit(renderTexture, this.m_CoCHistory, material, 6);
				}
				RenderTexture temporary = RenderTexture.GetTemporary(this.context.width / 2, this.context.height / 2, 0, RenderTextureFormat.RHalf);
				temporary.filterMode = FilterMode.Point;
				temporary.name = "CoC History";
				this.m_MRT[0] = renderTexture2.colorBuffer;
				this.m_MRT[1] = temporary.colorBuffer;
				material.SetTexture(DepthOfFieldComponent.Uniforms._MainTex, renderTexture);
				material.SetTexture(DepthOfFieldComponent.Uniforms._HistoryCoC, this.m_CoCHistory);
				Graphics.SetRenderTarget(this.m_MRT, renderTexture.depthBuffer);
				GraphicsUtils.Blit(material, 5);
				RenderTexture.ReleaseTemporary(this.m_CoCHistory);
				this.m_CoCHistory = temporary;
			}
			RenderTexture renderTexture3 = this.context.renderTextureFactory.Get(this.context.width / 2, this.context.height / 2, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Default, FilterMode.Bilinear, TextureWrapMode.Clamp, "FactoryTempTexture");
			Graphics.Blit(renderTexture2, renderTexture3, material, (int)(1 + settings.kernelSize));
			if (this.context.profile.debugViews.IsModeActive(BuiltinDebugViewsModel.Mode.FocusPlane))
			{
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldTex, renderTexture);
				uberMaterial.SetVector(DepthOfFieldComponent.Uniforms._DepthOfFieldParams, new Vector2(num, num3));
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD_COC_VIEW");
				this.context.Interrupt();
			}
			else
			{
				uberMaterial.SetTexture(DepthOfFieldComponent.Uniforms._DepthOfFieldTex, renderTexture3);
				uberMaterial.EnableKeyword("DEPTH_OF_FIELD");
			}
			if (antialiasCoC)
			{
				this.context.renderTextureFactory.Release(renderTexture2);
			}
			this.context.renderTextureFactory.Release(renderTexture);
			source.filterMode = FilterMode.Bilinear;
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x001C89D3 File Offset: 0x001C6DD3
		public override void OnDisable()
		{
			if (this.m_CoCHistory != null)
			{
				RenderTexture.ReleaseTemporary(this.m_CoCHistory);
			}
			this.m_CoCHistory = null;
		}

		// Token: 0x04002EC2 RID: 11970
		private const string k_ShaderString = "Hidden/Post FX/Depth Of Field";

		// Token: 0x04002EC3 RID: 11971
		private RenderTexture m_CoCHistory;

		// Token: 0x04002EC4 RID: 11972
		private RenderBuffer[] m_MRT = new RenderBuffer[2];

		// Token: 0x04002EC5 RID: 11973
		private const float k_FilmHeight = 0.024f;

		// Token: 0x020007EE RID: 2030
		private static class Uniforms
		{
			// Token: 0x04002EC6 RID: 11974
			internal static readonly int _DepthOfFieldTex = Shader.PropertyToID("_DepthOfFieldTex");

			// Token: 0x04002EC7 RID: 11975
			internal static readonly int _Distance = Shader.PropertyToID("_Distance");

			// Token: 0x04002EC8 RID: 11976
			internal static readonly int _LensCoeff = Shader.PropertyToID("_LensCoeff");

			// Token: 0x04002EC9 RID: 11977
			internal static readonly int _MaxCoC = Shader.PropertyToID("_MaxCoC");

			// Token: 0x04002ECA RID: 11978
			internal static readonly int _RcpMaxCoC = Shader.PropertyToID("_RcpMaxCoC");

			// Token: 0x04002ECB RID: 11979
			internal static readonly int _RcpAspect = Shader.PropertyToID("_RcpAspect");

			// Token: 0x04002ECC RID: 11980
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04002ECD RID: 11981
			internal static readonly int _HistoryCoC = Shader.PropertyToID("_HistoryCoC");

			// Token: 0x04002ECE RID: 11982
			internal static readonly int _DepthOfFieldParams = Shader.PropertyToID("_DepthOfFieldParams");
		}
	}
}
