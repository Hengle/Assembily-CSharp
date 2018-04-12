using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000E7 RID: 231
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Controller")]
	public class RVOController : MonoBehaviour
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00049C5E File Offset: 0x0004805E
		public Vector3 position
		{
			get
			{
				return this.rvoAgent.InterpolatedPosition;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00049C6B File Offset: 0x0004806B
		public Vector3 velocity
		{
			get
			{
				return this.rvoAgent.Velocity;
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00049C78 File Offset: 0x00048078
		public void OnDisable()
		{
			if (this.simulator == null)
			{
				return;
			}
			this.simulator.RemoveAgent(this.rvoAgent);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00049C98 File Offset: 0x00048098
		public void Awake()
		{
			this.tr = base.transform;
			RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
			if (rvosimulator == null)
			{
				Debug.LogError("No RVOSimulator component found in the scene. Please add one.");
				return;
			}
			this.simulator = rvosimulator.GetSimulator();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00049CEC File Offset: 0x000480EC
		public void OnEnable()
		{
			if (this.simulator == null)
			{
				return;
			}
			if (this.rvoAgent != null)
			{
				this.simulator.AddAgent(this.rvoAgent);
			}
			else
			{
				this.rvoAgent = this.simulator.AddAgent(base.transform.position);
			}
			this.UpdateAgentProperties();
			this.rvoAgent.Teleport(base.transform.position);
			this.adjustedY = this.rvoAgent.Position.y;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00049D78 File Offset: 0x00048178
		protected void UpdateAgentProperties()
		{
			this.rvoAgent.Radius = this.radius;
			this.rvoAgent.MaxSpeed = this.maxSpeed;
			this.rvoAgent.Height = this.height;
			this.rvoAgent.AgentTimeHorizon = this.agentTimeHorizon;
			this.rvoAgent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			this.rvoAgent.Locked = this.locked;
			this.rvoAgent.MaxNeighbours = this.maxNeighbours;
			this.rvoAgent.DebugDraw = this.debug;
			this.rvoAgent.NeighbourDist = this.neighbourDist;
			this.rvoAgent.Layer = this.layer;
			this.rvoAgent.CollidesWith = this.collidesWith;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00049E40 File Offset: 0x00048240
		public void Move(Vector3 vel)
		{
			this.desiredVelocity = vel;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00049E49 File Offset: 0x00048249
		public void Teleport(Vector3 pos)
		{
			this.tr.position = pos;
			this.lastPosition = pos;
			this.rvoAgent.Teleport(pos);
			this.adjustedY = pos.y;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00049E78 File Offset: 0x00048278
		public void Update()
		{
			if (this.rvoAgent == null)
			{
				return;
			}
			if (this.lastPosition != this.tr.position)
			{
				this.Teleport(this.tr.position);
			}
			if (this.lockWhenNotMoving)
			{
				this.locked = (this.desiredVelocity == Vector3.zero);
			}
			this.UpdateAgentProperties();
			Vector3 interpolatedPosition = this.rvoAgent.InterpolatedPosition;
			interpolatedPosition.y = this.adjustedY;
			RaycastHit raycastHit;
			if (this.mask != 0 && Physics.Raycast(interpolatedPosition + Vector3.up * this.height * 0.5f, Vector3.down, out raycastHit, float.PositiveInfinity, this.mask))
			{
				this.adjustedY = raycastHit.point.y;
			}
			else
			{
				this.adjustedY = 0f;
			}
			interpolatedPosition.y = this.adjustedY;
			this.rvoAgent.SetYPosition(this.adjustedY);
			Vector3 a = Vector3.zero;
			if (this.wallAvoidFalloff > 0f && this.wallAvoidForce > 0f)
			{
				List<ObstacleVertex> neighbourObstacles = this.rvoAgent.NeighbourObstacles;
				if (neighbourObstacles != null)
				{
					for (int i = 0; i < neighbourObstacles.Count; i++)
					{
						Vector3 position = neighbourObstacles[i].position;
						Vector3 position2 = neighbourObstacles[i].next.position;
						Vector3 vector = this.position - AstarMath.NearestPointStrict(position, position2, this.position);
						if (!(vector == position) && !(vector == position2))
						{
							float sqrMagnitude = vector.sqrMagnitude;
							vector /= sqrMagnitude * this.wallAvoidFalloff;
							a += vector;
						}
					}
				}
			}
			this.rvoAgent.DesiredVelocity = this.desiredVelocity + a * this.wallAvoidForce;
			this.tr.position = interpolatedPosition + Vector3.up * this.height * 0.5f - this.center;
			this.lastPosition = this.tr.position;
			if (this.enableRotation && this.velocity != Vector3.zero)
			{
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(this.velocity), Time.deltaTime * this.rotationSpeed * Mathf.Min(this.velocity.magnitude, 0.2f));
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0004A144 File Offset: 0x00048544
		public void OnDrawGizmos()
		{
			Gizmos.color = RVOController.GizmoColor;
			Gizmos.DrawWireSphere(base.transform.position + this.center - Vector3.up * this.height * 0.5f + Vector3.up * this.radius * 0.5f, this.radius);
			Gizmos.DrawLine(base.transform.position + this.center - Vector3.up * this.height * 0.5f, base.transform.position + this.center + Vector3.up * this.height * 0.5f);
			Gizmos.DrawWireSphere(base.transform.position + this.center + Vector3.up * this.height * 0.5f - Vector3.up * this.radius * 0.5f, this.radius);
		}

		// Token: 0x04000656 RID: 1622
		[Tooltip("Radius of the agent")]
		public float radius = 5f;

		// Token: 0x04000657 RID: 1623
		[Tooltip("Max speed of the agent. In world units/second")]
		public float maxSpeed = 2f;

		// Token: 0x04000658 RID: 1624
		[Tooltip("Height of the agent. In world units")]
		public float height = 1f;

		// Token: 0x04000659 RID: 1625
		[Tooltip("A locked unit cannot move. Other units will still avoid it. But avoidance quailty is not the best")]
		public bool locked;

		// Token: 0x0400065A RID: 1626
		[Tooltip("Automatically set #locked to true when desired velocity is approximately zero")]
		public bool lockWhenNotMoving = true;

		// Token: 0x0400065B RID: 1627
		[Tooltip("How far in the time to look for collisions with other agents")]
		public float agentTimeHorizon = 2f;

		// Token: 0x0400065C RID: 1628
		[HideInInspector]
		public float obstacleTimeHorizon = 2f;

		// Token: 0x0400065D RID: 1629
		[Tooltip("Maximum distance to other agents to take them into account for collisions.\nDecreasing this value can lead to better performance, increasing it can lead to better quality of the simulation")]
		public float neighbourDist = 10f;

		// Token: 0x0400065E RID: 1630
		[Tooltip("Max number of other agents to take into account.\nA smaller value can reduce CPU load, a higher value can lead to better local avoidance quality.")]
		public int maxNeighbours = 10;

		// Token: 0x0400065F RID: 1631
		[Tooltip("Layer mask for the ground. The RVOController will raycast down to check for the ground to figure out where to place the agent")]
		public LayerMask mask = -1;

		// Token: 0x04000660 RID: 1632
		public RVOLayer layer = RVOLayer.DefaultAgent;

		// Token: 0x04000661 RID: 1633
		[AstarEnumFlag]
		public RVOLayer collidesWith = (RVOLayer)(-1);

		// Token: 0x04000662 RID: 1634
		[HideInInspector]
		public float wallAvoidForce = 1f;

		// Token: 0x04000663 RID: 1635
		[HideInInspector]
		public float wallAvoidFalloff = 1f;

		// Token: 0x04000664 RID: 1636
		[Tooltip("Center of the agent relative to the pivot point of this game object")]
		public Vector3 center;

		// Token: 0x04000665 RID: 1637
		private IAgent rvoAgent;

		// Token: 0x04000666 RID: 1638
		public bool enableRotation = true;

		// Token: 0x04000667 RID: 1639
		public float rotationSpeed = 30f;

		// Token: 0x04000668 RID: 1640
		private Simulator simulator;

		// Token: 0x04000669 RID: 1641
		private float adjustedY;

		// Token: 0x0400066A RID: 1642
		private Transform tr;

		// Token: 0x0400066B RID: 1643
		private Vector3 desiredVelocity;

		// Token: 0x0400066C RID: 1644
		public bool debug;

		// Token: 0x0400066D RID: 1645
		private Vector3 lastPosition;

		// Token: 0x0400066E RID: 1646
		private static readonly Color GizmoColor = new Color(0.9411765f, 0.8352941f, 0.117647059f);
	}
}
