using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000748 RID: 1864
	public class Tools : MonoBehaviour
	{
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x00159423 File Offset: 0x00157823
		public static bool isInitialized
		{
			get
			{
				return Tools._isInitialized;
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x0015942A File Offset: 0x0015782A
		private void Awake()
		{
			if (Tools.isInitialized)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			Tools._isInitialized = true;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x040023B5 RID: 9141
		private static bool _isInitialized;
	}
}
