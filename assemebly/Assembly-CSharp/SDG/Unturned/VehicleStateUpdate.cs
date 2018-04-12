using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004F8 RID: 1272
	public struct VehicleStateUpdate
	{
		// Token: 0x0600223C RID: 8764 RVA: 0x000BBC47 File Offset: 0x000BA047
		public VehicleStateUpdate(Vector3 pos, Quaternion rot)
		{
			this.pos = pos;
			this.rot = rot;
		}

		// Token: 0x04001470 RID: 5232
		public Vector3 pos;

		// Token: 0x04001471 RID: 5233
		public Quaternion rot;
	}
}
