using System;
using UnityEngine;

// Token: 0x020005D1 RID: 1489
public class ItemLook : MonoBehaviour
{
	// Token: 0x06002A24 RID: 10788 RVA: 0x001067E0 File Offset: 0x00104BE0
	private void Update()
	{
		this._yaw = Mathf.Lerp(this._yaw, this.yaw, 4f * Time.deltaTime);
		this.inspectCamera.transform.rotation = Quaternion.Euler(20f, this._yaw, 0f);
		this.inspectCamera.transform.position = this.pos - this.inspectCamera.transform.forward * this.dist;
	}

	// Token: 0x04001A24 RID: 6692
	public Camera inspectCamera;

	// Token: 0x04001A25 RID: 6693
	public float _yaw;

	// Token: 0x04001A26 RID: 6694
	public float yaw;

	// Token: 0x04001A27 RID: 6695
	public float dist = 2.25f;

	// Token: 0x04001A28 RID: 6696
	public Vector3 pos;
}
