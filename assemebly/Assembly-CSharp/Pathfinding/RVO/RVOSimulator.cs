using System;
using Pathfinding.RVO.Sampled;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000EB RID: 235
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Simulator")]
	public class RVOSimulator : MonoBehaviour
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x0004ABA7 File Offset: 0x00048FA7
		public Simulator GetSimulator()
		{
			if (this.simulator == null)
			{
				this.Awake();
			}
			return this.simulator;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0004ABC0 File Offset: 0x00048FC0
		private void Awake()
		{
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			if (this.simulator == null)
			{
				int workers = AstarPath.CalculateThreadCount(this.workerThreads);
				this.simulator = new Simulator(workers, this.doubleBuffering);
				this.simulator.Interpolation = this.interpolation;
				this.simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0004AC34 File Offset: 0x00049034
		private void Update()
		{
			if (this.desiredSimulationFPS < 1)
			{
				this.desiredSimulationFPS = 1;
			}
			Agent.DesiredVelocityWeight = this.desiredVelocityWeight;
			Agent.GlobalIncompressibility = this.incompressibility;
			Simulator simulator = this.GetSimulator();
			simulator.DesiredDeltaTime = 1f / (float)this.desiredSimulationFPS;
			simulator.Interpolation = this.interpolation;
			simulator.stepScale = this.stepScale;
			simulator.qualityCutoff = this.qualityCutoff;
			simulator.algorithm = this.algorithm;
			simulator.Oversampling = this.oversampling;
			simulator.WallThickness = this.wallThickness;
			simulator.Update();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0004ACD2 File Offset: 0x000490D2
		private void OnDestroy()
		{
			if (this.simulator != null)
			{
				this.simulator.OnDestroy();
			}
		}

		// Token: 0x0400067E RID: 1662
		[Tooltip("Calculate local avoidance in between frames.\nThis can increase jitter in the agents' movement so use it only if you really need the performance boost. It will also reduce the responsiveness of the agents to the commands you send to them.")]
		public bool doubleBuffering;

		// Token: 0x0400067F RID: 1663
		[Tooltip("Interpolate positions between simulation timesteps")]
		public bool interpolation = true;

		// Token: 0x04000680 RID: 1664
		[Tooltip("Desired FPS for rvo simulation. It is usually not necessary to run a crowd simulation at a very high fps.\nUsually 10-30 fps is enough, but can be increased for better quality.\nThe rvo simulation will never run at a higher fps than the game")]
		public int desiredSimulationFPS = 20;

		// Token: 0x04000681 RID: 1665
		[Tooltip("Number of RVO worker threads. If set to None, no multithreading will be used.")]
		public ThreadCount workerThreads = ThreadCount.Two;

		// Token: 0x04000682 RID: 1666
		[Tooltip("[GradientDecent][unitless][0...1] A higher value will result in lower quality local avoidance but faster calculations.")]
		public float qualityCutoff = 0.05f;

		// Token: 0x04000683 RID: 1667
		[Tooltip("[GradientDecent][unitless][0...2] How large steps to take when searching for a minimum to the penalty function. Larger values will make it faster, but less accurate, too low values (near 0) can also give large inaccuracies. Values around 0.5-1.5 work the best.")]
		public float stepScale = 1.5f;

		// Token: 0x04000684 RID: 1668
		[Tooltip("[0...infinity] Higher values will raise the penalty for agent-agent intersection")]
		public float incompressibility = 30f;

		// Token: 0x04000685 RID: 1669
		[Tooltip("Thickness of RVO obstacle walls.\nIf obstacles are passing through obstacles, try a larger value, if they are getting stuck near small obstacles, try reducing it")]
		public float wallThickness = 1f;

		// Token: 0x04000686 RID: 1670
		[Tooltip("[unitless][0...infinity] How much an agent will try to reach the desired velocity. A higher value will yield a more aggressive behaviour")]
		public float desiredVelocityWeight = 0.1f;

		// Token: 0x04000687 RID: 1671
		[Tooltip("What sampling algorithm to use. GradientDecent is a bit more agressive but makes it easier for agents to intersect.")]
		public Simulator.SamplingAlgorithm algorithm = Simulator.SamplingAlgorithm.GradientDecent;

		// Token: 0x04000688 RID: 1672
		[Tooltip("Run multiple simulation steps per step. Much slower, but may lead to slightly higher quality local avoidance.")]
		public bool oversampling;

		// Token: 0x04000689 RID: 1673
		private Simulator simulator;
	}
}
