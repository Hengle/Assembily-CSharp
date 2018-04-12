using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005DA RID: 1498
	public struct YawSnapshotInfo : ISnapshotInfo
	{
		// Token: 0x06002A4D RID: 10829 RVA: 0x00107B7A File Offset: 0x00105F7A
		public YawSnapshotInfo(Vector3 pos, float yaw)
		{
			this.pos = pos;
			this.yaw = yaw;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00107B8C File Offset: 0x00105F8C
		public ISnapshotInfo lerp(ISnapshotInfo targetTemp, float delta)
		{
			YawSnapshotInfo yawSnapshotInfo = (YawSnapshotInfo)targetTemp;
			return new YawSnapshotInfo
			{
				pos = Vector3.Lerp(this.pos, yawSnapshotInfo.pos, delta),
				yaw = Mathf.LerpAngle(this.yaw, yawSnapshotInfo.yaw, delta)
			};
		}

		// Token: 0x04001A43 RID: 6723
		public Vector3 pos;

		// Token: 0x04001A44 RID: 6724
		public float yaw;
	}
}
