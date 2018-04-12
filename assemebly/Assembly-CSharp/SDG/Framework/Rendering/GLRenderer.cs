using System;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Rendering
{
	// Token: 0x020001F6 RID: 502
	public class GLRenderer : MonoBehaviour
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000F08 RID: 3848 RVA: 0x00066848 File Offset: 0x00064C48
		// (remove) Token: 0x06000F09 RID: 3849 RVA: 0x0006687C File Offset: 0x00064C7C
		public static event GLRenderHandler render;

		// Token: 0x06000F0A RID: 3850 RVA: 0x000668B0 File Offset: 0x00064CB0
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			Graphics.Blit(source, destination);
			RenderTexture.active = destination;
			if (!Level.isEditor)
			{
				RenderTexture.active = null;
				return;
			}
			if (!Level.isDevkit && (EditorUI.window == null || !EditorUI.window.isEnabled))
			{
				RenderTexture.active = null;
				return;
			}
			if (GLRenderer.render == null)
			{
				RenderTexture.active = null;
				return;
			}
			GL.PushMatrix();
			GLRenderer.render();
			GL.PopMatrix();
			RenderTexture.active = null;
		}
	}
}
