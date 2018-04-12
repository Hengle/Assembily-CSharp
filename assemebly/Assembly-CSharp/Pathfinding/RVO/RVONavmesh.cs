using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000E8 RID: 232
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Navmesh")]
	public class RVONavmesh : GraphModifier
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0004A2C1 File Offset: 0x000486C1
		public override void OnPostCacheLoad()
		{
			this.OnLatePostScan();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0004A2CC File Offset: 0x000486CC
		public override void OnLatePostScan()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.RemoveObstacles();
			NavGraph[] graphs = AstarPath.active.graphs;
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			Simulator simulator = rvosimulator.GetSimulator();
			for (int i = 0; i < graphs.Length; i++)
			{
				this.AddGraphObstacles(simulator, graphs[i]);
			}
			simulator.UpdateObstacles();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0004A34C File Offset: 0x0004874C
		public void RemoveObstacles()
		{
			if (this.lastSim == null)
			{
				return;
			}
			Simulator simulator = this.lastSim;
			this.lastSim = null;
			for (int i = 0; i < this.obstacles.Count; i++)
			{
				simulator.RemoveObstacle(this.obstacles[i]);
			}
			this.obstacles.Clear();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0004A3AC File Offset: 0x000487AC
		public void AddGraphObstacles(Simulator sim, NavGraph graph)
		{
			if (this.obstacles.Count > 0 && this.lastSim != null && this.lastSim != sim)
			{
				Debug.LogError("Simulator has changed but some old obstacles are still added for the previous simulator. Deleting previous obstacles.");
				this.RemoveObstacles();
			}
			this.lastSim = sim;
			INavmesh navmesh = graph as INavmesh;
			if (navmesh == null)
			{
				return;
			}
			int[] uses = new int[20];
			navmesh.GetNodes(delegate(GraphNode _node)
			{
				TriangleMeshNode triangleMeshNode = _node as TriangleMeshNode;
				uses[0] = (uses[1] = (uses[2] = 0));
				if (triangleMeshNode != null)
				{
					for (int i = 0; i < triangleMeshNode.connections.Length; i++)
					{
						TriangleMeshNode triangleMeshNode2 = triangleMeshNode.connections[i] as TriangleMeshNode;
						if (triangleMeshNode2 != null)
						{
							int num = triangleMeshNode.SharedEdge(triangleMeshNode2);
							if (num != -1)
							{
								uses[num] = 1;
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (uses[j] == 0)
						{
							Vector3 a = (Vector3)triangleMeshNode.GetVertex(j);
							Vector3 b = (Vector3)triangleMeshNode.GetVertex((j + 1) % triangleMeshNode.GetVertexCount());
							float val = Math.Abs(a.y - b.y);
							val = Math.Max(val, 5f);
							this.obstacles.Add(sim.AddObstacle(a, b, this.wallHeight));
						}
					}
				}
				return true;
			});
		}

		// Token: 0x0400066F RID: 1647
		public float wallHeight = 5f;

		// Token: 0x04000670 RID: 1648
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x04000671 RID: 1649
		private Simulator lastSim;
	}
}
