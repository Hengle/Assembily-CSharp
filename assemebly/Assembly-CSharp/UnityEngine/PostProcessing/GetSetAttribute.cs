using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007DC RID: 2012
	public sealed class GetSetAttribute : PropertyAttribute
	{
		// Token: 0x06003AB3 RID: 15027 RVA: 0x001C61DF File Offset: 0x001C45DF
		public GetSetAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x04002E74 RID: 11892
		public readonly string name;

		// Token: 0x04002E75 RID: 11893
		public bool dirty;
	}
}
