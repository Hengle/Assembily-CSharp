using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200058A RID: 1418
	public class BarricadeRegion
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x000F07F9 File Offset: 0x000EEBF9
		public BarricadeRegion(Transform newParent)
		{
			this._drops = new List<BarricadeDrop>();
			this._barricades = new List<BarricadeData>();
			this._parent = newParent;
			this.isNetworked = false;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x000F0825 File Offset: 0x000EEC25
		public List<BarricadeDrop> drops
		{
			get
			{
				return this._drops;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000F082D File Offset: 0x000EEC2D
		public List<BarricadeData> barricades
		{
			get
			{
				return this._barricades;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x000F0835 File Offset: 0x000EEC35
		public Transform parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000F0840 File Offset: 0x000EEC40
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

		// Token: 0x040018C5 RID: 6341
		private List<BarricadeDrop> _drops;

		// Token: 0x040018C6 RID: 6342
		private List<BarricadeData> _barricades;

		// Token: 0x040018C7 RID: 6343
		private Transform _parent;

		// Token: 0x040018C8 RID: 6344
		public bool isNetworked;
	}
}
