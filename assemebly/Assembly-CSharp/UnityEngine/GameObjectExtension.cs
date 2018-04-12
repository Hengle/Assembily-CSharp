using System;

namespace UnityEngine
{
	// Token: 0x0200018F RID: 399
	public static class GameObjectExtension
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x0005B458 File Offset: 0x00059858
		public static T getOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			T t = gameObject.GetComponent<T>();
			if (t == null)
			{
				t = gameObject.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0005B485 File Offset: 0x00059885
		public static RectTransform getRectTransform(this GameObject gameObject)
		{
			return gameObject.transform as RectTransform;
		}
	}
}
