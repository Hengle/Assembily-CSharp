using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000294 RID: 660
	public class TranslationLeafUpdateField : Sleek2Element
	{
		// Token: 0x06001364 RID: 4964 RVA: 0x0007B5E8 File Offset: 0x000799E8
		public TranslationLeafUpdateField(TranslationLeaf newOriginLeaf, TranslationLeaf newTranslationLeaf)
		{
			base.name = "Leaf_Update";
			this.originLeaf = newOriginLeaf;
			this.translationLeaf = newTranslationLeaf;
			this.climbButton = new Sleek2ImageLabelButton();
			this.climbButton.transform.anchorMin = new Vector2(0f, 0f);
			this.climbButton.transform.anchorMax = new Vector2(1f, 0f);
			this.climbButton.transform.pivot = new Vector2(0f, 1f);
			this.climbButton.transform.offsetMin = new Vector2(0f, (float)(Sleek2Config.bodyHeight * 2));
			this.climbButton.transform.offsetMax = new Vector2(0f, (float)(Sleek2Config.bodyHeight * 3));
			this.addElement(this.climbButton);
			this.originField = new Sleek2Field();
			this.originField.transform.anchorMin = new Vector2(0f, 0f);
			this.originField.transform.anchorMax = new Vector2(1f, 0f);
			this.originField.transform.pivot = new Vector2(0f, 1f);
			this.originField.transform.offsetMin = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			this.originField.transform.offsetMax = new Vector2(0f, (float)(Sleek2Config.bodyHeight * 2));
			this.originField.fieldComponent.interactable = false;
			this.addElement(this.originField);
			this.translationField = new Sleek2Field();
			this.translationField.transform.anchorMin = new Vector2(0f, 0f);
			this.translationField.transform.anchorMax = new Vector2(1f, 0f);
			this.translationField.transform.pivot = new Vector2(0f, 1f);
			this.translationField.transform.offsetMin = new Vector2(0f, 0f);
			this.translationField.transform.offsetMax = new Vector2(0f, (float)Sleek2Config.bodyHeight);
			this.translationField.submitted += this.handleTextSubmitted;
			this.addElement(this.translationField);
			this.refreshFields();
			this.layoutComponent = base.gameObject.AddComponent<LayoutElement>();
			this.layoutComponent.preferredHeight = (float)(Sleek2Config.bodyHeight * 3);
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0007B888 File Offset: 0x00079C88
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0007B890 File Offset: 0x00079C90
		public TranslationLeaf originLeaf { get; protected set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0007B899 File Offset: 0x00079C99
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0007B8A1 File Offset: 0x00079CA1
		public TranslationLeaf translationLeaf { get; protected set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0007B8AA File Offset: 0x00079CAA
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0007B8B2 File Offset: 0x00079CB2
		public Sleek2ImageLabelButton climbButton { get; protected set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0007B8BB File Offset: 0x00079CBB
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0007B8C3 File Offset: 0x00079CC3
		public Sleek2Field originField { get; protected set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0007B8CC File Offset: 0x00079CCC
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x0007B8D4 File Offset: 0x00079CD4
		public Sleek2Field translationField { get; protected set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600136F RID: 4975 RVA: 0x0007B8DD File Offset: 0x00079CDD
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0007B8E5 File Offset: 0x00079CE5
		public LayoutElement layoutComponent { get; protected set; }

		// Token: 0x06001371 RID: 4977 RVA: 0x0007B8F0 File Offset: 0x00079CF0
		protected virtual void refreshFields()
		{
			if (this.originLeaf != null)
			{
				this.climbButton.label.textComponent.text = this.originLeaf.getReferenceTo().ToString();
				this.originField.text = this.originLeaf.text;
			}
			if (this.translationLeaf != null)
			{
				this.translationField.text = this.translationLeaf.text;
			}
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0007B970 File Offset: 0x00079D70
		protected virtual void handleTextSubmitted(Sleek2Field field, string value)
		{
			if (this.originLeaf != null && this.translationLeaf != null)
			{
				DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Translation")));
				DevkitTransactionUtility.recordObjectDelta(this.translationLeaf);
				DevkitTransactionUtility.recordObjectDelta(this.translationLeaf.translation);
				this.translationLeaf.text = value;
				this.translationLeaf.version = this.originLeaf.version;
				this.translationLeaf.translation.isDirty = true;
				DevkitTransactionManager.endTransaction();
			}
		}
	}
}
