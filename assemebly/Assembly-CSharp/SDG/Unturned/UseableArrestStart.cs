using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007AD RID: 1965
	public class UseableArrestStart : Useable
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x001A32DD File Offset: 0x001A16DD
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x001A32F4 File Offset: 0x001A16F4
		private void arrest()
		{
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemArrestStartAsset)base.player.equipment.asset).use);
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x001A3346 File Offset: 0x001A1746
		[SteamCall]
		public void askArrest(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.arrest();
			}
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x001A3374 File Offset: 0x001A1774
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
				if (raycastInfo.player != null && raycastInfo.player.animator.gesture == EPlayerGesture.SURRENDER_START)
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

		// Token: 0x0600391C RID: 14620 RVA: 0x001A34F8 File Offset: 0x001A18F8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x001A352C File Offset: 0x001A192C
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					if (this.enemy != null && this.enemy.animator.gesture == EPlayerGesture.SURRENDER_START)
					{
						this.enemy.animator.captorID = base.channel.owner.playerID.steamID;
						this.enemy.animator.captorItem = base.player.equipment.itemID;
						this.enemy.animator.captorStrength = ((ItemArrestStartAsset)base.player.equipment.asset).strength;
						this.enemy.animator.sendGesture(EPlayerGesture.ARREST_START, true);
						base.player.equipment.use();
					}
					else
					{
						base.player.equipment.dequip();
					}
				}
			}
		}

		// Token: 0x04002BF7 RID: 11255
		private float startedUse;

		// Token: 0x04002BF8 RID: 11256
		private float useTime;

		// Token: 0x04002BF9 RID: 11257
		private bool isUsing;

		// Token: 0x04002BFA RID: 11258
		private Player enemy;
	}
}
