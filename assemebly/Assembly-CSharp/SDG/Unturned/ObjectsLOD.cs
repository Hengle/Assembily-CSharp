using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000565 RID: 1381
	public class ObjectsLOD : MonoBehaviour
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x000DF838 File Offset: 0x000DDC38
		private void Update()
		{
			if (this.objects == null || this.objects.Count == 0)
			{
				return;
			}
			if (MainCamera.instance == null)
			{
				return;
			}
			float sqrMagnitude = (this.cullCenter - MainCamera.instance.transform.position).sqrMagnitude;
			if (sqrMagnitude < this.sqrCullMagnitude)
			{
				if (this.isCulled)
				{
					this.isCulled = false;
					this.load = 0;
				}
			}
			else if (!this.isCulled)
			{
				this.isCulled = true;
				this.load = 0;
			}
			if (this.load == -1)
			{
				return;
			}
			if (this.load >= this.objects.Count)
			{
				this.load = -1;
				return;
			}
			if (this.isCulled)
			{
				if (this.objects[this.load].isVisualEnabled)
				{
					this.objects[this.load].disableVisual();
				}
			}
			else if (!this.objects[this.load].isVisualEnabled)
			{
				this.objects[this.load].enableVisual();
			}
			this.load++;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000DF984 File Offset: 0x000DDD84
		private void OnDrawGizmos()
		{
			if (this.bounds == null || this.bounds.Count == 0)
			{
				return;
			}
			if (this.objects.Count == 0)
			{
				Gizmos.color = Color.black;
			}
			else if (this.isCulled)
			{
				Gizmos.color = Color.red;
			}
			else
			{
				Gizmos.color = Color.green;
			}
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.matrix = base.transform.localToWorldMatrix;
			for (int i = 0; i < this.bounds.Count; i++)
			{
				Bounds bounds = this.bounds[i];
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
			Gizmos.matrix = matrix;
			Gizmos.DrawWireCube(this.cullCenter, Vector3.one);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000DFA58 File Offset: 0x000DDE58
		private void OnDisable()
		{
			if (this.objects == null || this.objects.Count == 0)
			{
				return;
			}
			this.isCulled = true;
			this.load = -1;
			for (int i = 0; i < this.objects.Count; i++)
			{
				if (this.objects[i].isVisualEnabled)
				{
					this.objects[i].disableVisual();
				}
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000DFAD4 File Offset: 0x000DDED4
		private void findInBounds(Bounds bound)
		{
			byte b;
			byte b2;
			Regions.tryGetCoordinate(base.transform.TransformPoint(bound.min), out b, out b2);
			byte b3;
			byte b4;
			Regions.tryGetCoordinate(base.transform.TransformPoint(bound.max), out b3, out b4);
			for (byte b5 = b; b5 <= b3; b5 += 1)
			{
				for (byte b6 = b2; b6 <= b4; b6 += 1)
				{
					for (int i = 0; i < LevelObjects.objects[(int)b5, (int)b6].Count; i++)
					{
						LevelObject levelObject = LevelObjects.objects[(int)b5, (int)b6][i];
						if (levelObject.asset != null && !(levelObject.transform == null) && levelObject.asset.type != EObjectType.LARGE)
						{
							if (!levelObject.isSpeciallyCulled)
							{
								Vector3 point = base.transform.InverseTransformPoint(levelObject.transform.position);
								if (bound.Contains(point))
								{
									levelObject.isSpeciallyCulled = true;
									this.objects.Add(levelObject);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000DFC0C File Offset: 0x000DE00C
		public void calculateBounds()
		{
			this.cullMagnitude = 64f * this.bias;
			this.sqrCullMagnitude = this.cullMagnitude * this.cullMagnitude;
			if (this.lod == EObjectLOD.MESH)
			{
				ObjectsLOD.meshes.Clear();
				base.GetComponentsInChildren<MeshFilter>(true, ObjectsLOD.meshes);
				if (ObjectsLOD.meshes.Count == 0)
				{
					base.enabled = false;
					return;
				}
				Bounds item = default(Bounds);
				for (int i = 0; i < ObjectsLOD.meshes.Count; i++)
				{
					Mesh sharedMesh = ObjectsLOD.meshes[i].sharedMesh;
					if (!(sharedMesh == null))
					{
						Bounds bounds = sharedMesh.bounds;
						item.Encapsulate(bounds.min);
						item.Encapsulate(bounds.max);
					}
				}
				item.Expand(-1f);
				item.center += this.center;
				item.size += this.size;
				if (item.size.x < 1f || item.size.y < 1f || item.size.z < 1f)
				{
					base.enabled = false;
					return;
				}
				this.bounds = new List<Bounds>();
				this.bounds.Add(item);
			}
			else if (this.lod == EObjectLOD.AREA)
			{
				ObjectsLOD.areas.Clear();
				base.GetComponentsInChildren<OcclusionArea>(true, ObjectsLOD.areas);
				if (ObjectsLOD.areas.Count == 0)
				{
					base.enabled = false;
					return;
				}
				this.bounds = new List<Bounds>();
				for (int j = 0; j < ObjectsLOD.areas.Count; j++)
				{
					OcclusionArea occlusionArea = ObjectsLOD.areas[j];
					Bounds item2 = new Bounds(occlusionArea.transform.localPosition + occlusionArea.center, new Vector3(occlusionArea.size.x, occlusionArea.size.z, occlusionArea.size.y));
					this.bounds.Add(item2);
				}
			}
			this.objects = new List<LevelObject>();
			for (int k = 0; k < this.bounds.Count; k++)
			{
				Bounds bounds2 = this.bounds[k];
				this.cullCenter += bounds2.center;
			}
			this.cullCenter /= (float)this.bounds.Count;
			this.cullCenter = base.transform.TransformPoint(this.cullCenter);
			byte b;
			byte b2;
			Regions.tryGetCoordinate(this.cullCenter, out b, out b2);
			for (int l = (int)(b - 1); l <= (int)(b + 1); l++)
			{
				for (int m = (int)(b2 - 1); m <= (int)(b2 + 1); m++)
				{
					for (int n = 0; n < LevelObjects.objects[l, m].Count; n++)
					{
						LevelObject levelObject = LevelObjects.objects[l, m][n];
						if (levelObject.asset != null && !(levelObject.transform == null) && levelObject.asset.type != EObjectType.LARGE)
						{
							if (!levelObject.isSpeciallyCulled)
							{
								Vector3 point = base.transform.InverseTransformPoint(levelObject.transform.position);
								bool flag = false;
								for (int num = 0; num < this.bounds.Count; num++)
								{
									if (this.bounds[num].Contains(point))
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									levelObject.isSpeciallyCulled = true;
									this.objects.Add(levelObject);
								}
							}
						}
					}
				}
			}
			if (this.objects.Count == 0)
			{
				base.enabled = false;
				return;
			}
		}

		// Token: 0x040017BC RID: 6076
		private static List<MeshFilter> meshes = new List<MeshFilter>();

		// Token: 0x040017BD RID: 6077
		private static List<OcclusionArea> areas = new List<OcclusionArea>();

		// Token: 0x040017BE RID: 6078
		public EObjectLOD lod;

		// Token: 0x040017BF RID: 6079
		public float bias;

		// Token: 0x040017C0 RID: 6080
		public Vector3 center;

		// Token: 0x040017C1 RID: 6081
		public Vector3 size;

		// Token: 0x040017C2 RID: 6082
		private Vector3 cullCenter;

		// Token: 0x040017C3 RID: 6083
		private float cullMagnitude;

		// Token: 0x040017C4 RID: 6084
		private float sqrCullMagnitude;

		// Token: 0x040017C5 RID: 6085
		private List<Bounds> bounds;

		// Token: 0x040017C6 RID: 6086
		private List<LevelObject> objects;

		// Token: 0x040017C7 RID: 6087
		private bool isCulled;

		// Token: 0x040017C8 RID: 6088
		private int load;
	}
}
