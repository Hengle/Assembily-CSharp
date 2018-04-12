using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000579 RID: 1401
	public class ZombieBossQuest : MonoBehaviour
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x000E4DD0 File Offset: 0x000E31D0
		private IEnumerator teleport()
		{
			yield return new WaitForSeconds(3f);
			if (this.target != null)
			{
				ZombieBossQuest.nearbyPlayers.Clear();
				PlayerTool.getPlayersInRadius(base.transform.position, this.sqrRadius, ZombieBossQuest.nearbyPlayers);
				for (int i = 0; i < ZombieBossQuest.nearbyPlayers.Count; i++)
				{
					Player player = ZombieBossQuest.nearbyPlayers[i];
					if (!player.life.isDead)
					{
						player.quests.sendRemoveQuest(213);
						player.quests.setFlag(213, 1);
						player.sendTeleport(this.target.position, MeasurementTool.angleToByte(this.target.rotation.eulerAngles.y));
					}
				}
			}
			yield break;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000E4DEC File Offset: 0x000E31EC
		private void onPlayerLifeUpdated(Player player)
		{
			if (player == null || !player.life.isDead)
			{
				return;
			}
			if ((player.transform.position - base.transform.position).sqrMagnitude > this.sqrRadius)
			{
				return;
			}
			player.quests.sendRemoveQuest(213);
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000E4E58 File Offset: 0x000E3258
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
			EffectManager.sendEffect(this.teleportEffect, 16f, zombie.transform.position + Vector3.up);
			base.StartCoroutine("teleport");
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000E4ED4 File Offset: 0x000E32D4
		private void OnEnable()
		{
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
			byte b;
			if (LevelNavigation.tryGetBounds(base.transform.position, out b))
			{
				this.region = ZombieManager.regions[(int)b];
			}
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

		// Token: 0x060026A9 RID: 9897 RVA: 0x000E4F94 File Offset: 0x000E3394
		private void OnDisable()
		{
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

		// Token: 0x04001830 RID: 6192
		private static List<Player> nearbyPlayers = new List<Player>();

		// Token: 0x04001831 RID: 6193
		public Transform target;

		// Token: 0x04001832 RID: 6194
		public float sqrRadius;

		// Token: 0x04001833 RID: 6195
		public ushort teleportEffect;

		// Token: 0x04001834 RID: 6196
		private ZombieRegion region;

		// Token: 0x04001835 RID: 6197
		private bool isListeningPlayer;

		// Token: 0x04001836 RID: 6198
		private bool isListeningZombie;
	}
}
