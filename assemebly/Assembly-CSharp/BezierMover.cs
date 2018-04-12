using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class BezierMover : MonoBehaviour
{
	// Token: 0x060003D7 RID: 983 RVA: 0x0001E6FE File Offset: 0x0001CAFE
	private void Update()
	{
		this.Move(true);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001E708 File Offset: 0x0001CB08
	private Vector3 Plot(float t)
	{
		int num = this.points.Length;
		int num2 = Mathf.FloorToInt(t);
		Vector3 normalized = ((this.points[(num2 + 1) % num].position - this.points[num2 % num].position).normalized - (this.points[(num2 - 1 + num) % num].position - this.points[num2 % num].position).normalized).normalized;
		Vector3 normalized2 = ((this.points[(num2 + 2) % num].position - this.points[(num2 + 1) % num].position).normalized - (this.points[(num2 + num) % num].position - this.points[(num2 + 1) % num].position).normalized).normalized;
		Debug.DrawLine(this.points[num2 % num].position, this.points[num2 % num].position + normalized * this.tangentLengths, Color.red);
		Debug.DrawLine(this.points[(num2 + 1) % num].position - normalized2 * this.tangentLengths, this.points[(num2 + 1) % num].position, Color.green);
		return AstarMath.CubicBezier(this.points[num2 % num].position, this.points[num2 % num].position + normalized * this.tangentLengths, this.points[(num2 + 1) % num].position - normalized2 * this.tangentLengths, this.points[(num2 + 1) % num].position, t - (float)num2);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001E8E4 File Offset: 0x0001CCE4
	private void Move(bool progress)
	{
		float num = this.time;
		float num2 = this.time + 1f;
		while (num2 - num > 0.0001f)
		{
			float num3 = (num + num2) / 2f;
			Vector3 a = this.Plot(num3);
			if ((a - base.transform.position).sqrMagnitude > this.speed * Time.deltaTime * (this.speed * Time.deltaTime))
			{
				num2 = num3;
			}
			else
			{
				num = num3;
			}
		}
		this.time = (num + num2) / 2f;
		Vector3 vector = this.Plot(this.time);
		Vector3 a2 = this.Plot(this.time + 0.001f);
		base.transform.position = vector;
		base.transform.rotation = Quaternion.LookRotation(a2 - vector);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001E9C4 File Offset: 0x0001CDC4
	public void OnDrawGizmos()
	{
		if (this.points.Length >= 3)
		{
			for (int i = 0; i < this.points.Length; i++)
			{
				if (this.points[i] == null)
				{
					return;
				}
			}
			for (int j = 0; j < this.points.Length; j++)
			{
				int num = this.points.Length;
				Vector3 normalized = ((this.points[(j + 1) % num].position - this.points[j].position).normalized - (this.points[(j - 1 + num) % num].position - this.points[j].position).normalized).normalized;
				Vector3 normalized2 = ((this.points[(j + 2) % num].position - this.points[(j + 1) % num].position).normalized - (this.points[(j + num) % num].position - this.points[(j + 1) % num].position).normalized).normalized;
				Vector3 from = this.points[j].position;
				for (int k = 1; k <= 100; k++)
				{
					Vector3 vector = AstarMath.CubicBezier(this.points[j].position, this.points[j].position + normalized * this.tangentLengths, this.points[(j + 1) % num].position - normalized2 * this.tangentLengths, this.points[(j + 1) % num].position, (float)k / 100f);
					Gizmos.DrawLine(from, vector);
					from = vector;
				}
			}
		}
	}

	// Token: 0x04000346 RID: 838
	public Transform[] points;

	// Token: 0x04000347 RID: 839
	public float tangentLengths = 5f;

	// Token: 0x04000348 RID: 840
	public float speed = 1f;

	// Token: 0x04000349 RID: 841
	private float time;
}
