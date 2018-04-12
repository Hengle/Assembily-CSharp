using System;

namespace UnityEngine
{
	// Token: 0x02000197 RID: 407
	public static class Vector2Extension
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0005B8BE File Offset: 0x00059CBE
		public static Vector2 Cross(this Vector2 vector)
		{
			return new Vector2(vector.y, -vector.x);
		}
	}
}
