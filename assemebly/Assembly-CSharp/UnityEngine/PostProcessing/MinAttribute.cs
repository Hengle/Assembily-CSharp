using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x020007DD RID: 2013
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x06003AB4 RID: 15028 RVA: 0x001C61EE File Offset: 0x001C45EE
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04002E76 RID: 11894
		public readonly float min;
	}
}
