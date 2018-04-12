using System;

namespace SDG.Unturned
{
	// Token: 0x020007C2 RID: 1986
	public class UseableWalkieTalkie : Useable
	{
		// Token: 0x06003A22 RID: 14882 RVA: 0x001BD9C9 File Offset: 0x001BBDC9
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			base.player.voice.hasWalkieTalkie = true;
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x001BD9F2 File Offset: 0x001BBDF2
		public override void dequip()
		{
			base.player.voice.hasWalkieTalkie = false;
		}
	}
}
