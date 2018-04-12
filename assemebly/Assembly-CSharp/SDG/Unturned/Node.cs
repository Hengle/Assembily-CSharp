using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000564 RID: 1380
	public class Node
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000C5481 File Offset: 0x000C3881
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000C5489 File Offset: 0x000C3889
		public ENodeType type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000C5491 File Offset: 0x000C3891
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000C5499 File Offset: 0x000C3899
		public void move(Vector3 newPoint)
		{
			this._point = newPoint;
			this.model.position = this.point;
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000C54B3 File Offset: 0x000C38B3
		public void setEnabled(bool isEnabled)
		{
			this.model.gameObject.SetActive(isEnabled);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000C54C6 File Offset: 0x000C38C6
		public void remove()
		{
			UnityEngine.Object.Destroy(this.model.gameObject);
		}

		// Token: 0x040017B9 RID: 6073
		protected Vector3 _point;

		// Token: 0x040017BA RID: 6074
		protected ENodeType _type;

		// Token: 0x040017BB RID: 6075
		protected Transform _model;
	}
}
