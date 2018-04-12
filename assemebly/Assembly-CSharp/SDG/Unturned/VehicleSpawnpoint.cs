using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000574 RID: 1396
	public class VehicleSpawnpoint
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x000E418C File Offset: 0x000E258C
		public VehicleSpawnpoint(byte newType, Vector3 newPoint, float newAngle)
		{
			this.type = newType;
			this._point = newPoint;
			this._angle = newAngle;
			if (Level.isEditor)
			{
				this._node = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Vehicle"))).transform;
				this.node.name = this.type.ToString();
				this.node.position = this.point;
				this.node.rotation = Quaternion.Euler(0f, this.angle, 0f);
				this.node.parent = LevelVehicles.models;
				this.node.GetComponent<Renderer>().material.color = LevelVehicles.tables[(int)this.type].color;
				this.node.FindChild("Arrow").GetComponent<Renderer>().material.color = LevelVehicles.tables[(int)this.type].color;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x000E4298 File Offset: 0x000E2698
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x000E42A0 File Offset: 0x000E26A0
		public float angle
		{
			get
			{
				return this._angle;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x000E42A8 File Offset: 0x000E26A8
		public Transform node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000E42B0 File Offset: 0x000E26B0
		public void setEnabled(bool isEnabled)
		{
			this.node.transform.gameObject.SetActive(isEnabled);
		}

		// Token: 0x0400181E RID: 6174
		public byte type;

		// Token: 0x0400181F RID: 6175
		private Vector3 _point;

		// Token: 0x04001820 RID: 6176
		private float _angle;

		// Token: 0x04001821 RID: 6177
		private Transform _node;
	}
}
