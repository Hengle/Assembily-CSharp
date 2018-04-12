using System;
using SDG.Framework.Utilities;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007BF RID: 1983
	public class UseableThrowable : Useable
	{
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x001BC98B File Offset: 0x001BAD8B
		private bool isUseable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x001BC9A1 File Offset: 0x001BADA1
		private bool isThrowable
		{
			get
			{
				return Time.realtimeSinceStartup - this.startedUse > this.useTime * 0.6f;
			}
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x001BC9C0 File Offset: 0x001BADC0
		private void toss(Vector3 origin, Vector3 direction)
		{
			Transform transform = UnityEngine.Object.Instantiate<GameObject>(((ItemThrowableAsset)base.player.equipment.asset).throwable).transform;
			transform.name = "Throwable";
			transform.parent = Level.effects;
			transform.position = origin;
			transform.rotation = Quaternion.LookRotation(direction);
			transform.GetComponent<Rigidbody>().AddForce(direction * (float)((base.player.skills.boost != EPlayerBoost.OLYMPIC) ? 750 : 1500));
			if (((ItemThrowableAsset)base.player.equipment.asset).isExplosive)
			{
				if (Provider.isServer)
				{
					Grenade grenade = transform.gameObject.AddComponent<Grenade>();
					grenade.killer = base.channel.owner.playerID.steamID;
					grenade.range = ((ItemThrowableAsset)base.player.equipment.asset).range;
					grenade.playerDamage = ((ItemThrowableAsset)base.player.equipment.asset).playerDamageMultiplier.damage;
					grenade.zombieDamage = ((ItemThrowableAsset)base.player.equipment.asset).zombieDamageMultiplier.damage;
					grenade.animalDamage = ((ItemThrowableAsset)base.player.equipment.asset).animalDamageMultiplier.damage;
					grenade.barricadeDamage = ((ItemThrowableAsset)base.player.equipment.asset).barricadeDamage;
					grenade.structureDamage = ((ItemThrowableAsset)base.player.equipment.asset).structureDamage;
					grenade.vehicleDamage = ((ItemThrowableAsset)base.player.equipment.asset).vehicleDamage;
					grenade.resourceDamage = ((ItemThrowableAsset)base.player.equipment.asset).resourceDamage;
					grenade.objectDamage = ((ItemThrowableAsset)base.player.equipment.asset).objectDamage;
					grenade.explosion = ((ItemThrowableAsset)base.player.equipment.asset).explosion;
					grenade.fuseLength = ((ItemThrowableAsset)base.player.equipment.asset).fuseLength;
				}
				else
				{
					UnityEngine.Object.Destroy(transform.gameObject, ((ItemThrowableAsset)base.player.equipment.asset).fuseLength);
				}
			}
			else if (((ItemThrowableAsset)base.player.equipment.asset).isFlash)
			{
				if (!Dedicator.isDedicated)
				{
					Flashbang flashbang = transform.gameObject.AddComponent<Flashbang>();
					flashbang.fuseLength = ((ItemThrowableAsset)base.player.equipment.asset).fuseLength;
				}
				else
				{
					UnityEngine.Object.Destroy(transform.gameObject, ((ItemThrowableAsset)base.player.equipment.asset).fuseLength);
				}
			}
			else
			{
				transform.gameObject.AddComponent<Distraction>();
				UnityEngine.Object.Destroy(transform.gameObject, ((ItemThrowableAsset)base.player.equipment.asset).fuseLength);
			}
			if (((ItemThrowableAsset)base.player.equipment.asset).isSticky)
			{
				StickyGrenade stickyGrenade = transform.gameObject.AddComponent<StickyGrenade>();
				stickyGrenade.ignoreTransform = base.transform;
			}
			if (((ItemThrowableAsset)base.player.equipment.asset).explodeOnImpact)
			{
				ImpactGrenade impactGrenade = transform.gameObject.AddComponent<ImpactGrenade>();
				impactGrenade.explodable = transform.GetComponent<IExplodableThrowable>();
				impactGrenade.ignoreTransform = base.transform;
			}
			if (Dedicator.isDedicated)
			{
				Transform transform2 = transform.FindChild("Smoke");
				if (transform2 != null)
				{
					UnityEngine.Object.Destroy(transform2.gameObject);
				}
			}
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x001BCDA4 File Offset: 0x001BB1A4
		private void swing()
		{
			this.isSwinging = true;
			base.player.animator.play("Use", false);
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemThrowableAsset)base.player.equipment.asset).use);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x001BCE1C File Offset: 0x001BB21C
		[SteamCall]
		public void askToss(CSteamID steamID, Vector3 origin, Vector3 direction)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.toss(origin, direction);
			}
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x001BCE4C File Offset: 0x001BB24C
		[SteamCall]
		public void askSwing(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.swing();
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x001BCE7C File Offset: 0x001BB27C
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			base.player.equipment.isBusy = true;
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.swing();
			if (Provider.isServer)
			{
				if (((ItemThrowableAsset)base.player.equipment.asset).isExplosive)
				{
					base.player.life.markAggressive(false, true);
				}
				base.channel.send("askSwing", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
			}
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x001BCF1C File Offset: 0x001BB31C
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.useTime = base.player.animator.getAnimationLength("Use");
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x001BCF50 File Offset: 0x001BB350
		public override void tick()
		{
			if (!base.player.equipment.isEquipped)
			{
				return;
			}
			if ((base.channel.isOwner || Provider.isServer) && this.isSwinging && this.isThrowable)
			{
				Vector3 vector = base.player.look.aim.position;
				Vector3 forward = base.player.look.aim.forward;
				RaycastHit raycastHit;
				if (!PhysicsUtility.raycast(new Ray(vector, forward), out raycastHit, 1f, RayMasks.DAMAGE_SERVER, QueryTriggerInteraction.UseGlobal))
				{
					vector += forward;
				}
				this.toss(vector, forward);
				if (base.channel.isOwner)
				{
					int num;
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Found_Throwables", out num))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Found_Throwables", num + 1);
					}
				}
				else
				{
					base.channel.send("askToss", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						vector,
						forward
					});
				}
				if (Provider.isServer)
				{
					base.player.equipment.useStepA();
				}
				this.isSwinging = false;
			}
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x001BD09C File Offset: 0x001BB49C
		public override void simulate(uint simulation, bool inputSteady)
		{
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

		// Token: 0x04002CFA RID: 11514
		private float startedUse;

		// Token: 0x04002CFB RID: 11515
		private float useTime;

		// Token: 0x04002CFC RID: 11516
		private bool isUsing;

		// Token: 0x04002CFD RID: 11517
		private bool isSwinging;
	}
}
