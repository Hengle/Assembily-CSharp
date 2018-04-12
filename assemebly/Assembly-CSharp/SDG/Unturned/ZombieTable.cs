using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200057F RID: 1407
	public class ZombieTable
	{
		// Token: 0x060026C0 RID: 9920 RVA: 0x000E59F0 File Offset: 0x000E3DF0
		public ZombieTable(string newName)
		{
			this._slots = new ZombieSlot[4];
			for (int i = 0; i < this.slots.Length; i++)
			{
				this.slots[i] = new ZombieSlot(1f, new List<ZombieCloth>());
			}
			this._color = Color.white;
			this.name = newName;
			this.isMega = false;
			this.health = 100;
			this.damage = 15;
			this.lootIndex = 0;
			this.lootID = 0;
			this.xp = 3u;
			this.regen = 10f;
			this.difficultyGUID = string.Empty;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000E5A94 File Offset: 0x000E3E94
		public ZombieTable(ZombieSlot[] newSlots, Color newColor, string newName, bool newMega, ushort newHealth, byte newDamage, byte newLootIndex, ushort newLootID, uint newXP, float newRegen, string newDifficultyGUID)
		{
			this._slots = newSlots;
			this._color = newColor;
			this.name = newName;
			this.isMega = newMega;
			this.health = newHealth;
			this.damage = newDamage;
			this.lootIndex = newLootIndex;
			this.lootID = newLootID;
			this.xp = newXP;
			this.regen = newRegen;
			this.difficultyGUID = newDifficultyGUID;
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x000E5AFC File Offset: 0x000E3EFC
		public ZombieSlot[] slots
		{
			get
			{
				return this._slots;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x000E5B04 File Offset: 0x000E3F04
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x000E5B0C File Offset: 0x000E3F0C
		public Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						ushort num = 0;
						while ((int)num < LevelZombies.spawns[(int)b, (int)b2].Count)
						{
							ZombieSpawnpoint zombieSpawnpoint = LevelZombies.spawns[(int)b, (int)b2][(int)num];
							if (zombieSpawnpoint.type == EditorSpawns.selectedZombie)
							{
								zombieSpawnpoint.node.GetComponent<Renderer>().material.color = this.color;
							}
							num += 1;
						}
						EditorSpawns.zombieSpawn.GetComponent<Renderer>().material.color = this.color;
					}
				}
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000E5BC9 File Offset: 0x000E3FC9
		// (set) Token: 0x060026C6 RID: 9926 RVA: 0x000E5BD4 File Offset: 0x000E3FD4
		public string difficultyGUID
		{
			get
			{
				return this._difficultyGUID;
			}
			set
			{
				this._difficultyGUID = value;
				try
				{
					this.difficulty = new AssetReference<ZombieDifficultyAsset>(new Guid(this.difficultyGUID));
				}
				catch
				{
					this.difficulty = AssetReference<ZombieDifficultyAsset>.invalid;
				}
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000E5C24 File Offset: 0x000E4024
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x000E5C2C File Offset: 0x000E402C
		public AssetReference<ZombieDifficultyAsset> difficulty { get; private set; }

		// Token: 0x060026C9 RID: 9929 RVA: 0x000E5C35 File Offset: 0x000E4035
		public void addCloth(byte slotIndex, ushort id)
		{
			this.slots[(int)slotIndex].addCloth(id);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000E5C45 File Offset: 0x000E4045
		public void removeCloth(byte slotIndex, byte clothIndex)
		{
			this.slots[(int)slotIndex].removeCloth(clothIndex);
		}

		// Token: 0x04001850 RID: 6224
		private ZombieSlot[] _slots;

		// Token: 0x04001851 RID: 6225
		private Color _color;

		// Token: 0x04001852 RID: 6226
		public string name;

		// Token: 0x04001853 RID: 6227
		public bool isMega;

		// Token: 0x04001854 RID: 6228
		public ushort health;

		// Token: 0x04001855 RID: 6229
		public byte damage;

		// Token: 0x04001856 RID: 6230
		public byte lootIndex;

		// Token: 0x04001857 RID: 6231
		public ushort lootID;

		// Token: 0x04001858 RID: 6232
		public uint xp;

		// Token: 0x04001859 RID: 6233
		public float regen;

		// Token: 0x0400185A RID: 6234
		private string _difficultyGUID;
	}
}
