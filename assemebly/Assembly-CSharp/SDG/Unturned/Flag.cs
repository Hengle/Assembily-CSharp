using System;
using System.Collections.Generic;
using Pathfinding;
using SDG.Framework.Landscapes;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200052E RID: 1326
	public class Flag
	{
		// Token: 0x060023BB RID: 9147 RVA: 0x000C63D4 File Offset: 0x000C47D4
		public Flag(Vector3 newPoint, RecastGraph newGraph, FlagData newData)
		{
			this._point = newPoint;
			this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Flag"))).transform;
			this.model.name = "Flag";
			this.model.position = this.point;
			this.model.parent = LevelNavigation.models;
			this._area = this.model.FindChild("Area").GetComponent<LineRenderer>();
			this._bounds = this.model.FindChild("Bounds").GetComponent<LineRenderer>();
			this.navmesh = this.model.FindChild("Navmesh").GetComponent<MeshFilter>();
			this.width = 0f;
			this.height = 0f;
			this._graph = newGraph;
			this.data = newData;
			this.setupGraph();
			this.buildMesh();
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000C64C0 File Offset: 0x000C48C0
		public Flag(Vector3 newPoint, float newWidth, float newHeight, RecastGraph newGraph, FlagData newData)
		{
			this._point = newPoint;
			this._model = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Flag"))).transform;
			this.model.name = "Flag";
			this.model.position = this.point;
			this.model.parent = LevelNavigation.models;
			this._area = this.model.FindChild("Area").GetComponent<LineRenderer>();
			this._bounds = this.model.FindChild("Bounds").GetComponent<LineRenderer>();
			this.navmesh = this.model.FindChild("Navmesh").GetComponent<MeshFilter>();
			this.width = newWidth;
			this.height = newHeight;
			this._graph = newGraph;
			this.data = newData;
			this.setupGraph();
			this.buildMesh();
			this.updateNavmesh();
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000C65AB File Offset: 0x000C49AB
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000C65B3 File Offset: 0x000C49B3
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x000C65BB File Offset: 0x000C49BB
		public LineRenderer area
		{
			get
			{
				return this._area;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x000C65C3 File Offset: 0x000C49C3
		public LineRenderer bounds
		{
			get
			{
				return this._bounds;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000C65CB File Offset: 0x000C49CB
		public RecastGraph graph
		{
			get
			{
				return this._graph;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000C65D3 File Offset: 0x000C49D3
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x000C65DB File Offset: 0x000C49DB
		public FlagData data { get; private set; }

		// Token: 0x060023C4 RID: 9156 RVA: 0x000C65E4 File Offset: 0x000C49E4
		public void move(Vector3 newPoint)
		{
			this._point = newPoint;
			this.model.position = this.point;
			this.navmesh.transform.position = Vector3.zero;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000C6613 File Offset: 0x000C4A13
		public void setEnabled(bool isEnabled)
		{
			this.model.gameObject.SetActive(isEnabled);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000C6628 File Offset: 0x000C4A28
		public void buildMesh()
		{
			float num = Flag.MIN_SIZE + this.width * (Flag.MAX_SIZE - Flag.MIN_SIZE);
			float num2 = Flag.MIN_SIZE + this.height * (Flag.MAX_SIZE - Flag.MIN_SIZE);
			this.area.SetPosition(0, new Vector3(-num / 2f, 0f, -num2 / 2f));
			this.area.SetPosition(1, new Vector3(num / 2f, 0f, -num2 / 2f));
			this.area.SetPosition(2, new Vector3(num / 2f, 0f, num2 / 2f));
			this.area.SetPosition(3, new Vector3(-num / 2f, 0f, num2 / 2f));
			this.area.SetPosition(4, new Vector3(-num / 2f, 0f, -num2 / 2f));
			num += LevelNavigation.BOUNDS_SIZE.x;
			num2 += LevelNavigation.BOUNDS_SIZE.z;
			this.bounds.SetPosition(0, new Vector3(-num / 2f, 0f, -num2 / 2f));
			this.bounds.SetPosition(1, new Vector3(num / 2f, 0f, -num2 / 2f));
			this.bounds.SetPosition(2, new Vector3(num / 2f, 0f, num2 / 2f));
			this.bounds.SetPosition(3, new Vector3(-num / 2f, 0f, num2 / 2f));
			this.bounds.SetPosition(4, new Vector3(-num / 2f, 0f, -num2 / 2f));
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000C67FB File Offset: 0x000C4BFB
		public void remove()
		{
			AstarPath.active.astarData.RemoveGraph(this.graph);
			UnityEngine.Object.Destroy(this.model.gameObject);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000C6824 File Offset: 0x000C4C24
		public void bakeNavigation()
		{
			float x = Flag.MIN_SIZE + this.width * (Flag.MAX_SIZE - Flag.MIN_SIZE);
			float z = Flag.MIN_SIZE + this.height * (Flag.MAX_SIZE - Flag.MIN_SIZE);
			if (!Level.info.configData.Use_Legacy_Ground)
			{
				this.graph.forcedBoundsCenter = new Vector3(this.point.x, 0f, this.point.z);
				this.graph.forcedBoundsSize = new Vector3(x, Landscape.TILE_HEIGHT, z);
			}
			else if (Level.info.configData.Use_Legacy_Water && LevelLighting.seaLevel < 0.99f && !Level.info.configData.Allow_Underwater_Features)
			{
				this.graph.forcedBoundsCenter = new Vector3(this.point.x, LevelLighting.seaLevel * Level.TERRAIN + (Level.TERRAIN - LevelLighting.seaLevel * Level.TERRAIN) / 2f - 0.625f, this.point.z);
				this.graph.forcedBoundsSize = new Vector3(x, Level.TERRAIN - LevelLighting.seaLevel * Level.TERRAIN + 1.25f, z);
			}
			else
			{
				this.graph.forcedBoundsCenter = new Vector3(this.point.x, Level.TERRAIN / 2f, this.point.z);
				this.graph.forcedBoundsSize = new Vector3(x, Level.TERRAIN, z);
			}
			if (LevelGround.models2 != null)
			{
				LevelGround.models2.gameObject.SetActive(false);
			}
			AstarPath.active.ScanSpecific(this.graph);
			LevelNavigation.updateBounds();
			if (LevelGround.models2 != null)
			{
				LevelGround.models2.gameObject.SetActive(true);
			}
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000C6A28 File Offset: 0x000C4E28
		private void updateNavmesh()
		{
			if (Level.isEditor && this.graph != null)
			{
				List<Vector3> list = new List<Vector3>();
				List<int> list2 = new List<int>();
				List<Vector2> list3 = new List<Vector2>();
				RecastGraph.NavmeshTile[] tiles = this.graph.GetTiles();
				int num = 0;
				if (tiles == null)
				{
					return;
				}
				foreach (RecastGraph.NavmeshTile navmeshTile in tiles)
				{
					for (int j = 0; j < navmeshTile.verts.Length; j++)
					{
						Vector3 item = (Vector3)navmeshTile.verts[j];
						item.y += 0.1f;
						list.Add(item);
						list3.Add(new Vector2(item.x, item.z));
					}
					for (int k = 0; k < navmeshTile.tris.Length; k++)
					{
						list2.Add(navmeshTile.tris[k] + num);
					}
					num += navmeshTile.verts.Length;
				}
				Mesh mesh = new Mesh();
				mesh.name = "Navmesh";
				mesh.vertices = list.ToArray();
				mesh.triangles = list2.ToArray();
				mesh.normals = new Vector3[list.Count];
				mesh.uv = list3.ToArray();
				this.navmesh.transform.position = Vector3.zero;
				this.navmesh.sharedMesh = mesh;
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000C6BA9 File Offset: 0x000C4FA9
		private void OnGraphPostScan(NavGraph updated)
		{
			if (updated == this.graph)
			{
				this.needsNavigationSave = true;
				this.updateNavmesh();
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000C6BC4 File Offset: 0x000C4FC4
		private void setupGraph()
		{
			AstarPath.OnGraphPostScan = (OnGraphDelegate)Delegate.Combine(AstarPath.OnGraphPostScan, new OnGraphDelegate(this.OnGraphPostScan));
		}

		// Token: 0x040015D1 RID: 5585
		public static readonly float MIN_SIZE = 32f;

		// Token: 0x040015D2 RID: 5586
		public static readonly float MAX_SIZE = 1024f;

		// Token: 0x040015D3 RID: 5587
		public float width;

		// Token: 0x040015D4 RID: 5588
		public float height;

		// Token: 0x040015D5 RID: 5589
		private Vector3 _point;

		// Token: 0x040015D6 RID: 5590
		private Transform _model;

		// Token: 0x040015D7 RID: 5591
		private MeshFilter navmesh;

		// Token: 0x040015D8 RID: 5592
		private LineRenderer _area;

		// Token: 0x040015D9 RID: 5593
		private LineRenderer _bounds;

		// Token: 0x040015DA RID: 5594
		private RecastGraph _graph;

		// Token: 0x040015DC RID: 5596
		public bool needsNavigationSave;
	}
}
