using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LoadUI
{
	// Token: 0x02000280 RID: 640
	public class DevkitLoadContainer : Sleek2PopoutContainer
	{
		// Token: 0x060012D4 RID: 4820 RVA: 0x00078494 File Offset: 0x00076894
		public DevkitLoadContainer()
		{
			base.name = "Load";
			this.selectedLevelInfo = null;
			base.titlebar.titleLabel.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.Load.Title"));
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
			sleek2TranslatedLabel.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Load.Cancel"));
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
			sleek2TranslatedLabel2.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Load.Submit"));
			sleek2TranslatedLabel2.translation.format();
			sleek2TranslatedLabel2.textComponent.color = Sleek2Config.darkTextColor;
			this.submitButton.addElement(sleek2TranslatedLabel2);
			this.updateList();
			Level.onLevelsRefreshed = (LevelsRefreshed)Delegate.Combine(Level.onLevelsRefreshed, new LevelsRefreshed(this.onLevelsRefreshed));
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000787C0 File Offset: 0x00076BC0
		protected void updateList()
		{
			this.list.clearElements();
			LevelInfo[] levels = Level.getLevels(ESingleplayerMapCategory.EDITABLE);
			foreach (LevelInfo newLevelInfo in levels)
			{
				Sleek2Loadable sleek2Loadable = new Sleek2Loadable(newLevelInfo);
				sleek2Loadable.transform.anchorMin = new Vector2(0f, 1f);
				sleek2Loadable.transform.anchorMax = new Vector2(1f, 1f);
				sleek2Loadable.transform.pivot = new Vector2(0.5f, 1f);
				sleek2Loadable.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
				sleek2Loadable.clicked += this.handleLoadableClicked;
				this.list.addElement(sleek2Loadable);
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0007888F File Offset: 0x00076C8F
		protected virtual void handleLoadableClicked(Sleek2ImageButton button)
		{
			this.selectedLevelInfo = (button as Sleek2Loadable).levelInfo;
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000788A2 File Offset: 0x00076CA2
		protected virtual void handleCancelButtonClicked(Sleek2ImageButton button)
		{
			DevkitWindowManager.removeContainer(this);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000788AA File Offset: 0x00076CAA
		protected virtual void handleSubmitButtonClicked(Sleek2ImageButton button)
		{
			Level.edit(this.selectedLevelInfo, true);
			DevkitWindowManager.removeContainer(this);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000788BE File Offset: 0x00076CBE
		protected virtual void onLevelsRefreshed()
		{
			this.updateList();
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000788C6 File Offset: 0x00076CC6
		protected override void triggerDestroyed()
		{
			Level.onLevelsRefreshed = (LevelsRefreshed)Delegate.Remove(Level.onLevelsRefreshed, new LevelsRefreshed(this.onLevelsRefreshed));
			base.triggerDestroyed();
		}

		// Token: 0x04000AD3 RID: 2771
		protected Sleek2Element list;

		// Token: 0x04000AD4 RID: 2772
		protected Sleek2Scrollview view;

		// Token: 0x04000AD5 RID: 2773
		protected Sleek2ImageButton cancelButton;

		// Token: 0x04000AD6 RID: 2774
		protected Sleek2ImageButton submitButton;

		// Token: 0x04000AD7 RID: 2775
		protected LevelInfo selectedLevelInfo;
	}
}
