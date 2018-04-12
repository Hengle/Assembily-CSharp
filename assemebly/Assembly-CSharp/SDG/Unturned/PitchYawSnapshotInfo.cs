using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005DB RID: 1499
	public struct PitchYawSnapshotInfo : ISnapshotInfo
	{
		// Token: 0x06002A4F RID: 10831 RVA: 0x00107BE2 File Offset: 0x00105FE2
		public PitchYawSnapshotInfo(Vector3 pos, float pitch, float yaw)
		{
			this.pos = pos;
			this.pitch = pitch;
			this.yaw = yaw;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00107BFC File Offset: 0x00105FFC
		public ISnapshotInfo lerp(ISnapshotInfo targetTemp, float delta)
		{
			PitchYawSnapshotInfo pitchYawSnapshotInfo = (PitchYawSnapshotInfo)targetTemp;
			return new PitchYawSnapshotInfo
			{
				pos = Vector3.Lerp(this.pos, pitchYawSnapshotInfo.pos, delta),
				pitch = Mathf.LerpAngle(this.pitch, pitchYawSnapshotInfo.pitch, delta),
				yaw = Mathf.LerpAngle(this.yaw, pitchYawSnapshotInfo.yaw, delta)
			};
		}

		// Token: 0x04001A45 RID: 6725
		public Vector3 pos;

		// Token: 0x04001A46 RID: 6726
		public float pitch;

		// Token: 0x04001A47 RID: 6727
		public float yaw;
	}
}
