using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200021A RID: 538
	public class DragableTypeDestination<T> : IDropHandler, IEventSystemHandler
	{
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000FFF RID: 4095 RVA: 0x00069D4C File Offset: 0x0006814C
		// (remove) Token: 0x06001000 RID: 4096 RVA: 0x00069D84 File Offset: 0x00068184
		public event TypeReferenceDockedHandler<T> typeReferenceDocked;

		// Token: 0x06001001 RID: 4097 RVA: 0x00069DBC File Offset: 0x000681BC
		public void OnDrop(PointerEventData eventData)
		{
			if (!(Sleek2DragManager.item is ITypeReference))
			{
				return;
			}
			if (typeof(T).IsAssignableFrom(Sleek2DragManager.item.GetType().GetGenericArguments()[0]))
			{
				ITypeReference typeReference = (ITypeReference)Sleek2DragManager.item;
				this.triggerTypeReferenceDocked(new TypeReference<T>(typeReference));
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00069E15 File Offset: 0x00068215
		protected virtual void triggerTypeReferenceDocked(TypeReference<T> typeReference)
		{
			if (this.typeReferenceDocked != null)
			{
				this.typeReferenceDocked(typeReference);
			}
		}
	}
}
