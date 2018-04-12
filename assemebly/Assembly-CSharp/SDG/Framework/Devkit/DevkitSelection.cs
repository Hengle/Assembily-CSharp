using System;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000133 RID: 307
	public struct DevkitSelection : IEquatable<DevkitSelection>
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x0004F3F1 File Offset: 0x0004D7F1
		public DevkitSelection(GameObject newGameObject, Collider newCollider)
		{
			this.gameObject = newGameObject;
			this.collider = newCollider;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x0004F401 File Offset: 0x0004D801
		// (set) Token: 0x0600096A RID: 2410 RVA: 0x0004F425 File Offset: 0x0004D825
		public Transform transform
		{
			get
			{
				return (!(this.gameObject != null)) ? null : this.gameObject.transform;
			}
			set
			{
				this.gameObject = ((!(value != null)) ? null : value.gameObject);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0004F445 File Offset: 0x0004D845
		public bool isValid
		{
			get
			{
				return this.gameObject != null && this.collider != null;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0004F467 File Offset: 0x0004D867
		public static bool operator ==(DevkitSelection a, DevkitSelection b)
		{
			return a.gameObject == b.gameObject;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0004F47C File Offset: 0x0004D87C
		public static bool operator !=(DevkitSelection a, DevkitSelection b)
		{
			return !(a == b);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0004F488 File Offset: 0x0004D888
		public bool Equals(DevkitSelection other)
		{
			return this.gameObject == other.gameObject;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0004F49C File Offset: 0x0004D89C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DevkitSelection devkitSelection = (DevkitSelection)obj;
			return this.gameObject == devkitSelection.gameObject;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0004F4CA File Offset: 0x0004D8CA
		public override int GetHashCode()
		{
			if (this.gameObject == null)
			{
				return -1;
			}
			return this.gameObject.GetHashCode();
		}

		// Token: 0x04000720 RID: 1824
		public static DevkitSelection invalid = new DevkitSelection(null, null);

		// Token: 0x04000721 RID: 1825
		public GameObject gameObject;

		// Token: 0x04000722 RID: 1826
		public Collider collider;
	}
}
