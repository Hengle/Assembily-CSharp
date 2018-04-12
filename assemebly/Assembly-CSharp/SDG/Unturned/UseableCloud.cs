using System;

namespace SDG.Unturned
{
	// Token: 0x020007B2 RID: 1970
	public class UseableCloud : Useable
	{
		// Token: 0x0600394A RID: 14666 RVA: 0x001A8A44 File Offset: 0x001A6E44
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			base.player.movement.itemGravityMultiplier = ((ItemCloudAsset)base.player.equipment.asset).gravity;
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x001A8A91 File Offset: 0x001A6E91
		public override void dequip()
		{
			base.player.movement.itemGravityMultiplier = 1f;
		}
	}
}
