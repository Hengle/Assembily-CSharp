using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200059D RID: 1437
	public class ItemData
	{
		// Token: 0x06002827 RID: 10279 RVA: 0x000F3538 File Offset: 0x000F1938
		public ItemData(Item newItem, uint newInstanceID, Vector3 newPoint, bool newDropped)
		{
			this._item = newItem;
			this._instanceID = newInstanceID;
			this._point = newPoint;
			this._isDropped = newDropped;
			this._lastDropped = Time.realtimeSinceStartup;
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000F3568 File Offset: 0x000F1968
		public Item item
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000F3570 File Offset: 0x000F1970
		public uint instanceID
		{
			get
			{
				return this._instanceID;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000F3578 File Offset: 0x000F1978
		public Vector3 point
		{
			get
			{
				return this._point;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x000F3580 File Offset: 0x000F1980
		public bool isDropped
		{
			get
			{
				return this._isDropped;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000F3588 File Offset: 0x000F1988
		public float lastDropped
		{
			get
			{
				return this._lastDropped;
			}
		}

		// Token: 0x0400190F RID: 6415
		private Item _item;

		// Token: 0x04001910 RID: 6416
		private uint _instanceID;

		// Token: 0x04001911 RID: 6417
		private Vector3 _point;

		// Token: 0x04001912 RID: 6418
		private bool _isDropped;

		// Token: 0x04001913 RID: 6419
		private float _lastDropped;
	}
}
