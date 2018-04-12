using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000175 RID: 373
	public abstract class DevkitSelectionToolInstantiationInfoBase : IDevkitSelectionToolInstantiationInfo
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x00059D89 File Offset: 0x00058189
		public DevkitSelectionToolInstantiationInfoBase()
		{
			this.scale = Vector3.one;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00059D9C File Offset: 0x0005819C
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00059DA4 File Offset: 0x000581A4
		public virtual Vector3 position { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00059DAD File Offset: 0x000581AD
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x00059DB5 File Offset: 0x000581B5
		public virtual Quaternion rotation { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00059DBE File Offset: 0x000581BE
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00059DC6 File Offset: 0x000581C6
		public virtual Vector3 scale { get; set; }

		// Token: 0x06000B3F RID: 2879
		public abstract void instantiate();
	}
}
