using System;
using System.Collections;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000532 RID: 1330
	public class GermanyTeleporterB : GermanyTeleporterA
	{
		// Token: 0x060023DD RID: 9181 RVA: 0x000C7004 File Offset: 0x000C5404
		protected override IEnumerator teleport()
		{
			yield return new WaitForSeconds(1f);
			if (this.target != null)
			{
				GermanyTeleporterA.nearbyPlayers.Clear();
				PlayerTool.getPlayersInRadius(base.transform.position, this.sqrRadius, GermanyTeleporterA.nearbyPlayers);
				for (int i = 0; i < GermanyTeleporterA.nearbyPlayers.Count; i++)
				{
					Player player = GermanyTeleporterA.nearbyPlayers[i];
					if (!player.life.isDead)
					{
						if (player.quests.getQuestStatus(248) == ENPCQuestStatus.COMPLETED)
						{
							player.sendTeleport(this.target.position, MeasurementTool.angleToByte(this.target.rotation.eulerAngles.y));
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000C7020 File Offset: 0x000C5420
		private void onPlayerLifeUpdated(Player player)
		{
			if (player == null || !player.life.isDead)
			{
				return;
			}
			if ((player.transform.position - base.transform.position).sqrMagnitude > this.sqrBossRadius)
			{
				return;
			}
			if (player.quests.getQuestStatus(248) == ENPCQuestStatus.COMPLETED)
			{
				return;
			}
			player.quests.sendRemoveQuest(248);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000C70A0 File Offset: 0x000C54A0
		private void onZombieLifeUpdated(Zombie zombie)
		{
			if (!zombie.isDead)
			{
				return;
			}
			if ((zombie.transform.position - base.transform.position).sqrMagnitude > this.sqrBossRadius)
			{
				return;
			}
			GermanyTeleporterA.nearbyPlayers.Clear();
			PlayerTool.getPlayersInRadius(base.transform.position, this.sqrBossRadius, GermanyTeleporterA.nearbyPlayers);
			for (int i = 0; i < GermanyTeleporterA.nearbyPlayers.Count; i++)
			{
				Player player = GermanyTeleporterA.nearbyPlayers[i];
				if (!player.life.isDead)
				{
					player.quests.sendRemoveQuest(248);
					player.quests.sendSetFlag(248, 1);
				}
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000C716C File Offset: 0x000C556C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (!Provider.isServer)
			{
				return;
			}
			if (!this.isListeningPlayer)
			{
				PlayerLife.onPlayerLifeUpdated = (PlayerLifeUpdated)Delegate.Combine(PlayerLife.onPlayerLifeUpdated, new PlayerLifeUpdated(this.onPlayerLifeUpdated));
				this.isListeningPlayer = true;
			}
			if (this.region != null)
			{
				return;
			}
			this.region = ZombieManager.regions[this.navIndex];
			if (this.region == null)
			{
				return;
			}
			if (!this.isListeningZombie)
			{
				ZombieRegion zombieRegion = this.region;
				zombieRegion.onZombieLifeUpdated = (ZombieLifeUpdated)Delegate.Combine(zombieRegion.onZombieLifeUpdated, new ZombieLifeUpdated(this.onZombieLifeUpdated));
				this.isListeningZombie = true;
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000C7220 File Offset: 0x000C5620
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.isListeningPlayer)
			{
				PlayerLife.onPlayerLifeUpdated = (PlayerLifeUpdated)Delegate.Remove(PlayerLife.onPlayerLifeUpdated, new PlayerLifeUpdated(this.onPlayerLifeUpdated));
				this.isListeningPlayer = false;
			}
			if (this.isListeningZombie && this.region != null)
			{
				ZombieRegion zombieRegion = this.region;
				zombieRegion.onZombieLifeUpdated = (ZombieLifeUpdated)Delegate.Remove(zombieRegion.onZombieLifeUpdated, new ZombieLifeUpdated(this.onZombieLifeUpdated));
				this.isListeningZombie = false;
			}
			this.region = null;
		}

		// Token: 0x040015EC RID: 5612
		public float sqrBossRadius;

		// Token: 0x040015ED RID: 5613
		public int navIndex;

		// Token: 0x040015EE RID: 5614
		private ZombieRegion region;

		// Token: 0x040015EF RID: 5615
		private bool isListeningPlayer;

		// Token: 0x040015F0 RID: 5616
		private bool isListeningZombie;
	}
}
