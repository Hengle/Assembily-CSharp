using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000841 RID: 2113
	[Serializable]
	public abstract class PostProcessingModel
	{
		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x001CB5C8 File Offset: 0x001C99C8
		// (set) Token: 0x06003BCC RID: 15308 RVA: 0x001CB5D0 File Offset: 0x001C99D0
		public bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				if (value)
				{
					this.OnValidate();
				}
			}
		}

		// Token: 0x06003BCD RID: 15309
		public abstract void Reset();

		// Token: 0x06003BCE RID: 15310 RVA: 0x001CB5E5 File Offset: 0x001C99E5
		public virtual void OnValidate()
		{
		}

		// Token: 0x04003048 RID: 12360
		[SerializeField]
		[GetSet("enabled")]
		private bool m_Enabled;
	}
}
