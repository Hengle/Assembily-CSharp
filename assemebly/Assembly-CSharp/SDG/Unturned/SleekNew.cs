using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006F4 RID: 1780
	public class SleekNew : SleekLabel
	{
		// Token: 0x060032EF RID: 13039 RVA: 0x0014A2DC File Offset: 0x001486DC
		public SleekNew(bool isUpdate = false)
		{
			base.init();
			base.positionOffset_X = -105;
			base.positionScale_X = 1f;
			base.sizeOffset_X = 100;
			base.sizeOffset_Y = 30;
			this.fontAlignment = TextAnchor.MiddleRight;
			base.text = Provider.localization.format((!isUpdate) ? "New" : "Updated");
			this.foregroundTint = ESleekTint.NONE;
			base.foregroundColor = Color.green;
		}
	}
}
