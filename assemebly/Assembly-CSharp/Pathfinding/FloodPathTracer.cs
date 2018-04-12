using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000E1 RID: 225
	public class FloodPathTracer : ABPath
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x000484DC File Offset: 0x000468DC
		[Obsolete("Use the Construct method instead")]
		public FloodPathTracer(Vector3 start, FloodPath flood, OnPathDelegate callbackDelegate)
		{
			throw new Exception("This constructor is obsolete");
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000484EE File Offset: 0x000468EE
		public FloodPathTracer()
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000484F8 File Offset: 0x000468F8
		public static FloodPathTracer Construct(Vector3 start, FloodPath flood, OnPathDelegate callback = null)
		{
			FloodPathTracer path = PathPool<FloodPathTracer>.GetPath();
			path.Setup(start, flood, callback);
			return path;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00048518 File Offset: 0x00046918
		protected void Setup(Vector3 start, FloodPath flood, OnPathDelegate callback)
		{
			this.flood = flood;
			if (flood == null || flood.GetState() < PathState.Returned)
			{
				throw new ArgumentException("You must supply a calculated FloodPath to the 'flood' argument");
			}
			base.Setup(start, flood.originalStartPoint, callback);
			this.nnConstraint = new FloodPathConstraint(flood);
			this.hasEndPoint = false;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0004856A File Offset: 0x0004696A
		public override void Reset()
		{
			base.Reset();
			this.flood = null;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00048579 File Offset: 0x00046979
		protected override void Recycle()
		{
			PathPool<FloodPathTracer>.Recycle(this);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00048584 File Offset: 0x00046984
		public override void Initialize()
		{
			if (this.startNode != null && this.flood.HasPathTo(this.startNode))
			{
				this.Trace(this.startNode);
				base.CompleteState = PathCompleteState.Complete;
			}
			else
			{
				base.Error();
				base.LogError("Could not find valid start node");
			}
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000485DB File Offset: 0x000469DB
		public override void CalculateStep(long targetTick)
		{
			if (!base.IsDone())
			{
				base.Error();
				base.LogError("Something went wrong. At this point the path should be completed");
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000485FC File Offset: 0x000469FC
		public void Trace(GraphNode from)
		{
			GraphNode graphNode = from;
			int num = 0;
			while (graphNode != null)
			{
				this.path.Add(graphNode);
				this.vectorPath.Add((Vector3)graphNode.position);
				graphNode = this.flood.GetParent(graphNode);
				num++;
				if (num > 1024)
				{
					Debug.LogWarning("Inifinity loop? >1024 node path. Remove this message if you really have that long paths (FloodPathTracer.cs, Trace function)");
					break;
				}
			}
		}

		// Token: 0x04000634 RID: 1588
		protected FloodPath flood;
	}
}
