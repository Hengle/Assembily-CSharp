using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x0200083D RID: 2109
	public abstract class PostProcessingComponent<T> : PostProcessingComponentBase where T : PostProcessingModel
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x001C622B File Offset: 0x001C462B
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x001C6233 File Offset: 0x001C4633
		public T model { get; internal set; }

		// Token: 0x06003BB8 RID: 15288 RVA: 0x001C623C File Offset: 0x001C463C
		public void Init(PostProcessingContext pcontext, T pmodel)
		{
			this.context = pcontext;
			this.model = pmodel;
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x001C624C File Offset: 0x001C464C
		public override PostProcessingModel GetModel()
		{
			return this.model;
		}
	}
}
