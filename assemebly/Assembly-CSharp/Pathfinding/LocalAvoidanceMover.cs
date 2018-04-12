using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007C RID: 124
	[RequireComponent(typeof(LocalAvoidance))]
	[Obsolete("Use the RVO system instead")]
	public class LocalAvoidanceMover : MonoBehaviour
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00020AFD File Offset: 0x0001EEFD
		private void Start()
		{
			this.targetPoint = base.transform.forward * this.targetPointDist + base.transform.position;
			this.controller = base.GetComponent<LocalAvoidance>();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00020B38 File Offset: 0x0001EF38
		private void Update()
		{
			if (this.controller != null)
			{
				this.controller.SimpleMove((this.targetPoint - base.transform.position).normalized * this.speed);
			}
		}

		// Token: 0x04000380 RID: 896
		public float targetPointDist = 10f;

		// Token: 0x04000381 RID: 897
		public float speed = 2f;

		// Token: 0x04000382 RID: 898
		private Vector3 targetPoint;

		// Token: 0x04000383 RID: 899
		private LocalAvoidance controller;
	}
}
