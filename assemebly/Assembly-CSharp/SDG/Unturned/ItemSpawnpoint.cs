using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000539 RID: 1337
	public class ItemSpawnpoint
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x000C781C File Offset: 0x000C5C1C
		public ItemSpawnpoint(byte newType, Vector3 newPoint)
		{
			this.type = newType;
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._node = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Item"))).transform;
				this.node.name = this.type.ToString();
				this.node.position = this.point;
				this.node.parent = LevelItems.models;
				this.node.GetComponent<Renderer>().material.color = LevelItems.tables[(int)this.type].color;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000C78CD File Offset: 0x000C5CCD
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000C78D5 File Offset: 0x000C5CD5
		public Transform node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000C78DD File Offset: 0x000C5CDD
		public void setEnabled(bool isEnabled)
		{
			this.node.transform.gameObject.SetActive(isEnabled);
		}

		// Token: 0x0400161E RID: 5662
		public byte type;

		// Token: 0x0400161F RID: 5663
		private Vector3 _point;

		// Token: 0x04001620 RID: 5664
		private Transform _node;
	}
}
