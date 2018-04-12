using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Unturned
{
	// Token: 0x02000718 RID: 1816
	public class WebLink : MonoBehaviour
	{
		// Token: 0x06003396 RID: 13206 RVA: 0x0014EBC1 File Offset: 0x0014CFC1
		private void onClick()
		{
			if (!Provider.provider.browserService.canOpenBrowser)
			{
				MenuUI.alert(MenuDashboardUI.localization.format("Overlay"));
				return;
			}
			Provider.provider.browserService.open(this.url);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0014EC01 File Offset: 0x0014D001
		private void Start()
		{
			this.targetButton.onClick.AddListener(new UnityAction(this.onClick));
		}

		// Token: 0x040022FE RID: 8958
		public Button targetButton;

		// Token: 0x040022FF RID: 8959
		public string url;
	}
}
