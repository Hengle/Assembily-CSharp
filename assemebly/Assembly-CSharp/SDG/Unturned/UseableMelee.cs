﻿using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007BB RID: 1979
	public class UseableMelee : Useable
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x001B7C84 File Offset: 0x001B6084
		private bool isUseable
		{
			get
			{
				if (this.swingMode == ESwingMode.WEAK)
				{
					return base.player.input.simulation - this.startedUse > this.weakTime;
				}
				return this.swingMode == ESwingMode.STRONG && base.player.input.simulation - this.startedUse > this.strongTime;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x001B7CEC File Offset: 0x001B60EC
		private bool isDamageable
		{
			get
			{
				if (this.swingMode == ESwingMode.WEAK)
				{
					return base.player.input.simulation - this.startedUse > this.weakTime * ((ItemMeleeAsset)base.player.equipment.asset).weak;
				}
				return this.swingMode == ESwingMode.STRONG && base.player.input.simulation - this.startedUse > this.strongTime * ((ItemMeleeAsset)base.player.equipment.asset).strong;
			}
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x001B7D90 File Offset: 0x001B6190
		private void swing()
		{
			this.startedUse = base.player.input.simulation;
			this.startedSwing = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.isSwinging = true;
			if (this.swingMode == ESwingMode.WEAK)
			{
				base.player.animator.play("Weak", false);
			}
			else if (this.swingMode == ESwingMode.STRONG)
			{
				base.player.animator.play("Strong", false);
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x001B7E14 File Offset: 0x001B6214
		private void startSwing()
		{
			this.startedUse = base.player.input.simulation;
			this.startedSwing = Time.realtimeSinceStartup;
			this.isUsing = true;
			this.isSwinging = true;
			base.player.animator.play("Start_Swing", false);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x001B7E66 File Offset: 0x001B6266
		private void stopSwing()
		{
			this.isUsing = false;
			this.isSwinging = false;
			base.player.animator.play("Stop_Swing", false);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x001B7E8C File Offset: 0x001B628C
		[SteamCall]
		public void askInteractMelee(CSteamID steamID)
		{
			if (base.channel.checkOwner(steamID) && Provider.isServer)
			{
				if (base.player.equipment.isBusy)
				{
					return;
				}
				if (base.player.equipment.asset == null)
				{
					return;
				}
				if (!((ItemMeleeAsset)base.player.equipment.asset).isLight)
				{
					return;
				}
				this.interact = !this.interact;
				base.player.equipment.state[0] = ((!this.interact) ? 0 : 1);
				base.player.equipment.sendUpdateState();
				EffectManager.sendEffect(8, EffectManager.SMALL, base.transform.position);
			}
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x001B7F5A File Offset: 0x001B635A
		[SteamCall]
		public void askSwingStart(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.startSwing();
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x001B7F88 File Offset: 0x001B6388
		[SteamCall]
		public void askSwingStop(CSteamID steamID)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.stopSwing();
			}
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x001B7FB6 File Offset: 0x001B63B6
		[SteamCall]
		public void askSwing(CSteamID steamID, byte mode)
		{
			if (base.channel.checkServer(steamID) && base.player.equipment.isEquipped)
			{
				this.swingMode = (ESwingMode)mode;
				this.swing();
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x001B7FEC File Offset: 0x001B63EC
		private void fire()
		{
			float num = (float)base.player.equipment.quality / 100f;
			if (Provider.isServer)
			{
				AlertTool.alert(base.transform.position, ((ItemMeleeAsset)base.player.equipment.asset).alertRadius);
				if (Provider.modeConfigData.Items.Has_Durability && base.player.equipment.quality > 0 && UnityEngine.Random.value < ((ItemWeaponAsset)base.player.equipment.asset).durability)
				{
					if (base.player.equipment.quality > ((ItemWeaponAsset)base.player.equipment.asset).wear)
					{
						PlayerEquipment equipment = base.player.equipment;
						equipment.quality -= ((ItemWeaponAsset)base.player.equipment.asset).wear;
					}
					else
					{
						base.player.equipment.quality = 0;
					}
					base.player.equipment.sendUpdateQuality();
				}
			}
			if (base.channel.isOwner)
			{
				int num2;
				if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Shot", out num2))
				{
					Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Shot", num2 + 1);
				}
				Ray ray = new Ray(base.player.look.aim.position, base.player.look.aim.forward);
				RaycastInfo raycastInfo = DamageTool.raycast(ray, ((ItemWeaponAsset)base.player.equipment.asset).range, RayMasks.DAMAGE_CLIENT);
				if (raycastInfo.player != null && ((ItemMeleeAsset)base.player.equipment.asset).playerDamageMultiplier.damage > 1f && !base.player.quests.isMemberOfSameGroupAs(raycastInfo.player) && Provider.isPvP)
				{
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
					}
					if (raycastInfo.limb == ELimb.SKULL && Provider.provider.statisticsService.userStatisticsService.getStatistic("Headshots", out num2))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Headshots", num2 + 1);
					}
					PlayerUI.hitmark(0, raycastInfo.point, false, (raycastInfo.limb != ELimb.SKULL) ? EPlayerHit.ENTITIY : EPlayerHit.CRITICAL);
				}
				else if ((raycastInfo.zombie != null && ((ItemMeleeAsset)base.player.equipment.asset).zombieDamageMultiplier.damage > 1f) || (raycastInfo.animal != null && ((ItemMeleeAsset)base.player.equipment.asset).animalDamageMultiplier.damage > 1f))
				{
					if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
					}
					if (raycastInfo.limb == ELimb.SKULL && Provider.provider.statisticsService.userStatisticsService.getStatistic("Headshots", out num2))
					{
						Provider.provider.statisticsService.userStatisticsService.setStatistic("Headshots", num2 + 1);
					}
					PlayerUI.hitmark(0, raycastInfo.point, false, (raycastInfo.limb != ELimb.SKULL) ? EPlayerHit.ENTITIY : EPlayerHit.CRITICAL);
				}
				else if (raycastInfo.vehicle != null && ((ItemMeleeAsset)base.player.equipment.asset).vehicleDamage > 1f)
				{
					if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
					{
						if (!raycastInfo.vehicle.isExploded && !raycastInfo.vehicle.isRepaired)
						{
							if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
							{
								Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
							}
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
					else if (!raycastInfo.vehicle.isDead && raycastInfo.vehicle.asset != null && raycastInfo.vehicle.canBeDamaged && (raycastInfo.vehicle.asset.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable))
					{
						if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
						{
							Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
						}
						PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
					}
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Barricade") && ((ItemMeleeAsset)base.player.equipment.asset).barricadeDamage > 1f)
				{
					InteractableDoorHinge component = raycastInfo.transform.GetComponent<InteractableDoorHinge>();
					if (component != null)
					{
						raycastInfo.transform = component.transform.parent.parent;
					}
					ushort id;
					if (ushort.TryParse(raycastInfo.transform.name, out id))
					{
						ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
						if (itemBarricadeAsset != null)
						{
							if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
							{
								Interactable2HP component2 = raycastInfo.transform.GetComponent<Interactable2HP>();
								if (component2 != null && itemBarricadeAsset.isRepairable && component2.hp < 100)
								{
									if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
									{
										Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
									}
									PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
								}
							}
							else if (itemBarricadeAsset.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable)
							{
								if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
								{
									Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
								}
								PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
							}
						}
					}
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Structure") && ((ItemMeleeAsset)base.player.equipment.asset).structureDamage > 1f)
				{
					ushort id2;
					if (ushort.TryParse(raycastInfo.transform.name, out id2))
					{
						ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id2);
						if (itemStructureAsset != null)
						{
							if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
							{
								Interactable2HP component3 = raycastInfo.transform.GetComponent<Interactable2HP>();
								if (component3 != null && itemStructureAsset.isRepairable && component3.hp < 100)
								{
									if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
									{
										Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
									}
									PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
								}
							}
							else if (itemStructureAsset.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable)
							{
								if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
								{
									Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
								}
								PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
							}
						}
					}
				}
				else if (raycastInfo.transform != null && raycastInfo.transform.CompareTag("Resource") && ((ItemMeleeAsset)base.player.equipment.asset).resourceDamage > 1f)
				{
					byte x;
					byte y;
					ushort index;
					if (ResourceManager.tryGetRegion(raycastInfo.transform, out x, out y, out index))
					{
						ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(x, y, index);
						if (resourceSpawnpoint != null && !resourceSpawnpoint.isDead && resourceSpawnpoint.asset.bladeID == ((ItemWeaponAsset)base.player.equipment.asset).bladeID)
						{
							if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
							{
								Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
							}
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				else if (raycastInfo.transform != null && ((ItemMeleeAsset)base.player.equipment.asset).objectDamage > 1f)
				{
					InteractableObjectRubble component4 = raycastInfo.transform.GetComponent<InteractableObjectRubble>();
					if (component4 != null)
					{
						raycastInfo.section = component4.getSection(raycastInfo.collider.transform);
						if (!component4.isSectionDead(raycastInfo.section) && (component4.asset.rubbleIsVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable))
						{
							if (Provider.provider.statisticsService.userStatisticsService.getStatistic("Accuracy_Hit", out num2))
							{
								Provider.provider.statisticsService.userStatisticsService.setStatistic("Accuracy_Hit", num2 + 1);
							}
							PlayerUI.hitmark(0, raycastInfo.point, false, EPlayerHit.BUILD);
						}
					}
				}
				base.player.input.sendRaycast(raycastInfo);
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
				if ((input.point - base.player.look.aim.position).sqrMagnitude > Mathf.Pow(((ItemMeleeAsset)base.player.equipment.asset).range + 4f, 2f))
				{
					return;
				}
				if (!((ItemMeleeAsset)base.player.equipment.asset).isRepair)
				{
					DamageTool.impact(input.point, input.normal, input.material, input.type != ERaycastInfoType.NONE && input.type != ERaycastInfoType.OBJECT);
				}
				EPlayerKill eplayerKill = EPlayerKill.NONE;
				uint num3 = 0u;
				float num4 = 1f;
				num4 *= 1f + base.channel.owner.player.skills.mastery(0, 0) * 0.5f;
				num4 *= ((this.swingMode != ESwingMode.STRONG) ? 1f : ((ItemMeleeAsset)base.player.equipment.asset).strength);
				num4 *= ((num >= 0.5f) ? 1f : (0.5f + num));
				if (input.type == ERaycastInfoType.PLAYER)
				{
					if (input.player != null && !base.player.quests.isMemberOfSameGroupAs(input.player) && Provider.isPvP)
					{
						DamageTool.damage(input.player, EDeathCause.MELEE, input.limb, base.channel.owner.playerID.steamID, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).playerDamageMultiplier, num4, true, out eplayerKill);
					}
				}
				else if (input.type == ERaycastInfoType.ZOMBIE)
				{
					if (input.zombie != null)
					{
						DamageTool.damage(input.zombie, input.limb, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).zombieDamageMultiplier, num4, true, out eplayerKill, out num3);
						if (base.player.movement.nav != 255)
						{
							input.zombie.alert(base.transform.position, true);
						}
					}
				}
				else if (input.type == ERaycastInfoType.ANIMAL)
				{
					if (input.animal != null)
					{
						DamageTool.damage(input.animal, input.limb, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).animalDamageMultiplier, num4, out eplayerKill, out num3);
						input.animal.alertPoint(base.transform.position, true);
					}
				}
				else if (input.type == ERaycastInfoType.VEHICLE)
				{
					if (input.vehicle != null && input.vehicle.asset != null && input.vehicle.canBeDamaged && (input.vehicle.asset.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable || ((ItemMeleeAsset)base.player.equipment.asset).isRepair))
					{
						if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
						{
							num4 *= 1f + base.channel.owner.player.skills.mastery(2, 6);
						}
						DamageTool.damage(input.vehicle, true, input.point, ((ItemMeleeAsset)base.player.equipment.asset).isRepair, ((ItemMeleeAsset)base.player.equipment.asset).vehicleDamage, num4, true, out eplayerKill);
					}
				}
				else if (input.type == ERaycastInfoType.BARRICADE)
				{
					ushort id3;
					if (input.transform != null && input.transform.CompareTag("Barricade") && ushort.TryParse(input.transform.name, out id3))
					{
						ItemBarricadeAsset itemBarricadeAsset2 = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id3);
						if (itemBarricadeAsset2 != null)
						{
							if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
							{
								if (itemBarricadeAsset2.isRepairable)
								{
									num4 *= 1f + base.channel.owner.player.skills.mastery(2, 6);
									DamageTool.damage(input.transform, true, ((ItemMeleeAsset)base.player.equipment.asset).barricadeDamage, num4, out eplayerKill);
								}
							}
							else if (itemBarricadeAsset2.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable)
							{
								DamageTool.damage(input.transform, false, ((ItemMeleeAsset)base.player.equipment.asset).barricadeDamage, num4, out eplayerKill);
							}
						}
					}
				}
				else if (input.type == ERaycastInfoType.STRUCTURE)
				{
					ushort id4;
					if (input.transform != null && input.transform.CompareTag("Structure") && ushort.TryParse(input.transform.name, out id4))
					{
						ItemStructureAsset itemStructureAsset2 = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id4);
						if (itemStructureAsset2 != null)
						{
							if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
							{
								if (itemStructureAsset2.isRepairable)
								{
									num4 *= 1f + base.channel.owner.player.skills.mastery(2, 6);
									DamageTool.damage(input.transform, true, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).structureDamage, num4, out eplayerKill);
								}
							}
							else if (itemStructureAsset2.isVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable)
							{
								DamageTool.damage(input.transform, false, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).structureDamage, num4, out eplayerKill);
							}
						}
					}
				}
				else if (input.type == ERaycastInfoType.RESOURCE)
				{
					if (input.transform != null && input.transform.CompareTag("Resource"))
					{
						num4 *= 1f + base.channel.owner.player.skills.mastery(2, 2) * 0.5f;
						byte x2;
						byte y2;
						ushort index2;
						if (ResourceManager.tryGetRegion(input.transform, out x2, out y2, out index2))
						{
							ResourceSpawnpoint resourceSpawnpoint2 = ResourceManager.getResourceSpawnpoint(x2, y2, index2);
							if (resourceSpawnpoint2 != null && !resourceSpawnpoint2.isDead && resourceSpawnpoint2.asset.bladeID == ((ItemWeaponAsset)base.player.equipment.asset).bladeID)
							{
								DamageTool.damage(input.transform, input.direction, ((ItemMeleeAsset)base.player.equipment.asset).resourceDamage, num4, 1f + base.channel.owner.player.skills.mastery(2, 2) * 0.5f, out eplayerKill, out num3);
							}
						}
					}
				}
				else if (input.type == ERaycastInfoType.OBJECT && input.transform != null && input.section < 255)
				{
					InteractableObjectRubble component5 = input.transform.GetComponent<InteractableObjectRubble>();
					if (component5 != null && !component5.isSectionDead(input.section) && (component5.asset.rubbleIsVulnerable || ((ItemWeaponAsset)base.player.equipment.asset).isInvulnerable))
					{
						DamageTool.damage(input.transform, input.direction, input.section, ((ItemMeleeAsset)base.player.equipment.asset).objectDamage, num4, out eplayerKill, out num3);
					}
				}
				if (input.type != ERaycastInfoType.PLAYER && input.type != ERaycastInfoType.ZOMBIE && input.type != ERaycastInfoType.ANIMAL && !base.player.life.isAggressor)
				{
					float num5 = ((ItemMeleeAsset)base.player.equipment.asset).range + Provider.modeConfigData.Players.Ray_Aggressor_Distance;
					num5 *= num5;
					float num6 = Provider.modeConfigData.Players.Ray_Aggressor_Distance;
					num6 *= num6;
					Vector3 forward = base.player.look.aim.forward;
					for (int i = 0; i < Provider.clients.Count; i++)
					{
						if (Provider.clients[i] != base.channel.owner)
						{
							Player player = Provider.clients[i].player;
							if (!(player == null))
							{
								Vector3 vector = player.look.aim.position - base.player.look.aim.position;
								Vector3 a = Vector3.Project(vector, forward);
								if (a.sqrMagnitude < num5 && (a - vector).sqrMagnitude < num6)
								{
									base.player.life.markAggressive(false, true);
								}
							}
						}
					}
				}
				if (Level.info.type == ELevelType.HORDE)
				{
					if (input.zombie != null)
					{
						if (input.limb == ELimb.SKULL)
						{
							base.player.skills.askPay(10u);
						}
						else
						{
							base.player.skills.askPay(5u);
						}
					}
					if (eplayerKill == EPlayerKill.ZOMBIE)
					{
						if (input.limb == ELimb.SKULL)
						{
							base.player.skills.askPay(50u);
						}
						else
						{
							base.player.skills.askPay(25u);
						}
					}
				}
				else
				{
					if (eplayerKill == EPlayerKill.PLAYER && Level.info.type == ELevelType.ARENA)
					{
						base.player.skills.askPay(100u);
					}
					base.player.sendStat(eplayerKill);
					if (num3 > 0u)
					{
						base.player.skills.askPay(num3);
					}
				}
			}
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x001B95E4 File Offset: 0x001B79E4
		public override void startPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated)
			{
				if (!this.isSwinging)
				{
					this.swingMode = ESwingMode.WEAK;
					this.startSwing();
					if (Provider.isServer)
					{
						base.channel.send("askSwingStart", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
					}
				}
			}
			else if (this.isUseable)
			{
				base.player.equipment.isBusy = true;
				this.startedUse = base.player.input.simulation;
				this.startedSwing = Time.realtimeSinceStartup;
				this.isUsing = true;
				this.swingMode = ESwingMode.WEAK;
				this.swing();
				if (Provider.isServer)
				{
					base.channel.send("askSwing", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						0
					});
				}
			}
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x001B96E4 File Offset: 0x001B7AE4
		public override void stopPrimary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated && this.isSwinging)
			{
				this.stopSwing();
				if (Provider.isServer)
				{
					base.channel.send("askSwingStop", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x001B975C File Offset: 0x001B7B5C
		public override void startSecondary()
		{
			if (base.player.equipment.isBusy)
			{
				return;
			}
			if (!((ItemMeleeAsset)base.player.equipment.asset).isRepeated && this.isUseable && (float)base.player.life.stamina >= (float)((ItemMeleeAsset)base.player.equipment.asset).stamina * (1f - base.player.skills.mastery(0, 4) * 0.75f))
			{
				base.player.life.askTire((byte)((float)((ItemMeleeAsset)base.player.equipment.asset).stamina * (1f - base.player.skills.mastery(0, 4) * 0.5f)));
				base.player.equipment.isBusy = true;
				this.swingMode = ESwingMode.STRONG;
				this.swing();
				if (Provider.isServer)
				{
					base.channel.send("askSwing", ESteamCall.NOT_OWNER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
					{
						1
					});
				}
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x001B988E File Offset: 0x001B7C8E
		public override bool canInspect
		{
			get
			{
				return !this.isUsing && !this.isSwinging;
			}
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x001B98A8 File Offset: 0x001B7CA8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			if (((ItemMeleeAsset)base.player.equipment.asset).isLight)
			{
				this.interact = (base.player.equipment.state[0] == 1);
				if (base.channel.isOwner)
				{
					this.firstLightHook = base.player.equipment.firstModel.FindChild("Model_0").FindChild("Light");
					this.firstLightHook.tag = "Viewmodel";
					this.firstLightHook.gameObject.layer = LayerMasks.VIEWMODEL;
					Transform transform = this.firstLightHook.FindChild("Light");
					if (transform != null)
					{
						transform.tag = "Viewmodel";
						transform.gameObject.layer = LayerMasks.VIEWMODEL;
					}
					PlayerUI.message(EPlayerMessage.LIGHT, string.Empty);
				}
				this.thirdLightHook = base.player.equipment.thirdModel.FindChild("Model_0").FindChild("Light");
				LightLODTool.applyLightLOD(this.thirdLightHook);
				if (base.channel.isOwner && this.thirdLightHook != null)
				{
					Transform transform2 = this.thirdLightHook.FindChild("Light");
					if (transform2 != null)
					{
						this.firstFakeLight = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject).transform;
						this.firstFakeLight.name = "Emitter";
						this.firstFakeLight.parent = Level.effects;
					}
				}
			}
			else
			{
				this.firstLightHook = null;
				this.thirdLightHook = null;
			}
			this.updateAttachments();
			if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated)
			{
				if (base.channel.isOwner && base.player.equipment.firstModel.FindChild("Hit") != null)
				{
					this.firstEmitter = base.player.equipment.firstModel.FindChild("Hit").GetComponent<ParticleSystem>();
					this.firstEmitter.tag = "Viewmodel";
					this.firstEmitter.gameObject.layer = LayerMasks.VIEWMODEL;
				}
				if (base.player.equipment.thirdModel.FindChild("Hit") != null)
				{
					this.thirdEmitter = base.player.equipment.thirdModel.FindChild("Hit").GetComponent<ParticleSystem>();
				}
				this.weakTime = (uint)(base.player.animator.getAnimationLength("Start_Swing") / PlayerInput.RATE);
				this.strongTime = (uint)(base.player.animator.getAnimationLength("Stop_Swing") / PlayerInput.RATE);
			}
			else
			{
				this.weakTime = (uint)(base.player.animator.getAnimationLength("Weak") / PlayerInput.RATE);
				this.strongTime = (uint)(base.player.animator.getAnimationLength("Strong") / PlayerInput.RATE);
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x001B9BE0 File Offset: 0x001B7FE0
		public override void dequip()
		{
			base.player.updateSpot(false);
			if (base.channel.isOwner)
			{
				base.player.animator.viewOffset = Vector3.zero;
				if (this.firstFakeLight != null)
				{
					UnityEngine.Object.Destroy(this.firstFakeLight.gameObject);
					this.firstFakeLight = null;
				}
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x001B9C46 File Offset: 0x001B8046
		public override void updateState(byte[] newState)
		{
			if (((ItemMeleeAsset)base.player.equipment.asset).isLight)
			{
				this.interact = (newState[0] == 1);
			}
			this.updateAttachments();
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x001B9C7C File Offset: 0x001B807C
		public override void tick()
		{
			if (!base.player.equipment.isEquipped)
			{
				return;
			}
			if (!Dedicator.isDedicated && this.isSwinging)
			{
				if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated)
				{
					if ((double)(Time.realtimeSinceStartup - this.startedSwing) > 0.1)
					{
						this.startedSwing = Time.realtimeSinceStartup;
						if (this.firstEmitter != null && base.player.look.perspective == EPlayerPerspective.FIRST)
						{
							this.firstEmitter.Emit(4);
						}
						if (this.thirdEmitter != null && (!base.channel.isOwner || base.player.look.perspective == EPlayerPerspective.THIRD))
						{
							this.thirdEmitter.Emit(4);
						}
						if (((ItemMeleeAsset)base.player.equipment.asset).isRepair)
						{
							base.player.playSound(((ItemMeleeAsset)base.player.equipment.asset).use, 0.1f);
						}
						else
						{
							base.player.playSound(((ItemMeleeAsset)base.player.equipment.asset).use, 0.5f);
						}
					}
				}
				else if (this.isDamageable)
				{
					if (this.swingMode == ESwingMode.WEAK)
					{
						base.player.playSound(((ItemMeleeAsset)base.player.equipment.asset).use, 0.5f);
					}
					else if (this.swingMode == ESwingMode.STRONG)
					{
						base.player.playSound(((ItemMeleeAsset)base.player.equipment.asset).use, 0.5f, 0.7f, 0.1f);
					}
					this.isSwinging = false;
				}
			}
			if (base.channel.isOwner)
			{
				if (this.isSwinging)
				{
					if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated && !((ItemMeleeAsset)base.player.equipment.asset).isRepair)
					{
						base.player.animator.viewOffset = new Vector3(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f));
					}
					else
					{
						base.player.animator.viewOffset = Vector3.zero;
					}
				}
				if (Input.GetKeyDown(ControlsSettings.tactical) && ((ItemMeleeAsset)base.player.equipment.asset).isLight)
				{
					base.channel.send("askInteractMelee", ESteamCall.SERVER, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[0]);
				}
			}
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x001B9F74 File Offset: 0x001B8374
		public override void simulate(uint simulation, bool inputSteady)
		{
			if (this.isUsing && this.isDamageable)
			{
				if (((ItemMeleeAsset)base.player.equipment.asset).isRepeated)
				{
					this.startedUse = base.player.input.simulation;
				}
				else
				{
					base.player.equipment.isBusy = false;
					this.isUsing = false;
				}
				this.fire();
			}
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x001B9FF0 File Offset: 0x001B83F0
		private void updateAttachments()
		{
			if (((ItemMeleeAsset)base.player.equipment.asset).isLight)
			{
				if (!Dedicator.isDedicated)
				{
					if (base.channel.isOwner && this.firstLightHook != null)
					{
						this.firstLightHook.gameObject.SetActive(this.interact);
					}
					if (this.thirdLightHook != null)
					{
						this.thirdLightHook.gameObject.SetActive(this.interact);
					}
				}
				base.player.updateSpot(this.interact);
			}
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x001BA098 File Offset: 0x001B8498
		private void Update()
		{
			if (base.channel.isOwner && this.firstFakeLight != null && this.thirdLightHook != null)
			{
				this.firstFakeLight.position = this.thirdLightHook.position;
				if (this.firstFakeLight.gameObject.activeSelf != (base.player.look.perspective == EPlayerPerspective.FIRST && this.thirdLightHook.gameObject.activeSelf))
				{
					this.firstFakeLight.gameObject.SetActive(base.player.look.perspective == EPlayerPerspective.FIRST && this.thirdLightHook.gameObject.activeSelf);
				}
			}
		}

		// Token: 0x04002CD4 RID: 11476
		private uint startedUse;

		// Token: 0x04002CD5 RID: 11477
		private float startedSwing;

		// Token: 0x04002CD6 RID: 11478
		private uint weakTime;

		// Token: 0x04002CD7 RID: 11479
		private uint strongTime;

		// Token: 0x04002CD8 RID: 11480
		private bool isUsing;

		// Token: 0x04002CD9 RID: 11481
		private bool isSwinging;

		// Token: 0x04002CDA RID: 11482
		private ESwingMode swingMode;

		// Token: 0x04002CDB RID: 11483
		private ParticleSystem firstEmitter;

		// Token: 0x04002CDC RID: 11484
		private ParticleSystem thirdEmitter;

		// Token: 0x04002CDD RID: 11485
		private Transform firstLightHook;

		// Token: 0x04002CDE RID: 11486
		private Transform thirdLightHook;

		// Token: 0x04002CDF RID: 11487
		private Transform firstFakeLight;

		// Token: 0x04002CE0 RID: 11488
		private bool interact;
	}
}
