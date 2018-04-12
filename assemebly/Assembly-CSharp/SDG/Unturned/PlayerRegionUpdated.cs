using System;

namespace SDG.Unturned
{
	// Token: 0x0200068C RID: 1676
	// (Invoke) Token: 0x060030C0 RID: 12480
	public delegate void PlayerRegionUpdated(Player player, byte old_x, byte old_y, byte new_x, byte new_y, byte index, ref bool canIncrementIndex);
}
