using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000840 RID: 2112
	public class PostProcessingContext
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x001CD1DA File Offset: 0x001CB5DA
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x001CD1E2 File Offset: 0x001CB5E2
		public bool interrupted { get; private set; }

		// Token: 0x06003BC3 RID: 15299 RVA: 0x001CD1EB File Offset: 0x001CB5EB
		public void Interrupt()
		{
			this.interrupted = true;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x001CD1F4 File Offset: 0x001CB5F4
		public PostProcessingContext Reset()
		{
			this.profile = null;
			this.camera = null;
			this.materialFactory = null;
			this.renderTextureFactory = null;
			this.interrupted = false;
			return this;
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x001CD21A File Offset: 0x001CB61A
		public bool isGBufferAvailable
		{
			get
			{
				return this.camera.actualRenderingPath == RenderingPath.DeferredShading;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x001CD22A File Offset: 0x001CB62A
		public bool isHdr
		{
			get
			{
				return this.camera.hdr;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x001CD237 File Offset: 0x001CB637
		public int width
		{
			get
			{
				return this.camera.pixelWidth;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x001CD244 File Offset: 0x001CB644
		public int height
		{
			get
			{
				return this.camera.pixelHeight;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x001CD251 File Offset: 0x001CB651
		public Rect viewport
		{
			get
			{
				return this.camera.rect;
			}
		}

		// Token: 0x04003043 RID: 12355
		public PostProcessingProfile profile;

		// Token: 0x04003044 RID: 12356
		public Camera camera;

		// Token: 0x04003045 RID: 12357
		public MaterialFactory materialFactory;

		// Token: 0x04003046 RID: 12358
		public RenderTextureFactory renderTextureFactory;
	}
}
