using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000554 RID: 1364
	public class LevelNavigation
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x000D7D5E File Offset: 0x000D615E
		public static Transform models
		{
			get
			{
				return LevelNavigation._models;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x000D7D65 File Offset: 0x000D6165
		public static List<Bounds> bounds
		{
			get
			{
				return LevelNavigation._bounds;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x000D7D6C File Offset: 0x000D616C
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x000D7D73 File Offset: 0x000D6173
		public static List<FlagData> flagData { get; private set; }

		// Token: 0x06002548 RID: 9544 RVA: 0x000D7D7C File Offset: 0x000D617C
		public static bool tryGetBounds(Vector3 point, out byte bound)
		{
			bound = byte.MaxValue;
			if (LevelNavigation.bounds != null)
			{
				byte b = 0;
				while ((int)b < LevelNavigation.bounds.Count)
				{
					if (LevelNavigation.bounds[(int)b].Contains(point))
					{
						bound = b;
						return true;
					}
					b += 1;
				}
			}
			return false;
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x000D7DD8 File Offset: 0x000D61D8
		public static bool tryGetNavigation(Vector3 point, out byte nav)
		{
			nav = byte.MaxValue;
			if (AstarPath.active != null)
			{
				byte b = 0;
				while ((int)b < Mathf.Min(LevelNavigation.bounds.Count, AstarPath.active.graphs.Length))
				{
					if (AstarPath.active.graphs[(int)b] != null && ((RecastGraph)AstarPath.active.graphs[(int)b]).forcedBounds.Contains(point))
					{
						nav = b;
						return true;
					}
					b += 1;
				}
			}
			return false;
		}

		// Token: 0x0600254A RID: 9546 RVA: 0x000D7E64 File Offset: 0x000D6264
		public static bool checkSafe(byte bound)
		{
			return LevelNavigation.bounds != null && (int)bound < LevelNavigation.bounds.Count;
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x000D7E80 File Offset: 0x000D6280
		public static bool checkSafe(Vector3 point)
		{
			if (LevelNavigation.bounds == null)
			{
				return false;
			}
			byte b = 0;
			while ((int)b < LevelNavigation.bounds.Count)
			{
				if (LevelNavigation.bounds[(int)b].Contains(point))
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x000D7ED4 File Offset: 0x000D62D4
		public static bool checkSafeFakeNav(Vector3 point)
		{
			if (LevelNavigation.bounds == null)
			{
				return false;
			}
			byte b = 0;
			while ((int)b < LevelNavigation.bounds.Count)
			{
				Bounds bounds = LevelNavigation.bounds[(int)b];
				bounds.size -= LevelNavigation.BOUNDS_SIZE;
				if (bounds.Contains(point))
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000D7F3C File Offset: 0x000D633C
		public static bool checkNavigation(Vector3 point)
		{
			if (AstarPath.active == null)
			{
				return false;
			}
			byte b = 0;
			while ((int)b < AstarPath.active.graphs.Length)
			{
				if (AstarPath.active.graphs[(int)b] != null && ((RecastGraph)AstarPath.active.graphs[(int)b]).forcedBounds.Contains(point))
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000D7FB4 File Offset: 0x000D63B4
		public static void setEnabled(bool isEnabled)
		{
			if (LevelNavigation.flags == null)
			{
				return;
			}
			for (int i = 0; i < LevelNavigation.flags.Count; i++)
			{
				LevelNavigation.flags[i].setEnabled(isEnabled);
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000D7FF8 File Offset: 0x000D63F8
		public static RecastGraph addGraph()
		{
			RecastGraph recastGraph = (RecastGraph)AstarPath.active.astarData.AddGraph(typeof(RecastGraph));
			recastGraph.cellSize = 0.1f;
			recastGraph.cellHeight = 0.1f;
			recastGraph.useTiles = true;
			recastGraph.editorTileSize = 128;
			recastGraph.minRegionSize = 64f;
			recastGraph.walkableHeight = 2f;
			recastGraph.walkableClimb = 0.75f;
			recastGraph.characterRadius = 0.5f;
			recastGraph.maxSlope = 75f;
			recastGraph.maxEdgeLength = 16f;
			recastGraph.contourMaxError = 2f;
			recastGraph.terrainSampleSize = 1;
			recastGraph.rasterizeTrees = false;
			recastGraph.rasterizeMeshes = false;
			recastGraph.rasterizeColliders = true;
			recastGraph.colliderRasterizeDetail = 4f;
			recastGraph.mask = RayMasks.BLOCK_NAVMESH;
			return recastGraph;
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x000D80D4 File Offset: 0x000D64D4
		public static void updateBounds()
		{
			LevelNavigation._bounds = new List<Bounds>();
			for (int i = 0; i < AstarPath.active.graphs.Length; i++)
			{
				RecastGraph recastGraph = (RecastGraph)AstarPath.active.graphs[i];
				if (recastGraph != null)
				{
					RecastGraph.NavmeshTile[] tiles = recastGraph.GetTiles();
					if (tiles != null && tiles.Length > 0)
					{
						LevelNavigation.bounds.Add(new Bounds(recastGraph.forcedBoundsCenter, recastGraph.forcedBoundsSize + LevelNavigation.BOUNDS_SIZE));
					}
				}
			}
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x000D815C File Offset: 0x000D655C
		public static Transform addFlag(Vector3 point)
		{
			RecastGraph newGraph = LevelNavigation.addGraph();
			FlagData flagData = new FlagData(string.Empty, 64, true);
			LevelNavigation.flags.Add(new Flag(point, newGraph, flagData));
			LevelNavigation.flagData.Add(flagData);
			return LevelNavigation.flags[LevelNavigation.flags.Count - 1].model;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000D81B8 File Offset: 0x000D65B8
		public static void removeFlag(Transform select)
		{
			for (int i = 0; i < LevelNavigation.flags.Count; i++)
			{
				if (LevelNavigation.flags[i].model == select)
				{
					for (int j = i + 1; j < LevelNavigation.flags.Count; j++)
					{
						LevelNavigation.flags[j].needsNavigationSave = true;
					}
					try
					{
						LevelNavigation.flags[i].remove();
					}
					catch
					{
					}
					LevelNavigation.flags.RemoveAt(i);
					LevelNavigation.flagData.RemoveAt(i);
					break;
				}
			}
			LevelNavigation.updateBounds();
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000D8274 File Offset: 0x000D6674
		public static Flag getFlag(Transform select)
		{
			for (int i = 0; i < LevelNavigation.flags.Count; i++)
			{
				if (LevelNavigation.flags[i].model == select)
				{
					return LevelNavigation.flags[i];
				}
			}
			return null;
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000D82C4 File Offset: 0x000D66C4
		public static void load()
		{
			LevelNavigation._models = new GameObject().transform;
			LevelNavigation.models.name = "Navigation";
			LevelNavigation.models.parent = Level.level;
			LevelNavigation.models.tag = "Logic";
			LevelNavigation.models.gameObject.layer = LayerMasks.LOGIC;
			LevelNavigation._bounds = new List<Bounds>();
			LevelNavigation.flagData = new List<FlagData>();
			if (ReadWrite.fileExists(Level.info.path + "/Environment/Bounds.dat", false, false))
			{
				River river = new River(Level.info.path + "/Environment/Bounds.dat", false);
				byte b = river.readByte();
				if (b > 0)
				{
					byte b2 = river.readByte();
					for (byte b3 = 0; b3 < b2; b3 += 1)
					{
						Vector3 center = river.readSingleVector3();
						Vector3 size = river.readSingleVector3();
						LevelNavigation.bounds.Add(new Bounds(center, size));
					}
				}
				river.closeRiver();
			}
			if (ReadWrite.fileExists(Level.info.path + "/Environment/Flags_Data.dat", false, false))
			{
				River river2 = new River(Level.info.path + "/Environment/Flags_Data.dat", false);
				byte b4 = river2.readByte();
				if (b4 > 0)
				{
					byte b5 = river2.readByte();
					for (byte b6 = 0; b6 < b5; b6 += 1)
					{
						string newDifficultyGUID = river2.readString();
						byte newMaxZombies = 64;
						if (b4 > 1)
						{
							newMaxZombies = river2.readByte();
						}
						bool newSpawnZombies = true;
						if (b4 > 2)
						{
							newSpawnZombies = river2.readBoolean();
						}
						LevelNavigation.flagData.Add(new FlagData(newDifficultyGUID, newMaxZombies, newSpawnZombies));
					}
				}
				river2.closeRiver();
			}
			if (LevelNavigation.flagData.Count < LevelNavigation.bounds.Count)
			{
				for (int i = LevelNavigation.flagData.Count; i < LevelNavigation.bounds.Count; i++)
				{
					LevelNavigation.flagData.Add(new FlagData(string.Empty, 64, true));
				}
			}
			if (Level.isEditor)
			{
				LevelNavigation.flags = new List<Flag>();
				UnityEngine.Object.Destroy(AstarPath.active.GetComponent<TileHandlerHelpers>());
				if (ReadWrite.fileExists(Level.info.path + "/Environment/Flags.dat", false, false))
				{
					River river3 = new River(Level.info.path + "/Environment/Flags.dat", false);
					byte b7 = river3.readByte();
					if (b7 > 2)
					{
						byte b8 = river3.readByte();
						for (byte b9 = 0; b9 < b8; b9 += 1)
						{
							Vector3 newPoint = river3.readSingleVector3();
							float num = river3.readSingle();
							float num2 = river3.readSingle();
							if (b7 < 4)
							{
								num *= 0.5f;
								num2 *= 0.5f;
							}
							RecastGraph recastGraph = null;
							if (ReadWrite.fileExists(string.Concat(new object[]
							{
								Level.info.path,
								"/Environment/Navigation_",
								b9,
								".dat"
							}), false, false))
							{
								River river4 = new River(string.Concat(new object[]
								{
									Level.info.path,
									"/Environment/Navigation_",
									b9,
									".dat"
								}), false);
								byte b10 = river4.readByte();
								if (b10 > 0)
								{
									recastGraph = LevelNavigation.buildGraph(river4);
								}
								river4.closeRiver();
							}
							if (recastGraph == null)
							{
								recastGraph = LevelNavigation.addGraph();
							}
							LevelNavigation.flags.Add(new Flag(newPoint, num, num2, recastGraph, LevelNavigation.flagData[(int)b9]));
						}
					}
					river3.closeRiver();
				}
			}
			else if (Provider.isServer)
			{
				byte b11 = 0;
				while (ReadWrite.fileExists(string.Concat(new object[]
				{
					Level.info.path,
					"/Environment/Navigation_",
					b11,
					".dat"
				}), false, false))
				{
					River river5 = new River(string.Concat(new object[]
					{
						Level.info.path,
						"/Environment/Navigation_",
						b11,
						".dat"
					}), false);
					byte b12 = river5.readByte();
					if (b12 > 0)
					{
						LevelNavigation.buildGraph(river5);
					}
					river5.closeRiver();
					b11 += 1;
				}
			}
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x000D8734 File Offset: 0x000D6B34
		public static void save()
		{
			River river = new River(Level.info.path + "/Environment/Bounds.dat", false);
			river.writeByte(LevelNavigation.SAVEDATA_BOUNDS_VERSION);
			river.writeByte((byte)LevelNavigation.bounds.Count);
			byte b = 0;
			while ((int)b < LevelNavigation.bounds.Count)
			{
				river.writeSingleVector3(LevelNavigation.bounds[(int)b].center);
				river.writeSingleVector3(LevelNavigation.bounds[(int)b].size);
				b += 1;
			}
			river.closeRiver();
			River river2 = new River(Level.info.path + "/Environment/Flags_Data.dat", false);
			river2.writeByte(LevelNavigation.SAVEDATA_FLAG_DATA_VERSION);
			river2.writeByte((byte)LevelNavigation.flagData.Count);
			byte b2 = 0;
			while ((int)b2 < LevelNavigation.flagData.Count)
			{
				river2.writeString(LevelNavigation.flagData[(int)b2].difficultyGUID);
				river2.writeByte(LevelNavigation.flagData[(int)b2].maxZombies);
				river2.writeBoolean(LevelNavigation.flagData[(int)b2].spawnZombies);
				b2 += 1;
			}
			river2.closeRiver();
			River river3 = new River(Level.info.path + "/Environment/Flags.dat", false);
			river3.writeByte(LevelNavigation.SAVEDATA_FLAGS_VERSION);
			int num = LevelNavigation.flags.Count;
			while (ReadWrite.fileExists(string.Concat(new object[]
			{
				Level.info.path,
				"/Environment/Navigation_",
				num,
				".dat"
			}), false, false))
			{
				ReadWrite.deleteFile(string.Concat(new object[]
				{
					Level.info.path,
					"/Environment/Navigation_",
					num,
					".dat"
				}), false, false);
				num++;
			}
			river3.writeByte((byte)LevelNavigation.flags.Count);
			byte b3 = 0;
			while ((int)b3 < LevelNavigation.flags.Count)
			{
				Flag flag = LevelNavigation.flags[(int)b3];
				river3.writeSingleVector3(flag.point);
				river3.writeSingle(flag.width);
				river3.writeSingle(flag.height);
				if (flag.needsNavigationSave)
				{
					River river4 = new River(string.Concat(new object[]
					{
						Level.info.path,
						"/Environment/Navigation_",
						b3,
						".dat"
					}), false);
					river4.writeByte(LevelNavigation.SAVEDATA_NAVIGATION_VERSION);
					RecastGraph graph = flag.graph;
					river4.writeSingleVector3(graph.forcedBoundsCenter);
					river4.writeSingleVector3(graph.forcedBoundsSize);
					river4.writeByte((byte)graph.tileXCount);
					river4.writeByte((byte)graph.tileZCount);
					RecastGraph.NavmeshTile[] tiles = graph.GetTiles();
					for (int i = 0; i < graph.tileZCount; i++)
					{
						for (int j = 0; j < graph.tileXCount; j++)
						{
							RecastGraph.NavmeshTile navmeshTile = tiles[j + i * graph.tileXCount];
							river4.writeUInt16((ushort)navmeshTile.tris.Length);
							for (int k = 0; k < navmeshTile.tris.Length; k++)
							{
								river4.writeUInt16((ushort)navmeshTile.tris[k]);
							}
							river4.writeUInt16((ushort)navmeshTile.verts.Length);
							for (int l = 0; l < navmeshTile.verts.Length; l++)
							{
								Int3 @int = navmeshTile.verts[l];
								river4.writeInt32(@int.x);
								river4.writeInt32(@int.y);
								river4.writeInt32(@int.z);
							}
						}
					}
					river4.closeRiver();
					flag.needsNavigationSave = false;
				}
				b3 += 1;
			}
			river3.closeRiver();
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000D8B44 File Offset: 0x000D6F44
		private static RecastGraph buildGraph(River river)
		{
			RecastGraph recastGraph = LevelNavigation.addGraph();
			int graphIndex = AstarPath.active.astarData.GetGraphIndex(recastGraph);
			TriangleMeshNode.SetNavmeshHolder(graphIndex, recastGraph);
			recastGraph.forcedBoundsCenter = river.readSingleVector3();
			recastGraph.forcedBoundsSize = river.readSingleVector3();
			recastGraph.tileXCount = (int)river.readByte();
			recastGraph.tileZCount = (int)river.readByte();
			RecastGraph.NavmeshTile[] array = new RecastGraph.NavmeshTile[recastGraph.tileXCount * recastGraph.tileZCount];
			recastGraph.SetTiles(array);
			for (int i = 0; i < recastGraph.tileZCount; i++)
			{
				for (int j = 0; j < recastGraph.tileXCount; j++)
				{
					RecastGraph.NavmeshTile navmeshTile = new RecastGraph.NavmeshTile();
					navmeshTile.x = j;
					navmeshTile.z = i;
					navmeshTile.w = 1;
					navmeshTile.d = 1;
					navmeshTile.bbTree = new BBTree(navmeshTile);
					int num = j + i * recastGraph.tileXCount;
					array[num] = navmeshTile;
					navmeshTile.tris = new int[(int)river.readUInt16()];
					for (int k = 0; k < navmeshTile.tris.Length; k++)
					{
						navmeshTile.tris[k] = (int)river.readUInt16();
					}
					navmeshTile.verts = new Int3[(int)river.readUInt16()];
					for (int l = 0; l < navmeshTile.verts.Length; l++)
					{
						navmeshTile.verts[l] = new Int3(river.readInt32(), river.readInt32(), river.readInt32());
					}
					navmeshTile.nodes = new TriangleMeshNode[navmeshTile.tris.Length / 3];
					num <<= 12;
					for (int m = 0; m < navmeshTile.nodes.Length; m++)
					{
						navmeshTile.nodes[m] = new TriangleMeshNode(AstarPath.active);
						TriangleMeshNode triangleMeshNode = navmeshTile.nodes[m];
						triangleMeshNode.GraphIndex = (uint)graphIndex;
						triangleMeshNode.Penalty = 0u;
						triangleMeshNode.Walkable = true;
						triangleMeshNode.v0 = (navmeshTile.tris[m * 3] | num);
						triangleMeshNode.v1 = (navmeshTile.tris[m * 3 + 1] | num);
						triangleMeshNode.v2 = (navmeshTile.tris[m * 3 + 2] | num);
						triangleMeshNode.UpdatePositionFromVertices();
						navmeshTile.bbTree.Insert(triangleMeshNode);
					}
					recastGraph.CreateNodeConnections(navmeshTile.nodes);
				}
			}
			for (int n = 0; n < recastGraph.tileZCount; n++)
			{
				for (int num2 = 0; num2 < recastGraph.tileXCount; num2++)
				{
					RecastGraph.NavmeshTile tile = array[num2 + n * recastGraph.tileXCount];
					recastGraph.ConnectTileWithNeighbours(tile);
				}
			}
			return recastGraph;
		}

		// Token: 0x04001752 RID: 5970
		public static readonly Vector3 BOUNDS_SIZE = new Vector3(64f, 64f, 64f);

		// Token: 0x04001753 RID: 5971
		public static readonly byte SAVEDATA_BOUNDS_VERSION = 1;

		// Token: 0x04001754 RID: 5972
		public static readonly byte SAVEDATA_FLAGS_VERSION = 4;

		// Token: 0x04001755 RID: 5973
		public static readonly byte SAVEDATA_FLAG_DATA_VERSION = 3;

		// Token: 0x04001756 RID: 5974
		public static readonly byte SAVEDATA_NAVIGATION_VERSION = 1;

		// Token: 0x04001757 RID: 5975
		private static Transform _models;

		// Token: 0x04001758 RID: 5976
		private static List<Flag> flags;

		// Token: 0x04001759 RID: 5977
		private static List<Bounds> _bounds;
	}
}
