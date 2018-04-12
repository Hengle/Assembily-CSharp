using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A2 RID: 1186
	public class ReunObjectAdd : IReun
	{
		// Token: 0x06001F46 RID: 8006 RVA: 0x000AD7FF File Offset: 0x000ABBFF
		public ReunObjectAdd(int newStep, ObjectAsset newObjectAsset, ItemAsset newItemAsset, Vector3 newPosition, Quaternion newRotation, Vector3 newScale)
		{
			this.step = newStep;
			this.model = null;
			this.objectAsset = newObjectAsset;
			this.itemAsset = newItemAsset;
			this.position = newPosition;
			this.rotation = newRotation;
			this.scale = newScale;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x000AD83B File Offset: 0x000ABC3B
		// (set) Token: 0x06001F48 RID: 8008 RVA: 0x000AD843 File Offset: 0x000ABC43
		public int step { get; private set; }

		// Token: 0x06001F49 RID: 8009 RVA: 0x000AD84C File Offset: 0x000ABC4C
		public Transform redo()
		{
			if (this.model == null)
			{
				if (this.objectAsset != null)
				{
					this.model = LevelObjects.addObject(this.position, this.rotation, this.scale, this.objectAsset.id, this.objectAsset.name, this.objectAsset.GUID, ELevelObjectPlacementOrigin.MANUAL);
				}
				else if (this.itemAsset != null)
				{
					this.model = LevelObjects.addBuildable(this.position, this.rotation, this.itemAsset.id);
				}
			}
			return this.model;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000AD8EC File Offset: 0x000ABCEC
		public void undo()
		{
			if (this.model != null)
			{
				LevelObjects.removeObject(this.model);
				this.model = null;
			}
		}

		// Token: 0x040012F2 RID: 4850
		private Transform model;

		// Token: 0x040012F3 RID: 4851
		private ObjectAsset objectAsset;

		// Token: 0x040012F4 RID: 4852
		private ItemAsset itemAsset;

		// Token: 0x040012F5 RID: 4853
		private Vector3 position;

		// Token: 0x040012F6 RID: 4854
		private Quaternion rotation;

		// Token: 0x040012F7 RID: 4855
		private Vector3 scale;
	}
}
