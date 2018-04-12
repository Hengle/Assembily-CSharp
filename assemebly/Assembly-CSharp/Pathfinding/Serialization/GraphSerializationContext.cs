using System;
using System.IO;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004B RID: 75
	public class GraphSerializationContext
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0001923C File Offset: 0x0001763C
		public GraphSerializationContext(BinaryReader reader, GraphNode[] id2NodeMapping, int graphIndex)
		{
			this.reader = reader;
			this.id2NodeMapping = id2NodeMapping;
			this.graphIndex = graphIndex;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00019259 File Offset: 0x00017659
		public GraphSerializationContext(BinaryWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00019268 File Offset: 0x00017668
		public int GetNodeIdentifier(GraphNode node)
		{
			return (node != null) ? node.NodeIndex : -1;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001927C File Offset: 0x0001767C
		public GraphNode GetNodeFromIdentifier(int id)
		{
			if (this.id2NodeMapping == null)
			{
				throw new Exception("Calling GetNodeFromIdentifier when serializing");
			}
			if (id == -1)
			{
				return null;
			}
			GraphNode graphNode = this.id2NodeMapping[id];
			if (graphNode == null)
			{
				throw new Exception("Invalid id");
			}
			return graphNode;
		}

		// Token: 0x04000264 RID: 612
		private readonly GraphNode[] id2NodeMapping;

		// Token: 0x04000265 RID: 613
		public readonly BinaryReader reader;

		// Token: 0x04000266 RID: 614
		public readonly BinaryWriter writer;

		// Token: 0x04000267 RID: 615
		public readonly int graphIndex;
	}
}
