using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000195 RID: 405
	public static class TransformExtension
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x0005B7F2 File Offset: 0x00059BF2
		public static T GetOrAddComponent<T>(this Transform transform) where T : Component
		{
			return transform.gameObject.getOrAddComponent<T>();
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0005B800 File Offset: 0x00059C00
		public static void FindAllChildrenWithName(this Transform transform, string name, List<GameObject> gameObjects)
		{
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child.name == name)
				{
					gameObjects.Add(child.gameObject);
				}
				child.FindAllChildrenWithName(name, gameObjects);
			}
		}
	}
}
