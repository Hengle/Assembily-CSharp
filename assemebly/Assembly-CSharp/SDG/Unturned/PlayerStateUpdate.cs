using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000632 RID: 1586
	public struct PlayerStateUpdate
	{
		// Token: 0x06002D32 RID: 11570 RVA: 0x001220D7 File Offset: 0x001204D7
		public PlayerStateUpdate(Vector3 pos, byte angle, byte rot)
		{
			this.pos = pos;
			this.angle = angle;
			this.rot = rot;
		}

		// Token: 0x04001D20 RID: 7456
		public Vector3 pos;

		// Token: 0x04001D21 RID: 7457
		public byte angle;

		// Token: 0x04001D22 RID: 7458
		public byte rot;
	}
}
