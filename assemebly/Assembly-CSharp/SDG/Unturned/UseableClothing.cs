using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B1 RID: 1969
	public class UseableClothing : Useable
	{
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x001A87A5 File Offset: 0x001A6BA5
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x001A87BB File Offset: 0x001A6BBB
		private void wear()
		{
			base.player.animator.play("Use", false);
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x001A87F2 File Offset: 0x001A6BF2
		[SteamCall]
		public void askWear(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.wear();
			}
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x001A8820 File Offset: 0x001A6C20
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			base.player.equipment.isBusy = true;
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			if (Provider.isServer)
			{
				base.channel.send("askWear", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
			this.wear();
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x001A888F File Offset: 0x001A6C8F
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x001A88C4 File Offset: 0x001A6CC4
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					EItemType type = base.player.equipment.asset.type;
					ushort itemID = base.player.equipment.itemID;
					byte quality = base.player.equipment.quality;
					byte[] state = base.player.equipment.state;
					base.player.equipment.use();
					if (type == EItemType.HAT)
					{
						base.player.clothing.askWearHat(itemID, quality, state, true);
					}
					else if (type == EItemType.SHIRT)
					{
						base.player.clothing.askWearShirt(itemID, quality, state, true);
					}
					else if (type == EItemType.PANTS)
					{
						base.player.clothing.askWearPants(itemID, quality, state, true);
					}
					else if (type == EItemType.BACKPACK)
					{
						base.player.clothing.askWearBackpack(itemID, quality, state, true);
					}
					else if (type == EItemType.VEST)
					{
						base.player.clothing.askWearVest(itemID, quality, state, true);
					}
					else if (type == EItemType.MASK)
					{
						base.player.clothing.askWearMask(itemID, quality, state, true);
					}
					else if (type == EItemType.GLASSES)
					{
						base.player.clothing.askWearGlasses(itemID, quality, state, true);
					}
				}
			}
		}

		// Token: 0x04002C2A RID: 11306
		private float startedUse;

		// Token: 0x04002C2B RID: 11307
		private float useTime;

		// Token: 0x04002C2C RID: 11308
		private bool isUsing;
	}
}
