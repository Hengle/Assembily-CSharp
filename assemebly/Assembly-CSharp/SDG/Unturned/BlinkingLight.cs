using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000520 RID: 1312
	public class BlinkingLight : MonoBehaviour
	{
		// Token: 0x060023A1 RID: 9121 RVA: 0x000C5F33 File Offset: 0x000C4333
		private void Update()
		{
			if (Time.time - this.blinkTime < 1f)
			{
				return;
			}
			this.blinkTime = Time.time;
			this.target.SetActive(!this.target.activeSelf);
		}

		// Token: 0x0400158D RID: 5517
		public GameObject target;

		// Token: 0x0400158E RID: 5518
		private float blinkTime;
	}
}
