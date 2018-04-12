using System;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004D RID: 77
	internal class GraphMeta
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0001A644 File Offset: 0x00018A44
		public Type GetGraphType(int i)
		{
			if (this.typeNames[i] == null)
			{
				return null;
			}
			Type type = Type.GetType(this.typeNames[i]);
			if (!object.Equals(type, null))
			{
				return type;
			}
			throw new Exception("No graph of type '" + this.typeNames[i] + "' could be created, type does not exist");
		}

		// Token: 0x04000275 RID: 629
		public Version version;

		// Token: 0x04000276 RID: 630
		public int graphs;

		// Token: 0x04000277 RID: 631
		public string[] guids;

		// Token: 0x04000278 RID: 632
		public string[] typeNames;

		// Token: 0x04000279 RID: 633
		public int[] nodeCounts;
	}
}
