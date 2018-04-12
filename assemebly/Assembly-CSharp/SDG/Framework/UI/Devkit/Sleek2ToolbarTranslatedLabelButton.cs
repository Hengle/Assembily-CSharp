using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x020002EA RID: 746
	public class Sleek2ToolbarTranslatedLabelButton : Sleek2ImageTranslatedLabelButton
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x00082790 File Offset: 0x00080B90
		public Sleek2ToolbarTranslatedLabelButton()
		{
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			base.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Hover_Background");
			base.label.textComponent.color = Sleek2Config.lightTextColor;
		}
	}
}
