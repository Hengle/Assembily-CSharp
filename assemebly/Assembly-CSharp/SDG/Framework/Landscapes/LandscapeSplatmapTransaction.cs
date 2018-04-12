using System;
using SDG.Framework.Devkit.Transactions;

namespace SDG.Framework.Landscapes
{
	// Token: 0x020001E2 RID: 482
	public class LandscapeSplatmapTransaction : IDevkitTransaction
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x00063CF7 File Offset: 0x000620F7
		public LandscapeSplatmapTransaction(LandscapeTile newTile)
		{
			this.tile = newTile;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00063D06 File Offset: 0x00062106
		public bool delta
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00063D0C File Offset: 0x0006210C
		public void undo()
		{
			if (this.tile == null)
			{
				return;
			}
			float[,,] sourceSplatmap = this.tile.sourceSplatmap;
			this.tile.sourceSplatmap = this.splatmapCopy;
			this.splatmapCopy = sourceSplatmap;
			this.tile.data.SetAlphamaps(0, 0, this.tile.sourceSplatmap);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00063D66 File Offset: 0x00062166
		public void redo()
		{
			this.undo();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00063D70 File Offset: 0x00062170
		public void begin()
		{
			this.splatmapCopy = LandscapeSplatmapCopyPool.claim();
			for (int i = 0; i < Landscape.SPLATMAP_RESOLUTION; i++)
			{
				for (int j = 0; j < Landscape.SPLATMAP_RESOLUTION; j++)
				{
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						this.splatmapCopy[i, j, k] = this.tile.sourceSplatmap[i, j, k];
					}
				}
			}
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x00063DEB File Offset: 0x000621EB
		public void end()
		{
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00063DED File Offset: 0x000621ED
		public void forget()
		{
			LandscapeSplatmapCopyPool.release(this.splatmapCopy);
		}

		// Token: 0x04000931 RID: 2353
		protected LandscapeTile tile;

		// Token: 0x04000932 RID: 2354
		protected float[,,] splatmapCopy;
	}
}
