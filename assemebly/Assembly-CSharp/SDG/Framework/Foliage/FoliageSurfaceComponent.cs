using System;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Foliage
{
	// Token: 0x020001A6 RID: 422
	public class FoliageSurfaceComponent : MonoBehaviour, IFoliageSurface
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x0005D340 File Offset: 0x0005B740
		public FoliageBounds getFoliageSurfaceBounds()
		{
			bool activeSelf = base.gameObject.activeSelf;
			if (!activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			FoliageBounds result = new FoliageBounds(this.surfaceCollider.bounds);
			if (!activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			return result;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0005D390 File Offset: 0x0005B790
		public bool getFoliageSurfaceInfo(Vector3 position, out Vector3 surfacePosition, out Vector3 surfaceNormal)
		{
			RaycastHit raycastHit;
			if (this.surfaceCollider.Raycast(new Ray(position, Vector3.down), out raycastHit, 1024f))
			{
				surfacePosition = raycastHit.point;
				surfaceNormal = raycastHit.normal;
				return true;
			}
			surfacePosition = Vector3.zero;
			surfaceNormal = Vector3.up;
			return false;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0005D3F4 File Offset: 0x0005B7F4
		public void bakeFoliageSurface(FoliageBakeSettings bakeSettings, FoliageTile foliageTile)
		{
			FoliageInfoCollectionAsset foliageInfoCollectionAsset = Assets.find<FoliageInfoCollectionAsset>(this.foliage);
			if (foliageInfoCollectionAsset == null)
			{
				return;
			}
			bool activeSelf = base.gameObject.activeSelf;
			if (!activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			Bounds worldBounds = foliageTile.worldBounds;
			Vector3 min = worldBounds.min;
			Vector3 max = worldBounds.max;
			Bounds bounds = this.surfaceCollider.bounds;
			Vector3 min2 = bounds.min;
			Vector3 max2 = bounds.max;
			foliageInfoCollectionAsset.bakeFoliage(bakeSettings, this, new Bounds
			{
				min = new Vector3(Mathf.Max(min.x, min2.x), min2.y, Mathf.Max(min.z, min2.z)),
				max = new Vector3(Mathf.Min(max.x, max2.x), max2.y, Mathf.Min(max.z, max2.z))
			}, 1f);
			if (!activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0005D504 File Offset: 0x0005B904
		protected void OnEnable()
		{
			if (this.isRegistered)
			{
				return;
			}
			this.isRegistered = true;
			FoliageSystem.addSurface(this);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0005D51F File Offset: 0x0005B91F
		protected void OnDestroy()
		{
			if (!this.isRegistered)
			{
				return;
			}
			this.isRegistered = false;
			FoliageSystem.removeSurface(this);
		}

		// Token: 0x040008A8 RID: 2216
		public AssetReference<FoliageInfoCollectionAsset> foliage;

		// Token: 0x040008A9 RID: 2217
		public Collider surfaceCollider;

		// Token: 0x040008AA RID: 2218
		protected bool isRegistered;
	}
}
