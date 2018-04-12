using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000015 RID: 21
	[AddComponentMenu("Pathfinding/GraphUpdateScene")]
	public class GraphUpdateScene : GraphModifier
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000D618 File Offset: 0x0000BA18
		public void Start()
		{
			if (!this.firstApplied && this.applyOnStart)
			{
				this.Apply();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000D636 File Offset: 0x0000BA36
		public override void OnPostScan()
		{
			if (this.applyOnScan)
			{
				this.Apply();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000D64C File Offset: 0x0000BA4C
		public virtual void InvertSettings()
		{
			this.setWalkability = !this.setWalkability;
			this.penaltyDelta = -this.penaltyDelta;
			if (this.setTagInvert == 0)
			{
				this.setTagInvert = this.setTag;
				this.setTag = 0;
			}
			else
			{
				this.setTag = this.setTagInvert;
				this.setTagInvert = 0;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000D6AB File Offset: 0x0000BAAB
		public void RecalcConvex()
		{
			if (this.convex)
			{
				this.convexPoints = Polygon.ConvexHull(this.points);
			}
			else
			{
				this.convexPoints = null;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000D6D8 File Offset: 0x0000BAD8
		public void ToggleUseWorldSpace()
		{
			this.useWorldSpace = !this.useWorldSpace;
			if (this.points == null)
			{
				return;
			}
			this.convexPoints = null;
			Matrix4x4 matrix4x = (!this.useWorldSpace) ? base.transform.worldToLocalMatrix : base.transform.localToWorldMatrix;
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i] = matrix4x.MultiplyPoint3x4(this.points[i]);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000D770 File Offset: 0x0000BB70
		public void LockToY()
		{
			if (this.points == null)
			{
				return;
			}
			for (int i = 0; i < this.points.Length; i++)
			{
				this.points[i].y = this.lockToYValue;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000D7B9 File Offset: 0x0000BBB9
		public void Apply(AstarPath active)
		{
			if (this.applyOnScan)
			{
				this.Apply();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000D7CC File Offset: 0x0000BBCC
		public Bounds GetBounds()
		{
			Bounds bounds;
			if (this.points == null || this.points.Length == 0)
			{
				if (base.GetComponent<Collider>() != null)
				{
					bounds = base.GetComponent<Collider>().bounds;
				}
				else
				{
					if (!(base.GetComponent<Renderer>() != null))
					{
						return new Bounds(Vector3.zero, Vector3.zero);
					}
					bounds = base.GetComponent<Renderer>().bounds;
				}
			}
			else
			{
				Matrix4x4 matrix4x = Matrix4x4.identity;
				if (!this.useWorldSpace)
				{
					matrix4x = base.transform.localToWorldMatrix;
				}
				Vector3 vector = matrix4x.MultiplyPoint3x4(this.points[0]);
				Vector3 vector2 = matrix4x.MultiplyPoint3x4(this.points[0]);
				for (int i = 0; i < this.points.Length; i++)
				{
					Vector3 rhs = matrix4x.MultiplyPoint3x4(this.points[i]);
					vector = Vector3.Min(vector, rhs);
					vector2 = Vector3.Max(vector2, rhs);
				}
				bounds = new Bounds((vector + vector2) * 0.5f, vector2 - vector);
			}
			if (bounds.size.y < this.minBoundsHeight)
			{
				bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
			}
			return bounds;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000D950 File Offset: 0x0000BD50
		public void Apply()
		{
			if (AstarPath.active == null)
			{
				Debug.LogError("There is no AstarPath object in the scene");
				return;
			}
			GraphUpdateObject graphUpdateObject;
			if (this.points == null || this.points.Length == 0)
			{
				Bounds bounds;
				if (base.GetComponent<Collider>() != null)
				{
					bounds = base.GetComponent<Collider>().bounds;
				}
				else
				{
					if (!(base.GetComponent<Renderer>() != null))
					{
						Debug.LogWarning("Cannot apply GraphUpdateScene, no points defined and no renderer or collider attached");
						return;
					}
					bounds = base.GetComponent<Renderer>().bounds;
				}
				if (bounds.size.y < this.minBoundsHeight)
				{
					bounds.size = new Vector3(bounds.size.x, this.minBoundsHeight, bounds.size.z);
				}
				graphUpdateObject = new GraphUpdateObject(bounds);
			}
			else
			{
				GraphUpdateShape graphUpdateShape = new GraphUpdateShape();
				graphUpdateShape.convex = this.convex;
				Vector3[] array = this.points;
				if (!this.useWorldSpace)
				{
					array = new Vector3[this.points.Length];
					Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = localToWorldMatrix.MultiplyPoint3x4(this.points[i]);
					}
				}
				graphUpdateShape.points = array;
				Bounds bounds2 = graphUpdateShape.GetBounds();
				if (bounds2.size.y < this.minBoundsHeight)
				{
					bounds2.size = new Vector3(bounds2.size.x, this.minBoundsHeight, bounds2.size.z);
				}
				graphUpdateObject = new GraphUpdateObject(bounds2);
				graphUpdateObject.shape = graphUpdateShape;
			}
			this.firstApplied = true;
			graphUpdateObject.modifyWalkability = this.modifyWalkability;
			graphUpdateObject.setWalkability = this.setWalkability;
			graphUpdateObject.addPenalty = this.penaltyDelta;
			graphUpdateObject.updatePhysics = this.updatePhysics;
			graphUpdateObject.updateErosion = this.updateErosion;
			graphUpdateObject.resetPenaltyOnPhysics = this.resetPenaltyOnPhysics;
			graphUpdateObject.modifyTag = this.modifyTag;
			graphUpdateObject.setTag = this.setTag;
			AstarPath.active.UpdateGraphs(graphUpdateObject);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000DB9A File Offset: 0x0000BF9A
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000DBA3 File Offset: 0x0000BFA3
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000DBAC File Offset: 0x0000BFAC
		public void OnDrawGizmos(bool selected)
		{
			Color color = (!selected) ? new Color(0.8901961f, 0.239215687f, 0.08627451f, 0.9f) : new Color(0.8901961f, 0.239215687f, 0.08627451f, 1f);
			if (selected)
			{
				Gizmos.color = Color.Lerp(color, new Color(1f, 1f, 1f, 0.2f), 0.9f);
				Bounds bounds = this.GetBounds();
				Gizmos.DrawCube(bounds.center, bounds.size);
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
			if (this.points == null)
			{
				return;
			}
			if (this.convex)
			{
				color.a *= 0.5f;
			}
			Gizmos.color = color;
			Matrix4x4 matrix4x = (!this.useWorldSpace) ? base.transform.localToWorldMatrix : Matrix4x4.identity;
			if (this.convex)
			{
				color.r -= 0.1f;
				color.g -= 0.2f;
				color.b -= 0.1f;
				Gizmos.color = color;
			}
			if (selected || !this.convex)
			{
				for (int i = 0; i < this.points.Length; i++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.points[i]), matrix4x.MultiplyPoint3x4(this.points[(i + 1) % this.points.Length]));
				}
			}
			if (this.convex)
			{
				if (this.convexPoints == null)
				{
					this.RecalcConvex();
				}
				Gizmos.color = ((!selected) ? new Color(0.8901961f, 0.239215687f, 0.08627451f, 0.9f) : new Color(0.8901961f, 0.239215687f, 0.08627451f, 1f));
				for (int j = 0; j < this.convexPoints.Length; j++)
				{
					Gizmos.DrawLine(matrix4x.MultiplyPoint3x4(this.convexPoints[j]), matrix4x.MultiplyPoint3x4(this.convexPoints[(j + 1) % this.convexPoints.Length]));
				}
			}
		}

		// Token: 0x040000E8 RID: 232
		public Vector3[] points;

		// Token: 0x040000E9 RID: 233
		private Vector3[] convexPoints;

		// Token: 0x040000EA RID: 234
		[HideInInspector]
		public bool convex = true;

		// Token: 0x040000EB RID: 235
		[HideInInspector]
		public float minBoundsHeight = 1f;

		// Token: 0x040000EC RID: 236
		[HideInInspector]
		public int penaltyDelta;

		// Token: 0x040000ED RID: 237
		[HideInInspector]
		public bool modifyWalkability;

		// Token: 0x040000EE RID: 238
		[HideInInspector]
		public bool setWalkability;

		// Token: 0x040000EF RID: 239
		[HideInInspector]
		public bool applyOnStart = true;

		// Token: 0x040000F0 RID: 240
		[HideInInspector]
		public bool applyOnScan = true;

		// Token: 0x040000F1 RID: 241
		[HideInInspector]
		public bool useWorldSpace;

		// Token: 0x040000F2 RID: 242
		[HideInInspector]
		public bool updatePhysics;

		// Token: 0x040000F3 RID: 243
		[HideInInspector]
		public bool resetPenaltyOnPhysics = true;

		// Token: 0x040000F4 RID: 244
		[HideInInspector]
		public bool updateErosion = true;

		// Token: 0x040000F5 RID: 245
		[HideInInspector]
		public bool lockToY;

		// Token: 0x040000F6 RID: 246
		[HideInInspector]
		public float lockToYValue;

		// Token: 0x040000F7 RID: 247
		[HideInInspector]
		public bool modifyTag;

		// Token: 0x040000F8 RID: 248
		[HideInInspector]
		public int setTag;

		// Token: 0x040000F9 RID: 249
		private int setTagInvert;

		// Token: 0x040000FA RID: 250
		private bool firstApplied;
	}
}
