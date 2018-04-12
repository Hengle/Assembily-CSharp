using System;
using SDG.Framework.Devkit.Transactions;
using SDG.Framework.UI.Devkit;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.UI
{
	// Token: 0x02000237 RID: 567
	public class DevkitHotkeys : MonoBehaviour
	{
		// Token: 0x060010B4 RID: 4276 RVA: 0x0006DB2B File Offset: 0x0006BF2B
		public static void registerTool(int hotkey, Sleek2Window tool)
		{
			if (hotkey < 0 || hotkey >= 10)
			{
				return;
			}
			DevkitHotkeys.tools[hotkey] = tool;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0006DB48 File Offset: 0x0006BF48
		private void Update()
		{
			if (EventSystem.current.currentSelectedGameObject != null && DevkitWindowManager.isActive)
			{
				return;
			}
			if (Player.player != null)
			{
				return;
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				int num = -1;
				if (Input.GetKeyDown(KeyCode.Alpha0))
				{
					num = 0;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					num = 1;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					num = 2;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					num = 3;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					num = 4;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha5))
				{
					num = 5;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha6))
				{
					num = 6;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha7))
				{
					num = 7;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha8))
				{
					num = 8;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha9))
				{
					num = 9;
				}
				if (num != -1 && DevkitHotkeys.tools[num] != null)
				{
					DevkitHotkeys.tools[num].isActive = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftControl))
			{
				if (Input.GetKey(KeyCode.LeftShift))
				{
					DevkitTransactionManager.redo();
				}
				else
				{
					DevkitTransactionManager.undo();
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0006DCAC File Offset: 0x0006C0AC
		private void OnEnable()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0006DCB9 File Offset: 0x0006C0B9
		private void Start()
		{
			if (Dedicator.isDedicated)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x04000A11 RID: 2577
		private static Sleek2Window[] tools = new Sleek2Window[10];
	}
}
