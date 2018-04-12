using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000575 RID: 1397
	public class VehicleTable
	{
		// Token: 0x0600268C RID: 9868 RVA: 0x000E42C8 File Offset: 0x000E26C8
		public VehicleTable(string newName)
		{
			this._tiers = new List<VehicleTier>();
			this._color = Color.white;
			this.name = newName;
			this.tableID = 0;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000E42F4 File Offset: 0x000E26F4
		public VehicleTable(List<VehicleTier> newTiers, Color newColor, string newName, ushort newTableID)
		{
			this._tiers = newTiers;
			this._color = newColor;
			this.name = newName;
			this.tableID = newTableID;
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x000E4319 File Offset: 0x000E2719
		public List<VehicleTier> tiers
		{
			get
			{
				return this._tiers;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x000E4321 File Offset: 0x000E2721
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000E432C File Offset: 0x000E272C
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
				while ((int)num < LevelVehicles.spawns.Count)
				{
					VehicleSpawnpoint vehicleSpawnpoint = LevelVehicles.spawns[(int)num];
					if (vehicleSpawnpoint.type == EditorSpawns.selectedVehicle)
					{
						vehicleSpawnpoint.node.GetComponent<Renderer>().material.color = this.color;
						vehicleSpawnpoint.node.FindChild("Arrow").GetComponent<Renderer>().material.color = this.color;
					}
					num += 1;
				}
				EditorSpawns.vehicleSpawn.GetComponent<Renderer>().material.color = this.color;
				EditorSpawns.vehicleSpawn.FindChild("Arrow").GetComponent<Renderer>().material.color = this.color;
			}
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000E43F8 File Offset: 0x000E27F8
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
				this.tiers.Add(new VehicleTier(new List<VehicleSpawn>(), name, 1f));
			}
			else
			{
				this.tiers.Add(new VehicleTier(new List<VehicleSpawn>(), name, 0f));
			}
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000E449F File Offset: 0x000E289F
		public void removeTier(int tierIndex)
		{
			this.updateChance(tierIndex, 0f);
			this.tiers.RemoveAt(tierIndex);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000E44B9 File Offset: 0x000E28B9
		public void addVehicle(byte tierIndex, ushort id)
		{
			this.tiers[(int)tierIndex].addVehicle(id);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000E44CD File Offset: 0x000E28CD
		public void removeVehicle(byte tierIndex, byte vehicleIndex)
		{
			this.tiers[(int)tierIndex].removeVehicle(vehicleIndex);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000E44E4 File Offset: 0x000E28E4
		public ushort getVehicle()
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
					VehicleTier vehicleTier = this.tiers[i];
					if (vehicleTier.table.Count > 0)
					{
						return vehicleTier.table[UnityEngine.Random.Range(0, vehicleTier.table.Count)].vehicle;
					}
					return 0;
				}
				else
				{
					i++;
				}
			}
			VehicleTier vehicleTier2 = this.tiers[UnityEngine.Random.Range(0, this.tiers.Count)];
			if (vehicleTier2.table.Count > 0)
			{
				return vehicleTier2.table[UnityEngine.Random.Range(0, vehicleTier2.table.Count)].vehicle;
			}
			return 0;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000E45E8 File Offset: 0x000E29E8
		public void buildTable()
		{
			List<VehicleTier> list = new List<VehicleTier>();
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

		// Token: 0x06002697 RID: 9879 RVA: 0x000E46F0 File Offset: 0x000E2AF0
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

		// Token: 0x04001822 RID: 6178
		private List<VehicleTier> _tiers;

		// Token: 0x04001823 RID: 6179
		private Color _color;

		// Token: 0x04001824 RID: 6180
		public string name;

		// Token: 0x04001825 RID: 6181
		public ushort tableID;
	}
}
