using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004ED RID: 1261
	public class InteractableTrap : Interactable
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x000BB79C File Offset: 0x000B9B9C
		public override void updateState(Asset asset, byte[] state)
		{
			this.range2 = ((ItemTrapAsset)asset).range2;
			this.playerDamage = ((ItemTrapAsset)asset).playerDamage;
			this.zombieDamage = ((ItemTrapAsset)asset).zombieDamage;
			this.animalDamage = ((ItemTrapAsset)asset).animalDamage;
			this.barricadeDamage = ((ItemTrapAsset)asset).barricadeDamage;
			this.structureDamage = ((ItemTrapAsset)asset).structureDamage;
			this.vehicleDamage = ((ItemTrapAsset)asset).vehicleDamage;
			this.resourceDamage = ((ItemTrapAsset)asset).resourceDamage;
			this.objectDamage = ((ItemTrapAsset)asset).objectDamage;
			this.explosion2 = ((ItemTrapAsset)asset).explosion2;
			this.isBroken = ((ItemTrapAsset)asset).isBroken;
			this.isExplosive = ((ItemTrapAsset)asset).isExplosive;
			if (((ItemTrapAsset)asset).damageTires)
			{
				base.transform.parent.GetOrAddComponent<InteractableTrapDamageTires>();
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000BB896 File Offset: 0x000B9C96
		public override bool checkInteractable()
		{
			return false;
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000BB899 File Offset: 0x000B9C99
		private void OnEnable()
		{
			this.lastActive = Time.realtimeSinceStartup;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000BB8A8 File Offset: 0x000B9CA8
		private void OnTriggerEnter(Collider other)
		{
			if (other.isTrigger)
			{
				return;
			}
			if (Time.realtimeSinceStartup - this.lastActive < 0.25f)
			{
				return;
			}
			if (other.transform == base.transform.parent)
			{
				return;
			}
			if (Provider.isServer)
			{
				if (this.isExplosive)
				{
					if (other.transform.CompareTag("Player"))
					{
						if (Provider.isPvP && !other.transform.parent.CompareTag("Vehicle"))
						{
							EffectManager.sendEffect(this.explosion2, EffectManager.LARGE, base.transform.position);
							List<EPlayerKill> list;
							DamageTool.explode(base.transform.position, this.range2, EDeathCause.LANDMINE, CSteamID.Nil, this.playerDamage, this.zombieDamage, this.animalDamage, this.barricadeDamage, this.structureDamage, this.vehicleDamage, this.resourceDamage, this.objectDamage, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
						}
					}
					else
					{
						EffectManager.sendEffect(this.explosion2, EffectManager.LARGE, base.transform.position);
						List<EPlayerKill> list2;
						DamageTool.explode(base.transform.position, this.range2, EDeathCause.LANDMINE, CSteamID.Nil, this.playerDamage, this.zombieDamage, this.animalDamage, this.barricadeDamage, this.structureDamage, this.vehicleDamage, this.resourceDamage, this.objectDamage, out list2, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
					}
				}
				else if (other.transform.CompareTag("Player"))
				{
					if (Provider.isPvP && !other.transform.parent.CompareTag("Vehicle"))
					{
						Player player = DamageTool.getPlayer(other.transform);
						if (player != null)
						{
							EPlayerKill eplayerKill;
							DamageTool.damage(player, EDeathCause.SHRED, ELimb.SPINE, CSteamID.Nil, Vector3.up, this.playerDamage, 1f, out eplayerKill);
							if (this.isBroken)
							{
								player.life.breakLegs();
							}
							EffectManager.sendEffect(5, EffectManager.SMALL, base.transform.position + Vector3.up, Vector3.down);
							BarricadeManager.damage(base.transform.parent, 5f, 1f, false);
						}
					}
				}
				else if (other.transform.CompareTag("Agent"))
				{
					Zombie zombie = DamageTool.getZombie(other.transform);
					if (zombie != null)
					{
						EPlayerKill eplayerKill2;
						uint num;
						DamageTool.damage(zombie, base.transform.forward, this.zombieDamage, 1f, out eplayerKill2, out num);
						EffectManager.sendEffect((!zombie.isRadioactive) ? 5 : 95, EffectManager.SMALL, base.transform.position + Vector3.up, Vector3.down);
						BarricadeManager.damage(base.transform.parent, (!zombie.isHyper) ? 5f : 10f, 1f, false);
					}
					else
					{
						Animal animal = DamageTool.getAnimal(other.transform);
						if (animal != null)
						{
							EPlayerKill eplayerKill3;
							uint num2;
							DamageTool.damage(animal, base.transform.forward, this.animalDamage, 1f, out eplayerKill3, out num2);
							EffectManager.sendEffect(5, EffectManager.SMALL, base.transform.position + Vector3.up, Vector3.down);
							BarricadeManager.damage(base.transform.parent, 5f, 1f, false);
						}
					}
				}
			}
		}

		// Token: 0x0400145B RID: 5211
		private float range2;

		// Token: 0x0400145C RID: 5212
		private float playerDamage;

		// Token: 0x0400145D RID: 5213
		private float zombieDamage;

		// Token: 0x0400145E RID: 5214
		private float animalDamage;

		// Token: 0x0400145F RID: 5215
		private float barricadeDamage;

		// Token: 0x04001460 RID: 5216
		private float structureDamage;

		// Token: 0x04001461 RID: 5217
		private float vehicleDamage;

		// Token: 0x04001462 RID: 5218
		private float resourceDamage;

		// Token: 0x04001463 RID: 5219
		private float objectDamage;

		// Token: 0x04001464 RID: 5220
		private ushort explosion2;

		// Token: 0x04001465 RID: 5221
		private bool isBroken;

		// Token: 0x04001466 RID: 5222
		private bool isExplosive;

		// Token: 0x04001467 RID: 5223
		private float lastActive;
	}
}
