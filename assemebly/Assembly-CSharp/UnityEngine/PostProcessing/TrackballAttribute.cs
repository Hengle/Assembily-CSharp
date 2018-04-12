using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007DE RID: 2014
	public sealed class TrackballAttribute : PropertyAttribute
	{
		// Token: 0x06003AB5 RID: 15029 RVA: 0x001C61FD File Offset: 0x001C45FD
		public TrackballAttribute(string method)
		{
			this.method = method;
		}

		// Token: 0x04002E77 RID: 11895
		public readonly string method;
	}
}
