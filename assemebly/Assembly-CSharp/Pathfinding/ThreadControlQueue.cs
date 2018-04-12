using System;
using System.Threading;

namespace Pathfinding
{
	// Token: 0x0200002D RID: 45
	public class ThreadControlQueue
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x00012F64 File Offset: 0x00011364
		public ThreadControlQueue(int numReceivers)
		{
			this.numReceivers = numReceivers;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00012F8A File Offset: 0x0001138A
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00012F95 File Offset: 0x00011395
		public bool IsTerminating
		{
			get
			{
				return this.terminate;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00012FA0 File Offset: 0x000113A0
		public void Block()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = true;
				this.block.Reset();
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00012FEC File Offset: 0x000113EC
		public void Unblock()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = false;
				this.block.Set();
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00013038 File Offset: 0x00011438
		public void Lock()
		{
			Monitor.Enter(this.lockObj);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00013045 File Offset: 0x00011445
		public void Unlock()
		{
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00013052 File Offset: 0x00011452
		public bool AllReceiversBlocked
		{
			get
			{
				return this.blocked && this.blockedReceivers == this.numReceivers;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00013070 File Offset: 0x00011470
		public void PushFront(Path p)
		{
			if (this.terminate)
			{
				return;
			}
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.tail == null)
				{
					this.head = p;
					this.tail = p;
					if (this.starving && !this.blocked)
					{
						this.starving = false;
						this.block.Set();
					}
					else
					{
						this.starving = false;
					}
				}
				else
				{
					p.next = this.head;
					this.head = p;
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00013118 File Offset: 0x00011518
		public void Push(Path p)
		{
			if (this.terminate)
			{
				return;
			}
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.tail == null)
				{
					this.head = p;
					this.tail = p;
					if (this.starving && !this.blocked)
					{
						this.starving = false;
						this.block.Set();
					}
					else
					{
						this.starving = false;
					}
				}
				else
				{
					this.tail.next = p;
					this.tail = p;
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000131C0 File Offset: 0x000115C0
		private void Starving()
		{
			this.starving = true;
			this.block.Reset();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000131D5 File Offset: 0x000115D5
		public void TerminateReceivers()
		{
			this.terminate = true;
			this.block.Set();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000131EC File Offset: 0x000115EC
		public Path Pop()
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				while (this.blocked || this.starving)
				{
					this.blockedReceivers++;
					if (this.terminate)
					{
						throw new ThreadControlQueue.QueueTerminationException();
					}
					if (this.blockedReceivers != this.numReceivers)
					{
						if (this.blockedReceivers > this.numReceivers)
						{
							throw new InvalidOperationException(string.Concat(new object[]
							{
								"More receivers are blocked than specified in constructor (",
								this.blockedReceivers,
								" > ",
								this.numReceivers,
								")"
							}));
						}
					}
					Monitor.Exit(this.lockObj);
					this.block.WaitOne();
					Monitor.Enter(this.lockObj);
					this.blockedReceivers--;
					if (this.head == null)
					{
						this.Starving();
					}
				}
				Path path = this.head;
				if (this.head.next == null)
				{
					this.tail = null;
				}
				this.head = this.head.next;
				result = path;
			}
			return result;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001337C File Offset: 0x0001177C
		public void ReceiverTerminated()
		{
			Monitor.Enter(this.lockObj);
			this.blockedReceivers++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000133A4 File Offset: 0x000117A4
		public Path PopNoBlock(bool blockedBefore)
		{
			Path result;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				if (this.blocked || this.starving)
				{
					if (!blockedBefore)
					{
						this.blockedReceivers++;
						if (this.terminate)
						{
							throw new ThreadControlQueue.QueueTerminationException();
						}
						if (this.blockedReceivers != this.numReceivers)
						{
							if (this.blockedReceivers > this.numReceivers)
							{
								throw new InvalidOperationException(string.Concat(new object[]
								{
									"More receivers are blocked than specified in constructor (",
									this.blockedReceivers,
									" > ",
									this.numReceivers,
									")"
								}));
							}
						}
					}
					result = null;
				}
				else
				{
					if (blockedBefore)
					{
						this.blockedReceivers--;
					}
					Path path = this.head;
					if (this.head.next == null)
					{
						this.tail = null;
					}
					this.head = this.head.next;
					result = path;
				}
			}
			return result;
		}

		// Token: 0x04000178 RID: 376
		private Path head;

		// Token: 0x04000179 RID: 377
		private Path tail;

		// Token: 0x0400017A RID: 378
		private object lockObj = new object();

		// Token: 0x0400017B RID: 379
		private int numReceivers;

		// Token: 0x0400017C RID: 380
		private bool blocked;

		// Token: 0x0400017D RID: 381
		private int blockedReceivers;

		// Token: 0x0400017E RID: 382
		private bool starving;

		// Token: 0x0400017F RID: 383
		private bool terminate;

		// Token: 0x04000180 RID: 384
		private ManualResetEvent block = new ManualResetEvent(true);

		// Token: 0x0200002E RID: 46
		public class QueueTerminationException : Exception
		{
		}
	}
}
