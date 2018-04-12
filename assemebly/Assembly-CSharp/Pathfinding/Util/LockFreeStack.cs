using System;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x020000F3 RID: 243
	public class LockFreeStack
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0004BC80 File Offset: 0x0004A080
		public void Push(Path p)
		{
			Path path;
			do
			{
				p.next = this.head;
				path = Interlocked.CompareExchange<Path>(ref this.head, p, p.next);
			}
			while (path != p.next);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0004BCC2 File Offset: 0x0004A0C2
		public Path PopAll()
		{
			return Interlocked.Exchange<Path>(ref this.head, null);
		}

		// Token: 0x0400069C RID: 1692
		public Path head;
	}
}
