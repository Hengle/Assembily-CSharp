using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005D9 RID: 1497
	public struct TransformSnapshotInfo : ISnapshotInfo
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x00107B13 File Offset: 0x00105F13
		public TransformSnapshotInfo(Vector3 pos, Quaternion rot)
		{
			this.pos = pos;
			this.rot = rot;
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x00107B24 File Offset: 0x00105F24
		public ISnapshotInfo lerp(ISnapshotInfo targetTemp, float delta)
		{
			TransformSnapshotInfo transformSnapshotInfo = (TransformSnapshotInfo)targetTemp;
			return new TransformSnapshotInfo
			{
				pos = Vector3.Lerp(this.pos, transformSnapshotInfo.pos, delta),
				rot = Quaternion.Slerp(this.rot, transformSnapshotInfo.rot, delta)
			};
		}

		// Token: 0x04001A41 RID: 6721
		public Vector3 pos;

		// Token: 0x04001A42 RID: 6722
		public Quaternion rot;
	}
}
