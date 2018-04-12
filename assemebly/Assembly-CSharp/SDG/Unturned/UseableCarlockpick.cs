using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B0 RID: 1968
	public class UseableCarlockpick : Useable
	{
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x001A83AD File Offset: 0x001A67AD
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x001A83C3 File Offset: 0x001A67C3
		private bool isUnlockable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.75f;
			}
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x001A83E0 File Offset: 0x001A67E0
		private void jimmy()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemToolAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x001A8463 File Offset: 0x001A6863
		[SteamCall]
		public void askJimmy(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.jimmy();
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x001A8494 File Offset: 0x001A6894
		private bool fire()
		{
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.vehicle == null || !raycastInfo.vehicle.isEmpty || !raycastInfo.vehicle.isLocked)
				{
					return false;
				}
				base.player.input.sendRaycast(raycastInfo);
			}
			if (Provider.isServer)
			{
				if (!base.player.input.hasInputs())
				{
					return false;
				}
				InputInfo input = base.player.input.getInput(true);
				if (input == null)
				{
					return false;
				}
				if ((input.point - base.player.look.aim.position).sqrMagnitude > 49f)
				{
					return false;
				}
				if (input.type != ERaycastInfoType.VEHICLE)
				{
					return false;
				}
				if (input.vehicle == null || !input.vehicle.isEmpty || !input.vehicle.isLocked)
				{
					return false;
				}
				this.isUnlocking = true;
				this.vehicle = input.vehicle;
			}
			return true;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x001A85FC File Offset: 0x001A69FC
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (this.isUseable && this.fire())
			{
				base.player.equipment.isBusy = true;
				this.startedUse = Time.realtimeSinceStartup;
				this.isUsing = true;
				this.jimmy();
				if (Provider.isServer)
				{
					base.player.life.markAggressive(true, true);
					base.channel.send("askJimmy", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x001A8693 File Offset: 0x001A6A93
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x001A86C8 File Offset: 0x001A6AC8
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUnlocking && this.isUnlockable)
			{
				this.isUnlocking = false;
				if (this.vehicle != null && this.vehicle.isEmpty && this.vehicle.isLocked)
				{
					VehicleManager.unlockVehicle(this.vehicle, base.player);
					this.vehicle = null;
				}
				if (Provider.isServer)
				{
					base.player.equipment.useStepA();
				}
			}
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					base.player.equipment.useStepB();
				}
			}
		}

		// Token: 0x04002C25 RID: 11301
		private float startedUse;

		// Token: 0x04002C26 RID: 11302
		private float useTime;

		// Token: 0x04002C27 RID: 11303
		private bool isUsing;

		// Token: 0x04002C28 RID: 11304
		private bool isUnlocking;

		// Token: 0x04002C29 RID: 11305
		private InteractableVehicle vehicle;
	}
}
