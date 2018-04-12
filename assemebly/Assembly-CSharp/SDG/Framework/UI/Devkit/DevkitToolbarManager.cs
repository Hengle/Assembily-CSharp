﻿using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x0200023C RID: 572
	public class DevkitToolbarManager
	{
		// Token: 0x060010C8 RID: 4296 RVA: 0x0006DF7C File Offset: 0x0006C37C
		public static void registerToolbarElement(string path, Sleek2Element element)
		{
			string[] array = path.Split(new char[]
			{
				'/'
			});
			DevkitToolbarBranch devkitToolbarBranch = DevkitToolbarManager.toolbarRoot;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				DevkitToolbarBranch devkitToolbarBranch2;
				if (devkitToolbarBranch.tree.TryGetValue(text, out devkitToolbarBranch2))
				{
					devkitToolbarBranch = devkitToolbarBranch2;
				}
				else
				{
					devkitToolbarBranch2 = new DevkitToolbarBranch();
					Sleek2ToolbarButton sleek2ToolbarButton = new Sleek2ToolbarButton();
					sleek2ToolbarButton.label.textComponent.text = text;
					devkitToolbarBranch2.dropdown = sleek2ToolbarButton.panel;
					if (i == 0)
					{
						DevkitWindowManager.toolbar.addElement(sleek2ToolbarButton);
					}
					else
					{
						sleek2ToolbarButton.transform.anchorMin = new Vector2(0f, 1f);
						sleek2ToolbarButton.transform.anchorMax = Vector2.one;
						sleek2ToolbarButton.transform.pivot = new Vector2(0.5f, 1f);
						sleek2ToolbarButton.transform.sizeDelta = new Vector2(0f, (float)Sleek2Config.bodyHeight);
						devkitToolbarBranch.dropdown.addElement(sleek2ToolbarButton);
					}
					devkitToolbarBranch.tree.Add(text, devkitToolbarBranch2);
					devkitToolbarBranch = devkitToolbarBranch2;
				}
			}
			devkitToolbarBranch.dropdown.addElement(element);
		}

		// Token: 0x04000A19 RID: 2585
		protected static DevkitToolbarBranch toolbarRoot = new DevkitToolbarBranch();
	}
}
