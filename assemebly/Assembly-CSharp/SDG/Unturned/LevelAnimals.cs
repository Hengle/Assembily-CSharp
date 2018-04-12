using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000544 RID: 1348
	public class LevelAnimals
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x000CB1C4 File Offset: 0x000C95C4
		public static Transform models
		{
			get
			{
				return LevelAnimals._models;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000CB1CB File Offset: 0x000C95CB
		public static List<AnimalTable> tables
		{
			get
			{
				return LevelAnimals._tables;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000CB1D2 File Offset: 0x000C95D2
		public static List<AnimalSpawnpoint> spawns
		{
			get
			{
				return LevelAnimals._spawns;
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000CB1DC File Offset: 0x000C95DC
		public static void setEnabled(bool isEnabled)
		{
			if (LevelAnimals.spawns == null)
			{
				return;
			}
			for (int i = 0; i < LevelAnimals.spawns.Count; i++)
			{
				LevelAnimals.spawns[i].setEnabled(isEnabled);
			}
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000CB220 File Offset: 0x000C9620
		public static void addTable(string name)
		{
			if (LevelAnimals.tables.Count == 255)
			{
				return;
			}
			LevelAnimals.tables.Add(new AnimalTable(name));
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000CB248 File Offset: 0x000C9648
		public static void removeTable()
		{
			LevelAnimals.tables.RemoveAt((int)EditorSpawns.selectedAnimal);
			List<AnimalSpawnpoint> list = new List<AnimalSpawnpoint>();
			for (int i = 0; i < LevelAnimals.spawns.Count; i++)
			{
				AnimalSpawnpoint animalSpawnpoint = LevelAnimals.spawns[i];
				if (animalSpawnpoint.type == EditorSpawns.selectedAnimal)
				{
					UnityEngine.Object.Destroy(animalSpawnpoint.node.gameObject);
				}
				else
				{
					if (animalSpawnpoint.type > EditorSpawns.selectedAnimal)
					{
						AnimalSpawnpoint animalSpawnpoint2 = animalSpawnpoint;
						animalSpawnpoint2.type -= 1;
					}
					list.Add(animalSpawnpoint);
				}
			}
			LevelAnimals._spawns = list;
			EditorSpawns.selectedAnimal = 0;
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				EditorSpawns.animalSpawn.GetComponent<Renderer>().material.color = LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].color;
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000CB324 File Offset: 0x000C9724
		public static void addSpawn(Vector3 point)
		{
			if ((int)EditorSpawns.selectedAnimal >= LevelAnimals.tables.Count)
			{
				return;
			}
			LevelAnimals.spawns.Add(new AnimalSpawnpoint(EditorSpawns.selectedAnimal, point));
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000CB350 File Offset: 0x000C9750
		public static void removeSpawn(Vector3 point, float radius)
		{
			radius *= radius;
			List<AnimalSpawnpoint> list = new List<AnimalSpawnpoint>();
			for (int i = 0; i < LevelAnimals.spawns.Count; i++)
			{
				AnimalSpawnpoint animalSpawnpoint = LevelAnimals.spawns[i];
				if ((animalSpawnpoint.point - point).sqrMagnitude < radius)
				{
					UnityEngine.Object.Destroy(animalSpawnpoint.node.gameObject);
				}
				else
				{
					list.Add(animalSpawnpoint);
				}
			}
			LevelAnimals._spawns = list;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000CB3CB File Offset: 0x000C97CB
		public static ushort getAnimal(AnimalSpawnpoint spawn)
		{
			return LevelAnimals.getAnimal(spawn.type);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000CB3D8 File Offset: 0x000C97D8
		public static ushort getAnimal(byte type)
		{
			return LevelAnimals.tables[(int)type].getAnimal();
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000CB3EC File Offset: 0x000C97EC
		public static void load()
		{
			LevelAnimals._models = new GameObject().transform;
			LevelAnimals.models.name = "Animals";
			LevelAnimals.models.parent = Level.spawns;
			LevelAnimals.models.tag = "Logic";
			LevelAnimals.models.gameObject.layer = LayerMasks.LOGIC;
			if (Level.isEditor || Provider.isServer)
			{
				LevelAnimals._tables = new List<AnimalTable>();
				LevelAnimals._spawns = new List<AnimalSpawnpoint>();
				if (ReadWrite.fileExists(Level.info.path + "/Spawns/Fauna.dat", false, false))
				{
					River river = new River(Level.info.path + "/Spawns/Fauna.dat", false);
					byte b = river.readByte();
					byte b2 = river.readByte();
					for (byte b3 = 0; b3 < b2; b3 += 1)
					{
						Color newColor = river.readColor();
						string text = river.readString();
						ushort num;
						if (b > 2)
						{
							num = river.readUInt16();
							if (num != 0 && SpawnTableTool.resolve(num) == 0)
							{
								Assets.errors.Add(string.Concat(new object[]
								{
									Level.info.name,
									" animal table \"",
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
						List<AnimalTier> list = new List<AnimalTier>();
						byte b4 = river.readByte();
						for (byte b5 = 0; b5 < b4; b5 += 1)
						{
							string newName = river.readString();
							float newChance = river.readSingle();
							List<AnimalSpawn> list2 = new List<AnimalSpawn>();
							byte b6 = river.readByte();
							for (byte b7 = 0; b7 < b6; b7 += 1)
							{
								ushort newAnimal = river.readUInt16();
								list2.Add(new AnimalSpawn(newAnimal));
							}
							list.Add(new AnimalTier(list2, newName, newChance));
						}
						LevelAnimals.tables.Add(new AnimalTable(list, newColor, text, num));
						if (!Level.isEditor)
						{
							LevelAnimals.tables[(int)b3].buildTable();
						}
					}
					ushort num2 = river.readUInt16();
					for (int i = 0; i < (int)num2; i++)
					{
						byte newType = river.readByte();
						Vector3 newPoint = river.readSingleVector3();
						LevelAnimals.spawns.Add(new AnimalSpawnpoint(newType, newPoint));
					}
					river.closeRiver();
				}
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000CB654 File Offset: 0x000C9A54
		public static void save()
		{
			River river = new River(Level.info.path + "/Spawns/Fauna.dat", false);
			river.writeByte(LevelAnimals.SAVEDATA_VERSION);
			river.writeByte((byte)LevelAnimals.tables.Count);
			byte b = 0;
			while ((int)b < LevelAnimals.tables.Count)
			{
				AnimalTable animalTable = LevelAnimals.tables[(int)b];
				river.writeColor(animalTable.color);
				river.writeString(animalTable.name);
				river.writeUInt16(animalTable.tableID);
				river.writeByte((byte)animalTable.tiers.Count);
				byte b2 = 0;
				while ((int)b2 < animalTable.tiers.Count)
				{
					AnimalTier animalTier = animalTable.tiers[(int)b2];
					river.writeString(animalTier.name);
					river.writeSingle(animalTier.chance);
					river.writeByte((byte)animalTier.table.Count);
					byte b3 = 0;
					while ((int)b3 < animalTier.table.Count)
					{
						AnimalSpawn animalSpawn = animalTier.table[(int)b3];
						river.writeUInt16(animalSpawn.animal);
						b3 += 1;
					}
					b2 += 1;
				}
				b += 1;
			}
			river.writeUInt16((ushort)LevelAnimals.spawns.Count);
			for (int i = 0; i < LevelAnimals.spawns.Count; i++)
			{
				AnimalSpawnpoint animalSpawnpoint = LevelAnimals.spawns[i];
				river.writeByte(animalSpawnpoint.type);
				river.writeSingleVector3(animalSpawnpoint.point);
			}
			river.closeRiver();
		}

		// Token: 0x04001654 RID: 5716
		public static readonly byte SAVEDATA_VERSION = 3;

		// Token: 0x04001655 RID: 5717
		private static Transform _models;

		// Token: 0x04001656 RID: 5718
		private static List<AnimalTable> _tables;

		// Token: 0x04001657 RID: 5719
		private static List<AnimalSpawnpoint> _spawns;
	}
}
