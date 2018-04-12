using System;
using SDG.Framework.UI.Components;
using SDG.Framework.UI.Devkit;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200012E RID: 302
	public class DevkitInput : MonoBehaviour
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0004EAF2 File Offset: 0x0004CEF2
		public static bool canEditorReceiveInput
		{
			get
			{
				return Viewport.hasPointer || !EventSystem.current.IsPointerOverGameObject();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x0004EB0E File Offset: 0x0004CF0E
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x0004EB15 File Offset: 0x0004CF15
		public static Vector3 pointerViewportPoint { get; protected set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x0004EB1D File Offset: 0x0004CF1D
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0004EB24 File Offset: 0x0004CF24
		public static Ray pointerToWorldRay { get; protected set; }

		// Token: 0x06000940 RID: 2368 RVA: 0x0004EB2C File Offset: 0x0004CF2C
		protected void Update()
		{
			if (DevkitWindowManager.isActive)
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition.x -= Viewport.screenRect.x;
				mousePosition.x /= Viewport.screenRect.size.x;
				mousePosition.x = Mathf.Clamp01(mousePosition.x);
				mousePosition.y -= Viewport.screenRect.y;
				mousePosition.y /= Viewport.screenRect.size.y;
				mousePosition.y = Mathf.Clamp01(mousePosition.y);
				DevkitInput.pointerViewportPoint = mousePosition;
			}
			else
			{
				Vector3 mousePosition2 = Input.mousePosition;
				mousePosition2.x /= (float)Screen.width;
				mousePosition2.y /= (float)Screen.height;
				DevkitInput.pointerViewportPoint = mousePosition2;
			}
			DevkitInput.pointerToWorldRay = MainCamera.instance.ViewportPointToRay(DevkitInput.pointerViewportPoint);
		}
	}
}
