using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200055C RID: 1372
	public class LevelVehicles
	{
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000DDBA9 File Offset: 0x000DBFA9
		public static Transform models
		{
			get
			{
				return LevelVehicles._models;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x000DDBB0 File Offset: 0x000DBFB0
		public static List<VehicleTable> tables
		{
			get
			{
				return LevelVehicles._tables;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x000DDBB7 File Offset: 0x000DBFB7
		public static List<VehicleSpawnpoint> spawns
		{
			get
			{
				return LevelVehicles._spawns;
			}
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000DDBC0 File Offset: 0x000DBFC0
		public static void setEnabled(bool isEnabled)
		{
			if (LevelVehicles.spawns == null)
			{
				return;
			}
			for (int i = 0; i < LevelVehicles.spawns.Count; i++)
			{
				LevelVehicles.spawns[i].setEnabled(isEnabled);
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000DDC04 File Offset: 0x000DC004
		public static void addTable(string name)
		{
			if (LevelVehicles.tables.Count == 255)
			{
				return;
			}
			LevelVehicles.tables.Add(new VehicleTable(name));
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000DDC2C File Offset: 0x000DC02C
		public static void removeTable()
		{
			LevelVehicles.tables.RemoveAt((int)EditorSpawns.selectedVehicle);
			List<VehicleSpawnpoint> list = new List<VehicleSpawnpoint>();
			for (int i = 0; i < LevelVehicles.spawns.Count; i++)
			{
				VehicleSpawnpoint vehicleSpawnpoint = LevelVehicles.spawns[i];
				if (vehicleSpawnpoint.type == EditorSpawns.selectedVehicle)
				{
					UnityEngine.Object.Destroy(vehicleSpawnpoint.node.gameObject);
				}
				else
				{
					if (vehicleSpawnpoint.type > EditorSpawns.selectedVehicle)
					{
						VehicleSpawnpoint vehicleSpawnpoint2 = vehicleSpawnpoint;
						vehicleSpawnpoint2.type -= 1;
					}
					list.Add(vehicleSpawnpoint);
				}
			}
			LevelVehicles._spawns = list;
			EditorSpawns.selectedVehicle = 0;
			if ((int)EditorSpawns.selectedVehicle < LevelVehicles.tables.Count)
			{
				EditorSpawns.vehicleSpawn.GetComponent<Renderer>().material.color = LevelVehicles.tables[(int)EditorSpawns.selectedVehicle].color;
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000DDD08 File Offset: 0x000DC108
		public static void addSpawn(Vector3 point, float angle)
		{
			if ((int)EditorSpawns.selectedVehicle >= LevelVehicles.tables.Count)
			{
				return;
			}
			LevelVehicles.spawns.Add(new VehicleSpawnpoint(EditorSpawns.selectedVehicle, point, angle));
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000DDD38 File Offset: 0x000DC138
		public static void removeSpawn(Vector3 point, float radius)
		{
			radius *= radius;
			List<VehicleSpawnpoint> list = new List<VehicleSpawnpoint>();
			for (int i = 0; i < LevelVehicles.spawns.Count; i++)
			{
				VehicleSpawnpoint vehicleSpawnpoint = LevelVehicles.spawns[i];
				if ((vehicleSpawnpoint.point - point).sqrMagnitude < radius)
				{
					UnityEngine.Object.Destroy(vehicleSpawnpoint.node.gameObject);
				}
				else
				{
					list.Add(vehicleSpawnpoint);
				}
			}
			LevelVehicles._spawns = list;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000DDDB3 File Offset: 0x000DC1B3
		public static ushort getVehicle(VehicleSpawnpoint spawn)
		{
			return LevelVehicles.getVehicle(spawn.type);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000DDDC0 File Offset: 0x000DC1C0
		public static ushort getVehicle(byte type)
		{
			return LevelVehicles.tables[(int)type].getVehicle();
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000DDDD4 File Offset: 0x000DC1D4
		public static void load()
		{
			LevelVehicles._models = new GameObject().transform;
			LevelVehicles.models.name = "Vehicles";
			LevelVehicles.models.parent = Level.spawns;
			LevelVehicles.models.tag = "Logic";
			LevelVehicles.models.gameObject.layer = LayerMasks.LOGIC;
			if (Level.isEditor || Provider.isServer)
			{
				LevelVehicles._tables = new List<VehicleTable>();
				LevelVehicles._spawns = new List<VehicleSpawnpoint>();
				if (ReadWrite.fileExists(Level.info.path + "/Spawns/Vehicles.dat", false, false))
				{
					River river = new River(Level.info.path + "/Spawns/Vehicles.dat", false);
					byte b = river.readByte();
					if (b > 1 && b < 3)
					{
						river.readSteamID();
					}
					byte b2 = river.readByte();
					for (byte b3 = 0; b3 < b2; b3 += 1)
					{
						Color newColor = river.readColor();
						string text = river.readString();
						ushort num;
						if (b > 3)
						{
							num = river.readUInt16();
							if (num != 0 && SpawnTableTool.resolve(num) == 0)
							{
								Assets.errors.Add(string.Concat(new object[]
								{
									Level.info.name,
									" vehicle table \"",
									text,
									"\" references invalid spawn table ",
									num,
									"!"
								}));
							}
						}
						else
						{
							num = 0;
						}
						List<VehicleTier> list = new List<VehicleTier>();
						byte b4 = river.readByte();
						for (byte b5 = 0; b5 < b4; b5 += 1)
						{
							string newName = river.readString();
							float newChance = river.readSingle();
							List<VehicleSpawn> list2 = new List<VehicleSpawn>();
							byte b6 = river.readByte();
							for (byte b7 = 0; b7 < b6; b7 += 1)
							{
								ushort newVehicle = river.readUInt16();
								list2.Add(new VehicleSpawn(newVehicle));
							}
							list.Add(new VehicleTier(list2, newName, newChance));
						}
						LevelVehicles.tables.Add(new VehicleTable(list, newColor, text, num));
						if (!Level.isEditor)
						{
							LevelVehicles.tables[(int)b3].buildTable();
						}
					}
					ushort num2 = river.readUInt16();
					for (int i = 0; i < (int)num2; i++)
					{
						byte newType = river.readByte();
						Vector3 newPoint = river.readSingleVector3();
						float newAngle = (float)(river.readByte() * 2);
						LevelVehicles.spawns.Add(new VehicleSpawnpoint(newType, newPoint, newAngle));
					}
					river.closeRiver();
				}
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000DE060 File Offset: 0x000DC460
		public static void save()
		{
			River river = new River(Level.info.path + "/Spawns/Vehicles.dat", false);
			river.writeByte(LevelVehicles.SAVEDATA_VERSION);
			river.writeByte((byte)LevelVehicles.tables.Count);
			byte b = 0;
			while ((int)b < LevelVehicles.tables.Count)
			{
				VehicleTable vehicleTable = LevelVehicles.tables[(int)b];
				river.writeColor(vehicleTable.color);
				river.writeString(vehicleTable.name);
				river.writeUInt16(vehicleTable.tableID);
				river.writeByte((byte)vehicleTable.tiers.Count);
				byte b2 = 0;
				while ((int)b2 < vehicleTable.tiers.Count)
				{
					VehicleTier vehicleTier = vehicleTable.tiers[(int)b2];
					river.writeString(vehicleTier.name);
					river.writeSingle(vehicleTier.chance);
					river.writeByte((byte)vehicleTier.table.Count);
					byte b3 = 0;
					while ((int)b3 < vehicleTier.table.Count)
					{
						VehicleSpawn vehicleSpawn = vehicleTier.table[(int)b3];
						river.writeUInt16(vehicleSpawn.vehicle);
						b3 += 1;
					}
					b2 += 1;
				}
				b += 1;
			}
			river.writeUInt16((ushort)LevelVehicles.spawns.Count);
			for (int i = 0; i < LevelVehicles.spawns.Count; i++)
			{
				VehicleSpawnpoint vehicleSpawnpoint = LevelVehicles.spawns[i];
				river.writeByte(vehicleSpawnpoint.type);
				river.writeSingleVector3(vehicleSpawnpoint.point);
				river.writeByte(MeasurementTool.angleToByte(vehicleSpawnpoint.angle));
			}
			river.closeRiver();
		}

		// Token: 0x04001793 RID: 6035
		public static readonly byte SAVEDATA_VERSION = 4;

		// Token: 0x04001794 RID: 6036
		private static Transform _models;

		// Token: 0x04001795 RID: 6037
		private static List<VehicleTable> _tables;

		// Token: 0x04001796 RID: 6038
		private static List<VehicleSpawnpoint> _spawns;
	}
}
