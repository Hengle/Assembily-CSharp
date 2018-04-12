using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000291 RID: 657
	public class TranslationBranchButton : Sleek2Element
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x0007AB50 File Offset: 0x00078F50
		public TranslationBranchButton(TranslationBranch newBranch)
		{
			base.name = "Branch";
			this.branch = newBranch;
			this.branch.keyChanged += this.handleKeyChanged;
			this.climbButton = new Sleek2ImageLabelButton();
			this.climbButton.transform.anchorMin = new Vector2(0f, 0f);
			this.climbButton.transform.anchorMax = new Vector2(0.5f, 1f);
			this.climbButton.transform.offsetMin = new Vector2(0f, 0f);
			this.climbButton.transform.offsetMax = new Vector2(0f, 0f);
			this.addElement(this.climbButton);
			this.refreshClimbButton();
			this.keyField = new Sleek2Field();
			this.keyField.transform.anchorMin = new Vector2(0.5f, 0f);
			this.keyField.transform.anchorMax = new Vector2(1f, 1f);
			this.keyField.transform.offsetMin = new Vector2(0f, 0f);
			this.keyField.transform.offsetMax = new Vector2(0f, 0f);
			this.keyField.text = this.branch.key;
			this.keyField.submitted += this.handleKeySubmitted;
			this.addElement(this.keyField);
			this.layoutComponent = base.gameObject.AddComponent<LayoutElement>();
			this.layoutComponent.preferredHeight = (float)Sleek2Config.bodyHeight;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0007AD09 File Offset: 0x00079109
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x0007AD11 File Offset: 0x00079111
		public TranslationBranch branch { get; protected set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0007AD1A File Offset: 0x0007911A
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x0007AD22 File Offset: 0x00079122
		public LayoutElement layoutComponent { get; protected set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0007AD2B File Offset: 0x0007912B
		// (set) Token: 0x06001340 RID: 4928 RVA: 0x0007AD33 File Offset: 0x00079133
		public Sleek2ImageLabelButton climbButton { get; protected set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0007AD3C File Offset: 0x0007913C
		// (set) Token: 0x06001342 RID: 4930 RVA: 0x0007AD44 File Offset: 0x00079144
		public Sleek2Field keyField { get; protected set; }

		// Token: 0x06001343 RID: 4931 RVA: 0x0007AD50 File Offset: 0x00079150
		protected virtual void refreshClimbButton()
		{
			this.climbButton.label.textComponent.text = this.branch.getReferenceTo().ToString();
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0007AD8B File Offset: 0x0007918B
		protected virtual void handleKeyChanged(TranslationBranch branch, string oldKey, string newKey)
		{
			this.refreshClimbButton();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0007AD93 File Offset: 0x00079193
		protected virtual void handleKeySubmitted(Sleek2Field field, string value)
		{
			this.branch.key = value;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0007ADA1 File Offset: 0x000791A1
		protected override void triggerDestroyed()
		{
			this.branch.keyChanged -= this.handleKeyChanged;
			base.triggerDestroyed();
		}
	}
}
