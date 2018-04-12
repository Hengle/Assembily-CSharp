using System;
using SDG.Framework.Modules;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000582 RID: 1410
	public class Setup : MonoBehaviour
	{
		// Token: 0x060026E3 RID: 9955 RVA: 0x000E725C File Offset: 0x000E565C
		private void Awake()
		{
			if (this.awakeDedicator)
			{
				base.GetComponent<Dedicator>().awake();
			}
			if (this.awakeLogs)
			{
				base.GetComponent<Logs>().awake();
			}
			if (this.awakeModuleHook)
			{
				base.GetComponent<ModuleHook>().awake();
			}
			if (this.awakeProvider)
			{
				base.GetComponent<Provider>().awake();
			}
			if (this.startModuleHook)
			{
				base.GetComponent<ModuleHook>().start();
			}
			if (this.startProvider)
			{
				base.GetComponent<Provider>().start();
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000E72ED File Offset: 0x000E56ED
		private void Start()
		{
			if (!Dedicator.isDedicated)
			{
				MenuSettings.load();
				GraphicsSettings.resize();
				LoadingUI.updateScene();
			}
		}

		// Token: 0x04001895 RID: 6293
		public bool awakeDedicator = true;

		// Token: 0x04001896 RID: 6294
		public bool awakeLogs = true;

		// Token: 0x04001897 RID: 6295
		public bool awakeModuleHook = true;

		// Token: 0x04001898 RID: 6296
		public bool awakeProvider = true;

		// Token: 0x04001899 RID: 6297
		public bool startModuleHook = true;

		// Token: 0x0400189A RID: 6298
		public bool startProvider = true;
	}
}
