using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x0200017A RID: 378
	public class DevkitGameObjectDestructionTransaction : IDevkitTransaction
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x0005A0B9 File Offset: 0x000584B9
		public DevkitGameObjectDestructionTransaction(GameObject newGO)
		{
			this.go = newGO;
			this.isActive = false;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0005A0CF File Offset: 0x000584CF
		public bool delta
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0005A0D2 File Offset: 0x000584D2
		public void undo()
		{
			if (this.go != null)
			{
				this.go.SetActive(true);
			}
			this.isActive = true;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0005A0F8 File Offset: 0x000584F8
		public void redo()
		{
			if (this.go != null)
			{
				this.go.SetActive(false);
			}
			this.isActive = false;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0005A11E File Offset: 0x0005851E
		public void begin()
		{
			if (this.go != null)
			{
				this.go.SetActive(false);
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0005A13D File Offset: 0x0005853D
		public void end()
		{
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0005A13F File Offset: 0x0005853F
		public void forget()
		{
			if (this.go != null && !this.isActive)
			{
				UnityEngine.Object.Destroy(this.go);
			}
		}

		// Token: 0x04000839 RID: 2105
		protected GameObject go;

		// Token: 0x0400083A RID: 2106
		protected bool isActive;
	}
}
