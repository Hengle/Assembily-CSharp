using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005C2 RID: 1474
	public class StructureRegion
	{
		// Token: 0x0600294D RID: 10573 RVA: 0x000FD803 File Offset: 0x000FBC03
		public StructureRegion()
		{
			this._drops = new List<StructureDrop>();
			this._structures = new List<StructureData>();
			this.isNetworked = false;
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000FD828 File Offset: 0x000FBC28
		public List<StructureDrop> drops
		{
			get
			{
				return this._drops;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000FD830 File Offset: 0x000FBC30
		public List<StructureData> structures
		{
			get
			{
				return this._structures;
			}
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000FD838 File Offset: 0x000FBC38
		public void destroy()
		{
			ushort num = 0;
			while ((int)num < this.drops.Count)
			{
				IManualOnDestroy component = this.drops[(int)num].model.GetComponent<IManualOnDestroy>();
				if (component != null)
				{
					component.ManualOnDestroy();
				}
				UnityEngine.Object.Destroy(this.drops[(int)num].model.gameObject);
				this.drops[(int)num].model.position = Vector3.zero;
				num += 1;
			}
			this.drops.Clear();
		}

		// Token: 0x040019B5 RID: 6581
		private List<StructureDrop> _drops;

		// Token: 0x040019B6 RID: 6582
		private List<StructureData> _structures;

		// Token: 0x040019B7 RID: 6583
		public bool isNetworked;
	}
}
