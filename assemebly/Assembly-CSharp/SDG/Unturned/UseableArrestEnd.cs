using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007AC RID: 1964
	public class UseableArrestEnd : Useable
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x001A2F3F File Offset: 0x001A133F
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x001A2F58 File Offset: 0x001A1358
		private void arrest()
		{
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemArrestEndAsset)base.player.equipment.asset).use);
			}
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x001A2FAA File Offset: 0x001A13AA
		[SteamCall]
		public void askArrest(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.arrest();
			}
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x001A2FD8 File Offset: 0x001A13D8
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.player != null && raycastInfo.player.animator.gesture == EPlayerGesture.ARREST_START)
				{
					base.player.input.sendRaycast(raycastInfo);
					if (!Provider.isServer)
					{
						base.player.equipment.isBusy = true;
						this.startedUse = Time.realtimeSinceStartup;
						this.isUsing = true;
						this.arrest();
					}
				}
			}
			if (Provider.isServer)
			{
				if (!base.player.input.hasInputs())
				{
					return;
				}
				InputInfo input = base.player.input.getInput(true);
				if (input == null)
				{
					return;
				}
				if (input.type == ERaycastInfoType.PLAYER && input.player != null)
				{
					this.enemy = input.player;
					base.player.equipment.isBusy = true;
					this.startedUse = Time.realtimeSinceStartup;
					this.isUsing = true;
					this.arrest();
					base.channel.send("askArrest", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x001A315D File Offset: 0x001A155D
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x001A3190 File Offset: 0x001A1590
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					if (this.enemy != null && this.enemy.animator.gesture == EPlayerGesture.ARREST_START && this.enemy.animator.captorID == base.channel.owner.playerID.steamID && this.enemy.animator.captorItem == ((ItemArrestEndAsset)base.player.equipment.asset).recover)
					{
						this.enemy.animator.captorID = CSteamID.Nil;
						this.enemy.animator.captorStrength = 0;
						this.enemy.animator.sendGesture(EPlayerGesture.ARREST_STOP, true);
						base.player.inventory.forceAddItem(new Item(((ItemArrestEndAsset)base.player.equipment.asset).recover, EItemOrigin.NATURE), false);
					}
					base.player.equipment.dequip();
				}
			}
		}

		// Token: 0x04002BF3 RID: 11251
		private float startedUse;

		// Token: 0x04002BF4 RID: 11252
		private float useTime;

		// Token: 0x04002BF5 RID: 11253
		private bool isUsing;

		// Token: 0x04002BF6 RID: 11254
		private Player enemy;
	}
}
