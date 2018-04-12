using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004FB RID: 1275
	public class Passenger
	{
		// Token: 0x060022DA RID: 8922 RVA: 0x000C2F50 File Offset: 0x000C1350
		public Passenger(Transform newSeat, Transform newObj, Transform newTurretYaw, Transform newTurretPitch, Transform newTurretAim)
		{
			this._seat = newSeat;
			this._obj = newObj;
			this._turretYaw = newTurretYaw;
			this._turretPitch = newTurretPitch;
			this._turretAim = newTurretAim;
			if (this.turretYaw != null)
			{
				this.rotationYaw = this.turretYaw.localRotation;
			}
			if (this.turretPitch != null)
			{
				this.rotationPitch = this.turretPitch.localRotation;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x000C2FCC File Offset: 0x000C13CC
		public Transform seat
		{
			get
			{
				return this._seat;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000C2FD4 File Offset: 0x000C13D4
		public Transform obj
		{
			get
			{
				return this._obj;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x000C2FDC File Offset: 0x000C13DC
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x000C2FE4 File Offset: 0x000C13E4
		public Quaternion rotationYaw { get; private set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000C2FED File Offset: 0x000C13ED
		public Transform turretYaw
		{
			get
			{
				return this._turretYaw;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000C2FF5 File Offset: 0x000C13F5
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x000C2FFD File Offset: 0x000C13FD
		public Quaternion rotationPitch { get; private set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000C3006 File Offset: 0x000C1406
		public Transform turretPitch
		{
			get
			{
				return this._turretPitch;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000C300E File Offset: 0x000C140E
		public Transform turretAim
		{
			get
			{
				return this._turretAim;
			}
		}

		// Token: 0x040014EB RID: 5355
		public SteamPlayer player;

		// Token: 0x040014EC RID: 5356
		public TurretInfo turret;

		// Token: 0x040014ED RID: 5357
		private Transform _seat;

		// Token: 0x040014EE RID: 5358
		private Transform _obj;

		// Token: 0x040014F0 RID: 5360
		private Transform _turretYaw;

		// Token: 0x040014F2 RID: 5362
		private Transform _turretPitch;

		// Token: 0x040014F3 RID: 5363
		private Transform _turretAim;

		// Token: 0x040014F4 RID: 5364
		public byte[] state;
	}
}
