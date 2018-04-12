using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200049C RID: 1180
	public class EditorSelection
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x000ABBF5 File Offset: 0x000A9FF5
		public EditorSelection(Transform newTransform, Transform newParent, Vector3 newFromPosition, Quaternion newFromRotation, Vector3 newFromScale)
		{
			this._transform = newTransform;
			this._parent = newParent;
			this.fromPosition = newFromPosition;
			this.fromRotation = newFromRotation;
			this.fromScale = newFromScale;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000ABC22 File Offset: 0x000AA022
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000ABC2A File Offset: 0x000AA02A
		public Transform parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x040012B6 RID: 4790
		private Transform _transform;

		// Token: 0x040012B7 RID: 4791
		private Transform _parent;

		// Token: 0x040012B8 RID: 4792
		public Vector3 fromPosition;

		// Token: 0x040012B9 RID: 4793
		public Quaternion fromRotation;

		// Token: 0x040012BA RID: 4794
		public Vector3 fromScale;
	}
}
