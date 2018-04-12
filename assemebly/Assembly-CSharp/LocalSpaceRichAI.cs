using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class LocalSpaceRichAI : RichAI
{
	// Token: 0x060003DF RID: 991 RVA: 0x0001EBE4 File Offset: 0x0001CFE4
	public override void UpdatePath()
	{
		this.canSearchPath = true;
		this.waitingForPathCalc = false;
		Path currentPath = this.seeker.GetCurrentPath();
		if (currentPath != null && !this.seeker.IsDone())
		{
			currentPath.Error();
			currentPath.Claim(this);
			currentPath.Release(this);
		}
		this.waitingForPathCalc = true;
		this.lastRepath = Time.time;
		Matrix4x4 matrix = this.graph.GetMatrix();
		this.seeker.StartPath(matrix.MultiplyPoint3x4(this.tr.position), matrix.MultiplyPoint3x4(this.target.position));
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001EC84 File Offset: 0x0001D084
	protected override Vector3 UpdateTarget(RichFunnel fn)
	{
		Matrix4x4 matrix = this.graph.GetMatrix();
		Matrix4x4 inverse = matrix.inverse;
		Debug.DrawRay(matrix.MultiplyPoint3x4(this.tr.position), Vector3.up * 2f, Color.red);
		Debug.DrawRay(inverse.MultiplyPoint3x4(this.tr.position), Vector3.up * 2f, Color.green);
		this.buffer.Clear();
		Vector3 vector = this.tr.position;
		bool flag;
		vector = inverse.MultiplyPoint3x4(fn.Update(matrix.MultiplyPoint3x4(vector), this.buffer, 2, out this.lastCorner, out flag));
		Debug.DrawRay(vector, Vector3.up * 3f, Color.black);
		for (int i = 0; i < this.buffer.Count; i++)
		{
			this.buffer[i] = inverse.MultiplyPoint3x4(this.buffer[i]);
			Debug.DrawRay(this.buffer[i], Vector3.up * 3f, Color.yellow);
		}
		if (flag && !this.waitingForPathCalc)
		{
			this.UpdatePath();
		}
		return vector;
	}

	// Token: 0x0400034B RID: 843
	public LocalSpaceGraph graph;
}
