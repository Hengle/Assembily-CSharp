using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200057D RID: 1405
	public class ZombieSoulTeleporter : MonoBehaviour
	{
		// Token: 0x060026B7 RID: 9911 RVA: 0x000E54EC File Offset: 0x000E38EC
		private IEnumerator teleport()
		{
			yield return new WaitForSeconds(3f);
			if (this.target != null)
			{
				ZombieSoulTeleporter.nearbyPlayers.Clear();
				PlayerTool.getPlayersInRadius(base.transform.position, this.sqrRadius, ZombieSoulTeleporter.nearbyPlayers);
				for (int i = 0; i < ZombieSoulTeleporter.nearbyPlayers.Count; i++)
				{
					Player player = ZombieSoulTeleporter.nearbyPlayers[i];
					if (!player.life.isDead)
					{
						short num;
						short num2;
						if (player.quests.getFlag(211, out num) && num == 1 && player.quests.getFlag(212, out num2) && num2 == 1 && player.quests.getQuestStatus(213) != ENPCQuestStatus.COMPLETED)
						{
							player.quests.sendSetFlag(214, 0);
							player.quests.sendAddQuest(213);
							player.sendTeleport(this.targetBoss.position, MeasurementTool.angleToByte(this.targetBoss.rotation.eulerAngles.y));
						}
						else
						{
							player.sendTeleport(this.target.position, MeasurementTool.angleToByte(this.target.rotation.eulerAngles.y));
							if (player.equipment.isSelected)
							{
								player.equipment.dequip();
							}
							player.equipment.canEquip = false;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x000E5508 File Offset: 0x000E3908
		private void onZombieLifeUpdated(Zombie zombie)
		{
			if (!zombie.isDead)
			{
				return;
			}
			if ((zombie.transform.position - base.transform.position).sqrMagnitude > this.sqrRadius)
			{
				return;
			}
			EffectManager.sendEffect(this.collectEffect, 16f, zombie.transform.position + Vector3.up, (base.transform.position - zombie.transform.position + Vector3.up).normalized);
			this.soulsCollected += 1;
			if (this.soulsCollected >= this.soulsNeeded)
			{
				EffectManager.sendEffect(this.teleportEffect, 16f, base.transform.position);
				this.soulsCollected = 0;
				base.StartCoroutine("teleport");
			}
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x000E55F0 File Offset: 0x000E39F0
		private void OnEnable()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (this.region != null)
			{
				return;
			}
			byte b;
			if (LevelNavigation.tryGetBounds(base.transform.position, out b))
			{
				this.region = ZombieManager.regions[(int)b];
			}
			if (this.region == null)
			{
				return;
			}
			if (!this.isListening)
			{
				ZombieRegion zombieRegion = this.region;
				zombieRegion.onZombieLifeUpdated = (ZombieLifeUpdated)Delegate.Combine(zombieRegion.onZombieLifeUpdated, new ZombieLifeUpdated(this.onZombieLifeUpdated));
				this.isListening = true;
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000E5680 File Offset: 0x000E3A80
		private void OnDisable()
		{
			if (this.isListening && this.region != null)
			{
				ZombieRegion zombieRegion = this.region;
				zombieRegion.onZombieLifeUpdated = (ZombieLifeUpdated)Delegate.Remove(zombieRegion.onZombieLifeUpdated, new ZombieLifeUpdated(this.onZombieLifeUpdated));
				this.isListening = false;
			}
			this.region = null;
		}

		// Token: 0x04001843 RID: 6211
		private static List<Player> nearbyPlayers = new List<Player>();

		// Token: 0x04001844 RID: 6212
		public Transform target;

		// Token: 0x04001845 RID: 6213
		public Transform targetBoss;

		// Token: 0x04001846 RID: 6214
		public float sqrRadius;

		// Token: 0x04001847 RID: 6215
		public byte soulsNeeded;

		// Token: 0x04001848 RID: 6216
		public ushort collectEffect;

		// Token: 0x04001849 RID: 6217
		public ushort teleportEffect;

		// Token: 0x0400184A RID: 6218
		private ZombieRegion region;

		// Token: 0x0400184B RID: 6219
		private byte soulsCollected;

		// Token: 0x0400184C RID: 6220
		private bool isListening;
	}
}
