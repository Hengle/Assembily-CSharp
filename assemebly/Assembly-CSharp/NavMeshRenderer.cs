using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[ExecuteInEditMode]
public class NavMeshRenderer : MonoBehaviour
{
	// Token: 0x060005B3 RID: 1459 RVA: 0x00034899 File Offset: 0x00032C99
	public string SomeFunction()
	{
		return this.lastLevel;
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x000348A1 File Offset: 0x00032CA1
	private void Update()
	{
	}

	// Token: 0x040004AC RID: 1196
	private string lastLevel = string.Empty;
}
