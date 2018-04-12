using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000745 RID: 1861
	public class RaycastInfo
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x00157A68 File Offset: 0x00155E68
		public RaycastInfo(RaycastHit hit)
		{
			this.transform = hit.transform;
			this.collider = hit.collider;
			this.distance = hit.distance;
			this.point = hit.point;
			this.normal = hit.normal;
			this.section = byte.MaxValue;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00157AC7 File Offset: 0x00155EC7
		public RaycastInfo(Transform hit)
		{
			this.transform = hit;
			this.point = hit.position;
			this.section = byte.MaxValue;
		}

		// Token: 0x040023A8 RID: 9128
		public Transform transform;

		// Token: 0x040023A9 RID: 9129
		public Collider collider;

		// Token: 0x040023AA RID: 9130
		public float distance;

		// Token: 0x040023AB RID: 9131
		public Vector3 point;

		// Token: 0x040023AC RID: 9132
		public Vector3 direction;

		// Token: 0x040023AD RID: 9133
		public Vector3 normal;

		// Token: 0x040023AE RID: 9134
		public Player player;

		// Token: 0x040023AF RID: 9135
		public Zombie zombie;

		// Token: 0x040023B0 RID: 9136
		public Animal animal;

		// Token: 0x040023B1 RID: 9137
		public ELimb limb;

		// Token: 0x040023B2 RID: 9138
		public EPhysicsMaterial material;

		// Token: 0x040023B3 RID: 9139
		public InteractableVehicle vehicle;

		// Token: 0x040023B4 RID: 9140
		public byte section;
	}
}
