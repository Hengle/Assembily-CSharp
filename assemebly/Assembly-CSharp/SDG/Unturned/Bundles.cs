using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200038E RID: 910
	public class Bundles : MonoBehaviour
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x0009062B File Offset: 0x0008EA2B
		public static bool isInitialized
		{
			get
			{
				return Bundles._isInitialized;
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00090632 File Offset: 0x0008EA32
		public static Bundle getBundle(string path)
		{
			return Bundles.getBundle(path, true);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0009063B File Offset: 0x0008EA3B
		public static Bundle getBundle(string path, bool usePath)
		{
			return Bundles.getBundle(path, usePath, false);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00090645 File Offset: 0x0008EA45
		public static Bundle getBundle(string path, bool usePath, bool loadFromResources)
		{
			return new Bundle(path, usePath, loadFromResources);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0009064F File Offset: 0x0008EA4F
		private void Awake()
		{
			if (Bundles.isInitialized)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			Bundles._isInitialized = true;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x04000DB4 RID: 3508
		private static bool _isInitialized;
	}
}
