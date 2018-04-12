using System;
using UnityEngine;

// Token: 0x020007DB RID: 2011
[ExecuteInEditMode]
public class WaterTile : MonoBehaviour
{
	// Token: 0x06003AB0 RID: 15024 RVA: 0x001C611F File Offset: 0x001C451F
	public void Start()
	{
		this.AcquireComponents();
	}

	// Token: 0x06003AB1 RID: 15025 RVA: 0x001C6128 File Offset: 0x001C4528
	private void AcquireComponents()
	{
		if (!this.reflection)
		{
			if (base.transform.parent)
			{
				this.reflection = base.transform.parent.GetComponent<PlanarReflection>();
			}
			else
			{
				this.reflection = base.transform.GetComponent<PlanarReflection>();
			}
		}
	}

	// Token: 0x06003AB2 RID: 15026 RVA: 0x001C6188 File Offset: 0x001C4588
	public void OnWillRenderObject()
	{
		Camera current = Camera.current;
		if (this.reflection)
		{
			this.reflection.WaterTileBeingRendered(base.transform, current);
		}
		if (this.waterBase)
		{
			this.waterBase.WaterTileBeingRendered(base.transform, current);
		}
	}

	// Token: 0x04002E72 RID: 11890
	public PlanarReflection reflection;

	// Token: 0x04002E73 RID: 11891
	public WaterBase waterBase;
}
