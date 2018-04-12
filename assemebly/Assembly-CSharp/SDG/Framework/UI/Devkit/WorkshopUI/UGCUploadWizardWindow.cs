using System;
using SDG.Framework.Debug;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.WorkshopUI
{
	// Token: 0x020002B1 RID: 689
	public class UGCUploadWizardWindow : Sleek2Window
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x0007FE28 File Offset: 0x0007E228
		public UGCUploadWizardWindow()
		{
			base.gameObject.name = "UGC_Upload_Wizard";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.UGC_Upload_Wizard.Title"));
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 1f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(0f, -420f);
			this.inspector.transform.offsetMax = new Vector2(0f, 0f);
			this.inspector.inspect(this);
			base.safePanel.addElement(this.inspector);
			this.publishedFilesView = new Sleek2Scrollview();
			this.publishedFilesView.transform.anchorMin = new Vector2(0f, 1f);
			this.publishedFilesView.transform.anchorMax = new Vector2(1f, 1f);
			this.publishedFilesView.transform.pivot = new Vector2(0.5f, 1f);
			this.publishedFilesView.transform.offsetMin = new Vector2(0f, -560f);
			this.publishedFilesView.transform.offsetMax = new Vector2(0f, -460f);
			this.publishedFilesView.vertical = true;
			this.publishedFilesPanel = new Sleek2Element();
			this.publishedFilesPanel.name = "Panel";
			GridLayoutGroup gridLayoutGroup = this.publishedFilesPanel.gameObject.AddComponent<GridLayoutGroup>();
			gridLayoutGroup.cellSize = new Vector2(200f, 50f);
			gridLayoutGroup.spacing = new Vector2(5f, 5f);
			ContentSizeFitter contentSizeFitter = this.publishedFilesPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.publishedFilesPanel.transform.reset();
			this.publishedFilesPanel.transform.pivot = new Vector2(0f, 1f);
			this.publishedFilesView.panel = this.publishedFilesPanel;
			base.safePanel.addElement(this.publishedFilesView);
			this.legalButton = new Sleek2ImageTranslatedLabelButton();
			this.legalButton.transform.anchorMin = new Vector2(0f, 1f);
			this.legalButton.transform.anchorMax = new Vector2(1f, 1f);
			this.legalButton.transform.pivot = new Vector2(0.5f, 1f);
			this.legalButton.transform.offsetMin = new Vector2(0f, -440f);
			this.legalButton.transform.offsetMax = new Vector2(0f, -420f);
			this.legalButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.UGC_Upload_Wizard.Legal.Label"));
			this.legalButton.label.translation.format();
			this.legalButton.clicked += this.handleLegalButtonClicked;
			base.safePanel.addElement(this.legalButton);
			this.uploadButton = new Sleek2ImageTranslatedLabelButton();
			this.uploadButton.transform.anchorMin = new Vector2(0f, 1f);
			this.uploadButton.transform.anchorMax = new Vector2(1f, 1f);
			this.uploadButton.transform.pivot = new Vector2(0.5f, 1f);
			this.uploadButton.transform.offsetMin = new Vector2(0f, -460f);
			this.uploadButton.transform.offsetMax = new Vector2(0f, -440f);
			this.uploadButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.UGC_Upload_Wizard.Upload.Label"));
			this.uploadButton.label.translation.format();
			this.uploadButton.clicked += this.handleUploadButtonClicked;
			base.safePanel.addElement(this.uploadButton);
			TempSteamworksWorkshop workshopService = Provider.provider.workshopService;
			workshopService.onPublishedAdded = (TempSteamworksWorkshop.PublishedAdded)Delegate.Combine(workshopService.onPublishedAdded, new TempSteamworksWorkshop.PublishedAdded(this.onPublishedAdded));
			TempSteamworksWorkshop workshopService2 = Provider.provider.workshopService;
			workshopService2.onPublishedRemoved = (TempSteamworksWorkshop.PublishedRemoved)Delegate.Combine(workshopService2.onPublishedRemoved, new TempSteamworksWorkshop.PublishedRemoved(this.onPublishedRemoved));
			this.onPublishedAdded();
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0008032F File Offset: 0x0007E72F
		protected virtual void handleLegalButtonClicked(Sleek2ImageButton button)
		{
			Provider.provider.browserService.open("http://steamcommunity.com/sharedfiles/workshoplegalagreement/?appid=304930");
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x00080348 File Offset: 0x0007E748
		protected virtual void handleUploadButtonClicked(Sleek2ImageButton button)
		{
			Provider.provider.workshopService.prepareUGC(this.itemName, this.itemDesc, this.contentPath.absolutePath, this.previewImagePath.absolutePath, this.changeNote, this.itemType, this.itemTag, this.visibility);
			Provider.provider.workshopService.createUGC(this.curated);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x000803B4 File Offset: 0x0007E7B4
		protected virtual void handlePublishedFileButtonClicked(Sleek2ImageButton button)
		{
			SteamPublished file = (button as UGCUploadPublishedFileButton).file;
			Provider.provider.workshopService.prepareUGC(this.itemName, this.itemDesc, this.contentPath.absolutePath, this.previewImagePath.absolutePath, this.changeNote, this.itemType, this.itemTag, this.visibility);
			Provider.provider.workshopService.prepareUGC(file.id);
			Provider.provider.workshopService.updateUGC();
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0008043C File Offset: 0x0007E83C
		protected virtual void onPublishedAdded()
		{
			for (int i = 0; i < Provider.provider.workshopService.published.Count; i++)
			{
				SteamPublished newFile = Provider.provider.workshopService.published[i];
				UGCUploadPublishedFileButton ugcuploadPublishedFileButton = new UGCUploadPublishedFileButton(newFile);
				ugcuploadPublishedFileButton.clicked += this.handlePublishedFileButtonClicked;
				this.publishedFilesPanel.addElement(ugcuploadPublishedFileButton);
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x000804AA File Offset: 0x0007E8AA
		protected virtual void onPublishedRemoved()
		{
			this.publishedFilesPanel.clearElements();
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000804B8 File Offset: 0x0007E8B8
		public override void destroy()
		{
			TempSteamworksWorkshop workshopService = Provider.provider.workshopService;
			workshopService.onPublishedAdded = (TempSteamworksWorkshop.PublishedAdded)Delegate.Remove(workshopService.onPublishedAdded, new TempSteamworksWorkshop.PublishedAdded(this.onPublishedAdded));
			TempSteamworksWorkshop workshopService2 = Provider.provider.workshopService;
			workshopService2.onPublishedRemoved = (TempSteamworksWorkshop.PublishedRemoved)Delegate.Remove(workshopService2.onPublishedRemoved, new TempSteamworksWorkshop.PublishedRemoved(this.onPublishedRemoved));
			base.destroy();
		}

		// Token: 0x04000B8F RID: 2959
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Item_Name.Name", null)]
		public string itemName;

		// Token: 0x04000B90 RID: 2960
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Item_Desc.Name", null)]
		public string itemDesc;

		// Token: 0x04000B91 RID: 2961
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Content_Path.Name", null)]
		public InspectableDirectoryPath contentPath;

		// Token: 0x04000B92 RID: 2962
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Preview_Image_Path.Name", null)]
		public InspectableFilePath previewImagePath = new InspectableFilePath("*.png");

		// Token: 0x04000B93 RID: 2963
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Change_Note.Name", null)]
		public string changeNote;

		// Token: 0x04000B94 RID: 2964
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Item_Type.Name", null)]
		public ESteamUGCType itemType;

		// Token: 0x04000B95 RID: 2965
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Item_Tag.Name", null)]
		public string itemTag;

		// Token: 0x04000B96 RID: 2966
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Visibility.Name", null)]
		public ESteamUGCVisibility visibility;

		// Token: 0x04000B97 RID: 2967
		[Inspectable("#SDG::Devkit.Window.UGC_Upload_Wizard.Curated.Name", null)]
		public bool curated;

		// Token: 0x04000B98 RID: 2968
		protected Sleek2Inspector inspector;

		// Token: 0x04000B99 RID: 2969
		protected Sleek2Element publishedFilesPanel;

		// Token: 0x04000B9A RID: 2970
		protected Sleek2Scrollview publishedFilesView;

		// Token: 0x04000B9B RID: 2971
		protected Sleek2ImageTranslatedLabelButton legalButton;

		// Token: 0x04000B9C RID: 2972
		protected Sleek2ImageTranslatedLabelButton uploadButton;
	}
}
