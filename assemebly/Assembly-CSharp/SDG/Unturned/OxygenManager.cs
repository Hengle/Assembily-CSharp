using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005B7 RID: 1463
	public class OxygenManager : MonoBehaviour
	{
		// Token: 0x060028F2 RID: 10482 RVA: 0x000F9BE0 File Offset: 0x000F7FE0
		public static bool checkPointBreathable(Vector3 point)
		{
			for (int i = 0; i < OxygenManager.bubbles.Count; i++)
			{
				OxygenBubble oxygenBubble = OxygenManager.bubbles[i];
				if (!(oxygenBubble.origin == null))
				{
					if ((oxygenBubble.origin.position - point).sqrMagnitude < oxygenBubble.sqrRadius)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x000F9C54 File Offset: 0x000F8054
		public static OxygenBubble registerBubble(Transform origin, float radius)
		{
			OxygenBubble oxygenBubble = new OxygenBubble(origin, radius * radius);
			OxygenManager.bubbles.Add(oxygenBubble);
			return oxygenBubble;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x000F9C77 File Offset: 0x000F8077
		public static void deregisterBubble(OxygenBubble bubble)
		{
			OxygenManager.bubbles.Remove(bubble);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x000F9C85 File Offset: 0x000F8085
		private void onLevelLoaded(int level)
		{
			OxygenManager.bubbles = new List<OxygenBubble>();
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000F9C91 File Offset: 0x000F8091
		private void Start()
		{
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x04001990 RID: 6544
		private static List<OxygenBubble> bubbles;
	}
}
