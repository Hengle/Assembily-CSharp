using System;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.Devkit.Visibility;
using SDG.Framework.UI.Devkit;
using SDG.Framework.UI.Devkit.AssetBrowserUI;
using SDG.Framework.UI.Devkit.ContentBrowserUI;
using SDG.Framework.UI.Devkit.FoliageUI;
using SDG.Framework.UI.Devkit.HierarchyUI;
using SDG.Framework.UI.Devkit.InspectorUI;
using SDG.Framework.UI.Devkit.LandscapeUI;
using SDG.Framework.UI.Devkit.LayoutUI;
using SDG.Framework.UI.Devkit.LoadUI;
using SDG.Framework.UI.Devkit.ObjectBrowserUI;
using SDG.Framework.UI.Devkit.SaveUI;
using SDG.Framework.UI.Devkit.SelectionUI;
using SDG.Framework.UI.Devkit.TerminalUI;
using SDG.Framework.UI.Devkit.TransactionUI;
using SDG.Framework.UI.Devkit.TranslationUI;
using SDG.Framework.UI.Devkit.ViewportUI;
using SDG.Framework.UI.Devkit.VisibilityUI;
using SDG.Framework.UI.Devkit.WorkshopUI;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.UI
{
	// Token: 0x02000236 RID: 566
	public class DevkitCanvas : MonoBehaviour
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0006D6B4 File Offset: 0x0006BAB4
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0006D6BB File Offset: 0x0006BABB
		public static Canvas instance
		{
			get
			{
				return DevkitCanvas._instance;
			}
			protected set
			{
				if (DevkitCanvas.instance != value)
				{
					DevkitCanvas._instance = value;
					DevkitCanvas.triggerInstanceChanged();
				}
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0006D6D8 File Offset: 0x0006BAD8
		public static float scaleFactor
		{
			get
			{
				return DevkitCanvas.instance.transform.localScale.x;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0006D6FC File Offset: 0x0006BAFC
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x0006D704 File Offset: 0x0006BB04
		public static Sleek2Element tooltip
		{
			get
			{
				return DevkitCanvas._tooltip;
			}
			set
			{
				DevkitCanvas._tooltip = value;
				if (DevkitCanvas.tooltip != null)
				{
					DevkitCanvas.tooltip.transform.SetParent(DevkitCanvas.instance.transform);
					Canvas orAddComponent = DevkitCanvas.tooltip.gameObject.getOrAddComponent<Canvas>();
					orAddComponent.overrideSorting = true;
					orAddComponent.sortingOrder = 30000;
				}
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060010AA RID: 4266 RVA: 0x0006D75C File Offset: 0x0006BB5C
		// (remove) Token: 0x060010AB RID: 4267 RVA: 0x0006D790 File Offset: 0x0006BB90
		public static event DevkitCanvasInstanceChangedHandler instanceChanged;

		// Token: 0x060010AC RID: 4268 RVA: 0x0006D7C4 File Offset: 0x0006BBC4
		protected static void triggerInstanceChanged()
		{
			if (DevkitCanvas.instanceChanged != null)
			{
				DevkitCanvas.instanceChanged();
			}
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0006D7DC File Offset: 0x0006BBDC
		protected void handleToolbarCreated()
		{
			DevkitToolbarManager.registerToolbarElement("File", new DevkitToolbarContainerButton(typeof(DevkitLoadContainer)));
			DevkitToolbarManager.registerToolbarElement("File", new DevkitToolbarContainerButton(typeof(DevkitSaveContainer)));
			DevkitToolbarManager.registerToolbarElement("File", new DevkitToolbarExitButton());
			DevkitToolbarManager.registerToolbarElement("Edit", new DevkitToolbarUndoButton());
			DevkitToolbarManager.registerToolbarElement("Edit", new DevkitToolbarRedoButton());
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(InspectorWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(AssetBrowserWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(ContentBrowserWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(TypeBrowserWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(TerminalWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(ViewportWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(LayoutWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Tools", new DevkitToolbarWindowButton(typeof(LandscapeToolWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Tools", new DevkitToolbarWindowButton(typeof(SelectionToolWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Tools", new DevkitToolbarWindowButton(typeof(FoliageToolWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(TransactionWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(HierarchyWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Wizards", new DevkitToolbarWindowButton(typeof(GroundUpgradeWizardWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(VisbilityWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(TranslationWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Wizards", new DevkitToolbarWindowButton(typeof(UGCUploadWizardWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Wizards", new DevkitToolbarWindowButton(typeof(SkinCreatorWizardWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Wizards", new DevkitToolbarWindowButton(typeof(SkinAcceptorWizardWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Wizards", new DevkitToolbarWindowButton(typeof(ObjectsUpgradeWizardWindow)));
			DevkitToolbarManager.registerToolbarElement("Windows/Panels", new DevkitToolbarWindowButton(typeof(ObjectBrowserWindow)));
			DevkitToolbarManager.registerToolbarElement("Help/SubMenu", new DevkitToolbarHelpButton("Guides", "http://steamcommunity.com/sharedfiles/filedetails/?id=460136012"));
			DevkitToolbarManager.registerToolbarElement("Help/SubMenu", new DevkitToolbarHelpButton("Forums", "http://steamcommunity.com/app/304930/discussions/"));
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0006DA6E File Offset: 0x0006BE6E
		private void Update()
		{
			if (DevkitCanvas.tooltip != null)
			{
				DevkitCanvas.tooltip.transform.position = Input.mousePosition;
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0006DA8E File Offset: 0x0006BE8E
		private void OnEnable()
		{
			if (DevkitCanvas.instance != null)
			{
				return;
			}
			DevkitCanvas.instance = base.GetComponent<Canvas>();
			DevkitWindowManager.toolbarCreated += this.handleToolbarCreated;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0006DAC8 File Offset: 0x0006BEC8
		private void Start()
		{
			if (Dedicator.isDedicated)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0006DAF0 File Offset: 0x0006BEF0
		private void OnApplicationQuit()
		{
			DevkitWindowLayout.save("Default");
			VisibilityManager.save();
			DevkitSelectionToolOptions.save();
			DevkitLandscapeToolHeightmapOptions.save();
			DevkitLandscapeToolSplatmapOptions.save();
			DevkitFoliageToolOptions.save();
		}

		// Token: 0x04000A0E RID: 2574
		protected static Canvas _instance;

		// Token: 0x04000A0F RID: 2575
		private static Sleek2Element _tooltip;
	}
}
