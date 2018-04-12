using System;
using SDG.Framework.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002FD RID: 765
	public class Sleek2WindowTabDock : Sleek2Element
	{
		// Token: 0x060015FD RID: 5629 RVA: 0x00083F94 File Offset: 0x00082394
		public Sleek2WindowTabDock()
		{
			base.name = "Tab_Dock";
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0f, 1f);
			base.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Background");
			this.imageComponent.type = Image.Type.Sliced;
			this.destination = base.gameObject.AddComponent<DragableTabDestination>();
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0008405E File Offset: 0x0008245E
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00084066 File Offset: 0x00082466
		public Image imageComponent { get; protected set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0008406F File Offset: 0x0008246F
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x00084077 File Offset: 0x00082477
		public DragableTabDestination destination { get; protected set; }
	}
}
