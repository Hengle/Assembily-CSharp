using System;
using SDG.Framework.Debug;
using SDG.Framework.Foliage;
using SDG.Framework.Landscapes;
using SDG.Framework.Translations;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.LandscapeUI
{
	// Token: 0x0200027A RID: 634
	public class GroundUpgradeWizardWindow : Sleek2Window
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x00076B1C File Offset: 0x00074F1C
		public GroundUpgradeWizardWindow()
		{
			base.gameObject.name = "Ground_Upgrade_Wizard";
			base.tab.label.translation = new TranslatedTextFallback("Ground Upgrade Wizard");
			base.tab.label.translation.format();
			this.inspector = new Sleek2Inspector();
			this.inspector.transform.anchorMin = new Vector2(0f, 1f);
			this.inspector.transform.anchorMax = new Vector2(1f, 1f);
			this.inspector.transform.pivot = new Vector2(0f, 1f);
			this.inspector.transform.offsetMin = new Vector2(0f, -320f);
			this.inspector.transform.offsetMax = new Vector2(0f, 0f);
			base.safePanel.addElement(this.inspector);
			this.upgradeButton = new Sleek2ImageLabelButton();
			this.upgradeButton.transform.anchorMin = new Vector2(0f, 1f);
			this.upgradeButton.transform.anchorMax = new Vector2(1f, 1f);
			this.upgradeButton.transform.pivot = new Vector2(0.5f, 1f);
			this.upgradeButton.transform.offsetMin = new Vector2(0f, -350f);
			this.upgradeButton.transform.offsetMax = new Vector2(0f, -330f);
			this.upgradeButton.label.textComponent.text = "Upgrade";
			this.upgradeButton.clicked += this.handleUpgradeButtonClicked;
			base.safePanel.addElement(this.upgradeButton);
			this.materials = new InspectableList<AssetReference<LandscapeMaterialAsset>>(Landscape.SPLATMAP_LAYERS);
			for (int i = 0; i < Landscape.SPLATMAP_LAYERS; i++)
			{
				this.materials.Add(AssetReference<LandscapeMaterialAsset>.invalid);
			}
			this.materials.canInspectorAdd = false;
			this.materials.canInspectorRemove = false;
			this.inspector.inspect(this);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00076D6C File Offset: 0x0007516C
		protected virtual void handleUpgradeButtonClicked(Sleek2ImageButton button)
		{
			int size = (int)Level.size;
			int num = size / Landscape.TILE_SIZE_INT;
			for (int i = -num; i < num; i++)
			{
				for (int j = -num; j < num; j++)
				{
					LandscapeCoord coord = new LandscapeCoord(i, j);
					LandscapeTile orAddTile = Landscape.getOrAddTile(coord);
					orAddTile.convertLegacyHeightmap();
					orAddTile.convertLegacySplatmap();
					for (int k = 0; k < Landscape.SPLATMAP_LAYERS; k++)
					{
						orAddTile.materials[k] = this.materials[k];
					}
					orAddTile.updatePrototypes();
				}
			}
			FoliageVolume foliageVolume = new GameObject
			{
				transform = 
				{
					position = Vector3.zero,
					rotation = Quaternion.identity,
					localScale = new Vector3((float)size, Landscape.TILE_HEIGHT, (float)size)
				}
			}.AddComponent<FoliageVolume>();
			foliageVolume.mode = FoliageVolume.EFoliageVolumeMode.ADDITIVE;
			foliageVolume.devkitHierarchySpawn();
		}

		// Token: 0x04000AB0 RID: 2736
		[Inspectable("#SDG::Tile.Materials", null)]
		public InspectableList<AssetReference<LandscapeMaterialAsset>> materials;

		// Token: 0x04000AB1 RID: 2737
		protected Sleek2Inspector inspector;

		// Token: 0x04000AB2 RID: 2738
		protected Sleek2ImageLabelButton upgradeButton;
	}
}
