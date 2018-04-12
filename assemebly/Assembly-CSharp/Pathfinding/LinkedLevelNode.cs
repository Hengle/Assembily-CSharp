using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008B RID: 139
	public class LinkedLevelNode
	{
		// Token: 0x040003F6 RID: 1014
		public Vector3 position;

		// Token: 0x040003F7 RID: 1015
		public bool walkable;

		// Token: 0x040003F8 RID: 1016
		public RaycastHit hit;

		// Token: 0x040003F9 RID: 1017
		public float height;

		// Token: 0x040003FA RID: 1018
		public LinkedLevelNode next;
	}
}
