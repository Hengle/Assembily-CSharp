using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.ViewportUI
{
	// Token: 0x0200029C RID: 668
	public class ViewportWindow : Sleek2Window
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0007D718 File Offset: 0x0007BB18
		public ViewportWindow()
		{
			base.gameObject.name = "Viewport";
			base.gameObject.tag = "Viewport";
			base.tab.label.textComponent.text = "Viewport";
			this.imageComponent = base.gameObject.AddComponent<RawImage>();
			this.viewport = base.gameObject.AddComponent<Viewport>();
			this.isAvailable = false;
			MainCamera.availabilityChanged += this.handleMainCameraAvailabilityChanged;
			DevkitWindowManager.activityChanged += this.handleDevkitVisibilityChanged;
			this.viewport.dimensionsChanged += this.handleViewportDimensionsChanged;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0007D7CA File Offset: 0x0007BBCA
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0007D7D2 File Offset: 0x0007BBD2
		public RawImage imageComponent { get; protected set; }

		// Token: 0x060013A6 RID: 5030 RVA: 0x0007D7DC File Offset: 0x0007BBDC
		public virtual void updateRenderTarget()
		{
			if (MainCamera.instance != null)
			{
				MainCamera.instance.targetTexture = null;
			}
			this.clearRenderTarget();
			if (this.imageComponent != null)
			{
				if (MainCamera.isAvailable && MainCamera.instance != null && DevkitWindowManager.isActive)
				{
					this.renderTarget = RenderTexture.GetTemporary((int)base.transform.rect.width, (int)base.transform.rect.height);
					MainCamera.instance.targetTexture = this.renderTarget;
				}
				this.imageComponent.texture = this.renderTarget;
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0007D893 File Offset: 0x0007BC93
		protected virtual void clearRenderTarget()
		{
			if (this.renderTarget != null)
			{
				RenderTexture.ReleaseTemporary(this.renderTarget);
				this.renderTarget = null;
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0007D8B8 File Offset: 0x0007BCB8
		protected virtual void handleMainCameraAvailabilityChanged()
		{
			this.updateRenderTarget();
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0007D8C0 File Offset: 0x0007BCC0
		protected virtual void handleDevkitVisibilityChanged()
		{
			this.updateRenderTarget();
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0007D8C8 File Offset: 0x0007BCC8
		protected virtual void handleViewportDimensionsChanged(Viewport viewport)
		{
			this.updateRenderTarget();
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0007D8D0 File Offset: 0x0007BCD0
		protected override void triggerDestroyed()
		{
			this.clearRenderTarget();
			MainCamera.instanceChanged -= this.handleMainCameraAvailabilityChanged;
			DevkitWindowManager.activityChanged -= this.handleDevkitVisibilityChanged;
			this.viewport.dimensionsChanged -= this.handleViewportDimensionsChanged;
			base.triggerDestroyed();
		}

		// Token: 0x04000B43 RID: 2883
		protected bool isAvailable;

		// Token: 0x04000B44 RID: 2884
		protected Viewport viewport;

		// Token: 0x04000B45 RID: 2885
		protected RenderTexture renderTarget;
	}
}
