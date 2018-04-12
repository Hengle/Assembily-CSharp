using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.InspectorUI
{
	// Token: 0x02000258 RID: 600
	public class Sleek2InspectorFoldout : Sleek2Element
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x000724E4 File Offset: 0x000708E4
		public Sleek2InspectorFoldout()
		{
			base.name = "Foldout";
			this.title = new Sleek2Image();
			this.title.name = "Title";
			this.title.transform.pivot = new Vector2(0f, 1f);
			this.title.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Toolbar_Background");
			this.title.imageComponent.type = Image.Type.Sliced;
			this.addElement(this.title);
			this.button = new Sleek2ImageButton();
			this.button.transform.anchorMin = new Vector2(0f, 1f);
			this.button.transform.anchorMax = new Vector2(0f, 1f);
			this.button.transform.pivot = new Vector2(0f, 1f);
			this.button.transform.sizeDelta = new Vector2((float)Sleek2Config.bodyHeight, (float)Sleek2Config.bodyHeight);
			this.button.imageComponent.color = new Color(0.5f, 0.5f, 0.5f);
			this.button.clicked += this.handleButtonClicked;
			this.title.addElement(this.button);
			this.label = new Sleek2TranslatedLabel();
			this.label.transform.anchorMin = new Vector2(0f, 0f);
			this.label.transform.anchorMax = new Vector2(1f, 1f);
			this.label.transform.pivot = new Vector2(0f, 0f);
			this.label.transform.offsetMin = new Vector2((float)(Sleek2Config.bodyHeight + 5), 0f);
			this.label.transform.offsetMax = new Vector2(0f, 0f);
			this.label.textComponent.alignment = TextAnchor.MiddleLeft;
			this.title.addElement(this.label);
			this.contents = new Sleek2Element();
			this.contents.name = "Contents";
			this.contents.transform.pivot = new Vector2(0f, 1f);
			this.addElement(this.contents);
			this.layoutComponent = this.title.gameObject.AddComponent<LayoutElement>();
			this.layoutComponent.minHeight = (float)Sleek2Config.bodyHeight;
			this.groupComponent = base.gameObject.AddComponent<VerticalLayoutGroup>();
			this.groupComponent.childForceExpandWidth = true;
			this.groupComponent.childForceExpandHeight = false;
			this.foldoutComponent = this.contents.gameObject.AddComponent<VerticalLayoutGroup>();
			this.foldoutComponent.padding.left = 25;
			this.foldoutComponent.padding.top = 5;
			this.foldoutComponent.spacing = 5f;
			this.foldoutComponent.childForceExpandWidth = true;
			this.foldoutComponent.childForceExpandHeight = false;
			this.isOpen = true;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00072812 File Offset: 0x00070C12
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x0007281A File Offset: 0x00070C1A
		public Sleek2Image title { get; protected set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00072823 File Offset: 0x00070C23
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x0007282B File Offset: 0x00070C2B
		public Sleek2ImageButton button { get; protected set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x00072834 File Offset: 0x00070C34
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x0007283C File Offset: 0x00070C3C
		public Sleek2TranslatedLabel label { get; protected set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x00072845 File Offset: 0x00070C45
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x0007284D File Offset: 0x00070C4D
		public Sleek2Element contents { get; protected set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00072856 File Offset: 0x00070C56
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0007285E File Offset: 0x00070C5E
		public LayoutElement layoutComponent { get; protected set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00072867 File Offset: 0x00070C67
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0007286F File Offset: 0x00070C6F
		public VerticalLayoutGroup groupComponent { get; protected set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00072878 File Offset: 0x00070C78
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00072880 File Offset: 0x00070C80
		public VerticalLayoutGroup foldoutComponent { get; protected set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x00072889 File Offset: 0x00070C89
		// (set) Token: 0x0600119E RID: 4510 RVA: 0x0007289C File Offset: 0x00070C9C
		public virtual bool isOpen
		{
			get
			{
				return this.contents.gameObject.activeSelf;
			}
			set
			{
				this.contents.gameObject.SetActive(value);
				if (value)
				{
					this.button.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Fold_In");
				}
				else
				{
					this.button.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Fold_Out");
				}
			}
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000728F9 File Offset: 0x00070CF9
		protected virtual void handleButtonClicked(Sleek2ImageButton button)
		{
			this.isOpen = !this.isOpen;
		}
	}
}
