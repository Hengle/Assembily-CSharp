using System;
using UnityEngine;

// Token: 0x02000317 RID: 791
public class LaserHelper : MonoBehaviour
{
	// Token: 0x06001693 RID: 5779 RVA: 0x00085678 File Offset: 0x00083A78
	private void OnEnable()
	{
		if (this.begin == null)
		{
			return;
		}
		if (this.end == null)
		{
			return;
		}
		if (this.laser == null)
		{
			return;
		}
		Vector3 vector = this.end.position - this.begin.position;
		this.laser.position = this.begin.position + vector / 2f;
		this.laser.rotation = Quaternion.LookRotation(vector) * Quaternion.Euler(-90f, 0f, 0f);
		this.laser.localScale = new Vector3(1f, vector.magnitude / 2f, 1f);
	}

	// Token: 0x04000C46 RID: 3142
	public Transform begin;

	// Token: 0x04000C47 RID: 3143
	public Transform end;

	// Token: 0x04000C48 RID: 3144
	public Transform laser;
}
