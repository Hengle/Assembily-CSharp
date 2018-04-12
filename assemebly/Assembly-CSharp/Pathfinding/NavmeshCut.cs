using System;
using System.Collections.Generic;
using Pathfinding.ClipperLib;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000D6 RID: 214
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	public class NavmeshCut : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000705 RID: 1797 RVA: 0x00044E14 File Offset: 0x00043214
		// (remove) Token: 0x06000706 RID: 1798 RVA: 0x00044E48 File Offset: 0x00043248
		public static event Action<NavmeshCut> OnDestroyCallback;

		// Token: 0x06000707 RID: 1799 RVA: 0x00044E7C File Offset: 0x0004327C
		private static void AddCut(NavmeshCut obj)
		{
			NavmeshCut.allCuts.Add(obj);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00044E89 File Offset: 0x00043289
		private static void RemoveCut(NavmeshCut obj)
		{
			NavmeshCut.allCuts.Remove(obj);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00044E98 File Offset: 0x00043298
		public static List<NavmeshCut> GetAllInRange(Bounds b)
		{
			List<NavmeshCut> list = ListPool<NavmeshCut>.Claim();
			for (int i = 0; i < NavmeshCut.allCuts.Count; i++)
			{
				if (NavmeshCut.allCuts[i].enabled && NavmeshCut.Intersects(b, NavmeshCut.allCuts[i].GetBounds()))
				{
					list.Add(NavmeshCut.allCuts[i]);
				}
			}
			return list;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00044F08 File Offset: 0x00043308
		private static bool Intersects(Bounds b1, Bounds b2)
		{
			Vector3 min = b1.min;
			Vector3 max = b1.max;
			Vector3 min2 = b2.min;
			Vector3 max2 = b2.max;
			return min.x <= max2.x && max.x >= min2.x && min.y <= max2.y && max.y >= min2.y && min.z <= max2.z && max.z >= min2.z;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00044FAA File Offset: 0x000433AA
		public static List<NavmeshCut> GetAll()
		{
			return NavmeshCut.allCuts;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00044FB1 File Offset: 0x000433B1
		public Bounds LastBounds
		{
			get
			{
				return this.lastBounds;
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00044FB9 File Offset: 0x000433B9
		public void Awake()
		{
			NavmeshCut.AddCut(this);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00044FC1 File Offset: 0x000433C1
		public void OnEnable()
		{
			this.tr = base.transform;
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.lastRotation = this.tr.rotation;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00044FFA File Offset: 0x000433FA
		public void OnDestroy()
		{
			if (NavmeshCut.OnDestroyCallback != null)
			{
				NavmeshCut.OnDestroyCallback(this);
			}
			NavmeshCut.RemoveCut(this);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00045017 File Offset: 0x00043417
		public void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00045034 File Offset: 0x00043434
		public bool RequiresUpdate()
		{
			return this.wasEnabled != base.enabled || (this.wasEnabled && ((this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotation && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance)));
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000450C2 File Offset: 0x000434C2
		public virtual void UsedForCut()
		{
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000450C4 File Offset: 0x000434C4
		public void NotifyUpdated()
		{
			this.wasEnabled = base.enabled;
			if (this.wasEnabled)
			{
				this.lastPosition = this.tr.position;
				this.lastBounds = this.GetBounds();
				if (this.useRotation)
				{
					this.lastRotation = this.tr.rotation;
				}
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00045124 File Offset: 0x00043524
		private void CalculateMeshContour()
		{
			if (this.mesh == null)
			{
				return;
			}
			NavmeshCut.edges.Clear();
			NavmeshCut.pointers.Clear();
			Vector3[] vertices = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (Polygon.IsClockwise(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]))
				{
					int num = triangles[i];
					triangles[i] = triangles[i + 2];
					triangles[i + 2] = num;
				}
				NavmeshCut.edges[new Int2(triangles[i], triangles[i + 1])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 1], triangles[i + 2])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 2], triangles[i])] = i;
			}
			for (int j = 0; j < triangles.Length; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					if (!NavmeshCut.edges.ContainsKey(new Int2(triangles[j + (k + 1) % 3], triangles[j + k % 3])))
					{
						NavmeshCut.pointers[triangles[j + k % 3]] = triangles[j + (k + 1) % 3];
					}
				}
			}
			List<Vector3[]> list = new List<Vector3[]>();
			List<Vector3> list2 = ListPool<Vector3>.Claim();
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					list2.Clear();
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						list2.Add(vertices[num2]);
						num2 = num3;
						if (num2 == -1)
						{
							goto Block_9;
						}
					}
					while (num2 != l);
					IL_20C:
					if (list2.Count > 0)
					{
						list.Add(list2.ToArray());
						goto IL_227;
					}
					goto IL_227;
					Block_9:
					Debug.LogError("Invalid Mesh '" + this.mesh.name + " in " + base.gameObject.name);
					goto IL_20C;
				}
				IL_227:;
			}
			ListPool<Vector3>.Release(list2);
			this.contours = list.ToArray();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0004537C File Offset: 0x0004377C
		public Bounds GetBounds()
		{
			NavmeshCut.MeshType meshType = this.type;
			if (meshType != NavmeshCut.MeshType.Rectangle)
			{
				if (meshType != NavmeshCut.MeshType.Circle)
				{
					if (meshType == NavmeshCut.MeshType.CustomMesh)
					{
						if (!(this.mesh == null))
						{
							Bounds bounds = this.mesh.bounds;
							if (this.useRotation)
							{
								Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
								bounds.center *= this.meshScale;
								bounds.size *= this.meshScale;
								this.bounds = new Bounds(localToWorldMatrix.MultiplyPoint3x4(this.center + bounds.center), Vector3.zero);
								Vector3 max = bounds.max;
								Vector3 min = bounds.min;
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(max.x, max.y, max.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(min.x, max.y, max.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(min.x, max.y, min.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(max.x, max.y, min.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(max.x, min.y, max.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(min.x, min.y, max.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(min.x, min.y, min.z)));
								this.bounds.Encapsulate(localToWorldMatrix.MultiplyPoint3x4(this.center + new Vector3(max.x, min.y, min.z)));
								Vector3 size = this.bounds.size;
								size.y = Mathf.Max(size.y, this.height * this.tr.lossyScale.y);
								this.bounds.size = size;
							}
							else
							{
								Vector3 size2 = bounds.size * this.meshScale;
								size2.y = Mathf.Max(size2.y, this.height);
								this.bounds = new Bounds(base.transform.position + this.center + bounds.center * this.meshScale, size2);
							}
						}
					}
				}
				else if (this.useRotation)
				{
					this.bounds = new Bounds(this.tr.localToWorldMatrix.MultiplyPoint3x4(this.center), new Vector3(this.circleRadius * 2f, this.height, this.circleRadius * 2f));
				}
				else
				{
					this.bounds = new Bounds(base.transform.position + this.center, new Vector3(this.circleRadius * 2f, this.height, this.circleRadius * 2f));
				}
			}
			else if (this.useRotation)
			{
				Matrix4x4 localToWorldMatrix2 = this.tr.localToWorldMatrix;
				this.bounds = new Bounds(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, -this.height, -this.rectangleSize.y) * 0.5f), Vector3.zero);
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, -this.height, -this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, -this.height, this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, -this.height, this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, this.height, -this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, this.height, -this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, this.height, this.rectangleSize.y) * 0.5f));
				this.bounds.Encapsulate(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, this.height, this.rectangleSize.y) * 0.5f));
			}
			else
			{
				this.bounds = new Bounds(this.tr.position + this.center, new Vector3(this.rectangleSize.x, this.height, this.rectangleSize.y));
			}
			return this.bounds;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00045A1C File Offset: 0x00043E1C
		public void GetContour(List<List<IntPoint>> buffer)
		{
			if (this.circleResolution < 3)
			{
				this.circleResolution = 3;
			}
			Vector3 a = this.tr.position;
			NavmeshCut.MeshType meshType = this.type;
			if (meshType != NavmeshCut.MeshType.Rectangle)
			{
				if (meshType != NavmeshCut.MeshType.Circle)
				{
					if (meshType == NavmeshCut.MeshType.CustomMesh)
					{
						if (this.mesh != this.lastMesh || this.contours == null)
						{
							this.CalculateMeshContour();
							this.lastMesh = this.mesh;
						}
						if (this.contours != null)
						{
							a += this.center;
							bool flag = Vector3.Dot(this.tr.up, Vector3.up) < 0f;
							for (int i = 0; i < this.contours.Length; i++)
							{
								Vector3[] array = this.contours[i];
								List<IntPoint> list = ListPool<IntPoint>.Claim(array.Length);
								if (this.useRotation)
								{
									Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
									for (int j = 0; j < array.Length; j++)
									{
										list.Add(this.V3ToIntPoint(localToWorldMatrix.MultiplyPoint3x4(this.center + array[j] * this.meshScale)));
									}
								}
								else
								{
									for (int k = 0; k < array.Length; k++)
									{
										list.Add(this.V3ToIntPoint(a + array[k] * this.meshScale));
									}
								}
								if (flag)
								{
									list.Reverse();
								}
								buffer.Add(list);
							}
						}
					}
				}
				else
				{
					List<IntPoint> list = ListPool<IntPoint>.Claim(this.circleResolution);
					if (this.useRotation)
					{
						Matrix4x4 localToWorldMatrix2 = this.tr.localToWorldMatrix;
						for (int l = 0; l < this.circleResolution; l++)
						{
							list.Add(this.V3ToIntPoint(localToWorldMatrix2.MultiplyPoint3x4(this.center + new Vector3(Mathf.Cos((float)(l * 2) * 3.14159274f / (float)this.circleResolution), 0f, Mathf.Sin((float)(l * 2) * 3.14159274f / (float)this.circleResolution)) * this.circleRadius)));
						}
					}
					else
					{
						a += this.center;
						for (int m = 0; m < this.circleResolution; m++)
						{
							list.Add(this.V3ToIntPoint(a + new Vector3(Mathf.Cos((float)(m * 2) * 3.14159274f / (float)this.circleResolution), 0f, Mathf.Sin((float)(m * 2) * 3.14159274f / (float)this.circleResolution)) * this.circleRadius));
						}
					}
					buffer.Add(list);
				}
			}
			else
			{
				List<IntPoint> list = ListPool<IntPoint>.Claim();
				if (this.useRotation)
				{
					Matrix4x4 localToWorldMatrix3 = this.tr.localToWorldMatrix;
					list.Add(this.V3ToIntPoint(localToWorldMatrix3.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f)));
					list.Add(this.V3ToIntPoint(localToWorldMatrix3.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f)));
					list.Add(this.V3ToIntPoint(localToWorldMatrix3.MultiplyPoint3x4(this.center + new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f)));
					list.Add(this.V3ToIntPoint(localToWorldMatrix3.MultiplyPoint3x4(this.center + new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f)));
				}
				else
				{
					a += this.center;
					list.Add(this.V3ToIntPoint(a + new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f));
					list.Add(this.V3ToIntPoint(a + new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f));
					list.Add(this.V3ToIntPoint(a + new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f));
					list.Add(this.V3ToIntPoint(a + new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f));
				}
				buffer.Add(list);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00045F54 File Offset: 0x00044354
		public IntPoint V3ToIntPoint(Vector3 p)
		{
			Int3 @int = (Int3)p;
			return new IntPoint((long)@int.x, (long)@int.z);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00045F80 File Offset: 0x00044380
		public Vector3 IntPointToV3(IntPoint p)
		{
			Int3 ob = new Int3((int)p.X, 0, (int)p.Y);
			return (Vector3)ob;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00045FAC File Offset: 0x000443AC
		public void OnDrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			List<List<IntPoint>> list = ListPool<List<IntPoint>>.Claim();
			this.GetContour(list);
			Gizmos.color = NavmeshCut.GizmoColor;
			Bounds bounds = this.GetBounds();
			float y = bounds.min.y;
			Vector3 b = Vector3.up * (bounds.max.y - y);
			for (int i = 0; i < list.Count; i++)
			{
				List<IntPoint> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = this.IntPointToV3(list2[j]);
					vector.y = y;
					Vector3 vector2 = this.IntPointToV3(list2[(j + 1) % list2.Count]);
					vector2.y = y;
					Gizmos.DrawLine(vector, vector2);
					Gizmos.DrawLine(vector + b, vector2 + b);
					Gizmos.DrawLine(vector, vector + b);
					Gizmos.DrawLine(vector2, vector2 + b);
				}
			}
			ListPool<List<IntPoint>>.Release(list);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000460E8 File Offset: 0x000444E8
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.Lerp(NavmeshCut.GizmoColor, new Color(1f, 1f, 1f, 0.2f), 0.9f);
			Bounds bounds = this.GetBounds();
			Gizmos.DrawCube(bounds.center, bounds.size);
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}

		// Token: 0x040005EE RID: 1518
		private static List<NavmeshCut> allCuts = new List<NavmeshCut>();

		// Token: 0x040005F0 RID: 1520
		public NavmeshCut.MeshType type;

		// Token: 0x040005F1 RID: 1521
		public Mesh mesh;

		// Token: 0x040005F2 RID: 1522
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x040005F3 RID: 1523
		public float circleRadius = 1f;

		// Token: 0x040005F4 RID: 1524
		public int circleResolution = 6;

		// Token: 0x040005F5 RID: 1525
		public float height = 1f;

		// Token: 0x040005F6 RID: 1526
		public float meshScale = 1f;

		// Token: 0x040005F7 RID: 1527
		public Vector3 center;

		// Token: 0x040005F8 RID: 1528
		public float updateDistance = 0.4f;

		// Token: 0x040005F9 RID: 1529
		public bool isDual;

		// Token: 0x040005FA RID: 1530
		public bool cutsAddedGeom = true;

		// Token: 0x040005FB RID: 1531
		public float updateRotationDistance = 10f;

		// Token: 0x040005FC RID: 1532
		public bool useRotation;

		// Token: 0x040005FD RID: 1533
		private Vector3[][] contours;

		// Token: 0x040005FE RID: 1534
		protected Transform tr;

		// Token: 0x040005FF RID: 1535
		private Mesh lastMesh;

		// Token: 0x04000600 RID: 1536
		private Vector3 lastPosition;

		// Token: 0x04000601 RID: 1537
		private Quaternion lastRotation;

		// Token: 0x04000602 RID: 1538
		private bool wasEnabled;

		// Token: 0x04000603 RID: 1539
		private Bounds bounds;

		// Token: 0x04000604 RID: 1540
		private Bounds lastBounds;

		// Token: 0x04000605 RID: 1541
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x04000606 RID: 1542
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x04000607 RID: 1543
		public static readonly Color GizmoColor = new Color(0.145098045f, 0.721568644f, 0.9372549f);

		// Token: 0x020000D7 RID: 215
		public enum MeshType
		{
			// Token: 0x04000609 RID: 1545
			Rectangle,
			// Token: 0x0400060A RID: 1546
			Circle,
			// Token: 0x0400060B RID: 1547
			CustomMesh
		}
	}
}
