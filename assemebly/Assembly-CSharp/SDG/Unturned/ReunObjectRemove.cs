using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A3 RID: 1187
	public class ReunObjectRemove : IReun
	{
		// Token: 0x06001F4B RID: 8011 RVA: 0x000AD911 File Offset: 0x000ABD11
		public ReunObjectRemove(int newStep, Transform newModel, ObjectAsset newObjectAsset, ItemAsset newItemAsset, Vector3 newPosition, Quaternion newRotation, Vector3 newScale)
		{
			this.step = newStep;
			this.model = newModel;
			this.objectAsset = newObjectAsset;
			this.itemAsset = newItemAsset;
			this.position = newPosition;
			this.rotation = newRotation;
			this.scale = newScale;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000AD94E File Offset: 0x000ABD4E
		// (set) Token: 0x06001F4D RID: 8013 RVA: 0x000AD956 File Offset: 0x000ABD56
		public int step { get; private set; }

		// Token: 0x06001F4E RID: 8014 RVA: 0x000AD960 File Offset: 0x000ABD60
		public Transform redo()
		{
			if (this.model != null)
			{
				if (this.objectAsset != null)
				{
					LevelObjects.removeObject(this.model);
					this.model = null;
				}
				else if (this.itemAsset != null)
				{
					LevelObjects.removeBuildable(this.model);
					this.model = null;
				}
			}
			return null;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x000AD9C0 File Offset: 0x000ABDC0
		public void undo()
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
		}

		// Token: 0x040012F9 RID: 4857
		private Transform model;

		// Token: 0x040012FA RID: 4858
		private ObjectAsset objectAsset;

		// Token: 0x040012FB RID: 4859
		private ItemAsset itemAsset;

		// Token: 0x040012FC RID: 4860
		private Vector3 position;

		// Token: 0x040012FD RID: 4861
		private Quaternion rotation;

		// Token: 0x040012FE RID: 4862
		private Vector3 scale;
	}
}
