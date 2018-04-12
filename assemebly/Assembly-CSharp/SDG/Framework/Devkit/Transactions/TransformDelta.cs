using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000182 RID: 386
	public class TransformDelta
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0005AB6F File Offset: 0x00058F6F
		public TransformDelta(Transform newParent)
		{
			this.parent = newParent;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0005AB7E File Offset: 0x00058F7E
		public void get(Transform transform)
		{
			this.localPosition = transform.localPosition;
			this.localRotation = transform.localRotation;
			this.localScale = transform.localScale;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0005ABA4 File Offset: 0x00058FA4
		public void set(Transform transform)
		{
			transform.parent = this.parent;
			transform.localPosition = this.localPosition;
			transform.localRotation = this.localRotation;
			transform.localScale = this.localScale;
		}

		// Token: 0x0400084A RID: 2122
		public Transform parent;

		// Token: 0x0400084B RID: 2123
		public Vector3 localPosition;

		// Token: 0x0400084C RID: 2124
		public Quaternion localRotation;

		// Token: 0x0400084D RID: 2125
		public Vector3 localScale;
	}
}
