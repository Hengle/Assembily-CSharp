using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000728 RID: 1832
	public class DamageInfo
	{
		// Token: 0x060033CB RID: 13259 RVA: 0x00150800 File Offset: 0x0014EC00
		public void update(RaycastHit hit)
		{
			this.transform = hit.transform;
			this.collider = hit.collider;
			this.distance = hit.distance;
			this.point = hit.point;
			this.normal = hit.normal;
		}

		// Token: 0x0400232E RID: 9006
		public Transform transform;

		// Token: 0x0400232F RID: 9007
		public Collider collider;

		// Token: 0x04002330 RID: 9008
		public float distance;

		// Token: 0x04002331 RID: 9009
		public Vector3 point;

		// Token: 0x04002332 RID: 9010
		public Vector3 normal;

		// Token: 0x04002333 RID: 9011
		public Player player;

		// Token: 0x04002334 RID: 9012
		public Zombie zombie;

		// Token: 0x04002335 RID: 9013
		public InteractableVehicle vehicle;
	}
}
