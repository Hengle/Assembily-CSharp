using System;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
[RequireComponent(typeof(WaterBase))]
[ExecuteInEditMode]
public class SpecularLighting : MonoBehaviour
{
	// Token: 0x06003AA9 RID: 15017 RVA: 0x001C5F39 File Offset: 0x001C4339
	public void Start()
	{
		this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
	}

	// Token: 0x06003AAA RID: 15018 RVA: 0x001C5F5C File Offset: 0x001C435C
	public void Update()
	{
		if (!this.waterBase)
		{
			this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
		}
		if (this.specularLight && this.waterBase.sharedMaterial)
		{
			this.waterBase.sharedMaterial.SetVector("_WorldLightDir", this.specularLight.transform.forward);
		}
	}

	// Token: 0x04002E69 RID: 11881
	public Transform specularLight;

	// Token: 0x04002E6A RID: 11882
	private WaterBase waterBase;
}
