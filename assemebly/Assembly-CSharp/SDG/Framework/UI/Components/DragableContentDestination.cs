using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x02000211 RID: 529
	public class DragableContentDestination<T> : IDropHandler, IEventSystemHandler where T : UnityEngine.Object
	{
		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000FCF RID: 4047 RVA: 0x00069304 File Offset: 0x00067704
		// (remove) Token: 0x06000FD0 RID: 4048 RVA: 0x0006933C File Offset: 0x0006773C
		public event ContentReferenceDockedHandler<T> contentReferenceDocked;

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00069374 File Offset: 0x00067774
		public void OnDrop(PointerEventData eventData)
		{
			if (!(Sleek2DragManager.item is IContentReference))
			{
				return;
			}
			if (typeof(T).IsAssignableFrom(Sleek2DragManager.item.GetType().GetGenericArguments()[0]))
			{
				IContentReference contentReference = (IContentReference)Sleek2DragManager.item;
				this.triggerContentReferenceDocked(new ContentReference<T>(contentReference));
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000693CD File Offset: 0x000677CD
		protected virtual void triggerContentReferenceDocked(ContentReference<T> contentReference)
		{
			if (this.contentReferenceDocked != null)
			{
				this.contentReferenceDocked(contentReference);
			}
		}
	}
}
