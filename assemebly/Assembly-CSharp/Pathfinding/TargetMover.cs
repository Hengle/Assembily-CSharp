using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000080 RID: 128
	public class TargetMover : MonoBehaviour
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0002119C File Offset: 0x0001F59C
		public void Start()
		{
			this.cam = Camera.main;
			this.ais = (UnityEngine.Object.FindObjectsOfType(typeof(RichAI)) as RichAI[]);
			this.ais2 = (UnityEngine.Object.FindObjectsOfType(typeof(AIPath)) as AIPath[]);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000211E8 File Offset: 0x0001F5E8
		public void OnGUI()
		{
			if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00021236 File Offset: 0x0001F636
		private void Update()
		{
			if (!this.onlyOnDoubleClick && this.cam != null)
			{
				this.UpdateTargetPosition();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0002125C File Offset: 0x0001F65C
		public void UpdateTargetPosition()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask) && raycastHit.point != this.target.position)
			{
				this.target.position = raycastHit.point;
				if (this.ais != null && this.onlyOnDoubleClick)
				{
					for (int i = 0; i < this.ais.Length; i++)
					{
						if (this.ais[i] != null)
						{
							this.ais[i].UpdatePath();
						}
					}
				}
				if (this.ais2 != null && this.onlyOnDoubleClick)
				{
					for (int j = 0; j < this.ais2.Length; j++)
					{
						if (this.ais2[j] != null)
						{
							this.ais2[j].SearchPath();
						}
					}
				}
			}
		}

		// Token: 0x0400038E RID: 910
		public LayerMask mask;

		// Token: 0x0400038F RID: 911
		public Transform target;

		// Token: 0x04000390 RID: 912
		private RichAI[] ais;

		// Token: 0x04000391 RID: 913
		private AIPath[] ais2;

		// Token: 0x04000392 RID: 914
		public bool onlyOnDoubleClick;

		// Token: 0x04000393 RID: 915
		private Camera cam;
	}
}
