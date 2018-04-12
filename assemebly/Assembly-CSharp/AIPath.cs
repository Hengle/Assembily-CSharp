using System;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x02000002 RID: 2
[RequireComponent(typeof(Seeker))]
[AddComponentMenu("Pathfinding/AI/AIPath (generic)")]
public class AIPath : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x000020FB File Offset: 0x000004FB
	public bool TargetReached
	{
		get
		{
			return this.targetReached;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000504
	protected virtual void Awake()
	{
		this.seeker = base.GetComponent<Seeker>();
		this.tr = base.transform;
		this.controller = base.GetComponent<CharacterController>();
		this.navController = base.GetComponent<NavmeshController>();
		this.rvoController = base.GetComponent<RVOController>();
		if (this.rvoController != null)
		{
			this.rvoController.enableRotation = false;
		}
		this.rigid = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002176 File Offset: 0x00000576
	protected virtual void Start()
	{
		this.startHasRun = true;
		this.OnEnable();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002188 File Offset: 0x00000588
	protected virtual void OnEnable()
	{
		this.lastRepath = -9999f;
		this.canSearchAgain = true;
		this.lastFoundWaypointPosition = this.GetFeetPosition();
		if (this.startHasRun)
		{
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000021E8 File Offset: 0x000005E8
	public void OnDisable()
	{
		if (this.seeker != null && !this.seeker.IsDone())
		{
			this.seeker.GetCurrentPath().Error();
		}
		if (this.path != null)
		{
			this.path.Release(this);
		}
		this.path = null;
		Seeker seeker = this.seeker;
		seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000226C File Offset: 0x0000066C
	public virtual void SearchPath()
	{
		if (this.target == null)
		{
			throw new InvalidOperationException("Target is null");
		}
		this.lastRepath = Time.time;
		Vector3 position = this.target.position;
		this.canSearchAgain = false;
		this.seeker.StartPath(this.GetFeetPosition(), position);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000022C6 File Offset: 0x000006C6
	public virtual void OnTargetReached()
	{
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000022C8 File Offset: 0x000006C8
	public virtual void OnPathComplete(Path _p)
	{
		ABPath abpath = _p as ABPath;
		if (abpath == null)
		{
			throw new Exception("This function only handles ABPaths, do not use special path types");
		}
		this.canSearchAgain = true;
		abpath.Claim(this);
		if (abpath.error)
		{
			abpath.Release(this);
			return;
		}
		if (this.path != null)
		{
			this.path.Release(this);
		}
		this.path = abpath;
		this.currentWaypointIndex = 0;
		this.targetReached = false;
		if (this.closestOnPathCheck)
		{
			Vector3 vector = (Time.time - this.lastFoundWaypointTime >= 0.3f) ? abpath.originalStartPoint : this.lastFoundWaypointPosition;
			Vector3 feetPosition = this.GetFeetPosition();
			Vector3 vector2 = feetPosition - vector;
			float magnitude = vector2.magnitude;
			vector2 /= magnitude;
			int num = (int)(magnitude / this.pickNextWaypointDist);
			for (int i = 0; i <= num; i++)
			{
				this.CalculateVelocity(vector);
				vector += vector2;
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000023C1 File Offset: 0x000007C1
	public void stop()
	{
		this.path = null;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000023CC File Offset: 0x000007CC
	public virtual Vector3 GetFeetPosition()
	{
		if (this.rvoController != null)
		{
			return this.tr.position - Vector3.up * this.rvoController.height * 0.5f;
		}
		if (this.controller != null)
		{
			return this.tr.position;
		}
		return this.tr.position;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002444 File Offset: 0x00000844
	public void move(float delta)
	{
		if (!this.canMove)
		{
			return;
		}
		if (Time.time - this.lastRepath >= this.repathRate && this.canSearchAgain && this.canSearch && this.target != null)
		{
			this.SearchPath();
		}
		if (this.path == null)
		{
			return;
		}
		Vector3 vector = this.CalculateVelocity(base.transform.position);
		vector.y = Physics.gravity.y * 2f;
		this.RotateTowards(this.targetDirection);
		if (this.rvoController != null)
		{
			this.rvoController.Move(vector);
		}
		else if (!(this.navController != null))
		{
			if (this.controller != null && this.controller.enabled)
			{
				this.controller.Move(vector * delta);
			}
			else if (this.rigid != null)
			{
				this.rigid.AddForce(vector);
			}
			else
			{
				base.transform.Translate(vector * delta, Space.World);
			}
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000258C File Offset: 0x0000098C
	protected float XZSqrMagnitude(Vector3 a, Vector3 b)
	{
		float num = b.x - a.x;
		float num2 = b.z - a.z;
		return num * num + num2 * num2;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000025C0 File Offset: 0x000009C0
	protected Vector3 CalculateVelocity(Vector3 currentPosition)
	{
		if (this.path == null || this.path.vectorPath == null || this.path.vectorPath.Count == 0)
		{
			return Vector3.zero;
		}
		List<Vector3> vectorPath = this.path.vectorPath;
		if (vectorPath.Count == 1)
		{
			vectorPath.Insert(0, currentPosition);
		}
		if (this.currentWaypointIndex >= vectorPath.Count)
		{
			this.currentWaypointIndex = vectorPath.Count - 1;
		}
		if (this.currentWaypointIndex <= 1)
		{
			this.currentWaypointIndex = 1;
		}
		while (this.currentWaypointIndex < vectorPath.Count - 1)
		{
			float num = this.XZSqrMagnitude(vectorPath[this.currentWaypointIndex], currentPosition);
			if (num < this.pickNextWaypointDist * this.pickNextWaypointDist)
			{
				this.lastFoundWaypointPosition = currentPosition;
				this.lastFoundWaypointTime = Time.time;
				this.currentWaypointIndex++;
			}
			else
			{
				IL_FB:
				Vector3 vector = vectorPath[this.currentWaypointIndex] - vectorPath[this.currentWaypointIndex - 1];
				Vector3 a = this.CalculateTargetPoint(currentPosition, vectorPath[this.currentWaypointIndex - 1], vectorPath[this.currentWaypointIndex], this.currentWaypointIndex == vectorPath.Count - 1);
				vector = a - currentPosition;
				vector.y = 0f;
				float magnitude = vector.magnitude;
				float num2 = Mathf.Clamp01(magnitude / this.slowdownDistance);
				if (this.canTurn)
				{
					this.targetDirection = vector;
				}
				this.targetPoint = a;
				if (this.currentWaypointIndex == vectorPath.Count - 1 && magnitude <= this.endReachedDistance)
				{
					if (!this.targetReached)
					{
						this.targetReached = true;
						this.OnTargetReached();
					}
					return Vector3.zero;
				}
				Vector3 forward = this.tr.forward;
				float a2 = Vector3.Dot(vector.normalized, forward);
				float num3 = this.speed * Mathf.Max(a2, this.minMoveScale) * num2;
				if (Time.deltaTime > 0f)
				{
					num3 = Mathf.Clamp(num3, 0f, magnitude / (Time.deltaTime * 2f));
				}
				return forward * num3;
			}
		}
		goto IL_FB;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002808 File Offset: 0x00000C08
	protected virtual void RotateTowards(Vector3 dir)
	{
		if (dir == Vector3.zero)
		{
			return;
		}
		Quaternion quaternion = this.tr.rotation;
		Quaternion b = Quaternion.LookRotation(dir);
		Vector3 eulerAngles = Quaternion.Slerp(quaternion, b, this.turningSpeed * Time.deltaTime).eulerAngles;
		eulerAngles.z = 0f;
		eulerAngles.x = 0f;
		quaternion = Quaternion.Euler(eulerAngles);
		this.tr.rotation = quaternion;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002880 File Offset: 0x00000C80
	protected Vector3 CalculateTargetPoint(Vector3 p, Vector3 a, Vector3 b, bool canGoDirectly)
	{
		if (canGoDirectly && (b - this.target.position).sqrMagnitude < 16f)
		{
			return this.target.position;
		}
		a.y = p.y;
		b.y = p.y;
		float magnitude = (a - b).magnitude;
		if (magnitude == 0f)
		{
			return a;
		}
		float num = AstarMath.Clamp01(AstarMath.NearestPointFactor(a, b, p));
		Vector3 a2 = (b - a) * num + a;
		float magnitude2 = (a2 - p).magnitude;
		float num2 = Mathf.Clamp(this.forwardLook - magnitude2, 0f, this.forwardLook);
		float num3 = num2 / magnitude;
		num3 = Mathf.Clamp(num3 + num, 0f, 1f);
		return (b - a) * num3 + a;
	}

	// Token: 0x04000001 RID: 1
	public float repathRate = 0.5f;

	// Token: 0x04000002 RID: 2
	public Transform target;

	// Token: 0x04000003 RID: 3
	public bool canSearch = true;

	// Token: 0x04000004 RID: 4
	public bool canMove = true;

	// Token: 0x04000005 RID: 5
	public bool canTurn = true;

	// Token: 0x04000006 RID: 6
	public bool canSmooth = true;

	// Token: 0x04000007 RID: 7
	public float speed = 3f;

	// Token: 0x04000008 RID: 8
	public float turningSpeed = 5f;

	// Token: 0x04000009 RID: 9
	public float slowdownDistance = 0.6f;

	// Token: 0x0400000A RID: 10
	public float pickNextWaypointDist = 2f;

	// Token: 0x0400000B RID: 11
	public float forwardLook = 1f;

	// Token: 0x0400000C RID: 12
	public float endReachedDistance = 0.2f;

	// Token: 0x0400000D RID: 13
	public bool closestOnPathCheck = true;

	// Token: 0x0400000E RID: 14
	protected float minMoveScale = 0.05f;

	// Token: 0x0400000F RID: 15
	protected Seeker seeker;

	// Token: 0x04000010 RID: 16
	protected Transform tr;

	// Token: 0x04000011 RID: 17
	protected float lastRepath = -9999f;

	// Token: 0x04000012 RID: 18
	protected Path path;

	// Token: 0x04000013 RID: 19
	protected CharacterController controller;

	// Token: 0x04000014 RID: 20
	protected NavmeshController navController;

	// Token: 0x04000015 RID: 21
	protected RVOController rvoController;

	// Token: 0x04000016 RID: 22
	protected Rigidbody rigid;

	// Token: 0x04000017 RID: 23
	protected int currentWaypointIndex;

	// Token: 0x04000018 RID: 24
	protected bool targetReached;

	// Token: 0x04000019 RID: 25
	protected bool canSearchAgain = true;

	// Token: 0x0400001A RID: 26
	protected Vector3 lastFoundWaypointPosition;

	// Token: 0x0400001B RID: 27
	protected float lastFoundWaypointTime = -9999f;

	// Token: 0x0400001C RID: 28
	private bool startHasRun;

	// Token: 0x0400001D RID: 29
	protected Vector3 targetPoint;

	// Token: 0x0400001E RID: 30
	public Vector3 targetDirection;
}
