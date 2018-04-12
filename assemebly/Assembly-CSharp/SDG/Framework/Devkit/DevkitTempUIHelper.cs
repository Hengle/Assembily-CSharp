using System;
using SDG.Framework.UI.Devkit;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000135 RID: 309
	public class DevkitTempUIHelper : MonoBehaviour
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x0004FB67 File Offset: 0x0004DF67
		protected void OnGUI()
		{
			if (DevkitTempUIHelper.window == null)
			{
				return;
			}
			DevkitTempUIHelper.window.draw(false);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0004FB80 File Offset: 0x0004DF80
		protected void Update()
		{
			if (DevkitTempUIHelper.window == null)
			{
				return;
			}
			if (Input.GetKeyDown(ControlsSettings.screenshot))
			{
				Provider.takeScreenshot();
			}
			if (Input.GetKeyDown(ControlsSettings.hud))
			{
				DevkitWindowManager.isActive = false;
				DevkitTempUIHelper.window.isEnabled = !DevkitTempUIHelper.window.isEnabled;
				DevkitTempUIHelper.window.drawCursorWhileDisabled = false;
			}
			if (Input.GetKeyDown(ControlsSettings.terminal))
			{
				DevkitWindowManager.isActive = !DevkitWindowManager.isActive;
				DevkitTempUIHelper.window.isEnabled = !DevkitWindowManager.isActive;
				DevkitTempUIHelper.window.drawCursorWhileDisabled = DevkitWindowManager.isActive;
			}
			if (Input.GetKeyDown(ControlsSettings.refreshAssets))
			{
				Assets.refresh();
			}
			DevkitTempUIHelper.window.showCursor = !DevkitNavigation.isNavigating;
			DevkitTempUIHelper.window.updateDebug();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0004FC50 File Offset: 0x0004E050
		protected void Awake()
		{
			AudioListener component = LoadingUI.loader.GetComponent<AudioListener>();
			if (component)
			{
				UnityEngine.Object.Destroy(component);
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0004FC79 File Offset: 0x0004E079
		protected void Start()
		{
			DevkitTempUIHelper.window = new SleekWindow();
			OptionsSettings.apply();
			GraphicsSettings.apply();
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0004FC8F File Offset: 0x0004E08F
		protected void OnDestroy()
		{
			if (DevkitTempUIHelper.window == null)
			{
				return;
			}
			DevkitTempUIHelper.window.destroy();
		}

		// Token: 0x0400072C RID: 1836
		public static SleekWindow window;
	}
}
