using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007C1 RID: 1985
	public class UseableVehicleBattery : Useable
	{
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x001BD604 File Offset: 0x001BBA04
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x001BD61A File Offset: 0x001BBA1A
		private bool isReplaceable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.75f;
			}
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x001BD638 File Offset: 0x001BBA38
		private void replace()
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

		// Token: 0x06003A1C RID: 14876 RVA: 0x001BD6BB File Offset: 0x001BBABB
		[SteamCall]
		public void askReplace(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.replace();
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x001BD6EC File Offset: 0x001BBAEC
		private bool fire()
		{
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.vehicle == null || !raycastInfo.vehicle.isBatteryReplaceable)
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
				if (input.vehicle == null || !input.vehicle.isBatteryReplaceable)
				{
					return false;
				}
				this.isReplacing = true;
				this.vehicle = input.vehicle;
			}
			return true;
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x001BD834 File Offset: 0x001BBC34
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
				this.replace();
				if (Provider.isServer)
				{
					base.channel.send("askReplace", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x001BD8B9 File Offset: 0x001BBCB9
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x001BD8EC File Offset: 0x001BBCEC
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isReplacing && this.isReplaceable)
			{
				this.isReplacing = false;
				if (this.vehicle != null && this.vehicle.isBatteryReplaceable)
				{
					this.vehicle.replaceBattery(base.player, base.player.equipment.quality);
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

		// Token: 0x04002D04 RID: 11524
		private float startedUse;

		// Token: 0x04002D05 RID: 11525
		private float useTime;

		// Token: 0x04002D06 RID: 11526
		private bool isUsing;

		// Token: 0x04002D07 RID: 11527
		private bool isReplacing;

		// Token: 0x04002D08 RID: 11528
		private InteractableVehicle vehicle;
	}
}
