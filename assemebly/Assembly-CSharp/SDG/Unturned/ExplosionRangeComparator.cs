using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200072A RID: 1834
	public class ExplosionRangeComparator : IComparer<Transform>
	{
		// Token: 0x060033CD RID: 13261 RVA: 0x00150858 File Offset: 0x0014EC58
		public int Compare(Transform a, Transform b)
		{
			return Mathf.RoundToInt(((a.position - this.point).sqrMagnitude - (b.position - this.point).sqrMagnitude) * 100f);
		}

		// Token: 0x0400233B RID: 9019
		public Vector3 point;
	}
}
