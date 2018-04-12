using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000516 RID: 1302
	public class Scrolling : MonoBehaviour
	{
		// Token: 0x06002374 RID: 9076 RVA: 0x000C5273 File Offset: 0x000C3673
		private void Update()
		{
			this.material.mainTextureOffset = new Vector2(this.x * Time.time, this.y * Time.time);
		}

		// Token: 0x04001578 RID: 5496
		public Material material;

		// Token: 0x04001579 RID: 5497
		public float x;

		// Token: 0x0400157A RID: 5498
		public float y;
	}
}
