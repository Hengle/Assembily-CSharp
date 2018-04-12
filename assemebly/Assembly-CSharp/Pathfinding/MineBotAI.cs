using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200007D RID: 125
	[RequireComponent(typeof(Seeker))]
	public class MineBotAI : AIPath
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00020BA8 File Offset: 0x0001EFA8
		public new void Start()
		{
			this.anim["forward"].layer = 10;
			this.anim.Play("awake");
			this.anim.Play("forward");
			this.anim["awake"].wrapMode = WrapMode.Once;
			this.anim["awake"].speed = 0f;
			this.anim["awake"].normalizedTime = 1f;
			base.Start();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00020C40 File Offset: 0x0001F040
		public override void OnTargetReached()
		{
			if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
				this.lastTarget = this.tr.position;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00020CB1 File Offset: 0x0001F0B1
		public override Vector3 GetFeetPosition()
		{
			return this.tr.position;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00020CC0 File Offset: 0x0001F0C0
		protected void Update()
		{
			Vector3 direction;
			if (this.canMove)
			{
				Vector3 vector = base.CalculateVelocity(this.GetFeetPosition());
				this.RotateTowards(this.targetDirection);
				vector.y = 0f;
				if (vector.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
				{
					vector = Vector3.zero;
				}
				if (this.rvoController != null)
				{
					this.rvoController.Move(vector);
					direction = this.rvoController.velocity;
				}
				else if (this.navController != null)
				{
					direction = Vector3.zero;
				}
				else if (this.controller != null)
				{
					this.controller.SimpleMove(vector);
					direction = this.controller.velocity;
				}
				else
				{
					Debug.LogWarning("No NavmeshController or CharacterController attached to GameObject");
					direction = Vector3.zero;
				}
			}
			else
			{
				direction = Vector3.zero;
			}
			Vector3 vector2 = this.tr.InverseTransformDirection(direction);
			vector2.y = 0f;
			if (direction.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
			{
				this.anim.Blend("forward", 0f, 0.2f);
			}
			else
			{
				this.anim.Blend("forward", 1f, 0.2f);
				AnimationState animationState = this.anim["forward"];
				float z = vector2.z;
				animationState.speed = z * this.animationSpeed;
			}
		}

		// Token: 0x04000384 RID: 900
		public Animation anim;

		// Token: 0x04000385 RID: 901
		public float sleepVelocity = 0.4f;

		// Token: 0x04000386 RID: 902
		public float animationSpeed = 0.2f;

		// Token: 0x04000387 RID: 903
		public GameObject endOfPathEffect;

		// Token: 0x04000388 RID: 904
		protected Vector3 lastTarget;
	}
}
