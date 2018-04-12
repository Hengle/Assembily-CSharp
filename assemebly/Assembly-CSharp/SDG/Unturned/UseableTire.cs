using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007C0 RID: 1984
	public class UseableTire : Useable
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x001BD100 File Offset: 0x001BB500
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x001BD116 File Offset: 0x001BB516
		private bool isAttachable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.75f;
			}
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x001BD134 File Offset: 0x001BB534
		private void attach()
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

		// Token: 0x06003A13 RID: 14867 RVA: 0x001BD1B7 File Offset: 0x001BB5B7
		[SteamCall]
		public void askAttach(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.attach();
			}
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x001BD1E8 File Offset: 0x001BB5E8
		private bool fire()
		{
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.vehicle == null || !raycastInfo.vehicle.isTireReplaceable)
				{
					return false;
				}
				int closestAliveTireIndex = raycastInfo.vehicle.getClosestAliveTireIndex(raycastInfo.point, ((ItemTireAsset)base.player.equipment.asset).mode == EUseableTireMode.REMOVE);
				if (closestAliveTireIndex == -1)
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
				if (input.vehicle == null || !input.vehicle.isTireReplaceable)
				{
					return false;
				}
				int closestAliveTireIndex2 = input.vehicle.getClosestAliveTireIndex(input.point, ((ItemTireAsset)base.player.equipment.asset).mode == EUseableTireMode.REMOVE);
				if (closestAliveTireIndex2 == -1)
				{
					return false;
				}
				this.isAttaching = true;
				this.vehicle = input.vehicle;
				this.vehicleWheelIndex = closestAliveTireIndex2;
			}
			return true;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x001BD3AC File Offset: 0x001BB7AC
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
				this.attach();
				if (Provider.isServer)
				{
					base.channel.send("askAttach", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x001BD431 File Offset: 0x001BB831
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x001BD464 File Offset: 0x001BB864
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isAttaching && this.isAttachable)
			{
				this.isAttaching = false;
				if (this.vehicle != null && this.vehicle.isTireReplaceable && this.vehicleWheelIndex != -1)
				{
					if (((ItemTireAsset)base.player.equipment.asset).mode == EUseableTireMode.ADD)
					{
						if (!this.vehicle.tires[this.vehicleWheelIndex].isAlive)
						{
							this.vehicle.askRepairTire(this.vehicleWheelIndex);
						}
					}
					else if (this.vehicle.tires[this.vehicleWheelIndex].isAlive)
					{
						this.vehicle.askDamageTire(this.vehicleWheelIndex);
						base.player.inventory.forceAddItem(new Item(1451, true), false);
					}
					this.vehicle = null;
				}
				if (((ItemTireAsset)base.player.equipment.asset).mode == EUseableTireMode.ADD && Provider.isServer)
				{
					base.player.equipment.useStepA();
				}
			}
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (((ItemTireAsset)base.player.equipment.asset).mode == EUseableTireMode.ADD && Provider.isServer)
				{
					base.player.equipment.useStepB();
				}
			}
		}

		// Token: 0x04002CFE RID: 11518
		private float startedUse;

		// Token: 0x04002CFF RID: 11519
		private float useTime;

		// Token: 0x04002D00 RID: 11520
		private bool isUsing;

		// Token: 0x04002D01 RID: 11521
		private bool isAttaching;

		// Token: 0x04002D02 RID: 11522
		private InteractableVehicle vehicle;

		// Token: 0x04002D03 RID: 11523
		private int vehicleWheelIndex = -1;
	}
}
