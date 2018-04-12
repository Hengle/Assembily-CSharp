using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200059E RID: 1438
	public class ItemDrop
	{
		// Token: 0x0600282D RID: 10285 RVA: 0x000F3590 File Offset: 0x000F1990
		public ItemDrop(Transform newModel, InteractableItem newInteractableItem, uint newInstanceID)
		{
			this._model = newModel;
			this._interactableItem = newInteractableItem;
			this._instanceID = newInstanceID;
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000F35AD File Offset: 0x000F19AD
		public Transform model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000F35B5 File Offset: 0x000F19B5
		public InteractableItem interactableItem
		{
			get
			{
				return this._interactableItem;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x000F35BD File Offset: 0x000F19BD
		public uint instanceID
		{
			get
			{
				return this._instanceID;
			}
		}

		// Token: 0x04001914 RID: 6420
		private Transform _model;

		// Token: 0x04001915 RID: 6421
		private InteractableItem _interactableItem;

		// Token: 0x04001916 RID: 6422
		private uint _instanceID;
	}
}
