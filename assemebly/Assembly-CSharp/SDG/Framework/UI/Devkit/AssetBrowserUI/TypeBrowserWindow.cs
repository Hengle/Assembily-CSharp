using System;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Tools;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Modules;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.AssetBrowserUI
{
	// Token: 0x0200029A RID: 666
	public class TypeBrowserWindow : Sleek2Window
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x0007D1DC File Offset: 0x0007B5DC
		public TypeBrowserWindow()
		{
			base.gameObject.name = "Type_Browser";
			base.tab.label.translation = new TranslatedText(new TranslationReference("SDG", "Devkit.Window.Type_Browser.Title"));
			base.tab.label.translation.format();
			this.modulesBox = new Sleek2Element();
			this.modulesBox.name = "Modules";
			this.addElement(this.modulesBox);
			this.typesBox = new Sleek2Element();
			this.typesBox.name = "Types";
			this.addElement(this.typesBox);
			this.separator = new Sleek2Separator();
			this.separator.handle.value = 0.25f;
			this.separator.handle.a = this.modulesBox.transform;
			this.separator.handle.b = this.typesBox.transform;
			this.addElement(this.separator);
			this.modulesView = new Sleek2Scrollview();
			this.modulesView.transform.reset();
			this.modulesView.transform.offsetMin = new Vector2(5f, 5f);
			this.modulesView.transform.offsetMax = new Vector2(-5f, -5f);
			this.modulesView.vertical = true;
			this.modulesPanel = new Sleek2VerticalScrollviewContents();
			this.modulesPanel.name = "Panel";
			this.modulesView.panel = this.modulesPanel;
			this.modulesBox.addElement(this.modulesView);
			TypeBrowserModuleButton typeBrowserModuleButton = new TypeBrowserModuleButton(null, ModuleHook.coreTypes);
			typeBrowserModuleButton.clicked += this.handleModuleButtonButtonClicked;
			this.modulesPanel.addElement(typeBrowserModuleButton);
			for (int i = 0; i < ModuleHook.modules.Count; i++)
			{
				Module module = ModuleHook.modules[i];
				TypeBrowserModuleButton typeBrowserModuleButton2 = new TypeBrowserModuleButton(module, module.types);
				typeBrowserModuleButton2.clicked += this.handleModuleButtonButtonClicked;
				this.modulesPanel.addElement(typeBrowserModuleButton2);
			}
			this.typesView = new Sleek2Scrollview();
			this.typesView.transform.reset();
			this.typesView.transform.offsetMin = new Vector2(5f, 5f);
			this.typesView.transform.offsetMax = new Vector2(-5f, -5f);
			this.typesView.vertical = true;
			this.typesPanel = new Sleek2Element();
			this.typesPanel.name = "Panel";
			GridLayoutGroup gridLayoutGroup = this.typesPanel.gameObject.AddComponent<GridLayoutGroup>();
			gridLayoutGroup.cellSize = new Vector2(200f, 50f);
			gridLayoutGroup.spacing = new Vector2(5f, 5f);
			ContentSizeFitter contentSizeFitter = this.typesPanel.gameObject.AddComponent<ContentSizeFitter>();
			contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
			this.typesPanel.transform.reset();
			this.typesPanel.transform.pivot = new Vector2(0f, 1f);
			this.typesView.panel = this.typesPanel;
			this.typesBox.addElement(this.typesView);
			TypeBrowserWindow.browsed += this.handleBrowsed;
			this.handleBrowsed(TypeBrowserWindow.currentTypes);
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001396 RID: 5014 RVA: 0x0007D54C File Offset: 0x0007B94C
		// (remove) Token: 0x06001397 RID: 5015 RVA: 0x0007D580 File Offset: 0x0007B980
		protected static event TypeBrowserWindow.TypeBrowserWindowBrowsedHandler browsed;

		// Token: 0x06001398 RID: 5016 RVA: 0x0007D5B4 File Offset: 0x0007B9B4
		public static void browse(Type[] types)
		{
			TypeBrowserWindow.currentTypes = types;
			if (TypeBrowserWindow.browsed != null)
			{
				TypeBrowserWindow.browsed(TypeBrowserWindow.currentTypes);
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0007D5D5 File Offset: 0x0007B9D5
		protected override void readWindow(IFormattedFileReader reader)
		{
			this.separator.handle.value = reader.readValue<float>("Split");
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0007D5F2 File Offset: 0x0007B9F2
		protected override void writeWindow(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Split", this.separator.handle.value);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0007D60F File Offset: 0x0007BA0F
		protected override void triggerDestroyed()
		{
			TypeBrowserWindow.browsed -= this.handleBrowsed;
			base.triggerDestroyed();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0007D628 File Offset: 0x0007BA28
		protected void handleBrowsed(Type[] types)
		{
			this.typesPanel.clearElements();
			if (types == null)
			{
				return;
			}
			foreach (Type type in types)
			{
				if (!type.IsAbstract && typeof(IDevkitHierarchySpawnable).IsAssignableFrom(type))
				{
					TypeBrowserTypeButton typeBrowserTypeButton = new TypeBrowserTypeButton(type);
					typeBrowserTypeButton.clicked += this.handleTypeButtonClicked;
					this.typesPanel.addElement(typeBrowserTypeButton);
				}
			}
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0007D6AC File Offset: 0x0007BAAC
		protected void handleTypeButtonClicked(Sleek2ImageButton button)
		{
			Type type = (button as TypeBrowserTypeButton).type;
			if (type == null)
			{
				return;
			}
			DevkitSelectionToolTypeInstantiationInfo devkitSelectionToolTypeInstantiationInfo = new DevkitSelectionToolTypeInstantiationInfo();
			devkitSelectionToolTypeInstantiationInfo.type = type;
			if (typeof(IDevkitHierarchyAutoSpawnable).IsAssignableFrom(type))
			{
				devkitSelectionToolTypeInstantiationInfo.instantiate();
			}
			else
			{
				DevkitSelectionToolOptions.instance.instantiationInfo = devkitSelectionToolTypeInstantiationInfo;
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0007D704 File Offset: 0x0007BB04
		protected void handleModuleButtonButtonClicked(Sleek2ImageButton button)
		{
			TypeBrowserWindow.browse((button as TypeBrowserModuleButton).types);
		}

		// Token: 0x04000B39 RID: 2873
		protected static Type[] currentTypes;

		// Token: 0x04000B3A RID: 2874
		protected Sleek2Element modulesBox;

		// Token: 0x04000B3B RID: 2875
		protected Sleek2Element typesBox;

		// Token: 0x04000B3C RID: 2876
		protected Sleek2Separator separator;

		// Token: 0x04000B3D RID: 2877
		protected Sleek2Element modulesPanel;

		// Token: 0x04000B3E RID: 2878
		protected Sleek2Scrollview modulesView;

		// Token: 0x04000B3F RID: 2879
		protected Sleek2Element typesPanel;

		// Token: 0x04000B40 RID: 2880
		protected Sleek2Scrollview typesView;

		// Token: 0x04000B41 RID: 2881
		protected Sleek2Scrollbar modulesScrollbar;

		// Token: 0x0200029B RID: 667
		// (Invoke) Token: 0x060013A0 RID: 5024
		protected delegate void TypeBrowserWindowBrowsedHandler(Type[] types);
	}
}
