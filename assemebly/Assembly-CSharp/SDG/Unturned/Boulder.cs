using System;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200071A RID: 1818
	public class Boulder : MonoBehaviour
	{
		// Token: 0x0600339D RID: 13213 RVA: 0x0014ECF0 File Offset: 0x0014D0F0
		private void OnTriggerEnter(Collider other)
		{
			if (this.isExploded)
			{
				return;
			}
			if (other.isTrigger)
			{
				return;
			}
			if (other.transform.CompareTag("Agent"))
			{
				return;
			}
			this.isExploded = true;
			Vector3 normalized = (base.transform.position - this.lastPos).normalized;
			if (Provider.isServer)
			{
				float num = Mathf.Clamp(base.transform.parent.GetComponent<Rigidbody>().velocity.magnitude, 0f, 20f);
				if (num < 3f)
				{
					return;
				}
				if (other.transform.CompareTag("Player"))
				{
					Player player = DamageTool.getPlayer(other.transform);
					if (player != null)
					{
						EPlayerKill eplayerKill;
						DamageTool.damage(player, EDeathCause.BOULDER, ELimb.SPINE, CSteamID.Nil, normalized, Boulder.DAMAGE_PLAYER, num, out eplayerKill);
					}
				}
				else if (other.transform.CompareTag("Vehicle"))
				{
					InteractableVehicle component = other.transform.GetComponent<InteractableVehicle>();
					if (component != null && component.asset != null && component.asset.isVulnerableToEnvironment)
					{
						VehicleManager.damage(component, Boulder.DAMAGE_VEHICLE, num, true);
					}
				}
				else if (other.transform.CompareTag("Barricade"))
				{
					Transform transform = other.transform;
					InteractableDoorHinge component2 = transform.GetComponent<InteractableDoorHinge>();
					if (component2 != null)
					{
						transform = component2.transform.parent.parent;
					}
					BarricadeManager.damage(transform, Boulder.DAMAGE_BARRICADE, num, true);
				}
				else if (other.transform.CompareTag("Structure"))
				{
					StructureManager.damage(other.transform, normalized, Boulder.DAMAGE_STRUCTURE, num, true);
				}
				else if (other.transform.CompareTag("Resource"))
				{
					EPlayerKill eplayerKill2;
					uint num2;
					ResourceManager.damage(other.transform, normalized, Boulder.DAMAGE_RESOURCE, num, 1f, out eplayerKill2, out num2);
				}
				else
				{
					InteractableObjectRubble componentInParent = other.transform.GetComponentInParent<InteractableObjectRubble>();
					if (componentInParent != null)
					{
						EPlayerKill eplayerKill3;
						uint num3;
						DamageTool.damage(componentInParent.transform, normalized, componentInParent.getSection(other.transform), Boulder.DAMAGE_OBJECT, num, out eplayerKill3, out num3);
					}
				}
			}
			if (!Dedicator.isDedicated)
			{
				EffectManager.effect(52, base.transform.position, -normalized);
			}
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x0014EF5C File Offset: 0x0014D35C
		private void FixedUpdate()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x0014EF6F File Offset: 0x0014D36F
		private void Awake()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x04002303 RID: 8963
		private static readonly float DAMAGE_PLAYER = 3f;

		// Token: 0x04002304 RID: 8964
		private static readonly float DAMAGE_BARRICADE = 15f;

		// Token: 0x04002305 RID: 8965
		private static readonly float DAMAGE_STRUCTURE = 15f;

		// Token: 0x04002306 RID: 8966
		private static readonly float DAMAGE_OBJECT = 25f;

		// Token: 0x04002307 RID: 8967
		private static readonly float DAMAGE_VEHICLE = 10f;

		// Token: 0x04002308 RID: 8968
		private static readonly float DAMAGE_RESOURCE = 25f;

		// Token: 0x04002309 RID: 8969
		private bool isExploded;

		// Token: 0x0400230A RID: 8970
		private Vector3 lastPos;
	}
}
