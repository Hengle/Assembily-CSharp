using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000D8 RID: 216
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	public class RelevantGraphSurface : MonoBehaviour
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0004619B File Offset: 0x0004459B
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x000461A3 File Offset: 0x000445A3
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000461AB File Offset: 0x000445AB
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000461B3 File Offset: 0x000445B3
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000461BA File Offset: 0x000445BA
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000461CD File Offset: 0x000445CD
		private void OnEnable()
		{
			this.UpdatePosition();
			if (RelevantGraphSurface.root == null)
			{
				RelevantGraphSurface.root = this;
			}
			else
			{
				this.next = RelevantGraphSurface.root;
				RelevantGraphSurface.root.prev = this;
				RelevantGraphSurface.root = this;
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0004620C File Offset: 0x0004460C
		private void OnDisable()
		{
			if (RelevantGraphSurface.root == this)
			{
				RelevantGraphSurface.root = this.next;
				if (RelevantGraphSurface.root != null)
				{
					RelevantGraphSurface.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000462A8 File Offset: 0x000446A8
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000462DC File Offset: 0x000446DC
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = UnityEngine.Object.FindObjectsOfType(typeof(RelevantGraphSurface)) as RelevantGraphSurface[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00046324 File Offset: 0x00044724
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f, 0.4f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00046394 File Offset: 0x00044794
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.223529413f, 0.827451f, 0.180392161f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x0400060C RID: 1548
		private static RelevantGraphSurface root;

		// Token: 0x0400060D RID: 1549
		public float maxRange = 1f;

		// Token: 0x0400060E RID: 1550
		private RelevantGraphSurface prev;

		// Token: 0x0400060F RID: 1551
		private RelevantGraphSurface next;

		// Token: 0x04000610 RID: 1552
		private Vector3 position;
	}
}
