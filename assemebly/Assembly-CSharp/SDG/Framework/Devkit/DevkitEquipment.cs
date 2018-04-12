using System;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000129 RID: 297
	public class DevkitEquipment : MonoBehaviour
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0004E53E File Offset: 0x0004C93E
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0004E545 File Offset: 0x0004C945
		public static DevkitEquipment instance { get; protected set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0004E54D File Offset: 0x0004C94D
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0004E555 File Offset: 0x0004C955
		public IDevkitTool tool { get; protected set; }

		// Token: 0x0600090E RID: 2318 RVA: 0x0004E55E File Offset: 0x0004C95E
		public virtual void equip(IDevkitTool newTool)
		{
			this.dequip();
			this.tool = newTool;
			if (this.tool != null)
			{
				this.tool.equip();
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0004E583 File Offset: 0x0004C983
		public virtual void dequip()
		{
			if (this.tool != null)
			{
				this.tool.dequip();
			}
			this.tool = null;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0004E5A2 File Offset: 0x0004C9A2
		protected void Update()
		{
			if (this.tool != null)
			{
				this.tool.update();
			}
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0004E5BA File Offset: 0x0004C9BA
		protected void Awake()
		{
			DevkitEquipment.instance = this;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0004E5C2 File Offset: 0x0004C9C2
		protected void OnDestroy()
		{
			this.dequip();
		}
	}
}
