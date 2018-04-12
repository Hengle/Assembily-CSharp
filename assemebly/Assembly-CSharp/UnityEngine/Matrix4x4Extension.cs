using System;

namespace UnityEngine
{
	// Token: 0x02000193 RID: 403
	public static class Matrix4x4Extension
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0005B678 File Offset: 0x00059A78
		public static Vector3 GetPosition(this Matrix4x4 matrix)
		{
			Vector3 result;
			result.x = matrix.m03;
			result.y = matrix.m13;
			result.z = matrix.m23;
			return result;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0005B6B0 File Offset: 0x00059AB0
		public static Quaternion GetRotation(this Matrix4x4 matrix)
		{
			Vector3 forward;
			forward.x = matrix.m02;
			forward.y = matrix.m12;
			forward.z = matrix.m22;
			Vector3 upwards;
			upwards.x = matrix.m01;
			upwards.y = matrix.m11;
			upwards.z = matrix.m21;
			return Quaternion.LookRotation(forward, upwards);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0005B718 File Offset: 0x00059B18
		public static Vector3 GetScale(this Matrix4x4 matrix)
		{
			Vector4 vector = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30);
			Vector3 result;
			result.x = vector.magnitude;
			Vector4 vector2 = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31);
			result.y = vector2.magnitude;
			Vector4 vector3 = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32);
			result.z = vector3.magnitude;
			return result;
		}
	}
}
