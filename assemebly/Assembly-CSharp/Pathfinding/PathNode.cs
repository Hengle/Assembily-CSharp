using System;

namespace Pathfinding
{
	// Token: 0x02000036 RID: 54
	public class PathNode
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001479F File Offset: 0x00012B9F
		// (set) Token: 0x0600026D RID: 621 RVA: 0x000147AD File Offset: 0x00012BAD
		public uint cost
		{
			get
			{
				return this.flags & 268435455u;
			}
			set
			{
				this.flags = ((this.flags & 4026531840u) | value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000147C3 File Offset: 0x00012BC3
		// (set) Token: 0x0600026F RID: 623 RVA: 0x000147D7 File Offset: 0x00012BD7
		public bool flag1
		{
			get
			{
				return (this.flags & 268435456u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 4026531839u) | ((!value) ? 0u : 268435456u));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000147FD File Offset: 0x00012BFD
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00014811 File Offset: 0x00012C11
		public bool flag2
		{
			get
			{
				return (this.flags & 536870912u) != 0u;
			}
			set
			{
				this.flags = ((this.flags & 3758096383u) | ((!value) ? 0u : 536870912u));
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00014837 File Offset: 0x00012C37
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0001483F File Offset: 0x00012C3F
		public uint G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00014848 File Offset: 0x00012C48
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00014850 File Offset: 0x00012C50
		public uint H
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00014859 File Offset: 0x00012C59
		public uint F
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x040001B6 RID: 438
		public GraphNode node;

		// Token: 0x040001B7 RID: 439
		public PathNode parent;

		// Token: 0x040001B8 RID: 440
		public ushort pathID;

		// Token: 0x040001B9 RID: 441
		private uint flags;

		// Token: 0x040001BA RID: 442
		private const uint CostMask = 268435455u;

		// Token: 0x040001BB RID: 443
		private const int Flag1Offset = 28;

		// Token: 0x040001BC RID: 444
		private const uint Flag1Mask = 268435456u;

		// Token: 0x040001BD RID: 445
		private const int Flag2Offset = 29;

		// Token: 0x040001BE RID: 446
		private const uint Flag2Mask = 536870912u;

		// Token: 0x040001BF RID: 447
		private uint g;

		// Token: 0x040001C0 RID: 448
		private uint h;
	}
}
