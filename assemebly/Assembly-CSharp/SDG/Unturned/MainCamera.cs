using System;
using System.Collections;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004BD RID: 1213
	public class MainCamera : MonoBehaviour
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000B1DCA File Offset: 0x000B01CA
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x000B1DD1 File Offset: 0x000B01D1
		public static Camera instance
		{
			get
			{
				return MainCamera._instance;
			}
			protected set
			{
				if (MainCamera.instance != value)
				{
					MainCamera._instance = value;
					MainCamera.triggerInstanceChanged();
				}
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000B1DEE File Offset: 0x000B01EE
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x000B1DF5 File Offset: 0x000B01F5
		public static bool isAvailable
		{
			get
			{
				return MainCamera._isAvailable;
			}
			protected set
			{
				if (MainCamera.isAvailable != value)
				{
					MainCamera._isAvailable = value;
					MainCamera.triggerAvailabilityChanged();
				}
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002072 RID: 8306 RVA: 0x000B1E10 File Offset: 0x000B0210
		public static Plane[] frustumPlanes
		{
			get
			{
				if (MainCamera.instance != null && MainCamera.instance.transform.hasChanged)
				{
					MainCamera._frustumPlanes = GeometryUtility.CalculateFrustumPlanes(MainCamera.instance);
					MainCamera.instance.transform.hasChanged = false;
				}
				return MainCamera._frustumPlanes;
			}
		}

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06002073 RID: 8307 RVA: 0x000B1E68 File Offset: 0x000B0268
		// (remove) Token: 0x06002074 RID: 8308 RVA: 0x000B1E9C File Offset: 0x000B029C
		public static event MainCameraInstanceChangedHandler instanceChanged;

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06002075 RID: 8309 RVA: 0x000B1ED0 File Offset: 0x000B02D0
		// (remove) Token: 0x06002076 RID: 8310 RVA: 0x000B1F04 File Offset: 0x000B0304
		public static event MainCameraAvailabilityChangedHandler availabilityChanged;

		// Token: 0x06002077 RID: 8311 RVA: 0x000B1F38 File Offset: 0x000B0338
		public IEnumerator activate()
		{
			yield return new WaitForEndOfFrame();
			MainCamera.isAvailable = true;
			yield break;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x000B1F4C File Offset: 0x000B034C
		protected static void triggerInstanceChanged()
		{
			if (MainCamera.instanceChanged != null)
			{
				MainCamera.instanceChanged();
			}
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x000B1F62 File Offset: 0x000B0362
		protected static void triggerAvailabilityChanged()
		{
			if (MainCamera.availabilityChanged != null)
			{
				MainCamera.availabilityChanged();
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000B1F78 File Offset: 0x000B0378
		public void Awake()
		{
			MainCamera.isAvailable = false;
			MainCamera.instance = base.transform.GetComponent<Camera>();
			base.StartCoroutine(this.activate());
		}

		// Token: 0x0400134D RID: 4941
		protected static Camera _instance;

		// Token: 0x0400134E RID: 4942
		protected static bool _isAvailable;

		// Token: 0x0400134F RID: 4943
		protected static Plane[] _frustumPlanes = new Plane[6];
	}
}
