using System;

namespace Pathfinding
{
	// Token: 0x02000055 RID: 85
	public struct Progress
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0001ABF1 File Offset: 0x00018FF1
		public Progress(float p, string d)
		{
			this.progress = p;
			this.description = d;
		}

		// Token: 0x040002AA RID: 682
		public float progress;

		// Token: 0x040002AB RID: 683
		public string description;
	}
}
