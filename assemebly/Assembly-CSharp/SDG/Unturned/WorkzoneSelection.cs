using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A5 RID: 1189
	public class WorkzoneSelection
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x000ADB61 File Offset: 0x000ABF61
		public WorkzoneSelection(Transform newTransform, Transform newParent)
		{
			this._transform = newTransform;
			this._parent = newParent;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x000ADB77 File Offset: 0x000ABF77
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x000ADB7F File Offset: 0x000ABF7F
		public Transform parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x04001307 RID: 4871
		private Transform _transform;

		// Token: 0x04001308 RID: 4872
		private Transform _parent;

		// Token: 0x04001309 RID: 4873
		public Vector3 point;
	}
}
