using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TranslationUI
{
	// Token: 0x02000292 RID: 658
	public class TranslationLeafField : Sleek2Element
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0007ADC4 File Offset: 0x000791C4
		public TranslationLeafField(TranslationLeaf newLeaf)
		{
			base.name = "Leaf";
			this.leaf = newLeaf;
			if (this.leaf != null)
			{
				this.leaf.branch.keyChanged += this.handleKeyChanged;
				this.leaf.versionChanged += this.handleVersionChanged;
			}
			this.climbButton = new Sleek2ImageLabelButton();
			this.climbButton.transform.anchorMin = new Vector2(0f, 0.5f);
			this.climbButton.transform.anchorMax = new Vector2(0.5f, 1f);
			this.climbButton.transform.offsetMin = new Vector2(0f, 0f);
			this.climbButton.transform.offsetMax = new Vector2(0f, 0f);
			this.addElement(this.climbButton);
			this.keyField = new Sleek2Field();
			this.keyField.transform.anchorMin = new Vector2(0.5f, 0.5f);
			this.keyField.transform.anchorMax = new Vector2(1f, 1f);
			this.keyField.transform.offsetMin = new Vector2(0f, 0f);
			this.keyField.transform.offsetMax = new Vector2(0f, 0f);
			this.keyField.submitted += this.handleKeySubmitted;
			this.addElement(this.keyField);
			this.textField = new Sleek2Field();
			this.textField.transform.anchorMin = new Vector2(0f, 0f);
			this.textField.transform.anchorMax = new Vector2(1f, 0.5f);
			this.textField.transform.offsetMin = new Vector2(0f, 0f);
			this.textField.transform.offsetMax = new Vector2(-40f, 0f);
			this.textField.submitted += this.handleTextSubmitted;
			this.addElement(this.textField);
			this.versionField = new Sleek2IntField();
			this.versionField.transform.anchorMin = new Vector2(1f, 0f);
			this.versionField.transform.anchorMax = new Vector2(1f, 0.5f);
			this.versionField.transform.offsetMin = new Vector2(-40f, 0f);
			this.versionField.transform.offsetMax = new Vector2(0f, 0f);
			this.versionField.fieldComponent.interactable = false;
			this.addElement(this.versionField);
			this.refreshKey();
			this.refreshFields();
			this.refreshVersion();
			this.layoutComponent = base.gameObject.AddComponent<LayoutElement>();
			this.layoutComponent.preferredHeight = (float)(Sleek2Config.bodyHeight * 2);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0007B0EC File Offset: 0x000794EC
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0007B0F4 File Offset: 0x000794F4
		public TranslationLeaf leaf { get; protected set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0007B0FD File Offset: 0x000794FD
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0007B105 File Offset: 0x00079505
		public Sleek2ImageLabelButton climbButton { get; protected set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0007B10E File Offset: 0x0007950E
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0007B116 File Offset: 0x00079516
		public Sleek2Field keyField { get; protected set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0007B11F File Offset: 0x0007951F
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0007B127 File Offset: 0x00079527
		public Sleek2Field textField { get; protected set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0007B130 File Offset: 0x00079530
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x0007B138 File Offset: 0x00079538
		public Sleek2IntField versionField { get; protected set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x0007B141 File Offset: 0x00079541
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x0007B149 File Offset: 0x00079549
		public LayoutElement layoutComponent { get; protected set; }

		// Token: 0x06001354 RID: 4948 RVA: 0x0007B154 File Offset: 0x00079554
		protected virtual void refreshKey()
		{
			if (this.leaf != null)
			{
				this.climbButton.label.textComponent.text = this.leaf.getReferenceTo().ToString();
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0007B19A File Offset: 0x0007959A
		protected virtual void refreshFields()
		{
			if (this.leaf != null)
			{
				this.keyField.text = this.leaf.branch.key;
				this.textField.text = this.leaf.text;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0007B1D8 File Offset: 0x000795D8
		protected virtual void refreshVersion()
		{
			if (this.leaf != null)
			{
				this.versionField.value = this.leaf.version;
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0007B1FB File Offset: 0x000795FB
		protected virtual void handleKeyChanged(TranslationBranch branch, string oldKey, string newKey)
		{
			this.refreshKey();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0007B203 File Offset: 0x00079603
		protected virtual void handleVersionChanged(TranslationLeaf leaf, int oldVersion, int newVersion)
		{
			this.refreshVersion();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0007B20B File Offset: 0x0007960B
		protected virtual void handleKeySubmitted(Sleek2Field field, string value)
		{
			if (this.leaf != null)
			{
				this.leaf.branch.key = value;
				this.leaf.translation.isDirty = true;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0007B23C File Offset: 0x0007963C
		protected virtual void handleTextSubmitted(Sleek2Field field, string value)
		{
			if (this.leaf != null)
			{
				DevkitTransactionManager.beginTransaction(new TranslatedText(new TranslationReference("#SDG::Devkit.Transactions.Translation")));
				DevkitTransactionUtility.recordObjectDelta(this.leaf);
				DevkitTransactionUtility.recordObjectDelta(this.leaf.translation);
				this.leaf.text = value;
				this.leaf.version++;
				this.leaf.translation.isDirty = true;
				DevkitTransactionManager.endTransaction();
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0007B2B8 File Offset: 0x000796B8
		protected override void triggerDestroyed()
		{
			if (this.leaf != null)
			{
				this.leaf.branch.keyChanged -= this.handleKeyChanged;
				this.leaf.versionChanged -= this.handleVersionChanged;
			}
			base.triggerDestroyed();
		}
	}
}
