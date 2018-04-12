using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004BF RID: 1215
	public class Bumper : MonoBehaviour
	{
		// Token: 0x0600207F RID: 8319 RVA: 0x000B204F File Offset: 0x000B044F
		public void init(InteractableVehicle newVehicle)
		{
			this.vehicle = newVehicle;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x000B2058 File Offset: 0x000B0458
		private void OnTriggerEnter(Collider other)
		{
			if (Provider.isServer)
			{
				if (other.isTrigger)
				{
					return;
				}
				if (other.CompareTag("Debris"))
				{
					return;
				}
				float num = Mathf.Clamp(this.vehicle.speed * this.vehicle.asset.bumperMultiplier, -10f, 10f);
				if (this.reverse)
				{
					num = -num;
				}
				if (num < 3f)
				{
					return;
				}
				if (other.transform.parent.CompareTag("Vehicle"))
				{
					return;
				}
				if (other.transform.CompareTag("Player"))
				{
					if (Provider.isPvP && this.vehicle.isDriven)
					{
						Player player = DamageTool.getPlayer(other.transform);
						if (player != null && player.movement.getVehicle() == null && !this.vehicle.passengers[0].player.player.quests.isMemberOfSameGroupAs(player))
						{
							EPlayerKill eplayerKill;
							DamageTool.damage(player, EDeathCause.ROADKILL, ELimb.SPINE, this.vehicle.passengers[0].player.playerID.steamID, base.transform.forward, (!this.instakill) ? Bumper.DAMAGE_PLAYER : 101f, num, out eplayerKill);
							EffectManager.sendEffect(5, EffectManager.SMALL, other.transform.position + other.transform.up, -base.transform.forward);
							if (this.vehicle.asset.isVulnerableToBumper)
							{
								this.vehicle.askDamage(2, true);
							}
						}
					}
				}
				else if (other.transform.CompareTag("Agent"))
				{
					Zombie zombie = DamageTool.getZombie(other.transform);
					if (zombie != null)
					{
						EPlayerKill eplayerKill2;
						uint num2;
						DamageTool.damage(zombie, base.transform.forward, (!this.instakill) ? Bumper.DAMAGE_ZOMBIE : 65000f, num, out eplayerKill2, out num2);
						EffectManager.sendEffect((!zombie.isRadioactive) ? 5 : 95, EffectManager.SMALL, other.transform.position + other.transform.up, -base.transform.forward);
						if (this.vehicle.asset.isVulnerableToBumper)
						{
							this.vehicle.askDamage(2, true);
						}
					}
					else
					{
						Animal animal = DamageTool.getAnimal(other.transform);
						if (animal != null)
						{
							EPlayerKill eplayerKill3;
							uint num3;
							DamageTool.damage(animal, base.transform.forward, (!this.instakill) ? Bumper.DAMAGE_ANIMAL : 65000f, num, out eplayerKill3, out num3);
							EffectManager.sendEffect(5, EffectManager.SMALL, other.transform.position + other.transform.up, -base.transform.forward);
							if (this.vehicle.asset.isVulnerableToBumper)
							{
								this.vehicle.askDamage(2, true);
							}
						}
					}
				}
				else
				{
					if (other.transform.CompareTag("Barricade"))
					{
						Transform transform = other.transform;
						while (transform.parent != LevelBarricades.models && !transform.parent.CompareTag("Vehicle"))
						{
							transform = transform.parent;
						}
						if (this.instakill && !transform.parent.CompareTag("Vehicle"))
						{
							DamageTool.impact(base.transform.position + base.transform.forward * ((BoxCollider)base.transform.GetComponent<Collider>()).size.z / 2f, -base.transform.forward, DamageTool.getMaterial(base.transform.position, transform, other), true);
							BarricadeManager.damage(transform, 65000f, num, false);
							if (this.vehicle.asset.isVulnerableToBumper)
							{
								this.vehicle.askDamage((ushort)(Bumper.DAMAGE_VEHICLE * num), true);
							}
						}
					}
					else if (other.transform.CompareTag("Structure"))
					{
						if (this.instakill)
						{
							StructureManager.damage(other.transform, base.transform.forward, 65000f, num, false);
							DamageTool.impact(base.transform.position + base.transform.forward * ((BoxCollider)base.transform.GetComponent<Collider>()).size.z / 2f, -base.transform.forward, DamageTool.getMaterial(base.transform.position, other.transform, other.GetComponent<Collider>()), true);
							if (this.vehicle.asset.isVulnerableToBumper)
							{
								this.vehicle.askDamage((ushort)(Bumper.DAMAGE_VEHICLE * num), true);
							}
						}
					}
					else if (other.transform.CompareTag("Resource"))
					{
						DamageTool.impact(base.transform.position + base.transform.forward * ((BoxCollider)base.transform.GetComponent<Collider>()).size.z / 2f, -base.transform.forward, DamageTool.getMaterial(base.transform.position, other.transform, other.GetComponent<Collider>()), true);
						EPlayerKill eplayerKill4;
						uint num4;
						ResourceManager.damage(other.transform, base.transform.forward, (!this.instakill) ? Bumper.DAMAGE_RESOURCE : 65000f, num, 1f, out eplayerKill4, out num4);
						if (this.vehicle.asset.isVulnerableToBumper)
						{
							this.vehicle.askDamage((ushort)(Bumper.DAMAGE_VEHICLE * num), true);
						}
					}
					else
					{
						InteractableObjectRubble componentInParent = other.transform.GetComponentInParent<InteractableObjectRubble>();
						if (componentInParent != null)
						{
							EPlayerKill eplayerKill5;
							uint num5;
							DamageTool.damage(componentInParent.transform, base.transform.forward, componentInParent.getSection(other.transform), (!this.instakill) ? Bumper.DAMAGE_OBJECT : 65000f, num, out eplayerKill5, out num5);
							if (Time.realtimeSinceStartup - this.lastDamageImpact > 0.2f)
							{
								this.lastDamageImpact = Time.realtimeSinceStartup;
								DamageTool.impact(base.transform.position + base.transform.forward * ((BoxCollider)base.transform.GetComponent<Collider>()).size.z / 2f, -base.transform.forward, DamageTool.getMaterial(base.transform.position, other.transform, other.GetComponent<Collider>()), true);
								if (this.vehicle.asset.isVulnerableToBumper)
								{
									this.vehicle.askDamage((ushort)(Bumper.DAMAGE_VEHICLE * num), true);
								}
							}
						}
						else if (Time.realtimeSinceStartup - this.lastDamageImpact > 0.2f)
						{
							ObjectAsset asset = LevelObjects.getAsset(other.transform);
							if (asset != null && !asset.isSoft)
							{
								this.lastDamageImpact = Time.realtimeSinceStartup;
								DamageTool.impact(base.transform.position + base.transform.forward * ((BoxCollider)base.transform.GetComponent<Collider>()).size.z / 2f, -base.transform.forward, DamageTool.getMaterial(base.transform.position, other.transform, other.GetComponent<Collider>()), true);
								if (this.vehicle.asset.isVulnerableToBumper)
								{
									this.vehicle.askDamage((ushort)(Bumper.DAMAGE_VEHICLE * num), true);
								}
							}
						}
					}
					if (!this.vehicle.isDead && this.vehicle.asset.isVulnerableToBumper && !other.transform.CompareTag("Border") && ((this.vehicle.asset.engine == EEngine.PLANE && this.vehicle.speed > 20f) || (this.vehicle.asset.engine == EEngine.HELICOPTER && this.vehicle.speed > 10f)))
					{
						this.vehicle.askDamage(20000, false);
					}
				}
			}
		}

		// Token: 0x04001352 RID: 4946
		public bool reverse;

		// Token: 0x04001353 RID: 4947
		public bool instakill;

		// Token: 0x04001354 RID: 4948
		private static readonly float DAMAGE_PLAYER = 10f;

		// Token: 0x04001355 RID: 4949
		private static readonly float DAMAGE_ZOMBIE = 15f;

		// Token: 0x04001356 RID: 4950
		private static readonly float DAMAGE_ANIMAL = 15f;

		// Token: 0x04001357 RID: 4951
		private static readonly float DAMAGE_OBJECT = 30f;

		// Token: 0x04001358 RID: 4952
		private static readonly float DAMAGE_VEHICLE = 8f;

		// Token: 0x04001359 RID: 4953
		private static readonly float DAMAGE_RESOURCE = 85f;

		// Token: 0x0400135A RID: 4954
		private InteractableVehicle vehicle;

		// Token: 0x0400135B RID: 4955
		private float lastDamageImpact;
	}
}
