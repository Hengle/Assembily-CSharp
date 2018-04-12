using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000598 RID: 1432
	public class ClaimManager : MonoBehaviour
	{
		// Token: 0x060027E0 RID: 10208 RVA: 0x000F1D6C File Offset: 0x000F016C
		public static bool checkCanBuild(Vector3 point, CSteamID owner, CSteamID group, bool isClaim)
		{
			for (int i = 0; i < ClaimManager.bubbles.Count; i++)
			{
				ClaimBubble claimBubble = ClaimManager.bubbles[i];
				if (((!isClaim) ? ((claimBubble.origin - point).sqrMagnitude < claimBubble.sqrRadius) : ((claimBubble.origin - point).sqrMagnitude < 4f * claimBubble.sqrRadius)) && ((!Dedicator.isDedicated) ? (!claimBubble.hasOwnership) : (!OwnershipTool.checkToggle(owner, claimBubble.owner, group, claimBubble.group))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000F1E24 File Offset: 0x000F0224
		public static ClaimBubble registerBubble(Vector3 origin, float radius, ulong owner, ulong group)
		{
			ClaimBubble claimBubble = new ClaimBubble(origin, radius * radius, owner, group);
			ClaimManager.bubbles.Add(claimBubble);
			return claimBubble;
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000F1E49 File Offset: 0x000F0249
		public static void deregisterBubble(ClaimBubble bubble)
		{
			ClaimManager.bubbles.Remove(bubble);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000F1E57 File Offset: 0x000F0257
		private void onLevelLoaded(int level)
		{
			ClaimManager.bubbles = new List<ClaimBubble>();
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000F1E63 File Offset: 0x000F0263
		private void Start()
		{
			Level.onPrePreLevelLoaded = (PrePreLevelLoaded)Delegate.Combine(Level.onPrePreLevelLoaded, new PrePreLevelLoaded(this.onLevelLoaded));
		}

		// Token: 0x040018FB RID: 6395
		private static List<ClaimBubble> bubbles;
	}
}
