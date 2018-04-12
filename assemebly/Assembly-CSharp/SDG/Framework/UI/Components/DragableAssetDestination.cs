using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200020F RID: 527
	public class DragableAssetDestination<T> : IDropHandler, IEventSystemHandler where T : Asset
	{
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000FC6 RID: 4038 RVA: 0x00069218 File Offset: 0x00067618
		// (remove) Token: 0x06000FC7 RID: 4039 RVA: 0x00069250 File Offset: 0x00067650
		public event AssetReferenceDockedHandler<T> assetReferenceDocked;

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00069288 File Offset: 0x00067688
		public void OnDrop(PointerEventData eventData)
		{
			if (!(Sleek2DragManager.item is IAssetReference))
			{
				return;
			}
			if (typeof(T).IsAssignableFrom(Sleek2DragManager.item.GetType().GetGenericArguments()[0]))
			{
				IAssetReference assetReference = (IAssetReference)Sleek2DragManager.item;
				this.triggerAssetReferenceDocked(new AssetReference<T>(assetReference));
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000692E1 File Offset: 0x000676E1
		protected virtual void triggerAssetReferenceDocked(AssetReference<T> assetReference)
		{
			if (this.assetReferenceDocked != null)
			{
				this.assetReferenceDocked(assetReference);
			}
		}
	}
}
