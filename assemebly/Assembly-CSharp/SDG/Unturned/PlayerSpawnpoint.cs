using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000566 RID: 1382
	public class PlayerSpawnpoint
	{
		// Token: 0x06002620 RID: 9760 RVA: 0x000E0064 File Offset: 0x000DE464
		public PlayerSpawnpoint(Vector3 newPoint, float newAngle, bool newIsAlt)
		{
			this._point = newPoint;
			this._angle = newAngle;
			this._isAlt = newIsAlt;
			if (Level.isEditor)
			{
				this._node = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load((!this.isAlt) ? "Edit/Player" : "Edit/Player_Alt"))).transform;
				this.node.name = "Player";
				this.node.position = this.point;
				this.node.rotation = Quaternion.Euler(0f, this.angle, 0f);
				this.node.parent = LevelPlayers.models;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x000E011B File Offset: 0x000DE51B
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x000E0123 File Offset: 0x000DE523
		public float angle
		{
			get
			{
				return this._angle;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x000E012B File Offset: 0x000DE52B
		public bool isAlt
		{
			get
			{
				return this._isAlt;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x000E0133 File Offset: 0x000DE533
		public Transform node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000E013B File Offset: 0x000DE53B
		public void setEnabled(bool isEnabled)
		{
			this.node.transform.gameObject.SetActive(isEnabled);
		}

		// Token: 0x040017C9 RID: 6089
		private Vector3 _point;

		// Token: 0x040017CA RID: 6090
		private float _angle;

		// Token: 0x040017CB RID: 6091
		private bool _isAlt;

		// Token: 0x040017CC RID: 6092
		private Transform _node;
	}
}
