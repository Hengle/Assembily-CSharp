using System;
using System.Diagnostics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000F5 RID: 245
	public class Profile
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0004C78C File Offset: 0x0004AB8C
		public Profile(string name)
		{
			this.name = name;
			this.w = new Stopwatch();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0004C7B1 File Offset: 0x0004ABB1
		public int ControlValue()
		{
			return this.control;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0004C7B9 File Offset: 0x0004ABB9
		[Conditional("PROFILE")]
		public void Start()
		{
			if (this.dontCountFirst && this.counter == 1)
			{
				return;
			}
			this.w.Start();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0004C7DE File Offset: 0x0004ABDE
		[Conditional("PROFILE")]
		public void Stop()
		{
			this.counter++;
			if (this.dontCountFirst && this.counter == 1)
			{
				return;
			}
			this.w.Stop();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0004C811 File Offset: 0x0004AC11
		[Conditional("PROFILE")]
		public void Log()
		{
			UnityEngine.Debug.Log(this.ToString());
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0004C81E File Offset: 0x0004AC1E
		[Conditional("PROFILE")]
		public void ConsoleLog()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0004C82C File Offset: 0x0004AC2C
		[Conditional("PROFILE")]
		public void Stop(int control)
		{
			this.counter++;
			if (this.dontCountFirst && this.counter == 1)
			{
				return;
			}
			this.w.Stop();
			if (this.control == 1073741824)
			{
				this.control = control;
			}
			else if (this.control != control)
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match ",
					this.control,
					" != ",
					control
				}));
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0004C8CC File Offset: 0x0004ACCC
		[Conditional("PROFILE")]
		public void Control(Profile other)
		{
			if (this.ControlValue() != other.ControlValue())
			{
				throw new Exception(string.Concat(new object[]
				{
					"Control numbers do not match (",
					this.name,
					" ",
					other.name,
					") ",
					this.ControlValue(),
					" != ",
					other.ControlValue()
				}));
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0004C94C File Offset: 0x0004AD4C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.name,
				" #",
				this.counter,
				" ",
				this.w.Elapsed.TotalMilliseconds.ToString("0.0 ms"),
				" avg: ",
				(this.w.Elapsed.TotalMilliseconds / (double)this.counter).ToString("0.00 ms")
			});
		}

		// Token: 0x0400069F RID: 1695
		private const bool PROFILE_MEM = false;

		// Token: 0x040006A0 RID: 1696
		public string name;

		// Token: 0x040006A1 RID: 1697
		private Stopwatch w;

		// Token: 0x040006A2 RID: 1698
		private int counter;

		// Token: 0x040006A3 RID: 1699
		private long mem;

		// Token: 0x040006A4 RID: 1700
		private long smem;

		// Token: 0x040006A5 RID: 1701
		private int control = 1073741824;

		// Token: 0x040006A6 RID: 1702
		private bool dontCountFirst;
	}
}
