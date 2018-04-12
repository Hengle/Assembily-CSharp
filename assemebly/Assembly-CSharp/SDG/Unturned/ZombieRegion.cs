using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005CB RID: 1483
	public class ZombieRegion
	{
		// Token: 0x060029EA RID: 10730 RVA: 0x00104108 File Offset: 0x00102508
		public ZombieRegion(byte newNav)
		{
			this._zombies = new List<Zombie>();
			this.nav = newNav;
			this.updates = 0;
			this.respawnZombieIndex = 0;
			this.alive = 0;
			this.isNetworked = false;
			this.lastMega = -1000f;
			this.hasMega = false;
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x0010415B File Offset: 0x0010255B
		public List<Zombie> zombies
		{
			get
			{
				return this._zombies;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x00104163 File Offset: 0x00102563
		// (set) Token: 0x060029ED RID: 10733 RVA: 0x0010416B File Offset: 0x0010256B
		public byte nav { get; protected set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x00104174 File Offset: 0x00102574
		// (set) Token: 0x060029EF RID: 10735 RVA: 0x0010417C File Offset: 0x0010257C
		public bool hasBeacon
		{
			get
			{
				return this._hasBeacon;
			}
			set
			{
				if (value != this._hasBeacon)
				{
					this._hasBeacon = value;
					if (this.onHyperUpdated != null)
					{
						this.onHyperUpdated(this.isHyper);
					}
				}
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x001041AD File Offset: 0x001025AD
		public bool isHyper
		{
			get
			{
				return LightingManager.isFullMoon || this.hasBeacon;
			}
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x001041C4 File Offset: 0x001025C4
		public void UpdateRegion()
		{
			if (this.bossZombie == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				SteamPlayer steamPlayer = Provider.clients[i];
				if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead)
				{
					if (steamPlayer.player.movement.bound == this.nav)
					{
						flag = true;
					}
					if (steamPlayer.player.movement.nav == this.nav)
					{
						flag2 = true;
					}
					if (flag && flag2)
					{
						break;
					}
				}
			}
			if (flag)
			{
				if (this.bossZombie.isDead)
				{
					this.bossZombie = null;
					if (flag2)
					{
						this.UpdateBoss();
					}
				}
			}
			else
			{
				EPlayerKill eplayerKill;
				uint num;
				this.bossZombie.askDamage(50000, Vector3.up, out eplayerKill, out num, false, false);
			}
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x001042FC File Offset: 0x001026FC
		public void UpdateBoss()
		{
			if (this.bossZombie != null)
			{
				return;
			}
			for (int i = 0; i < Provider.clients.Count; i++)
			{
				SteamPlayer steamPlayer = Provider.clients[i];
				if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead)
				{
					if (steamPlayer.player.movement.nav == this.nav)
					{
						for (int j = 0; j < steamPlayer.player.quests.questsList.Count; j++)
						{
							PlayerQuest playerQuest = steamPlayer.player.quests.questsList[j];
							if (playerQuest != null && playerQuest.asset != null)
							{
								for (int k = 0; k < playerQuest.asset.conditions.Length; k++)
								{
									NPCZombieKillsCondition npczombieKillsCondition = playerQuest.asset.conditions[k] as NPCZombieKillsCondition;
									if (npczombieKillsCondition != null)
									{
										if (npczombieKillsCondition.nav == this.nav && npczombieKillsCondition.spawn && !npczombieKillsCondition.isConditionMet(steamPlayer.player))
										{
											Zombie zombie = null;
											for (int l = 0; l < this.zombies.Count; l++)
											{
												Zombie zombie2 = this.zombies[l];
												if (zombie2 != null && zombie2.isDead)
												{
													zombie = zombie2;
													break;
												}
											}
											if (zombie == null)
											{
												for (int m = 0; m < this.zombies.Count; m++)
												{
													Zombie zombie3 = this.zombies[m];
													if (zombie3 != null && !zombie3.isHunting)
													{
														zombie = zombie3;
														break;
													}
												}
											}
											if (zombie == null)
											{
												zombie = this.zombies[UnityEngine.Random.Range(0, this.zombies.Count)];
											}
											Vector3 position = zombie.transform.position;
											if (zombie.isDead)
											{
												for (int n = 0; n < 10; n++)
												{
													ZombieSpawnpoint zombieSpawnpoint = LevelZombies.zombies[(int)this.nav][UnityEngine.Random.Range(0, LevelZombies.zombies[(int)this.nav].Count)];
													if (SafezoneManager.checkPointValid(zombieSpawnpoint.point))
													{
														break;
													}
													position = zombieSpawnpoint.point;
													position.y += 0.1f;
												}
											}
											this.bossZombie = zombie;
											this.bossZombie.sendRevive(this.bossZombie.type, (byte)npczombieKillsCondition.zombie, this.bossZombie.shirt, this.bossZombie.pants, this.bossZombie.hat, this.bossZombie.gear, position, UnityEngine.Random.Range(0f, 360f));
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00104647 File Offset: 0x00102A47
		private void onMoonUpdated(bool isFullMoon)
		{
			if (this.onHyperUpdated != null)
			{
				this.onHyperUpdated(this.isHyper);
			}
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00104668 File Offset: 0x00102A68
		public void destroy()
		{
			ushort num = 0;
			while ((int)num < this.zombies.Count)
			{
				UnityEngine.Object.Destroy(this.zombies[(int)num].gameObject);
				num += 1;
			}
			this.zombies.Clear();
			this.hasMega = false;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x001046BA File Offset: 0x00102ABA
		public void init()
		{
			LightingManager.onMoonUpdated = (MoonUpdated)Delegate.Combine(LightingManager.onMoonUpdated, new MoonUpdated(this.onMoonUpdated));
		}

		// Token: 0x040019D4 RID: 6612
		public HyperUpdated onHyperUpdated;

		// Token: 0x040019D5 RID: 6613
		public ZombieLifeUpdated onZombieLifeUpdated;

		// Token: 0x040019D6 RID: 6614
		private List<Zombie> _zombies;

		// Token: 0x040019D8 RID: 6616
		public ushort updates;

		// Token: 0x040019D9 RID: 6617
		public ushort respawnZombieIndex;

		// Token: 0x040019DA RID: 6618
		public int alive;

		// Token: 0x040019DB RID: 6619
		public bool isNetworked;

		// Token: 0x040019DC RID: 6620
		public float lastMega;

		// Token: 0x040019DD RID: 6621
		public bool hasMega;

		// Token: 0x040019DE RID: 6622
		private bool _hasBeacon;

		// Token: 0x040019DF RID: 6623
		public bool isRadioactive;

		// Token: 0x040019E0 RID: 6624
		private Zombie bossZombie;
	}
}
