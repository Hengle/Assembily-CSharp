using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200051C RID: 1308
	public class AnimalTable
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x000C5659 File Offset: 0x000C3A59
		public AnimalTable(string newName)
		{
			this._tiers = new List<AnimalTier>();
			this._color = Color.white;
			this.name = newName;
			this.tableID = 0;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000C5685 File Offset: 0x000C3A85
		public AnimalTable(List<AnimalTier> newTiers, Color newColor, string newName, ushort newTableID)
		{
			this._tiers = newTiers;
			this._color = newColor;
			this.name = newName;
			this.tableID = newTableID;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000C56AA File Offset: 0x000C3AAA
		public List<AnimalTier> tiers
		{
			get
			{
				return this._tiers;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000C56B2 File Offset: 0x000C3AB2
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x000C56BC File Offset: 0x000C3ABC
		public Color color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
				ushort num = 0;
				while ((int)num < LevelAnimals.spawns.Count)
				{
					AnimalSpawnpoint animalSpawnpoint = LevelAnimals.spawns[(int)num];
					if (animalSpawnpoint.type == EditorSpawns.selectedAnimal)
					{
						animalSpawnpoint.node.GetComponent<Renderer>().material.color = this.color;
					}
					num += 1;
				}
				EditorSpawns.animalSpawn.GetComponent<Renderer>().material.color = this.color;
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000C5740 File Offset: 0x000C3B40
		public void addTier(string name)
		{
			if (this.tiers.Count == 255)
			{
				return;
			}
			for (int i = 0; i < this.tiers.Count; i++)
			{
				if (this.tiers[i].name == name)
				{
					return;
				}
			}
			if (this.tiers.Count == 0)
			{
				this.tiers.Add(new AnimalTier(new List<AnimalSpawn>(), name, 1f));
			}
			else
			{
				this.tiers.Add(new AnimalTier(new List<AnimalSpawn>(), name, 0f));
			}
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000C57E7 File Offset: 0x000C3BE7
		public void removeTier(int tierIndex)
		{
			this.updateChance(tierIndex, 0f);
			this.tiers.RemoveAt(tierIndex);
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000C5801 File Offset: 0x000C3C01
		public void addAnimal(byte tierIndex, ushort id)
		{
			this.tiers[(int)tierIndex].addAnimal(id);
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000C5815 File Offset: 0x000C3C15
		public void removeAnimal(byte tierIndex, byte animalIndex)
		{
			this.tiers[(int)tierIndex].removeAnimal(animalIndex);
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000C582C File Offset: 0x000C3C2C
		public ushort getAnimal()
		{
			if (this.tableID != 0)
			{
				return SpawnTableTool.resolve(this.tableID);
			}
			float value = UnityEngine.Random.value;
			if (this.tiers.Count == 0)
			{
				return 0;
			}
			int i = 0;
			while (i < this.tiers.Count)
			{
				if (value < this.tiers[i].chance)
				{
					AnimalTier animalTier = this.tiers[i];
					if (animalTier.table.Count > 0)
					{
						return animalTier.table[UnityEngine.Random.Range(0, animalTier.table.Count)].animal;
					}
					return 0;
				}
				else
				{
					i++;
				}
			}
			AnimalTier animalTier2 = this.tiers[UnityEngine.Random.Range(0, this.tiers.Count)];
			if (animalTier2.table.Count > 0)
			{
				return animalTier2.table[UnityEngine.Random.Range(0, animalTier2.table.Count)].animal;
			}
			return 0;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000C5930 File Offset: 0x000C3D30
		public void buildTable()
		{
			List<AnimalTier> list = new List<AnimalTier>();
			for (int i = 0; i < this.tiers.Count; i++)
			{
				if (list.Count == 0)
				{
					list.Add(this.tiers[i]);
				}
				else
				{
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						if (this.tiers[i].chance < list[j].chance)
						{
							flag = true;
							list.Insert(j, this.tiers[i]);
							break;
						}
					}
					if (!flag)
					{
						list.Add(this.tiers[i]);
					}
				}
			}
			float num = 0f;
			for (int k = 0; k < list.Count; k++)
			{
				num += list[k].chance;
				list[k].chance = num;
			}
			this._tiers = list;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000C5A38 File Offset: 0x000C3E38
		public void updateChance(int tierIndex, float chance)
		{
			float num = chance - this.tiers[tierIndex].chance;
			this.tiers[tierIndex].chance = chance;
			float num2 = Mathf.Abs(num);
			while (num2 > 0.001f)
			{
				int num3 = 0;
				for (int i = 0; i < this.tiers.Count; i++)
				{
					if (((num < 0f && this.tiers[i].chance < 1f) || (num > 0f && this.tiers[i].chance > 0f)) && i != tierIndex)
					{
						num3++;
					}
				}
				if (num3 == 0)
				{
					break;
				}
				float num4 = num2 / (float)num3;
				for (int j = 0; j < this.tiers.Count; j++)
				{
					if (((num < 0f && this.tiers[j].chance < 1f) || (num > 0f && this.tiers[j].chance > 0f)) && j != tierIndex)
					{
						if (num > 0f)
						{
							if (this.tiers[j].chance >= num4)
							{
								num2 -= num4;
								this.tiers[j].chance -= num4;
							}
							else
							{
								num2 -= this.tiers[j].chance;
								this.tiers[j].chance = 0f;
							}
						}
						else if (this.tiers[j].chance <= 1f - num4)
						{
							num2 -= num4;
							this.tiers[j].chance += num4;
						}
						else
						{
							num2 -= 1f - this.tiers[j].chance;
							this.tiers[j].chance = 1f;
						}
					}
				}
			}
			float num5 = 0f;
			for (int k = 0; k < this.tiers.Count; k++)
			{
				num5 += this.tiers[k].chance;
			}
			for (int l = 0; l < this.tiers.Count; l++)
			{
				this.tiers[l].chance /= num5;
			}
		}

		// Token: 0x04001583 RID: 5507
		private List<AnimalTier> _tiers;

		// Token: 0x04001584 RID: 5508
		private Color _color;

		// Token: 0x04001585 RID: 5509
		public string name;

		// Token: 0x04001586 RID: 5510
		public ushort tableID;
	}
}
