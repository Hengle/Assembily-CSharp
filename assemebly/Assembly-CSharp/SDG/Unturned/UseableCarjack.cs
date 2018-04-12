using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007AF RID: 1967
	public class UseableCarjack : Useable
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x001A7FA5 File Offset: 0x001A63A5
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x001A7FBB File Offset: 0x001A63BB
		private bool isJackable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.75f;
			}
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x001A7FD8 File Offset: 0x001A63D8
		private void pull()
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

		// Token: 0x06003934 RID: 14644 RVA: 0x001A805B File Offset: 0x001A645B
		[SteamCall]
		public void askPull(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.pull();
			}
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x001A808C File Offset: 0x001A648C
		private bool fire()
		{
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.vehicle == null || !raycastInfo.vehicle.isEmpty)
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
				if (input.vehicle == null || !input.vehicle.isEmpty)
				{
					return false;
				}
				this.isJacking = true;
				this.vehicle = input.vehicle;
			}
			return true;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x001A81D4 File Offset: 0x001A65D4
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
				this.pull();
				if (Provider.isServer)
				{
					base.channel.send("askPull", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x001A8259 File Offset: 0x001A6659
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x001A828C File Offset: 0x001A668C
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isJacking && this.isJackable)
			{
				this.isJacking = false;
				if (this.vehicle != null && this.vehicle.isEmpty)
				{
					this.vehicle.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.Range(-32f, 32f), UnityEngine.Random.Range(480f, 544f) * (float)((base.player.skills.boost != EPlayerBoost.FLIGHT) ? 1 : 4), UnityEngine.Random.Range(-32f, 32f));
					this.vehicle.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(-64f, 64f), UnityEngine.Random.Range(-64f, 64f), UnityEngine.Random.Range(-64f, 64f));
					this.vehicle = null;
				}
			}
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
			}
		}

		// Token: 0x04002C20 RID: 11296
		private float startedUse;

		// Token: 0x04002C21 RID: 11297
		private float useTime;

		// Token: 0x04002C22 RID: 11298
		private bool isUsing;

		// Token: 0x04002C23 RID: 11299
		private bool isJacking;

		// Token: 0x04002C24 RID: 11300
		private InteractableVehicle vehicle;
	}
}
