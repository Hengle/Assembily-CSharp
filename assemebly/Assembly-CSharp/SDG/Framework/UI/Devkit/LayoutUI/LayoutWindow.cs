using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LayoutUI
{
	// Token: 0x0200027F RID: 639
	public class LayoutWindow : Sleek2Window
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x000780EC File Offset: 0x000764EC
		public LayoutWindow()
		{
			base.name = "Layouts";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Layout.Title"));
			base.tab.label.translation.format();
			this.nameField = new Sleek2Field();
			this.nameField.transform.anchorMin = new Vector2(0f, 1f);
			this.nameField.transform.anchorMax = new Vector2(1f, 1f);
			this.nameField.transform.pivot = new Vector2(0.5f, 1f);
			this.nameField.transform.offsetMin = new Vector2(5f, -35f);
			this.nameField.transform.offsetMax = new Vector2(-5f, -5f);
			this.addElement(this.nameField);
			this.loadButton = new Sleek2ImageButton();
			this.loadButton.transform.anchorMin = new Vector2(0f, 1f);
			this.loadButton.transform.anchorMax = new Vector2(0.5f, 1f);
			this.loadButton.transform.pivot = new Vector2(0.5f, 1f);
			this.loadButton.transform.offsetMin = new Vector2(5f, -75f);
			this.loadButton.transform.offsetMax = new Vector2(-5f, -45f);
			this.loadButton.clicked += this.handleLoadClicked;
			this.addElement(this.loadButton);
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.reset();
			sleek2TranslatedLabel.textComponent.color = Sleek2Config.darkTextColor;
			sleek2TranslatedLabel.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Layout.Load"));
			sleek2TranslatedLabel.translation.format();
			this.loadButton.addElement(sleek2TranslatedLabel);
			this.saveButton = new Sleek2ImageButton();
			this.saveButton.transform.anchorMin = new Vector2(0.5f, 1f);
			this.saveButton.transform.anchorMax = new Vector2(1f, 1f);
			this.saveButton.transform.pivot = new Vector2(0.5f, 1f);
			this.saveButton.transform.offsetMin = new Vector2(5f, -75f);
			this.saveButton.transform.offsetMax = new Vector2(-5f, -45f);
			this.saveButton.clicked += this.handleSaveClicked;
			this.addElement(this.saveButton);
			Sleek2TranslatedLabel sleek2TranslatedLabel2 = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel2.transform.reset();
			sleek2TranslatedLabel2.textComponent.color = Sleek2Config.darkTextColor;
			sleek2TranslatedLabel2.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Layout.Save"));
			sleek2TranslatedLabel2.translation.format();
			this.saveButton.addElement(sleek2TranslatedLabel2);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00078430 File Offset: 0x00076830
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x00078438 File Offset: 0x00076838
		public Sleek2Field nameField { get; protected set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00078441 File Offset: 0x00076841
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x00078449 File Offset: 0x00076849
		public Sleek2ImageButton loadButton { get; protected set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00078452 File Offset: 0x00076852
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0007845A File Offset: 0x0007685A
		public Sleek2ImageButton saveButton { get; protected set; }

		// Token: 0x060012D2 RID: 4818 RVA: 0x00078463 File Offset: 0x00076863
		protected virtual void handleLoadClicked(Sleek2ImageButton button)
		{
			DevkitWindowLayout.load(this.nameField.fieldComponent.text);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0007847A File Offset: 0x0007687A
		protected virtual void handleSaveClicked(Sleek2ImageButton button)
		{
			DevkitWindowLayout.save(this.nameField.fieldComponent.text);
		}
	}
}
