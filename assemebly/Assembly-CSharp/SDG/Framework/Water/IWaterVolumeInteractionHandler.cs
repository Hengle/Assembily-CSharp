using System;

namespace SDG.Framework.Water
{
	// Token: 0x02000312 RID: 786
	public interface IWaterVolumeInteractionHandler
	{
		// Token: 0x0600165D RID: 5725
		void waterBeginCollision(WaterVolume volume);

		// Token: 0x0600165E RID: 5726
		void waterEndCollision(WaterVolume volume);
	}
}
