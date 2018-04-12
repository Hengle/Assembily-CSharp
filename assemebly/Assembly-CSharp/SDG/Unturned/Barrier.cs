using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200051F RID: 1311
	public class Barrier : MonoBehaviour
	{
		// Token: 0x0600239F RID: 9119 RVA: 0x000C5EC0 File Offset: 0x000C42C0
		private void OnTriggerEnter(Collider other)
		{
			if (Provider.isServer && other.transform.CompareTag("Player"))
			{
				Player player = DamageTool.getPlayer(other.transform);
				if (player != null)
				{
					EPlayerKill eplayerKill;
					player.life.askDamage(101, Vector3.up * 10f, EDeathCause.SUICIDE, ELimb.SKULL, CSteamID.Nil, out eplayerKill);
				}
			}
		}
	}
}
