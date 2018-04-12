using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class Screenshot : MonoBehaviour
{
	// Token: 0x06001695 RID: 5781 RVA: 0x00085756 File Offset: 0x00083B56
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			Debug.Log("A");
			Application.CaptureScreenshot("Screenshot.png", 8);
			Debug.Log("B");
		}
	}
}
