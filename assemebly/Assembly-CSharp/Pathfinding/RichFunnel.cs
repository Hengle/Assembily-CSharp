﻿using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000007 RID: 7
	public class RichFunnel : RichPathPart
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00004528 File Offset: 0x00002928
		public RichFunnel()
		{
			this.left = ListPool<Vector3>.Claim();
			this.right = ListPool<Vector3>.Claim();
			this.nodes = new List<TriangleMeshNode>();
			this.graph = null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004576 File Offset: 0x00002976
		public RichFunnel Initialize(RichPath path, IFunnelGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (this.graph != null)
			{
				throw new InvalidOperationException("Trying to initialize an already initialized object. " + graph);
			}
			this.graph = graph;
			this.path = path;
			return this;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000045B4 File Offset: 0x000029B4
		public override void OnEnterPool()
		{
			this.left.Clear();
			this.right.Clear();
			this.nodes.Clear();
			this.graph = null;
			this.currentNode = 0;
			this.tmpCounter = 0;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000045EC File Offset: 0x000029EC
		public void BuildFunnelCorridor(List<GraphNode> nodes, int start, int end)
		{
			this.exactStart = (nodes[start] as MeshNode).ClosestPointOnNode(this.exactStart);
			this.exactEnd = (nodes[end] as MeshNode).ClosestPointOnNode(this.exactEnd);
			this.left.Clear();
			this.right.Clear();
			this.left.Add(this.exactStart);
			this.right.Add(this.exactStart);
			this.nodes.Clear();
			IRaycastableGraph raycastableGraph = this.graph as IRaycastableGraph;
			if (raycastableGraph != null && this.funnelSimplificationMode != RichFunnel.FunnelSimplification.None)
			{
				List<GraphNode> list = ListPool<GraphNode>.Claim(end - start);
				RichFunnel.FunnelSimplification funnelSimplification = this.funnelSimplificationMode;
				if (funnelSimplification != RichFunnel.FunnelSimplification.Iterative)
				{
					if (funnelSimplification != RichFunnel.FunnelSimplification.RecursiveBinary)
					{
						if (funnelSimplification == RichFunnel.FunnelSimplification.RecursiveTrinary)
						{
							RichFunnel.SimplifyPath3(raycastableGraph, nodes, start, end, list, this.exactStart, this.exactEnd, 0);
						}
					}
					else
					{
						RichFunnel.SimplifyPath2(raycastableGraph, nodes, start, end, list, this.exactStart, this.exactEnd);
					}
				}
				else
				{
					this.SimplifyPath(raycastableGraph, nodes, start, end, list, this.exactStart, this.exactEnd);
				}
				if (this.nodes.Capacity < list.Count)
				{
					this.nodes.Capacity = list.Count;
				}
				for (int i = 0; i < list.Count; i++)
				{
					TriangleMeshNode triangleMeshNode = list[i] as TriangleMeshNode;
					if (triangleMeshNode != null)
					{
						this.nodes.Add(triangleMeshNode);
					}
				}
				ListPool<GraphNode>.Release(list);
			}
			else
			{
				if (this.nodes.Capacity < end - start)
				{
					this.nodes.Capacity = end - start;
				}
				for (int j = start; j <= end; j++)
				{
					TriangleMeshNode triangleMeshNode2 = nodes[j] as TriangleMeshNode;
					if (triangleMeshNode2 != null)
					{
						this.nodes.Add(triangleMeshNode2);
					}
				}
			}
			for (int k = 0; k < this.nodes.Count - 1; k++)
			{
				this.nodes[k].GetPortal(this.nodes[k + 1], this.left, this.right, false);
			}
			this.left.Add(this.exactEnd);
			this.right.Add(this.exactEnd);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004848 File Offset: 0x00002C48
		public static void SimplifyPath3(IRaycastableGraph rcg, List<GraphNode> nodes, int start, int end, List<GraphNode> result, Vector3 startPoint, Vector3 endPoint, int depth = 0)
		{
			if (start == end)
			{
				result.Add(nodes[start]);
				return;
			}
			if (start + 1 == end)
			{
				result.Add(nodes[start]);
				result.Add(nodes[end]);
				return;
			}
			int count = result.Count;
			GraphHitInfo graphHitInfo;
			if (rcg.Linecast(startPoint, endPoint, nodes[start], out graphHitInfo, result) || result[result.Count - 1] != nodes[end])
			{
				result.RemoveRange(count, result.Count - count);
				int num = 0;
				float num2 = 0f;
				for (int i = start + 1; i < end - 1; i++)
				{
					float num3 = AstarMath.DistancePointSegmentStrict(startPoint, endPoint, (Vector3)nodes[i].position);
					if (num3 > num2)
					{
						num = i;
						num2 = num3;
					}
				}
				int num4 = (num + start) / 2;
				int num5 = (num + end) / 2;
				if (num4 == num5)
				{
					RichFunnel.SimplifyPath3(rcg, nodes, start, num4, result, startPoint, (Vector3)nodes[num4].position, 0);
					result.RemoveAt(result.Count - 1);
					RichFunnel.SimplifyPath3(rcg, nodes, num4, end, result, (Vector3)nodes[num4].position, endPoint, depth + 1);
				}
				else
				{
					RichFunnel.SimplifyPath3(rcg, nodes, start, num4, result, startPoint, (Vector3)nodes[num4].position, depth + 1);
					result.RemoveAt(result.Count - 1);
					RichFunnel.SimplifyPath3(rcg, nodes, num4, num5, result, (Vector3)nodes[num4].position, (Vector3)nodes[num5].position, depth + 1);
					result.RemoveAt(result.Count - 1);
					RichFunnel.SimplifyPath3(rcg, nodes, num5, end, result, (Vector3)nodes[num5].position, endPoint, depth + 1);
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004A48 File Offset: 0x00002E48
		public static void SimplifyPath2(IRaycastableGraph rcg, List<GraphNode> nodes, int start, int end, List<GraphNode> result, Vector3 startPoint, Vector3 endPoint)
		{
			int count = result.Count;
			if (end <= start + 1)
			{
				result.Add(nodes[start]);
				result.Add(nodes[end]);
				return;
			}
			GraphHitInfo graphHitInfo;
			if (rcg.Linecast(startPoint, endPoint, nodes[start], out graphHitInfo, result) || result[result.Count - 1] != nodes[end])
			{
				result.RemoveRange(count, result.Count - count);
				int num = -1;
				float num2 = float.PositiveInfinity;
				for (int i = start + 1; i < end; i++)
				{
					float num3 = AstarMath.DistancePointSegmentStrict(startPoint, endPoint, (Vector3)nodes[i].position);
					if (num == -1 || num3 < num2)
					{
						num = i;
						num2 = num3;
					}
				}
				RichFunnel.SimplifyPath2(rcg, nodes, start, num, result, startPoint, (Vector3)nodes[num].position);
				result.RemoveAt(result.Count - 1);
				RichFunnel.SimplifyPath2(rcg, nodes, num, end, result, (Vector3)nodes[num].position, endPoint);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004B68 File Offset: 0x00002F68
		public void SimplifyPath(IRaycastableGraph graph, List<GraphNode> nodes, int start, int end, List<GraphNode> result, Vector3 startPoint, Vector3 endPoint)
		{
			if (start > end)
			{
				throw new ArgumentException("start >= end");
			}
			if (graph == null)
			{
				throw new InvalidOperationException("graph is not a IRaycastableGraph");
			}
			int num = start;
			int num2 = 0;
			while (num2++ <= 1000)
			{
				if (start == end)
				{
					result.Add(nodes[end]);
					return;
				}
				int count = result.Count;
				int i = end + 1;
				int num3 = start + 1;
				bool flag = false;
				while (i > num3 + 1)
				{
					int num4 = (i + num3) / 2;
					Vector3 start2 = (start != num) ? ((Vector3)nodes[start].position) : startPoint;
					Vector3 end2 = (num4 != end) ? ((Vector3)nodes[num4].position) : endPoint;
					GraphHitInfo graphHitInfo;
					if (graph.Linecast(start2, end2, nodes[start], out graphHitInfo))
					{
						i = num4;
					}
					else
					{
						flag = true;
						num3 = num4;
					}
				}
				if (!flag)
				{
					result.Add(nodes[start]);
					start = num3;
				}
				else
				{
					Vector3 start3 = (start != num) ? ((Vector3)nodes[start].position) : startPoint;
					Vector3 end3 = (num3 != end) ? ((Vector3)nodes[num3].position) : endPoint;
					GraphHitInfo graphHitInfo2;
					graph.Linecast(start3, end3, nodes[start], out graphHitInfo2, result);
					long num5 = 0L;
					long num6 = 0L;
					for (int j = start; j <= num3; j++)
					{
						num5 += (long)((ulong)nodes[j].Penalty + (ulong)((long)((!(this.path.seeker != null)) ? 0 : this.path.seeker.tagPenalties[(int)((UIntPtr)nodes[j].Tag)])));
					}
					for (int k = count; k < result.Count; k++)
					{
						num6 += (long)((ulong)result[k].Penalty + (ulong)((long)((!(this.path.seeker != null)) ? 0 : this.path.seeker.tagPenalties[(int)((UIntPtr)result[k].Tag)])));
					}
					if ((double)num5 * 1.4 * (double)(num3 - start + 1) < (double)(num6 * (long)(result.Count - count)) || result[result.Count - 1] != nodes[num3])
					{
						result.RemoveRange(count, result.Count - count);
						result.Add(nodes[start]);
						start++;
					}
					else
					{
						result.RemoveAt(result.Count - 1);
						start = num3;
					}
				}
			}
			Debug.LogError("!!!");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004E50 File Offset: 0x00003250
		public void UpdateFunnelCorridor(int splitIndex, TriangleMeshNode prefix)
		{
			if (splitIndex > 0)
			{
				this.nodes.RemoveRange(0, splitIndex - 1);
				this.nodes[0] = prefix;
			}
			else
			{
				this.nodes.Insert(0, prefix);
			}
			this.left.Clear();
			this.right.Clear();
			this.left.Add(this.exactStart);
			this.right.Add(this.exactStart);
			for (int i = 0; i < this.nodes.Count - 1; i++)
			{
				this.nodes[i].GetPortal(this.nodes[i + 1], this.left, this.right, false);
			}
			this.left.Add(this.exactEnd);
			this.right.Add(this.exactEnd);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004F38 File Offset: 0x00003338
		public Vector3 Update(Vector3 position, List<Vector3> buffer, int numCorners, out bool lastCorner, out bool requiresRepath)
		{
			lastCorner = false;
			requiresRepath = false;
			Int3 @int = (Int3)position;
			if (this.nodes[this.currentNode].Destroyed)
			{
				requiresRepath = true;
				lastCorner = false;
				buffer.Add(position);
				return position;
			}
			if (this.nodes[this.currentNode].ContainsPoint(@int))
			{
				if (this.tmpCounter >= 10)
				{
					this.tmpCounter = 0;
					int i = 0;
					int count = this.nodes.Count;
					while (i < count)
					{
						if (this.nodes[i].Destroyed)
						{
							requiresRepath = true;
							break;
						}
						i++;
					}
				}
				else
				{
					this.tmpCounter++;
				}
			}
			else
			{
				bool flag = false;
				int num = this.currentNode + 1;
				int num2 = Math.Min(this.currentNode + 3, this.nodes.Count);
				while (num < num2 && !flag)
				{
					if (this.nodes[num].Destroyed)
					{
						requiresRepath = true;
						lastCorner = false;
						buffer.Add(position);
						return position;
					}
					if (this.nodes[num].ContainsPoint(@int))
					{
						this.currentNode = num;
						flag = true;
					}
					num++;
				}
				int num3 = this.currentNode - 1;
				int num4 = Math.Max(this.currentNode - 3, 0);
				while (num3 > num4 && !flag)
				{
					if (this.nodes[num3].Destroyed)
					{
						requiresRepath = true;
						lastCorner = false;
						buffer.Add(position);
						return position;
					}
					if (this.nodes[num3].ContainsPoint(@int))
					{
						this.currentNode = num3;
						flag = true;
					}
					num3--;
				}
				int num5 = 0;
				float num6 = float.PositiveInfinity;
				Vector3 vector = Vector3.zero;
				int num7 = 0;
				int count2 = this.nodes.Count;
				while (num7 < count2 && !flag)
				{
					if (this.nodes[num7].Destroyed)
					{
						requiresRepath = true;
						lastCorner = false;
						buffer.Add(position);
						return position;
					}
					if (this.nodes[num7].ContainsPoint(@int))
					{
						this.currentNode = num7;
						flag = true;
						vector = position;
					}
					else
					{
						Vector3 vector2 = this.nodes[num7].ClosestPointOnNodeXZ(position);
						float sqrMagnitude = (vector2 - position).sqrMagnitude;
						if (sqrMagnitude < num6)
						{
							num6 = sqrMagnitude;
							num5 = num7;
							vector = vector2;
						}
					}
					num7++;
				}
				this.tmpCounter = 0;
				int j = 0;
				int count3 = this.nodes.Count;
				while (j < count3)
				{
					if (this.nodes[j].Destroyed)
					{
						requiresRepath = true;
						break;
					}
					j++;
				}
				if (!flag)
				{
					vector.y = position.y;
					MeshNode containingPoint = null;
					int containingIndex = this.nodes.Count - 1;
					Int3 i3Copy = @int;
					GraphNodeDelegate del = delegate(GraphNode node)
					{
						if ((containingIndex <= 0 || node != this.nodes[containingIndex - 1]) && (containingIndex >= this.nodes.Count - 1 || node != this.nodes[containingIndex + 1]))
						{
							MeshNode meshNode2 = node as MeshNode;
							if (meshNode2 != null && meshNode2.ContainsPoint(i3Copy))
							{
								containingPoint = meshNode2;
							}
						}
					};
					while (containingIndex >= 0 && containingPoint == null)
					{
						MeshNode meshNode = this.nodes[containingIndex];
						meshNode.GetConnections(del);
						containingIndex--;
					}
					if (containingPoint != null)
					{
						containingIndex++;
						this.exactStart = position;
						this.UpdateFunnelCorridor(containingIndex, containingPoint as TriangleMeshNode);
						this.currentNode = 0;
					}
					else
					{
						position = vector;
						this.currentNode = num5;
					}
				}
			}
			this.currentPosition = position;
			if (!this.FindNextCorners(position, this.currentNode, buffer, numCorners, out lastCorner))
			{
				Debug.LogError("Oh oh");
				buffer.Add(position);
				return position;
			}
			return position;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000534C File Offset: 0x0000374C
		public void FindWalls(List<Vector3> wallBuffer, float range)
		{
			this.FindWalls(this.currentNode, wallBuffer, this.currentPosition, range);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00005364 File Offset: 0x00003764
		private void FindWalls(int nodeIndex, List<Vector3> wallBuffer, Vector3 position, float range)
		{
			if (range <= 0f)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			range *= range;
			position.y = 0f;
			int num = 0;
			while (!flag || !flag2)
			{
				if (num >= 0 || !flag)
				{
					if (num <= 0 || !flag2)
					{
						if (num < 0 && nodeIndex + num < 0)
						{
							flag = true;
						}
						else if (num > 0 && nodeIndex + num >= this.nodes.Count)
						{
							flag2 = true;
						}
						else
						{
							TriangleMeshNode triangleMeshNode = (nodeIndex + num - 1 >= 0) ? this.nodes[nodeIndex + num - 1] : null;
							TriangleMeshNode triangleMeshNode2 = this.nodes[nodeIndex + num];
							TriangleMeshNode triangleMeshNode3 = (nodeIndex + num + 1 < this.nodes.Count) ? this.nodes[nodeIndex + num + 1] : null;
							if (triangleMeshNode2.Destroyed)
							{
								break;
							}
							if ((triangleMeshNode2.ClosestPointOnNodeXZ(position) - position).sqrMagnitude > range)
							{
								if (num < 0)
								{
									flag = true;
								}
								else
								{
									flag2 = true;
								}
							}
							else
							{
								for (int i = 0; i < 3; i++)
								{
									this.triBuffer[i] = 0;
								}
								for (int j = 0; j < triangleMeshNode2.connections.Length; j++)
								{
									TriangleMeshNode triangleMeshNode4 = triangleMeshNode2.connections[j] as TriangleMeshNode;
									if (triangleMeshNode4 != null)
									{
										int num2 = -1;
										for (int k = 0; k < 3; k++)
										{
											for (int l = 0; l < 3; l++)
											{
												if (triangleMeshNode2.GetVertex(k) == triangleMeshNode4.GetVertex((l + 1) % 3) && triangleMeshNode2.GetVertex((k + 1) % 3) == triangleMeshNode4.GetVertex(l))
												{
													num2 = k;
													k = 3;
													break;
												}
											}
										}
										if (num2 != -1)
										{
											this.triBuffer[num2] = ((triangleMeshNode4 != triangleMeshNode && triangleMeshNode4 != triangleMeshNode3) ? 1 : 2);
										}
									}
								}
								for (int m = 0; m < 3; m++)
								{
									if (this.triBuffer[m] == 0)
									{
										wallBuffer.Add((Vector3)triangleMeshNode2.GetVertex(m));
										wallBuffer.Add((Vector3)triangleMeshNode2.GetVertex((m + 1) % 3));
									}
								}
							}
						}
					}
				}
				num = ((num >= 0) ? (-num - 1) : (-num));
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00005610 File Offset: 0x00003A10
		public bool FindNextCorners(Vector3 origin, int startIndex, List<Vector3> funnelPath, int numCorners, out bool lastCorner)
		{
			lastCorner = false;
			if (this.left == null)
			{
				throw new ArgumentNullException("left");
			}
			if (this.right == null)
			{
				throw new ArgumentNullException("right");
			}
			if (funnelPath == null)
			{
				throw new ArgumentNullException("funnelPath");
			}
			if (this.left.Count != this.right.Count)
			{
				throw new ArgumentException("left and right lists must have equal length");
			}
			int count = this.left.Count;
			if (count == 0)
			{
				throw new ArgumentException("no diagonals");
			}
			if (count - startIndex < 3)
			{
				funnelPath.Add(this.left[count - 1]);
				lastCorner = true;
				return true;
			}
			while (this.left[startIndex + 1] == this.left[startIndex + 2] && this.right[startIndex + 1] == this.right[startIndex + 2])
			{
				startIndex++;
				if (count - startIndex <= 3)
				{
					return false;
				}
			}
			Vector3 vector = this.left[startIndex + 2];
			if (vector == this.left[startIndex + 1])
			{
				vector = this.right[startIndex + 2];
			}
			while (Polygon.IsColinear(origin, this.left[startIndex + 1], this.right[startIndex + 1]) || Polygon.Left(this.left[startIndex + 1], this.right[startIndex + 1], vector) == Polygon.Left(this.left[startIndex + 1], this.right[startIndex + 1], origin))
			{
				startIndex++;
				if (count - startIndex < 3)
				{
					funnelPath.Add(this.left[count - 1]);
					lastCorner = true;
					return true;
				}
				vector = this.left[startIndex + 2];
				if (vector == this.left[startIndex + 1])
				{
					vector = this.right[startIndex + 2];
				}
			}
			Vector3 vector2 = origin;
			Vector3 vector3 = this.left[startIndex + 1];
			Vector3 vector4 = this.right[startIndex + 1];
			int num = startIndex + 1;
			int num2 = startIndex + 1;
			int i = startIndex + 2;
			while (i < count)
			{
				if (funnelPath.Count >= numCorners)
				{
					return true;
				}
				if (funnelPath.Count > 2000)
				{
					Debug.LogWarning("Avoiding infinite loop. Remove this check if you have this long paths.");
					break;
				}
				Vector3 vector5 = this.left[i];
				Vector3 vector6 = this.right[i];
				if (Polygon.TriangleArea2(vector2, vector4, vector6) < 0f)
				{
					goto IL_2FB;
				}
				if (vector2 == vector4 || Polygon.TriangleArea2(vector2, vector3, vector6) <= 0f)
				{
					vector4 = vector6;
					num = i;
					goto IL_2FB;
				}
				funnelPath.Add(vector3);
				vector2 = vector3;
				int num3 = num2;
				vector3 = vector2;
				vector4 = vector2;
				num2 = num3;
				num = num3;
				i = num3;
				IL_35F:
				i++;
				continue;
				IL_2FB:
				if (Polygon.TriangleArea2(vector2, vector3, vector5) > 0f)
				{
					goto IL_35F;
				}
				if (vector2 == vector3 || Polygon.TriangleArea2(vector2, vector4, vector5) >= 0f)
				{
					vector3 = vector5;
					num2 = i;
					goto IL_35F;
				}
				funnelPath.Add(vector4);
				vector2 = vector4;
				num3 = num;
				vector3 = vector2;
				vector4 = vector2;
				num2 = num3;
				num = num3;
				i = num3;
				goto IL_35F;
			}
			lastCorner = true;
			funnelPath.Add(this.left[count - 1]);
			return true;
		}

		// Token: 0x04000050 RID: 80
		private List<Vector3> left;

		// Token: 0x04000051 RID: 81
		private List<Vector3> right;

		// Token: 0x04000052 RID: 82
		private List<TriangleMeshNode> nodes;

		// Token: 0x04000053 RID: 83
		public Vector3 exactStart;

		// Token: 0x04000054 RID: 84
		public Vector3 exactEnd;

		// Token: 0x04000055 RID: 85
		private IFunnelGraph graph;

		// Token: 0x04000056 RID: 86
		private int currentNode;

		// Token: 0x04000057 RID: 87
		private Vector3 currentPosition;

		// Token: 0x04000058 RID: 88
		private int tmpCounter;

		// Token: 0x04000059 RID: 89
		private RichPath path;

		// Token: 0x0400005A RID: 90
		private int[] triBuffer = new int[3];

		// Token: 0x0400005B RID: 91
		public RichFunnel.FunnelSimplification funnelSimplificationMode = RichFunnel.FunnelSimplification.Iterative;

		// Token: 0x02000008 RID: 8
		public enum FunnelSimplification
		{
			// Token: 0x0400005D RID: 93
			None,
			// Token: 0x0400005E RID: 94
			Iterative,
			// Token: 0x0400005F RID: 95
			RecursiveBinary,
			// Token: 0x04000060 RID: 96
			RecursiveTrinary
		}
	}
}
