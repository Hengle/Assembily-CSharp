using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x0200017B RID: 379
	public class DevkitGameObjectInstantiationTransaction : IDevkitTransaction
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x0005A168 File Offset: 0x00058568
		public DevkitGameObjectInstantiationTransaction(GameObject newGO)
		{
			this.go = newGO;
			this.isActive = true;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0005A17E File Offset: 0x0005857E
		public bool delta
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0005A181 File Offset: 0x00058581
		public void undo()
		{
			if (this.go != null)
			{
				this.go.SetActive(false);
			}
			this.isActive = false;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0005A1A7 File Offset: 0x000585A7
		public void redo()
		{
			if (this.go != null)
			{
				this.go.SetActive(true);
			}
			this.isActive = true;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0005A1CD File Offset: 0x000585CD
		public void begin()
		{
			if (this.go != null)
			{
				this.go.SetActive(true);
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0005A1EC File Offset: 0x000585EC
		public void end()
		{
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0005A1EE File Offset: 0x000585EE
		public void forget()
		{
			if (this.go != null && !this.isActive)
			{
				UnityEngine.Object.Destroy(this.go);
			}
		}

		// Token: 0x0400083B RID: 2107
		protected GameObject go;

		// Token: 0x0400083C RID: 2108
		protected bool isActive;
	}
}
