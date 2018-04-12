using System;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000257 RID: 599
	public class Sleek2InspectorArray : Sleek2Element
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x00072210 File Offset: 0x00070610
		public Sleek2InspectorArray(Sleek2Inspector newRootInspector, ObjectInspectableInfo newParentInfo, Array newArray, Type newArrayType)
		{
			this.rootInspector = newRootInspector;
			this.parentInfo = newParentInfo;
			this.array = newArray;
			this.arrayType = newArrayType;
			base.name = "Array";
			this.panel = new Sleek2Element();
			this.panel.transform.reset();
			this.addElement(this.panel);
			VerticalLayoutGroup verticalLayoutGroup = base.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup.spacing = 5f;
			verticalLayoutGroup.childForceExpandWidth = true;
			verticalLayoutGroup.childForceExpandHeight = false;
			VerticalLayoutGroup verticalLayoutGroup2 = this.panel.gameObject.AddComponent<VerticalLayoutGroup>();
			verticalLayoutGroup2.spacing = 5f;
			verticalLayoutGroup2.childForceExpandWidth = true;
			verticalLayoutGroup2.childForceExpandHeight = false;
			this.refresh();
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x000722C7 File Offset: 0x000706C7
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x000722CF File Offset: 0x000706CF
		public Sleek2Element panel { get; protected set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000722D8 File Offset: 0x000706D8
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x000722E0 File Offset: 0x000706E0
		public Sleek2Inspector rootInspector { get; protected set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x000722E9 File Offset: 0x000706E9
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x000722F1 File Offset: 0x000706F1
		public ObjectInspectableInfo parentInfo { get; protected set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x000722FA File Offset: 0x000706FA
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00072302 File Offset: 0x00070702
		public Array array { get; protected set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0007230B File Offset: 0x0007070B
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00072313 File Offset: 0x00070713
		public Type arrayType { get; protected set; }

		// Token: 0x0600118D RID: 4493 RVA: 0x0007231C File Offset: 0x0007071C
		protected virtual void refresh()
		{
			this.panel.clearElements();
			for (int i = 0; i < this.array.Length; i++)
			{
				Type type = this.array.GetValue(i).GetType();
				Sleek2TypeInspector sleek2TypeInspector = TypeInspectorRegistry.inspect(type);
				if (sleek2TypeInspector != null)
				{
					sleek2TypeInspector.inspect(new ObjectInspectableArray(this.parentInfo, this.rootInspector.instance as IDirtyable, this.array, this.arrayType, i, new TranslationReference("#SDG::Array_" + i), TranslationReference.invalid));
					this.panel.addElement(sleek2TypeInspector);
				}
				else
				{
					Sleek2InspectorFoldout sleek2InspectorFoldout = new Sleek2InspectorFoldout();
					sleek2InspectorFoldout.transform.anchorMin = new Vector2(0f, 1f);
					sleek2InspectorFoldout.transform.anchorMax = new Vector2(1f, 1f);
					sleek2InspectorFoldout.transform.pivot = new Vector2(0.5f, 1f);
					sleek2InspectorFoldout.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
					sleek2InspectorFoldout.label.translation = new TranslatedTextFallback('[' + i.ToString() + ']');
					sleek2InspectorFoldout.label.translation.format();
					this.panel.addElement(sleek2InspectorFoldout);
					this.rootInspector.reflect(new ObjectInspectableArray(this.parentInfo, this.rootInspector.instance as IDirtyable, this.array, this.arrayType, i, new TranslationReference("#SDG::Array_" + i), TranslationReference.invalid), this.array.GetValue(i), sleek2InspectorFoldout.contents);
				}
			}
		}
	}
}
