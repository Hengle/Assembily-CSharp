using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000E9 RID: 233
	public abstract class RVOObstacle : MonoBehaviour
	{
		// Token: 0x060007A8 RID: 1960
		protected abstract void CreateObstacles();

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060007A9 RID: 1961
		protected abstract bool ExecuteInEditor { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060007AA RID: 1962
		protected abstract bool LocalCoordinates { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060007AB RID: 1963
		protected abstract bool StaticObstacle { get; }

		// Token: 0x060007AC RID: 1964
		protected abstract bool AreGizmosDirty();

		// Token: 0x060007AD RID: 1965 RVA: 0x0004A576 File Offset: 0x00048976
		public void OnDrawGizmos()
		{
			this.OnDrawGizmos(false);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0004A57F File Offset: 0x0004897F
		public void OnDrawGizmosSelected()
		{
			this.OnDrawGizmos(true);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0004A588 File Offset: 0x00048988
		public void OnDrawGizmos(bool selected)
		{
			this.gizmoDrawing = true;
			Gizmos.color = new Color(0.615f, 1f, 0.06f, (!selected) ? 0.7f : 1f);
			if (this.gizmoVerts == null || this.AreGizmosDirty() || this._obstacleMode != this.obstacleMode)
			{
				this._obstacleMode = this.obstacleMode;
				if (this.gizmoVerts == null)
				{
					this.gizmoVerts = new List<Vector3[]>();
				}
				else
				{
					this.gizmoVerts.Clear();
				}
				this.CreateObstacles();
			}
			Matrix4x4 matrix = this.GetMatrix();
			for (int i = 0; i < this.gizmoVerts.Count; i++)
			{
				Vector3[] array = this.gizmoVerts[i];
				int j = 0;
				int num = array.Length - 1;
				while (j < array.Length)
				{
					Gizmos.DrawLine(matrix.MultiplyPoint3x4(array[j]), matrix.MultiplyPoint3x4(array[num]));
					num = j++;
				}
				if (selected)
				{
					int k = 0;
					int num2 = array.Length - 1;
					while (k < array.Length)
					{
						Vector3 vector = matrix.MultiplyPoint3x4(array[num2]);
						Vector3 vector2 = matrix.MultiplyPoint3x4(array[k]);
						Vector3 vector3 = (vector + vector2) * 0.5f;
						Vector3 normalized = (vector2 - vector).normalized;
						if (!(normalized == Vector3.zero))
						{
							Vector3 vector4 = Vector3.Cross(Vector3.up, normalized);
							Gizmos.DrawLine(vector3, vector3 + vector4);
							Gizmos.DrawLine(vector3 + vector4, vector3 + vector4 * 0.5f + normalized * 0.5f);
							Gizmos.DrawLine(vector3 + vector4, vector3 + vector4 * 0.5f - normalized * 0.5f);
						}
						num2 = k++;
					}
				}
			}
			this.gizmoDrawing = false;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0004A7BF File Offset: 0x00048BBF
		protected virtual Matrix4x4 GetMatrix()
		{
			if (this.LocalCoordinates)
			{
				return base.transform.localToWorldMatrix;
			}
			return Matrix4x4.identity;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0004A7E0 File Offset: 0x00048BE0
		public void OnDisable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnEnable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.RemoveObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0004A844 File Offset: 0x00048C44
		public void OnEnable()
		{
			if (this.addedObstacles != null)
			{
				if (this.sim == null)
				{
					throw new Exception("This should not happen! Make sure you are not overriding the OnDisable function");
				}
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					ObstacleVertex obstacleVertex = this.addedObstacles[i];
					ObstacleVertex obstacleVertex2 = obstacleVertex;
					do
					{
						obstacleVertex.layer = this.layer;
						obstacleVertex = obstacleVertex.next;
					}
					while (obstacleVertex != obstacleVertex2);
					this.sim.AddObstacle(this.addedObstacles[i]);
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0004A8CF File Offset: 0x00048CCF
		public void Start()
		{
			this.addedObstacles = new List<ObstacleVertex>();
			this.sourceObstacles = new List<Vector3[]>();
			this.prevUpdateMatrix = this.GetMatrix();
			this.CreateObstacles();
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0004A8FC File Offset: 0x00048CFC
		public void Update()
		{
			Matrix4x4 matrix = this.GetMatrix();
			if (matrix != this.prevUpdateMatrix)
			{
				for (int i = 0; i < this.addedObstacles.Count; i++)
				{
					this.sim.UpdateObstacle(this.addedObstacles[i], this.sourceObstacles[i], matrix);
				}
				this.prevUpdateMatrix = matrix;
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0004A968 File Offset: 0x00048D68
		protected void FindSimulator()
		{
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				throw new InvalidOperationException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.sim = rvosimulator.GetSimulator();
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0004A9B0 File Offset: 0x00048DB0
		protected void AddObstacle(Vector3[] vertices, float height)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices Must Not Be Null");
			}
			if (height < 0f)
			{
				throw new ArgumentOutOfRangeException("Height must be non-negative");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("An obstacle must have at least two vertices");
			}
			if (this.gizmoDrawing)
			{
				Vector3[] array = new Vector3[vertices.Length];
				this.WindCorrectly(vertices);
				Array.Copy(vertices, array, vertices.Length);
				this.gizmoVerts.Add(array);
				return;
			}
			if (this.sim == null)
			{
				this.FindSimulator();
			}
			if (vertices.Length == 2)
			{
				this.AddObstacleInternal(vertices, height);
				return;
			}
			this.WindCorrectly(vertices);
			this.AddObstacleInternal(vertices, height);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0004AA5C File Offset: 0x00048E5C
		private void AddObstacleInternal(Vector3[] vertices, float height)
		{
			this.addedObstacles.Add(this.sim.AddObstacle(vertices, height, this.GetMatrix(), this.layer));
			this.sourceObstacles.Add(vertices);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0004AA90 File Offset: 0x00048E90
		private void WindCorrectly(Vector3[] vertices)
		{
			int num = 0;
			float num2 = float.PositiveInfinity;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (vertices[i].x < num2)
				{
					num = i;
					num2 = vertices[i].x;
				}
			}
			if (Polygon.IsClockwise(vertices[(num - 1 + vertices.Length) % vertices.Length], vertices[num], vertices[(num + 1) % vertices.Length]))
			{
				if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepOut)
				{
					Array.Reverse(vertices);
				}
			}
			else if (this.obstacleMode == RVOObstacle.ObstacleVertexWinding.KeepIn)
			{
				Array.Reverse(vertices);
			}
		}

		// Token: 0x04000672 RID: 1650
		public RVOObstacle.ObstacleVertexWinding obstacleMode;

		// Token: 0x04000673 RID: 1651
		public RVOLayer layer = RVOLayer.DefaultObstacle;

		// Token: 0x04000674 RID: 1652
		protected Simulator sim;

		// Token: 0x04000675 RID: 1653
		private List<ObstacleVertex> addedObstacles;

		// Token: 0x04000676 RID: 1654
		private List<Vector3[]> sourceObstacles;

		// Token: 0x04000677 RID: 1655
		private bool gizmoDrawing;

		// Token: 0x04000678 RID: 1656
		private List<Vector3[]> gizmoVerts;

		// Token: 0x04000679 RID: 1657
		private RVOObstacle.ObstacleVertexWinding _obstacleMode;

		// Token: 0x0400067A RID: 1658
		private Matrix4x4 prevUpdateMatrix;

		// Token: 0x020000EA RID: 234
		public enum ObstacleVertexWinding
		{
			// Token: 0x0400067C RID: 1660
			KeepOut,
			// Token: 0x0400067D RID: 1661
			KeepIn
		}
	}
}
