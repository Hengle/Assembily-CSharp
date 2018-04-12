﻿using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class EuclideanEmbedding
	{
		// Token: 0x060005AB RID: 1451 RVA: 0x00033D90 File Offset: 0x00032190
		public uint GetRandom()
		{
			this.rval = this.ra * this.rval + this.rc;
			return this.rval;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00033DB4 File Offset: 0x000321B4
		private void EnsureCapacity(int index)
		{
			if (index > this.maxNodeIndex)
			{
				object obj = this.lockObj;
				lock (obj)
				{
					if (index > this.maxNodeIndex)
					{
						if (index >= this.costs.Length)
						{
							uint[] array = new uint[Math.Max(index * 2, this.pivots.Length * 2)];
							for (int i = 0; i < this.costs.Length; i++)
							{
								array[i] = this.costs[i];
							}
							this.costs = array;
						}
						this.maxNodeIndex = index;
					}
				}
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00033E5C File Offset: 0x0003225C
		public uint GetHeuristic(int nodeIndex1, int nodeIndex2)
		{
			nodeIndex1 *= this.pivotCount;
			nodeIndex2 *= this.pivotCount;
			if (nodeIndex1 >= this.costs.Length || nodeIndex2 >= this.costs.Length)
			{
				this.EnsureCapacity((nodeIndex1 <= nodeIndex2) ? nodeIndex2 : nodeIndex1);
			}
			uint num = 0u;
			for (int i = 0; i < this.pivotCount; i++)
			{
				uint num2 = (uint)Math.Abs((int)(this.costs[nodeIndex1 + i] - this.costs[nodeIndex2 + i]));
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00033EEC File Offset: 0x000322EC
		private void GetClosestWalkableNodesToChildrenRecursively(Transform tr, List<GraphNode> nodes)
		{
			IEnumerator enumerator = tr.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					NNInfo nearest = AstarPath.active.GetNearest(transform.position, NNConstraint.Default);
					if (nearest.node != null && nearest.node.Walkable)
					{
						nodes.Add(nearest.node);
					}
					this.GetClosestWalkableNodesToChildrenRecursively(tr, nodes);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00033F90 File Offset: 0x00032390
		public void RecalculatePivots()
		{
			if (this.mode == HeuristicOptimizationMode.None)
			{
				this.pivotCount = 0;
				this.pivots = null;
				return;
			}
			this.rval = (uint)this.seed;
			NavGraph[] graphs = AstarPath.active.graphs;
			List<GraphNode> pivotList = ListPool<GraphNode>.Claim();
			if (this.mode == HeuristicOptimizationMode.Custom)
			{
				if (this.pivotPointRoot == null)
				{
					throw new Exception("Grid Graph -> heuristicOptimizationMode is HeuristicOptimizationMode.Custom, but no 'customHeuristicOptimizationPivotsRoot' is set");
				}
				this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, pivotList);
			}
			else if (this.mode == HeuristicOptimizationMode.Random)
			{
				int n = 0;
				for (int i = 0; i < graphs.Length; i++)
				{
					graphs[i].GetNodes(delegate(GraphNode node)
					{
						if (!node.Destroyed && node.Walkable)
						{
							n++;
							if ((ulong)this.GetRandom() % (ulong)((long)n) < (ulong)((long)this.spreadOutCount))
							{
								if (pivotList.Count < this.spreadOutCount)
								{
									pivotList.Add(node);
								}
								else
								{
									pivotList[(int)((ulong)this.GetRandom() % (ulong)((long)pivotList.Count))] = node;
								}
							}
						}
						return true;
					});
				}
			}
			else
			{
				if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
				{
					throw new Exception("Invalid HeuristicOptimizationMode: " + this.mode);
				}
				GraphNode first = null;
				if (this.pivotPointRoot != null)
				{
					this.GetClosestWalkableNodesToChildrenRecursively(this.pivotPointRoot, pivotList);
				}
				else
				{
					for (int j = 0; j < graphs.Length; j++)
					{
						graphs[j].GetNodes(delegate(GraphNode node)
						{
							if (node != null && node.Walkable)
							{
								first = node;
								return false;
							}
							return true;
						});
					}
					if (first == null)
					{
						Debug.LogError("Could not find any walkable node in any of the graphs.");
						ListPool<GraphNode>.Release(pivotList);
						return;
					}
					pivotList.Add(first);
				}
				for (int k = 0; k < this.spreadOutCount; k++)
				{
					pivotList.Add(null);
				}
			}
			this.pivots = pivotList.ToArray();
			ListPool<GraphNode>.Release(pivotList);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00034180 File Offset: 0x00032580
		public void RecalculateCosts()
		{
			if (this.pivots == null)
			{
				this.RecalculatePivots();
			}
			if (this.mode == HeuristicOptimizationMode.None)
			{
				return;
			}
			this.pivotCount = 0;
			for (int i = 0; i < this.pivots.Length; i++)
			{
				if (this.pivots[i] != null && (this.pivots[i].Destroyed || !this.pivots[i].Walkable))
				{
					throw new Exception("Invalid pivot nodes (destroyed or unwalkable)");
				}
			}
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int j = 0; j < this.pivots.Length; j++)
				{
					if (this.pivots[j] == null)
					{
						throw new Exception("Invalid pivot nodes (null)");
					}
				}
			}
			Debug.Log("Recalculating costs...");
			this.pivotCount = this.pivots.Length;
			Action<int> startCostCalculation = null;
			startCostCalculation = delegate(int k)
			{
				GraphNode pivot = this.pivots[k];
				FloodPath fp = null;
				fp = FloodPath.Construct(pivot, null);
				fp.immediateCallback = delegate(Path _p)
				{
					_p.Claim(this);
					MeshNode meshNode = pivot as MeshNode;
					uint costOffset = 0u;
					int k;
					if (meshNode != null && meshNode.connectionCosts != null)
					{
						for (k = 0; k < meshNode.connectionCosts.Length; k++)
						{
							costOffset = Math.Max(costOffset, meshNode.connectionCosts[k]);
						}
					}
					NavGraph[] graphs = AstarPath.active.graphs;
					for (int m = graphs.Length - 1; m >= 0; m--)
					{
						graphs[m].GetNodes(delegate(GraphNode node)
						{
							int num6 = node.NodeIndex * this.pivotCount + k;
							this.EnsureCapacity(num6);
							PathNode pathNode = fp.pathHandler.GetPathNode(node);
							if (costOffset > 0u)
							{
								this.costs[num6] = ((pathNode.pathID != fp.pathID || pathNode.parent == null) ? 0u : Math.Max(pathNode.parent.G - costOffset, 0u));
							}
							else
							{
								this.costs[num6] = ((pathNode.pathID != fp.pathID) ? 0u : pathNode.G);
							}
							return true;
						});
					}
					if (this.mode == HeuristicOptimizationMode.RandomSpreadOut && k < this.pivots.Length - 1)
					{
						int num = -1;
						uint num2 = 0u;
						int num3 = this.maxNodeIndex / this.pivotCount;
						for (int n = 1; n < num3; n++)
						{
							uint num4 = 1073741824u;
							for (int num5 = 0; num5 <= k; num5++)
							{
								num4 = Math.Min(num4, this.costs[n * this.pivotCount + num5]);
							}
							GraphNode node2 = fp.pathHandler.GetPathNode(n).node;
							if ((num4 > num2 || num == -1) && node2 != null && !node2.Destroyed && node2.Walkable)
							{
								num = n;
								num2 = num4;
							}
						}
						if (num == -1)
						{
							Debug.LogError("Failed generating random pivot points for heuristic optimizations");
							return;
						}
						this.pivots[k + 1] = fp.pathHandler.GetPathNode(num).node;
						Debug.Log(string.Concat(new object[]
						{
							"Found node at ",
							this.pivots[k + 1].position,
							" with score ",
							num2
						}));
						startCostCalculation(k + 1);
					}
					_p.Release(this);
				};
				AstarPath.StartPath(fp, true);
			};
			if (this.mode != HeuristicOptimizationMode.RandomSpreadOut)
			{
				for (int l = 0; l < this.pivots.Length; l++)
				{
					startCostCalculation(l);
				}
			}
			else
			{
				startCostCalculation(0);
			}
			this.dirty = false;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000342CC File Offset: 0x000326CC
		public void OnDrawGizmos()
		{
			if (this.pivots != null)
			{
				for (int i = 0; i < this.pivots.Length; i++)
				{
					Gizmos.color = new Color(0.623529434f, 0.368627459f, 0.7607843f, 0.8f);
					if (this.pivots[i] != null && !this.pivots[i].Destroyed)
					{
						Gizmos.DrawCube((Vector3)this.pivots[i].position, Vector3.one);
					}
				}
			}
		}

		// Token: 0x0400049F RID: 1183
		public HeuristicOptimizationMode mode;

		// Token: 0x040004A0 RID: 1184
		public int seed;

		// Token: 0x040004A1 RID: 1185
		public Transform pivotPointRoot;

		// Token: 0x040004A2 RID: 1186
		public int spreadOutCount = 1;

		// Token: 0x040004A3 RID: 1187
		private uint[] costs = new uint[8];

		// Token: 0x040004A4 RID: 1188
		private int maxNodeIndex;

		// Token: 0x040004A5 RID: 1189
		private int pivotCount;

		// Token: 0x040004A6 RID: 1190
		[NonSerialized]
		public bool dirty;

		// Token: 0x040004A7 RID: 1191
		private GraphNode[] pivots;

		// Token: 0x040004A8 RID: 1192
		private uint ra = 12820163u;

		// Token: 0x040004A9 RID: 1193
		private uint rc = 1140671485u;

		// Token: 0x040004AA RID: 1194
		private uint rval;

		// Token: 0x040004AB RID: 1195
		private object lockObj = new object();
	}
}
