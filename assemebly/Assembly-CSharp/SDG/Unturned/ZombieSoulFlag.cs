using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200057C RID: 1404
	public class ZombieSoulFlag : MonoBehaviour
	{
		// Token: 0x060026B2 RID: 9906 RVA: 0x000E524C File Offset: 0x000E364C
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
			ZombieSoulFlag.nearbyPlayers.Clear();
			PlayerTool.getPlayersInRadius(base.transform.position, this.sqrRadius, ZombieSoulFlag.nearbyPlayers);
			for (int i = 0; i < ZombieSoulFlag.nearbyPlayers.Count; i++)
			{
				Player player = ZombieSoulFlag.nearbyPlayers[i];
				if (!player.life.isDead)
				{
					short num;
					if (player.quests.getFlag(this.flagPlaced, out num) && num == 1)
					{
						EffectManager.sendEffect(this.collectEffect, player.channel.owner.playerID.steamID, zombie.transform.position + Vector3.up, (base.transform.position - zombie.transform.position + Vector3.up).normalized);
						short num2;
						player.quests.getFlag(this.flagKills, out num2);
						num2 += 1;
						player.quests.sendSetFlag(this.flagKills, num2);
						if (num2 >= (short)this.soulsNeeded)
						{
							EffectManager.sendEffect(this.teleportEffect, player.channel.owner.playerID.steamID, base.transform.position);
							player.quests.sendSetFlag(this.flagPlaced, 2);
						}
					}
				}
			}
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x000E53F0 File Offset: 0x000E37F0
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

		// Token: 0x060026B4 RID: 9908 RVA: 0x000E5480 File Offset: 0x000E3880
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

		// Token: 0x0400183A RID: 6202
		private static List<Player> nearbyPlayers = new List<Player>();

		// Token: 0x0400183B RID: 6203
		public ushort flagPlaced;

		// Token: 0x0400183C RID: 6204
		public ushort flagKills;

		// Token: 0x0400183D RID: 6205
		public float sqrRadius;

		// Token: 0x0400183E RID: 6206
		public byte soulsNeeded;

		// Token: 0x0400183F RID: 6207
		public ushort collectEffect;

		// Token: 0x04001840 RID: 6208
		public ushort teleportEffect;

		// Token: 0x04001841 RID: 6209
		private ZombieRegion region;

		// Token: 0x04001842 RID: 6210
		private bool isListening;
	}
}
