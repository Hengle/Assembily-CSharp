using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

// Token: 0x02000078 RID: 120
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Pathfinding/AI/AIFollow (deprecated)")]
public class AIFollow : MonoBehaviour
{
	// Token: 0x060003EA RID: 1002 RVA: 0x0001FDB3 File Offset: 0x0001E1B3
	public void Start()
	{
		this.seeker = base.GetComponent<Seeker>();
		this.controller = base.GetComponent<CharacterController>();
		this.navmeshController = base.GetComponent<NavmeshController>();
		this.tr = base.transform;
		this.Repath();
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0001FDEB File Offset: 0x0001E1EB
	public void Reset()
	{
		this.path = null;
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001FDF4 File Offset: 0x0001E1F4
	public void OnPathComplete(Path p)
	{
		base.StartCoroutine(this.WaitToRepath());
		if (p.error)
		{
			return;
		}
		this.path = p.vectorPath.ToArray();
		float num = float.PositiveInfinity;
		int num2 = 0;
		for (int i = 0; i < this.path.Length - 1; i++)
		{
			float num3 = AstarMath.DistancePointSegmentStrict(this.path[i], this.path[i + 1], this.tr.position);
			if (num3 < num)
			{
				num2 = 0;
				num = num3;
				this.pathIndex = i + 1;
			}
			else if (num2 > 6)
			{
				break;
			}
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001FEA8 File Offset: 0x0001E2A8
	public IEnumerator WaitToRepath()
	{
		float timeLeft = this.repathRate - (Time.time - this.lastPathSearch);
		yield return new WaitForSeconds(timeLeft);
		this.Repath();
		yield break;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001FEC3 File Offset: 0x0001E2C3
	public void Stop()
	{
		this.canMove = false;
		this.canSearch = false;
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0001FED3 File Offset: 0x0001E2D3
	public void Resume()
	{
		this.canMove = true;
		this.canSearch = true;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0001FEE4 File Offset: 0x0001E2E4
	public virtual void Repath()
	{
		this.lastPathSearch = Time.time;
		if (this.seeker == null || this.target == null || !this.canSearch || !this.seeker.IsDone())
		{
			base.StartCoroutine(this.WaitToRepath());
			return;
		}
		Path p = ABPath.Construct(base.transform.position, this.target.position, null);
		this.seeker.StartPath(p, new OnPathDelegate(this.OnPathComplete), -1);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0001FF80 File Offset: 0x0001E380
	public void PathToTarget(Vector3 targetPoint)
	{
		this.lastPathSearch = Time.time;
		if (this.seeker == null)
		{
			return;
		}
		this.seeker.StartPath(base.transform.position, targetPoint, new OnPathDelegate(this.OnPathComplete));
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001FFCE File Offset: 0x0001E3CE
	public virtual void ReachedEndOfPath()
	{
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0001FFD0 File Offset: 0x0001E3D0
	public void Update()
	{
		if (this.path == null || this.pathIndex >= this.path.Length || this.pathIndex < 0 || !this.canMove)
		{
			return;
		}
		Vector3 a = this.path[this.pathIndex];
		a.y = this.tr.position.y;
		while ((a - this.tr.position).sqrMagnitude < this.pickNextWaypointDistance * this.pickNextWaypointDistance)
		{
			this.pathIndex++;
			if (this.pathIndex >= this.path.Length)
			{
				if ((a - this.tr.position).sqrMagnitude < this.pickNextWaypointDistance * this.targetReached * (this.pickNextWaypointDistance * this.targetReached))
				{
					this.ReachedEndOfPath();
					return;
				}
				this.pathIndex--;
				break;
			}
			else
			{
				a = this.path[this.pathIndex];
				a.y = this.tr.position.y;
			}
		}
		Vector3 forward = a - this.tr.position;
		this.tr.rotation = Quaternion.Slerp(this.tr.rotation, Quaternion.LookRotation(forward), this.rotationSpeed * Time.deltaTime);
		this.tr.eulerAngles = new Vector3(0f, this.tr.eulerAngles.y, 0f);
		Vector3 a2 = base.transform.forward;
		a2 *= this.speed;
		a2 *= Mathf.Clamp01(Vector3.Dot(forward.normalized, this.tr.forward));
		if (!(this.navmeshController != null))
		{
			if (this.controller != null)
			{
				this.controller.SimpleMove(a2);
			}
			else
			{
				base.transform.Translate(a2 * Time.deltaTime, Space.World);
			}
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0002021C File Offset: 0x0001E61C
	public void OnDrawGizmos()
	{
		if (!this.drawGizmos || this.path == null || this.pathIndex >= this.path.Length || this.pathIndex < 0)
		{
			return;
		}
		Vector3 vector = this.path[this.pathIndex];
		vector.y = this.tr.position.y;
		Debug.DrawLine(base.transform.position, vector, Color.blue);
		float num = this.pickNextWaypointDistance;
		if (this.pathIndex == this.path.Length - 1)
		{
			num *= this.targetReached;
		}
		Vector3 start = vector + num * new Vector3(1f, 0f, 0f);
		float num2 = 0f;
		while ((double)num2 < 6.2831853071795862)
		{
			Vector3 vector2 = vector + new Vector3((float)Math.Cos((double)num2) * num, 0f, (float)Math.Sin((double)num2) * num);
			Debug.DrawLine(start, vector2, Color.yellow);
			start = vector2;
			num2 += 0.1f;
		}
		Debug.DrawLine(start, vector + num * new Vector3(1f, 0f, 0f), Color.yellow);
	}

	// Token: 0x0400035D RID: 861
	public Transform target;

	// Token: 0x0400035E RID: 862
	public float repathRate = 0.1f;

	// Token: 0x0400035F RID: 863
	public float pickNextWaypointDistance = 1f;

	// Token: 0x04000360 RID: 864
	public float targetReached = 0.2f;

	// Token: 0x04000361 RID: 865
	public float speed = 5f;

	// Token: 0x04000362 RID: 866
	public float rotationSpeed = 1f;

	// Token: 0x04000363 RID: 867
	public bool drawGizmos;

	// Token: 0x04000364 RID: 868
	public bool canSearch = true;

	// Token: 0x04000365 RID: 869
	public bool canMove = true;

	// Token: 0x04000366 RID: 870
	protected Seeker seeker;

	// Token: 0x04000367 RID: 871
	protected CharacterController controller;

	// Token: 0x04000368 RID: 872
	protected NavmeshController navmeshController;

	// Token: 0x04000369 RID: 873
	protected Transform tr;

	// Token: 0x0400036A RID: 874
	protected float lastPathSearch = -9999f;

	// Token: 0x0400036B RID: 875
	protected int pathIndex;

	// Token: 0x0400036C RID: 876
	protected Vector3[] path;
}
