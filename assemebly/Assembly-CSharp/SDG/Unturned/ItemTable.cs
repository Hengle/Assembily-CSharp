using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200053A RID: 1338
	public class ItemTable
	{
		// Token: 0x060023F5 RID: 9205 RVA: 0x000C78F5 File Offset: 0x000C5CF5
		public ItemTable(string newName)
		{
			this._tiers = new List<ItemTier>();
			this._color = Color.white;
			this.name = newName;
			this.tableID = 0;
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000C7921 File Offset: 0x000C5D21
		public ItemTable(List<ItemTier> newTiers, Color newColor, string newName, ushort newTableID)
		{
			this._tiers = newTiers;
			this._color = newColor;
			this.name = newName;
			this.tableID = newTableID;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000C7946 File Offset: 0x000C5D46
		public List<ItemTier> tiers
		{
			get
			{
				return this._tiers;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060023F8 RID: 9208 RVA: 0x000C794E File Offset: 0x000C5D4E
		// (set) Token: 0x060023F9 RID: 9209 RVA: 0x000C7958 File Offset: 0x000C5D58
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
						while ((int)num < LevelItems.spawns[(int)b, (int)b2].Count)
						{
							ItemSpawnpoint itemSpawnpoint = LevelItems.spawns[(int)b, (int)b2][(int)num];
							if (itemSpawnpoint.type == EditorSpawns.selectedItem)
							{
								itemSpawnpoint.node.GetComponent<Renderer>().material.color = this.color;
							}
							num += 1;
						}
						EditorSpawns.itemSpawn.GetComponent<Renderer>().material.color = this.color;
					}
				}
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000C7A18 File Offset: 0x000C5E18
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
				this.tiers.Add(new ItemTier(new List<ItemSpawn>(), name, 1f));
			}
			else
			{
				this.tiers.Add(new ItemTier(new List<ItemSpawn>(), name, 0f));
			}
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000C7ABF File Offset: 0x000C5EBF
		public void removeTier(int tierIndex)
		{
			this.updateChance(tierIndex, 0f);
			this.tiers.RemoveAt(tierIndex);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000C7AD9 File Offset: 0x000C5ED9
		public void addItem(byte tierIndex, ushort id)
		{
			this.tiers[(int)tierIndex].addItem(id);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000C7AED File Offset: 0x000C5EED
		public void removeItem(byte tierIndex, byte itemIndex)
		{
			this.tiers[(int)tierIndex].removeItem(itemIndex);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000C7B04 File Offset: 0x000C5F04
		public ushort getItem()
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
					ItemTier itemTier = this.tiers[i];
					if (itemTier.table.Count > 0)
					{
						return itemTier.table[UnityEngine.Random.Range(0, itemTier.table.Count)].item;
					}
					return 0;
				}
				else
				{
					i++;
				}
			}
			ItemTier itemTier2 = this.tiers[UnityEngine.Random.Range(0, this.tiers.Count)];
			if (itemTier2.table.Count > 0)
			{
				return itemTier2.table[UnityEngine.Random.Range(0, itemTier2.table.Count)].item;
			}
			return 0;
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000C7C08 File Offset: 0x000C6008
		public void buildTable()
		{
			List<ItemTier> list = new List<ItemTier>();
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

		// Token: 0x06002400 RID: 9216 RVA: 0x000C7D10 File Offset: 0x000C6110
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

		// Token: 0x04001621 RID: 5665
		private List<ItemTier> _tiers;

		// Token: 0x04001622 RID: 5666
		private Color _color;

		// Token: 0x04001623 RID: 5667
		public string name;

		// Token: 0x04001624 RID: 5668
		public ushort tableID;
	}
}
