using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005BC RID: 1468
	public class SafezoneManager : MonoBehaviour
	{
		// Token: 0x06002912 RID: 10514 RVA: 0x000FB174 File Offset: 0x000F9574
		public static bool checkPointValid(Vector3 point)
		{
			for (int i = 0; i < SafezoneManager.bubbles.Count; i++)
			{
				SafezoneBubble safezoneBubble = SafezoneManager.bubbles[i];
				if ((safezoneBubble.origin - point).sqrMagnitude < safezoneBubble.sqrRadius)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000FB1CC File Offset: 0x000F95CC
		public static SafezoneBubble registerBubble(Vector3 origin, float radius)
		{
			SafezoneBubble safezoneBubble = new SafezoneBubble(origin, radius * radius);
			SafezoneManager.bubbles.Add(safezoneBubble);
			return safezoneBubble;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000FB1EF File Offset: 0x000F95EF
		public static void deregisterBubble(SafezoneBubble bubble)
		{
			SafezoneManager.bubbles.Remove(bubble);
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000FB1FD File Offset: 0x000F95FD
		private void onLevelLoaded(int level)
		{
			SafezoneManager.bubbles = new List<SafezoneBubble>();
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000FB209 File Offset: 0x000F9609
		private void Start()
		{
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x0400199D RID: 6557
		private static List<SafezoneBubble> bubbles;
	}
}
