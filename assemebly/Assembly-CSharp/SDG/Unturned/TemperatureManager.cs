using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005C4 RID: 1476
	public class TemperatureManager : MonoBehaviour
	{
		// Token: 0x06002953 RID: 10579 RVA: 0x000FD8EC File Offset: 0x000FBCEC
		public static EPlayerTemperature checkPointTemperature(Vector3 point, bool proofFire)
		{
			EPlayerTemperature eplayerTemperature = EPlayerTemperature.NONE;
			for (int i = 0; i < TemperatureManager.bubbles.Count; i++)
			{
				TemperatureBubble temperatureBubble = TemperatureManager.bubbles[i];
				if (!(temperatureBubble.origin == null))
				{
					if (!proofFire || temperatureBubble.temperature != EPlayerTemperature.BURNING)
					{
						if ((temperatureBubble.origin.position - point).sqrMagnitude < temperatureBubble.sqrRadius)
						{
							if (temperatureBubble.temperature == EPlayerTemperature.ACID)
							{
								return temperatureBubble.temperature;
							}
							if (temperatureBubble.temperature == EPlayerTemperature.BURNING)
							{
								eplayerTemperature = temperatureBubble.temperature;
							}
							else if (eplayerTemperature != EPlayerTemperature.BURNING)
							{
								eplayerTemperature = temperatureBubble.temperature;
							}
						}
					}
				}
			}
			return eplayerTemperature;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000FD9B0 File Offset: 0x000FBDB0
		public static TemperatureBubble registerBubble(Transform origin, float radius, EPlayerTemperature temperature)
		{
			TemperatureBubble temperatureBubble = new TemperatureBubble(origin, radius * radius, temperature);
			TemperatureManager.bubbles.Add(temperatureBubble);
			return temperatureBubble;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000FD9D4 File Offset: 0x000FBDD4
		public static void deregisterBubble(TemperatureBubble bubble)
		{
			TemperatureManager.bubbles.Remove(bubble);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000FD9E2 File Offset: 0x000FBDE2
		private void onLevelLoaded(int level)
		{
			TemperatureManager.bubbles = new List<TemperatureBubble>();
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000FD9EE File Offset: 0x000FBDEE
		private void Start()
		{
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x040019BB RID: 6587
		private static List<TemperatureBubble> bubbles;
	}
}
