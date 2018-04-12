using System;
using SDG.Framework.Devkit.Transactions;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001DC RID: 476
	public class LandscapeHeightmapTransaction : IDevkitTransaction
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x0006335C File Offset: 0x0006175C
		public LandscapeHeightmapTransaction(LandscapeTile newTile)
		{
			this.tile = newTile;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0006336B File Offset: 0x0006176B
		public bool delta
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00063370 File Offset: 0x00061770
		public void undo()
		{
			if (this.tile == null)
			{
				return;
			}
			float[,] sourceHeightmap = this.tile.sourceHeightmap;
			this.tile.sourceHeightmap = this.heightmapCopy;
			this.heightmapCopy = sourceHeightmap;
			this.tile.data.SetHeightsDelayLOD(0, 0, this.tile.sourceHeightmap);
			this.tile.terrain.ApplyDelayedHeightmapModification();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000633DA File Offset: 0x000617DA
		public void redo()
		{
			this.undo();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000633E4 File Offset: 0x000617E4
		public void begin()
		{
			this.heightmapCopy = LandscapeHeightmapCopyPool.claim();
			for (int i = 0; i < Landscape.HEIGHTMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.HEIGHTMAP_RESOLUTION; j++)
				{
					this.heightmapCopy[i, j] = this.tile.sourceHeightmap[i, j];
				}
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00063447 File Offset: 0x00061847
		public void end()
		{
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00063449 File Offset: 0x00061849
		public void forget()
		{
			LandscapeHeightmapCopyPool.release(this.heightmapCopy);
		}

		// Token: 0x04000920 RID: 2336
		protected LandscapeTile tile;

		// Token: 0x04000921 RID: 2337
		protected float[,] heightmapCopy;
	}
}
