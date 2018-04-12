using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.ObjectBrowserUI
{
	// Token: 0x02000283 RID: 643
	public class ObjectBrowserWindow : Sleek2Window
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x000789DC File Offset: 0x00076DDC
		public ObjectBrowserWindow()
		{
			base.gameObject.name = "Object_Browser";
			this.searchLength = -1;
			this.searchResults = new List<ObjectAsset>();
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Object_Browser.Title"));
			base.tab.label.translation.format();
			this.searchField = new Sleek2Field();
			this.searchField.transform.anchorMin = new Vector2(0f, 1f);
			this.searchField.transform.anchorMax = new Vector2(1f, 1f);
			this.searchField.transform.pivot = new Vector2(0.5f, 1f);
			this.searchField.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.searchField.transform.offsetMax = new Vector2(0f, 0f);
			this.searchField.typed += this.handleSearchFieldTyped;
			base.safePanel.addElement(this.searchField);
			this.itemsView = new Sleek2Scrollview();
			this.itemsView.transform.anchorMin = new Vector2(0f, 0f);
			this.itemsView.transform.anchorMax = new Vector2(1f, 1f);
			this.itemsView.transform.pivot = new Vector2(0f, 1f);
			this.itemsView.transform.offsetMin = new Vector2(0f, 0f);
			this.itemsView.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 5));
			this.itemsView.vertical = true;
			this.itemsPanel = new Sleek2VerticalScrollviewContents();
			this.itemsPanel.name = "Panel";
			this.itemsView.panel = this.itemsPanel;
			base.safePanel.addElement(this.itemsView);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00078C0F File Offset: 0x0007700F
		// (set) Token: 0x060012E3 RID: 4835 RVA: 0x00078C16 File Offset: 0x00077016
		[TerminalCommandProperty("object_browser.show_official_objects", "include objects from vanilla game", true)]
		public static bool showOfficialObjects
		{
			get
			{
				return ObjectBrowserWindow._showOfficialObjects;
			}
			set
			{
				ObjectBrowserWindow._showOfficialObjects = value;
				TerminalUtility.printCommandPass("Set show_official_objects to: " + ObjectBrowserWindow.showOfficialObjects);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00078C37 File Offset: 0x00077037
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x00078C3E File Offset: 0x0007703E
		[TerminalCommandProperty("object_browser.show_curated_objects", "include objects from curated maps", true)]
		public static bool showCuratedObjects
		{
			get
			{
				return ObjectBrowserWindow._showCuratedObjects;
			}
			set
			{
				ObjectBrowserWindow._showCuratedObjects = value;
				TerminalUtility.printCommandPass("Set show_curated_objects to: " + ObjectBrowserWindow.showCuratedObjects);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00078C5F File Offset: 0x0007705F
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x00078C66 File Offset: 0x00077066
		[TerminalCommandProperty("object_browser.show_workshop_objects", "include objects from workshop downloads", true)]
		public static bool showWorkshopObjects
		{
			get
			{
				return ObjectBrowserWindow._showWorkshopObjects;
			}
			set
			{
				ObjectBrowserWindow._showWorkshopObjects = value;
				TerminalUtility.printCommandPass("Set show_workshop_objects to: " + ObjectBrowserWindow.showWorkshopObjects);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00078C87 File Offset: 0x00077087
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00078C8E File Offset: 0x0007708E
		[TerminalCommandProperty("object_browser.show_misc_objects", "include objects from other origins", true)]
		public static bool showMiscObjects
		{
			get
			{
				return ObjectBrowserWindow._showMiscObjects;
			}
			set
			{
				ObjectBrowserWindow._showMiscObjects = value;
				TerminalUtility.printCommandPass("Set show_misc_objects to: " + ObjectBrowserWindow.showMiscObjects);
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00078CB0 File Offset: 0x000770B0
		protected virtual void handleSearchFieldTyped(Sleek2Field field, string value)
		{
			if (this.searchLength == -1 || value.Length < this.searchLength)
			{
				this.searchResults.Clear();
				Assets.find<ObjectAsset>(this.searchResults);
			}
			this.searchLength = value.Length;
			this.itemsPanel.clearElements();
			this.itemsPanel.transform.offsetMin = new Vector2(0f, 0f);
			this.itemsPanel.transform.offsetMax = new Vector2(0f, 0f);
			if (value.Length > 0)
			{
				string[] array = value.Split(new char[]
				{
					' '
				});
				for (int i = this.searchResults.Count - 1; i >= 0; i--)
				{
					ObjectAsset objectAsset = this.searchResults[i];
					bool flag = true;
					switch (objectAsset.assetOrigin)
					{
					case EAssetOrigin.OFFICIAL:
						flag &= ObjectBrowserWindow.showOfficialObjects;
						break;
					case EAssetOrigin.CURATED:
						flag &= ObjectBrowserWindow.showCuratedObjects;
						break;
					case EAssetOrigin.WORKSHOP:
						flag &= ObjectBrowserWindow.showWorkshopObjects;
						break;
					case EAssetOrigin.MISC:
						flag &= ObjectBrowserWindow.showMiscObjects;
						break;
					}
					if (flag && !objectAsset.canUse)
					{
						flag = false;
					}
					if (flag)
					{
						foreach (string value2 in array)
						{
							if (objectAsset.objectName.IndexOf(value2, StringComparison.InvariantCultureIgnoreCase) == -1 && objectAsset.name.IndexOf(value2, StringComparison.InvariantCultureIgnoreCase) == -1)
							{
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						this.searchResults.RemoveAtFast(i);
					}
				}
				if (this.searchResults.Count <= 64)
				{
					this.searchResults.Sort(new ObjectBrowserWindow.ObjectBrowserAssetComparer());
					foreach (ObjectAsset newAsset in this.searchResults)
					{
						ObjectBrowserAssetButton objectBrowserAssetButton = new ObjectBrowserAssetButton(newAsset);
						objectBrowserAssetButton.clicked += this.handleAssetButtonClicked;
						this.itemsPanel.addElement(objectBrowserAssetButton);
					}
				}
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00078EFC File Offset: 0x000772FC
		protected void handleAssetButtonClicked(Sleek2ImageButton button)
		{
			ObjectAsset asset = (button as ObjectBrowserAssetButton).asset;
			if (asset == null)
			{
				return;
			}
			if (!asset.canUse)
			{
				return;
			}
			DevkitSelectionToolObjectInstantiationInfo devkitSelectionToolObjectInstantiationInfo = new DevkitSelectionToolObjectInstantiationInfo();
			devkitSelectionToolObjectInstantiationInfo.asset = asset;
			devkitSelectionToolObjectInstantiationInfo.rotation = Quaternion.Euler(-90f, 0f, 0f);
			DevkitSelectionToolOptions.instance.instantiationInfo = devkitSelectionToolObjectInstantiationInfo;
		}

		// Token: 0x04000ADA RID: 2778
		private static bool _showOfficialObjects = true;

		// Token: 0x04000ADB RID: 2779
		private static bool _showCuratedObjects = true;

		// Token: 0x04000ADC RID: 2780
		private static bool _showWorkshopObjects = true;

		// Token: 0x04000ADD RID: 2781
		private static bool _showMiscObjects = true;

		// Token: 0x04000ADE RID: 2782
		protected int searchLength;

		// Token: 0x04000ADF RID: 2783
		protected List<ObjectAsset> searchResults;

		// Token: 0x04000AE0 RID: 2784
		protected Sleek2Field searchField;

		// Token: 0x04000AE1 RID: 2785
		protected Sleek2Element itemsPanel;

		// Token: 0x04000AE2 RID: 2786
		protected Sleek2Scrollview itemsView;

		// Token: 0x02000284 RID: 644
		private class ObjectBrowserAssetComparer : IComparer<ObjectAsset>
		{
			// Token: 0x060012EE RID: 4846 RVA: 0x00078F7C File Offset: 0x0007737C
			public int Compare(ObjectAsset x, ObjectAsset y)
			{
				return x.objectName.CompareTo(y.objectName);
			}
		}
	}
}
