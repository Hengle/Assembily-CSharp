using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007E5 RID: 2021
	public sealed class BuiltinDebugViewsComponent : PostProcessingComponentCommandBuffer<BuiltinDebugViewsModel>
	{
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x001C6C83 File Offset: 0x001C5083
		public override bool active
		{
			get
			{
				return base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Depth) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.Normals) || base.model.IsModeActive(BuiltinDebugViewsModel.Mode.MotionVectors);
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x001C6CB8 File Offset: 0x001C50B8
		public override DepthTextureMode GetCameraFlags()
		{
			BuiltinDebugViewsModel.Mode mode = base.model.settings.mode;
			DepthTextureMode depthTextureMode = DepthTextureMode.None;
			if (mode != BuiltinDebugViewsModel.Mode.Normals)
			{
				if (mode != BuiltinDebugViewsModel.Mode.MotionVectors)
				{
					if (mode == BuiltinDebugViewsModel.Mode.Depth)
					{
						depthTextureMode |= DepthTextureMode.Depth;
					}
				}
				else
				{
					depthTextureMode |= (DepthTextureMode.Depth | DepthTextureMode.MotionVectors);
				}
			}
			else
			{
				depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			return depthTextureMode;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x001C6D14 File Offset: 0x001C5114
		public override CameraEvent GetCameraEvent()
		{
			return (base.model.settings.mode != BuiltinDebugViewsModel.Mode.MotionVectors) ? CameraEvent.BeforeImageEffectsOpaque : CameraEvent.BeforeImageEffects;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x001C6D43 File Offset: 0x001C5143
		public override string GetName()
		{
			return "Builtin Debug Views";
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x001C6D4C File Offset: 0x001C514C
		public override void PopulateCommandBuffer(CommandBuffer cb)
		{
			BuiltinDebugViewsModel.Settings settings = base.model.settings;
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			material.shaderKeywords = null;
			if (this.context.isGBufferAvailable)
			{
				material.EnableKeyword("SOURCE_GBUFFER");
			}
			BuiltinDebugViewsModel.Mode mode = settings.mode;
			if (mode != BuiltinDebugViewsModel.Mode.Depth)
			{
				if (mode != BuiltinDebugViewsModel.Mode.Normals)
				{
					if (mode == BuiltinDebugViewsModel.Mode.MotionVectors)
					{
						this.MotionVectorsPass(cb);
					}
				}
				else
				{
					this.DepthNormalsPass(cb);
				}
			}
			else
			{
				this.DepthPass(cb);
			}
			this.context.Interrupt();
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x001C6DF0 File Offset: 0x001C51F0
		private void DepthPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.DepthSettings depth = base.model.settings.depth;
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._DepthScale, 1f / depth.scale);
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 0);
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x001C6E50 File Offset: 0x001C5250
		private void DepthNormalsPass(CommandBuffer cb)
		{
			Material mat = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			cb.Blit(null, BuiltinRenderTextureType.CameraTarget, mat, 1);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x001C6E84 File Offset: 0x001C5284
		private void MotionVectorsPass(CommandBuffer cb)
		{
			Material material = this.context.materialFactory.Get("Hidden/Post FX/Builtin Debug Views");
			BuiltinDebugViewsModel.MotionVectorsSettings motionVectors = base.model.settings.motionVectors;
			int nameID = BuiltinDebugViewsComponent.Uniforms._TempRT;
			cb.GetTemporaryRT(nameID, this.context.width, this.context.height, 0, FilterMode.Bilinear);
			cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.sourceOpacity);
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, BuiltinRenderTextureType.CameraTarget);
			cb.Blit(BuiltinRenderTextureType.CameraTarget, nameID, material, 2);
			if (motionVectors.motionImageOpacity > 0f && motionVectors.motionImageAmplitude > 0f)
			{
				int tempRT = BuiltinDebugViewsComponent.Uniforms._TempRT2;
				cb.GetTemporaryRT(tempRT, this.context.width, this.context.height, 0, FilterMode.Bilinear);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionImageOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionImageAmplitude);
				cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
				cb.Blit(nameID, tempRT, material, 3);
				cb.ReleaseTemporaryRT(nameID);
				nameID = tempRT;
			}
			if (motionVectors.motionVectorsOpacity > 0f && motionVectors.motionVectorsAmplitude > 0f)
			{
				this.PrepareArrows();
				float num = 1f / (float)motionVectors.motionVectorsResolution;
				float x = num * (float)this.context.height / (float)this.context.width;
				cb.SetGlobalVector(BuiltinDebugViewsComponent.Uniforms._Scale, new Vector2(x, num));
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Opacity, motionVectors.motionVectorsOpacity);
				cb.SetGlobalFloat(BuiltinDebugViewsComponent.Uniforms._Amplitude, motionVectors.motionVectorsAmplitude);
				cb.DrawMesh(this.m_Arrows.mesh, Matrix4x4.identity, material, 0, 4);
			}
			cb.SetGlobalTexture(BuiltinDebugViewsComponent.Uniforms._MainTex, nameID);
			cb.Blit(nameID, BuiltinRenderTextureType.CameraTarget);
			cb.ReleaseTemporaryRT(nameID);
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x001C708C File Offset: 0x001C548C
		private void PrepareArrows()
		{
			int motionVectorsResolution = base.model.settings.motionVectors.motionVectorsResolution;
			int num = motionVectorsResolution * Screen.width / Screen.height;
			if (this.m_Arrows == null)
			{
				this.m_Arrows = new BuiltinDebugViewsComponent.ArrowArray();
			}
			if (this.m_Arrows.columnCount != num || this.m_Arrows.rowCount != motionVectorsResolution)
			{
				this.m_Arrows.Release();
				this.m_Arrows.BuildMesh(num, motionVectorsResolution);
			}
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x001C7110 File Offset: 0x001C5510
		public override void OnDisable()
		{
			if (this.m_Arrows != null)
			{
				this.m_Arrows.Release();
			}
			this.m_Arrows = null;
		}

		// Token: 0x04002E95 RID: 11925
		private const string k_ShaderString = "Hidden/Post FX/Builtin Debug Views";

		// Token: 0x04002E96 RID: 11926
		private BuiltinDebugViewsComponent.ArrowArray m_Arrows;

		// Token: 0x020007E6 RID: 2022
		private static class Uniforms
		{
			// Token: 0x04002E97 RID: 11927
			internal static readonly int _DepthScale = Shader.PropertyToID("_DepthScale");

			// Token: 0x04002E98 RID: 11928
			internal static readonly int _TempRT = Shader.PropertyToID("_TempRT");

			// Token: 0x04002E99 RID: 11929
			internal static readonly int _Opacity = Shader.PropertyToID("_Opacity");

			// Token: 0x04002E9A RID: 11930
			internal static readonly int _MainTex = Shader.PropertyToID("_MainTex");

			// Token: 0x04002E9B RID: 11931
			internal static readonly int _TempRT2 = Shader.PropertyToID("_TempRT2");

			// Token: 0x04002E9C RID: 11932
			internal static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");

			// Token: 0x04002E9D RID: 11933
			internal static readonly int _Scale = Shader.PropertyToID("_Scale");
		}

		// Token: 0x020007E7 RID: 2023
		private enum Pass
		{
			// Token: 0x04002E9F RID: 11935
			Depth,
			// Token: 0x04002EA0 RID: 11936
			Normals,
			// Token: 0x04002EA1 RID: 11937
			MovecOpacity,
			// Token: 0x04002EA2 RID: 11938
			MovecImaging,
			// Token: 0x04002EA3 RID: 11939
			MovecArrows
		}

		// Token: 0x020007E8 RID: 2024
		private class ArrowArray
		{
			// Token: 0x17000A8E RID: 2702
			// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x001C71AE File Offset: 0x001C55AE
			// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x001C71B6 File Offset: 0x001C55B6
			public Mesh mesh { get; private set; }

			// Token: 0x17000A8F RID: 2703
			// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x001C71BF File Offset: 0x001C55BF
			// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x001C71C7 File Offset: 0x001C55C7
			public int columnCount { get; private set; }

			// Token: 0x17000A90 RID: 2704
			// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x001C71D0 File Offset: 0x001C55D0
			// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x001C71D8 File Offset: 0x001C55D8
			public int rowCount { get; private set; }

			// Token: 0x06003AD7 RID: 15063 RVA: 0x001C71E4 File Offset: 0x001C55E4
			public void BuildMesh(int columns, int rows)
			{
				Vector3[] array = new Vector3[]
				{
					new Vector3(0f, 0f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 0f)
				};
				int num = 6 * columns * rows;
				List<Vector3> list = new List<Vector3>(num);
				List<Vector2> list2 = new List<Vector2>(num);
				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < columns; j++)
					{
						Vector2 item = new Vector2((0.5f + (float)j) / (float)columns, (0.5f + (float)i) / (float)rows);
						for (int k = 0; k < 6; k++)
						{
							list.Add(array[k]);
							list2.Add(item);
						}
					}
				}
				int[] array2 = new int[num];
				for (int l = 0; l < num; l++)
				{
					array2[l] = l;
				}
				this.mesh = new Mesh
				{
					hideFlags = HideFlags.DontSave
				};
				this.mesh.SetVertices(list);
				this.mesh.SetUVs(0, list2);
				this.mesh.SetIndices(array2, MeshTopology.Lines, 0);
				this.mesh.UploadMeshData(true);
				this.columnCount = columns;
				this.rowCount = rows;
			}

			// Token: 0x06003AD8 RID: 15064 RVA: 0x001C73C7 File Offset: 0x001C57C7
			public void Release()
			{
				GraphicsUtils.Destroy(this.mesh);
				this.mesh = null;
			}
		}
	}
}
