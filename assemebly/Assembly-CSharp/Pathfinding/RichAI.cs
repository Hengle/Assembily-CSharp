using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000004 RID: 4
	[RequireComponent(typeof(Seeker))]
	[AddComponentMenu("Pathfinding/AI/RichAI (for navmesh)")]
	public class RichAI : MonoBehaviour
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002F28 File Offset: 0x00001328
		public Vector3 Velocity
		{
			get
			{
				return this.velocity;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F30 File Offset: 0x00001330
		private void Awake()
		{
			this.seeker = base.GetComponent<Seeker>();
			this.controller = base.GetComponent<CharacterController>();
			this.rvoController = base.GetComponent<RVOController>();
			if (this.rvoController != null)
			{
				this.rvoController.enableRotation = false;
			}
			this.tr = base.transform;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002F8A File Offset: 0x0000138A
		protected virtual void Start()
		{
			this.startHasRun = true;
			this.OnEnable();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002F9C File Offset: 0x0000139C
		protected virtual void OnEnable()
		{
			this.lastRepath = -9999f;
			this.waitingForPathCalc = false;
			this.canSearchPath = true;
			if (this.startHasRun)
			{
				Seeker seeker = this.seeker;
				seeker.pathCallback = (OnPathDelegate)Delegate.Combine(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
				base.StartCoroutine(this.SearchPaths());
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003004 File Offset: 0x00001404
		public void OnDisable()
		{
			if (this.seeker != null && !this.seeker.IsDone())
			{
				this.seeker.GetCurrentPath().Error();
			}
			Seeker seeker = this.seeker;
			seeker.pathCallback = (OnPathDelegate)Delegate.Remove(seeker.pathCallback, new OnPathDelegate(this.OnPathComplete));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000306C File Offset: 0x0000146C
		public virtual void UpdatePath()
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
			if (this.target == null)
			{
				return;
			}
			this.waitingForPathCalc = true;
			this.lastRepath = Time.time;
			this.seeker.StartPath(this.tr.position, this.target.position);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003104 File Offset: 0x00001504
		private IEnumerator SearchPaths()
		{
			for (;;)
			{
				while (!this.repeatedlySearchPaths || this.waitingForPathCalc || !this.canSearchPath || Time.time - this.lastRepath < this.repathRate)
				{
					yield return null;
				}
				this.UpdatePath();
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003120 File Offset: 0x00001520
		private void OnPathComplete(Path p)
		{
			this.waitingForPathCalc = false;
			p.Claim(this);
			if (p.error)
			{
				p.Release(this);
				return;
			}
			if (this.traversingSpecialPath)
			{
				this.delayUpdatePath = true;
			}
			else
			{
				if (this.rp == null)
				{
					this.rp = new RichPath();
				}
				this.rp.Initialize(this.seeker, p, true, this.funnelSimplification);
			}
			p.Release(this);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000319B File Offset: 0x0000159B
		public bool TraversingSpecial
		{
			get
			{
				return this.traversingSpecialPath;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000031A3 File Offset: 0x000015A3
		public Vector3 TargetPoint
		{
			get
			{
				return this.lastTargetPoint;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000031AB File Offset: 0x000015AB
		public bool ApproachingPartEndpoint
		{
			get
			{
				return this.lastCorner;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000031B3 File Offset: 0x000015B3
		public bool ApproachingPathEndpoint
		{
			get
			{
				return this.rp != null && this.ApproachingPartEndpoint && !this.rp.PartsLeft();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000031E2 File Offset: 0x000015E2
		public float DistanceToNextWaypoint
		{
			get
			{
				return this.distanceToWaypoint;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000031EA File Offset: 0x000015EA
		private void NextPart()
		{
			this.rp.NextPart();
			this.lastCorner = false;
			if (!this.rp.PartsLeft())
			{
				this.OnTargetReached();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003214 File Offset: 0x00001614
		protected virtual void OnTargetReached()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003218 File Offset: 0x00001618
		protected virtual Vector3 UpdateTarget(RichFunnel fn)
		{
			this.buffer.Clear();
			Vector3 vector = this.tr.position;
			bool flag;
			vector = fn.Update(vector, this.buffer, 2, out this.lastCorner, out flag);
			if (flag && !this.waitingForPathCalc)
			{
				this.UpdatePath();
			}
			return vector;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000326C File Offset: 0x0000166C
		protected virtual void Update()
		{
			RichAI.deltaTime = Mathf.Min(Time.smoothDeltaTime * 2f, Time.deltaTime);
			if (this.rp != null)
			{
				RichPathPart currentPart = this.rp.GetCurrentPart();
				RichFunnel richFunnel = currentPart as RichFunnel;
				if (richFunnel != null)
				{
					Vector3 vector = this.UpdateTarget(richFunnel);
					if (Time.frameCount % 5 == 0)
					{
						this.wallBuffer.Clear();
						richFunnel.FindWalls(this.wallBuffer, this.wallDist);
					}
					int num = 0;
					Vector3 vector2 = this.buffer[num];
					Vector3 vector3 = vector2 - vector;
					vector3.y = 0f;
					bool flag = Vector3.Dot(vector3, this.currentTargetDirection) < 0f;
					if (flag && this.buffer.Count - num > 1)
					{
						num++;
						vector2 = this.buffer[num];
					}
					if (vector2 != this.lastTargetPoint)
					{
						this.currentTargetDirection = vector2 - vector;
						this.currentTargetDirection.y = 0f;
						this.currentTargetDirection.Normalize();
						this.lastTargetPoint = vector2;
					}
					vector3 = vector2 - vector;
					vector3.y = 0f;
					float magnitude = vector3.magnitude;
					this.distanceToWaypoint = magnitude;
					vector3 = ((magnitude != 0f) ? (vector3 / magnitude) : Vector3.zero);
					Vector3 lhs = vector3;
					Vector3 a = Vector3.zero;
					if (this.wallForce > 0f && this.wallDist > 0f)
					{
						float num2 = 0f;
						float num3 = 0f;
						for (int i = 0; i < this.wallBuffer.Count; i += 2)
						{
							Vector3 a2 = AstarMath.NearestPointStrict(this.wallBuffer[i], this.wallBuffer[i + 1], this.tr.position);
							float sqrMagnitude = (a2 - vector).sqrMagnitude;
							if (sqrMagnitude <= this.wallDist * this.wallDist)
							{
								Vector3 normalized = (this.wallBuffer[i + 1] - this.wallBuffer[i]).normalized;
								float num4 = Vector3.Dot(vector3, normalized) * (1f - Math.Max(0f, 2f * (sqrMagnitude / (this.wallDist * this.wallDist)) - 1f));
								if (num4 > 0f)
								{
									num3 = Math.Max(num3, num4);
								}
								else
								{
									num2 = Math.Max(num2, -num4);
								}
							}
						}
						Vector3 a3 = Vector3.Cross(Vector3.up, vector3);
						a = a3 * (num3 - num2);
					}
					bool flag2 = this.lastCorner && this.buffer.Count - num == 1;
					if (flag2)
					{
						if (this.slowdownTime < 0.001f)
						{
							this.slowdownTime = 0.001f;
						}
						Vector3 a4 = vector2 - vector;
						a4.y = 0f;
						if (this.preciseSlowdown)
						{
							vector3 = (6f * a4 - 4f * this.slowdownTime * this.velocity) / (this.slowdownTime * this.slowdownTime);
						}
						else
						{
							vector3 = 2f * (a4 - this.slowdownTime * this.velocity) / (this.slowdownTime * this.slowdownTime);
						}
						vector3 = Vector3.ClampMagnitude(vector3, this.acceleration);
						a *= Math.Min(magnitude / 0.5f, 1f);
						if (magnitude < this.endReachedDistance)
						{
							this.NextPart();
						}
					}
					else
					{
						vector3 *= this.acceleration;
					}
					this.velocity += (vector3 + a * this.wallForce) * RichAI.deltaTime;
					if (this.slowWhenNotFacingTarget)
					{
						float a5 = (Vector3.Dot(lhs, this.tr.forward) + 0.5f) * 0.6666667f;
						float a6 = Mathf.Sqrt(this.velocity.x * this.velocity.x + this.velocity.z * this.velocity.z);
						float y = this.velocity.y;
						this.velocity.y = 0f;
						float d = Mathf.Min(a6, this.maxSpeed * Mathf.Max(a5, 0.2f));
						this.velocity = Vector3.Lerp(this.tr.forward * d, this.velocity.normalized * d, Mathf.Clamp((!flag2) ? 0f : (magnitude * 2f), 0.5f, 1f));
						this.velocity.y = y;
					}
					else
					{
						float num5 = Mathf.Sqrt(this.velocity.x * this.velocity.x + this.velocity.z * this.velocity.z);
						num5 = this.maxSpeed / num5;
						if (num5 < 1f)
						{
							this.velocity.x = this.velocity.x * num5;
							this.velocity.z = this.velocity.z * num5;
						}
					}
					if (flag2)
					{
						Vector3 trotdir = Vector3.Lerp(this.velocity, this.currentTargetDirection, Math.Max(1f - magnitude * 2f, 0f));
						this.RotateTowards(trotdir);
					}
					else
					{
						this.RotateTowards(this.velocity);
					}
					this.velocity += RichAI.deltaTime * this.gravity;
					if (this.rvoController != null && this.rvoController.enabled)
					{
						this.tr.position = vector;
						this.rvoController.Move(this.velocity);
					}
					else if (this.controller != null && this.controller.enabled)
					{
						this.tr.position = vector;
						this.controller.Move(this.velocity * RichAI.deltaTime);
					}
					else
					{
						float y2 = vector.y;
						vector += this.velocity * RichAI.deltaTime;
						vector = this.RaycastPosition(vector, y2);
						this.tr.position = vector;
					}
				}
				else if (this.rvoController != null && this.rvoController.enabled)
				{
					this.rvoController.Move(Vector3.zero);
				}
				if (currentPart is RichSpecial)
				{
					RichSpecial rs = currentPart as RichSpecial;
					if (!this.traversingSpecialPath)
					{
						base.StartCoroutine(this.TraverseSpecial(rs));
					}
				}
			}
			else if (this.rvoController != null && this.rvoController.enabled)
			{
				this.rvoController.Move(Vector3.zero);
			}
			else if (!(this.controller != null) || !this.controller.enabled)
			{
				this.tr.position = this.RaycastPosition(this.tr.position, this.tr.position.y);
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003A34 File Offset: 0x00001E34
		private Vector3 RaycastPosition(Vector3 position, float lasty)
		{
			if (this.raycastingForGroundPlacement)
			{
				float num = Mathf.Max(this.centerOffset, lasty - position.y + this.centerOffset);
				RaycastHit raycastHit;
				if (Physics.Raycast(position + Vector3.up * num, Vector3.down, out raycastHit, num, this.groundMask))
				{
					if (raycastHit.distance < num)
					{
						position = raycastHit.point;
						this.velocity.y = 0f;
					}
				}
			}
			return position;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003AC4 File Offset: 0x00001EC4
		private bool RotateTowards(Vector3 trotdir)
		{
			Quaternion rotation = this.tr.rotation;
			trotdir.y = 0f;
			if (trotdir != Vector3.zero)
			{
				Vector3 eulerAngles = Quaternion.LookRotation(trotdir).eulerAngles;
				Vector3 eulerAngles2 = rotation.eulerAngles;
				eulerAngles2.y = Mathf.MoveTowardsAngle(eulerAngles2.y, eulerAngles.y, this.rotationSpeed * RichAI.deltaTime);
				this.tr.rotation = Quaternion.Euler(eulerAngles2);
				return Mathf.Abs(eulerAngles2.y - eulerAngles.y) < 5f;
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003B64 File Offset: 0x00001F64
		public void OnDrawGizmos()
		{
			if (this.drawGizmos)
			{
				if (this.raycastingForGroundPlacement)
				{
					Gizmos.color = RichAI.GizmoColorRaycast;
					Gizmos.DrawLine(base.transform.position, base.transform.position + Vector3.up * this.centerOffset);
					Gizmos.DrawLine(base.transform.position + Vector3.left * 0.1f, base.transform.position + Vector3.right * 0.1f);
					Gizmos.DrawLine(base.transform.position + Vector3.back * 0.1f, base.transform.position + Vector3.forward * 0.1f);
				}
				if (this.tr != null && this.buffer != null)
				{
					Gizmos.color = RichAI.GizmoColorPath;
					Vector3 from = this.tr.position;
					for (int i = 0; i < this.buffer.Count; i++)
					{
						Gizmos.DrawLine(from, this.buffer[i]);
						from = this.buffer[i];
					}
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003CB4 File Offset: 0x000020B4
		private IEnumerator TraverseSpecial(RichSpecial rs)
		{
			this.traversingSpecialPath = true;
			this.velocity = Vector3.zero;
			AnimationLink al = rs.nodeLink as AnimationLink;
			if (al == null)
			{
				Debug.LogError("Unhandled RichSpecial");
				yield break;
			}
			while (!this.RotateTowards(rs.first.forward))
			{
				yield return null;
			}
			this.tr.parent.position = this.tr.position;
			this.tr.parent.rotation = this.tr.rotation;
			this.tr.localPosition = Vector3.zero;
			this.tr.localRotation = Quaternion.identity;
			if (rs.reverse && al.reverseAnim)
			{
				this.anim[al.clip].speed = -al.animSpeed;
				this.anim[al.clip].normalizedTime = 1f;
				this.anim.Play(al.clip);
				this.anim.Sample();
			}
			else
			{
				this.anim[al.clip].speed = al.animSpeed;
				this.anim.Rewind(al.clip);
				this.anim.Play(al.clip);
			}
			this.tr.parent.position -= this.tr.position - this.tr.parent.position;
			yield return new WaitForSeconds(Mathf.Abs(this.anim[al.clip].length / al.animSpeed));
			this.traversingSpecialPath = false;
			this.NextPart();
			if (this.delayUpdatePath)
			{
				this.delayUpdatePath = false;
				this.UpdatePath();
			}
			yield break;
		}

		// Token: 0x04000025 RID: 37
		public Transform target;

		// Token: 0x04000026 RID: 38
		public bool drawGizmos = true;

		// Token: 0x04000027 RID: 39
		public bool repeatedlySearchPaths;

		// Token: 0x04000028 RID: 40
		public float repathRate = 0.5f;

		// Token: 0x04000029 RID: 41
		public float maxSpeed = 1f;

		// Token: 0x0400002A RID: 42
		public float acceleration = 5f;

		// Token: 0x0400002B RID: 43
		public float slowdownTime = 0.5f;

		// Token: 0x0400002C RID: 44
		public float rotationSpeed = 360f;

		// Token: 0x0400002D RID: 45
		public float endReachedDistance = 0.01f;

		// Token: 0x0400002E RID: 46
		public float wallForce = 3f;

		// Token: 0x0400002F RID: 47
		public float wallDist = 1f;

		// Token: 0x04000030 RID: 48
		public Vector3 gravity = new Vector3(0f, -9.82f, 0f);

		// Token: 0x04000031 RID: 49
		public bool raycastingForGroundPlacement = true;

		// Token: 0x04000032 RID: 50
		public LayerMask groundMask = -1;

		// Token: 0x04000033 RID: 51
		public float centerOffset = 1f;

		// Token: 0x04000034 RID: 52
		public RichFunnel.FunnelSimplification funnelSimplification;

		// Token: 0x04000035 RID: 53
		public Animation anim;

		// Token: 0x04000036 RID: 54
		public bool preciseSlowdown = true;

		// Token: 0x04000037 RID: 55
		public bool slowWhenNotFacingTarget = true;

		// Token: 0x04000038 RID: 56
		private Vector3 velocity;

		// Token: 0x04000039 RID: 57
		protected RichPath rp;

		// Token: 0x0400003A RID: 58
		protected Seeker seeker;

		// Token: 0x0400003B RID: 59
		protected Transform tr;

		// Token: 0x0400003C RID: 60
		private CharacterController controller;

		// Token: 0x0400003D RID: 61
		private RVOController rvoController;

		// Token: 0x0400003E RID: 62
		private Vector3 lastTargetPoint;

		// Token: 0x0400003F RID: 63
		private Vector3 currentTargetDirection;

		// Token: 0x04000040 RID: 64
		protected bool waitingForPathCalc;

		// Token: 0x04000041 RID: 65
		protected bool canSearchPath;

		// Token: 0x04000042 RID: 66
		protected bool delayUpdatePath;

		// Token: 0x04000043 RID: 67
		protected bool traversingSpecialPath;

		// Token: 0x04000044 RID: 68
		protected bool lastCorner;

		// Token: 0x04000045 RID: 69
		private float distanceToWaypoint = 999f;

		// Token: 0x04000046 RID: 70
		protected List<Vector3> buffer = new List<Vector3>();

		// Token: 0x04000047 RID: 71
		protected List<Vector3> wallBuffer = new List<Vector3>();

		// Token: 0x04000048 RID: 72
		private bool startHasRun;

		// Token: 0x04000049 RID: 73
		protected float lastRepath = -9999f;

		// Token: 0x0400004A RID: 74
		private static float deltaTime = 0f;

		// Token: 0x0400004B RID: 75
		public static readonly Color GizmoColorRaycast = new Color(0.4627451f, 0.807843149f, 0.4392157f);

		// Token: 0x0400004C RID: 76
		public static readonly Color GizmoColorPath = new Color(0.03137255f, 0.305882365f, 0.7607843f);
	}
}
