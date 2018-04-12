using System;
using Pathfinding.Serialization;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000081 RID: 129
	public abstract class NavGraph
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00021370 File Offset: 0x0001F770
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000213B5 File Offset: 0x0001F7B5
		[JsonMember]
		public Pathfinding.Util.Guid guid
		{
			get
			{
				if (this._sguid == null || this._sguid.Length != 16)
				{
					this._sguid = Pathfinding.Util.Guid.NewGuid().ToByteArray();
				}
				return new Pathfinding.Util.Guid(this._sguid);
			}
			set
			{
				this._sguid = value.ToByteArray();
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000213C4 File Offset: 0x0001F7C4
		public virtual int CountNodes()
		{
			int count = 0;
			GraphNodeDelegateCancelable del = delegate(GraphNode node)
			{
				count++;
				return true;
			};
			this.GetNodes(del);
			return count;
		}

		// Token: 0x0600041A RID: 1050
		public abstract void GetNodes(GraphNodeDelegateCancelable del);

		// Token: 0x0600041B RID: 1051 RVA: 0x000213F8 File Offset: 0x0001F7F8
		public void SetMatrix(Matrix4x4 m)
		{
			this.matrix = m;
			this.inverseMatrix = m.inverse;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0002140E File Offset: 0x0001F80E
		public virtual void CreateNodes(int number)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00021418 File Offset: 0x0001F818
		public virtual void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			Matrix4x4 inverse = oldMatrix.inverse;
			Matrix4x4 m = inverse * newMatrix;
			this.GetNodes(delegate(GraphNode node)
			{
				node.position = (Int3)m.MultiplyPoint((Vector3)node.position);
				return true;
			});
			this.SetMatrix(newMatrix);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00021459 File Offset: 0x0001F859
		public NNInfo GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00021467 File Offset: 0x0001F867
		public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00021474 File Offset: 0x0001F874
		public virtual NNInfo GetNearest(Vector3 position, NNConstraint constraint, GraphNode hint)
		{
			float maxDistSqr = (!constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistanceSqr;
			float minDist = float.PositiveInfinity;
			GraphNode minNode = null;
			float minConstDist = float.PositiveInfinity;
			GraphNode minConstNode = null;
			this.GetNodes(delegate(GraphNode node)
			{
				float sqrMagnitude = (position - (Vector3)node.position).sqrMagnitude;
				if (sqrMagnitude < minDist)
				{
					minDist = sqrMagnitude;
					minNode = node;
				}
				if (sqrMagnitude < minConstDist && sqrMagnitude < maxDistSqr && constraint.Suitable(node))
				{
					minConstDist = sqrMagnitude;
					minConstNode = node;
				}
				return true;
			});
			NNInfo result = new NNInfo(minNode);
			result.constrainedNode = minConstNode;
			if (minConstNode != null)
			{
				result.constClampedPosition = (Vector3)minConstNode.position;
			}
			else if (minNode != null)
			{
				result.constrainedNode = minNode;
				result.constClampedPosition = (Vector3)minNode.position;
			}
			return result;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00021566 File Offset: 0x0001F966
		public virtual NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00021570 File Offset: 0x0001F970
		public virtual void Awake()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00021572 File Offset: 0x0001F972
		public void SafeOnDestroy()
		{
			AstarPath.RegisterSafeUpdate(new OnVoidDelegate(this.OnDestroy));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00021586 File Offset: 0x0001F986
		public virtual void OnDestroy()
		{
			this.GetNodes(delegate(GraphNode node)
			{
				node.Destroy();
				return true;
			});
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000215AC File Offset: 0x0001F9AC
		public void ScanGraph()
		{
			if (AstarPath.OnPreScan != null)
			{
				AstarPath.OnPreScan(AstarPath.active);
			}
			if (AstarPath.OnGraphPreScan != null)
			{
				AstarPath.OnGraphPreScan(this);
			}
			this.ScanInternal();
			if (AstarPath.OnGraphPostScan != null)
			{
				AstarPath.OnGraphPostScan(this);
			}
			if (AstarPath.OnPostScan != null)
			{
				AstarPath.OnPostScan(AstarPath.active);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0002161B File Offset: 0x0001FA1B
		[Obsolete("Please use AstarPath.active.Scan or if you really want this.ScanInternal which has the same functionality as this method had")]
		public void Scan()
		{
			throw new Exception("This method is deprecated. Please use AstarPath.active.Scan or if you really want this.ScanInternal which has the same functionality as this method had.");
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00021627 File Offset: 0x0001FA27
		public void ScanInternal()
		{
			this.ScanInternal(null);
		}

		// Token: 0x06000428 RID: 1064
		public abstract void ScanInternal(OnScanStatus statusCallback);

		// Token: 0x06000429 RID: 1065 RVA: 0x00021630 File Offset: 0x0001FA30
		public virtual Color NodeColor(GraphNode node, PathHandler data)
		{
			Color result = AstarColor.NodeConnection;
			bool flag = false;
			if (node == null)
			{
				return AstarColor.NodeConnection;
			}
			GraphDebugMode debugMode = AstarPath.active.debugMode;
			if (debugMode != GraphDebugMode.Areas)
			{
				if (debugMode != GraphDebugMode.Penalty)
				{
					if (debugMode == GraphDebugMode.Tags)
					{
						result = AstarMath.IntToColor((int)node.Tag, 0.5f);
						flag = true;
					}
				}
				else
				{
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, node.Penalty / AstarPath.active.debugRoof);
					flag = true;
				}
			}
			else
			{
				result = AstarColor.GetAreaColor(node.Area);
				flag = true;
			}
			if (!flag)
			{
				if (data == null)
				{
					return AstarColor.NodeConnection;
				}
				PathNode pathNode = data.GetPathNode(node);
				GraphDebugMode debugMode2 = AstarPath.active.debugMode;
				if (debugMode2 != GraphDebugMode.G)
				{
					if (debugMode2 != GraphDebugMode.H)
					{
						if (debugMode2 == GraphDebugMode.F)
						{
							result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, pathNode.F / AstarPath.active.debugRoof);
						}
					}
					else
					{
						result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, pathNode.H / AstarPath.active.debugRoof);
					}
				}
				else
				{
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, pathNode.G / AstarPath.active.debugRoof);
				}
			}
			result.a *= 0.5f;
			return result;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0002179D File Offset: 0x0001FB9D
		public virtual void SerializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0002179F File Offset: 0x0001FB9F
		public virtual void DeserializeExtraInfo(GraphSerializationContext ctx)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000217A1 File Offset: 0x0001FBA1
		public virtual void PostDeserialization()
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000217A4 File Offset: 0x0001FBA4
		public bool InSearchTree(GraphNode node, Path path)
		{
			if (path == null || path.pathHandler == null)
			{
				return true;
			}
			PathNode pathNode = path.pathHandler.GetPathNode(node);
			return pathNode.pathID == path.pathID;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000217E0 File Offset: 0x0001FBE0
		public virtual void OnDrawGizmos(bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			PathHandler data = AstarPath.active.debugPathData;
			GraphNode node = null;
			GraphNodeDelegate del = delegate(GraphNode o)
			{
				Gizmos.DrawLine((Vector3)node.position, (Vector3)o.position);
			};
			this.GetNodes(delegate(GraphNode _node)
			{
				node = _node;
				Gizmos.color = this.NodeColor(node, AstarPath.active.debugPathData);
				if (AstarPath.active.showSearchTree && !this.InSearchTree(node, AstarPath.active.debugPath))
				{
					return true;
				}
				PathNode pathNode = (data == null) ? null : data.GetPathNode(node);
				if (AstarPath.active.showSearchTree && pathNode != null && pathNode.parent != null)
				{
					Gizmos.DrawLine((Vector3)node.position, (Vector3)pathNode.parent.node.position);
				}
				else
				{
					node.GetConnections(del);
				}
				return true;
			});
		}

		// Token: 0x04000394 RID: 916
		public byte[] _sguid;

		// Token: 0x04000395 RID: 917
		public AstarPath active;

		// Token: 0x04000396 RID: 918
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x04000397 RID: 919
		[JsonMember]
		public bool open;

		// Token: 0x04000398 RID: 920
		public uint graphIndex;

		// Token: 0x04000399 RID: 921
		[JsonMember]
		public string name;

		// Token: 0x0400039A RID: 922
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x0400039B RID: 923
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x0400039C RID: 924
		[JsonMember]
		public Matrix4x4 matrix;

		// Token: 0x0400039D RID: 925
		public Matrix4x4 inverseMatrix;
	}
}
