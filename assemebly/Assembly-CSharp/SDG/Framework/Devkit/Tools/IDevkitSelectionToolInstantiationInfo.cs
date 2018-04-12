using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000179 RID: 377
	public interface IDevkitSelectionToolInstantiationInfo
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000B4F RID: 2895
		// (set) Token: 0x06000B50 RID: 2896
		Vector3 position { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000B51 RID: 2897
		// (set) Token: 0x06000B52 RID: 2898
		Quaternion rotation { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000B53 RID: 2899
		// (set) Token: 0x06000B54 RID: 2900
		Vector3 scale { get; set; }

		// Token: 0x06000B55 RID: 2901
		void instantiate();
	}
}
