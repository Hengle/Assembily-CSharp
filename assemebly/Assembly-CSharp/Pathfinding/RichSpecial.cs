using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000009 RID: 9
	public class RichSpecial : RichPathPart
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00005A4B File Offset: 0x00003E4B
		public override void OnEnterPool()
		{
			this.nodeLink = null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005A54 File Offset: 0x00003E54
		public RichSpecial Initialize(NodeLink2 nodeLink, GraphNode first)
		{
			this.nodeLink = nodeLink;
			if (first == nodeLink.StartNode)
			{
				this.first = nodeLink.StartTransform;
				this.second = nodeLink.EndTransform;
				this.reverse = false;
			}
			else
			{
				this.first = nodeLink.EndTransform;
				this.second = nodeLink.StartTransform;
				this.reverse = true;
			}
			return this;
		}

		// Token: 0x04000061 RID: 97
		public NodeLink2 nodeLink;

		// Token: 0x04000062 RID: 98
		public Transform first;

		// Token: 0x04000063 RID: 99
		public Transform second;

		// Token: 0x04000064 RID: 100
		public bool reverse;
	}
}
