using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C6 RID: 1222
	public class InteractableBeacon : MonoBehaviour, IManualOnDestroy
	{
		// Token: 0x0600209A RID: 8346 RVA: 0x000B3035 File Offset: 0x000B1435
		public void updateState(ItemBarricadeAsset asset)
		{
			this.asset = (ItemBeaconAsset)asset;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x000B3043 File Offset: 0x000B1443
		public bool isPlant
		{
			get
			{
				return base.transform.parent != null && base.transform.parent.CompareTag("Vehicle");
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x000B3073 File Offset: 0x000B1473
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x000B307B File Offset: 0x000B147B
		public int initialParticipants { get; private set; }

		// Token: 0x0600209E RID: 8350 RVA: 0x000B3084 File Offset: 0x000B1484
		public void init(int amount)
		{
			if (this.wasInit)
			{
				return;
			}
			if (amount >= (int)this.asset.wave)
			{
				this.remaining = 0;
				this.alive = (int)this.asset.wave;
			}
			else
			{
				this.remaining = (int)this.asset.wave - amount;
				this.alive = amount;
			}
			this.wasInit = true;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000B30EC File Offset: 0x000B14EC
		public int getRemaining()
		{
			return this.remaining;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000B30F4 File Offset: 0x000B14F4
		public void spawnRemaining()
		{
			if (this.remaining <= 0)
			{
				return;
			}
			this.remaining--;
			this.alive++;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000B311F File Offset: 0x000B151F
		public int getAlive()
		{
			return this.alive;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x000B3128 File Offset: 0x000B1528
		public void despawnAlive()
		{
			if (this.alive <= 0)
			{
				return;
			}
			this.alive--;
			if (this.remaining == 0 && this.alive == 0)
			{
				BarricadeManager.damage(base.transform, 10000f, 1f, false);
			}
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000B317C File Offset: 0x000B157C
		private void Update()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (Time.realtimeSinceStartup - this.started < 3f)
			{
				return;
			}
			if (this.isRegistered)
			{
				for (int i = 0; i < Provider.clients.Count; i++)
				{
					SteamPlayer steamPlayer = Provider.clients[i];
					if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead)
					{
						if (steamPlayer.player.movement.nav == this.nav)
						{
							return;
						}
					}
				}
			}
			BarricadeManager.damage(base.transform, 10000f, 1f, false);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x000B3268 File Offset: 0x000B1668
		private void Start()
		{
			this.started = Time.realtimeSinceStartup;
			Transform transform = base.transform.FindChild("Engine");
			if (transform != null)
			{
				transform.gameObject.SetActive(true);
			}
			if (!Provider.isServer)
			{
				return;
			}
			if (this.isRegistered)
			{
				return;
			}
			if (this.isPlant)
			{
				return;
			}
			if (!LevelNavigation.checkNavigation(base.transform.position))
			{
				return;
			}
			LevelNavigation.tryGetNavigation(base.transform.position, out this.nav);
			this.initialParticipants = BeaconManager.getParticipants(this.nav);
			BeaconManager.registerBeacon(this.nav, this);
			this.isRegistered = true;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x000B3320 File Offset: 0x000B1720
		public void ManualOnDestroy()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (!this.isRegistered)
			{
				return;
			}
			BeaconManager.deregisterBeacon(this.nav, this);
			this.isRegistered = false;
			if (!this.wasInit)
			{
				return;
			}
			if (this.remaining > 0 || this.alive > 0)
			{
				return;
			}
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				if (Provider.clients[i].player != null && !Provider.clients[i].player.life.isDead && Provider.clients[i].player.movement.nav == this.nav)
				{
					Provider.clients[i].player.quests.trackHordeKill();
				}
			}
			int num = (int)this.asset.rewards;
			num *= Mathf.Max(1, this.initialParticipants);
			for (int j = 0; j < num; j++)
			{
				ushort num2 = SpawnTableTool.resolve(this.asset.rewardID);
				if (num2 != 0)
				{
					ItemManager.dropItem(new Item(num2, EItemOrigin.NATURE), base.transform.position, false, true, true);
				}
			}
		}

		// Token: 0x0400136E RID: 4974
		private ItemBeaconAsset asset;

		// Token: 0x04001370 RID: 4976
		private byte nav;

		// Token: 0x04001371 RID: 4977
		private bool wasInit;

		// Token: 0x04001372 RID: 4978
		private float started;

		// Token: 0x04001373 RID: 4979
		private int remaining;

		// Token: 0x04001374 RID: 4980
		private int alive;

		// Token: 0x04001375 RID: 4981
		private bool isRegistered;
	}
}
