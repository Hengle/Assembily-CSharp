using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Pathfinding.Serialization;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using Pathfinding.Voxels;
using SDG.Framework.Landscapes;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000099 RID: 153
	[JsonOptIn]
	[Serializable]
	public class RecastGraph : NavGraph, INavmesh, IRaycastableGraph, IFunnelGraph, IUpdatableGraph, INavmeshHolder
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0002E8A9 File Offset: 0x0002CCA9
		public override void CreateNodes(int number)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0002E8B0 File Offset: 0x0002CCB0
		public Bounds forcedBounds
		{
			get
			{
				return new Bounds(this.forcedBoundsCenter, this.forcedBoundsSize);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0002E8C3 File Offset: 0x0002CCC3
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0002E8CB File Offset: 0x0002CCCB
		public BBTree bbTree
		{
			get
			{
				return this._bbTree;
			}
			set
			{
				this._bbTree = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0002E8D4 File Offset: 0x0002CCD4
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0002E8DC File Offset: 0x0002CCDC
		public Int3[] vertices
		{
			get
			{
				return this._vertices;
			}
			set
			{
				this._vertices = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0002E8E8 File Offset: 0x0002CCE8
		public Vector3[] vectorVertices
		{
			get
			{
				if (this._vectorVertices != null && this._vectorVertices.Length == this.vertices.Length)
				{
					return this._vectorVertices;
				}
				if (this.vertices == null)
				{
					return null;
				}
				this._vectorVertices = new Vector3[this.vertices.Length];
				for (int i = 0; i < this._vectorVertices.Length; i++)
				{
					this._vectorVertices[i] = (Vector3)this.vertices[i];
				}
				return this._vectorVertices;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002E984 File Offset: 0x0002CD84
		public Int3 GetVertex(int index)
		{
			int num = index >> 12 & 524287;
			return this.tiles[num].GetVertex(index);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0002E9AA File Offset: 0x0002CDAA
		public int GetTileIndex(int index)
		{
			return index >> 12 & 524287;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002E9B6 File Offset: 0x0002CDB6
		public int GetVertexArrayIndex(int index)
		{
			return index & 4095;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002E9BF File Offset: 0x0002CDBF
		public void GetTileCoordinates(int tileIndex, out int x, out int z)
		{
			z = tileIndex / this.tileXCount;
			x = tileIndex - z * this.tileXCount;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002E9D8 File Offset: 0x0002CDD8
		public RecastGraph.NavmeshTile[] GetTiles()
		{
			return this.tiles;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002E9E0 File Offset: 0x0002CDE0
		public void SetTiles(RecastGraph.NavmeshTile[] newTiles)
		{
			this.tiles = newTiles;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002E9EC File Offset: 0x0002CDEC
		public Bounds GetTileBounds(int x, int z)
		{
			Bounds result = default(Bounds);
			result.SetMinMax(new Vector3((float)(x * this.tileSizeX) * this.cellSize, 0f, (float)(z * this.tileSizeZ) * this.cellSize) + this.forcedBounds.min, new Vector3((float)((x + 1) * this.tileSizeX) * this.cellSize, this.forcedBounds.size.y, (float)((z + 1) * this.tileSizeZ) * this.cellSize) + this.forcedBounds.min);
			return result;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002EA9C File Offset: 0x0002CE9C
		public Bounds GetTileBounds(int x, int z, int width, int depth)
		{
			Bounds result = default(Bounds);
			result.SetMinMax(new Vector3((float)(x * this.tileSizeX) * this.cellSize, 0f, (float)(z * this.tileSizeZ) * this.cellSize) + this.forcedBounds.min, new Vector3((float)((x + width) * this.tileSizeX) * this.cellSize, this.forcedBounds.size.y, (float)((z + depth) * this.tileSizeZ) * this.cellSize) + this.forcedBounds.min);
			return result;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002EB4C File Offset: 0x0002CF4C
		public Int2 GetTileCoordinates(Vector3 p)
		{
			p -= this.forcedBounds.min;
			p.x /= this.cellSize * (float)this.tileSizeX;
			p.z /= this.cellSize * (float)this.tileSizeZ;
			return new Int2((int)p.x, (int)p.z);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002EBBC File Offset: 0x0002CFBC
		public override void OnDestroy()
		{
			base.OnDestroy();
			TriangleMeshNode.SetNavmeshHolder(this.active.astarData.GetGraphIndex(this), null);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002EBDC File Offset: 0x0002CFDC
		private static RecastGraph.NavmeshTile NewEmptyTile(int x, int z)
		{
			RecastGraph.NavmeshTile navmeshTile = new RecastGraph.NavmeshTile();
			navmeshTile.x = x;
			navmeshTile.z = z;
			navmeshTile.w = 1;
			navmeshTile.d = 1;
			navmeshTile.verts = new Int3[0];
			navmeshTile.tris = new int[0];
			navmeshTile.nodes = new TriangleMeshNode[0];
			navmeshTile.bbTree = new BBTree(navmeshTile);
			return navmeshTile;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002EC3C File Offset: 0x0002D03C
		public override void GetNodes(GraphNodeDelegateCancelable del)
		{
			if (this.tiles == null)
			{
				return;
			}
			for (int i = 0; i < this.tiles.Length; i++)
			{
				if (this.tiles[i] != null && this.tiles[i].x + this.tiles[i].z * this.tileXCount == i)
				{
					TriangleMeshNode[] nodes = this.tiles[i].nodes;
					if (nodes != null)
					{
						int num = 0;
						while (num < nodes.Length && del(nodes[num]))
						{
							num++;
						}
					}
				}
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002ECE3 File Offset: 0x0002D0E3
		public Vector3 ClosestPointOnNode(TriangleMeshNode node, Vector3 pos)
		{
			return Polygon.ClosestPointOnTriangle((Vector3)this.GetVertex(node.v0), (Vector3)this.GetVertex(node.v1), (Vector3)this.GetVertex(node.v2), pos);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002ED20 File Offset: 0x0002D120
		public bool ContainsPoint(TriangleMeshNode node, Vector3 pos)
		{
			return Polygon.IsClockwise((Vector3)this.GetVertex(node.v0), (Vector3)this.GetVertex(node.v1), pos) && Polygon.IsClockwise((Vector3)this.GetVertex(node.v1), (Vector3)this.GetVertex(node.v2), pos) && Polygon.IsClockwise((Vector3)this.GetVertex(node.v2), (Vector3)this.GetVertex(node.v0), pos);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002EDB8 File Offset: 0x0002D1B8
		public void SnapForceBoundsToScene()
		{
			List<ExtraMesh> sceneMeshes = this.GetSceneMeshes(this.forcedBounds);
			if (sceneMeshes.Count == 0)
			{
				return;
			}
			Bounds bounds = default(Bounds);
			bounds = sceneMeshes[0].bounds;
			for (int i = 1; i < sceneMeshes.Count; i++)
			{
				bounds.Encapsulate(sceneMeshes[i].bounds);
			}
			this.forcedBoundsCenter = bounds.center;
			this.forcedBoundsSize = bounds.size;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002EE40 File Offset: 0x0002D240
		public void GetRecastMeshObjs(Bounds bounds, List<ExtraMesh> buffer)
		{
			List<RecastMeshObj> list = ListPool<RecastMeshObj>.Claim();
			RecastMeshObj.GetAllInBounds(list, bounds);
			Dictionary<Mesh, Vector3[]> dictionary = new Dictionary<Mesh, Vector3[]>();
			Dictionary<Mesh, int[]> dictionary2 = new Dictionary<Mesh, int[]>();
			for (int i = 0; i < list.Count; i++)
			{
				MeshFilter meshFilter = list[i].GetMeshFilter();
				if (meshFilter != null)
				{
					Mesh sharedMesh = meshFilter.sharedMesh;
					ExtraMesh item = default(ExtraMesh);
					item.matrix = meshFilter.GetComponent<Renderer>().localToWorldMatrix;
					item.original = meshFilter;
					item.area = list[i].area;
					if (dictionary.ContainsKey(sharedMesh))
					{
						item.vertices = dictionary[sharedMesh];
						item.triangles = dictionary2[sharedMesh];
					}
					else
					{
						item.vertices = sharedMesh.vertices;
						item.triangles = sharedMesh.triangles;
						dictionary[sharedMesh] = item.vertices;
						dictionary2[sharedMesh] = item.triangles;
					}
					item.bounds = meshFilter.GetComponent<Renderer>().bounds;
					buffer.Add(item);
				}
				else
				{
					Collider collider = list[i].GetCollider();
					if (collider == null)
					{
						UnityEngine.Debug.LogError("RecastMeshObject (" + list[i].gameObject.name + ") didn't have a collider or MeshFilter attached");
					}
					else
					{
						ExtraMesh item2 = this.RasterizeCollider(collider);
						item2.area = list[i].area;
						if (item2.vertices != null)
						{
							buffer.Add(item2);
						}
					}
				}
			}
			this.capsuleCache.Clear();
			ListPool<RecastMeshObj>.Release(list);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002EFEC File Offset: 0x0002D3EC
		public List<ExtraMesh> GetSceneMeshes(Bounds bounds)
		{
			if ((this.tagMask != null && this.tagMask.Count > 0) || this.mask != 0)
			{
				MeshFilter[] array = UnityEngine.Object.FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[];
				List<MeshFilter> list = new List<MeshFilter>(array.Length / 3);
				foreach (MeshFilter meshFilter in array)
				{
					if (meshFilter.GetComponent<Renderer>() != null && meshFilter.sharedMesh != null && meshFilter.GetComponent<Renderer>().enabled && ((1 << meshFilter.gameObject.layer & this.mask) == 1 << meshFilter.gameObject.layer || this.tagMask.Contains(meshFilter.tag)) && meshFilter.GetComponent<RecastMeshObj>() == null)
					{
						list.Add(meshFilter);
					}
				}
				List<ExtraMesh> list2 = new List<ExtraMesh>();
				Dictionary<Mesh, Vector3[]> dictionary = new Dictionary<Mesh, Vector3[]>();
				Dictionary<Mesh, int[]> dictionary2 = new Dictionary<Mesh, int[]>();
				bool flag = false;
				foreach (MeshFilter meshFilter2 in list)
				{
					if (meshFilter2.GetComponent<Renderer>().isPartOfStaticBatch)
					{
						flag = true;
					}
					else if (meshFilter2.GetComponent<Renderer>().bounds.Intersects(bounds))
					{
						Mesh sharedMesh = meshFilter2.sharedMesh;
						ExtraMesh item = default(ExtraMesh);
						item.matrix = meshFilter2.GetComponent<Renderer>().localToWorldMatrix;
						item.original = meshFilter2;
						if (dictionary.ContainsKey(sharedMesh))
						{
							item.vertices = dictionary[sharedMesh];
							item.triangles = dictionary2[sharedMesh];
						}
						else
						{
							item.vertices = sharedMesh.vertices;
							item.triangles = sharedMesh.triangles;
							dictionary[sharedMesh] = item.vertices;
							dictionary2[sharedMesh] = item.triangles;
						}
						item.bounds = meshFilter2.GetComponent<Renderer>().bounds;
						list2.Add(item);
					}
					if (flag)
					{
						UnityEngine.Debug.LogWarning("Some meshes were statically batched. These meshes can not be used for navmesh calculation due to technical constraints.");
					}
				}
				return list2;
			}
			return new List<ExtraMesh>();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002F25C File Offset: 0x0002D65C
		public IntRect GetTouchingTiles(Bounds b)
		{
			b.center -= this.forcedBounds.min;
			IntRect intRect = new IntRect(Mathf.FloorToInt(b.min.x / ((float)this.tileSizeX * this.cellSize)), Mathf.FloorToInt(b.min.z / ((float)this.tileSizeZ * this.cellSize)), Mathf.FloorToInt(b.max.x / ((float)this.tileSizeX * this.cellSize)), Mathf.FloorToInt(b.max.z / ((float)this.tileSizeZ * this.cellSize)));
			intRect = IntRect.Intersection(intRect, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			return intRect;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002F340 File Offset: 0x0002D740
		public IntRect GetTouchingTilesRound(Bounds b)
		{
			b.center -= this.forcedBounds.min;
			IntRect intRect = new IntRect(Mathf.RoundToInt(b.min.x / ((float)this.tileSizeX * this.cellSize)), Mathf.RoundToInt(b.min.z / ((float)this.tileSizeZ * this.cellSize)), Mathf.RoundToInt(b.max.x / ((float)this.tileSizeX * this.cellSize)) - 1, Mathf.RoundToInt(b.max.z / ((float)this.tileSizeZ * this.cellSize)) - 1);
			intRect = IntRect.Intersection(intRect, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			return intRect;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002F425 File Offset: 0x0002D825
		public GraphUpdateThreading CanUpdateAsync(GraphUpdateObject o)
		{
			if (o.updatePhysics)
			{
				return GraphUpdateThreading.SeparateAndUnityInit;
			}
			return GraphUpdateThreading.SeparateThread;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002F438 File Offset: 0x0002D838
		public void UpdateAreaInit(GraphUpdateObject o)
		{
			if (!o.updatePhysics)
			{
				return;
			}
			if (!this.dynamic)
			{
				throw new Exception("Recast graph must be marked as dynamic to enable graph updates");
			}
			RelevantGraphSurface.UpdateAllPositions();
			IntRect touchingTiles = this.GetTouchingTiles(o.bounds);
			Bounds bounds = default(Bounds);
			Vector3 min = this.forcedBounds.min;
			Vector3 max = this.forcedBounds.max;
			float num = (float)this.tileSizeX * this.cellSize;
			float num2 = (float)this.tileSizeZ * this.cellSize;
			bounds.SetMinMax(new Vector3((float)touchingTiles.xmin * num, 0f, (float)touchingTiles.ymin * num2) + min, new Vector3((float)(touchingTiles.xmax + 1) * num + min.x, max.y, (float)(touchingTiles.ymax + 1) * num2 + min.z));
			int num3 = Mathf.CeilToInt(this.characterRadius / this.cellSize);
			int num4 = num3 + 3;
			bounds.Expand(new Vector3((float)num4, 0f, (float)num4) * this.cellSize * 2f);
			List<ExtraMesh> inputExtraMeshes;
			if (!this.CollectMeshes(out inputExtraMeshes, bounds))
			{
			}
			Voxelize voxelize = this.globalVox;
			if (voxelize == null)
			{
				voxelize = new Voxelize(this.cellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope);
				voxelize.maxEdgeLength = this.maxEdgeLength;
				if (this.dynamic)
				{
					this.globalVox = voxelize;
				}
			}
			voxelize.inputExtraMeshes = inputExtraMeshes;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002F5D8 File Offset: 0x0002D9D8
		public void UpdateArea(GraphUpdateObject guo)
		{
			Bounds bounds = guo.bounds;
			bounds.center -= this.forcedBounds.min;
			IntRect a = new IntRect(Mathf.FloorToInt(bounds.min.x / ((float)this.tileSizeX * this.cellSize)), Mathf.FloorToInt(bounds.min.z / ((float)this.tileSizeZ * this.cellSize)), Mathf.FloorToInt(bounds.max.x / ((float)this.tileSizeX * this.cellSize)), Mathf.FloorToInt(bounds.max.z / ((float)this.tileSizeZ * this.cellSize)));
			a = IntRect.Intersection(a, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			if (!guo.updatePhysics)
			{
				for (int i = a.ymin; i <= a.ymax; i++)
				{
					for (int j = a.xmin; j <= a.xmax; j++)
					{
						RecastGraph.NavmeshTile navmeshTile = this.tiles[i * this.tileXCount + j];
						navmeshTile.flag = true;
					}
				}
				for (int k = a.ymin; k <= a.ymax; k++)
				{
					for (int l = a.xmin; l <= a.xmax; l++)
					{
						RecastGraph.NavmeshTile navmeshTile2 = this.tiles[k * this.tileXCount + l];
						if (navmeshTile2.flag)
						{
							navmeshTile2.flag = false;
							NavMeshGraph.UpdateArea(guo, navmeshTile2);
						}
					}
				}
				return;
			}
			if (!this.dynamic)
			{
				throw new Exception("Recast graph must be marked as dynamic to enable graph updates with updatePhysics = true");
			}
			Voxelize voxelize = this.globalVox;
			if (voxelize == null)
			{
				throw new InvalidOperationException("No Voxelizer object. UpdateAreaInit should have been called before this function.");
			}
			for (int m = a.xmin; m <= a.xmax; m++)
			{
				for (int n = a.ymin; n <= a.ymax; n++)
				{
					this.RemoveConnectionsFromTile(this.tiles[m + n * this.tileXCount]);
				}
			}
			for (int num = a.xmin; num <= a.xmax; num++)
			{
				for (int num2 = a.ymin; num2 <= a.ymax; num2++)
				{
					this.BuildTileMesh(voxelize, num, num2);
				}
			}
			uint graphIndex = (uint)AstarPath.active.astarData.GetGraphIndex(this);
			for (int num3 = a.xmin; num3 <= a.xmax; num3++)
			{
				for (int num4 = a.ymin; num4 <= a.ymax; num4++)
				{
					RecastGraph.NavmeshTile navmeshTile3 = this.tiles[num3 + num4 * this.tileXCount];
					GraphNode[] nodes = navmeshTile3.nodes;
					for (int num5 = 0; num5 < nodes.Length; num5++)
					{
						nodes[num5].GraphIndex = graphIndex;
					}
				}
			}
			a = a.Expand(1);
			a = IntRect.Intersection(a, new IntRect(0, 0, this.tileXCount - 1, this.tileZCount - 1));
			for (int num6 = a.xmin; num6 <= a.xmax; num6++)
			{
				for (int num7 = a.ymin; num7 <= a.ymax; num7++)
				{
					if (num6 < this.tileXCount - 1 && a.Contains(num6 + 1, num7))
					{
						this.ConnectTiles(this.tiles[num6 + num7 * this.tileXCount], this.tiles[num6 + 1 + num7 * this.tileXCount]);
					}
					if (num7 < this.tileZCount - 1 && a.Contains(num6, num7 + 1))
					{
						this.ConnectTiles(this.tiles[num6 + num7 * this.tileXCount], this.tiles[num6 + (num7 + 1) * this.tileXCount]);
					}
				}
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002FA24 File Offset: 0x0002DE24
		public void ConnectTileWithNeighbours(RecastGraph.NavmeshTile tile)
		{
			if (tile.x > 0)
			{
				int num = tile.x - 1;
				for (int i = tile.z; i < tile.z + tile.d; i++)
				{
					this.ConnectTiles(this.tiles[num + i * this.tileXCount], tile);
				}
			}
			if (tile.x + tile.w < this.tileXCount)
			{
				int num2 = tile.x + tile.w;
				for (int j = tile.z; j < tile.z + tile.d; j++)
				{
					this.ConnectTiles(this.tiles[num2 + j * this.tileXCount], tile);
				}
			}
			if (tile.z > 0)
			{
				int num3 = tile.z - 1;
				for (int k = tile.x; k < tile.x + tile.w; k++)
				{
					this.ConnectTiles(this.tiles[k + num3 * this.tileXCount], tile);
				}
			}
			if (tile.z + tile.d < this.tileZCount)
			{
				int num4 = tile.z + tile.d;
				for (int l = tile.x; l < tile.x + tile.w; l++)
				{
					this.ConnectTiles(this.tiles[l + num4 * this.tileXCount], tile);
				}
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002FBA4 File Offset: 0x0002DFA4
		public void RemoveConnectionsFromTile(RecastGraph.NavmeshTile tile)
		{
			if (tile.x > 0)
			{
				int num = tile.x - 1;
				for (int i = tile.z; i < tile.z + tile.d; i++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num + i * this.tileXCount], tile);
				}
			}
			if (tile.x + tile.w < this.tileXCount)
			{
				int num2 = tile.x + tile.w;
				for (int j = tile.z; j < tile.z + tile.d; j++)
				{
					this.RemoveConnectionsFromTo(this.tiles[num2 + j * this.tileXCount], tile);
				}
			}
			if (tile.z > 0)
			{
				int num3 = tile.z - 1;
				for (int k = tile.x; k < tile.x + tile.w; k++)
				{
					this.RemoveConnectionsFromTo(this.tiles[k + num3 * this.tileXCount], tile);
				}
			}
			if (tile.z + tile.d < this.tileZCount)
			{
				int num4 = tile.z + tile.d;
				for (int l = tile.x; l < tile.x + tile.w; l++)
				{
					this.RemoveConnectionsFromTo(this.tiles[l + num4 * this.tileXCount], tile);
				}
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002FD24 File Offset: 0x0002E124
		public void RemoveConnectionsFromTo(RecastGraph.NavmeshTile a, RecastGraph.NavmeshTile b)
		{
			if (a == null || b == null)
			{
				return;
			}
			if (a == b)
			{
				return;
			}
			int num = b.x + b.z * this.tileXCount;
			for (int i = 0; i < a.nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = a.nodes[i];
				if (triangleMeshNode.connections != null)
				{
					for (int j = 0; j < triangleMeshNode.connections.Length; j++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[j] as TriangleMeshNode;
						if (triangleMeshNode2 != null)
						{
							int num2 = triangleMeshNode2.GetVertexIndex(0);
							num2 = (num2 >> 12 & 524287);
							if (num2 == num)
							{
								triangleMeshNode.RemoveConnection(triangleMeshNode.connections[j]);
								j--;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002FDFC File Offset: 0x0002E1FC
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			return this.GetNearestForce(position, null);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002FE14 File Offset: 0x0002E214
		public override NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return default(NNInfo);
			}
			Vector3 vector = position - this.forcedBounds.min;
			int num = Mathf.FloorToInt(vector.x / (this.cellSize * (float)this.tileSizeX));
			int num2 = Mathf.FloorToInt(vector.z / (this.cellSize * (float)this.tileSizeZ));
			num = Mathf.Clamp(num, 0, this.tileXCount - 1);
			num2 = Mathf.Clamp(num2, 0, this.tileZCount - 1);
			int num3 = Math.Max(this.tileXCount, this.tileZCount);
			NNInfo nninfo = default(NNInfo);
			float positiveInfinity = float.PositiveInfinity;
			bool flag = this.nearestSearchOnlyXZ || (constraint != null && constraint.distanceXZ);
			for (int i = 0; i < num3; i++)
			{
				if (!flag && positiveInfinity < (float)(i - 1) * this.cellSize * (float)Math.Max(this.tileSizeX, this.tileSizeZ))
				{
					break;
				}
				int num4 = Math.Min(i + num2 + 1, this.tileZCount);
				for (int j = Math.Max(-i + num2, 0); j < num4; j++)
				{
					int num5 = Math.Abs(i - Math.Abs(j - num2));
					if (-num5 + num >= 0)
					{
						int num6 = -num5 + num;
						RecastGraph.NavmeshTile navmeshTile = this.tiles[num6 + j * this.tileXCount];
						if (navmeshTile != null)
						{
							if (flag)
							{
								nninfo = navmeshTile.bbTree.QueryClosestXZ(position, constraint, ref positiveInfinity, nninfo);
								if (positiveInfinity < float.PositiveInfinity)
								{
									break;
								}
							}
							else
							{
								nninfo = navmeshTile.bbTree.QueryClosest(position, constraint, ref positiveInfinity, nninfo);
							}
						}
					}
					if (num5 != 0 && num5 + num < this.tileXCount)
					{
						int num7 = num5 + num;
						RecastGraph.NavmeshTile navmeshTile2 = this.tiles[num7 + j * this.tileXCount];
						if (navmeshTile2 != null)
						{
							if (flag)
							{
								nninfo = navmeshTile2.bbTree.QueryClosestXZ(position, constraint, ref positiveInfinity, nninfo);
								if (positiveInfinity < float.PositiveInfinity)
								{
									break;
								}
							}
							else
							{
								nninfo = navmeshTile2.bbTree.QueryClosest(position, constraint, ref positiveInfinity, nninfo);
							}
						}
					}
				}
			}
			nninfo.node = nninfo.constrainedNode;
			nninfo.constrainedNode = null;
			nninfo.clampedPosition = nninfo.constClampedPosition;
			return nninfo;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00030090 File Offset: 0x0002E490
		public GraphNode PointOnNavmesh(Vector3 position, NNConstraint constraint)
		{
			if (this.tiles == null)
			{
				return null;
			}
			Vector3 vector = position - this.forcedBounds.min;
			int num = Mathf.FloorToInt(vector.x / (this.cellSize * (float)this.tileSizeX));
			int num2 = Mathf.FloorToInt(vector.z / (this.cellSize * (float)this.tileSizeZ));
			if (num < 0 || num2 < 0 || num >= this.tileXCount || num2 >= this.tileZCount)
			{
				return null;
			}
			RecastGraph.NavmeshTile navmeshTile = this.tiles[num + num2 * this.tileXCount];
			if (navmeshTile != null)
			{
				return navmeshTile.bbTree.QueryInside(position, constraint);
			}
			return null;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0003014D File Offset: 0x0002E54D
		public void BuildFunnelCorridor(List<GraphNode> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right)
		{
			NavMeshGraph.BuildFunnelCorridor(this, path, startIndex, endIndex, left, right);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0003015C File Offset: 0x0002E55C
		public void AddPortal(GraphNode n1, GraphNode n2, List<Vector3> left, List<Vector3> right)
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0003015E File Offset: 0x0002E55E
		public static string GetRecastPath()
		{
			return Application.dataPath + "/Recast/recast";
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0003016F File Offset: 0x0002E56F
		public override void ScanInternal(OnScanStatus statusCallback)
		{
			TriangleMeshNode.SetNavmeshHolder(AstarPath.active.astarData.GetGraphIndex(this), this);
			this.ScanTiledNavmesh(statusCallback);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0003018E File Offset: 0x0002E58E
		protected void ScanTiledNavmesh(OnScanStatus statusCallback)
		{
			this.ScanAllTiles(statusCallback);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00030198 File Offset: 0x0002E598
		protected void ScanAllTiles(OnScanStatus statusCallback)
		{
			int num = (int)(this.forcedBounds.size.x / this.cellSize + 0.5f);
			int num2 = (int)(this.forcedBounds.size.z / this.cellSize + 0.5f);
			if (!this.useTiles)
			{
				this.tileSizeX = num;
				this.tileSizeZ = num2;
			}
			else
			{
				this.tileSizeX = this.editorTileSize;
				this.tileSizeZ = this.editorTileSize;
			}
			int num3 = (num + this.tileSizeX - 1) / this.tileSizeX;
			int num4 = (num2 + this.tileSizeZ - 1) / this.tileSizeZ;
			this.tileXCount = num3;
			this.tileZCount = num4;
			if (this.tileXCount * this.tileZCount > 524288)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Too many tiles (",
					this.tileXCount * this.tileZCount,
					") maximum is ",
					524288,
					"\nTry disabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* inspector."
				}));
			}
			this.tiles = new RecastGraph.NavmeshTile[this.tileXCount * this.tileZCount];
			if (this.scanEmptyGraph)
			{
				for (int i = 0; i < num4; i++)
				{
					for (int j = 0; j < num3; j++)
					{
						this.tiles[i * this.tileXCount + j] = RecastGraph.NewEmptyTile(j, i);
					}
				}
				return;
			}
			Console.WriteLine("Collecting Meshes");
			List<ExtraMesh> inputExtraMeshes;
			this.CollectMeshes(out inputExtraMeshes, this.forcedBounds);
			Voxelize voxelize = new Voxelize(this.cellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope);
			voxelize.inputExtraMeshes = inputExtraMeshes;
			voxelize.maxEdgeLength = this.maxEdgeLength;
			int num5 = -1;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (int k = 0; k < num4; k++)
			{
				for (int l = 0; l < num3; l++)
				{
					int num6 = k * this.tileXCount + l;
					Console.WriteLine(string.Concat(new object[]
					{
						"Generating Tile #",
						num6,
						" of ",
						num4 * num3
					}));
					if ((num6 * 10 / this.tiles.Length > num5 || stopwatch.ElapsedMilliseconds > 2000L) && statusCallback != null)
					{
						num5 = num6 * 10 / this.tiles.Length;
						stopwatch.Reset();
						stopwatch.Start();
						statusCallback(new Progress(AstarMath.MapToRange(0.1f, 0.9f, (float)num6 / (float)this.tiles.Length), string.Concat(new object[]
						{
							"Building Tile ",
							num6,
							"/",
							this.tiles.Length
						})));
					}
					this.BuildTileMesh(voxelize, l, k);
				}
			}
			Console.WriteLine("Assigning Graph Indices");
			if (statusCallback != null)
			{
				statusCallback(new Progress(0.9f, "Connecting tiles"));
			}
			uint graphIndex = (uint)AstarPath.active.astarData.GetGraphIndex(this);
			GraphNodeDelegateCancelable del = delegate(GraphNode n)
			{
				n.GraphIndex = graphIndex;
				return true;
			};
			this.GetNodes(del);
			for (int m = 0; m < num4; m++)
			{
				for (int n2 = 0; n2 < num3; n2++)
				{
					Console.WriteLine(string.Concat(new object[]
					{
						"Connecing Tile #",
						m * this.tileXCount + n2,
						" of ",
						num4 * num3
					}));
					if (n2 < num3 - 1)
					{
						this.ConnectTiles(this.tiles[n2 + m * this.tileXCount], this.tiles[n2 + 1 + m * this.tileXCount]);
					}
					if (m < num4 - 1)
					{
						this.ConnectTiles(this.tiles[n2 + m * this.tileXCount], this.tiles[n2 + (m + 1) * this.tileXCount]);
					}
				}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000305F4 File Offset: 0x0002E9F4
		protected void BuildTileMesh(Voxelize vox, int x, int z)
		{
			float num = (float)this.tileSizeX * this.cellSize;
			float num2 = (float)this.tileSizeZ * this.cellSize;
			int num3 = Mathf.CeilToInt(this.characterRadius / this.cellSize);
			Vector3 min = this.forcedBounds.min;
			Vector3 max = this.forcedBounds.max;
			Bounds forcedBounds = default(Bounds);
			forcedBounds.SetMinMax(new Vector3((float)x * num, 0f, (float)z * num2) + min, new Vector3((float)(x + 1) * num + min.x, max.y, (float)(z + 1) * num2 + min.z));
			vox.borderSize = num3 + 3;
			forcedBounds.Expand(new Vector3((float)vox.borderSize, 0f, (float)vox.borderSize) * this.cellSize * 2f);
			vox.forcedBounds = forcedBounds;
			vox.width = this.tileSizeX + vox.borderSize * 2;
			vox.depth = this.tileSizeZ + vox.borderSize * 2;
			if (!this.useTiles && this.relevantGraphSurfaceMode == RecastGraph.RelevantGraphSurfaceMode.OnlyForCompletelyInsideTile)
			{
				vox.relevantGraphSurfaceMode = RecastGraph.RelevantGraphSurfaceMode.RequireForAll;
			}
			else
			{
				vox.relevantGraphSurfaceMode = this.relevantGraphSurfaceMode;
			}
			vox.minRegionSize = Mathf.RoundToInt(this.minRegionSize / (this.cellSize * this.cellSize));
			vox.Init();
			vox.CollectMeshes();
			vox.VoxelizeInput();
			vox.FilterLedges(vox.voxelWalkableHeight, vox.voxelWalkableClimb, vox.cellSize, vox.cellHeight, vox.forcedBounds.min);
			vox.FilterLowHeightSpans(vox.voxelWalkableHeight, vox.cellSize, vox.cellHeight, vox.forcedBounds.min);
			vox.BuildCompactField();
			vox.BuildVoxelConnections();
			vox.ErodeWalkableArea(num3);
			vox.BuildDistanceField();
			vox.BuildRegions();
			VoxelContourSet cset = new VoxelContourSet();
			vox.BuildContours(this.contourMaxError, 1, cset, 1);
			VoxelMesh mesh;
			vox.BuildPolyMesh(cset, 3, out mesh);
			for (int i = 0; i < mesh.verts.Length; i++)
			{
				mesh.verts[i] = mesh.verts[i] * 1000 * vox.cellScale + (Int3)vox.voxelOffset;
			}
			RecastGraph.NavmeshTile navmeshTile = this.CreateTile(vox, mesh, x, z);
			this.tiles[navmeshTile.x + navmeshTile.z * this.tileXCount] = navmeshTile;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00030894 File Offset: 0x0002EC94
		private RecastGraph.NavmeshTile CreateTile(Voxelize vox, VoxelMesh mesh, int x, int z)
		{
			if (mesh.tris == null)
			{
				throw new ArgumentNullException("The mesh must be valid. tris is null.");
			}
			if (mesh.verts == null)
			{
				throw new ArgumentNullException("The mesh must be valid. verts is null.");
			}
			RecastGraph.NavmeshTile navmeshTile = new RecastGraph.NavmeshTile();
			navmeshTile.x = x;
			navmeshTile.z = z;
			navmeshTile.w = 1;
			navmeshTile.d = 1;
			navmeshTile.tris = mesh.tris;
			navmeshTile.verts = mesh.verts;
			navmeshTile.bbTree = new BBTree(navmeshTile);
			if (navmeshTile.tris.Length % 3 != 0)
			{
				throw new ArgumentException("Indices array's length must be a multiple of 3 (mesh.tris)");
			}
			if (navmeshTile.verts.Length >= 4095)
			{
				throw new ArgumentException("Too many vertices per tile (more than " + 4095 + ").\nTry enabling ASTAR_RECAST_LARGER_TILES under the 'Optimizations' tab in the A* Inspector");
			}
			Dictionary<Int3, int> dictionary = this.cachedInt3_int_dict;
			dictionary.Clear();
			int[] array = new int[navmeshTile.verts.Length];
			int num = 0;
			for (int i = 0; i < navmeshTile.verts.Length; i++)
			{
				try
				{
					dictionary.Add(navmeshTile.verts[i], num);
					array[i] = num;
					navmeshTile.verts[num] = navmeshTile.verts[i];
					num++;
				}
				catch
				{
					array[i] = dictionary[navmeshTile.verts[i]];
				}
			}
			for (int j = 0; j < navmeshTile.tris.Length; j++)
			{
				navmeshTile.tris[j] = array[navmeshTile.tris[j]];
			}
			Int3[] array2 = new Int3[num];
			for (int k = 0; k < num; k++)
			{
				array2[k] = navmeshTile.verts[k];
			}
			navmeshTile.verts = array2;
			TriangleMeshNode[] array3 = new TriangleMeshNode[navmeshTile.tris.Length / 3];
			navmeshTile.nodes = array3;
			int graphIndex = AstarPath.active.astarData.graphs.Length;
			TriangleMeshNode.SetNavmeshHolder(graphIndex, navmeshTile);
			int num2 = x + z * this.tileXCount;
			num2 <<= 12;
			for (int l = 0; l < array3.Length; l++)
			{
				TriangleMeshNode triangleMeshNode = new TriangleMeshNode(this.active);
				array3[l] = triangleMeshNode;
				triangleMeshNode.GraphIndex = (uint)graphIndex;
				triangleMeshNode.v0 = (navmeshTile.tris[l * 3] | num2);
				triangleMeshNode.v1 = (navmeshTile.tris[l * 3 + 1] | num2);
				triangleMeshNode.v2 = (navmeshTile.tris[l * 3 + 2] | num2);
				if (!Polygon.IsClockwise(triangleMeshNode.GetVertex(0), triangleMeshNode.GetVertex(1), triangleMeshNode.GetVertex(2)))
				{
					int v = triangleMeshNode.v0;
					triangleMeshNode.v0 = triangleMeshNode.v2;
					triangleMeshNode.v2 = v;
				}
				triangleMeshNode.Walkable = true;
				triangleMeshNode.Penalty = this.initialPenalty;
				triangleMeshNode.UpdatePositionFromVertices();
				navmeshTile.bbTree.Insert(triangleMeshNode);
			}
			this.CreateNodeConnections(navmeshTile.nodes);
			TriangleMeshNode.SetNavmeshHolder(graphIndex, null);
			return navmeshTile;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00030BD4 File Offset: 0x0002EFD4
		public void CreateNodeConnections(TriangleMeshNode[] nodes)
		{
			List<MeshNode> list = ListPool<MeshNode>.Claim();
			List<uint> list2 = ListPool<uint>.Claim();
			Dictionary<Int2, int> dictionary = this.cachedInt2_int_dict;
			dictionary.Clear();
			for (int i = 0; i < nodes.Length; i++)
			{
				TriangleMeshNode triangleMeshNode = nodes[i];
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int j = 0; j < vertexCount; j++)
				{
					try
					{
						dictionary.Add(new Int2(triangleMeshNode.GetVertexIndex(j), triangleMeshNode.GetVertexIndex((j + 1) % vertexCount)), i);
					}
					catch (Exception)
					{
					}
				}
			}
			foreach (TriangleMeshNode triangleMeshNode2 in nodes)
			{
				list.Clear();
				list2.Clear();
				int vertexCount2 = triangleMeshNode2.GetVertexCount();
				for (int l = 0; l < vertexCount2; l++)
				{
					int vertexIndex = triangleMeshNode2.GetVertexIndex(l);
					int vertexIndex2 = triangleMeshNode2.GetVertexIndex((l + 1) % vertexCount2);
					int num;
					if (dictionary.TryGetValue(new Int2(vertexIndex2, vertexIndex), out num))
					{
						TriangleMeshNode triangleMeshNode3 = nodes[num];
						int vertexCount3 = triangleMeshNode3.GetVertexCount();
						for (int m = 0; m < vertexCount3; m++)
						{
							if (triangleMeshNode3.GetVertexIndex(m) == vertexIndex2 && triangleMeshNode3.GetVertexIndex((m + 1) % vertexCount3) == vertexIndex)
							{
								uint costMagnitude = (uint)(triangleMeshNode2.position - triangleMeshNode3.position).costMagnitude;
								list.Add(triangleMeshNode3);
								list2.Add(costMagnitude);
								break;
							}
						}
					}
				}
				triangleMeshNode2.connections = list.ToArray();
				triangleMeshNode2.connectionCosts = list2.ToArray();
			}
			ListPool<MeshNode>.Release(list);
			ListPool<uint>.Release(list2);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00030D90 File Offset: 0x0002F190
		private void ConnectTiles(RecastGraph.NavmeshTile tile1, RecastGraph.NavmeshTile tile2)
		{
			if (tile1 == null)
			{
				return;
			}
			if (tile2 == null)
			{
				return;
			}
			if (tile1.nodes == null)
			{
				throw new ArgumentException("tile1 does not contain any nodes");
			}
			if (tile2.nodes == null)
			{
				throw new ArgumentException("tile2 does not contain any nodes");
			}
			int num = Mathf.Clamp(tile2.x, tile1.x, tile1.x + tile1.w - 1);
			int num2 = Mathf.Clamp(tile1.x, tile2.x, tile2.x + tile2.w - 1);
			int num3 = Mathf.Clamp(tile2.z, tile1.z, tile1.z + tile1.d - 1);
			int num4 = Mathf.Clamp(tile1.z, tile2.z, tile2.z + tile2.d - 1);
			int num5;
			int i;
			int num6;
			int num7;
			float num8;
			if (num == num2)
			{
				num5 = 2;
				i = 0;
				num6 = num3;
				num7 = num4;
				num8 = (float)this.tileSizeZ * this.cellSize;
			}
			else
			{
				if (num3 != num4)
				{
					throw new ArgumentException("Tiles are not adjacent (neither x or z coordinates match)");
				}
				num5 = 0;
				i = 2;
				num6 = num;
				num7 = num2;
				num8 = (float)this.tileSizeX * this.cellSize;
			}
			if (Math.Abs(num6 - num7) != 1)
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					tile1.x,
					" ",
					tile1.z,
					" ",
					tile1.w,
					" ",
					tile1.d,
					"\n",
					tile2.x,
					" ",
					tile2.z,
					" ",
					tile2.w,
					" ",
					tile2.d,
					"\n",
					num,
					" ",
					num3,
					" ",
					num2,
					" ",
					num4
				}));
				throw new ArgumentException(string.Concat(new object[]
				{
					"Tiles are not adjacent (tile coordinates must differ by exactly 1. Got '",
					num6,
					"' and '",
					num7,
					"')"
				}));
			}
			int num9 = (int)Math.Round((double)(((float)Math.Max(num6, num7) * num8 + this.forcedBounds.min[num5]) * 1000f));
			TriangleMeshNode[] nodes = tile1.nodes;
			TriangleMeshNode[] nodes2 = tile2.nodes;
			foreach (TriangleMeshNode triangleMeshNode in nodes)
			{
				int vertexCount = triangleMeshNode.GetVertexCount();
				for (int k = 0; k < vertexCount; k++)
				{
					Int3 vertex = triangleMeshNode.GetVertex(k);
					Int3 vertex2 = triangleMeshNode.GetVertex((k + 1) % vertexCount);
					if (Math.Abs(vertex[num5] - num9) < 2 && Math.Abs(vertex2[num5] - num9) < 2)
					{
						int num10 = Math.Min(vertex[i], vertex2[i]);
						int num11 = Math.Max(vertex[i], vertex2[i]);
						if (num10 != num11)
						{
							foreach (TriangleMeshNode triangleMeshNode2 in nodes2)
							{
								int vertexCount2 = triangleMeshNode2.GetVertexCount();
								for (int m = 0; m < vertexCount2; m++)
								{
									Int3 vertex3 = triangleMeshNode2.GetVertex(m);
									Int3 vertex4 = triangleMeshNode2.GetVertex((m + 1) % vertexCount);
									if (Math.Abs(vertex3[num5] - num9) < 2 && Math.Abs(vertex4[num5] - num9) < 2)
									{
										int num12 = Math.Min(vertex3[i], vertex4[i]);
										int num13 = Math.Max(vertex3[i], vertex4[i]);
										if (num12 != num13)
										{
											if (num11 > num12 && num10 < num13 && ((vertex == vertex3 && vertex2 == vertex4) || (vertex == vertex4 && vertex2 == vertex3) || Polygon.DistanceSegmentSegment3D((Vector3)vertex, (Vector3)vertex2, (Vector3)vertex3, (Vector3)vertex4) < this.walkableClimb * this.walkableClimb))
											{
												uint costMagnitude = (uint)(triangleMeshNode.position - triangleMeshNode2.position).costMagnitude;
												triangleMeshNode.AddConnection(triangleMeshNode2, costMagnitude);
												triangleMeshNode2.AddConnection(triangleMeshNode, costMagnitude);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00031298 File Offset: 0x0002F698
		public void StartBatchTileUpdate()
		{
			if (this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling StartBatchLoad when batching is already enabled");
			}
			this.batchTileUpdate = true;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000312B8 File Offset: 0x0002F6B8
		public void EndBatchTileUpdate()
		{
			if (!this.batchTileUpdate)
			{
				throw new InvalidOperationException("Calling EndBatchLoad when batching not enabled");
			}
			this.batchTileUpdate = false;
			int num = this.tileXCount;
			int num2 = this.tileZCount;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					this.tiles[j + i * this.tileXCount].flag = false;
				}
			}
			for (int k = 0; k < this.batchUpdatedTiles.Count; k++)
			{
				this.tiles[this.batchUpdatedTiles[k]].flag = true;
			}
			for (int l = 0; l < num2; l++)
			{
				for (int m = 0; m < num; m++)
				{
					if (m < num - 1 && (this.tiles[m + l * this.tileXCount].flag || this.tiles[m + 1 + l * this.tileXCount].flag) && this.tiles[m + l * this.tileXCount] != this.tiles[m + 1 + l * this.tileXCount])
					{
						this.ConnectTiles(this.tiles[m + l * this.tileXCount], this.tiles[m + 1 + l * this.tileXCount]);
					}
					if (l < num2 - 1 && (this.tiles[m + l * this.tileXCount].flag || this.tiles[m + (l + 1) * this.tileXCount].flag) && this.tiles[m + l * this.tileXCount] != this.tiles[m + (l + 1) * this.tileXCount])
					{
						this.ConnectTiles(this.tiles[m + l * this.tileXCount], this.tiles[m + (l + 1) * this.tileXCount]);
					}
				}
			}
			this.batchUpdatedTiles.Clear();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000314DE File Offset: 0x0002F8DE
		public void ReplaceTile(int x, int z, Int3[] verts, int[] tris, bool worldSpace)
		{
			this.ReplaceTile(x, z, 1, 1, verts, tris, worldSpace);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000314F0 File Offset: 0x0002F8F0
		public void ReplaceTile(int x, int z, int w, int d, Int3[] verts, int[] tris, bool worldSpace)
		{
			if (x + w > this.tileXCount || z + d > this.tileZCount || x < 0 || z < 0)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Tile is placed at an out of bounds position or extends out of the graph bounds (",
					x,
					", ",
					z,
					" [",
					w,
					", ",
					d,
					"] ",
					this.tileXCount,
					" ",
					this.tileZCount,
					")"
				}));
			}
			if (w < 1 || d < 1)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"width and depth must be greater or equal to 1. Was ",
					w,
					", ",
					d
				}));
			}
			for (int i = z; i < z + d; i++)
			{
				for (int j = x; j < x + w; j++)
				{
					RecastGraph.NavmeshTile navmeshTile = this.tiles[j + i * this.tileXCount];
					if (navmeshTile != null)
					{
						this.RemoveConnectionsFromTile(navmeshTile);
						for (int k = 0; k < navmeshTile.nodes.Length; k++)
						{
							navmeshTile.nodes[k].Destroy();
						}
						for (int l = navmeshTile.z; l < navmeshTile.z + navmeshTile.d; l++)
						{
							for (int m = navmeshTile.x; m < navmeshTile.x + navmeshTile.w; m++)
							{
								RecastGraph.NavmeshTile navmeshTile2 = this.tiles[m + l * this.tileXCount];
								if (navmeshTile2 == null || navmeshTile2 != navmeshTile)
								{
									throw new Exception("This should not happen");
								}
								if (l < z || l >= z + d || m < x || m >= x + w)
								{
									this.tiles[m + l * this.tileXCount] = RecastGraph.NewEmptyTile(m, l);
									if (this.batchTileUpdate)
									{
										this.batchUpdatedTiles.Add(m + l * this.tileXCount);
									}
								}
								else
								{
									this.tiles[m + l * this.tileXCount] = null;
								}
							}
						}
					}
				}
			}
			RecastGraph.NavmeshTile navmeshTile3 = new RecastGraph.NavmeshTile();
			navmeshTile3.x = x;
			navmeshTile3.z = z;
			navmeshTile3.w = w;
			navmeshTile3.d = d;
			navmeshTile3.tris = tris;
			navmeshTile3.verts = verts;
			navmeshTile3.bbTree = new BBTree(navmeshTile3);
			if (navmeshTile3.tris.Length % 3 != 0)
			{
				throw new ArgumentException("Triangle array's length must be a multiple of 3 (tris)");
			}
			if (navmeshTile3.verts.Length > 65535)
			{
				throw new ArgumentException("Too many vertices per tile (more than 65535)");
			}
			if (!worldSpace)
			{
				if (!Mathf.Approximately((float)(x * this.tileSizeX) * this.cellSize * 1000f, (float)Math.Round((double)((float)(x * this.tileSizeX) * this.cellSize * 1000f))))
				{
					UnityEngine.Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
				}
				if (!Mathf.Approximately((float)(z * this.tileSizeZ) * this.cellSize * 1000f, (float)Math.Round((double)((float)(z * this.tileSizeZ) * this.cellSize * 1000f))))
				{
					UnityEngine.Debug.LogWarning("Possible numerical imprecision. Consider adjusting tileSize and/or cellSize");
				}
				Int3 rhs = (Int3)(new Vector3((float)(x * this.tileSizeX) * this.cellSize, 0f, (float)(z * this.tileSizeZ) * this.cellSize) + this.forcedBounds.min);
				for (int n = 0; n < verts.Length; n++)
				{
					verts[n] += rhs;
				}
			}
			TriangleMeshNode[] array = new TriangleMeshNode[navmeshTile3.tris.Length / 3];
			navmeshTile3.nodes = array;
			int graphIndex = AstarPath.active.astarData.graphs.Length;
			TriangleMeshNode.SetNavmeshHolder(graphIndex, navmeshTile3);
			int num = x + z * this.tileXCount;
			num <<= 12;
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				TriangleMeshNode triangleMeshNode = new TriangleMeshNode(this.active);
				array[num2] = triangleMeshNode;
				triangleMeshNode.GraphIndex = (uint)graphIndex;
				triangleMeshNode.v0 = (navmeshTile3.tris[num2 * 3] | num);
				triangleMeshNode.v1 = (navmeshTile3.tris[num2 * 3 + 1] | num);
				triangleMeshNode.v2 = (navmeshTile3.tris[num2 * 3 + 2] | num);
				if (!Polygon.IsClockwise(triangleMeshNode.GetVertex(0), triangleMeshNode.GetVertex(1), triangleMeshNode.GetVertex(2)))
				{
					int v = triangleMeshNode.v0;
					triangleMeshNode.v0 = triangleMeshNode.v2;
					triangleMeshNode.v2 = v;
				}
				triangleMeshNode.Walkable = true;
				triangleMeshNode.Penalty = this.initialPenalty;
				triangleMeshNode.UpdatePositionFromVertices();
				navmeshTile3.bbTree.Insert(triangleMeshNode);
			}
			this.CreateNodeConnections(navmeshTile3.nodes);
			for (int num3 = z; num3 < z + d; num3++)
			{
				for (int num4 = x; num4 < x + w; num4++)
				{
					this.tiles[num4 + num3 * this.tileXCount] = navmeshTile3;
				}
			}
			if (this.batchTileUpdate)
			{
				this.batchUpdatedTiles.Add(x + z * this.tileXCount);
			}
			else
			{
				this.ConnectTileWithNeighbours(navmeshTile3);
			}
			TriangleMeshNode.SetNavmeshHolder(graphIndex, null);
			graphIndex = AstarPath.active.astarData.GetGraphIndex(this);
			for (int num5 = 0; num5 < array.Length; num5++)
			{
				array[num5].GraphIndex = (uint)graphIndex;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00031AE6 File Offset: 0x0002FEE6
		protected void ScanCRecast()
		{
			UnityEngine.Debug.LogError("The C++ version of recast can only be used in osx editor or osx standalone mode, I'm sure it cannot be used in the webplayer, but other platforms are not tested yet\nIf you are in the Unity Editor, try switching Platform to OSX Standalone just when scanning, scanned graphs can be cached to enable them to be used in a webplayer.");
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00031AF4 File Offset: 0x0002FEF4
		private void CollectTreeMeshes(List<ExtraMesh> extraMeshes, Terrain terrain)
		{
			TerrainData terrainData = terrain.terrainData;
			for (int i = 0; i < terrainData.treeInstances.Length; i++)
			{
				TreeInstance treeInstance = terrainData.treeInstances[i];
				TreePrototype treePrototype = terrainData.treePrototypes[treeInstance.prototypeIndex];
				if (treePrototype.prefab.GetComponent<Collider>() == null)
				{
					Bounds b = new Bounds(terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size), new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale));
					Matrix4x4 matrix = Matrix4x4.TRS(terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size), Quaternion.identity, new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale) * 0.5f);
					ExtraMesh item = new ExtraMesh(this.BoxColliderVerts, this.BoxColliderTris, b, matrix);
					extraMeshes.Add(item);
				}
				else
				{
					Vector3 pos = terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size);
					Vector3 s = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
					ExtraMesh item2 = this.RasterizeCollider(treePrototype.prefab.GetComponent<Collider>(), Matrix4x4.TRS(pos, Quaternion.identity, s));
					if (item2.vertices != null)
					{
						item2.RecalculateBounds();
						extraMeshes.Add(item2);
					}
				}
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00031C90 File Offset: 0x00030090
		private bool CollectMeshes(out List<ExtraMesh> extraMeshes, Bounds bounds)
		{
			List<ExtraMesh> list = new List<ExtraMesh>();
			if (this.rasterizeMeshes)
			{
				list = this.GetSceneMeshes(bounds);
			}
			this.GetRecastMeshObjs(bounds, list);
			Terrain[] array = UnityEngine.Object.FindObjectsOfType(typeof(Terrain)) as Terrain[];
			if (this.rasterizeTerrain && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					TerrainData terrainData = array[i].terrainData;
					if (!(terrainData == null))
					{
						Vector3 position = array[i].GetPosition();
						Vector3 center = position + terrainData.size * 0.5f;
						Bounds b = new Bounds(center, terrainData.size);
						if (b.Intersects(bounds))
						{
							float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
							List<Vector3> list2 = new List<Vector3>();
							List<int> list3 = new List<int>();
							Vector3 heightmapScale = terrainData.heightmapScale;
							float y = terrainData.size.y;
							for (int j = 0; j < terrainData.heightmapHeight - 1; j++)
							{
								for (int k = 0; k < terrainData.heightmapWidth - 1; k++)
								{
									Vector3 vector = new Vector3((float)j * heightmapScale.z, heights[k, j] * y, (float)k * heightmapScale.x) + position;
									if (!LandscapeHoleUtility.isPointInsideHoleVolume(vector))
									{
										Vector3 vector2 = new Vector3((float)(j + 1) * heightmapScale.z, heights[k, j + 1] * y, (float)k * heightmapScale.x) + position;
										if (!LandscapeHoleUtility.isPointInsideHoleVolume(vector2))
										{
											Vector3 vector3 = new Vector3((float)j * heightmapScale.z, heights[k + 1, j] * y, (float)(k + 1) * heightmapScale.x) + position;
											if (!LandscapeHoleUtility.isPointInsideHoleVolume(vector3))
											{
												Vector3 vector4 = new Vector3((float)(j + 1) * heightmapScale.z, heights[k + 1, j + 1] * y, (float)(k + 1) * heightmapScale.x) + position;
												if (!LandscapeHoleUtility.isPointInsideHoleVolume(vector4))
												{
													int count = list2.Count;
													list2.Add(vector);
													list2.Add(vector2);
													list2.Add(vector3);
													list2.Add(vector4);
													list3.Add(count);
													list3.Add(count + 2);
													list3.Add(count + 1);
													list3.Add(count + 3);
													list3.Add(count + 1);
													list3.Add(count + 2);
												}
											}
										}
									}
								}
							}
							list.Add(new ExtraMesh(list2.ToArray(), list3.ToArray(), b));
							if (this.rasterizeTrees)
							{
								this.CollectTreeMeshes(list, array[i]);
							}
						}
					}
				}
			}
			if (this.rasterizeColliders)
			{
				Collider[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(Collider)) as Collider[];
				foreach (Collider collider in array2)
				{
					if ((1 << collider.gameObject.layer & this.mask) == 1 << collider.gameObject.layer && collider.enabled && !collider.isTrigger && collider.bounds.Intersects(bounds))
					{
						ExtraMesh item = this.RasterizeCollider(collider);
						if (item.vertices != null)
						{
							list.Add(item);
						}
					}
				}
				this.capsuleCache.Clear();
			}
			extraMeshes = list;
			if (list.Count == 0)
			{
				UnityEngine.Debug.LogWarning("No MeshFilters where found contained in the layers specified by the 'mask' variable");
				return false;
			}
			return true;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00032073 File Offset: 0x00030473
		private ExtraMesh RasterizeCollider(Collider col)
		{
			return this.RasterizeCollider(col, col.transform.localToWorldMatrix);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00032088 File Offset: 0x00030488
		private ExtraMesh RasterizeCollider(Collider col, Matrix4x4 localToWorldMatrix)
		{
			if (col is BoxCollider)
			{
				BoxCollider boxCollider = col as BoxCollider;
				Matrix4x4 matrix4x = Matrix4x4.TRS(boxCollider.center, Quaternion.identity, boxCollider.size * 0.5f);
				matrix4x = localToWorldMatrix * matrix4x;
				Bounds bounds = boxCollider.bounds;
				ExtraMesh result = new ExtraMesh(this.BoxColliderVerts, this.BoxColliderTris, bounds, matrix4x);
				return result;
			}
			if (col is SphereCollider || col is CapsuleCollider)
			{
				SphereCollider sphereCollider = col as SphereCollider;
				CapsuleCollider capsuleCollider = col as CapsuleCollider;
				float num = (!(sphereCollider != null)) ? capsuleCollider.radius : sphereCollider.radius;
				float num2 = (!(sphereCollider != null)) ? (capsuleCollider.height * 0.5f / num - 1f) : 0f;
				Matrix4x4 matrix4x2 = Matrix4x4.TRS((!(sphereCollider != null)) ? capsuleCollider.center : sphereCollider.center, Quaternion.identity, Vector3.one * num);
				matrix4x2 = localToWorldMatrix * matrix4x2;
				int num3 = Mathf.Max(4, Mathf.RoundToInt(this.colliderRasterizeDetail * Mathf.Sqrt(matrix4x2.MultiplyVector(Vector3.one).magnitude)));
				if (num3 > 100)
				{
					UnityEngine.Debug.LogWarning("Very large detail for some collider meshes. Consider decreasing Collider Rasterize Detail (RecastGraph)");
				}
				int num4 = num3;
				RecastGraph.CapsuleCache capsuleCache = null;
				for (int i = 0; i < this.capsuleCache.Count; i++)
				{
					RecastGraph.CapsuleCache capsuleCache2 = this.capsuleCache[i];
					if (capsuleCache2.rows == num3 && Mathf.Approximately(capsuleCache2.height, num2))
					{
						capsuleCache = capsuleCache2;
					}
				}
				Vector3[] array;
				if (capsuleCache == null)
				{
					array = new Vector3[num3 * num4 + 2];
					List<int> list = new List<int>();
					array[array.Length - 1] = Vector3.up;
					for (int j = 0; j < num3; j++)
					{
						for (int k = 0; k < num4; k++)
						{
							array[k + j * num4] = new Vector3(Mathf.Cos((float)k * 3.14159274f * 2f / (float)num4) * Mathf.Sin((float)j * 3.14159274f / (float)(num3 - 1)), Mathf.Cos((float)j * 3.14159274f / (float)(num3 - 1)) + ((j >= num3 / 2) ? (-num2) : num2), Mathf.Sin((float)k * 3.14159274f * 2f / (float)num4) * Mathf.Sin((float)j * 3.14159274f / (float)(num3 - 1)));
						}
					}
					array[array.Length - 2] = Vector3.down;
					int l = 0;
					int num5 = num4 - 1;
					while (l < num4)
					{
						list.Add(array.Length - 1);
						list.Add(0 * num4 + num5);
						list.Add(0 * num4 + l);
						num5 = l++;
					}
					for (int m = 1; m < num3; m++)
					{
						int n = 0;
						int num6 = num4 - 1;
						while (n < num4)
						{
							list.Add(m * num4 + n);
							list.Add(m * num4 + num6);
							list.Add((m - 1) * num4 + n);
							list.Add((m - 1) * num4 + num6);
							list.Add((m - 1) * num4 + n);
							list.Add(m * num4 + num6);
							num6 = n++;
						}
					}
					int num7 = 0;
					int num8 = num4 - 1;
					while (num7 < num4)
					{
						list.Add(array.Length - 2);
						list.Add((num3 - 1) * num4 + num8);
						list.Add((num3 - 1) * num4 + num7);
						num8 = num7++;
					}
					capsuleCache = new RecastGraph.CapsuleCache();
					capsuleCache.rows = num3;
					capsuleCache.height = num2;
					capsuleCache.verts = array;
					capsuleCache.tris = list.ToArray();
					this.capsuleCache.Add(capsuleCache);
				}
				array = capsuleCache.verts;
				int[] tris = capsuleCache.tris;
				Bounds bounds2 = col.bounds;
				ExtraMesh result2 = new ExtraMesh(array, tris, bounds2, matrix4x2);
				return result2;
			}
			if (col is MeshCollider)
			{
				MeshCollider meshCollider = col as MeshCollider;
				if (meshCollider.sharedMesh != null)
				{
					ExtraMesh result3 = new ExtraMesh(meshCollider.sharedMesh.vertices, meshCollider.sharedMesh.triangles, meshCollider.bounds, localToWorldMatrix);
					return result3;
				}
			}
			return default(ExtraMesh);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0003254C File Offset: 0x0003094C
		public bool Linecast(Vector3 origin, Vector3 end)
		{
			return this.Linecast(origin, end, base.GetNearest(origin, NNConstraint.None).node);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00032575 File Offset: 0x00030975
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint, out GraphHitInfo hit)
		{
			return NavMeshGraph.Linecast(this, origin, end, hint, out hit, null);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00032584 File Offset: 0x00030984
		public bool Linecast(Vector3 origin, Vector3 end, GraphNode hint)
		{
			GraphHitInfo graphHitInfo;
			return NavMeshGraph.Linecast(this, origin, end, hint, out graphHitInfo, null);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0003259D File Offset: 0x0003099D
		public bool Linecast(Vector3 tmp_origin, Vector3 tmp_end, GraphNode hint, out GraphHitInfo hit, List<GraphNode> trace)
		{
			return NavMeshGraph.Linecast(this, tmp_origin, tmp_end, hint, out hit, trace);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000325AC File Offset: 0x000309AC
		public override void OnDrawGizmos(bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			if (this.bbTree != null)
			{
				this.bbTree.OnDrawGizmos();
			}
			Gizmos.DrawWireCube(this.forcedBounds.center, this.forcedBounds.size);
			PathHandler debugData = AstarPath.active.debugPathData;
			GraphNodeDelegateCancelable del = delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				if (AstarPath.active.showSearchTree && debugData != null)
				{
					bool flag = this.InSearchTree(triangleMeshNode, AstarPath.active.debugPath);
					if (flag && this.showNodeConnections)
					{
						PathNode pathNode = debugData.GetPathNode(triangleMeshNode);
						if (pathNode.parent != null)
						{
							Gizmos.color = this.NodeColor(triangleMeshNode, debugData);
							Gizmos.DrawLine((Vector3)triangleMeshNode.position, (Vector3)debugData.GetPathNode(triangleMeshNode).parent.node.position);
						}
					}
					if (this.showMeshOutline)
					{
						Gizmos.color = this.NodeColor(triangleMeshNode, debugData);
						if (!flag)
						{
							Gizmos.color *= new Color(1f, 1f, 1f, 0.1f);
						}
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(0), (Vector3)triangleMeshNode.GetVertex(1));
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(1), (Vector3)triangleMeshNode.GetVertex(2));
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(2), (Vector3)triangleMeshNode.GetVertex(0));
					}
				}
				else
				{
					if (this.showNodeConnections)
					{
						Gizmos.color = this.NodeColor(triangleMeshNode, null);
						for (int i = 0; i < triangleMeshNode.connections.Length; i++)
						{
							Gizmos.DrawLine((Vector3)triangleMeshNode.position, Vector3.Lerp((Vector3)triangleMeshNode.connections[i].position, (Vector3)triangleMeshNode.position, 0.4f));
						}
					}
					if (this.showMeshOutline)
					{
						Gizmos.color = this.NodeColor(triangleMeshNode, debugData);
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(0), (Vector3)triangleMeshNode.GetVertex(1));
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(1), (Vector3)triangleMeshNode.GetVertex(2));
						Gizmos.DrawLine((Vector3)triangleMeshNode.GetVertex(2), (Vector3)triangleMeshNode.GetVertex(0));
					}
				}
				return true;
			};
			this.GetNodes(del);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00032628 File Offset: 0x00030A28
		public override void SerializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryWriter writer = ctx.writer;
			if (this.tiles == null)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(this.tileXCount);
			writer.Write(this.tileZCount);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					RecastGraph.NavmeshTile navmeshTile = this.tiles[j + i * this.tileXCount];
					if (navmeshTile == null)
					{
						throw new Exception("NULL Tile");
					}
					writer.Write(navmeshTile.x);
					writer.Write(navmeshTile.z);
					UnityEngine.Debug.Log(string.Concat(new object[]
					{
						navmeshTile.x,
						" ",
						navmeshTile.z,
						" ",
						j,
						" ",
						i
					}));
					if (navmeshTile.x == j && navmeshTile.z == i)
					{
						writer.Write(navmeshTile.w);
						writer.Write(navmeshTile.d);
						writer.Write(navmeshTile.tris.Length);
						UnityEngine.Debug.Log("Tris saved " + navmeshTile.tris.Length);
						for (int k = 0; k < navmeshTile.tris.Length; k++)
						{
							writer.Write(navmeshTile.tris[k]);
						}
						writer.Write(navmeshTile.verts.Length);
						for (int l = 0; l < navmeshTile.verts.Length; l++)
						{
							writer.Write(navmeshTile.verts[l].x);
							writer.Write(navmeshTile.verts[l].y);
							writer.Write(navmeshTile.verts[l].z);
						}
						writer.Write(navmeshTile.nodes.Length);
						for (int m = 0; m < navmeshTile.nodes.Length; m++)
						{
							navmeshTile.nodes[m].SerializeNode(ctx);
						}
					}
				}
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00032860 File Offset: 0x00030C60
		public override void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
			BinaryReader reader = ctx.reader;
			this.tileXCount = reader.ReadInt32();
			if (this.tileXCount < 0)
			{
				return;
			}
			this.tileZCount = reader.ReadInt32();
			this.tiles = new RecastGraph.NavmeshTile[this.tileXCount * this.tileZCount];
			TriangleMeshNode.SetNavmeshHolder(ctx.graphIndex, this);
			for (int i = 0; i < this.tileZCount; i++)
			{
				for (int j = 0; j < this.tileXCount; j++)
				{
					int num = j + i * this.tileXCount;
					int num2 = reader.ReadInt32();
					if (num2 < 0)
					{
						throw new Exception("Invalid tile coordinates (x < 0)");
					}
					int num3 = reader.ReadInt32();
					if (num3 < 0)
					{
						throw new Exception("Invalid tile coordinates (z < 0)");
					}
					if (num2 != j || num3 != i)
					{
						this.tiles[num] = this.tiles[num3 * this.tileXCount + num2];
					}
					else
					{
						RecastGraph.NavmeshTile navmeshTile = new RecastGraph.NavmeshTile();
						navmeshTile.x = num2;
						navmeshTile.z = num3;
						navmeshTile.w = reader.ReadInt32();
						navmeshTile.d = reader.ReadInt32();
						navmeshTile.bbTree = new BBTree(navmeshTile);
						this.tiles[num] = navmeshTile;
						int num4 = reader.ReadInt32();
						if (num4 % 3 != 0)
						{
							throw new Exception("Corrupt data. Triangle indices count must be divisable by 3. Got " + num4);
						}
						navmeshTile.tris = new int[num4];
						for (int k = 0; k < navmeshTile.tris.Length; k++)
						{
							navmeshTile.tris[k] = reader.ReadInt32();
						}
						navmeshTile.verts = new Int3[reader.ReadInt32()];
						for (int l = 0; l < navmeshTile.verts.Length; l++)
						{
							navmeshTile.verts[l] = new Int3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
						}
						int num5 = reader.ReadInt32();
						navmeshTile.nodes = new TriangleMeshNode[num5];
						num <<= 12;
						for (int m = 0; m < navmeshTile.nodes.Length; m++)
						{
							TriangleMeshNode triangleMeshNode = new TriangleMeshNode(this.active);
							navmeshTile.nodes[m] = triangleMeshNode;
							triangleMeshNode.GraphIndex = (uint)ctx.graphIndex;
							triangleMeshNode.DeserializeNode(ctx);
							triangleMeshNode.v0 = (navmeshTile.tris[m * 3] | num);
							triangleMeshNode.v1 = (navmeshTile.tris[m * 3 + 1] | num);
							triangleMeshNode.v2 = (navmeshTile.tris[m * 3 + 2] | num);
							triangleMeshNode.UpdatePositionFromVertices();
							navmeshTile.bbTree.Insert(triangleMeshNode);
						}
					}
				}
			}
		}

		// Token: 0x0400044F RID: 1103
		public bool dynamic = true;

		// Token: 0x04000450 RID: 1104
		[JsonMember]
		public float characterRadius = 0.5f;

		// Token: 0x04000451 RID: 1105
		[JsonMember]
		public float contourMaxError = 2f;

		// Token: 0x04000452 RID: 1106
		[JsonMember]
		public float cellSize = 0.5f;

		// Token: 0x04000453 RID: 1107
		[JsonMember]
		public float cellHeight = 0.4f;

		// Token: 0x04000454 RID: 1108
		[JsonMember]
		public float walkableHeight = 2f;

		// Token: 0x04000455 RID: 1109
		[JsonMember]
		public float walkableClimb = 0.5f;

		// Token: 0x04000456 RID: 1110
		[JsonMember]
		public float maxSlope = 30f;

		// Token: 0x04000457 RID: 1111
		[JsonMember]
		public float maxEdgeLength = 20f;

		// Token: 0x04000458 RID: 1112
		[JsonMember]
		public float minRegionSize = 3f;

		// Token: 0x04000459 RID: 1113
		[JsonMember]
		public int editorTileSize = 128;

		// Token: 0x0400045A RID: 1114
		[JsonMember]
		public int tileSizeX = 128;

		// Token: 0x0400045B RID: 1115
		[JsonMember]
		public int tileSizeZ = 128;

		// Token: 0x0400045C RID: 1116
		[JsonMember]
		public bool nearestSearchOnlyXZ;

		// Token: 0x0400045D RID: 1117
		[JsonMember]
		public bool useTiles;

		// Token: 0x0400045E RID: 1118
		public bool scanEmptyGraph;

		// Token: 0x0400045F RID: 1119
		[JsonMember]
		public RecastGraph.RelevantGraphSurfaceMode relevantGraphSurfaceMode;

		// Token: 0x04000460 RID: 1120
		[JsonMember]
		public bool rasterizeColliders;

		// Token: 0x04000461 RID: 1121
		[JsonMember]
		public bool rasterizeMeshes = true;

		// Token: 0x04000462 RID: 1122
		[JsonMember]
		public bool rasterizeTerrain = true;

		// Token: 0x04000463 RID: 1123
		[JsonMember]
		public bool rasterizeTrees = true;

		// Token: 0x04000464 RID: 1124
		[JsonMember]
		public float colliderRasterizeDetail = 10f;

		// Token: 0x04000465 RID: 1125
		[JsonMember]
		public Vector3 forcedBoundsCenter;

		// Token: 0x04000466 RID: 1126
		[JsonMember]
		public Vector3 forcedBoundsSize = new Vector3(100f, 40f, 100f);

		// Token: 0x04000467 RID: 1127
		[JsonMember]
		public LayerMask mask = -1;

		// Token: 0x04000468 RID: 1128
		[JsonMember]
		public List<string> tagMask = new List<string>();

		// Token: 0x04000469 RID: 1129
		[JsonMember]
		public bool showMeshOutline = true;

		// Token: 0x0400046A RID: 1130
		[JsonMember]
		public bool showNodeConnections;

		// Token: 0x0400046B RID: 1131
		[JsonMember]
		public int terrainSampleSize = 3;

		// Token: 0x0400046C RID: 1132
		private Voxelize globalVox;

		// Token: 0x0400046D RID: 1133
		private BBTree _bbTree;

		// Token: 0x0400046E RID: 1134
		private Int3[] _vertices;

		// Token: 0x0400046F RID: 1135
		private Vector3[] _vectorVertices;

		// Token: 0x04000470 RID: 1136
		public int tileXCount;

		// Token: 0x04000471 RID: 1137
		public int tileZCount;

		// Token: 0x04000472 RID: 1138
		private RecastGraph.NavmeshTile[] tiles;

		// Token: 0x04000473 RID: 1139
		private bool batchTileUpdate;

		// Token: 0x04000474 RID: 1140
		private List<int> batchUpdatedTiles = new List<int>();

		// Token: 0x04000475 RID: 1141
		public const int VertexIndexMask = 4095;

		// Token: 0x04000476 RID: 1142
		public const int TileIndexMask = 524287;

		// Token: 0x04000477 RID: 1143
		public const int TileIndexOffset = 12;

		// Token: 0x04000478 RID: 1144
		public const int BorderVertexMask = 1;

		// Token: 0x04000479 RID: 1145
		public const int BorderVertexOffset = 31;

		// Token: 0x0400047A RID: 1146
		private Dictionary<Int2, int> cachedInt2_int_dict = new Dictionary<Int2, int>();

		// Token: 0x0400047B RID: 1147
		private Dictionary<Int3, int> cachedInt3_int_dict = new Dictionary<Int3, int>();

		// Token: 0x0400047C RID: 1148
		private readonly int[] BoxColliderTris = new int[]
		{
			0,
			1,
			2,
			0,
			2,
			3,
			6,
			5,
			4,
			7,
			6,
			4,
			0,
			5,
			1,
			0,
			4,
			5,
			1,
			6,
			2,
			1,
			5,
			6,
			2,
			7,
			3,
			2,
			6,
			7,
			3,
			4,
			0,
			3,
			7,
			4
		};

		// Token: 0x0400047D RID: 1149
		private readonly Vector3[] BoxColliderVerts = new Vector3[]
		{
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f)
		};

		// Token: 0x0400047E RID: 1150
		private List<RecastGraph.CapsuleCache> capsuleCache = new List<RecastGraph.CapsuleCache>();

		// Token: 0x0200009A RID: 154
		public enum RelevantGraphSurfaceMode
		{
			// Token: 0x04000480 RID: 1152
			DoNotRequire,
			// Token: 0x04000481 RID: 1153
			OnlyForCompletelyInsideTile,
			// Token: 0x04000482 RID: 1154
			RequireForAll
		}

		// Token: 0x0200009B RID: 155
		public class NavmeshTile : INavmeshHolder, INavmesh
		{
			// Token: 0x06000589 RID: 1417 RVA: 0x00032B2C File Offset: 0x00030F2C
			public void GetTileCoordinates(int tileIndex, out int x, out int z)
			{
				x = this.x;
				z = this.z;
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x00032B3E File Offset: 0x00030F3E
			public int GetVertexArrayIndex(int index)
			{
				return index & 4095;
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00032B48 File Offset: 0x00030F48
			public Int3 GetVertex(int index)
			{
				int num = index & 4095;
				return this.verts[num];
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x00032B70 File Offset: 0x00030F70
			public void GetNodes(GraphNodeDelegateCancelable del)
			{
				if (this.nodes == null)
				{
					return;
				}
				int num = 0;
				while (num < this.nodes.Length && del(this.nodes[num]))
				{
					num++;
				}
			}

			// Token: 0x04000483 RID: 1155
			public int[] tris;

			// Token: 0x04000484 RID: 1156
			public Int3[] verts;

			// Token: 0x04000485 RID: 1157
			public int x;

			// Token: 0x04000486 RID: 1158
			public int z;

			// Token: 0x04000487 RID: 1159
			public int w;

			// Token: 0x04000488 RID: 1160
			public int d;

			// Token: 0x04000489 RID: 1161
			public TriangleMeshNode[] nodes;

			// Token: 0x0400048A RID: 1162
			public BBTree bbTree;

			// Token: 0x0400048B RID: 1163
			public bool flag;
		}

		// Token: 0x0200009C RID: 156
		public struct SceneMesh
		{
			// Token: 0x0400048C RID: 1164
			public Mesh mesh;

			// Token: 0x0400048D RID: 1165
			public Matrix4x4 matrix;

			// Token: 0x0400048E RID: 1166
			public Bounds bounds;
		}

		// Token: 0x0200009D RID: 157
		private class CapsuleCache
		{
			// Token: 0x0400048F RID: 1167
			public int rows;

			// Token: 0x04000490 RID: 1168
			public float height;

			// Token: 0x04000491 RID: 1169
			public Vector3[] verts;

			// Token: 0x04000492 RID: 1170
			public int[] tris;
		}
	}
}
