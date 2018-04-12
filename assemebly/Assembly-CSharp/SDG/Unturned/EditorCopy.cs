using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000491 RID: 1169
	public class EditorCopy
	{
		// Token: 0x06001EA9 RID: 7849 RVA: 0x000A7BB6 File Offset: 0x000A5FB6
		public EditorCopy(Vector3 newPosition, Quaternion newRotation, Vector3 newScale, ObjectAsset newObjectAsset, ItemAsset newItemAsset)
		{
			this._position = newPosition;
			this._rotation = newRotation;
			this._scale = newScale;
			this._objectAsset = newObjectAsset;
			this._itemAsset = newItemAsset;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000A7BE3 File Offset: 0x000A5FE3
		public Vector3 position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x000A7BEB File Offset: 0x000A5FEB
		public Quaternion rotation
		{
			get
			{
				return this._rotation;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x000A7BF3 File Offset: 0x000A5FF3
		public Vector3 scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000A7BFB File Offset: 0x000A5FFB
		public ObjectAsset objectAsset
		{
			get
			{
				return this._objectAsset;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000A7C03 File Offset: 0x000A6003
		public ItemAsset itemAsset
		{
			get
			{
				return this._itemAsset;
			}
		}

		// Token: 0x04001268 RID: 4712
		private Vector3 _position;

		// Token: 0x04001269 RID: 4713
		private Quaternion _rotation;

		// Token: 0x0400126A RID: 4714
		private Vector3 _scale;

		// Token: 0x0400126B RID: 4715
		private ObjectAsset _objectAsset;

		// Token: 0x0400126C RID: 4716
		private ItemAsset _itemAsset;
	}
}
