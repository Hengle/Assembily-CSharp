using System;
using Steamworks;

namespace SDG.Unturned
{
	// Token: 0x02000670 RID: 1648
	// (Invoke) Token: 0x06002FC0 RID: 12224
	public delegate void TriggerReceive(SteamChannel channel, CSteamID steamID, byte[] packet, int offset, int size);
}
