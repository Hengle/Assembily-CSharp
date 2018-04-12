using System;
using SDG.Framework.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace SDG.Unturned
{
	// Token: 0x02000483 RID: 1155
	public class DecalRenderer : MonoBehaviour
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x000A73A8 File Offset: 0x000A57A8
		protected void handleGLRender()
		{
			if (!DecalSystem.decalVisibilityGroup.isVisible)
			{
				return;
			}
			float num = 128f + GraphicsSettings.distance * 128f;
			foreach (Decal decal in DecalSystem.decalsDiffuse)
			{
				if (!(decal.material == null))
				{
					float num2 = num * decal.lodBias;
					float num3 = num2 * num2;
					if ((decal.transform.position - this.cam.transform.position).sqrMagnitude <= num3)
					{
						GLUtility.matrix = decal.transform.localToWorldMatrix;
						GLUtility.volumeHelper(decal.isSelected, DecalSystem.decalVisibilityGroup);
					}
				}
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000A7498 File Offset: 0x000A5898
		private void OnEnable()
		{
			this.cam = base.GetComponent<Camera>();
			if (this.cam != null && this.buffer == null)
			{
				this.buffer = new CommandBuffer();
				this.buffer.name = "Decals";
				this.cam.AddCommandBuffer(CameraEvent.BeforeLighting, this.buffer);
			}
			GLRenderer.render += this.handleGLRender;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000A750C File Offset: 0x000A590C
		public void OnDisable()
		{
			if (this.cam != null && this.buffer != null)
			{
				this.cam.RemoveCommandBuffer(CameraEvent.BeforeLighting, this.buffer);
				this.buffer = null;
			}
			GLRenderer.render -= this.handleGLRender;
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000A7560 File Offset: 0x000A5960
		private void OnPreRender()
		{
			if (this.cam == null || this.buffer == null)
			{
				return;
			}
			if (GraphicsSettings.renderMode != ERenderMode.DEFERRED)
			{
				return;
			}
			this.buffer.Clear();
			int nameID = Shader.PropertyToID("_NormalsCopy");
			this.buffer.GetTemporaryRT(nameID, -1, -1);
			this.buffer.Blit(BuiltinRenderTextureType.GBuffer2, nameID);
			float num = 128f + GraphicsSettings.distance * 128f;
			this.buffer.SetRenderTarget(DecalRenderer.DIFFUSE, BuiltinRenderTextureType.CameraTarget);
			foreach (Decal decal in DecalSystem.decalsDiffuse)
			{
				if (!(decal.material == null))
				{
					float num2 = num * decal.lodBias;
					float num3 = num2 * num2;
					if ((decal.transform.position - this.cam.transform.position).sqrMagnitude <= num3)
					{
						this.buffer.DrawMesh(this.cube, decal.transform.localToWorldMatrix, decal.material);
					}
				}
			}
		}

		// Token: 0x0400121B RID: 4635
		private static readonly RenderTargetIdentifier[] DIFFUSE = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x0400121C RID: 4636
		public Mesh cube;

		// Token: 0x0400121D RID: 4637
		private Camera cam;

		// Token: 0x0400121E RID: 4638
		private CommandBuffer buffer;
	}
}
