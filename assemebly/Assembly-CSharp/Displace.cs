using System;
using UnityEngine;

// Token: 0x020007D2 RID: 2002
[ExecuteInEditMode]
[RequireComponent(typeof(WaterBase))]
public class Displace : MonoBehaviour
{
	// Token: 0x06003A87 RID: 14983 RVA: 0x001C55B1 File Offset: 0x001C39B1
	public void Awake()
	{
		if (base.enabled)
		{
			this.OnEnable();
		}
		else
		{
			this.OnDisable();
		}
	}

	// Token: 0x06003A88 RID: 14984 RVA: 0x001C55CF File Offset: 0x001C39CF
	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
	}

	// Token: 0x06003A89 RID: 14985 RVA: 0x001C55E5 File Offset: 0x001C39E5
	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
	}
}
