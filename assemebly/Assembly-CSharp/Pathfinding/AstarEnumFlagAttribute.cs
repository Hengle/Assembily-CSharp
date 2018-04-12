using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200001C RID: 28
	public class AstarEnumFlagAttribute : PropertyAttribute
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000FD84 File Offset: 0x0000E184
		public AstarEnumFlagAttribute()
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000FD8C File Offset: 0x0000E18C
		public AstarEnumFlagAttribute(string name)
		{
			this.enumName = name;
		}

		// Token: 0x04000134 RID: 308
		public string enumName;
	}
}
