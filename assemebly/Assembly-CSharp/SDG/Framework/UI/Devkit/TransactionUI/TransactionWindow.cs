using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.TransactionUI
{
	// Token: 0x02000290 RID: 656
	public class TransactionWindow : Sleek2Window
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x0007A688 File Offset: 0x00078A88
		public TransactionWindow()
		{
			base.gameObject.name = "Inspector";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Transaction.Title"));
			base.tab.label.translation.format();
			this.view = new Sleek2Scrollview();
			this.view.transform.reset();
			this.view.transform.offsetMin = new Vector2(5f, 5f);
			this.view.transform.offsetMax = new Vector2(-5f, -5f);
			this.view.vertical = true;
			this.panel = new Sleek2VerticalScrollviewContents();
			this.panel.name = "Panel";
			this.view.panel = this.panel;
			this.addElement(this.view);
			this.updateList();
			DevkitTransactionManager.transactionPerformed += this.handleTransactionPerformed;
			DevkitTransactionManager.transactionsChanged += this.handleTransactionsChanged;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0007A7AC File Offset: 0x00078BAC
		protected void updateList()
		{
			this.panel.clearElements();
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.anchorMin = new Vector2(0f, 1f);
			sleek2TranslatedLabel.transform.anchorMax = new Vector2(1f, 1f);
			sleek2TranslatedLabel.transform.pivot = new Vector2(0.5f, 1f);
			sleek2TranslatedLabel.transform.sizeDelta = new Vector2(0f, 50f);
			sleek2TranslatedLabel.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Transaction.Undo"));
			sleek2TranslatedLabel.translation.format();
			this.panel.addElement(sleek2TranslatedLabel);
			foreach (DevkitTransactionGroup newGroup in DevkitTransactionManager.getUndoable())
			{
				TransactionUndoButton transactionUndoButton = new TransactionUndoButton(newGroup);
				transactionUndoButton.transform.anchorMin = new Vector2(0f, 1f);
				transactionUndoButton.transform.anchorMax = new Vector2(1f, 1f);
				transactionUndoButton.transform.pivot = new Vector2(0.5f, 1f);
				transactionUndoButton.transform.sizeDelta = new Vector2(0f, 30f);
				transactionUndoButton.clicked += this.handleUndoButtonClicked;
				this.panel.addElement(transactionUndoButton);
			}
			Sleek2TranslatedLabel sleek2TranslatedLabel2 = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel2.transform.anchorMin = new Vector2(0f, 1f);
			sleek2TranslatedLabel2.transform.anchorMax = new Vector2(1f, 1f);
			sleek2TranslatedLabel2.transform.pivot = new Vector2(0.5f, 1f);
			sleek2TranslatedLabel2.transform.sizeDelta = new Vector2(0f, 50f);
			sleek2TranslatedLabel2.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Transaction.Redo"));
			sleek2TranslatedLabel2.translation.format();
			this.panel.addElement(sleek2TranslatedLabel2);
			foreach (DevkitTransactionGroup newGroup2 in DevkitTransactionManager.getRedoable())
			{
				TransactionRedoButton transactionRedoButton = new TransactionRedoButton(newGroup2);
				transactionRedoButton.transform.anchorMin = new Vector2(0f, 1f);
				transactionRedoButton.transform.anchorMax = new Vector2(1f, 1f);
				transactionRedoButton.transform.pivot = new Vector2(0.5f, 1f);
				transactionRedoButton.transform.sizeDelta = new Vector2(0f, 30f);
				transactionRedoButton.clicked += this.handleRedoButtonClicked;
				this.panel.addElement(transactionRedoButton);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0007AAB8 File Offset: 0x00078EB8
		protected void handleUndoButtonClicked(Sleek2ImageButton button)
		{
			DevkitTransactionGroup group = (button as TransactionUndoButton).group;
			for (DevkitTransactionGroup devkitTransactionGroup = null; devkitTransactionGroup != group; devkitTransactionGroup = DevkitTransactionManager.undo())
			{
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0007AAE8 File Offset: 0x00078EE8
		protected void handleRedoButtonClicked(Sleek2ImageButton button)
		{
			DevkitTransactionGroup group = (button as TransactionRedoButton).group;
			for (DevkitTransactionGroup devkitTransactionGroup = null; devkitTransactionGroup != group; devkitTransactionGroup = DevkitTransactionManager.redo())
			{
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0007AB15 File Offset: 0x00078F15
		protected void handleTransactionPerformed(DevkitTransactionGroup group)
		{
			this.updateList();
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0007AB1D File Offset: 0x00078F1D
		protected void handleTransactionsChanged()
		{
			this.updateList();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0007AB25 File Offset: 0x00078F25
		protected override void triggerDestroyed()
		{
			DevkitTransactionManager.transactionPerformed -= this.handleTransactionPerformed;
			DevkitTransactionManager.transactionsChanged -= this.handleTransactionsChanged;
			base.triggerDestroyed();
		}

		// Token: 0x04000B06 RID: 2822
		protected Sleek2Element panel;

		// Token: 0x04000B07 RID: 2823
		protected Sleek2Scrollview view;
	}
}
