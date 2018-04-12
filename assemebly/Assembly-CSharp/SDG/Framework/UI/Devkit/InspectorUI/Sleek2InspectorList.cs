using System;
using System.Collections;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x0200025A RID: 602
	public class Sleek2InspectorList : Sleek2Element
	{
		// Token: 0x060011A5 RID: 4517 RVA: 0x00072A2C File Offset: 0x00070E2C
		public Sleek2InspectorList(Sleek2Inspector newRootInspector, ObjectInspectableInfo newParentInfo, IList newList, Type newListType, IInspectableList newInspectable)
		{
			this.rootInspector = newRootInspector;
			this.parentInfo = newParentInfo;
			this.list = newList;
			this.listType = newListType;
			this.inspectable = newInspectable;
			base.name = "List";
			this.collapseFoldoutsByDefault = this.rootInspector.collapseFoldoutsByDefault;
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
			if (this.inspectable == null || this.inspectable.canInspectorAdd)
			{
				this.addButton = new Sleek2ImageLabelButton();
				this.addButton.transform.anchorMin = new Vector2(0f, 0f);
				this.addButton.transform.anchorMax = new Vector2(0f, 0f);
				this.addButton.transform.sizeDelta = new Vector2(200f, 0f);
				this.addButton.clicked += this.handleAddButtonClicked;
				this.addButton.label.textComponent.text = "+";
				this.addElement(this.addButton);
				LayoutElement layoutElement = this.addButton.gameObject.AddComponent<LayoutElement>();
				layoutElement.minHeight = (float)Sleek2Config.bodyHeight;
			}
			if (this.inspectable != null)
			{
				this.inspectable.inspectorChanged += this.handleListChanged;
			}
			this.refresh();
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00072BFD File Offset: 0x00070FFD
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00072C05 File Offset: 0x00071005
		public Sleek2Element panel { get; protected set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00072C0E File Offset: 0x0007100E
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x00072C16 File Offset: 0x00071016
		public Sleek2Inspector rootInspector { get; protected set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00072C1F File Offset: 0x0007101F
		// (set) Token: 0x060011AB RID: 4523 RVA: 0x00072C27 File Offset: 0x00071027
		public ObjectInspectableInfo parentInfo { get; protected set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x00072C30 File Offset: 0x00071030
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00072C38 File Offset: 0x00071038
		public IList list { get; protected set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x00072C41 File Offset: 0x00071041
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00072C49 File Offset: 0x00071049
		public Type listType { get; protected set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00072C52 File Offset: 0x00071052
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00072C5A File Offset: 0x0007105A
		public IInspectableList inspectable { get; protected set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x00072C63 File Offset: 0x00071063
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00072C6B File Offset: 0x0007106B
		public Sleek2ImageLabelButton addButton { get; protected set; }

		// Token: 0x060011B4 RID: 4532 RVA: 0x00072C74 File Offset: 0x00071074
		protected virtual void refresh()
		{
			this.panel.clearElements();
			for (int i = 0; i < this.list.Count; i++)
			{
				Type type = this.list[i].GetType();
				Sleek2TypeInspector sleek2TypeInspector = TypeInspectorRegistry.inspect(type);
				if (sleek2TypeInspector != null)
				{
					sleek2TypeInspector.inspect(new ObjectInspectableList(this.parentInfo, this.rootInspector.instance as IDirtyable, this.list, this.listType, i, new TranslationReference("#SDG::List_" + i), TranslationReference.invalid));
					this.panel.addElement(sleek2TypeInspector);
				}
				else
				{
					object obj = this.list[i];
					Sleek2InspectorFoldoutList sleek2InspectorFoldoutList = new Sleek2InspectorFoldoutList(i);
					sleek2InspectorFoldoutList.transform.anchorMin = new Vector2(0f, 1f);
					sleek2InspectorFoldoutList.transform.anchorMax = new Vector2(1f, 1f);
					sleek2InspectorFoldoutList.transform.pivot = new Vector2(0.5f, 1f);
					sleek2InspectorFoldoutList.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
					if (obj is IInspectableListElement)
					{
						sleek2InspectorFoldoutList.name = ((IInspectableListElement)obj).inspectableListIndexInternalName;
						sleek2InspectorFoldoutList.label.translation = new TranslatedText(((IInspectableListElement)obj).inspectableListIndexDisplayName);
					}
					else
					{
						sleek2InspectorFoldoutList.label.translation = new TranslatedTextFallback('[' + i.ToString() + ']');
					}
					sleek2InspectorFoldoutList.label.translation.format();
					sleek2InspectorFoldoutList.removeButton.clicked += this.handleRemoveButtonClicked;
					this.panel.addElement(sleek2InspectorFoldoutList);
					this.rootInspector.reflect(new ObjectInspectableList(this.parentInfo, this.rootInspector.instance as IDirtyable, this.list, this.listType, i, new TranslationReference("#SDG::List_" + i), TranslationReference.invalid), obj, sleek2InspectorFoldoutList.contents);
					if (this.collapseFoldoutsByDefault)
					{
						sleek2InspectorFoldoutList.isOpen = false;
					}
				}
			}
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00072EB4 File Offset: 0x000712B4
		protected virtual void handleAddButtonClicked(Sleek2ImageButton button)
		{
			if (this.inspectable == null || this.inspectable.canInspectorAdd)
			{
				object obj = Activator.CreateInstance(this.listType);
				this.list.Add(obj);
				if (this.inspectable == null)
				{
					this.refresh();
				}
				else
				{
					this.inspectable.inspectorAdd(obj);
				}
			}
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00072F18 File Offset: 0x00071318
		protected virtual void handleRemoveButtonClicked(Sleek2ImageButton button)
		{
			if (this.inspectable == null || this.inspectable.canInspectorAdd)
			{
				int index = (button.parent.parent as Sleek2InspectorFoldoutList).index;
				object instance = this.list[index];
				this.list.RemoveAt(index);
				if (this.inspectable == null)
				{
					this.refresh();
				}
				else
				{
					this.inspectable.inspectorRemove(instance);
				}
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00072F91 File Offset: 0x00071391
		protected virtual void handleListChanged(IInspectableList list)
		{
			this.refresh();
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00072F99 File Offset: 0x00071399
		protected override void triggerDestroyed()
		{
			if (this.inspectable != null)
			{
				this.inspectable.inspectorChanged -= this.handleListChanged;
			}
			base.triggerDestroyed();
		}

		// Token: 0x04000A70 RID: 2672
		public bool collapseFoldoutsByDefault;
	}
}
