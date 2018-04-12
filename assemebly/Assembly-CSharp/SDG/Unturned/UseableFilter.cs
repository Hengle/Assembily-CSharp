using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B5 RID: 1973
	public class UseableFilter : Useable
	{
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x001A9DCE File Offset: 0x001A81CE
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x001A9DE4 File Offset: 0x001A81E4
		private void filter()
		{
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemFilterAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001A9E55 File Offset: 0x001A8255
		[SteamCall]
		public void askFilter(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.filter();
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x001A9E84 File Offset: 0x001A8284
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (base.player.clothing.maskAsset == null || !base.player.clothing.maskAsset.proofRadiation || base.player.clothing.maskQuality == 100)
			{
				return;
			}
			base.player.equipment.isBusy = true;
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			if (Provider.isServer)
			{
				base.channel.send("askFilter", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
			this.filter();
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x001A9F3A File Offset: 0x001A833A
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x001A9F70 File Offset: 0x001A8370
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					if (base.player.clothing.maskAsset != null && base.player.clothing.maskAsset.proofRadiation && base.player.clothing.maskQuality < 100)
					{
						base.player.equipment.use();
						base.player.clothing.maskQuality = 100;
						base.player.clothing.sendUpdateMaskQuality();
					}
					else
					{
						base.player.equipment.dequip();
					}
				}
			}
		}

		// Token: 0x04002C3E RID: 11326
		private float startedUse;

		// Token: 0x04002C3F RID: 11327
		private float useTime;

		// Token: 0x04002C40 RID: 11328
		private bool isUsing;
	}
}
