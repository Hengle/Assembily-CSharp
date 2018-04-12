using System;
using System.Collections.Generic;
using SDG.Framework.Debug;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.Foliage;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Framework.Utilities;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.FoliageUI
{
	// Token: 0x0200024B RID: 587
	public class FoliageToolWindow : Sleek2Window
	{
		// Token: 0x06001116 RID: 4374 RVA: 0x0006FF48 File Offset: 0x0006E348
		public FoliageToolWindow()
		{
			base.gameObject.name = "Foliage_Tool";
			this.searchLength = -1;
			this.searchInfoAssets = new List<FoliageInfoAsset>();
			this.searchCollectionAssets = new List<FoliageInfoCollectionAsset>();
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Foliage_Tool.Title"));
			base.tab.label.translation.format();
			this.bakeGlobalButton = new Sleek2ImageLabelButton();
			this.bakeGlobalButton.transform.anchorMin = new Vector2(0f, 1f);
			this.bakeGlobalButton.transform.anchorMax = new Vector2(1f, 1f);
			this.bakeGlobalButton.transform.pivot = new Vector2(0.5f, 1f);
			this.bakeGlobalButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.bakeGlobalButton.transform.offsetMax = new Vector2(0f, 0f);
			this.bakeGlobalButton.label.textComponent.text = "Bake Foliage [Global]";
			this.bakeGlobalButton.clicked += this.handleBakeGlobalButtonClicked;
			base.safePanel.addElement(this.bakeGlobalButton);
			this.bakeLocalButton = new Sleek2ImageLabelButton();
			this.bakeLocalButton.transform.anchorMin = new Vector2(0f, 1f);
			this.bakeLocalButton.transform.anchorMax = new Vector2(1f, 1f);
			this.bakeLocalButton.transform.pivot = new Vector2(0.5f, 1f);
			this.bakeLocalButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			this.bakeLocalButton.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight));
			this.bakeLocalButton.label.textComponent.text = "Bake Foliage [Local]";
			this.bakeLocalButton.clicked += this.handleBakeLocalButtonClicked;
			base.safePanel.addElement(this.bakeLocalButton);
			this.bakeProgressLabel = new Sleek2Label();
			this.bakeProgressLabel.transform.anchorMin = new Vector2(0f, 1f);
			this.bakeProgressLabel.transform.anchorMax = new Vector2(0.75f, 1f);
			this.bakeProgressLabel.transform.pivot = new Vector2(0.5f, 1f);
			this.bakeProgressLabel.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3));
			this.bakeProgressLabel.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			base.safePanel.addElement(this.bakeProgressLabel);
			this.bakeCancelButton = new Sleek2ImageLabelButton();
			this.bakeCancelButton.transform.anchorMin = new Vector2(0.75f, 1f);
			this.bakeCancelButton.transform.anchorMax = new Vector2(1f, 1f);
			this.bakeCancelButton.transform.pivot = new Vector2(0.5f, 1f);
			this.bakeCancelButton.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3));
			this.bakeCancelButton.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 2));
			this.bakeCancelButton.label.textComponent.text = "Cancel";
			this.bakeCancelButton.clicked += this.handleBakeCancelButtonClicked;
			base.safePanel.addElement(this.bakeCancelButton);
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 1f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3 - 210));
			this.inspector.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3 - 10));
			base.safePanel.addElement(this.inspector);
			this.inspector.inspect(DevkitFoliageToolOptions.instance);
			this.searchField = new Sleek2Field();
			this.searchField.transform.anchorMin = new Vector2(0f, 1f);
			this.searchField.transform.anchorMax = new Vector2(1f, 1f);
			this.searchField.transform.pivot = new Vector2(0.5f, 1f);
			this.searchField.transform.offsetMin = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 4 - 220));
			this.searchField.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 3 - 220));
			this.searchField.typed += this.handleSearchFieldTyped;
			base.safePanel.addElement(this.searchField);
			this.foliageView = new Sleek2Scrollview();
			this.foliageView.transform.anchorMin = new Vector2(0f, 0f);
			this.foliageView.transform.anchorMax = new Vector2(1f, 1f);
			this.foliageView.transform.pivot = new Vector2(0.5f, 1f);
			this.foliageView.transform.offsetMin = new Vector2(0f, 0f);
			this.foliageView.transform.offsetMax = new Vector2(0f, (float)(-(float)Sleek2Config.bodyHeight * 4 - 225));
			this.foliageView.vertical = true;
			this.foliagePanel = new Sleek2VerticalScrollviewContents();
			this.foliagePanel.name = "Panel";
			this.foliageView.panel = this.foliagePanel;
			base.safePanel.addElement(this.foliageView);
			TimeUtility.updated += this.handleUpdated;
			DevkitHotkeys.registerTool(3, this);
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00070619 File Offset: 0x0006EA19
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00070620 File Offset: 0x0006EA20
		[TerminalCommandProperty("foliage.tool.show_official_assets", "include assets from vanilla game", true)]
		public static bool showOfficialAssets
		{
			get
			{
				return FoliageToolWindow._showOfficialAssets;
			}
			set
			{
				FoliageToolWindow._showOfficialAssets = value;
				TerminalUtility.printCommandPass("Set show_official_assets to: " + FoliageToolWindow.showOfficialAssets);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x00070641 File Offset: 0x0006EA41
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x00070648 File Offset: 0x0006EA48
		[TerminalCommandProperty("foliage.tool.show_curated_assets", "include assets from curated maps", true)]
		public static bool showCuratedAssets
		{
			get
			{
				return FoliageToolWindow._showCuratedAssets;
			}
			set
			{
				FoliageToolWindow._showCuratedAssets = value;
				TerminalUtility.printCommandPass("Set show_curated_assets to: " + FoliageToolWindow.showCuratedAssets);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00070669 File Offset: 0x0006EA69
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x00070670 File Offset: 0x0006EA70
		[TerminalCommandProperty("foliage.tool.show_workshop_assets", "include assets from workshop downloads", true)]
		public static bool showWorkshopAssets
		{
			get
			{
				return FoliageToolWindow._showWorkshopAssets;
			}
			set
			{
				FoliageToolWindow._showWorkshopAssets = value;
				TerminalUtility.printCommandPass("Set show_workshop_assets to: " + FoliageToolWindow.showWorkshopAssets);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00070691 File Offset: 0x0006EA91
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00070698 File Offset: 0x0006EA98
		[TerminalCommandProperty("foliage.tool.show_misc_assets", "include assets from other origins", true)]
		public static bool showMiscAssets
		{
			get
			{
				return FoliageToolWindow._showMiscAssets;
			}
			set
			{
				FoliageToolWindow._showMiscAssets = value;
				TerminalUtility.printCommandPass("Set show_misc_assets to: " + FoliageToolWindow.showMiscAssets);
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000706BC File Offset: 0x0006EABC
		protected virtual FoliageBakeSettings getBakeSettings()
		{
			return new FoliageBakeSettings
			{
				bakeInstancesMeshes = DevkitFoliageToolOptions.instance.bakeInstancedMeshes,
				bakeResources = DevkitFoliageToolOptions.instance.bakeResources,
				bakeObjects = DevkitFoliageToolOptions.instance.bakeObjects,
				bakeClear = DevkitFoliageToolOptions.instance.bakeClear,
				bakeApplyScale = DevkitFoliageToolOptions.instance.bakeApplyScale
			};
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00070728 File Offset: 0x0006EB28
		protected virtual void handleBakeGlobalButtonClicked(Sleek2ImageButton button)
		{
			FoliageBakeSettings bakeSettings = this.getBakeSettings();
			FoliageSystem.bakeGlobal(bakeSettings);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00070744 File Offset: 0x0006EB44
		protected virtual void handleBakeLocalButtonClicked(Sleek2ImageButton button)
		{
			FoliageBakeSettings bakeSettings = this.getBakeSettings();
			FoliageSystem.bakeLocal(bakeSettings);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0007075E File Offset: 0x0006EB5E
		protected virtual void handleBakeCancelButtonClicked(Sleek2ImageButton button)
		{
			FoliageSystem.bakeCancel();
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00070768 File Offset: 0x0006EB68
		protected virtual void handleUpdated()
		{
			int bakeQueueProgress = FoliageSystem.bakeQueueProgress;
			int bakeQueueTotal = FoliageSystem.bakeQueueTotal;
			if (bakeQueueProgress == bakeQueueTotal)
			{
				this.bakeProgressLabel.textComponent.text = "-/- [--.-%]";
				return;
			}
			float num = (float)Mathf.RoundToInt((float)bakeQueueProgress / (float)bakeQueueTotal * 1000f) / 10f;
			this.bakeProgressLabel.textComponent.text = string.Concat(new object[]
			{
				bakeQueueProgress.ToString(),
				'/',
				bakeQueueTotal.ToString(),
				" [",
				num.ToString(),
				"%]"
			});
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0007081B File Offset: 0x0006EC1B
		protected virtual void handleFoliageAssetButtonClicked(Sleek2ImageButton button)
		{
			DevkitFoliageTool.selectedInstanceAsset = (button as FoliageToolFoliageAssetButton).asset;
			DevkitFoliageTool.selectedCollectionAsset = null;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00070833 File Offset: 0x0006EC33
		protected virtual void handleFoliageCollectionAssetButtonClicked(Sleek2ImageButton button)
		{
			DevkitFoliageTool.selectedInstanceAsset = null;
			DevkitFoliageTool.selectedCollectionAsset = (button as FoliageToolFoliageCollectionAssetButton).asset;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0007084C File Offset: 0x0006EC4C
		protected virtual void handleSearchFieldTyped(Sleek2Field field, string value)
		{
			if (this.searchLength == -1 || value.Length < this.searchLength)
			{
				this.searchInfoAssets.Clear();
				Assets.find<FoliageInfoAsset>(this.searchInfoAssets);
				this.searchCollectionAssets.Clear();
				Assets.find<FoliageInfoCollectionAsset>(this.searchCollectionAssets);
			}
			this.searchLength = value.Length;
			this.foliagePanel.clearElements();
			this.foliagePanel.transform.offsetMin = new Vector2(0f, 0f);
			this.foliagePanel.transform.offsetMax = new Vector2(0f, 0f);
			if (value.Length > 0)
			{
				string[] array = value.Split(new char[]
				{
					' '
				});
				for (int i = this.searchInfoAssets.Count - 1; i >= 0; i--)
				{
					FoliageInfoAsset foliageInfoAsset = this.searchInfoAssets[i];
					bool flag = true;
					switch (foliageInfoAsset.assetOrigin)
					{
					case EAssetOrigin.OFFICIAL:
						flag &= FoliageToolWindow.showOfficialAssets;
						break;
					case EAssetOrigin.CURATED:
						flag &= FoliageToolWindow.showCuratedAssets;
						break;
					case EAssetOrigin.WORKSHOP:
						flag &= FoliageToolWindow.showWorkshopAssets;
						break;
					case EAssetOrigin.MISC:
						flag &= FoliageToolWindow.showMiscAssets;
						break;
					}
					if (flag)
					{
						foreach (string value2 in array)
						{
							if (foliageInfoAsset.name.IndexOf(value2, StringComparison.InvariantCultureIgnoreCase) == -1)
							{
								flag = false;
								break;
							}
						}
					}
					if (!flag)
					{
						this.searchInfoAssets.RemoveAtFast(i);
					}
				}
				for (int k = this.searchCollectionAssets.Count - 1; k >= 0; k--)
				{
					FoliageInfoCollectionAsset foliageInfoCollectionAsset = this.searchCollectionAssets[k];
					bool flag2 = true;
					switch (foliageInfoCollectionAsset.assetOrigin)
					{
					case EAssetOrigin.OFFICIAL:
						flag2 &= FoliageToolWindow.showOfficialAssets;
						break;
					case EAssetOrigin.CURATED:
						flag2 &= FoliageToolWindow.showCuratedAssets;
						break;
					case EAssetOrigin.WORKSHOP:
						flag2 &= FoliageToolWindow.showWorkshopAssets;
						break;
					case EAssetOrigin.MISC:
						flag2 &= FoliageToolWindow.showMiscAssets;
						break;
					}
					if (flag2)
					{
						foreach (string value3 in array)
						{
							if (foliageInfoCollectionAsset.name.IndexOf(value3, StringComparison.InvariantCultureIgnoreCase) == -1)
							{
								flag2 = false;
								break;
							}
						}
					}
					if (!flag2)
					{
						this.searchCollectionAssets.RemoveAtFast(k);
					}
				}
				if (this.searchInfoAssets.Count + this.searchCollectionAssets.Count <= 64)
				{
					this.searchInfoAssets.Sort(new FoliageToolWindow.FoliageToolAssetComparer());
					this.searchCollectionAssets.Sort(new FoliageToolWindow.FoliageToolAssetComparer());
					foreach (FoliageInfoAsset newAsset in this.searchInfoAssets)
					{
						FoliageToolFoliageAssetButton foliageToolFoliageAssetButton = new FoliageToolFoliageAssetButton(newAsset);
						foliageToolFoliageAssetButton.clicked += this.handleFoliageAssetButtonClicked;
						this.foliagePanel.addElement(foliageToolFoliageAssetButton);
					}
					foreach (FoliageInfoCollectionAsset newAsset2 in this.searchCollectionAssets)
					{
						FoliageToolFoliageCollectionAssetButton foliageToolFoliageCollectionAssetButton = new FoliageToolFoliageCollectionAssetButton(newAsset2);
						foliageToolFoliageCollectionAssetButton.clicked += this.handleFoliageCollectionAssetButtonClicked;
						this.foliagePanel.addElement(foliageToolFoliageCollectionAssetButton);
					}
				}
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00070C04 File Offset: 0x0006F004
		protected override void triggerFocused()
		{
			if (DevkitEquipment.instance != null)
			{
				if (this.isActive)
				{
					DevkitEquipment.instance.equip(Activator.CreateInstance(typeof(DevkitFoliageTool)) as IDevkitTool);
				}
				else
				{
					DevkitEquipment.instance.dequip();
				}
			}
			base.triggerFocused();
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00070C5F File Offset: 0x0006F05F
		protected override void triggerDestroyed()
		{
			TimeUtility.updated -= this.handleUpdated;
			base.triggerDestroyed();
		}

		// Token: 0x04000A3B RID: 2619
		private static bool _showOfficialAssets = true;

		// Token: 0x04000A3C RID: 2620
		private static bool _showCuratedAssets = true;

		// Token: 0x04000A3D RID: 2621
		private static bool _showWorkshopAssets = true;

		// Token: 0x04000A3E RID: 2622
		private static bool _showMiscAssets = true;

		// Token: 0x04000A3F RID: 2623
		protected Sleek2ImageLabelButton bakeGlobalButton;

		// Token: 0x04000A40 RID: 2624
		protected Sleek2ImageLabelButton bakeLocalButton;

		// Token: 0x04000A41 RID: 2625
		protected Sleek2Label bakeProgressLabel;

		// Token: 0x04000A42 RID: 2626
		protected Sleek2ImageLabelButton bakeCancelButton;

		// Token: 0x04000A43 RID: 2627
		protected Sleek2Inspector inspector;

		// Token: 0x04000A44 RID: 2628
		protected int searchLength;

		// Token: 0x04000A45 RID: 2629
		protected List<FoliageInfoAsset> searchInfoAssets;

		// Token: 0x04000A46 RID: 2630
		protected List<FoliageInfoCollectionAsset> searchCollectionAssets;

		// Token: 0x04000A47 RID: 2631
		protected Sleek2Field searchField;

		// Token: 0x04000A48 RID: 2632
		protected Sleek2Element foliagePanel;

		// Token: 0x04000A49 RID: 2633
		protected Sleek2Scrollview foliageView;

		// Token: 0x0200024C RID: 588
		private class FoliageToolAssetComparer : IComparer<FoliageInfoAsset>, IComparer<FoliageInfoCollectionAsset>
		{
			// Token: 0x0600112B RID: 4395 RVA: 0x00070C9B File Offset: 0x0006F09B
			public int Compare(FoliageInfoAsset x, FoliageInfoAsset y)
			{
				return x.name.CompareTo(y.name);
			}

			// Token: 0x0600112C RID: 4396 RVA: 0x00070CAE File Offset: 0x0006F0AE
			public int Compare(FoliageInfoCollectionAsset x, FoliageInfoCollectionAsset y)
			{
				return x.name.CompareTo(y.name);
			}
		}
	}
}
