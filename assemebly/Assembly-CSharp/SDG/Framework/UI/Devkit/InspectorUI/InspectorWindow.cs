using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x0200024F RID: 591
	public class InspectorWindow : Sleek2Window
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x0007108C File Offset: 0x0006F48C
		public InspectorWindow()
		{
			base.gameObject.name = "Inspector";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Inspector.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 0f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(5f, 5f);
			this.inspector.transform.offsetMax = new Vector2(-5f, -5f);
			this.addElement(this.inspector);
			InspectorWindow.inspected += this.handleInspected;
			this.handleInspected(InspectorWindow.currentInstance);
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001138 RID: 4408 RVA: 0x000711B8 File Offset: 0x0006F5B8
		// (remove) Token: 0x06001139 RID: 4409 RVA: 0x000711EC File Offset: 0x0006F5EC
		protected static event InspectorWindow.InspectorWindowInspectedHandler inspected;

		// Token: 0x0600113A RID: 4410 RVA: 0x00071220 File Offset: 0x0006F620
		public static void inspect(object instance)
		{
			InspectorWindow.currentInstance = instance;
			if (InspectorWindow.inspected != null)
			{
				InspectorWindow.inspected(InspectorWindow.currentInstance);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600113B RID: 4411 RVA: 0x00071241 File Offset: 0x0006F641
		// (set) Token: 0x0600113C RID: 4412 RVA: 0x00071249 File Offset: 0x0006F649
		public Sleek2Inspector inspector { get; protected set; }

		// Token: 0x0600113D RID: 4413 RVA: 0x00071252 File Offset: 0x0006F652
		protected override void triggerDestroyed()
		{
			InspectorWindow.inspected -= this.handleInspected;
			base.triggerDestroyed();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0007126B File Offset: 0x0006F66B
		protected void handleInspected(object instance)
		{
			this.inspector.inspect(instance);
		}

		// Token: 0x04000A4E RID: 2638
		protected static object currentInstance;

		// Token: 0x02000250 RID: 592
		// (Invoke) Token: 0x06001140 RID: 4416
		protected delegate void InspectorWindowInspectedHandler(object inspectionTarget);
	}
}
