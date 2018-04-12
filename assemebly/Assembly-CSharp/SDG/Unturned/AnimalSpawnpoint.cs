using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200051B RID: 1307
	public class AnimalSpawnpoint
	{
		// Token: 0x06002385 RID: 9093 RVA: 0x000C5580 File Offset: 0x000C3980
		public AnimalSpawnpoint(byte newType, Vector3 newPoint)
		{
			this.type = newType;
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._node = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Animal"))).transform;
				this.node.name = this.type.ToString();
				this.node.position = this.point;
				this.node.parent = LevelAnimals.models;
				this.node.GetComponent<Renderer>().material.color = LevelAnimals.tables[(int)this.type].color;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000C5631 File Offset: 0x000C3A31
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000C5639 File Offset: 0x000C3A39
		public Transform node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000C5641 File Offset: 0x000C3A41
		public void setEnabled(bool isEnabled)
		{
			this.node.transform.gameObject.SetActive(isEnabled);
		}

		// Token: 0x04001580 RID: 5504
		public byte type;

		// Token: 0x04001581 RID: 5505
		private Vector3 _point;

		// Token: 0x04001582 RID: 5506
		private Transform _node;
	}
}
