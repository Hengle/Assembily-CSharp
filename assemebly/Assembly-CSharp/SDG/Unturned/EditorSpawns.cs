using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200049D RID: 1181
	public class EditorSpawns : MonoBehaviour
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000ABC3A File Offset: 0x000AA03A
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x000ABC44 File Offset: 0x000AA044
		public static bool isSpawning
		{
			get
			{
				return EditorSpawns._isSpawning;
			}
			set
			{
				EditorSpawns._isSpawning = value;
				if (!EditorSpawns.isSpawning)
				{
					EditorSpawns.itemSpawn.gameObject.SetActive(false);
					EditorSpawns.playerSpawn.gameObject.SetActive(false);
					EditorSpawns.playerSpawnAlt.gameObject.SetActive(false);
					EditorSpawns.zombieSpawn.gameObject.SetActive(false);
					EditorSpawns.vehicleSpawn.gameObject.SetActive(false);
					EditorSpawns.animalSpawn.gameObject.SetActive(false);
					EditorSpawns.remove.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x000ABCD1 File Offset: 0x000AA0D1
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x000ABCD8 File Offset: 0x000AA0D8
		public static bool selectedAlt
		{
			get
			{
				return EditorSpawns._selectedAlt;
			}
			set
			{
				EditorSpawns._selectedAlt = value;
				EditorSpawns.playerSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER && EditorSpawns.isSpawning && !EditorSpawns.selectedAlt);
				EditorSpawns.playerSpawnAlt.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER && EditorSpawns.isSpawning && EditorSpawns.selectedAlt);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000ABD46 File Offset: 0x000AA146
		public static Transform itemSpawn
		{
			get
			{
				return EditorSpawns._itemSpawn;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000ABD4D File Offset: 0x000AA14D
		public static Transform playerSpawn
		{
			get
			{
				return EditorSpawns._playerSpawn;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000ABD54 File Offset: 0x000AA154
		public static Transform playerSpawnAlt
		{
			get
			{
				return EditorSpawns._playerSpawnAlt;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000ABD5B File Offset: 0x000AA15B
		public static Transform zombieSpawn
		{
			get
			{
				return EditorSpawns._zombieSpawn;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000ABD62 File Offset: 0x000AA162
		public static Transform vehicleSpawn
		{
			get
			{
				return EditorSpawns._vehicleSpawn;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000ABD69 File Offset: 0x000AA169
		public static Transform animalSpawn
		{
			get
			{
				return EditorSpawns._animalSpawn;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000ABD70 File Offset: 0x000AA170
		public static Transform remove
		{
			get
			{
				return EditorSpawns._remove;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000ABD77 File Offset: 0x000AA177
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x000ABD80 File Offset: 0x000AA180
		public static float rotation
		{
			get
			{
				return EditorSpawns._rotation;
			}
			set
			{
				EditorSpawns._rotation = value;
				if (EditorSpawns.playerSpawn != null)
				{
					EditorSpawns.playerSpawn.transform.rotation = Quaternion.Euler(0f, EditorSpawns.rotation, 0f);
				}
				if (EditorSpawns.playerSpawnAlt != null)
				{
					EditorSpawns.playerSpawnAlt.transform.rotation = Quaternion.Euler(0f, EditorSpawns.rotation, 0f);
				}
				if (EditorSpawns.vehicleSpawn != null)
				{
					EditorSpawns.vehicleSpawn.transform.rotation = Quaternion.Euler(0f, EditorSpawns.rotation, 0f);
				}
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x000ABE2C File Offset: 0x000AA22C
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x000ABE33 File Offset: 0x000AA233
		public static byte radius
		{
			get
			{
				return EditorSpawns._radius;
			}
			set
			{
				EditorSpawns._radius = value;
				if (EditorSpawns.remove != null)
				{
					EditorSpawns.remove.localScale = new Vector3((float)(EditorSpawns.radius * 2), (float)(EditorSpawns.radius * 2), (float)(EditorSpawns.radius * 2));
				}
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x000ABE72 File Offset: 0x000AA272
		// (set) Token: 0x06001F1C RID: 7964 RVA: 0x000ABE7C File Offset: 0x000AA27C
		public static ESpawnMode spawnMode
		{
			get
			{
				return EditorSpawns._spawnMode;
			}
			set
			{
				EditorSpawns._spawnMode = value;
				EditorSpawns.itemSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_ITEM && EditorSpawns.isSpawning);
				EditorSpawns.playerSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER && EditorSpawns.isSpawning && !EditorSpawns.selectedAlt);
				EditorSpawns.playerSpawnAlt.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER && EditorSpawns.isSpawning && EditorSpawns.selectedAlt);
				EditorSpawns.zombieSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_ZOMBIE && EditorSpawns.isSpawning);
				EditorSpawns.vehicleSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_VEHICLE && EditorSpawns.isSpawning);
				EditorSpawns.animalSpawn.gameObject.SetActive(EditorSpawns.spawnMode == ESpawnMode.ADD_ANIMAL && EditorSpawns.isSpawning);
				EditorSpawns.remove.gameObject.SetActive((EditorSpawns.spawnMode == ESpawnMode.REMOVE_RESOURCE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ITEM || EditorSpawns.spawnMode == ESpawnMode.REMOVE_PLAYER || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ZOMBIE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_VEHICLE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ANIMAL) && EditorSpawns.isSpawning);
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000ABFD0 File Offset: 0x000AA3D0
		private void Update()
		{
			if (!EditorSpawns.isSpawning)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (Input.GetKeyDown(ControlsSettings.tool_0))
				{
					if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_RESOURCE)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_RESOURCE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ITEM)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_ITEM;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_PLAYER)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_PLAYER;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ZOMBIE)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_ZOMBIE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_VEHICLE)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_VEHICLE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ANIMAL)
					{
						EditorSpawns.spawnMode = ESpawnMode.ADD_ANIMAL;
					}
				}
				if (Input.GetKeyDown(ControlsSettings.tool_1))
				{
					if (EditorSpawns.spawnMode == ESpawnMode.ADD_RESOURCE)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_RESOURCE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ITEM)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_ITEM;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_PLAYER;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ZOMBIE)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_ZOMBIE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_VEHICLE)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_VEHICLE;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ANIMAL)
					{
						EditorSpawns.spawnMode = ESpawnMode.REMOVE_ANIMAL;
					}
				}
				if (EditorInteract.worldHit.transform != null)
				{
					if (EditorSpawns.spawnMode == ESpawnMode.ADD_ITEM)
					{
						EditorSpawns.itemSpawn.position = EditorInteract.worldHit.point;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER)
					{
						EditorSpawns.playerSpawn.position = EditorInteract.worldHit.point;
						EditorSpawns.playerSpawnAlt.position = EditorInteract.worldHit.point;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ZOMBIE)
					{
						EditorSpawns.zombieSpawn.position = EditorInteract.worldHit.point + Vector3.up;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_VEHICLE)
					{
						EditorSpawns.vehicleSpawn.position = EditorInteract.worldHit.point;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ANIMAL)
					{
						EditorSpawns.animalSpawn.position = EditorInteract.worldHit.point;
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_RESOURCE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ITEM || EditorSpawns.spawnMode == ESpawnMode.REMOVE_PLAYER || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ZOMBIE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_VEHICLE || EditorSpawns.spawnMode == ESpawnMode.REMOVE_ANIMAL)
					{
						EditorSpawns.remove.position = EditorInteract.worldHit.point;
					}
				}
				if (Input.GetKeyDown(ControlsSettings.primary) && EditorInteract.worldHit.transform != null)
				{
					Vector3 point = EditorInteract.worldHit.point;
					if (EditorSpawns.spawnMode == ESpawnMode.ADD_RESOURCE)
					{
						if ((int)EditorSpawns.selectedResource < LevelGround.resources.Length)
						{
							LevelGround.addSpawn(point, EditorSpawns.selectedResource, false);
						}
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_RESOURCE)
					{
						LevelGround.removeSpawn(point, (float)EditorSpawns.radius);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ITEM)
					{
						if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
						{
							LevelItems.addSpawn(point);
						}
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ITEM)
					{
						LevelItems.removeSpawn(point, (float)EditorSpawns.radius);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_PLAYER)
					{
						LevelPlayers.addSpawn(point, EditorSpawns.rotation, EditorSpawns.selectedAlt);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_PLAYER)
					{
						LevelPlayers.removeSpawn(point, (float)EditorSpawns.radius);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ZOMBIE)
					{
						if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
						{
							LevelZombies.addSpawn(point);
						}
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ZOMBIE)
					{
						LevelZombies.removeSpawn(point, (float)EditorSpawns.radius);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_VEHICLE)
					{
						LevelVehicles.addSpawn(point, EditorSpawns.rotation);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_VEHICLE)
					{
						LevelVehicles.removeSpawn(point, (float)EditorSpawns.radius);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.ADD_ANIMAL)
					{
						LevelAnimals.addSpawn(point);
					}
					else if (EditorSpawns.spawnMode == ESpawnMode.REMOVE_ANIMAL)
					{
						LevelAnimals.removeSpawn(point, (float)EditorSpawns.radius);
					}
				}
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000AC434 File Offset: 0x000AA834
		private void Start()
		{
			EditorSpawns._isSpawning = false;
			EditorSpawns._itemSpawn = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Item"))).transform;
			EditorSpawns.itemSpawn.name = "Item Spawn";
			EditorSpawns.itemSpawn.parent = Level.editing;
			EditorSpawns.itemSpawn.gameObject.SetActive(false);
			if ((int)EditorSpawns.selectedItem < LevelItems.tables.Count)
			{
				EditorSpawns.itemSpawn.GetComponent<Renderer>().material.color = LevelItems.tables[(int)EditorSpawns.selectedItem].color;
			}
			EditorSpawns._playerSpawn = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Player"))).transform;
			EditorSpawns.playerSpawn.name = "Player Spawn";
			EditorSpawns.playerSpawn.parent = Level.editing;
			EditorSpawns.playerSpawn.gameObject.SetActive(false);
			EditorSpawns._playerSpawnAlt = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Player_Alt"))).transform;
			EditorSpawns.playerSpawnAlt.name = "Player Spawn Alt";
			EditorSpawns.playerSpawnAlt.parent = Level.editing;
			EditorSpawns.playerSpawnAlt.gameObject.SetActive(false);
			EditorSpawns._zombieSpawn = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Zombie"))).transform;
			EditorSpawns.zombieSpawn.name = "Zombie Spawn";
			EditorSpawns.zombieSpawn.parent = Level.editing;
			EditorSpawns.zombieSpawn.gameObject.SetActive(false);
			if ((int)EditorSpawns.selectedZombie < LevelZombies.tables.Count)
			{
				EditorSpawns.zombieSpawn.GetComponent<Renderer>().material.color = LevelZombies.tables[(int)EditorSpawns.selectedZombie].color;
			}
			EditorSpawns._vehicleSpawn = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Vehicle"))).transform;
			EditorSpawns.vehicleSpawn.name = "Vehicle Spawn";
			EditorSpawns.vehicleSpawn.parent = Level.editing;
			EditorSpawns.vehicleSpawn.gameObject.SetActive(false);
			if ((int)EditorSpawns.selectedVehicle < LevelVehicles.tables.Count)
			{
				EditorSpawns.vehicleSpawn.GetComponent<Renderer>().material.color = LevelVehicles.tables[(int)EditorSpawns.selectedVehicle].color;
				EditorSpawns.vehicleSpawn.FindChild("Arrow").GetComponent<Renderer>().material.color = LevelVehicles.tables[(int)EditorSpawns.selectedVehicle].color;
			}
			EditorSpawns._animalSpawn = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Animal"))).transform;
			EditorSpawns._animalSpawn.name = "Animal Spawn";
			EditorSpawns._animalSpawn.parent = Level.editing;
			EditorSpawns._animalSpawn.gameObject.SetActive(false);
			if ((int)EditorSpawns.selectedAnimal < LevelAnimals.tables.Count)
			{
				EditorSpawns.animalSpawn.GetComponent<Renderer>().material.color = LevelAnimals.tables[(int)EditorSpawns.selectedAnimal].color;
			}
			EditorSpawns._remove = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Remove"))).transform;
			EditorSpawns.remove.name = "Remove";
			EditorSpawns.remove.parent = Level.editing;
			EditorSpawns.remove.gameObject.SetActive(false);
			EditorSpawns.spawnMode = ESpawnMode.ADD_ITEM;
			EditorSpawns.load();
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000AC788 File Offset: 0x000AAB88
		public static void load()
		{
			if (ReadWrite.fileExists(Level.info.path + "/Editor/Spawns.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Editor/Spawns.dat", false, false, 1);
				EditorSpawns.rotation = block.readSingle();
				EditorSpawns.radius = block.readByte();
			}
			else
			{
				EditorSpawns.rotation = 0f;
				EditorSpawns.radius = EditorSpawns.MIN_REMOVE_SIZE;
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000AC804 File Offset: 0x000AAC04
		public static void save()
		{
			Block block = new Block();
			block.writeByte(EditorSpawns.SAVEDATA_VERSION);
			block.writeSingle(EditorSpawns.rotation);
			block.writeByte(EditorSpawns.radius);
			ReadWrite.writeBlock(Level.info.path + "/Editor/Spawns.dat", false, false, block);
		}

		// Token: 0x040012BB RID: 4795
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x040012BC RID: 4796
		public static readonly byte MIN_REMOVE_SIZE = 2;

		// Token: 0x040012BD RID: 4797
		public static readonly byte MAX_REMOVE_SIZE = 30;

		// Token: 0x040012BE RID: 4798
		private static bool _isSpawning;

		// Token: 0x040012BF RID: 4799
		public static byte selectedResource;

		// Token: 0x040012C0 RID: 4800
		public static byte selectedItem;

		// Token: 0x040012C1 RID: 4801
		public static byte selectedZombie;

		// Token: 0x040012C2 RID: 4802
		public static byte selectedVehicle;

		// Token: 0x040012C3 RID: 4803
		public static byte selectedAnimal;

		// Token: 0x040012C4 RID: 4804
		private static bool _selectedAlt;

		// Token: 0x040012C5 RID: 4805
		private static Transform _itemSpawn;

		// Token: 0x040012C6 RID: 4806
		private static Transform _playerSpawn;

		// Token: 0x040012C7 RID: 4807
		private static Transform _playerSpawnAlt;

		// Token: 0x040012C8 RID: 4808
		private static Transform _zombieSpawn;

		// Token: 0x040012C9 RID: 4809
		private static Transform _vehicleSpawn;

		// Token: 0x040012CA RID: 4810
		private static Transform _animalSpawn;

		// Token: 0x040012CB RID: 4811
		private static Transform _remove;

		// Token: 0x040012CC RID: 4812
		private static float _rotation;

		// Token: 0x040012CD RID: 4813
		private static byte _radius;

		// Token: 0x040012CE RID: 4814
		private static ESpawnMode _spawnMode;
	}
}
