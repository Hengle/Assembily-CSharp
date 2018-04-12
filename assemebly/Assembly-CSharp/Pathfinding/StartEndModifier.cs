using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000D2 RID: 210
	[Serializable]
	public class StartEndModifier : PathModifier
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00044105 File Offset: 0x00042505
		public override ModifierData input
		{
			get
			{
				return ModifierData.Vector;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00044109 File Offset: 0x00042509
		public override ModifierData output
		{
			get
			{
				return ((!this.addPoints) ? ModifierData.StrictVectorPath : ModifierData.None) | ModifierData.VectorPath;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00044120 File Offset: 0x00042520
		public override void Apply(Path _p, ModifierData source)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null)
			{
				return;
			}
			if (abpath.vectorPath.Count == 0)
			{
				return;
			}
			if (abpath.vectorPath.Count < 2 && !this.addPoints)
			{
				abpath.vectorPath.Add(abpath.vectorPath[0]);
			}
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			if (this.exactStartPoint == StartEndModifier.Exactness.Original)
			{
				vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.originalStartPoint, abpath.path[0]);
			}
			else if (this.exactStartPoint == StartEndModifier.Exactness.ClosestOnNode)
			{
				vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.startPoint, abpath.path[0]);
			}
			else if (this.exactStartPoint == StartEndModifier.Exactness.Interpolate)
			{
				vector = this.GetClampedPoint((Vector3)abpath.path[0].position, abpath.originalStartPoint, abpath.path[0]);
				vector = AstarMath.NearestPointStrict((Vector3)abpath.path[0].position, (Vector3)abpath.path[(1 < abpath.path.Count) ? 1 : 0].position, vector);
			}
			else
			{
				vector = (Vector3)abpath.path[0].position;
			}
			if (this.exactEndPoint == StartEndModifier.Exactness.Original)
			{
				vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.originalEndPoint, abpath.path[abpath.path.Count - 1]);
			}
			else if (this.exactEndPoint == StartEndModifier.Exactness.ClosestOnNode)
			{
				vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.endPoint, abpath.path[abpath.path.Count - 1]);
			}
			else if (this.exactEndPoint == StartEndModifier.Exactness.Interpolate)
			{
				vector2 = this.GetClampedPoint((Vector3)abpath.path[abpath.path.Count - 1].position, abpath.originalEndPoint, abpath.path[abpath.path.Count - 1]);
				vector2 = AstarMath.NearestPointStrict((Vector3)abpath.path[abpath.path.Count - 1].position, (Vector3)abpath.path[(abpath.path.Count - 2 >= 0) ? (abpath.path.Count - 2) : 0].position, vector2);
			}
			else
			{
				vector2 = (Vector3)abpath.path[abpath.path.Count - 1].position;
			}
			if (!this.addPoints)
			{
				abpath.vectorPath[0] = vector;
				abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
			}
			else
			{
				if (this.exactStartPoint != StartEndModifier.Exactness.SnapToNode)
				{
					abpath.vectorPath.Insert(0, vector);
				}
				if (this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
				{
					abpath.vectorPath.Add(vector2);
				}
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000444A0 File Offset: 0x000428A0
		public Vector3 GetClampedPoint(Vector3 from, Vector3 to, GraphNode hint)
		{
			Vector3 vector = to;
			RaycastHit raycastHit;
			if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
			{
				vector = raycastHit.point;
			}
			if (this.useGraphRaycasting && hint != null)
			{
				NavGraph graph = AstarData.GetGraph(hint);
				if (graph != null)
				{
					IRaycastableGraph raycastableGraph = graph as IRaycastableGraph;
					GraphHitInfo graphHitInfo;
					if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, hint, out graphHitInfo))
					{
						vector = graphHitInfo.point;
					}
				}
			}
			return vector;
		}

		// Token: 0x040005D4 RID: 1492
		public bool addPoints;

		// Token: 0x040005D5 RID: 1493
		public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x040005D6 RID: 1494
		public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x040005D7 RID: 1495
		public bool useRaycasting;

		// Token: 0x040005D8 RID: 1496
		public LayerMask mask = -1;

		// Token: 0x040005D9 RID: 1497
		public bool useGraphRaycasting;

		// Token: 0x020000D3 RID: 211
		public enum Exactness
		{
			// Token: 0x040005DB RID: 1499
			SnapToNode,
			// Token: 0x040005DC RID: 1500
			Original,
			// Token: 0x040005DD RID: 1501
			Interpolate,
			// Token: 0x040005DE RID: 1502
			ClosestOnNode
		}
	}
}
