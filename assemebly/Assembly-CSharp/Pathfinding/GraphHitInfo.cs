using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000051 RID: 81
	public struct GraphHitInfo
	{
		// Token: 0x06000358 RID: 856 RVA: 0x0001A963 File Offset: 0x00018D63
		public GraphHitInfo(Vector3 point)
		{
			this.tangentOrigin = Vector3.zero;
			this.origin = Vector3.zero;
			this.point = point;
			this.node = null;
			this.tangent = Vector3.zero;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001A994 File Offset: 0x00018D94
		public float distance
		{
			get
			{
				return (this.point - this.origin).magnitude;
			}
		}

		// Token: 0x04000298 RID: 664
		public Vector3 origin;

		// Token: 0x04000299 RID: 665
		public Vector3 point;

		// Token: 0x0400029A RID: 666
		public GraphNode node;

		// Token: 0x0400029B RID: 667
		public Vector3 tangentOrigin;

		// Token: 0x0400029C RID: 668
		public Vector3 tangent;
	}
}
