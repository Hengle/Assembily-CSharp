using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200083C RID: 2108
	public abstract class PostProcessingComponentBase
	{
		// Token: 0x06003BB0 RID: 15280 RVA: 0x001C621C File Offset: 0x001C461C
		public virtual DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.None;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003BB1 RID: 15281
		public abstract bool active { get; }

		// Token: 0x06003BB2 RID: 15282 RVA: 0x001C621F File Offset: 0x001C461F
		public virtual void OnEnable()
		{
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x001C6221 File Offset: 0x001C4621
		public virtual void OnDisable()
		{
		}

		// Token: 0x06003BB4 RID: 15284
		public abstract PostProcessingModel GetModel();

		// Token: 0x04003041 RID: 12353
		public PostProcessingContext context;
	}
}
