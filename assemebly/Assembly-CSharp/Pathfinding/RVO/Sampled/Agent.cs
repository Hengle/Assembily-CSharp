using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding.RVO.Sampled
{
	// Token: 0x02000038 RID: 56
	public class Agent : IAgent
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00014BA4 File Offset: 0x00012FA4
		public Agent(Vector3 pos)
		{
			this.MaxSpeed = 2f;
			this.NeighbourDist = 15f;
			this.AgentTimeHorizon = 2f;
			this.ObstacleTimeHorizon = 2f;
			this.Height = 5f;
			this.Radius = 5f;
			this.MaxNeighbours = 10;
			this.Locked = false;
			this.position = pos;
			this.Position = this.position;
			this.prevSmoothPos = this.position;
			this.smoothPos = this.position;
			this.Layer = RVOLayer.DefaultAgent;
			this.CollidesWith = (RVOLayer)(-1);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00014C78 File Offset: 0x00013078
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00014C80 File Offset: 0x00013080
		public Vector3 Position { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00014C89 File Offset: 0x00013089
		public Vector3 InterpolatedPosition
		{
			get
			{
				return this.smoothPos;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00014C91 File Offset: 0x00013091
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00014C99 File Offset: 0x00013099
		public Vector3 DesiredVelocity { get; set; }

		// Token: 0x0600028A RID: 650 RVA: 0x00014CA2 File Offset: 0x000130A2
		public void Teleport(Vector3 pos)
		{
			this.Position = pos;
			this.smoothPos = pos;
			this.prevSmoothPos = pos;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00014CBC File Offset: 0x000130BC
		public void SetYPosition(float yCoordinate)
		{
			this.Position = new Vector3(this.Position.x, yCoordinate, this.Position.z);
			this.smoothPos.y = yCoordinate;
			this.prevSmoothPos.y = yCoordinate;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00014D09 File Offset: 0x00013109
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00014D11 File Offset: 0x00013111
		public RVOLayer Layer { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00014D1A File Offset: 0x0001311A
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00014D22 File Offset: 0x00013122
		public RVOLayer CollidesWith { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00014D2B File Offset: 0x0001312B
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00014D33 File Offset: 0x00013133
		public bool Locked { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00014D3C File Offset: 0x0001313C
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00014D44 File Offset: 0x00013144
		public float Radius { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00014D4D File Offset: 0x0001314D
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00014D55 File Offset: 0x00013155
		public float Height { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00014D5E File Offset: 0x0001315E
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00014D66 File Offset: 0x00013166
		public float MaxSpeed { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00014D6F File Offset: 0x0001316F
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00014D77 File Offset: 0x00013177
		public float NeighbourDist { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00014D80 File Offset: 0x00013180
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00014D88 File Offset: 0x00013188
		public float AgentTimeHorizon { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00014D91 File Offset: 0x00013191
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00014D99 File Offset: 0x00013199
		public float ObstacleTimeHorizon { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00014DA2 File Offset: 0x000131A2
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00014DAA File Offset: 0x000131AA
		public Vector3 Velocity { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00014DB3 File Offset: 0x000131B3
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00014DBB File Offset: 0x000131BB
		public bool DebugDraw { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00014DC4 File Offset: 0x000131C4
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x00014DCC File Offset: 0x000131CC
		public int MaxNeighbours { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00014DD5 File Offset: 0x000131D5
		public List<ObstacleVertex> NeighbourObstacles
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014DD8 File Offset: 0x000131D8
		public void BufferSwitch()
		{
			this.radius = this.Radius;
			this.height = this.Height;
			this.maxSpeed = this.MaxSpeed;
			this.neighbourDist = this.NeighbourDist;
			this.agentTimeHorizon = this.AgentTimeHorizon;
			this.obstacleTimeHorizon = this.ObstacleTimeHorizon;
			this.maxNeighbours = this.MaxNeighbours;
			this.desiredVelocity = this.DesiredVelocity;
			this.locked = this.Locked;
			this.collidesWith = this.CollidesWith;
			this.layer = this.Layer;
			this.Velocity = this.velocity;
			List<ObstacleVertex> list = this.obstaclesBuffered;
			this.obstaclesBuffered = this.obstacles;
			this.obstacles = list;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014E90 File Offset: 0x00013290
		public void Update()
		{
			this.velocity = this.newVelocity;
			this.prevSmoothPos = this.smoothPos;
			this.position = this.prevSmoothPos;
			this.position += this.velocity * this.simulator.DeltaTime;
			this.Position = this.position;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00014EF4 File Offset: 0x000132F4
		public void Interpolate(float t)
		{
			this.smoothPos = this.prevSmoothPos + (this.Position - this.prevSmoothPos) * t;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014F20 File Offset: 0x00013320
		public void CalculateNeighbours()
		{
			this.neighbours.Clear();
			this.neighbourDists.Clear();
			if (this.locked)
			{
				return;
			}
			float num;
			if (this.MaxNeighbours > 0)
			{
				num = this.neighbourDist * this.neighbourDist;
				this.simulator.Quadtree.Query(new Vector2(this.position.x, this.position.z), this.neighbourDist, this);
			}
			this.obstacles.Clear();
			this.obstacleDists.Clear();
			num = this.obstacleTimeHorizon * this.maxSpeed + this.radius;
			num *= num;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00014FCA File Offset: 0x000133CA
		private float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00014FD0 File Offset: 0x000133D0
		public float InsertAgentNeighbour(Agent agent, float rangeSq)
		{
			if (this == agent)
			{
				return rangeSq;
			}
			if ((agent.layer & this.collidesWith) == (RVOLayer)0)
			{
				return rangeSq;
			}
			float num = this.Sqr(agent.position.x - this.position.x) + this.Sqr(agent.position.z - this.position.z);
			if (num < rangeSq)
			{
				if (this.neighbours.Count < this.maxNeighbours)
				{
					this.neighbours.Add(agent);
					this.neighbourDists.Add(num);
				}
				int num2 = this.neighbours.Count - 1;
				if (num < this.neighbourDists[num2])
				{
					while (num2 != 0 && num < this.neighbourDists[num2 - 1])
					{
						this.neighbours[num2] = this.neighbours[num2 - 1];
						this.neighbourDists[num2] = this.neighbourDists[num2 - 1];
						num2--;
					}
					this.neighbours[num2] = agent;
					this.neighbourDists[num2] = num;
				}
				if (this.neighbours.Count == this.maxNeighbours)
				{
					rangeSq = this.neighbourDists[this.neighbourDists.Count - 1];
				}
			}
			return rangeSq;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00015130 File Offset: 0x00013530
		public void InsertObstacleNeighbour(ObstacleVertex ob1, float rangeSq)
		{
			ObstacleVertex obstacleVertex = ob1.next;
			float num = AstarMath.DistancePointSegmentStrict(ob1.position, obstacleVertex.position, this.Position);
			if (num < rangeSq)
			{
				this.obstacles.Add(ob1);
				this.obstacleDists.Add(num);
				int num2 = this.obstacles.Count - 1;
				while (num2 != 0 && num < this.obstacleDists[num2 - 1])
				{
					this.obstacles[num2] = this.obstacles[num2 - 1];
					this.obstacleDists[num2] = this.obstacleDists[num2 - 1];
					num2--;
				}
				this.obstacles[num2] = ob1;
				this.obstacleDists[num2] = num;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000151FA File Offset: 0x000135FA
		private static Vector3 To3D(Vector2 p)
		{
			return new Vector3(p.x, 0f, p.y);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00015214 File Offset: 0x00013614
		private static void DrawCircle(Vector2 _p, float radius, Color col)
		{
			Agent.DrawCircle(_p, radius, 0f, 6.28318548f, col);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00015228 File Offset: 0x00013628
		private static void DrawCircle(Vector2 _p, float radius, float a0, float a1, Color col)
		{
			Vector3 a2 = Agent.To3D(_p);
			while (a0 > a1)
			{
				a0 -= 6.28318548f;
			}
			Vector3 b = new Vector3(Mathf.Cos(a0) * radius, 0f, Mathf.Sin(a0) * radius);
			int num = 0;
			while ((float)num <= 40f)
			{
				Vector3 vector = new Vector3(Mathf.Cos(Mathf.Lerp(a0, a1, (float)num / 40f)) * radius, 0f, Mathf.Sin(Mathf.Lerp(a0, a1, (float)num / 40f)) * radius);
				UnityEngine.Debug.DrawLine(a2 + b, a2 + vector, col);
				b = vector;
				num++;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000152D4 File Offset: 0x000136D4
		private static void DrawVO(Vector2 circleCenter, float radius, Vector2 origin)
		{
			float num = Mathf.Atan2((origin - circleCenter).y, (origin - circleCenter).x);
			float num2 = radius / (origin - circleCenter).magnitude;
			float num3 = (num2 > 1f) ? 0f : Mathf.Abs(Mathf.Acos(num2));
			Agent.DrawCircle(circleCenter, radius, num - num3, num + num3, Color.black);
			Vector2 vector = new Vector2(Mathf.Cos(num - num3), Mathf.Sin(num - num3)) * radius;
			Vector2 vector2 = new Vector2(Mathf.Cos(num + num3), Mathf.Sin(num + num3)) * radius;
			Vector2 p = -new Vector2(-vector.y, vector.x);
			Vector2 p2 = new Vector2(-vector2.y, vector2.x);
			vector += circleCenter;
			vector2 += circleCenter;
			UnityEngine.Debug.DrawRay(Agent.To3D(vector), Agent.To3D(p).normalized * 100f, Color.black);
			UnityEngine.Debug.DrawRay(Agent.To3D(vector2), Agent.To3D(p2).normalized * 100f, Color.black);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00015427 File Offset: 0x00013827
		private static void DrawCross(Vector2 p, float size = 1f)
		{
			Agent.DrawCross(p, Color.white, size);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015438 File Offset: 0x00013838
		private static void DrawCross(Vector2 p, Color col, float size = 1f)
		{
			size *= 0.5f;
			UnityEngine.Debug.DrawLine(new Vector3(p.x, 0f, p.y) - Vector3.right * size, new Vector3(p.x, 0f, p.y) + Vector3.right * size, col);
			UnityEngine.Debug.DrawLine(new Vector3(p.x, 0f, p.y) - Vector3.forward * size, new Vector3(p.x, 0f, p.y) + Vector3.forward * size, col);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000154FC File Offset: 0x000138FC
		internal void CalculateVelocity(Simulator.WorkerContext context)
		{
			if (this.locked)
			{
				this.newVelocity = Vector2.zero;
				return;
			}
			if (context.vos.Length < this.neighbours.Count + this.simulator.obstacles.Count)
			{
				context.vos = new Agent.VO[Mathf.Max(context.vos.Length * 2, this.neighbours.Count + this.simulator.obstacles.Count)];
			}
			Vector2 vector = new Vector2(this.position.x, this.position.z);
			Agent.VO[] vos = context.vos;
			int num = 0;
			Vector2 vector2 = new Vector2(this.velocity.x, this.velocity.z);
			float num2 = 1f / this.agentTimeHorizon;
			float wallThickness = this.simulator.WallThickness;
			float num3 = (this.simulator.algorithm != Simulator.SamplingAlgorithm.GradientDecent) ? 5f : 1f;
			for (int i = 0; i < this.simulator.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.simulator.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					if (obstacleVertex2.ignore || this.position.y > obstacleVertex2.position.y + obstacleVertex2.height || this.position.y + this.height < obstacleVertex2.position.y || (obstacleVertex2.layer & this.collidesWith) == (RVOLayer)0)
					{
						obstacleVertex2 = obstacleVertex2.next;
					}
					else
					{
						float num4 = Agent.VO.Det(new Vector2(obstacleVertex2.position.x, obstacleVertex2.position.z), obstacleVertex2.dir, vector);
						float num5 = num4;
						float num6 = Vector2.Dot(obstacleVertex2.dir, vector - new Vector2(obstacleVertex2.position.x, obstacleVertex2.position.z));
						bool flag = num6 <= wallThickness * 0.05f || num6 >= (new Vector2(obstacleVertex2.position.x, obstacleVertex2.position.z) - new Vector2(obstacleVertex2.next.position.x, obstacleVertex2.next.position.z)).magnitude - wallThickness * 0.05f;
						if (Mathf.Abs(num5) < this.neighbourDist)
						{
							if (num5 <= 0f && !flag && num5 > -wallThickness)
							{
								vos[num] = new Agent.VO(vector, new Vector2(obstacleVertex2.position.x, obstacleVertex2.position.z) - vector, obstacleVertex2.dir, num3 * 2f);
								num++;
							}
							else if (num5 > 0f)
							{
								Vector2 p = new Vector2(obstacleVertex2.position.x, obstacleVertex2.position.z) - vector;
								Vector2 p2 = new Vector2(obstacleVertex2.next.position.x, obstacleVertex2.next.position.z) - vector;
								Vector2 normalized = p.normalized;
								Vector2 normalized2 = p2.normalized;
								vos[num] = new Agent.VO(vector, p, p2, normalized, normalized2, num3);
								num++;
							}
						}
						obstacleVertex2 = obstacleVertex2.next;
					}
				}
				while (obstacleVertex2 != obstacleVertex);
			}
			for (int j = 0; j < this.neighbours.Count; j++)
			{
				Agent agent = this.neighbours[j];
				if (agent != this)
				{
					float num7 = Math.Min(this.position.y + this.height, agent.position.y + agent.height);
					float num8 = Math.Max(this.position.y, agent.position.y);
					if (num7 - num8 >= 0f)
					{
						Vector2 vector3 = new Vector2(agent.Velocity.x, agent.velocity.z);
						float num9 = this.radius + agent.radius;
						Vector2 vector4 = new Vector2(agent.position.x, agent.position.z) - vector;
						Vector2 sideChooser = vector2 - vector3;
						Vector2 vector5;
						if (agent.locked)
						{
							vector5 = vector3;
						}
						else
						{
							vector5 = (vector2 + vector3) * 0.5f;
						}
						vos[num] = new Agent.VO(vector4, vector5, num9, sideChooser, num2, 1f);
						num++;
						if (this.DebugDraw)
						{
							Agent.DrawVO(vector + vector4 * num2 + vector5, num9 * num2, vector + vector5);
						}
					}
				}
			}
			Vector2 vector6 = Vector2.zero;
			if (this.simulator.algorithm == Simulator.SamplingAlgorithm.GradientDecent)
			{
				if (this.DebugDraw)
				{
					for (int k = 0; k < 40; k++)
					{
						for (int l = 0; l < 40; l++)
						{
							Vector2 vector7 = new Vector2((float)k * 15f / 40f, (float)l * 15f / 40f);
							Vector2 a = Vector2.zero;
							float num10 = 0f;
							for (int m = 0; m < num; m++)
							{
								float num11 = 0f;
								a += vos[m].Sample(vector7 - vector, out num11);
								if (num11 > num10)
								{
									num10 = num11;
								}
							}
							Vector2 a2 = new Vector2(this.desiredVelocity.x, this.desiredVelocity.z) - (vector7 - vector);
							a += a2 * Agent.DesiredVelocityScale;
							if (a2.magnitude * Agent.DesiredVelocityWeight > num10)
							{
								num10 = a2.magnitude * Agent.DesiredVelocityWeight;
							}
							if (num10 > 0f)
							{
								a /= num10;
							}
							UnityEngine.Debug.DrawRay(Agent.To3D(vector7), Agent.To3D(a2 * 0f), Color.blue);
							float num12 = 0f;
							Vector2 vector8 = vector7 - Vector2.one * 15f * 0.5f;
							Vector2 vector9 = this.Trace(vos, num, vector8, 0.01f, out num12);
							if ((vector8 - vector9).sqrMagnitude < this.Sqr(0.375f) * 2.6f)
							{
								UnityEngine.Debug.DrawRay(Agent.To3D(vector9 + vector), Vector3.up * 1f, Color.red);
							}
						}
					}
				}
				float num13 = float.PositiveInfinity;
				Vector2 vector10 = new Vector2(this.velocity.x, this.velocity.z);
				float cutoff = vector10.magnitude * this.simulator.qualityCutoff;
				vector6 = this.Trace(vos, num, new Vector2(this.desiredVelocity.x, this.desiredVelocity.z), cutoff, out num13);
				if (this.DebugDraw)
				{
					Agent.DrawCross(vector6 + vector, Color.yellow, 0.5f);
				}
				Vector2 p3 = this.Velocity;
				float num14;
				Vector2 vector11 = this.Trace(vos, num, p3, cutoff, out num14);
				if (num14 < num13)
				{
					vector6 = vector11;
					num13 = num14;
				}
				if (this.DebugDraw)
				{
					Agent.DrawCross(vector11 + vector, Color.magenta, 0.5f);
				}
			}
			else
			{
				Vector2[] samplePos = context.samplePos;
				float[] sampleSize = context.sampleSize;
				int num15 = 0;
				Vector2 vector12 = new Vector2(this.desiredVelocity.x, this.desiredVelocity.z);
				float num16 = Mathf.Max(this.radius, Mathf.Max(vector12.magnitude, this.Velocity.magnitude));
				samplePos[num15] = vector12;
				sampleSize[num15] = num16 * 0.3f;
				num15++;
				samplePos[num15] = vector2;
				sampleSize[num15] = num16 * 0.3f;
				num15++;
				Vector2 a3 = vector2 * 0.5f;
				Vector2 a4 = new Vector2(a3.y, -a3.x);
				for (int n = 0; n < 8; n++)
				{
					samplePos[num15] = a4 * Mathf.Sin((float)n * 3.14159274f * 2f / 8f) + a3 * (1f + Mathf.Cos((float)n * 3.14159274f * 2f / 8f));
					sampleSize[num15] = (1f - Mathf.Abs((float)n - 4f) / 8f) * num16 * 0.5f;
					num15++;
				}
				a3 *= 0.6f;
				a4 *= 0.6f;
				for (int num17 = 0; num17 < 6; num17++)
				{
					samplePos[num15] = a4 * Mathf.Cos(((float)num17 + 0.5f) * 3.14159274f * 2f / 6f) + a3 * (1.66666663f + Mathf.Sin(((float)num17 + 0.5f) * 3.14159274f * 2f / 6f));
					sampleSize[num15] = num16 * 0.3f;
					num15++;
				}
				for (int num18 = 0; num18 < 6; num18++)
				{
					samplePos[num15] = vector2 + new Vector2(num16 * 0.2f * Mathf.Cos(((float)num18 + 0.5f) * 3.14159274f * 2f / 6f), num16 * 0.2f * Mathf.Sin(((float)num18 + 0.5f) * 3.14159274f * 2f / 6f));
					sampleSize[num15] = num16 * 0.2f * 2f;
					num15++;
				}
				samplePos[num15] = vector2 * 0.5f;
				sampleSize[num15] = num16 * 0.4f;
				num15++;
				Vector2[] bestPos = context.bestPos;
				float[] bestSizes = context.bestSizes;
				float[] bestScores = context.bestScores;
				for (int num19 = 0; num19 < 3; num19++)
				{
					bestScores[num19] = float.PositiveInfinity;
				}
				bestScores[3] = float.NegativeInfinity;
				Vector2 vector13 = vector2;
				float num20 = float.PositiveInfinity;
				for (int num21 = 0; num21 < 3; num21++)
				{
					for (int num22 = 0; num22 < num15; num22++)
					{
						float num23 = 0f;
						for (int num24 = 0; num24 < num; num24++)
						{
							num23 = Math.Max(num23, vos[num24].ScalarSample(samplePos[num22]));
						}
						float magnitude = (samplePos[num22] - vector12).magnitude;
						float num25 = num23 + magnitude * Agent.DesiredVelocityWeight;
						num23 += magnitude * 0.001f;
						if (this.DebugDraw)
						{
							Agent.DrawCross(vector + samplePos[num22], Agent.Rainbow(Mathf.Log(num23 + 1f) * 5f), sampleSize[num22] * 0.5f);
						}
						if (num25 < bestScores[0])
						{
							for (int num26 = 0; num26 < 3; num26++)
							{
								if (num25 >= bestScores[num26 + 1])
								{
									bestScores[num26] = num25;
									bestSizes[num26] = sampleSize[num22];
									bestPos[num26] = samplePos[num22];
									break;
								}
							}
						}
						if (num23 < num20)
						{
							vector13 = samplePos[num22];
							num20 = num23;
							if (num23 == 0f)
							{
								num21 = 100;
								break;
							}
						}
					}
					num15 = 0;
					for (int num27 = 0; num27 < 3; num27++)
					{
						Vector2 a5 = bestPos[num27];
						float num28 = bestSizes[num27];
						bestScores[num27] = float.PositiveInfinity;
						float num29 = num28 * 0.6f * 0.5f;
						samplePos[num15] = a5 + new Vector2(num29, num29);
						samplePos[num15 + 1] = a5 + new Vector2(-num29, num29);
						samplePos[num15 + 2] = a5 + new Vector2(-num29, -num29);
						samplePos[num15 + 3] = a5 + new Vector2(num29, -num29);
						num28 *= num28 * 0.6f;
						sampleSize[num15] = num28;
						sampleSize[num15 + 1] = num28;
						sampleSize[num15 + 2] = num28;
						sampleSize[num15 + 3] = num28;
						num15 += 4;
					}
				}
				vector6 = vector13;
			}
			if (this.DebugDraw)
			{
				Agent.DrawCross(vector6 + vector, 1f);
			}
			this.newVelocity = Agent.To3D(Vector2.ClampMagnitude(vector6, this.maxSpeed));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000162D0 File Offset: 0x000146D0
		private static Color Rainbow(float v)
		{
			Color result = new Color(v, 0f, 0f);
			if (result.r > 1f)
			{
				result.g = result.r - 1f;
				result.r = 1f;
			}
			if (result.g > 1f)
			{
				result.b = result.g - 1f;
				result.g = 1f;
			}
			return result;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00016354 File Offset: 0x00014754
		private Vector2 Trace(Agent.VO[] vos, int voCount, Vector2 p, float cutoff, out float score)
		{
			score = 0f;
			float stepScale = this.simulator.stepScale;
			float num = float.PositiveInfinity;
			Vector2 result = p;
			for (int i = 0; i < 50; i++)
			{
				float num2 = 1f - (float)i / 50f;
				num2 *= stepScale;
				Vector2 vector = Vector2.zero;
				float num3 = 0f;
				for (int j = 0; j < voCount; j++)
				{
					float num4;
					Vector2 b = vos[j].Sample(p, out num4);
					vector += b;
					if (num4 > num3)
					{
						num3 = num4;
					}
				}
				Vector2 a = new Vector2(this.desiredVelocity.x, this.desiredVelocity.z) - p;
				float val = a.magnitude * Agent.DesiredVelocityWeight;
				vector += a * Agent.DesiredVelocityScale;
				num3 = Math.Max(num3, val);
				score = num3;
				if (score < num)
				{
					num = score;
				}
				result = p;
				if (score <= cutoff && i > 10)
				{
					break;
				}
				float sqrMagnitude = vector.sqrMagnitude;
				if (sqrMagnitude > 0f)
				{
					vector *= num3 / Mathf.Sqrt(sqrMagnitude);
				}
				vector *= num2;
				Vector2 p2 = p;
				p += vector;
				if (this.DebugDraw)
				{
					UnityEngine.Debug.DrawLine(Agent.To3D(p2) + this.position, Agent.To3D(p) + this.position, Agent.Rainbow(0.1f / score) * new Color(1f, 1f, 1f, 0.2f));
				}
			}
			score = num;
			return result;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00016518 File Offset: 0x00014918
		public static bool IntersectionFactor(Vector2 start1, Vector2 dir1, Vector2 start2, Vector2 dir2, out float factor)
		{
			float num = dir2.y * dir1.x - dir2.x * dir1.y;
			if (num == 0f)
			{
				factor = 0f;
				return false;
			}
			float num2 = dir2.x * (start1.y - start2.y) - dir2.y * (start1.x - start2.x);
			factor = num2 / num;
			return true;
		}

		// Token: 0x040001CE RID: 462
		private Vector3 smoothPos;

		// Token: 0x040001D1 RID: 465
		public float radius;

		// Token: 0x040001D2 RID: 466
		public float height;

		// Token: 0x040001D3 RID: 467
		public float maxSpeed;

		// Token: 0x040001D4 RID: 468
		public float neighbourDist;

		// Token: 0x040001D5 RID: 469
		public float agentTimeHorizon;

		// Token: 0x040001D6 RID: 470
		public float obstacleTimeHorizon;

		// Token: 0x040001D7 RID: 471
		public float weight;

		// Token: 0x040001D8 RID: 472
		public bool locked;

		// Token: 0x040001D9 RID: 473
		private RVOLayer layer;

		// Token: 0x040001DA RID: 474
		private RVOLayer collidesWith;

		// Token: 0x040001DB RID: 475
		public int maxNeighbours;

		// Token: 0x040001DC RID: 476
		public Vector3 position;

		// Token: 0x040001DD RID: 477
		public Vector3 desiredVelocity;

		// Token: 0x040001DE RID: 478
		public Vector3 prevSmoothPos;

		// Token: 0x040001EB RID: 491
		internal Agent next;

		// Token: 0x040001EC RID: 492
		private Vector3 velocity;

		// Token: 0x040001ED RID: 493
		internal Vector3 newVelocity;

		// Token: 0x040001EE RID: 494
		public Simulator simulator;

		// Token: 0x040001EF RID: 495
		public List<Agent> neighbours = new List<Agent>();

		// Token: 0x040001F0 RID: 496
		public List<float> neighbourDists = new List<float>();

		// Token: 0x040001F1 RID: 497
		private List<ObstacleVertex> obstaclesBuffered = new List<ObstacleVertex>();

		// Token: 0x040001F2 RID: 498
		private List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x040001F3 RID: 499
		private List<float> obstacleDists = new List<float>();

		// Token: 0x040001F4 RID: 500
		public static Stopwatch watch1 = new Stopwatch();

		// Token: 0x040001F5 RID: 501
		public static Stopwatch watch2 = new Stopwatch();

		// Token: 0x040001F6 RID: 502
		public static float DesiredVelocityWeight = 0.02f;

		// Token: 0x040001F7 RID: 503
		public static float DesiredVelocityScale = 0.1f;

		// Token: 0x040001F8 RID: 504
		public static float GlobalIncompressibility = 30f;

		// Token: 0x040001F9 RID: 505
		private const float WallWeight = 5f;

		// Token: 0x02000039 RID: 57
		public struct VO
		{
			// Token: 0x060002B7 RID: 695 RVA: 0x000165C8 File Offset: 0x000149C8
			public VO(Vector2 offset, Vector2 p0, Vector2 dir, float weightFactor)
			{
				this.colliding = true;
				this.line1 = p0;
				this.dir1 = -dir;
				this.origin = Vector2.zero;
				this.center = Vector2.zero;
				this.line2 = Vector2.zero;
				this.dir2 = Vector2.zero;
				this.cutoffLine = Vector2.zero;
				this.cutoffDir = Vector2.zero;
				this.sqrCutoffDistance = 0f;
				this.leftSide = false;
				this.radius = 0f;
				this.weightFactor = weightFactor * 0.5f;
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0001665C File Offset: 0x00014A5C
			public VO(Vector2 offset, Vector2 p1, Vector2 p2, Vector2 tang1, Vector2 tang2, float weightFactor)
			{
				this.weightFactor = weightFactor * 0.5f;
				this.colliding = false;
				this.cutoffLine = p1;
				this.cutoffDir = (p2 - p1).normalized;
				this.line1 = p1;
				this.dir1 = tang1;
				this.line2 = p2;
				this.dir2 = tang2;
				this.dir2 = -this.dir2;
				this.cutoffDir = -this.cutoffDir;
				this.origin = Vector2.zero;
				this.center = Vector2.zero;
				this.sqrCutoffDistance = 0f;
				this.leftSide = false;
				this.radius = 0f;
				weightFactor = 5f;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00016714 File Offset: 0x00014B14
			public VO(Vector2 center, Vector2 offset, float radius, Vector2 sideChooser, float inverseDt, float weightFactor)
			{
				this.weightFactor = weightFactor * 0.5f;
				this.origin = offset;
				weightFactor = 0.5f;
				if (center.magnitude < radius)
				{
					this.colliding = true;
					this.leftSide = false;
					this.line1 = center.normalized * (center.magnitude - radius);
					Vector2 vector = new Vector2(this.line1.y, -this.line1.x);
					this.dir1 = vector.normalized;
					this.line1 += offset;
					this.cutoffDir = Vector2.zero;
					this.cutoffLine = Vector2.zero;
					this.sqrCutoffDistance = 0f;
					this.dir2 = Vector2.zero;
					this.line2 = Vector2.zero;
					this.center = Vector2.zero;
					this.radius = 0f;
				}
				else
				{
					this.colliding = false;
					center *= inverseDt;
					radius *= inverseDt;
					Vector2 b = center + offset;
					this.sqrCutoffDistance = center.magnitude - radius;
					this.center = center;
					this.cutoffLine = center.normalized * this.sqrCutoffDistance;
					Vector2 vector2 = new Vector2(-this.cutoffLine.y, this.cutoffLine.x);
					this.cutoffDir = vector2.normalized;
					this.cutoffLine += offset;
					this.sqrCutoffDistance *= this.sqrCutoffDistance;
					float num = Mathf.Atan2(-center.y, -center.x);
					float num2 = Mathf.Abs(Mathf.Acos(radius / center.magnitude));
					this.radius = radius;
					this.leftSide = Polygon.Left(Vector2.zero, center, sideChooser);
					this.line1 = new Vector2(Mathf.Cos(num + num2), Mathf.Sin(num + num2)) * radius;
					Vector2 vector3 = new Vector2(this.line1.y, -this.line1.x);
					this.dir1 = vector3.normalized;
					this.line2 = new Vector2(Mathf.Cos(num - num2), Mathf.Sin(num - num2)) * radius;
					Vector2 vector4 = new Vector2(this.line2.y, -this.line2.x);
					this.dir2 = vector4.normalized;
					this.line1 += b;
					this.line2 += b;
				}
			}

			// Token: 0x060002BA RID: 698 RVA: 0x000169A4 File Offset: 0x00014DA4
			public static bool Left(Vector2 a, Vector2 dir, Vector2 p)
			{
				return dir.x * (p.y - a.y) - (p.x - a.x) * dir.y <= 0f;
			}

			// Token: 0x060002BB RID: 699 RVA: 0x000169DF File Offset: 0x00014DDF
			public static float Det(Vector2 a, Vector2 dir, Vector2 p)
			{
				return (p.x - a.x) * dir.y - dir.x * (p.y - a.y);
			}

			// Token: 0x060002BC RID: 700 RVA: 0x00016A10 File Offset: 0x00014E10
			public Vector2 Sample(Vector2 p, out float weight)
			{
				if (this.colliding)
				{
					float num = Agent.VO.Det(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						weight = num * this.weightFactor;
						return new Vector2(-this.dir1.y, this.dir1.x) * weight * Agent.GlobalIncompressibility;
					}
					weight = 0f;
					return new Vector2(0f, 0f);
				}
				else
				{
					float num2 = Agent.VO.Det(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						weight = 0f;
						return Vector2.zero;
					}
					float num3 = Agent.VO.Det(this.line1, this.dir1, p);
					float num4 = Agent.VO.Det(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						weight = 0f;
						return new Vector2(0f, 0f);
					}
					if (this.leftSide)
					{
						if (num2 < this.radius)
						{
							weight = num2 * this.weightFactor;
							return new Vector2(-this.cutoffDir.y, this.cutoffDir.x) * weight;
						}
						weight = num3;
						return new Vector2(-this.dir1.y, this.dir1.x) * weight;
					}
					else
					{
						if (num2 < this.radius)
						{
							weight = num2 * this.weightFactor;
							return new Vector2(-this.cutoffDir.y, this.cutoffDir.x) * weight;
						}
						weight = num4 * this.weightFactor;
						return new Vector2(-this.dir2.y, this.dir2.x) * weight;
					}
				}
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00016BE8 File Offset: 0x00014FE8
			public float ScalarSample(Vector2 p)
			{
				if (this.colliding)
				{
					float num = Agent.VO.Det(this.line1, this.dir1, p);
					if (num >= 0f)
					{
						return num * Agent.GlobalIncompressibility * this.weightFactor;
					}
					return 0f;
				}
				else
				{
					float num2 = Agent.VO.Det(this.cutoffLine, this.cutoffDir, p);
					if (num2 <= 0f)
					{
						return 0f;
					}
					float num3 = Agent.VO.Det(this.line1, this.dir1, p);
					float num4 = Agent.VO.Det(this.line2, this.dir2, p);
					if (num3 < 0f || num4 < 0f)
					{
						return 0f;
					}
					if (this.leftSide)
					{
						if (num2 < this.radius)
						{
							return num2 * this.weightFactor;
						}
						return num3 * this.weightFactor;
					}
					else
					{
						if (num2 < this.radius)
						{
							return num2 * this.weightFactor;
						}
						return num4 * this.weightFactor;
					}
				}
			}

			// Token: 0x040001FA RID: 506
			public Vector2 origin;

			// Token: 0x040001FB RID: 507
			public Vector2 center;

			// Token: 0x040001FC RID: 508
			private Vector2 line1;

			// Token: 0x040001FD RID: 509
			private Vector2 line2;

			// Token: 0x040001FE RID: 510
			private Vector2 dir1;

			// Token: 0x040001FF RID: 511
			private Vector2 dir2;

			// Token: 0x04000200 RID: 512
			private Vector2 cutoffLine;

			// Token: 0x04000201 RID: 513
			private Vector2 cutoffDir;

			// Token: 0x04000202 RID: 514
			private float sqrCutoffDistance;

			// Token: 0x04000203 RID: 515
			private bool leftSide;

			// Token: 0x04000204 RID: 516
			private bool colliding;

			// Token: 0x04000205 RID: 517
			private float radius;

			// Token: 0x04000206 RID: 518
			private float weightFactor;
		}
	}
}
