using System;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class DontDestroyOnLoad : MonoBehaviour
{
	// Token: 0x06001691 RID: 5777 RVA: 0x00085663 File Offset: 0x00083A63
	private void OnEnable()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}
}
