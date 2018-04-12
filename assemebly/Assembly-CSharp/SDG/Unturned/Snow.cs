using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000572 RID: 1394
	public class Snow : MonoBehaviour
	{
		// Token: 0x06002683 RID: 9859 RVA: 0x000E411F File Offset: 0x000E251F
		private void Update()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this._Snow_Sparkle_Map != -1)
			{
				Shader.SetGlobalTexture(this._Snow_Sparkle_Map, this.Sparkle_Map);
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000E4149 File Offset: 0x000E2549
		private void OnEnable()
		{
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (this._Snow_Sparkle_Map == -1)
			{
				this._Snow_Sparkle_Map = Shader.PropertyToID("_Snow_Sparkle_Map");
			}
		}

		// Token: 0x0400181B RID: 6171
		private int _Snow_Sparkle_Map = -1;

		// Token: 0x0400181C RID: 6172
		public Texture2D Sparkle_Map;
	}
}
