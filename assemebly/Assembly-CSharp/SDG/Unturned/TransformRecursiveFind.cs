using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007CC RID: 1996
	public static class TransformRecursiveFind
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x001BEFD0 File Offset: 0x001BD3D0
		public static Transform FindChildRecursive(this Transform parent, string name)
		{
			for (int i = 0; i < parent.childCount; i++)
			{
				Transform transform = parent.GetChild(i);
				if (transform.name == name)
				{
					return transform;
				}
				if (transform.childCount != 0)
				{
					transform = transform.FindChildRecursive(name);
					if (transform != null)
					{
						return transform;
					}
				}
			}
			return null;
		}
	}
}
