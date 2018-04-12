using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200083F RID: 2111
	public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T> where T : PostProcessingModel
	{
		// Token: 0x06003BBF RID: 15295 RVA: 0x001C676C File Offset: 0x001C4B6C
		public virtual void Prepare(Material material)
		{
		}
	}
}
