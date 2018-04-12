using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000628 RID: 1576
	// (Invoke) Token: 0x06002CAB RID: 11435
	public delegate void Hurt(Player player, byte damage, Vector3 force, EDeathCause cause, ELimb limb, CSteamID killer);
}
