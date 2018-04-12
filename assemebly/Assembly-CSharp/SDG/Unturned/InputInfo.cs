using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000734 RID: 1844
	public class InputInfo
	{
		// Token: 0x04002373 RID: 9075
		public ERaycastInfoType type;

		// Token: 0x04002374 RID: 9076
		public Vector3 point;

		// Token: 0x04002375 RID: 9077
		public Vector3 direction;

		// Token: 0x04002376 RID: 9078
		public Vector3 normal;

		// Token: 0x04002377 RID: 9079
		public Player player;

		// Token: 0x04002378 RID: 9080
		public Zombie zombie;

		// Token: 0x04002379 RID: 9081
		public Animal animal;

		// Token: 0x0400237A RID: 9082
		public ELimb limb;

		// Token: 0x0400237B RID: 9083
		public EPhysicsMaterial material;

		// Token: 0x0400237C RID: 9084
		public InteractableVehicle vehicle;

		// Token: 0x0400237D RID: 9085
		public Transform transform;

		// Token: 0x0400237E RID: 9086
		public byte section;
	}
}
