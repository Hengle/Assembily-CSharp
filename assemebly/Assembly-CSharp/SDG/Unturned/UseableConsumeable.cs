using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007B3 RID: 1971
	public class UseableConsumeable : Useable
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x001A8AB0 File Offset: 0x001A6EB0
		private bool isUseable
		{
			get
			{
				if (this.consumeMode == EConsumeMode.USE)
				{
					return Time.realtimeSinceStartup - this.startedUse > this.useTime;
				}
				return this.consumeMode == EConsumeMode.AID && Time.realtimeSinceStartup - this.startedUse > this.aidTime;
			}
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x001A8B00 File Offset: 0x001A6F00
		private void consume()
		{
			if (this.consumeMode == EConsumeMode.USE)
			{
				base.player.animator.play("Use", false);
			}
			else if (this.consumeMode == EConsumeMode.AID && this.hasAid)
			{
				base.player.animator.play("Aid", false);
			}
			if (!Dedicator.isDedicated)
			{
				base.player.playSound(((ItemConsumeableAsset)base.player.equipment.asset).use, 0.5f);
			}
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, 8f);
			}
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x001A8BB3 File Offset: 0x001A6FB3
		[SteamCall]
		public void askConsume(CSteamID steamID, byte mode)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.consumeMode = (EConsumeMode)mode;
				this.consume();
			}
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x001A8BE8 File Offset: 0x001A6FE8
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			base.player.equipment.isBusy = true;
			this.startedUse = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.consumeMode = EConsumeMode.USE;
			this.consume();
			if (Provider.isServer)
			{
				base.channel.send("askConsume", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
				{
					(byte)this.consumeMode
				});
			}
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x001A8C70 File Offset: 0x001A7070
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (!this.hasAid)
			{
				return;
			}
			if (base.channel.isOwner)
			{
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, 3f, RayMasks.DAMAGE_CLIENT);
				base.player.input.sendRaycast(raycastInfo);
				if (!Provider.isServer && raycastInfo.player != null)
				{
					base.player.equipment.isBusy = true;
					this.startedUse = Time.realtimeSinceStartup;
					this.isUsing = true;
					this.consumeMode = EConsumeMode.AID;
					this.consume();
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
					this.consumeMode = EConsumeMode.AID;
					this.consume();
					base.channel.send("askConsume", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						(byte)this.consumeMode
					});
				}
			}
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x001A8E08 File Offset: 0x001A7208
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			this.hasAid = ((ItemConsumeableAsset)base.player.equipment.asset).hasAid;
			this.useTime = base.player.animator.getAnimationLength("Use");
			if (this.hasAid)
			{
				this.aidTime = base.player.animator.getAnimationLength("Aid");
			}
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x001A8E8C File Offset: 0x001A728C
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isUseable)
			{
				base.player.equipment.isBusy = false;
				this.isUsing = false;
				ItemConsumeableAsset itemConsumeableAsset = (ItemConsumeableAsset)base.player.equipment.asset;
				if (this.consumeMode == EConsumeMode.AID)
				{
					if (Provider.isServer)
					{
						if (itemConsumeableAsset != null && this.enemy != null)
						{
							byte health = this.enemy.life.health;
							byte virus = this.enemy.life.virus;
							bool isBleeding = this.enemy.life.isBleeding;
							bool isBroken = this.enemy.life.isBroken;
							this.enemy.life.askHeal((byte)((float)itemConsumeableAsset.health * (1f + base.player.skills.mastery(2, 0) * 0.5f)), itemConsumeableAsset.hasBleeding, itemConsumeableAsset.hasBroken);
							byte food = this.enemy.life.food;
							this.enemy.life.askEat((byte)((float)itemConsumeableAsset.food * ((float)base.player.equipment.quality / 100f)));
							byte food2 = this.enemy.life.food;
							byte b = (byte)((float)itemConsumeableAsset.water * ((float)base.player.equipment.quality / 100f));
							if (itemConsumeableAsset.foodConstrainsWater)
							{
								b = (byte)Mathf.Min((int)b, (int)(food2 - food));
							}
							this.enemy.life.askDrink(b);
							this.enemy.life.askInfect((byte)((float)itemConsumeableAsset.virus * (1f - this.enemy.skills.mastery(1, 2) * 0.5f)));
							this.enemy.life.askDisinfect((byte)((float)itemConsumeableAsset.disinfectant * (1f + this.enemy.skills.mastery(2, 0) * 0.5f)));
							if (base.player.equipment.quality < 50)
							{
								this.enemy.life.askInfect((byte)((float)(itemConsumeableAsset.food + itemConsumeableAsset.water) * 0.5f * (1f - (float)base.player.equipment.quality / 50f) * (1f - this.enemy.skills.mastery(1, 2) * 0.5f)));
							}
							byte health2 = this.enemy.life.health;
							byte virus2 = this.enemy.life.virus;
							bool isBleeding2 = this.enemy.life.isBleeding;
							bool isBroken2 = this.enemy.life.isBroken;
							uint num = 0u;
							int num2 = 0;
							if (health2 > health)
							{
								num += (uint)Mathf.RoundToInt((float)(health2 - health) / 2f);
								num2++;
							}
							if (virus2 > virus)
							{
								num += (uint)Mathf.RoundToInt((float)(virus2 - virus) / 2f);
								num2++;
							}
							if (isBleeding && !isBleeding2)
							{
								num += 15u;
								num2++;
							}
							if (isBroken && !isBroken2)
							{
								num += 15u;
								num2++;
							}
							if (num > 0u)
							{
								base.player.skills.askPay(num);
							}
							if (num2 > 0)
							{
								base.player.skills.askRep(num2);
							}
						}
						base.player.equipment.use();
					}
				}
				else
				{
					if (itemConsumeableAsset != null)
					{
						base.player.life.askRest(itemConsumeableAsset.energy);
						base.player.life.askView((byte)((float)itemConsumeableAsset.vision * (1f - base.player.skills.mastery(1, 2))));
						base.player.life.askWarm(itemConsumeableAsset.warmth);
						bool flag;
						if (base.channel.isOwner && itemConsumeableAsset.vision > 0 && Provider.provider.achievementsService.getAchievement("Berries", out flag) && !flag)
						{
							Provider.provider.achievementsService.setAchievement("Berries");
						}
					}
					if (Provider.isServer)
					{
						Vector3 point = base.transform.position + Vector3.up;
						if (itemConsumeableAsset != null)
						{
							base.player.life.askHeal((byte)((float)itemConsumeableAsset.health * (1f + base.player.skills.mastery(2, 0) * 0.5f)), itemConsumeableAsset.hasBleeding, itemConsumeableAsset.hasBroken);
							byte food3 = base.player.life.food;
							base.player.life.askEat((byte)((float)itemConsumeableAsset.food * ((float)base.player.equipment.quality / 100f)));
							byte food4 = base.player.life.food;
							byte b2 = (byte)((float)itemConsumeableAsset.water * ((float)base.player.equipment.quality / 100f));
							if (itemConsumeableAsset.foodConstrainsWater)
							{
								b2 = (byte)Mathf.Min((int)b2, (int)(food4 - food3));
							}
							base.player.life.askDrink(b2);
							base.player.life.askInfect((byte)((float)itemConsumeableAsset.virus * (1f - base.player.skills.mastery(1, 2) * 0.5f)));
							base.player.life.askDisinfect((byte)((float)itemConsumeableAsset.disinfectant * (1f + base.player.skills.mastery(2, 0) * 0.5f)));
							base.player.life.askWarm(itemConsumeableAsset.warmth);
							if (base.player.equipment.quality < 50)
							{
								base.player.life.askInfect((byte)((float)(itemConsumeableAsset.food + itemConsumeableAsset.water) * 0.5f * (1f - (float)base.player.equipment.quality / 50f) * (1f - base.player.skills.mastery(1, 2) * 0.5f)));
							}
						}
						base.player.equipment.use();
						if (itemConsumeableAsset != null && itemConsumeableAsset.explosion > 0)
						{
							EffectManager.sendEffect(itemConsumeableAsset.explosion, EffectManager.LARGE, point);
							List<EPlayerKill> list;
							DamageTool.explode(point, itemConsumeableAsset.range, EDeathCause.CHARGE, base.channel.owner.playerID.steamID, itemConsumeableAsset.playerDamageMultiplier.damage, itemConsumeableAsset.zombieDamageMultiplier.damage, itemConsumeableAsset.animalDamageMultiplier.damage, itemConsumeableAsset.barricadeDamage, itemConsumeableAsset.structureDamage, itemConsumeableAsset.vehicleDamage, itemConsumeableAsset.resourceDamage, itemConsumeableAsset.objectDamage, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
							if (itemConsumeableAsset.playerDamageMultiplier.damage > 0.5f)
							{
								EPlayerKill eplayerKill;
								base.player.life.askDamage(101, Vector3.up, EDeathCause.CHARGE, ELimb.SPINE, base.channel.owner.playerID.steamID, out eplayerKill);
							}
						}
					}
				}
			}
		}

		// Token: 0x04002C2D RID: 11309
		private float startedUse;

		// Token: 0x04002C2E RID: 11310
		private float useTime;

		// Token: 0x04002C2F RID: 11311
		private float aidTime;

		// Token: 0x04002C30 RID: 11312
		private bool isUsing;

		// Token: 0x04002C31 RID: 11313
		private EConsumeMode consumeMode;

		// Token: 0x04002C32 RID: 11314
		private Player enemy;

		// Token: 0x04002C33 RID: 11315
		private bool hasAid;
	}
}
