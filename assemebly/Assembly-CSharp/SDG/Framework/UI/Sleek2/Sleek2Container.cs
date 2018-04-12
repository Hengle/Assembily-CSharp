using System;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002B6 RID: 694
	public class Sleek2Container : Sleek2Element
	{
		// Token: 0x0600142F RID: 5167 RVA: 0x0006E96C File Offset: 0x0006CD6C
		public Sleek2Container()
		{
			base.name = "Container";
			Sleek2Image sleek2Image = new Sleek2Image();
			sleek2Image.name = "Header";
			sleek2Image.transform.anchorMin = new Vector2(0f, 1f);
			sleek2Image.transform.anchorMax = new Vector2(1f, 1f);
			sleek2Image.transform.pivot = new Vector2(0f, 1f);
			sleek2Image.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			sleek2Image.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Toolbar_Background");
			sleek2Image.imageComponent.type = Image.Type.Sliced;
			this.headerPanel = sleek2Image;
			this.addElement(sleek2Image);
			Sleek2Mask sleek2Mask = new Sleek2Mask();
			sleek2Mask.name = "Body";
			sleek2Mask.transform.anchorMin = new Vector2(0f, 0f);
			sleek2Mask.transform.anchorMax = new Vector2(1f, 1f);
			sleek2Mask.transform.pivot = new Vector2(0f, 1f);
			sleek2Mask.transform.offsetMin = new Vector2(0f, 0f);
			sleek2Mask.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.bodyPanel = sleek2Mask;
			this.addElement(sleek2Mask);
			this.backgroundImageComponent = base.gameObject.AddComponent<Image>();
			this.backgroundImageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Background");
			this.backgroundImageComponent.type = Image.Type.Sliced;
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0006EB08 File Offset: 0x0006CF08
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x0006EB10 File Offset: 0x0006CF10
		public Sleek2Element headerPanel { get; protected set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0006EB19 File Offset: 0x0006CF19
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0006EB21 File Offset: 0x0006CF21
		public Sleek2Element bodyPanel { get; protected set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0006EB2A File Offset: 0x0006CF2A
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0006EB32 File Offset: 0x0006CF32
		public Image backgroundImageComponent { get; protected set; }
	}
}
