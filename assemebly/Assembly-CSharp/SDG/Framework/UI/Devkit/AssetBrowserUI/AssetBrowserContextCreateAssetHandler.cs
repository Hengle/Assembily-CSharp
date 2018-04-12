using System;
using SDG.Framework.Devkit;
using SDG.Framework.Modules;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x0200022A RID: 554
	public class AssetBrowserContextCreateAssetHandler
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0006BEC4 File Offset: 0x0006A2C4
		protected static void createButtons(Sleek2HoverDropdown dropdown, Type[] types)
		{
			foreach (Type type in types)
			{
				if (!type.IsAbstract && typeof(Asset).IsAssignableFrom(type) && typeof(IDevkitAssetSpawnable).IsAssignableFrom(type))
				{
					AssetBrowserContextCreateAssetHandler.CreateAssetButton element = new AssetBrowserContextCreateAssetHandler.CreateAssetButton(type);
					dropdown.addElement(element);
				}
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0006BF30 File Offset: 0x0006A330
		public static void handleContextDropdownOpened(Sleek2HoverDropdown dropdown, AssetDirectory directory)
		{
			AssetBrowserContextCreateAssetHandler.directory = directory;
			AssetBrowserContextCreateAssetHandler.createButtons(dropdown, ModuleHook.coreTypes);
			foreach (Module module in ModuleHook.modules)
			{
				AssetBrowserContextCreateAssetHandler.createButtons(dropdown, module.types);
			}
		}

		// Token: 0x040009EF RID: 2543
		protected static AssetDirectory directory;

		// Token: 0x0200022B RID: 555
		protected class CreateAssetButton : Sleek2DropdownButtonTemplate
		{
			// Token: 0x0600106B RID: 4203 RVA: 0x0006C01F File Offset: 0x0006A41F
			public CreateAssetButton(Type newType)
			{
				this.type = newType;
				base.label.textComponent.text = this.type.Name;
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x0600106C RID: 4204 RVA: 0x0006C049 File Offset: 0x0006A449
			// (set) Token: 0x0600106D RID: 4205 RVA: 0x0006C051 File Offset: 0x0006A451
			public Type type { get; protected set; }

			// Token: 0x0600106E RID: 4206 RVA: 0x0006C05A File Offset: 0x0006A45A
			protected override void triggerClicked()
			{
				Assets.runtimeCreate(this.type, AssetBrowserContextCreateAssetHandler.directory);
				AssetBrowserWindow.browse(AssetBrowserContextCreateAssetHandler.directory);
				base.triggerClicked();
			}
		}
	}
}
