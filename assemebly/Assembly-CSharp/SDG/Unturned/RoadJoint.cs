using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200056D RID: 1389
	public class RoadJoint
	{
		// Token: 0x0600264D RID: 9805 RVA: 0x000E1329 File Offset: 0x000DF729
		public RoadJoint(Vector3 vertex)
		{
			this.vertex = vertex;
			this.tangents = new Vector3[2];
			this.mode = ERoadMode.MIRROR;
			this.offset = 0f;
			this.ignoreTerrain = false;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000E135D File Offset: 0x000DF75D
		public RoadJoint(Vector3 vertex, Vector3[] tangents, ERoadMode mode, float offset, bool ignoreTerrain)
		{
			this.vertex = vertex;
			this.tangents = tangents;
			this.mode = mode;
			this.offset = offset;
			this.ignoreTerrain = ignoreTerrain;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000E138A File Offset: 0x000DF78A
		public Vector3 getTangent(int index)
		{
			return this.tangents[index];
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x000E13A0 File Offset: 0x000DF7A0
		public void setTangent(int index, Vector3 tangent)
		{
			this.tangents[index] = tangent;
			if (this.mode == ERoadMode.MIRROR)
			{
				this.tangents[1 - index] = -tangent;
			}
			else if (this.mode == ERoadMode.ALIGNED)
			{
				this.tangents[1 - index] = -tangent.normalized * this.tangents[1 - index].magnitude;
			}
		}

		// Token: 0x040017FA RID: 6138
		public Vector3 vertex;

		// Token: 0x040017FB RID: 6139
		private Vector3[] tangents;

		// Token: 0x040017FC RID: 6140
		public ERoadMode mode;

		// Token: 0x040017FD RID: 6141
		public float offset;

		// Token: 0x040017FE RID: 6142
		public bool ignoreTerrain;
	}
}
