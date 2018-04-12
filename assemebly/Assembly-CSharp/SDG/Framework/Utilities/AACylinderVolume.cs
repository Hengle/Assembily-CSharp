using System;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x020002FE RID: 766
	public struct AACylinderVolume : IShapeVolume
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x00084080 File Offset: 0x00082480
		public AACylinderVolume(Vector3 newCenter, float newRadius, float newHeight)
		{
			this.center = newCenter;
			this.radius = newRadius;
			this.height = newHeight;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00084098 File Offset: 0x00082498
		public bool containsPoint(Vector3 point)
		{
			float num = this.height / 2f;
			if (point.y > this.center.y - num && point.y < this.center.y + num)
			{
				float num2 = this.radius * this.radius;
				return (new Vector2(point.x, point.z) - new Vector2(this.center.x, this.center.z)).sqrMagnitude < num2;
			}
			return false;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00084134 File Offset: 0x00082534
		public Bounds worldBounds
		{
			get
			{
				float num = this.radius * 2f;
				return new Bounds(this.center, new Vector3(num, this.height, num));
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00084166 File Offset: 0x00082566
		public float internalVolume
		{
			get
			{
				return this.height * 3.14159274f * this.radius * this.radius;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00084182 File Offset: 0x00082582
		public float surfaceArea
		{
			get
			{
				return 3.14159274f * this.radius * this.radius;
			}
		}

		// Token: 0x04000C20 RID: 3104
		public Vector3 center;

		// Token: 0x04000C21 RID: 3105
		public float radius;

		// Token: 0x04000C22 RID: 3106
		public float height;
	}
}
