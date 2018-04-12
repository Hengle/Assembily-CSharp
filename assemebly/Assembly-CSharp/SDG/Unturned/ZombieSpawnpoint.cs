using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200057E RID: 1406
	public class ZombieSpawnpoint
	{
		// Token: 0x060026BC RID: 9916 RVA: 0x000E590C File Offset: 0x000E3D0C
		public ZombieSpawnpoint(byte newType, Vector3 newPoint)
		{
			this.type = newType;
			this._point = newPoint;
			if (Level.isEditor)
			{
				this._node = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Zombie"))).transform;
				this.node.name = this.type.ToString();
				this.node.position = this.point + Vector3.up;
				this.node.parent = LevelItems.models;
				this.node.GetComponent<Renderer>().material.color = LevelZombies.tables[(int)this.type].color;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000E59C7 File Offset: 0x000E3DC7
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x000E59CF File Offset: 0x000E3DCF
		public Transform node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000E59D7 File Offset: 0x000E3DD7
		public void setEnabled(bool isEnabled)
		{
			this.node.transform.gameObject.SetActive(isEnabled);
		}

		// Token: 0x0400184D RID: 6221
		public byte type;

		// Token: 0x0400184E RID: 6222
		private Vector3 _point;

		// Token: 0x0400184F RID: 6223
		private Transform _node;
	}
}
