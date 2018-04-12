using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000588 RID: 1416
	// (Invoke) Token: 0x0600271A RID: 10010
	public delegate void SalvageBarricadeRequestHandler(CSteamID steamID, byte x, byte y, ushort plant, ushort index, ref bool shouldAllow);
}
