using System;
using SDG.Framework.Devkit;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.SaveUI
{
	// Token: 0x02000285 RID: 645
	public class DevkitSaveContainer : Sleek2PopoutContainer
	{
		// Token: 0x060012EF RID: 4847 RVA: 0x00078F90 File Offset: 0x00077390
		public DevkitSaveContainer()
		{
			base.name = "Save";
			base.titlebar.titleLabel.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.Save.Title"));
			base.titlebar.titleLabel.translation.format();
			this.view = new Sleek2Scrollview();
			this.view.transform.reset();
			this.view.transform.offsetMin = new Vector2(5f, 5f);
			this.view.transform.offsetMax = new Vector2(-5f, -5f);
			this.view.vertical = true;
			this.list = new Sleek2VerticalScrollviewContents();
			this.list.name = "Panel";
			this.view.panel = this.list;
			base.bodyPanel.addElement(this.view);
			this.cancelButton = new Sleek2ImageButton();
			this.cancelButton.transform.anchorMin = new Vector2(0f, 0f);
			this.cancelButton.transform.anchorMax = new Vector2(0f, 0f);
			this.cancelButton.transform.pivot = new Vector2(0f, 0f);
			this.cancelButton.transform.sizeDelta = new Vector2(200f, (float)Sleek2Config.bodyHeight);
			this.cancelButton.clicked += this.handleCancelButtonClicked;
			base.bodyPanel.addElement(this.cancelButton);
			Sleek2TranslatedLabel sleek2TranslatedLabel = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel.transform.reset();
			sleek2TranslatedLabel.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Save.Cancel"));
			sleek2TranslatedLabel.translation.format();
			sleek2TranslatedLabel.textComponent.color = Sleek2Config.darkTextColor;
			this.cancelButton.addElement(sleek2TranslatedLabel);
			this.submitButton = new Sleek2ImageButton();
			this.submitButton.transform.anchorMin = new Vector2(1f, 0f);
			this.submitButton.transform.anchorMax = new Vector2(1f, 0f);
			this.submitButton.transform.pivot = new Vector2(1f, 0f);
			this.submitButton.transform.sizeDelta = new Vector2(200f, (float)Sleek2Config.bodyHeight);
			this.submitButton.clicked += this.handleSubmitButtonClicked;
			base.bodyPanel.addElement(this.submitButton);
			Sleek2TranslatedLabel sleek2TranslatedLabel2 = new Sleek2TranslatedLabel();
			sleek2TranslatedLabel2.transform.reset();
			sleek2TranslatedLabel2.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Save.Submit"));
			sleek2TranslatedLabel2.translation.format();
			sleek2TranslatedLabel2.textComponent.color = Sleek2Config.darkTextColor;
			this.submitButton.addElement(sleek2TranslatedLabel2);
			this.updateList();
			DirtyManager.markedDirty += this.handleMarkedDirty;
			DirtyManager.markedClean += this.handleMarkedClean;
			DirtyManager.saved += this.handleSaved;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000792C4 File Offset: 0x000776C4
		protected void updateList()
		{
			this.list.clearElements();
			foreach (IDirtyable newDirtyable in DirtyManager.dirty)
			{
				Sleek2Saveable sleek2Saveable = new Sleek2Saveable(newDirtyable);
				sleek2Saveable.transform.anchorMin = new Vector2(0f, 1f);
				sleek2Saveable.transform.anchorMax = new Vector2(1f, 1f);
				sleek2Saveable.transform.pivot = new Vector2(0.5f, 1f);
				sleek2Saveable.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
				this.list.addElement(sleek2Saveable);
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000793A0 File Offset: 0x000777A0
		protected void handleCancelButtonClicked(Sleek2ImageButton button)
		{
			DevkitWindowManager.removeContainer(this);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000793A8 File Offset: 0x000777A8
		protected void handleSubmitButtonClicked(Sleek2ImageButton button)
		{
			DirtyManager.save();
			DevkitWindowManager.removeContainer(this);
			if (Level.isLoaded && Level.isEditor)
			{
				Level.save();
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000793CE File Offset: 0x000777CE
		protected void handleMarkedDirty(IDirtyable item)
		{
			this.updateList();
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000793D6 File Offset: 0x000777D6
		protected void handleMarkedClean(IDirtyable item)
		{
			this.updateList();
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000793DE File Offset: 0x000777DE
		protected void handleSaved()
		{
			this.updateList();
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000793E6 File Offset: 0x000777E6
		protected override void triggerDestroyed()
		{
			DirtyManager.markedDirty -= this.handleMarkedDirty;
			DirtyManager.markedClean -= this.handleMarkedClean;
			base.triggerDestroyed();
		}

		// Token: 0x04000AE3 RID: 2787
		protected Sleek2Element list;

		// Token: 0x04000AE4 RID: 2788
		protected Sleek2Scrollview view;

		// Token: 0x04000AE5 RID: 2789
		protected Sleek2ImageButton cancelButton;

		// Token: 0x04000AE6 RID: 2790
		protected Sleek2ImageButton submitButton;
	}
}
