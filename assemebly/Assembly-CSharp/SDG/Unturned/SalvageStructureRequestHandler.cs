using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x020005C0 RID: 1472
	// (Invoke) Token: 0x06002925 RID: 10533
	public delegate void SalvageStructureRequestHandler(CSteamID steamID, byte x, byte y, ushort index, ref bool shouldAllow);
}
