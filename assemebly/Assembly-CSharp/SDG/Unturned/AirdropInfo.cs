using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005A6 RID: 1446
	public class AirdropInfo
	{
		// Token: 0x0400193E RID: 6462
		public Transform model;

		// Token: 0x0400193F RID: 6463
		public ushort id;

		// Token: 0x04001940 RID: 6464
		public Vector3 state;

		// Token: 0x04001941 RID: 6465
		public Vector3 direction;

		// Token: 0x04001942 RID: 6466
		public float speed;

		// Token: 0x04001943 RID: 6467
		public float delay;

		// Token: 0x04001944 RID: 6468
		public float force;

		// Token: 0x04001945 RID: 6469
		public bool dropped;
	}
}
