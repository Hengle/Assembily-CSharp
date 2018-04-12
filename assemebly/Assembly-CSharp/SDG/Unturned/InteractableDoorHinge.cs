using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004CC RID: 1228
	public class InteractableDoorHinge : Interactable
	{
		// Token: 0x060020D9 RID: 8409 RVA: 0x000B3F73 File Offset: 0x000B2373
		public override bool checkUseable()
		{
			return this.door.checkToggle(Provider.client, Player.player.quests.groupID);
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000B3F94 File Offset: 0x000B2394
		public override void use()
		{
			BarricadeManager.toggleDoor(this.door.transform);
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000B3FA8 File Offset: 0x000B23A8
		public override bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			if (this.checkUseable())
			{
				if (this.door.isOpen)
				{
					message = EPlayerMessage.DOOR_CLOSE;
				}
				else
				{
					message = EPlayerMessage.DOOR_OPEN;
				}
			}
			else
			{
				message = EPlayerMessage.LOCKED;
			}
			text = string.Empty;
			color = Color.white;
			return true;
		}

		// Token: 0x04001394 RID: 5012
		public InteractableDoor door;
	}
}
