using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006B9 RID: 1721
	public class BlueprintItemIconsInfo
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x00144268 File Offset: 0x00142668
		public void onItemIconReady(Texture2D texture)
		{
			if (this.index >= this.textures.Length)
			{
				return;
			}
			this.textures[this.index] = texture;
			this.index++;
			if (this.index == this.textures.Length && this.callback != null)
			{
				this.callback();
			}
		}

		// Token: 0x040021DB RID: 8667
		public Texture2D[] textures;

		// Token: 0x040021DC RID: 8668
		public BlueprintItemIconsReady callback;

		// Token: 0x040021DD RID: 8669
		private int index;
	}
}
