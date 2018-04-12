using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000D4 RID: 212
	public class NavmeshAdd : MonoBehaviour
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x00044547 File Offset: 0x00042947
		private static void Add(NavmeshAdd obj)
		{
			NavmeshAdd.allCuts.Add(obj);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00044554 File Offset: 0x00042954
		private static void Remove(NavmeshAdd obj)
		{
			NavmeshAdd.allCuts.Remove(obj);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00044564 File Offset: 0x00042964
		public static List<NavmeshAdd> GetAllInRange(Bounds b)
		{
			List<NavmeshAdd> list = ListPool<NavmeshAdd>.Claim();
			for (int i = 0; i < NavmeshAdd.allCuts.Count; i++)
			{
				if (NavmeshAdd.allCuts[i].enabled && NavmeshAdd.Intersects(b, NavmeshAdd.allCuts[i].GetBounds()))
				{
					list.Add(NavmeshAdd.allCuts[i]);
				}
			}
			return list;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000445D4 File Offset: 0x000429D4
		private static bool Intersects(Bounds b1, Bounds b2)
		{
			Vector3 min = b1.min;
			Vector3 max = b1.max;
			Vector3 min2 = b2.min;
			Vector3 max2 = b2.max;
			return min.x <= max2.x && max.x >= min2.x && min.z <= max2.z && max.z >= min2.z;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00044650 File Offset: 0x00042A50
		public static List<NavmeshAdd> GetAll()
		{
			return NavmeshAdd.allCuts;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00044657 File Offset: 0x00042A57
		public void Awake()
		{
			NavmeshAdd.Add(this);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0004465F File Offset: 0x00042A5F
		public void OnEnable()
		{
			this.tr = base.transform;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0004466D File Offset: 0x00042A6D
		public void OnDestroy()
		{
			NavmeshAdd.Remove(this);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00044675 File Offset: 0x00042A75
		public Vector3 Center
		{
			get
			{
				return this.tr.position + ((!this.useRotation) ? this.center : this.tr.TransformPoint(this.center));
			}
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000446B0 File Offset: 0x00042AB0
		[ContextMenu("Rebuild Mesh")]
		public void RebuildMesh()
		{
			if (this.type == NavmeshAdd.MeshType.CustomMesh)
			{
				if (this.mesh == null)
				{
					this.verts = null;
					this.tris = null;
				}
				else
				{
					this.verts = this.mesh.vertices;
					this.tris = this.mesh.triangles;
				}
			}
			else
			{
				if (this.verts == null || this.verts.Length != 4 || this.tris == null || this.tris.Length != 6)
				{
					this.verts = new Vector3[4];
					this.tris = new int[6];
				}
				this.tris[0] = 0;
				this.tris[1] = 1;
				this.tris[2] = 2;
				this.tris[3] = 0;
				this.tris[4] = 2;
				this.tris[5] = 3;
				this.verts[0] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[1] = new Vector3(this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[2] = new Vector3(this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				this.verts[3] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0004488C File Offset: 0x00042C8C
		public Bounds GetBounds()
		{
			NavmeshAdd.MeshType meshType = this.type;
			if (meshType != NavmeshAdd.MeshType.Rectangle)
			{
				if (meshType == NavmeshAdd.MeshType.CustomMesh)
				{
					if (!(this.mesh == null))
					{
						Bounds bounds = this.mesh.bounds;
						if (this.useRotation)
						{
							Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position, this.tr.rotation, Vector3.one * this.meshScale);
							this.bounds = new Bounds(matrix4x.MultiplyPoint3x4(this.center + bounds.center), Vector3.zero);
							Vector3 max = bounds.max;
							Vector3 min = bounds.min;
							this.bounds.Encapsulate(matrix4x.MultiplyPoint3x4(this.center + new Vector3(max.x, min.y, max.z)));
							this.bounds.Encapsulate(matrix4x.MultiplyPoint3x4(this.center + new Vector3(min.x, min.y, max.z)));
							this.bounds.Encapsulate(matrix4x.MultiplyPoint3x4(this.center + new Vector3(min.x, max.y, min.z)));
							this.bounds.Encapsulate(matrix4x.MultiplyPoint3x4(this.center + new Vector3(max.x, max.y, min.z)));
						}
						else
						{
							Vector3 size = bounds.size * this.meshScale;
							this.bounds = new Bounds(base.transform.position + this.center + bounds.center * this.meshScale, size);
						}
					}
				}
			}
			else if (this.useRotation)
			{
				Matrix4x4 matrix4x2 = Matrix4x4.TRS(this.tr.position, this.tr.rotation, Vector3.one);
				this.bounds = new Bounds(matrix4x2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f), Vector3.zero);
				this.bounds.Encapsulate(matrix4x2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(matrix4x2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(matrix4x2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f));
			}
			else
			{
				this.bounds = new Bounds(this.tr.position + this.center, new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y));
			}
			return this.bounds;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00044C1C File Offset: 0x0004301C
		public void GetMesh(Int3 offset, ref Int3[] vbuffer, out int[] tbuffer)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			if (this.verts == null)
			{
				tbuffer = new int[0];
				return;
			}
			if (vbuffer == null || vbuffer.Length < this.verts.Length)
			{
				vbuffer = new Int3[this.verts.Length];
			}
			tbuffer = this.tris;
			if (this.useRotation)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position + this.center, this.tr.rotation, this.tr.localScale * this.meshScale);
				for (int i = 0; i < this.verts.Length; i++)
				{
					vbuffer[i] = offset + (Int3)matrix4x.MultiplyPoint3x4(this.verts[i]);
				}
			}
			else
			{
				Vector3 a = this.tr.position + this.center;
				for (int j = 0; j < this.verts.Length; j++)
				{
					vbuffer[j] = offset + (Int3)(a + this.verts[j] * this.meshScale);
				}
			}
		}

		// Token: 0x040005DF RID: 1503
		private static List<NavmeshAdd> allCuts = new List<NavmeshAdd>();

		// Token: 0x040005E0 RID: 1504
		public NavmeshAdd.MeshType type;

		// Token: 0x040005E1 RID: 1505
		public Mesh mesh;

		// Token: 0x040005E2 RID: 1506
		private Vector3[] verts;

		// Token: 0x040005E3 RID: 1507
		private int[] tris;

		// Token: 0x040005E4 RID: 1508
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x040005E5 RID: 1509
		public float meshScale = 1f;

		// Token: 0x040005E6 RID: 1510
		public Vector3 center;

		// Token: 0x040005E7 RID: 1511
		private Bounds bounds;

		// Token: 0x040005E8 RID: 1512
		public bool useRotation;

		// Token: 0x040005E9 RID: 1513
		protected Transform tr;

		// Token: 0x040005EA RID: 1514
		public static readonly Color GizmoColor = new Color(0.368627459f, 0.9372549f, 0.145098045f);

		// Token: 0x020000D5 RID: 213
		public enum MeshType
		{
			// Token: 0x040005EC RID: 1516
			Rectangle,
			// Token: 0x040005ED RID: 1517
			CustomMesh
		}
	}
}
