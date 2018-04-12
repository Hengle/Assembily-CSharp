using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A7 RID: 167
	[AddComponentMenu("Pathfinding/Navmesh/RecastMeshObj")]
	public class RecastMeshObj : MonoBehaviour
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x000359DC File Offset: 0x00033DDC
		public static void GetAllInBounds(List<RecastMeshObj> buffer, Bounds bounds)
		{
			if (!Application.isPlaying)
			{
				RecastMeshObj[] array = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].RecalculateBounds();
					if (array[i].GetBounds().Intersects(bounds))
					{
						buffer.Add(array[i]);
					}
				}
				return;
			}
			if (Time.timeSinceLevelLoad == 0f)
			{
				RecastMeshObj[] array2 = UnityEngine.Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Register();
				}
			}
			for (int k = 0; k < RecastMeshObj.dynamicMeshObjs.Count; k++)
			{
				if (RecastMeshObj.dynamicMeshObjs[k].GetBounds().Intersects(bounds))
				{
					buffer.Add(RecastMeshObj.dynamicMeshObjs[k]);
				}
			}
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			RecastMeshObj.tree.QueryInBounds(rect, buffer);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00035B2E File Offset: 0x00033F2E
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00035B38 File Offset: 0x00033F38
		private void Register()
		{
			if (this.registered)
			{
				return;
			}
			this.registered = true;
			this.area = Mathf.Clamp(this.area, -1, 33554432);
			Renderer component = base.GetComponent<Renderer>();
			Collider component2 = base.GetComponent<Collider>();
			if (component == null && component2 == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component3 = base.GetComponent<MeshFilter>();
			if (component != null && component3 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			if (component != null)
			{
				this.bounds = component.bounds;
			}
			else
			{
				this.bounds = component2.bounds;
			}
			this._dynamic = this.dynamic;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Add(this);
			}
			else
			{
				RecastMeshObj.tree.Insert(this);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00035C28 File Offset: 0x00034028
		private void RecalculateBounds()
		{
			Renderer component = base.GetComponent<Renderer>();
			Collider collider = this.GetCollider();
			if (component == null && collider == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			if (component != null && component2 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			if (component != null)
			{
				this.bounds = component.bounds;
			}
			else
			{
				this.bounds = collider.bounds;
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00035CB9 File Offset: 0x000340B9
		public Bounds GetBounds()
		{
			if (this._dynamic)
			{
				this.RecalculateBounds();
			}
			return this.bounds;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00035CD2 File Offset: 0x000340D2
		public MeshFilter GetMeshFilter()
		{
			return base.GetComponent<MeshFilter>();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00035CDA File Offset: 0x000340DA
		public Collider GetCollider()
		{
			return base.GetComponent<Collider>();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00035CE4 File Offset: 0x000340E4
		private void OnDisable()
		{
			this.registered = false;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Remove(this);
			}
			else if (!RecastMeshObj.tree.Remove(this))
			{
				throw new Exception("Could not remove RecastMeshObj from tree even though it should exist in it. Has the object moved without being marked as dynamic?");
			}
			this._dynamic = this.dynamic;
		}

		// Token: 0x040004B9 RID: 1209
		protected static RecastBBTree tree = new RecastBBTree();

		// Token: 0x040004BA RID: 1210
		protected static List<RecastMeshObj> dynamicMeshObjs = new List<RecastMeshObj>();

		// Token: 0x040004BB RID: 1211
		[HideInInspector]
		public Bounds bounds;

		// Token: 0x040004BC RID: 1212
		public bool dynamic;

		// Token: 0x040004BD RID: 1213
		public int area;

		// Token: 0x040004BE RID: 1214
		private bool _dynamic;

		// Token: 0x040004BF RID: 1215
		private bool registered;
	}
}
