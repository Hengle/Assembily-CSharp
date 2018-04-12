using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C1 RID: 1217
	public class Interactable : MonoBehaviour
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000B2ECE File Offset: 0x000B12CE
		public bool isPlant
		{
			get
			{
				return base.transform.parent != null && base.transform.parent.CompareTag("Vehicle");
			}
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000B2EFE File Offset: 0x000B12FE
		public virtual void updateState(Asset asset, byte[] state)
		{
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000B2F00 File Offset: 0x000B1300
		public virtual bool checkInteractable()
		{
			return true;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000B2F03 File Offset: 0x000B1303
		public virtual bool checkUseable()
		{
			return true;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000B2F06 File Offset: 0x000B1306
		public virtual bool checkHighlight(out Color color)
		{
			color = Color.white;
			return false;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000B2F14 File Offset: 0x000B1314
		public virtual bool checkHint(out EPlayerMessage message, out string text, out Color color)
		{
			message = EPlayerMessage.NONE;
			text = string.Empty;
			color = Color.white;
			return false;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000B2F2C File Offset: 0x000B132C
		public virtual void use()
		{
		}
	}
}
