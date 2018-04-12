using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000559 RID: 1369
	public class LevelPlayers
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x000DC9E5 File Offset: 0x000DADE5
		public static Transform models
		{
			get
			{
				return LevelPlayers._models;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000DC9EC File Offset: 0x000DADEC
		public static List<PlayerSpawnpoint> spawns
		{
			get
			{
				return LevelPlayers._spawns;
			}
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x000DC9F4 File Offset: 0x000DADF4
		public static void setEnabled(bool isEnabled)
		{
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				LevelPlayers.spawns[i].setEnabled(isEnabled);
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000DCA30 File Offset: 0x000DAE30
		public static bool checkCanBuild(Vector3 point)
		{
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				PlayerSpawnpoint playerSpawnpoint = LevelPlayers.spawns[i];
				if ((playerSpawnpoint.point - point).sqrMagnitude < 256f)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000DCA85 File Offset: 0x000DAE85
		public static void addSpawn(Vector3 point, float angle, bool isAlt)
		{
			LevelPlayers.spawns.Add(new PlayerSpawnpoint(point, angle, isAlt));
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x000DCA9C File Offset: 0x000DAE9C
		public static void removeSpawn(Vector3 point, float radius)
		{
			radius *= radius;
			List<PlayerSpawnpoint> list = new List<PlayerSpawnpoint>();
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				PlayerSpawnpoint playerSpawnpoint = LevelPlayers.spawns[i];
				if ((playerSpawnpoint.point - point).sqrMagnitude < radius)
				{
					UnityEngine.Object.Destroy(playerSpawnpoint.node.gameObject);
				}
				else
				{
					list.Add(playerSpawnpoint);
				}
			}
			LevelPlayers._spawns = list;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x000DCB18 File Offset: 0x000DAF18
		public static List<PlayerSpawnpoint> getRegSpawns()
		{
			List<PlayerSpawnpoint> list = new List<PlayerSpawnpoint>();
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				PlayerSpawnpoint playerSpawnpoint = LevelPlayers.spawns[i];
				if (!playerSpawnpoint.isAlt)
				{
					list.Add(playerSpawnpoint);
				}
			}
			return list;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x000DCB68 File Offset: 0x000DAF68
		public static List<PlayerSpawnpoint> getAltSpawns()
		{
			List<PlayerSpawnpoint> list = new List<PlayerSpawnpoint>();
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				PlayerSpawnpoint playerSpawnpoint = LevelPlayers.spawns[i];
				if (playerSpawnpoint.isAlt)
				{
					list.Add(playerSpawnpoint);
				}
			}
			return list;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000DCBB8 File Offset: 0x000DAFB8
		public static PlayerSpawnpoint getSpawn(bool isAlt)
		{
			List<PlayerSpawnpoint> list = (!isAlt) ? LevelPlayers.getRegSpawns() : LevelPlayers.getAltSpawns();
			if (list.Count == 0)
			{
				return new PlayerSpawnpoint(new Vector3(0f, 256f, 0f), 0f, isAlt);
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000DCC18 File Offset: 0x000DB018
		public static void load()
		{
			LevelPlayers._models = new GameObject().transform;
			LevelPlayers.models.name = "Players";
			LevelPlayers.models.parent = Level.spawns;
			LevelPlayers.models.tag = "Logic";
			LevelPlayers.models.gameObject.layer = LayerMasks.LOGIC;
			LevelPlayers._spawns = new List<PlayerSpawnpoint>();
			if (ReadWrite.fileExists(Level.info.path + "/Spawns/Players.dat", false, false))
			{
				River river = new River(Level.info.path + "/Spawns/Players.dat", false);
				byte b = river.readByte();
				if (b > 1 && b < 3)
				{
					river.readSteamID();
				}
				int num = 0;
				int num2 = 0;
				byte b2 = river.readByte();
				for (int i = 0; i < (int)b2; i++)
				{
					Vector3 point = river.readSingleVector3();
					float angle = (float)(river.readByte() * 2);
					bool flag = false;
					if (b > 3)
					{
						flag = river.readBoolean();
					}
					if (flag)
					{
						num2++;
					}
					else
					{
						num++;
					}
					LevelPlayers.addSpawn(point, angle, flag);
				}
				river.closeRiver();
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000DCD44 File Offset: 0x000DB144
		public static void save()
		{
			River river = new River(Level.info.path + "/Spawns/Players.dat", false);
			river.writeByte(LevelPlayers.SAVEDATA_VERSION);
			river.writeByte((byte)LevelPlayers.spawns.Count);
			for (int i = 0; i < LevelPlayers.spawns.Count; i++)
			{
				PlayerSpawnpoint playerSpawnpoint = LevelPlayers.spawns[i];
				river.writeSingleVector3(playerSpawnpoint.point);
				river.writeByte(MeasurementTool.angleToByte(playerSpawnpoint.angle));
				river.writeBoolean(playerSpawnpoint.isAlt);
			}
			river.closeRiver();
		}

		// Token: 0x04001788 RID: 6024
		public static readonly byte SAVEDATA_VERSION = 4;

		// Token: 0x04001789 RID: 6025
		private static Transform _models;

		// Token: 0x0400178A RID: 6026
		private static List<PlayerSpawnpoint> _spawns;
	}
}
