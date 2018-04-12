using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000DE RID: 222
	public class FleePath : RandomPath
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00048068 File Offset: 0x00046468
		[Obsolete("Please use the Construct method instead")]
		public FleePath(Vector3 start, Vector3 avoid, int length, OnPathDelegate callbackDelegate = null) : base(start, length, callbackDelegate)
		{
			throw new Exception("Please use the Construct method instead");
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0004807E File Offset: 0x0004647E
		public FleePath()
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00048088 File Offset: 0x00046488
		public static FleePath Construct(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback = null)
		{
			FleePath path = PathPool<FleePath>.GetPath();
			path.Setup(start, avoid, searchLength, callback);
			return path;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000480A8 File Offset: 0x000464A8
		protected void Setup(Vector3 start, Vector3 avoid, int searchLength, OnPathDelegate callback)
		{
			base.Setup(start, searchLength, callback);
			this.aim = avoid - start;
			this.aim *= 10f;
			this.aim = start - this.aim;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000480F5 File Offset: 0x000464F5
		protected override void Recycle()
		{
			PathPool<FleePath>.Recycle(this);
		}
	}
}
