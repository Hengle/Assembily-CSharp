using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x020002E9 RID: 745
	public class Sleek2ToolbarLabelButton : Sleek2ImageLabelButton
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x000749EC File Offset: 0x00072DEC
		public Sleek2ToolbarLabelButton()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			base.label.textComponent.color = Sleek2Config.lightTextColor;
		}
	}
}
