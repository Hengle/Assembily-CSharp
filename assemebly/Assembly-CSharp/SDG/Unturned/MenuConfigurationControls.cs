using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005D2 RID: 1490
	public class MenuConfigurationControls : MonoBehaviour
	{
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x00106872 File Offset: 0x00104C72
		// (set) Token: 0x06002A27 RID: 10791 RVA: 0x00106879 File Offset: 0x00104C79
		public static byte binding
		{
			get
			{
				return MenuConfigurationControls._binding;
			}
			set
			{
				MenuConfigurationControls._binding = value;
				SleekRender.allowInput = (MenuConfigurationControls.binding == byte.MaxValue);
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x00106892 File Offset: 0x00104C92
		private static void cancel()
		{
			MenuConfigurationControlsUI.cancel();
			MenuConfigurationControls.binding = byte.MaxValue;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x001068A3 File Offset: 0x00104CA3
		private static void bind(KeyCode key)
		{
			MenuConfigurationControlsUI.bind(key);
			MenuConfigurationControls.binding = byte.MaxValue;
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x001068B8 File Offset: 0x00104CB8
		private void Update()
		{
			if (MenuConfigurationControls.binding != 255)
			{
				if (Event.current.type == EventType.KeyDown)
				{
					if (Event.current.keyCode == KeyCode.Backspace || Event.current.keyCode == KeyCode.Escape)
					{
						MenuConfigurationControls.cancel();
					}
					else
					{
						MenuConfigurationControls.bind(Event.current.keyCode);
					}
				}
				else if (Event.current.type == EventType.MouseDown)
				{
					if (Event.current.button == 0)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse0);
					}
					else if (Event.current.button == 1)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse1);
					}
					else if (Event.current.button == 2)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse2);
					}
					else if (Event.current.button == 3)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse3);
					}
					else if (Event.current.button == 4)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse4);
					}
					else if (Event.current.button == 5)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse5);
					}
					else if (Event.current.button == 6)
					{
						MenuConfigurationControls.bind(KeyCode.Mouse6);
					}
				}
				else if (Event.current.shift)
				{
					MenuConfigurationControls.bind(KeyCode.LeftShift);
				}
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00106A23 File Offset: 0x00104E23
		private void Awake()
		{
			MenuConfigurationControls.binding = byte.MaxValue;
		}

		// Token: 0x04001A29 RID: 6697
		private static byte _binding;
	}
}
