using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000518 RID: 1304
	public class Achievement : MonoBehaviour
	{
		// Token: 0x06002380 RID: 9088 RVA: 0x000C53F0 File Offset: 0x000C37F0
		private void OnTriggerEnter(Collider other)
		{
			if (Dedicator.isDedicated || !other.transform.CompareTag("Player") || other.transform != Player.player.transform)
			{
				return;
			}
			bool flag;
			if (Provider.provider.achievementsService.getAchievement(base.transform.name, out flag) && !flag)
			{
				Provider.provider.achievementsService.setAchievement(base.transform.name);
			}
		}
	}
}
