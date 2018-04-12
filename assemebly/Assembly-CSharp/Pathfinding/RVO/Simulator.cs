using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200003D RID: 61
	public class Simulator
	{
		// Token: 0x060002DE RID: 734 RVA: 0x00016CE8 File Offset: 0x000150E8
		public Simulator(int workers, bool doubleBuffering)
		{
			this.workers = new Simulator.Worker[workers];
			this.doubleBuffering = doubleBuffering;
			for (int i = 0; i < workers; i++)
			{
				this.workers[i] = new Simulator.Worker(this);
			}
			this.agents = new List<Agent>();
			this.obstacles = new List<ObstacleVertex>();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00016DAA File Offset: 0x000151AA
		public RVOQuadtree Quadtree
		{
			get
			{
				return this.quadtree;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00016DB2 File Offset: 0x000151B2
		public float DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00016DBA File Offset: 0x000151BA
		public float PrevDeltaTime
		{
			get
			{
				return this.prevDeltaTime;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00016DC2 File Offset: 0x000151C2
		public bool Multithreading
		{
			get
			{
				return this.workers != null && this.workers.Length > 0;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00016DDD File Offset: 0x000151DD
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00016DE5 File Offset: 0x000151E5
		public float DesiredDeltaTime
		{
			get
			{
				return this.desiredDeltaTime;
			}
			set
			{
				this.desiredDeltaTime = Math.Max(value, 0f);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00016DF8 File Offset: 0x000151F8
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00016E00 File Offset: 0x00015200
		public float WallThickness
		{
			get
			{
				return this.wallThickness;
			}
			set
			{
				this.wallThickness = Math.Max(this.wallThickness, 0f);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00016E18 File Offset: 0x00015218
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00016E20 File Offset: 0x00015220
		public bool Interpolation
		{
			get
			{
				return this.interpolation;
			}
			set
			{
				this.interpolation = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00016E29 File Offset: 0x00015229
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00016E31 File Offset: 0x00015231
		public bool Oversampling
		{
			get
			{
				return this.oversampling;
			}
			set
			{
				this.oversampling = value;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00016E3A File Offset: 0x0001523A
		public List<Agent> GetAgents()
		{
			return this.agents;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00016E42 File Offset: 0x00015242
		public List<ObstacleVertex> GetObstacles()
		{
			return this.obstacles;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00016E4C File Offset: 0x0001524C
		public void ClearAgents()
		{
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			for (int j = 0; j < this.agents.Count; j++)
			{
				this.agents[j].simulator = null;
			}
			this.agents.Clear();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00016ED0 File Offset: 0x000152D0
		public void OnDestroy()
		{
			if (this.workers != null)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].Terminate();
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00016F10 File Offset: 0x00015310
		~Simulator()
		{
			this.OnDestroy();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00016F40 File Offset: 0x00015340
		public IAgent AddAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != null && agent2.simulator == this)
			{
				throw new ArgumentException("The agent is already in the simulation");
			}
			if (agent2.simulator != null)
			{
				throw new ArgumentException("The agent is already added to another simulation");
			}
			agent2.simulator = this;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.agents.Add(agent2);
			return agent;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001700C File Offset: 0x0001540C
		public IAgent AddAgent(Vector3 position)
		{
			Agent agent = new Agent(position);
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.agents.Add(agent);
			agent.simulator = this;
			return agent;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00017070 File Offset: 0x00015470
		public void RemoveAgent(IAgent agent)
		{
			if (agent == null)
			{
				throw new ArgumentNullException("Agent must not be null");
			}
			Agent agent2 = agent as Agent;
			if (agent2 == null)
			{
				throw new ArgumentException("The agent must be of type Agent. Agent was of type " + agent.GetType());
			}
			if (agent2.simulator != this)
			{
				throw new ArgumentException("The agent is not added to this simulation");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			agent2.simulator = null;
			if (!this.agents.Remove(agent2))
			{
				throw new ArgumentException("Critical Bug! This should not happen. Please report this.");
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00017128 File Offset: 0x00015528
		public ObstacleVertex AddObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Add(v);
			this.UpdateObstacles();
			return v;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00017195 File Offset: 0x00015595
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height)
		{
			return this.AddObstacle(vertices, height, Matrix4x4.identity, RVOLayer.DefaultObstacle);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000171A8 File Offset: 0x000155A8
		public ObstacleVertex AddObstacle(Vector3[] vertices, float height, Matrix4x4 matrix, RVOLayer layer = RVOLayer.DefaultObstacle)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			ObstacleVertex obstacleVertex = null;
			ObstacleVertex obstacleVertex2 = null;
			bool flag = matrix == Matrix4x4.identity;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			for (int j = 0; j < vertices.Length; j++)
			{
				ObstacleVertex obstacleVertex3 = new ObstacleVertex();
				if (obstacleVertex == null)
				{
					obstacleVertex = obstacleVertex3;
				}
				else
				{
					obstacleVertex2.next = obstacleVertex3;
				}
				obstacleVertex3.prev = obstacleVertex2;
				obstacleVertex3.layer = layer;
				obstacleVertex3.position = ((!flag) ? matrix.MultiplyPoint3x4(vertices[j]) : vertices[j]);
				obstacleVertex3.height = height;
				obstacleVertex2 = obstacleVertex3;
			}
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.prev = obstacleVertex2;
			ObstacleVertex obstacleVertex4 = obstacleVertex;
			do
			{
				Vector3 vector = obstacleVertex4.next.position - obstacleVertex4.position;
				ObstacleVertex obstacleVertex5 = obstacleVertex4;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				obstacleVertex5.dir = vector2.normalized;
				obstacleVertex4 = obstacleVertex4.next;
			}
			while (obstacleVertex4 != obstacleVertex);
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001731C File Offset: 0x0001571C
		public ObstacleVertex AddObstacle(Vector3 a, Vector3 b, float height)
		{
			ObstacleVertex obstacleVertex = new ObstacleVertex();
			ObstacleVertex obstacleVertex2 = new ObstacleVertex();
			obstacleVertex.prev = obstacleVertex2;
			obstacleVertex2.prev = obstacleVertex;
			obstacleVertex.next = obstacleVertex2;
			obstacleVertex2.next = obstacleVertex;
			obstacleVertex.position = a;
			obstacleVertex2.position = b;
			obstacleVertex.height = height;
			obstacleVertex2.height = height;
			obstacleVertex2.ignore = true;
			ObstacleVertex obstacleVertex3 = obstacleVertex;
			Vector2 vector = new Vector2(b.x - a.x, b.z - a.z);
			obstacleVertex3.dir = vector.normalized;
			obstacleVertex2.dir = -obstacleVertex.dir;
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Add(obstacleVertex);
			this.UpdateObstacles();
			return obstacleVertex;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00017408 File Offset: 0x00015808
		public void UpdateObstacle(ObstacleVertex obstacle, Vector3[] vertices, Matrix4x4 matrix)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("Vertices must not be null");
			}
			if (obstacle == null)
			{
				throw new ArgumentNullException("Obstacle must not be null");
			}
			if (vertices.Length < 2)
			{
				throw new ArgumentException("Less than 2 vertices in an obstacle");
			}
			if (obstacle.split)
			{
				throw new ArgumentException("Obstacle is not a start vertex. You should only pass those ObstacleVertices got from AddObstacle method calls");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			int num = 0;
			ObstacleVertex obstacleVertex = obstacle;
			for (;;)
			{
				while (obstacleVertex.next.split)
				{
					obstacleVertex.next = obstacleVertex.next.next;
					obstacleVertex.next.prev = obstacleVertex;
				}
				if (num >= vertices.Length)
				{
					break;
				}
				obstacleVertex.position = matrix.MultiplyPoint3x4(vertices[num]);
				num++;
				obstacleVertex = obstacleVertex.next;
				if (obstacleVertex == obstacle)
				{
					goto Block_9;
				}
			}
			Debug.DrawLine(obstacleVertex.prev.position, obstacleVertex.position, Color.red);
			throw new ArgumentException("Obstacle has more vertices than supplied for updating (" + vertices.Length + " supplied)");
			Block_9:
			obstacleVertex = obstacle;
			do
			{
				Vector3 vector = obstacleVertex.next.position - obstacleVertex.position;
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				obstacleVertex2.dir = vector2.normalized;
				obstacleVertex = obstacleVertex.next;
			}
			while (obstacleVertex != obstacle);
			this.ScheduleCleanObstacles();
			this.UpdateObstacles();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00017594 File Offset: 0x00015994
		private void ScheduleCleanObstacles()
		{
			this.doCleanObstacles = true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000175A0 File Offset: 0x000159A0
		private void CleanObstacles()
		{
			for (int i = 0; i < this.obstacles.Count; i++)
			{
				ObstacleVertex obstacleVertex = this.obstacles[i];
				ObstacleVertex obstacleVertex2 = obstacleVertex;
				do
				{
					while (obstacleVertex2.next.split)
					{
						obstacleVertex2.next = obstacleVertex2.next.next;
						obstacleVertex2.next.prev = obstacleVertex2;
					}
					obstacleVertex2 = obstacleVertex2.next;
				}
				while (obstacleVertex2 != obstacleVertex);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00017618 File Offset: 0x00015A18
		public void RemoveObstacle(ObstacleVertex v)
		{
			if (v == null)
			{
				throw new ArgumentNullException("Vertex must not be null");
			}
			if (this.Multithreading && this.doubleBuffering)
			{
				for (int i = 0; i < this.workers.Length; i++)
				{
					this.workers[i].WaitOne();
				}
			}
			this.obstacles.Remove(v);
			this.UpdateObstacles();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00017685 File Offset: 0x00015A85
		public void UpdateObstacles()
		{
			this.doUpdateObstacles = true;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00017690 File Offset: 0x00015A90
		private void BuildQuadtree()
		{
			this.quadtree.Clear();
			if (this.agents.Count > 0)
			{
				Rect bounds = Rect.MinMaxRect(this.agents[0].position.x, this.agents[0].position.y, this.agents[0].position.x, this.agents[0].position.y);
				for (int i = 1; i < this.agents.Count; i++)
				{
					Vector3 position = this.agents[i].position;
					bounds = Rect.MinMaxRect(Mathf.Min(bounds.xMin, position.x), Mathf.Min(bounds.yMin, position.z), Mathf.Max(bounds.xMax, position.x), Mathf.Max(bounds.yMax, position.z));
				}
				this.quadtree.SetBounds(bounds);
				for (int j = 0; j < this.agents.Count; j++)
				{
					this.quadtree.Insert(this.agents[j]);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000177D8 File Offset: 0x00015BD8
		public void Update()
		{
			if (this.lastStep < 0f)
			{
				this.lastStep = Time.time;
				this.deltaTime = this.DesiredDeltaTime;
				this.prevDeltaTime = this.deltaTime;
				this.lastStepInterpolationReference = this.lastStep;
			}
			if (Time.time - this.lastStep >= this.DesiredDeltaTime)
			{
				if (!this.doubleBuffering)
				{
					for (int i = 0; i < this.agents.Count; i++)
					{
						this.agents[i].Interpolate((Time.time - this.lastStepInterpolationReference) / this.DeltaTime);
					}
				}
				this.lastStepInterpolationReference = Time.time;
				this.prevDeltaTime = this.DeltaTime;
				this.deltaTime = Time.time - this.lastStep;
				this.lastStep = Time.time;
				this.deltaTime = Math.Max(this.deltaTime, 0.0005f);
				if (this.Multithreading)
				{
					if (this.doubleBuffering)
					{
						for (int j = 0; j < this.workers.Length; j++)
						{
							this.workers[j].WaitOne();
						}
						if (!this.Interpolation)
						{
							for (int k = 0; k < this.agents.Count; k++)
							{
								this.agents[k].Interpolate(1f);
							}
						}
					}
					if (this.doCleanObstacles)
					{
						this.CleanObstacles();
						this.doCleanObstacles = false;
						this.doUpdateObstacles = true;
					}
					if (this.doUpdateObstacles)
					{
						this.doUpdateObstacles = false;
					}
					this.BuildQuadtree();
					for (int l = 0; l < this.workers.Length; l++)
					{
						this.workers[l].start = l * this.agents.Count / this.workers.Length;
						this.workers[l].end = (l + 1) * this.agents.Count / this.workers.Length;
					}
					for (int m = 0; m < this.workers.Length; m++)
					{
						this.workers[m].Execute(1);
					}
					for (int n = 0; n < this.workers.Length; n++)
					{
						this.workers[n].WaitOne();
					}
					for (int num = 0; num < this.workers.Length; num++)
					{
						this.workers[num].Execute(0);
					}
					if (!this.doubleBuffering)
					{
						for (int num2 = 0; num2 < this.workers.Length; num2++)
						{
							this.workers[num2].WaitOne();
						}
						if (!this.Interpolation)
						{
							for (int num3 = 0; num3 < this.agents.Count; num3++)
							{
								this.agents[num3].Interpolate(1f);
							}
						}
					}
				}
				else
				{
					if (this.doCleanObstacles)
					{
						this.CleanObstacles();
						this.doCleanObstacles = false;
						this.doUpdateObstacles = true;
					}
					if (this.doUpdateObstacles)
					{
						this.doUpdateObstacles = false;
					}
					this.BuildQuadtree();
					for (int num4 = 0; num4 < this.agents.Count; num4++)
					{
						this.agents[num4].Update();
						this.agents[num4].BufferSwitch();
					}
					for (int num5 = 0; num5 < this.agents.Count; num5++)
					{
						this.agents[num5].CalculateNeighbours();
						this.agents[num5].CalculateVelocity(this.coroutineWorkerContext);
					}
					if (this.oversampling)
					{
						for (int num6 = 0; num6 < this.agents.Count; num6++)
						{
							this.agents[num6].Velocity = this.agents[num6].newVelocity;
						}
						for (int num7 = 0; num7 < this.agents.Count; num7++)
						{
							Vector3 newVelocity = this.agents[num7].newVelocity;
							this.agents[num7].CalculateVelocity(this.coroutineWorkerContext);
							this.agents[num7].newVelocity = (newVelocity + this.agents[num7].newVelocity) * 0.5f;
						}
					}
					if (!this.Interpolation)
					{
						for (int num8 = 0; num8 < this.agents.Count; num8++)
						{
							this.agents[num8].Interpolate(1f);
						}
					}
				}
			}
			if (this.Interpolation)
			{
				for (int num9 = 0; num9 < this.agents.Count; num9++)
				{
					this.agents[num9].Interpolate((Time.time - this.lastStepInterpolationReference) / this.DeltaTime);
				}
			}
		}

		// Token: 0x04000230 RID: 560
		private bool doubleBuffering = true;

		// Token: 0x04000231 RID: 561
		private float desiredDeltaTime = 0.05f;

		// Token: 0x04000232 RID: 562
		private bool interpolation = true;

		// Token: 0x04000233 RID: 563
		private Simulator.Worker[] workers;

		// Token: 0x04000234 RID: 564
		private List<Agent> agents;

		// Token: 0x04000235 RID: 565
		public List<ObstacleVertex> obstacles;

		// Token: 0x04000236 RID: 566
		public Simulator.SamplingAlgorithm algorithm;

		// Token: 0x04000237 RID: 567
		private RVOQuadtree quadtree = new RVOQuadtree();

		// Token: 0x04000238 RID: 568
		public float qualityCutoff = 0.05f;

		// Token: 0x04000239 RID: 569
		public float stepScale = 1.5f;

		// Token: 0x0400023A RID: 570
		private float deltaTime;

		// Token: 0x0400023B RID: 571
		private float prevDeltaTime;

		// Token: 0x0400023C RID: 572
		private float lastStep = -99999f;

		// Token: 0x0400023D RID: 573
		private float lastStepInterpolationReference = -9999f;

		// Token: 0x0400023E RID: 574
		private bool doUpdateObstacles;

		// Token: 0x0400023F RID: 575
		private bool doCleanObstacles;

		// Token: 0x04000240 RID: 576
		private bool oversampling;

		// Token: 0x04000241 RID: 577
		private float wallThickness = 1f;

		// Token: 0x04000242 RID: 578
		private Simulator.WorkerContext coroutineWorkerContext = new Simulator.WorkerContext();

		// Token: 0x0200003E RID: 62
		public enum SamplingAlgorithm
		{
			// Token: 0x04000244 RID: 580
			AdaptiveSampling,
			// Token: 0x04000245 RID: 581
			GradientDecent
		}

		// Token: 0x0200003F RID: 63
		internal class WorkerContext
		{
			// Token: 0x04000246 RID: 582
			public Agent.VO[] vos = new Agent.VO[20];

			// Token: 0x04000247 RID: 583
			public const int KeepCount = 3;

			// Token: 0x04000248 RID: 584
			public Vector2[] bestPos = new Vector2[3];

			// Token: 0x04000249 RID: 585
			public float[] bestSizes = new float[3];

			// Token: 0x0400024A RID: 586
			public float[] bestScores = new float[4];

			// Token: 0x0400024B RID: 587
			public Vector2[] samplePos = new Vector2[50];

			// Token: 0x0400024C RID: 588
			public float[] sampleSize = new float[50];
		}

		// Token: 0x02000040 RID: 64
		private class Worker
		{
			// Token: 0x060002FF RID: 767 RVA: 0x00017D68 File Offset: 0x00016168
			public Worker(Simulator sim)
			{
				this.simulator = sim;
				this.thread = new Thread(new ThreadStart(this.Run));
				this.thread.IsBackground = true;
				this.thread.Name = "RVO Simulator Thread";
				this.thread.Start();
			}

			// Token: 0x06000300 RID: 768 RVA: 0x00017DE3 File Offset: 0x000161E3
			public void Execute(int task)
			{
				this.task = task;
				this.waitFlag.Reset();
				this.runFlag.Set();
			}

			// Token: 0x06000301 RID: 769 RVA: 0x00017E04 File Offset: 0x00016204
			public void WaitOne()
			{
				this.waitFlag.WaitOne();
			}

			// Token: 0x06000302 RID: 770 RVA: 0x00017E12 File Offset: 0x00016212
			public void Terminate()
			{
				this.terminate = true;
			}

			// Token: 0x06000303 RID: 771 RVA: 0x00017E1C File Offset: 0x0001621C
			public void Run()
			{
				this.runFlag.WaitOne();
				while (!this.terminate)
				{
					try
					{
						List<Agent> agents = this.simulator.GetAgents();
						if (this.task == 0)
						{
							for (int i = this.start; i < this.end; i++)
							{
								agents[i].CalculateNeighbours();
								agents[i].CalculateVelocity(this.context);
							}
						}
						else if (this.task == 1)
						{
							for (int j = this.start; j < this.end; j++)
							{
								agents[j].Update();
								agents[j].BufferSwitch();
							}
						}
						else
						{
							if (this.task != 2)
							{
								Debug.LogError("Invalid Task Number: " + this.task);
								throw new Exception("Invalid Task Number: " + this.task);
							}
							this.simulator.BuildQuadtree();
						}
					}
					catch (Exception message)
					{
						Debug.LogError(message);
					}
					this.waitFlag.Set();
					this.runFlag.WaitOne();
				}
			}

			// Token: 0x0400024D RID: 589
			[NonSerialized]
			public Thread thread;

			// Token: 0x0400024E RID: 590
			public int start;

			// Token: 0x0400024F RID: 591
			public int end;

			// Token: 0x04000250 RID: 592
			public int task;

			// Token: 0x04000251 RID: 593
			public AutoResetEvent runFlag = new AutoResetEvent(false);

			// Token: 0x04000252 RID: 594
			public ManualResetEvent waitFlag = new ManualResetEvent(true);

			// Token: 0x04000253 RID: 595
			public Simulator simulator;

			// Token: 0x04000254 RID: 596
			private bool terminate;

			// Token: 0x04000255 RID: 597
			private Simulator.WorkerContext context = new Simulator.WorkerContext();
		}
	}
}
