using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B8 RID: 1976
	public class UseableGrower : Useable
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x001ABBAA File Offset: 0x001A9FAA
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x001ABBC0 File Offset: 0x001A9FC0
		private void grow()
		{
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemGrowerAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x001ABC43 File Offset: 0x001AA043
		[SteamCall]
		public void askGrow(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.grow();
			}
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x001ABC74 File Offset: 0x001AA074
		private bool fire()
		{
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.transform == null || !raycastInfo.transform.CompareTag("Barricade"))
				{
					return false;
				}
				InteractableFarm component = raycastInfo.transform.GetComponent<InteractableFarm>();
				if (component == null)
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
				if (input.type != ERaycastInfoType.BARRICADE)
				{
					return false;
				}
				if (input.transform == null || !input.transform.CompareTag("Barricade"))
				{
					return false;
				}
				this.farm = input.transform.GetComponent<InteractableFarm>();
				if (this.farm == null || this.farm.checkFarm())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x001ABE04 File Offset: 0x001AA204
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
				this.grow();
				if (Provider.isServer)
				{
					base.channel.send("askGrow", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x001ABE89 File Offset: 0x001AA289
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x001ABEBC File Offset: 0x001AA2BC
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				if (Provider.isServer)
				{
					if (this.farm != null && !this.farm.checkFarm())
					{
						BarricadeManager.updateFarm(this.farm.transform, 1u, true);
						base.player.equipment.use();
					}
					else
					{
						base.player.equipment.dequip();
					}
				}
			}
		}

		// Token: 0x04002C60 RID: 11360
		private float startedUse;

		// Token: 0x04002C61 RID: 11361
		private float useTime;

		// Token: 0x04002C62 RID: 11362
		private bool isUsing;

		// Token: 0x04002C63 RID: 11363
		private InteractableFarm farm;
	}
}
