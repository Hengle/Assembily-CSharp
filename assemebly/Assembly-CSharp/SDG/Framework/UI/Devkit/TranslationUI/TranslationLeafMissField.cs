using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000293 RID: 659
	public class TranslationLeafMissField : TranslationLeafField
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x0007B30C File Offset: 0x0007970C
		public TranslationLeafMissField(TranslationReference newReference) : base(null)
		{
			base.name = "Leaf_Miss";
			this.reference = newReference;
			base.climbButton.isVisible = false;
			base.keyField.isVisible = false;
			base.textField.isVisible = false;
			base.versionField.isVisible = false;
			this.addButton = new Sleek2ImageLabelButton();
			this.addButton.transform.anchorMin = new Vector2(0f, 0f);
			this.addButton.transform.anchorMax = new Vector2(0f, 1f);
			this.addButton.transform.offsetMin = new Vector2(0f, 0f);
			this.addButton.transform.offsetMax = new Vector2((float)(Sleek2Config.bodyHeight * 2), 0f);
			this.addButton.label.textComponent.text = "+";
			this.addButton.clicked += this.handleAddButtonClicked;
			this.addElement(this.addButton);
			this.referenceButton = new Sleek2ImageLabelButton();
			this.referenceButton.transform.anchorMin = new Vector2(0f, 0f);
			this.referenceButton.transform.anchorMax = new Vector2(1f, 1f);
			this.referenceButton.transform.offsetMin = new Vector2((float)(Sleek2Config.bodyHeight * 2), 0f);
			this.referenceButton.transform.offsetMax = new Vector2(0f, 0f);
			this.referenceButton.label.textComponent.text = this.reference.ToString();
			this.addElement(this.referenceButton);
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0007B4E9 File Offset: 0x000798E9
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x0007B4F1 File Offset: 0x000798F1
		public TranslationReference reference { get; protected set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0007B4FA File Offset: 0x000798FA
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0007B502 File Offset: 0x00079902
		public Sleek2ImageLabelButton addButton { get; protected set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0007B50B File Offset: 0x0007990B
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0007B513 File Offset: 0x00079913
		public Sleek2ImageLabelButton referenceButton { get; protected set; }

		// Token: 0x06001363 RID: 4963 RVA: 0x0007B51C File Offset: 0x0007991C
		protected virtual void handleAddButtonClicked(Sleek2ImageButton button)
		{
			base.leaf = Translator.addLeaf(this.reference);
			if (base.leaf == null)
			{
				return;
			}
			base.leaf.branch.keyChanged += this.handleKeyChanged;
			base.leaf.versionChanged += this.handleVersionChanged;
			base.leaf.translation.isDirty = true;
			base.climbButton.isVisible = true;
			base.keyField.isVisible = true;
			base.textField.isVisible = true;
			base.versionField.isVisible = true;
			this.addButton.isVisible = false;
			this.referenceButton.isVisible = false;
			this.refreshKey();
			this.refreshFields();
			this.refreshVersion();
		}
	}
}
