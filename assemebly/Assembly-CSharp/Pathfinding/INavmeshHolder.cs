using System;

namespace Pathfinding
{
	// Token: 0x02000092 RID: 146
	public interface INavmeshHolder
	{
		// Token: 0x060004FA RID: 1274
		Int3 GetVertex(int i);

		// Token: 0x060004FB RID: 1275
		int GetVertexArrayIndex(int index);

		// Token: 0x060004FC RID: 1276
		void GetTileCoordinates(int tileIndex, out int x, out int z);
	}
}
