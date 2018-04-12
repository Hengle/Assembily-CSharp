using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000546 RID: 1350
	public class LevelBuildableObject
	{
		// Token: 0x06002460 RID: 9312 RVA: 0x000CB864 File Offset: 0x000C9C64
		public LevelBuildableObject(Vector3 newPoint, Quaternion newRotation, ushort newID)
		{
			this.point = newPoint;
			this.rotation = newRotation;
			this._id = newID;
			this._asset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
			if (this.asset == null || this.asset.id != this.id)
			{
				this._asset = (ItemAsset)Assets.find(EAssetType.ITEM, this.id);
				if (this.asset == null)
				{
					return;
				}
			}
			if (Level.isEditor)
			{
				ItemBarricadeAsset itemBarricadeAsset = this.asset as ItemBarricadeAsset;
				ItemStructureAsset itemStructureAsset = this.asset as ItemStructureAsset;
				if (itemBarricadeAsset != null)
				{
					this._transform = UnityEngine.Object.Instantiate<GameObject>(itemBarricadeAsset.barricade).transform;
				}
				else if (itemStructureAsset != null)
				{
					this._transform = UnityEngine.Object.Instantiate<GameObject>(itemStructureAsset.structure).transform;
				}
				if (this.transform != null)
				{
					this.transform.name = this.id.ToString();
					this.transform.parent = LevelObjects.models;
					this.transform.position = newPoint;
					this.transform.rotation = newRotation;
					Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
					if (rigidbody == null)
					{
						rigidbody = this.transform.gameObject.AddComponent<Rigidbody>();
						rigidbody.useGravity = false;
						rigidbody.isKinematic = true;
					}
					this.transform.gameObject.SetActive(false);
					LevelBuildableObject.colliders.Clear();
					this.transform.GetComponentsInChildren<Collider>(true, LevelBuildableObject.colliders);
					for (int i = 0; i < LevelBuildableObject.colliders.Count; i++)
					{
						if (LevelBuildableObject.colliders[i].gameObject.layer != LayerMasks.BARRICADE && LevelBuildableObject.colliders[i].gameObject.layer != LayerMasks.STRUCTURE)
						{
							UnityEngine.Object.Destroy(LevelBuildableObject.colliders[i].gameObject);
						}
					}
				}
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000CBA7B File Offset: 0x000C9E7B
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000CBA83 File Offset: 0x000C9E83
		public ushort id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000CBA8B File Offset: 0x000C9E8B
		public ItemAsset asset
		{
			get
			{
				return this._asset;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000CBA93 File Offset: 0x000C9E93
		// (set) Token: 0x06002465 RID: 9317 RVA: 0x000CBA9B File Offset: 0x000C9E9B
		public bool isEnabled { get; private set; }

		// Token: 0x06002466 RID: 9318 RVA: 0x000CBAA4 File Offset: 0x000C9EA4
		public void enable()
		{
			this.isEnabled = true;
			if (this.transform != null)
			{
				this.transform.gameObject.SetActive(true);
			}
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000CBACF File Offset: 0x000C9ECF
		public void disable()
		{
			this.isEnabled = false;
			if (this.transform != null)
			{
				this.transform.gameObject.SetActive(false);
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000CBAFA File Offset: 0x000C9EFA
		public void destroy()
		{
			if (this.transform != null)
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}

		// Token: 0x04001659 RID: 5721
		private static List<Collider> colliders = new List<Collider>();

		// Token: 0x0400165A RID: 5722
		public Vector3 point;

		// Token: 0x0400165B RID: 5723
		public Quaternion rotation;

		// Token: 0x0400165C RID: 5724
		private Transform _transform;

		// Token: 0x0400165D RID: 5725
		private ushort _id;

		// Token: 0x0400165E RID: 5726
		private ItemAsset _asset;
	}
}
