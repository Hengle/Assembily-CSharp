using System;
using SDG.Framework.UI.Devkit;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A0 RID: 1184
	public class EditorUI : MonoBehaviour
	{
		// Token: 0x06001F3B RID: 7995 RVA: 0x000AD38C File Offset: 0x000AB78C
		public static void hint(EEditorMessage message, string text)
		{
			if (!EditorUI.isMessaged)
			{
				EditorUI.messageBox.isVisible = true;
				EditorUI.lastHinted = true;
				EditorUI.isHinted = true;
				if (message == EEditorMessage.FOCUS)
				{
					EditorUI.messageBox.text = text;
				}
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000AD3C4 File Offset: 0x000AB7C4
		public static void message(EEditorMessage message)
		{
			if (!OptionsSettings.hints)
			{
				return;
			}
			EditorUI.messageBox.isVisible = true;
			EditorUI.lastMessage = Time.realtimeSinceStartup;
			EditorUI.isMessaged = true;
			if (message == EEditorMessage.HEIGHTS)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Heights", new object[]
				{
					ControlsSettings.tool_2
				});
			}
			else if (message == EEditorMessage.ROADS)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Roads", new object[]
				{
					ControlsSettings.tool_1,
					ControlsSettings.tool_2
				});
			}
			else if (message == EEditorMessage.NAVIGATION)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Navigation", new object[]
				{
					ControlsSettings.tool_2
				});
			}
			else if (message == EEditorMessage.OBJECTS)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Objects", new object[]
				{
					ControlsSettings.other,
					ControlsSettings.tool_2,
					ControlsSettings.tool_2
				});
			}
			else if (message == EEditorMessage.NODES)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Nodes", new object[]
				{
					ControlsSettings.tool_2
				});
			}
			else if (message == EEditorMessage.VISIBILITY)
			{
				EditorUI.messageBox.text = EditorDashboardUI.localization.format("Visibility");
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x000AD555 File Offset: 0x000AB955
		private void OnGUI()
		{
			if (EditorUI.window == null)
			{
				return;
			}
			EditorUI.window.draw(false);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x000AD570 File Offset: 0x000AB970
		private void Update()
		{
			if (EditorUI.window == null)
			{
				return;
			}
			if (EditorLevelVisibilityUI.active)
			{
				EditorLevelVisibilityUI.update();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (EditorPauseUI.active)
				{
					EditorPauseUI.close();
				}
				else
				{
					EditorPauseUI.open();
				}
			}
			if (EditorUI.window != null)
			{
				if (Input.GetKeyDown(ControlsSettings.screenshot))
				{
					Provider.takeScreenshot();
				}
				if (Input.GetKeyDown(ControlsSettings.hud))
				{
					DevkitWindowManager.isActive = false;
					EditorUI.window.isEnabled = !EditorUI.window.isEnabled;
					EditorUI.window.drawCursorWhileDisabled = false;
				}
				if (Input.GetKeyDown(ControlsSettings.terminal))
				{
					DevkitWindowManager.isActive = !DevkitWindowManager.isActive;
					EditorUI.window.isEnabled = !DevkitWindowManager.isActive;
					EditorUI.window.drawCursorWhileDisabled = DevkitWindowManager.isActive;
				}
			}
			if (Input.GetKeyDown(ControlsSettings.refreshAssets))
			{
				Assets.refresh();
			}
			EditorUI.window.showCursor = !EditorInteract.isFlying;
			EditorUI.window.updateDebug();
			if (EditorUI.isMessaged)
			{
				if (Time.realtimeSinceStartup - EditorUI.lastMessage > EditorUI.MESSAGE_TIME)
				{
					EditorUI.isMessaged = false;
					if (!EditorUI.isHinted)
					{
						EditorUI.messageBox.isVisible = false;
					}
				}
			}
			else if (EditorUI.isHinted)
			{
				if (!EditorUI.lastHinted)
				{
					EditorUI.isHinted = false;
					EditorUI.messageBox.isVisible = false;
				}
				EditorUI.lastHinted = false;
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x000AD6E8 File Offset: 0x000ABAE8
		private void Awake()
		{
			AudioListener component = LoadingUI.loader.GetComponent<AudioListener>();
			if (component)
			{
				UnityEngine.Object.Destroy(component);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000AD714 File Offset: 0x000ABB14
		private void Start()
		{
			EditorUI.window = new SleekWindow();
			base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
			OptionsSettings.apply();
			GraphicsSettings.apply();
			new EditorDashboardUI();
			EditorUI.messageBox = new SleekBox();
			EditorUI.messageBox.positionOffset_X = -150;
			EditorUI.messageBox.positionOffset_Y = -60;
			EditorUI.messageBox.positionScale_X = 0.5f;
			EditorUI.messageBox.positionScale_Y = 1f;
			EditorUI.messageBox.sizeOffset_X = 300;
			EditorUI.messageBox.sizeOffset_Y = 50;
			EditorUI.messageBox.fontSize = 14;
			EditorUI.window.add(EditorUI.messageBox);
			EditorUI.messageBox.isVisible = false;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000AD7D2 File Offset: 0x000ABBD2
		private void OnDestroy()
		{
			if (EditorUI.window == null)
			{
				return;
			}
			EditorUI.window.destroy();
		}

		// Token: 0x040012E9 RID: 4841
		public static readonly float MESSAGE_TIME = 2f;

		// Token: 0x040012EA RID: 4842
		public static readonly float HINT_TIME = 0.15f;

		// Token: 0x040012EB RID: 4843
		public static SleekWindow window;

		// Token: 0x040012EC RID: 4844
		private static SleekBox messageBox;

		// Token: 0x040012ED RID: 4845
		private static float lastMessage;

		// Token: 0x040012EE RID: 4846
		private static bool isMessaged;

		// Token: 0x040012EF RID: 4847
		private static bool lastHinted;

		// Token: 0x040012F0 RID: 4848
		private static bool isHinted;
	}
}
