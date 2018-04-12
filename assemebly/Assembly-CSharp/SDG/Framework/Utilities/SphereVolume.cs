using System;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x0200030C RID: 780
	public struct SphereVolume : IShapeVolume
	{
		// Token: 0x06001645 RID: 5701 RVA: 0x00084907 File Offset: 0x00082D07
		public SphereVolume(Vector3 newCenter, float newRadius)
		{
			this.center = newCenter;
			this.radius = newRadius;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00084918 File Offset: 0x00082D18
		public bool containsPoint(Vector3 point)
		{
			if (Mathf.Abs(point.x - this.center.x) >= this.radius)
			{
				return false;
			}
			if (Mathf.Abs(point.y - this.center.y) >= this.radius)
			{
				return false;
			}
			if (Mathf.Abs(point.z - this.center.z) >= this.radius)
			{
				return false;
			}
			float num = this.radius * this.radius;
			return (point - this.center).sqrMagnitude < num;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000849BC File Offset: 0x00082DBC
		public Bounds worldBounds
		{
			get
			{
				float num = this.radius * 2f;
				return new Bounds(this.center, new Vector3(num, num, num));
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000849E9 File Offset: 0x00082DE9
		public float internalVolume
		{
			get
			{
				return 4.18879032f * this.radius * this.radius * this.radius;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00084A05 File Offset: 0x00082E05
		public float surfaceArea
		{
			get
			{
				return 12.566371f * this.radius * this.radius;
			}
		}

		// Token: 0x04000C32 RID: 3122
		public Vector3 center;

		// Token: 0x04000C33 RID: 3123
		public float radius;
	}
}
