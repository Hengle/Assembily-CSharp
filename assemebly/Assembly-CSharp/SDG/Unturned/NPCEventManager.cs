using System;

namespace SDG.Unturned
{
	// Token: 0x0200073C RID: 1852
	public class NPCEventManager
	{
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06003442 RID: 13378 RVA: 0x00154B04 File Offset: 0x00152F04
		// (remove) Token: 0x06003443 RID: 13379 RVA: 0x00154B38 File Offset: 0x00152F38
		public static event NPCEventTriggeredHandler eventTriggered;

		// Token: 0x06003444 RID: 13380 RVA: 0x00154B6C File Offset: 0x00152F6C
		public static void triggerEventTriggered(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}
			NPCEventTriggeredHandler npceventTriggeredHandler = NPCEventManager.eventTriggered;
			if (npceventTriggeredHandler != null)
			{
				npceventTriggeredHandler(id);
			}
		}
	}
}
