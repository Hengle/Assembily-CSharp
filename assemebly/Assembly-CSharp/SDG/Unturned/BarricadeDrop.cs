using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000587 RID: 1415
	public class BarricadeDrop
	{
		// Token: 0x06002714 RID: 10004 RVA: 0x000E8A8C File Offset: 0x000E6E8C
		public BarricadeDrop(Transform newModel, Interactable newInteractable, uint newInstanceID)
		{
			this._model = newModel;
			this._interactable = newInteractable;
			this.instanceID = newInstanceID;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x000E8AA9 File Offset: 0x000E6EA9
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x000E8AB1 File Offset: 0x000E6EB1
		public Interactable interactable
		{
			get
			{
				return this._interactable;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x000E8AB9 File Offset: 0x000E6EB9
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x000E8AC1 File Offset: 0x000E6EC1
		public uint instanceID { get; private set; }

		// Token: 0x040018B5 RID: 6325
		private Transform _model;

		// Token: 0x040018B6 RID: 6326
		private Interactable _interactable;
	}
}
