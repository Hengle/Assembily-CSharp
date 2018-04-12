using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Devkit.AssetBrowserUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200025D RID: 605
	public class Sleek2AssetReferenceInspector<T> : Sleek2KeyValueInspector where T : Asset
	{
		// Token: 0x060011C3 RID: 4547 RVA: 0x00073724 File Offset: 0x00071B24
		public Sleek2AssetReferenceInspector()
		{
			base.name = "Asset_Reference_Inspector";
			this.referenceButton = new Sleek2ImageLabelButton();
			this.referenceButton.transform.anchorMin = new Vector2(0f, 0f);
			this.referenceButton.transform.anchorMax = new Vector2(1f, 1f);
			this.referenceButton.transform.offsetMin = new Vector2(0f, 0f);
			this.referenceButton.transform.offsetMax = new Vector2(-100f, 0f);
			base.valuePanel.addElement(this.referenceButton);
			DragableDestination dragableDestination = this.referenceButton.gameObject.AddComponent<DragableDestination>();
			this.destination = new DragableAssetDestination<T>();
			this.destination.assetReferenceDocked += this.handleAssetReferenceDocked;
			dragableDestination.dropHandler = this.destination;
			this.browseButton = new Sleek2ImageLabelButton();
			this.browseButton.transform.anchorMin = new Vector2(1f, 0f);
			this.browseButton.transform.anchorMax = new Vector2(1f, 1f);
			this.browseButton.transform.offsetMin = new Vector2(-100f, 0f);
			this.browseButton.transform.offsetMax = new Vector2(-50f, 0f);
			this.browseButton.label.textComponent.text = "->";
			this.browseButton.clicked += this.handleBrowseButtonClicked;
			base.valuePanel.addElement(this.browseButton);
			this.clearButton = new Sleek2ImageLabelButton();
			this.clearButton.transform.anchorMin = new Vector2(1f, 0f);
			this.clearButton.transform.anchorMax = new Vector2(1f, 1f);
			this.clearButton.transform.offsetMin = new Vector2(-50f, 0f);
			this.clearButton.transform.offsetMax = new Vector2(0f, 0f);
			this.clearButton.label.textComponent.text = "x";
			this.clearButton.clicked += this.handleClearButtonClicked;
			base.valuePanel.addElement(this.clearButton);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000739AE File Offset: 0x00071DAE
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x000739B6 File Offset: 0x00071DB6
		public Sleek2ImageLabelButton referenceButton { get; protected set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000739BF File Offset: 0x00071DBF
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x000739C7 File Offset: 0x00071DC7
		public Sleek2ImageLabelButton browseButton { get; protected set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000739D0 File Offset: 0x00071DD0
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000739D8 File Offset: 0x00071DD8
		public Sleek2ImageLabelButton clearButton { get; protected set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000739E1 File Offset: 0x00071DE1
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x000739E9 File Offset: 0x00071DE9
		public DragableAssetDestination<T> destination { get; protected set; }

		// Token: 0x060011CC RID: 4556 RVA: 0x000739F2 File Offset: 0x00071DF2
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00073A08 File Offset: 0x00071E08
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			AssetReference<T> reference = (AssetReference<T>)base.inspectable.value;
			Asset asset = Assets.find<T>(reference);
			if (asset != null)
			{
				this.referenceButton.label.textComponent.text = asset.name;
			}
			else if (reference.isValid)
			{
				this.referenceButton.label.textComponent.text = reference.ToString();
			}
			else
			{
				this.referenceButton.label.textComponent.text = "nullptr";
			}
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00073AC1 File Offset: 0x00071EC1
		protected virtual void handleAssetReferenceDocked(AssetReference<T> assetReference)
		{
			base.inspectable.value = assetReference;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00073AD4 File Offset: 0x00071ED4
		protected virtual void handleBrowseButtonClicked(Sleek2ImageButton button)
		{
			AssetReference<T> reference = (AssetReference<T>)base.inspectable.value;
			Asset asset = Assets.find<T>(reference);
			if (asset != null)
			{
				AssetBrowserWindow.browse(asset.directory);
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00073B0F File Offset: 0x00071F0F
		protected virtual void handleClearButtonClicked(Sleek2ImageButton button)
		{
			base.inspectable.value = AssetReference<T>.invalid;
		}
	}
}
