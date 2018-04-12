using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI.Components
{
	// Token: 0x0200020D RID: 525
	public class ContextDropdownButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000FBD RID: 4029 RVA: 0x00069054 File Offset: 0x00067454
		// (remove) Token: 0x06000FBE RID: 4030 RVA: 0x0006908C File Offset: 0x0006748C
		public event ContextDropdownOpenedHandler opened;

		// Token: 0x06000FBF RID: 4031 RVA: 0x000690C4 File Offset: 0x000674C4
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Right)
			{
				return;
			}
			Sleek2HoverDropdown sleek2HoverDropdown = new Sleek2HoverDropdown();
			sleek2HoverDropdown.name = "Context";
			this.element.addElement(sleek2HoverDropdown);
			sleek2HoverDropdown.transform.anchorMin = new Vector2(0.5f, 1f);
			sleek2HoverDropdown.transform.anchorMax = new Vector2(0.5f, 1f);
			sleek2HoverDropdown.transform.offsetMin = new Vector2(-100f, 0f);
			sleek2HoverDropdown.transform.offsetMax = new Vector2(100f, 0f);
			sleek2HoverDropdown.transform.pivot = new Vector2(0.5f, 1f);
			sleek2HoverDropdown.transform.position = eventData.position;
			sleek2HoverDropdown.transform.anchoredPosition += new Vector2(0f, (float)(Sleek2Config.bodyHeight / 2));
			sleek2HoverDropdown.transform.sizeDelta = new Vector2((float)Sleek2Config.tabWidth, 0f);
			sleek2HoverDropdown.transform.gameObject.AddComponent<ContextDropdown>().element = sleek2HoverDropdown;
			this.triggerOpened(sleek2HoverDropdown);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000691F3 File Offset: 0x000675F3
		protected void triggerOpened(Sleek2HoverDropdown dropdown)
		{
			if (this.opened != null)
			{
				this.opened(this, dropdown);
			}
		}

		// Token: 0x0400099F RID: 2463
		public Sleek2Element element;
	}
}
