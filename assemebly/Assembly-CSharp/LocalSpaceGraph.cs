using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class LocalSpaceGraph : MonoBehaviour
{
	// Token: 0x060003DC RID: 988 RVA: 0x0001EBB1 File Offset: 0x0001CFB1
	private void Start()
	{
		this.originalMatrix = base.transform.localToWorldMatrix;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0001EBC4 File Offset: 0x0001CFC4
	public Matrix4x4 GetMatrix()
	{
		return base.transform.worldToLocalMatrix * this.originalMatrix;
	}

	// Token: 0x0400034A RID: 842
	protected Matrix4x4 originalMatrix;
}
