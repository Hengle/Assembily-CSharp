using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000CF RID: 207
	[AddComponentMenu("Pathfinding/Modifiers/Raycast Simplifier")]
	[Serializable]
	public class RaycastModifier : MonoModifier
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0004325E File Offset: 0x0004165E
		public override ModifierData input
		{
			get
			{
				return ModifierData.Vector;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00043262 File Offset: 0x00041662
		public override ModifierData output
		{
			get
			{
				return ModifierData.VectorPath;
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00043268 File Offset: 0x00041668
		public override void Apply(Path p, ModifierData source)
		{
			if (this.iterations <= 0)
			{
				return;
			}
			if (RaycastModifier.nodes == null)
			{
				RaycastModifier.nodes = new List<Vector3>(p.vectorPath.Count);
			}
			else
			{
				RaycastModifier.nodes.Clear();
			}
			RaycastModifier.nodes.AddRange(p.vectorPath);
			for (int i = 0; i < this.iterations; i++)
			{
				if (this.subdivideEveryIter && i != 0)
				{
					if (RaycastModifier.nodes.Capacity < RaycastModifier.nodes.Count * 3)
					{
						RaycastModifier.nodes.Capacity = RaycastModifier.nodes.Count * 3;
					}
					int count = RaycastModifier.nodes.Count;
					for (int j = 0; j < count - 1; j++)
					{
						RaycastModifier.nodes.Add(Vector3.zero);
						RaycastModifier.nodes.Add(Vector3.zero);
					}
					for (int k = count - 1; k > 0; k--)
					{
						Vector3 a = RaycastModifier.nodes[k];
						Vector3 b = RaycastModifier.nodes[k + 1];
						RaycastModifier.nodes[k * 3] = RaycastModifier.nodes[k];
						if (k != count - 1)
						{
							RaycastModifier.nodes[k * 3 + 1] = Vector3.Lerp(a, b, 0.33f);
							RaycastModifier.nodes[k * 3 + 2] = Vector3.Lerp(a, b, 0.66f);
						}
					}
				}
				int l = 0;
				while (l < RaycastModifier.nodes.Count - 2)
				{
					Vector3 v = RaycastModifier.nodes[l];
					Vector3 v2 = RaycastModifier.nodes[l + 2];
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					if (this.ValidateLine(null, null, v, v2))
					{
						RaycastModifier.nodes.RemoveAt(l + 1);
					}
					else
					{
						l++;
					}
					stopwatch.Stop();
				}
			}
			p.vectorPath.Clear();
			p.vectorPath.AddRange(RaycastModifier.nodes);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00043474 File Offset: 0x00041874
		public bool ValidateLine(GraphNode n1, GraphNode n2, Vector3 v1, Vector3 v2)
		{
			if (this.useRaycasting)
			{
				RaycastHit raycastHit2;
				if (this.thickRaycast && this.thickRaycastRadius > 0f)
				{
					RaycastHit raycastHit;
					if (Physics.SphereCast(v1 + this.raycastOffset, this.thickRaycastRadius, v2 - v1, out raycastHit, (v2 - v1).magnitude, this.mask))
					{
						return false;
					}
				}
				else if (Physics.Linecast(v1 + this.raycastOffset, v2 + this.raycastOffset, out raycastHit2, this.mask))
				{
					return false;
				}
			}
			if (this.useGraphRaycasting && n1 == null)
			{
				n1 = AstarPath.active.GetNearest(v1).node;
				n2 = AstarPath.active.GetNearest(v2).node;
			}
			if (this.useGraphRaycasting && n1 != null && n2 != null)
			{
				NavGraph graph = AstarData.GetGraph(n1);
				NavGraph graph2 = AstarData.GetGraph(n2);
				if (graph != graph2)
				{
					return false;
				}
				if (graph != null)
				{
					IRaycastableGraph raycastableGraph = graph as IRaycastableGraph;
					if (raycastableGraph != null && raycastableGraph.Linecast(v1, v2, n1))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x040005BD RID: 1469
		[HideInInspector]
		public bool useRaycasting = true;

		// Token: 0x040005BE RID: 1470
		[HideInInspector]
		public LayerMask mask = -1;

		// Token: 0x040005BF RID: 1471
		[HideInInspector]
		public bool thickRaycast;

		// Token: 0x040005C0 RID: 1472
		[HideInInspector]
		public float thickRaycastRadius;

		// Token: 0x040005C1 RID: 1473
		[HideInInspector]
		public Vector3 raycastOffset = Vector3.zero;

		// Token: 0x040005C2 RID: 1474
		[HideInInspector]
		public bool subdivideEveryIter;

		// Token: 0x040005C3 RID: 1475
		public int iterations = 2;

		// Token: 0x040005C4 RID: 1476
		[HideInInspector]
		public bool useGraphRaycasting;

		// Token: 0x040005C5 RID: 1477
		private static List<Vector3> nodes;
	}
}
