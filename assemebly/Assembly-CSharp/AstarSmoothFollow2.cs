using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class AstarSmoothFollow2 : MonoBehaviour
{
	// Token: 0x060003F6 RID: 1014 RVA: 0x00020484 File Offset: 0x0001E884
	private void LateUpdate()
	{
		Vector3 b;
		if (this.staticOffset)
		{
			b = this.target.position + new Vector3(0f, this.height, this.distance);
		}
		else if (this.followBehind)
		{
			b = this.target.TransformPoint(0f, this.height, -this.distance);
		}
		else
		{
			b = this.target.TransformPoint(0f, this.height, this.distance);
		}
		base.transform.position = Vector3.Lerp(base.transform.position, b, Time.deltaTime * this.damping);
		if (this.smoothRotation)
		{
			Quaternion b2 = Quaternion.LookRotation(this.target.position - base.transform.position, this.target.up);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b2, Time.deltaTime * this.rotationDamping);
		}
		else
		{
			base.transform.LookAt(this.target, this.target.up);
		}
	}

	// Token: 0x0400036D RID: 877
	public Transform target;

	// Token: 0x0400036E RID: 878
	public float distance = 3f;

	// Token: 0x0400036F RID: 879
	public float height = 3f;

	// Token: 0x04000370 RID: 880
	public float damping = 5f;

	// Token: 0x04000371 RID: 881
	public bool smoothRotation = true;

	// Token: 0x04000372 RID: 882
	public bool followBehind = true;

	// Token: 0x04000373 RID: 883
	public float rotationDamping = 10f;

	// Token: 0x04000374 RID: 884
	public bool staticOffset;
}
