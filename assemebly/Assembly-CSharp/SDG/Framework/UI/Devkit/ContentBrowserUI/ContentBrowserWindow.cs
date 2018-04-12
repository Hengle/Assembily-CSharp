using System;
using System.Collections.Generic;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.ContentBrowserUI
{
	// Token: 0x02000233 RID: 563
	public class ContentBrowserWindow : Sleek2Window
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x0006CF20 File Offset: 0x0006B320
		public ContentBrowserWindow()
		{
			base.gameObject.name = "Content_Browser";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Content_Browser.Title"));
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
			foreach (KeyValuePair<string, RootContentDirectory> keyValuePair in Assets.rootContentDirectories)
			{
				RootContentDirectory value = keyValuePair.Value;
				ContentBrowserRootDirectoryButton contentBrowserRootDirectoryButton = new ContentBrowserRootDirectoryButton(value);
				contentBrowserRootDirectoryButton.clicked += this.handleRootDirectoryButtonClicked;
				this.rootsPanel.addElement(contentBrowserRootDirectoryButton);
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
			ContentBrowserWindow.browsed += this.handleBrowsed;
			this.handleBrowsed(ContentBrowserWindow.currentDirectory);
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001093 RID: 4243 RVA: 0x0006D350 File Offset: 0x0006B750
		// (remove) Token: 0x06001094 RID: 4244 RVA: 0x0006D384 File Offset: 0x0006B784
		protected static event ContentBrowserWindow.ContentBrowserWindowBrowsedHandler browsed;

		// Token: 0x06001095 RID: 4245 RVA: 0x0006D3B8 File Offset: 0x0006B7B8
		public static void browse(ContentDirectory directory)
		{
			ContentBrowserWindow.currentDirectory = directory;
			if (ContentBrowserWindow.browsed != null)
			{
				ContentBrowserWindow.browsed(ContentBrowserWindow.currentDirectory);
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0006D3D9 File Offset: 0x0006B7D9
		protected override void readWindow(IFormattedFileReader reader)
		{
			this.separator.handle.value = reader.readValue<float>("Split");
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0006D3F6 File Offset: 0x0006B7F6
		protected override void writeWindow(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Split", this.separator.handle.value);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0006D413 File Offset: 0x0006B813
		protected override void triggerDestroyed()
		{
			ContentBrowserWindow.browsed -= this.handleBrowsed;
			base.triggerDestroyed();
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0006D42C File Offset: 0x0006B82C
		protected void handleBrowsed(ContentDirectory directory)
		{
			this.pathPanel.clearElements();
			this.itemsPanel.clearElements();
			this.itemsPanel.transform.offsetMin = new Vector2(0f, 0f);
			this.itemsPanel.transform.offsetMax = new Vector2(0f, 0f);
			if (directory == null)
			{
				return;
			}
			for (int i = 0; i < directory.files.Count; i++)
			{
				ContentBrowserFileButton element = new ContentBrowserFileButton(directory.files[i]);
				this.itemsPanel.addElement(element);
			}
			foreach (KeyValuePair<string, ContentDirectory> keyValuePair in directory.directories)
			{
				ContentDirectory value = keyValuePair.Value;
				ContentBrowserDirectoryButton contentBrowserDirectoryButton = new ContentBrowserDirectoryButton(value);
				contentBrowserDirectoryButton.clicked += this.handleDirectoryButtonClicked;
				this.itemsPanel.addElement(contentBrowserDirectoryButton);
			}
			do
			{
				ContentBrowserDirectoryButton contentBrowserDirectoryButton2 = new ContentBrowserDirectoryButton(directory);
				contentBrowserDirectoryButton2.transform.anchorMin = new Vector2(0f, 0f);
				contentBrowserDirectoryButton2.transform.anchorMax = new Vector2(0f, 1f);
				contentBrowserDirectoryButton2.transform.pivot = new Vector2(0f, 1f);
				contentBrowserDirectoryButton2.transform.sizeDelta = new Vector2(150f, 0f);
				contentBrowserDirectoryButton2.clicked += this.handleDirectoryButtonClicked;
				this.pathPanel.addElement(contentBrowserDirectoryButton2, 0);
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

		// Token: 0x0600109A RID: 4250 RVA: 0x0006D688 File Offset: 0x0006BA88
		protected void handleRootDirectoryButtonClicked(Sleek2ImageButton button)
		{
			ContentBrowserWindow.browse((button as ContentBrowserRootDirectoryButton).directory);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0006D69A File Offset: 0x0006BA9A
		protected void handleDirectoryButtonClicked(Sleek2ImageButton button)
		{
			ContentBrowserWindow.browse((button as ContentBrowserDirectoryButton).directory);
		}

		// Token: 0x04000A04 RID: 2564
		protected static ContentDirectory currentDirectory;

		// Token: 0x04000A05 RID: 2565
		protected Sleek2Element rootsBox;

		// Token: 0x04000A06 RID: 2566
		protected Sleek2Element itemsBox;

		// Token: 0x04000A07 RID: 2567
		protected Sleek2Separator separator;

		// Token: 0x04000A08 RID: 2568
		protected Sleek2Element rootsPanel;

		// Token: 0x04000A09 RID: 2569
		protected Sleek2Scrollview rootsView;

		// Token: 0x04000A0A RID: 2570
		protected Sleek2HorizontalScrollviewContents pathPanel;

		// Token: 0x04000A0B RID: 2571
		protected Sleek2Element itemsPanel;

		// Token: 0x04000A0C RID: 2572
		protected Sleek2Scrollview itemsView;

		// Token: 0x04000A0D RID: 2573
		protected Sleek2Scrollbar rootsScrollbar;

		// Token: 0x02000234 RID: 564
		// (Invoke) Token: 0x0600109D RID: 4253
		protected delegate void ContentBrowserWindowBrowsedHandler(ContentDirectory directory);
	}
}
