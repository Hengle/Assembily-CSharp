using System;
using UnityEngine;

// Token: 0x020007D4 RID: 2004
public class MeshContainer
{
	// Token: 0x06003A8B RID: 14987 RVA: 0x001C5603 File Offset: 0x001C3A03
	public MeshContainer(Mesh m)
	{
		this.mesh = m;
		this.vertices = m.vertices;
		this.normals = m.normals;
	}

	// Token: 0x06003A8C RID: 14988 RVA: 0x001C562A File Offset: 0x001C3A2A
	public void Update()
	{
		this.mesh.vertices = this.vertices;
		this.mesh.normals = this.normals;
	}

	// Token: 0x04002E57 RID: 11863
	public Mesh mesh;

	// Token: 0x04002E58 RID: 11864
	public Vector3[] vertices;

	// Token: 0x04002E59 RID: 11865
	public Vector3[] normals;
}
