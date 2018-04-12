using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x0200025B RID: 603
	public abstract class Sleek2TypeInspector : Sleek2Element
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x00072FC4 File Offset: 0x000713C4
		public Sleek2TypeInspector()
		{
			base.transform.anchorMin = new Vector2(0f, 1f);
			base.transform.anchorMax = new Vector2(1f, 1f);
			base.transform.pivot = new Vector2(0f, 1f);
			base.transform.offsetMin = new Vector2(0f, 0f);
			base.transform.offsetMax = new Vector2(0f, 0f);
			this.layoutComponent = base.gameObject.AddComponent<LayoutElement>();
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0007306A File Offset: 0x0007146A
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x00073072 File Offset: 0x00071472
		public ObjectInspectableInfo inspectable { get; protected set; }

		// Token: 0x060011BC RID: 4540
		public abstract void split(float value);

		// Token: 0x060011BD RID: 4541
		public abstract void inspect(ObjectInspectableInfo newInspectable);

		// Token: 0x060011BE RID: 4542
		public abstract void refresh();

		// Token: 0x04000A78 RID: 2680
		public LayoutElement layoutComponent;
	}
}
