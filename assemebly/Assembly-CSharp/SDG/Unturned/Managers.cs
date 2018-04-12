using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020005B3 RID: 1459
	public class Managers : MonoBehaviour
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000F7FE0 File Offset: 0x000F63E0
		public static bool isInitialized
		{
			get
			{
				return Managers._isInitialized;
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000F7FE7 File Offset: 0x000F63E7
		private void Awake()
		{
			if (Managers.isInitialized)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			Managers._isInitialized = true;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			base.GetComponent<SteamChannel>().setup();
		}

		// Token: 0x04001985 RID: 6533
		private static bool _isInitialized;
	}
}
