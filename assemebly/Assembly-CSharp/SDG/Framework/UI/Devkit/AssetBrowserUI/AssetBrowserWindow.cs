using System;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x0200022E RID: 558
	public class AssetBrowserWindow : Sleek2Window
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0006C564 File Offset: 0x0006A964
		public AssetBrowserWindow()
		{
			base.gameObject.name = "Asset_Browser";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Asset_Browser.Title"));
			base.tab.label.translation.format();
			this.rootsBox = new Sleek2Element();
			this.rootsBox.name = "Roots";
			this.addElement(this.rootsBox);
			this.itemsBox = new Sleek2Element();
			this.itemsBox.name = "Items";
			this.addElement(this.itemsBox);
			this.separator = new Sleek2Separator();
			this.separator.handle.value = 0.25f;
			this.separator.handle.a = this.rootsBox.transform;
			this.separator.handle.b = this.itemsBox.transform;
			this.addElement(this.separator);
			this.rootsView = new Sleek2Scrollview();
			this.rootsView.transform.reset();
			this.rootsView.transform.offsetMin = new Vector2(5f, 5f);
			this.rootsView.transform.offsetMax = new Vector2(-5f, -5f);
			this.rootsView.vertical = true;
			this.rootsPanel = new Sleek2VerticalScrollviewContents();
			this.rootsPanel.name = "Panel";
			this.rootsView.panel = this.rootsPanel;
			this.rootsBox.addElement(this.rootsView);
			for (int i = 0; i < Assets.rootAssetDirectories.Count; i++)
			{
				RootAssetDirectory newDirectory = Assets.rootAssetDirectories[i];
				AssetBrowserRootDirectoryButton assetBrowserRootDirectoryButton = new AssetBrowserRootDirectoryButton(newDirectory);
				assetBrowserRootDirectoryButton.clicked += this.handleRootDirectoryButtonClicked;
				this.rootsPanel.addElement(assetBrowserRootDirectoryButton);
			}
			this.pathPanel = new Sleek2HorizontalScrollviewContents();
			this.pathPanel.name = "Path";
			this.pathPanel.transform.anchorMin = new Vector2(0f, 1f);
			this.pathPanel.transform.anchorMax = new Vector2(1f, 1f);
			this.pathPanel.transform.pivot = new Vector2(0f, 1f);
			this.pathPanel.transform.offsetMin = new Vector2(5f, -55f);
			this.pathPanel.transform.offsetMax = new Vector2(5f, -5f);
			this.itemsBox.addElement(this.pathPanel);
			this.itemsView = new Sleek2Scrollview();
			this.itemsView.transform.reset();
			this.itemsView.transform.offsetMin = new Vector2(5f, 5f);
			this.itemsView.transform.offsetMax = new Vector2(-5f, -60f);
			this.itemsView.vertical = true;
			this.itemsPanel = new Sleek2Element();
			this.itemsPanel.name = "Panel";
			GridLayoutGroup gridLayoutGroup = this.itemsPanel.gameObject.AddComponent<GridLayoutGroup>();
			gridLayoutGroup.cellSize = new Vector2(200f, 50f);
			gridLayoutGroup.spacing = new Vector2(5f, 5f);
			ContentSizeFitter contentSizeFitter = this.itemsPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.itemsPanel.transform.reset();
			this.itemsPanel.transform.pivot = new Vector2(0f, 1f);
			this.itemsView.panel = this.itemsPanel;
			this.itemsBox.addElement(this.itemsView);
			AssetBrowserWindow.browsed += this.handleBrowsed;
			this.handleBrowsed(AssetBrowserWindow.currentDirectory);
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001079 RID: 4217 RVA: 0x0006C968 File Offset: 0x0006AD68
		// (remove) Token: 0x0600107A RID: 4218 RVA: 0x0006C99C File Offset: 0x0006AD9C
		protected static event AssetBrowserWindow.AssetBrowserWindowBrowsedHandler browsed;

		// Token: 0x0600107B RID: 4219 RVA: 0x0006C9D0 File Offset: 0x0006ADD0
		public static void browse(AssetDirectory directory)
		{
			AssetBrowserWindow.currentDirectory = directory;
			if (AssetBrowserWindow.browsed != null)
			{
				AssetBrowserWindow.browsed(AssetBrowserWindow.currentDirectory);
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0006C9F1 File Offset: 0x0006ADF1
		protected override void readWindow(IFormattedFileReader reader)
		{
			this.separator.handle.value = reader.readValue<float>("Split");
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0006CA0E File Offset: 0x0006AE0E
		protected override void writeWindow(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Split", this.separator.handle.value);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0006CA2B File Offset: 0x0006AE2B
		protected override void triggerDestroyed()
		{
			AssetBrowserWindow.browsed -= this.handleBrowsed;
			base.triggerDestroyed();
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0006CA44 File Offset: 0x0006AE44
		protected void handleBrowsed(AssetDirectory directory)
		{
			this.pathPanel.clearElements();
			this.itemsPanel.clearElements();
			this.itemsPanel.transform.offsetMin = new Vector2(0f, 0f);
			this.itemsPanel.transform.offsetMax = new Vector2(0f, 0f);
			if (directory == null)
			{
				return;
			}
			for (int i = 0; i < directory.assets.Count; i++)
			{
				AssetBrowserAssetButton assetBrowserAssetButton = new AssetBrowserAssetButton(directory.assets[i]);
				assetBrowserAssetButton.clicked += this.handleAssetButtonClicked;
				this.itemsPanel.addElement(assetBrowserAssetButton);
			}
			for (int j = 0; j < directory.directories.Count; j++)
			{
				AssetBrowserDirectoryButton assetBrowserDirectoryButton = new AssetBrowserDirectoryButton(directory.directories[j]);
				assetBrowserDirectoryButton.clicked += this.handleDirectoryButtonClicked;
				this.itemsPanel.addElement(assetBrowserDirectoryButton);
			}
			do
			{
				AssetBrowserDirectoryButton assetBrowserDirectoryButton2 = new AssetBrowserDirectoryButton(directory);
				assetBrowserDirectoryButton2.transform.anchorMin = new Vector2(0f, 0f);
				assetBrowserDirectoryButton2.transform.anchorMax = new Vector2(0f, 1f);
				assetBrowserDirectoryButton2.transform.pivot = new Vector2(0f, 1f);
				assetBrowserDirectoryButton2.transform.sizeDelta = new Vector2(150f, 0f);
				assetBrowserDirectoryButton2.clicked += this.handleDirectoryButtonClicked;
				this.pathPanel.addElement(assetBrowserDirectoryButton2, 0);
				if (directory.parent != null)
				{
					Sleek2Label sleek2Label = new Sleek2Label();
					sleek2Label.transform.anchorMin = new Vector2(0f, 0f);
					sleek2Label.transform.anchorMax = new Vector2(0f, 1f);
					sleek2Label.transform.pivot = new Vector2(0f, 1f);
					sleek2Label.transform.sizeDelta = new Vector2(30f, 0f);
					sleek2Label.textComponent.text = ">";
					this.pathPanel.addElement(sleek2Label, 0);
				}
			}
			while ((directory = directory.parent) != null);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0006CC84 File Offset: 0x0006B084
		protected void handleAssetButtonClicked(Sleek2ImageButton button)
		{
			Asset asset = (button as AssetBrowserAssetButton).asset;
			if (asset != null)
			{
				asset.clearHash();
				InspectorWindow.inspect(asset);
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0006CCAF File Offset: 0x0006B0AF
		protected void handleRootDirectoryButtonClicked(Sleek2ImageButton button)
		{
			AssetBrowserWindow.browse((button as AssetBrowserRootDirectoryButton).directory);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0006CCC1 File Offset: 0x0006B0C1
		protected void handleDirectoryButtonClicked(Sleek2ImageButton button)
		{
			AssetBrowserWindow.browse((button as AssetBrowserDirectoryButton).directory);
		}

		// Token: 0x040009F5 RID: 2549
		protected static AssetDirectory currentDirectory;

		// Token: 0x040009F6 RID: 2550
		protected Sleek2Element rootsBox;

		// Token: 0x040009F7 RID: 2551
		protected Sleek2Element itemsBox;

		// Token: 0x040009F8 RID: 2552
		protected Sleek2Separator separator;

		// Token: 0x040009F9 RID: 2553
		protected Sleek2Element rootsPanel;

		// Token: 0x040009FA RID: 2554
		protected Sleek2Scrollview rootsView;

		// Token: 0x040009FB RID: 2555
		protected Sleek2HorizontalScrollviewContents pathPanel;

		// Token: 0x040009FC RID: 2556
		protected Sleek2Element itemsPanel;

		// Token: 0x040009FD RID: 2557
		protected Sleek2Scrollview itemsView;

		// Token: 0x040009FE RID: 2558
		protected Sleek2Scrollbar rootsScrollbar;

		// Token: 0x0200022F RID: 559
		// (Invoke) Token: 0x06001084 RID: 4228
		protected delegate void AssetBrowserWindowBrowsedHandler(AssetDirectory directory);
	}
}
