using System;

namespace SDG.Unturned
{
	// Token: 0x02000707 RID: 1799
	public class SleekToggle : Sleek
	{
		// Token: 0x0600334D RID: 13133 RVA: 0x0014D3DE File Offset: 0x0014B7DE
		public SleekToggle()
		{
			base.init();
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x0014D3EC File Offset: 0x0014B7EC
		public override void draw(bool ignoreCulling)
		{
			bool flag = SleekRender.drawToggle(base.frame, base.backgroundColor, this.state);
			if (flag != this.state && this.onToggled != null)
			{
				this.onToggled(this, flag);
			}
			this.state = flag;
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022CA RID: 8906
		public Toggled onToggled;

		// Token: 0x040022CB RID: 8907
		public bool state;
	}
}
