using System;
using UnityEngine;

namespace SDG.Framework.Utilities
{
	// Token: 0x02000303 RID: 771
	public class MathUtility
	{
		// Token: 0x06001618 RID: 5656 RVA: 0x000842E8 File Offset: 0x000826E8
		public static void getPitchYawFromDirection(Vector3 direction, out float pitch, out float yaw)
		{
			pitch = 57.29578f * -Mathf.Sin(direction.y / direction.magnitude);
			yaw = 57.29578f * -Mathf.Atan2(direction.z, direction.x) + 90f;
		}

		// Token: 0x04000C29 RID: 3113
		public static readonly Quaternion IDENTITY_QUATERNION = Quaternion.identity;

		// Token: 0x04000C2A RID: 3114
		public static readonly Matrix4x4 IDENTITY_MATRIX = Matrix4x4.identity;
	}
}
