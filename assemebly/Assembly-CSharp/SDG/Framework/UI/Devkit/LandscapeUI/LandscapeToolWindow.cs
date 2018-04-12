using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.Landscapes;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LandscapeUI
{
	// Token: 0x0200027D RID: 637
	public class LandscapeToolWindow : Sleek2Window
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x00077054 File Offset: 0x00075454
		public LandscapeToolWindow()
		{
			base.gameObject.name = "Landscape_Tool";
			this.searchLength = -1;
			this.searchResults = new List<LandscapeMaterialAsset>();
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Landscape_Tool.Title"));
			base.tab.label.translation.format();
			this.heightmapModeButton = new Sleek2ImageTranslatedLabelButton();
			this.heightmapModeButton.transform.anchorMin = new Vector2(0f, 1f);
			this.heightmapModeButton.transform.anchorMax = new Vector2(0.33f, 1f);
			this.heightmapModeButton.transform.pivot = new Vector2(0.5f, 1f);
			this.heightmapModeButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.heightmapModeButton.transform.offsetMax = new Vector2(-5f, 0f);
			this.heightmapModeButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.Landscape_Tool.Heightmap_Mode_Button.Label"));
			this.heightmapModeButton.label.translation.format();
			this.heightmapModeButton.clicked += this.handleHeightmapModeButtonClicked;
			base.safePanel.addElement(this.heightmapModeButton);
			this.splatmapModeButton = new Sleek2ImageTranslatedLabelButton();
			this.splatmapModeButton.transform.anchorMin = new Vector2(0.33f, 1f);
			this.splatmapModeButton.transform.anchorMax = new Vector2(0.667f, 1f);
			this.splatmapModeButton.transform.pivot = new Vector2(0.5f, 1f);
			this.splatmapModeButton.transform.offsetMin = new Vector2(5f, (float)(-(float)Sleek2Config.bodyHeight));
			this.splatmapModeButton.transform.offsetMax = new Vector2(-10f, 0f);
			this.splatmapModeButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.Landscape_Tool.Splatmap_Mode_Button.Label"));
			this.splatmapModeButton.label.translation.format();
			this.splatmapModeButton.clicked += this.handleSplatmapModeButtonClicked;
			base.safePanel.addElement(this.splatmapModeButton);
			this.tileModeButton = new Sleek2ImageTranslatedLabelButton();
			this.tileModeButton.transform.anchorMin = new Vector2(0.667f, 1f);
			this.tileModeButton.transform.anchorMax = new Vector2(1f, 1f);
			this.tileModeButton.transform.pivot = new Vector2(0.5f, 1f);
			this.tileModeButton.transform.offsetMin = new Vector2(5f, (float)(-(float)Sleek2Config.bodyHeight));
			this.tileModeButton.transform.offsetMax = new Vector2(-5f, 0f);
			this.tileModeButton.label.translation = new TranslatedText(new TranslationReference("#SDG::Devkit.Window.Landscape_Tool.Tile_Mode_Button.Label"));
			this.tileModeButton.label.translation.format();
			this.tileModeButton.clicked += this.handleTileModeButtonClicked;
			base.safePanel.addElement(this.tileModeButton);
			this.modePanel = new Sleek2Element();
			this.modePanel.transform.anchorMin = new Vector2(0f, 0f);
			this.modePanel.transform.anchorMax = new Vector2(1f, 1f);
			this.modePanel.transform.pivot = new Vector2(0f, 1f);
			this.modePanel.transform.offsetMin = new Vector2(0f, 0f);
			this.modePanel.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 5));
			base.safePanel.addElement(this.modePanel);
			this.heightmapModePanel = new Sleek2Element();
			this.heightmapModePanel.transform.reset();
			this.modePanel.addElement(this.heightmapModePanel);
			this.splatmapModePanel = new Sleek2Element();
			this.splatmapModePanel.transform.reset();
			this.modePanel.addElement(this.splatmapModePanel);
			this.tileModePanel = new Sleek2Element();
			this.tileModePanel.transform.reset();
			this.modePanel.addElement(this.tileModePanel);
			this.updateMode();
			this.heightmapInspector = new Sleek2Inspector();
			this.heightmapInspector.transform.anchorMin = new Vector2(0f, 0f);
			this.heightmapInspector.transform.anchorMax = new Vector2(1f, 1f);
			this.heightmapInspector.transform.pivot = new Vector2(0f, 1f);
			this.heightmapInspector.transform.offsetMin = new Vector2(0f, 0f);
			this.heightmapInspector.transform.offsetMax = new Vector2(0f, 0f);
			this.heightmapModePanel.addElement(this.heightmapInspector);
			this.heightmapInspector.inspect(DevkitLandscapeToolHeightmapOptions.instance);
			this.splatmapInspector = new Sleek2Inspector();
			this.splatmapInspector.transform.anchorMin = new Vector2(0f, 1f);
			this.splatmapInspector.transform.anchorMax = new Vector2(1f, 1f);
			this.splatmapInspector.transform.pivot = new Vector2(0f, 1f);
			this.splatmapInspector.transform.offsetMin = new Vector2(0f, -235f);
			this.splatmapInspector.transform.offsetMax = new Vector2(0f, 0f);
			this.splatmapModePanel.addElement(this.splatmapInspector);
			this.splatmapInspector.inspect(DevkitLandscapeToolSplatmapOptions.instance);
			this.splatmapSearchField = new Sleek2Field();
			this.splatmapSearchField.transform.anchorMin = new Vector2(0f, 1f);
			this.splatmapSearchField.transform.anchorMax = new Vector2(1f, 1f);
			this.splatmapSearchField.transform.pivot = new Vector2(0.5f, 1f);
			this.splatmapSearchField.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 245));
			this.splatmapSearchField.transform.offsetMax = new Vector2(0f, -245f);
			this.splatmapSearchField.typed += this.handleSplatmapSearchFieldTyped;
			this.splatmapModePanel.addElement(this.splatmapSearchField);
			this.splatmapMaterialsView = new Sleek2Scrollview();
			this.splatmapMaterialsView.transform.anchorMin = new Vector2(0f, 0f);
			this.splatmapMaterialsView.transform.anchorMax = new Vector2(1f, 1f);
			this.splatmapMaterialsView.transform.pivot = new Vector2(0.5f, 1f);
			this.splatmapMaterialsView.transform.offsetMin = new Vector2(0f, 0f);
			this.splatmapMaterialsView.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 250));
			this.splatmapMaterialsView.vertical = true;
			this.splatmapMaterialsPanel = new Sleek2VerticalScrollviewContents();
			this.splatmapMaterialsPanel.name = "Panel";
			this.splatmapMaterialsView.panel = this.splatmapMaterialsPanel;
			this.splatmapModePanel.addElement(this.splatmapMaterialsView);
			this.tileInspector = new Sleek2Inspector();
			this.tileInspector.transform.anchorMin = new Vector2(0f, 1f);
			this.tileInspector.transform.anchorMax = new Vector2(1f, 1f);
			this.tileInspector.transform.pivot = new Vector2(0f, 1f);
			this.tileInspector.transform.offsetMin = new Vector2(0f, -320f);
			this.tileInspector.transform.offsetMax = new Vector2(0f, 0f);
			this.tileModePanel.addElement(this.tileInspector);
			this.tileResetHeightmapButton = new Sleek2ImageLabelButton();
			this.tileResetHeightmapButton.transform.anchorMin = new Vector2(0f, 1f);
			this.tileResetHeightmapButton.transform.anchorMax = new Vector2(1f, 1f);
			this.tileResetHeightmapButton.transform.pivot = new Vector2(0.5f, 1f);
			this.tileResetHeightmapButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 330));
			this.tileResetHeightmapButton.transform.offsetMax = new Vector2(0f, -330f);
			this.tileResetHeightmapButton.label.textComponent.text = "Reset Heightmap";
			this.tileResetHeightmapButton.clicked += this.handleTileResetHeightmapButtonClicked;
			this.tileModePanel.addElement(this.tileResetHeightmapButton);
			this.tileResetSplatmapButton = new Sleek2ImageLabelButton();
			this.tileResetSplatmapButton.transform.anchorMin = new Vector2(0f, 1f);
			this.tileResetSplatmapButton.transform.anchorMax = new Vector2(1f, 1f);
			this.tileResetSplatmapButton.transform.pivot = new Vector2(0.5f, 1f);
			this.tileResetSplatmapButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2 - 330));
			this.tileResetSplatmapButton.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight - 330));
			this.tileResetSplatmapButton.label.textComponent.text = "Reset Splatmap";
			this.tileResetSplatmapButton.clicked += this.handleTileResetSplatmapButtonClicked;
			this.tileModePanel.addElement(this.tileResetSplatmapButton);
			this.tileNormalizeSplatmapButton = new Sleek2ImageLabelButton();
			this.tileNormalizeSplatmapButton.transform.anchorMin = new Vector2(0f, 1f);
			this.tileNormalizeSplatmapButton.transform.anchorMax = new Vector2(1f, 1f);
			this.tileNormalizeSplatmapButton.transform.pivot = new Vector2(0.5f, 1f);
			this.tileNormalizeSplatmapButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3 - 330));
			this.tileNormalizeSplatmapButton.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2 - 330));
			this.tileNormalizeSplatmapButton.label.textComponent.text = "Normalize Splatmap";
			this.tileNormalizeSplatmapButton.clicked += this.handleTileNormalizeSplatmapButtonClicked;
			this.tileModePanel.addElement(this.tileNormalizeSplatmapButton);
			DevkitLandscapeTool.toolModeChanged += this.handleToolModeChanged;
			DevkitLandscapeTool.selectedTileChanged += this.handleSelectedTileChanged;
			DevkitHotkeys.registerTool(2, this);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00077C41 File Offset: 0x00076041
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00077C48 File Offset: 0x00076048
		[TerminalCommandProperty("landscape.tool.show_official_assets", "include assets from vanilla game", true)]
		public static bool showOfficialAssets
		{
			get
			{
				return LandscapeToolWindow._showOfficialAssets;
			}
			set
			{
				LandscapeToolWindow._showOfficialAssets = value;
				TerminalUtility.printCommandPass("Set show_official_assets to: " + LandscapeToolWindow.showOfficialAssets);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00077C69 File Offset: 0x00076069
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00077C70 File Offset: 0x00076070
		[TerminalCommandProperty("landscape.tool.show_curated_assets", "include assets from curated maps", true)]
		public static bool showCuratedAssets
		{
			get
			{
				return LandscapeToolWindow._showCuratedAssets;
			}
			set
			{
				LandscapeToolWindow._showCuratedAssets = value;
				TerminalUtility.printCommandPass("Set show_curated_assets to: " + LandscapeToolWindow.showCuratedAssets);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00077C91 File Offset: 0x00076091
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00077C98 File Offset: 0x00076098
		[TerminalCommandProperty("landscape.tool.show_workshop_assets", "include assets from workshop downloads", true)]
		public static bool showWorkshopAssets
		{
			get
			{
				return LandscapeToolWindow._showWorkshopAssets;
			}
			set
			{
				LandscapeToolWindow._showWorkshopAssets = value;
				TerminalUtility.printCommandPass("Set show_workshop_assets to: " + LandscapeToolWindow.showWorkshopAssets);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00077CB9 File Offset: 0x000760B9
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00077CC0 File Offset: 0x000760C0
		[TerminalCommandProperty("landscape.tool.show_misc_assets", "include assets from other origins", true)]
		public static bool showMiscAssets
		{
			get
			{
				return LandscapeToolWindow._showMiscAssets;
			}
			set
			{
				LandscapeToolWindow._showMiscAssets = value;
				TerminalUtility.printCommandPass("Set show_misc_assets to: " + LandscapeToolWindow.showMiscAssets);
			}
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00077CE4 File Offset: 0x000760E4
		protected virtual void updateMode()
		{
			this.heightmapModePanel.isVisible = (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP);
			this.splatmapModePanel.isVisible = (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.SPLATMAP);
			this.tileModePanel.isVisible = (DevkitLandscapeTool.toolMode == DevkitLandscapeTool.EDevkitLandscapeToolMode.TILE && DevkitLandscapeTool.selectedTile != null);
			if (this.tileModePanel.isVisible)
			{
				this.refreshTile();
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00077D51 File Offset: 0x00076151
		protected virtual void refreshTile()
		{
			this.tileInspector.inspect(DevkitLandscapeTool.selectedTile);
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00077D63 File Offset: 0x00076163
		protected virtual void handleToolModeChanged(DevkitLandscapeTool.EDevkitLandscapeToolMode oldMode, DevkitLandscapeTool.EDevkitLandscapeToolMode newMode)
		{
			this.updateMode();
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00077D6B File Offset: 0x0007616B
		protected virtual void handleSelectedTileChanged(LandscapeTile oldSelectedTile, LandscapeTile newSelectedTile)
		{
			this.updateMode();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00077D73 File Offset: 0x00076173
		protected virtual void handleHeightmapModeButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.HEIGHTMAP;
			this.updateMode();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00077D81 File Offset: 0x00076181
		protected virtual void handleSplatmapModeButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.SPLATMAP;
			this.updateMode();
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00077D8F File Offset: 0x0007618F
		protected virtual void handleTileModeButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.toolMode = DevkitLandscapeTool.EDevkitLandscapeToolMode.TILE;
			this.updateMode();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00077D9D File Offset: 0x0007619D
		protected virtual void handleHeightmapAdjustButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.ADJUST;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00077DA5 File Offset: 0x000761A5
		protected virtual void handleHeightmapFlattenButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.FLATTEN;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00077DAD File Offset: 0x000761AD
		protected virtual void handleHeightmapSmoothButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.SMOOTH;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00077DB5 File Offset: 0x000761B5
		protected virtual void handleHeightmapRampButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.heightmapMode = DevkitLandscapeTool.EDevkitLandscapeToolHeightmapMode.RAMP;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00077DBD File Offset: 0x000761BD
		protected virtual void handleMaterialAssetButtonClicked(Sleek2ImageButton button)
		{
			DevkitLandscapeTool.splatmapMaterialTarget = (button as LandscapeToolMaterialAssetButton).asset.getReferenceTo<LandscapeMaterialAsset>();
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00077DD4 File Offset: 0x000761D4
		protected virtual void handleSplatmapSearchFieldTyped(Sleek2Field field, string value)
		{
			if (this.searchLength == -1 || value.Length < this.searchLength)
			{
				this.searchResults.Clear();
				Assets.find<LandscapeMaterialAsset>(this.searchResults);
			}
			this.searchLength = value.Length;
			this.splatmapMaterialsPanel.clearElements();
			this.splatmapMaterialsPanel.transform.offsetMin = new Vector2(0f, 0f);
			this.splatmapMaterialsPanel.transform.offsetMax = new Vector2(0f, 0f);
			if (value.Length > 0)
			{
				string[] array = value.Split(new char[]
				{
					' '
				});
				for (int i = this.searchResults.Count - 1; i >= 0; i--)
				{
					LandscapeMaterialAsset landscapeMaterialAsset = this.searchResults[i];
					bool flag = true;
					switch (landscapeMaterialAsset.assetOrigin)
					{
					case EAssetOrigin.OFFICIAL:
						flag &= LandscapeToolWindow.showOfficialAssets;
						break;
					case EAssetOrigin.CURATED:
						flag &= LandscapeToolWindow.showCuratedAssets;
						break;
					case EAssetOrigin.WORKSHOP:
						flag &= LandscapeToolWindow.showWorkshopAssets;
						break;
					case EAssetOrigin.MISC:
						flag &= LandscapeToolWindow.showMiscAssets;
						break;
					}
					if (flag)
					{
						foreach (string value2 in array)
						{
							if (landscapeMaterialAsset.name.IndexOf(value2, StringComparison.InvariantCultureIgnoreCase) == -1)
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
					this.searchResults.Sort(new LandscapeToolWindow.LandscapeToolAssetComparer());
					foreach (LandscapeMaterialAsset newAsset in this.searchResults)
					{
						LandscapeToolMaterialAssetButton landscapeToolMaterialAssetButton = new LandscapeToolMaterialAssetButton(newAsset);
						landscapeToolMaterialAssetButton.clicked += this.handleMaterialAssetButtonClicked;
						this.splatmapMaterialsPanel.addElement(landscapeToolMaterialAssetButton);
					}
				}
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00077FF8 File Offset: 0x000763F8
		protected virtual void handleTileResetHeightmapButtonClicked(Sleek2ImageButton button)
		{
			if (DevkitLandscapeTool.selectedTile == null)
			{
				return;
			}
			DevkitLandscapeTool.selectedTile.resetHeightmap();
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0007800F File Offset: 0x0007640F
		protected virtual void handleTileResetSplatmapButtonClicked(Sleek2ImageButton button)
		{
			if (DevkitLandscapeTool.selectedTile == null)
			{
				return;
			}
			DevkitLandscapeTool.selectedTile.resetSplatmap();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00078026 File Offset: 0x00076426
		protected virtual void handleTileNormalizeSplatmapButtonClicked(Sleek2ImageButton button)
		{
			if (DevkitLandscapeTool.selectedTile == null)
			{
				return;
			}
			DevkitLandscapeTool.selectedTile.normalizeSplatmap();
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00078040 File Offset: 0x00076440
		protected override void triggerFocused()
		{
			if (DevkitEquipment.instance != null)
			{
				if (this.isActive)
				{
					DevkitEquipment.instance.equip(Activator.CreateInstance(typeof(DevkitLandscapeTool)) as IDevkitTool);
				}
				else
				{
					DevkitEquipment.instance.dequip();
				}
			}
			base.triggerFocused();
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0007809B File Offset: 0x0007649B
		protected override void triggerDestroyed()
		{
			DevkitLandscapeTool.toolModeChanged -= this.handleToolModeChanged;
			base.triggerDestroyed();
		}

		// Token: 0x04000AB6 RID: 2742
		private static bool _showOfficialAssets = true;

		// Token: 0x04000AB7 RID: 2743
		private static bool _showCuratedAssets = true;

		// Token: 0x04000AB8 RID: 2744
		private static bool _showWorkshopAssets = true;

		// Token: 0x04000AB9 RID: 2745
		private static bool _showMiscAssets = true;

		// Token: 0x04000ABA RID: 2746
		protected Sleek2ImageTranslatedLabelButton heightmapModeButton;

		// Token: 0x04000ABB RID: 2747
		protected Sleek2ImageTranslatedLabelButton splatmapModeButton;

		// Token: 0x04000ABC RID: 2748
		protected Sleek2ImageTranslatedLabelButton tileModeButton;

		// Token: 0x04000ABD RID: 2749
		protected Sleek2Element modePanel;

		// Token: 0x04000ABE RID: 2750
		protected Sleek2Element heightmapModePanel;

		// Token: 0x04000ABF RID: 2751
		protected Sleek2Element splatmapModePanel;

		// Token: 0x04000AC0 RID: 2752
		protected Sleek2Element tileModePanel;

		// Token: 0x04000AC1 RID: 2753
		protected Sleek2ImageTranslatedLabelButton heightmapAdjustButton;

		// Token: 0x04000AC2 RID: 2754
		protected Sleek2ImageTranslatedLabelButton heightmapFlattenButton;

		// Token: 0x04000AC3 RID: 2755
		protected Sleek2ImageTranslatedLabelButton heightmapSmoothButton;

		// Token: 0x04000AC4 RID: 2756
		protected Sleek2ImageTranslatedLabelButton heightmapRampButton;

		// Token: 0x04000AC5 RID: 2757
		protected Sleek2Inspector heightmapInspector;

		// Token: 0x04000AC6 RID: 2758
		protected Sleek2Inspector splatmapInspector;

		// Token: 0x04000AC7 RID: 2759
		protected Sleek2Field splatmapSearchField;

		// Token: 0x04000AC8 RID: 2760
		protected Sleek2Element splatmapMaterialsPanel;

		// Token: 0x04000AC9 RID: 2761
		protected Sleek2Scrollview splatmapMaterialsView;

		// Token: 0x04000ACA RID: 2762
		protected Sleek2Inspector tileInspector;

		// Token: 0x04000ACB RID: 2763
		protected Sleek2ImageLabelButton tileResetHeightmapButton;

		// Token: 0x04000ACC RID: 2764
		protected Sleek2ImageLabelButton tileResetSplatmapButton;

		// Token: 0x04000ACD RID: 2765
		protected Sleek2ImageLabelButton tileNormalizeSplatmapButton;

		// Token: 0x04000ACE RID: 2766
		protected int searchLength;

		// Token: 0x04000ACF RID: 2767
		protected List<LandscapeMaterialAsset> searchResults;

		// Token: 0x0200027E RID: 638
		private class LandscapeToolAssetComparer : IComparer<LandscapeMaterialAsset>
		{
			// Token: 0x060012CA RID: 4810 RVA: 0x000780D7 File Offset: 0x000764D7
			public int Compare(LandscapeMaterialAsset x, LandscapeMaterialAsset y)
			{
				return x.name.CompareTo(y.name);
			}
		}
	}
}
