using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005BF RID: 1471
	public class StructureDrop
	{
		// Token: 0x06002920 RID: 10528 RVA: 0x000FB408 File Offset: 0x000F9808
		public StructureDrop(Transform newModel, uint newInstanceID)
		{
			this._model = newModel;
			this.instanceID = newInstanceID;
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000FB41E File Offset: 0x000F981E
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000FB426 File Offset: 0x000F9826
		// (set) Token: 0x06002923 RID: 10531 RVA: 0x000FB42E File Offset: 0x000F982E
		public uint instanceID { get; private set; }

		// Token: 0x040019A8 RID: 6568
		private Transform _model;
	}
}
