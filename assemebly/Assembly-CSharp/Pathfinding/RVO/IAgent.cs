using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x0200003B RID: 59
	public interface IAgent
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002BF RID: 703
		Vector3 InterpolatedPosition { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002C0 RID: 704
		Vector3 Position { get; }

		// Token: 0x060002C1 RID: 705
		void SetYPosition(float yCoordinate);

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002C2 RID: 706
		// (set) Token: 0x060002C3 RID: 707
		Vector3 DesiredVelocity { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002C4 RID: 708
		// (set) Token: 0x060002C5 RID: 709
		Vector3 Velocity { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002C6 RID: 710
		// (set) Token: 0x060002C7 RID: 711
		bool Locked { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002C8 RID: 712
		// (set) Token: 0x060002C9 RID: 713
		float Radius { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002CA RID: 714
		// (set) Token: 0x060002CB RID: 715
		float Height { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002CC RID: 716
		// (set) Token: 0x060002CD RID: 717
		float MaxSpeed { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002CE RID: 718
		// (set) Token: 0x060002CF RID: 719
		float NeighbourDist { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002D0 RID: 720
		// (set) Token: 0x060002D1 RID: 721
		float AgentTimeHorizon { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002D2 RID: 722
		// (set) Token: 0x060002D3 RID: 723
		float ObstacleTimeHorizon { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002D4 RID: 724
		// (set) Token: 0x060002D5 RID: 725
		RVOLayer Layer { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002D6 RID: 726
		// (set) Token: 0x060002D7 RID: 727
		RVOLayer CollidesWith { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002D8 RID: 728
		// (set) Token: 0x060002D9 RID: 729
		bool DebugDraw { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002DA RID: 730
		// (set) Token: 0x060002DB RID: 731
		int MaxNeighbours { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002DC RID: 732
		List<ObstacleVertex> NeighbourObstacles { get; }

		// Token: 0x060002DD RID: 733
		void Teleport(Vector3 pos);
	}
}
