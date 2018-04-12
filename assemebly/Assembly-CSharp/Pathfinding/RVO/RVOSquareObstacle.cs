using System;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000EC RID: 236
	[AddComponentMenu("Pathfinding/Local Avoidance/Square Obstacle (disabled)")]
	public class RVOSquareObstacle : RVOObstacle
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0004AD0D File Offset: 0x0004910D
		protected override bool StaticObstacle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0004AD10 File Offset: 0x00049110
		protected override bool ExecuteInEditor
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0004AD13 File Offset: 0x00049113
		protected override bool LocalCoordinates
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0004AD16 File Offset: 0x00049116
		protected override bool AreGizmosDirty()
		{
			return false;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0004AD1C File Offset: 0x0004911C
		protected override void CreateObstacles()
		{
			this.size.x = Mathf.Abs(this.size.x);
			this.size.y = Mathf.Abs(this.size.y);
			this.height = Mathf.Abs(this.height);
			Vector3[] array = new Vector3[]
			{
				new Vector3(1f, 0f, -1f),
				new Vector3(1f, 0f, 1f),
				new Vector3(-1f, 0f, 1f),
				new Vector3(-1f, 0f, -1f)
			};
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Scale(new Vector3(this.size.x, 0f, this.size.y));
			}
			base.AddObstacle(array, this.height);
		}

		// Token: 0x0400068A RID: 1674
		public float height = 1f;

		// Token: 0x0400068B RID: 1675
		public Vector2 size = Vector3.one;
	}
}
