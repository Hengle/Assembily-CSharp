using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200072B RID: 1835
	// (Invoke) Token: 0x060033CF RID: 13263
	public delegate void DamageToolPlayerDamagedHandler(Player player, ref EDeathCause cause, ref ELimb limb, ref CSteamID killer, ref Vector3 direction, ref float damage, ref float times, ref bool canDamage);
}
