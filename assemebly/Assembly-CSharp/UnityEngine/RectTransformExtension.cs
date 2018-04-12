using System;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	public static class RectTransformExtension
	{
		// Token: 0x06000BF7 RID: 3063 RVA: 0x0005B7B9 File Offset: 0x00059BB9
		public static void reset(this RectTransform transform)
		{
			transform.anchorMin = Vector2.zero;
			transform.anchorMax = Vector2.one;
			transform.offsetMin = Vector2.zero;
			transform.offsetMax = Vector2.zero;
			transform.localScale = Vector3.one;
		}
	}
}
