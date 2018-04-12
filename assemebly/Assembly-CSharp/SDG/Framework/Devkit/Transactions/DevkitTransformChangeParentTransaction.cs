using System;
using UnityEngine;

namespace SDG.Framework.Devkit.Transactions
{
	// Token: 0x02000183 RID: 387
	public class DevkitTransformChangeParentTransaction : IDevkitTransaction
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0005ABD6 File Offset: 0x00058FD6
		public DevkitTransformChangeParentTransaction(Transform newTransform, Transform newParent)
		{
			this.transform = newTransform;
			this.parentAfter = new TransformDelta(newParent);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0005ABF1 File Offset: 0x00058FF1
		public bool delta
		{
			get
			{
				return this.parentBefore.parent != this.parentAfter.parent;
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0005AC0E File Offset: 0x0005900E
		public void undo()
		{
			this.parentBefore.set(this.transform);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0005AC21 File Offset: 0x00059021
		public void redo()
		{
			this.parentAfter.set(this.transform);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0005AC34 File Offset: 0x00059034
		public void begin()
		{
			this.parentBefore = new TransformDelta(this.transform.parent);
			this.parentBefore.get(this.transform);
			this.transform.parent = this.parentAfter.parent;
			this.parentAfter.get(this.transform);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0005AC8F File Offset: 0x0005908F
		public void end()
		{
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0005AC91 File Offset: 0x00059091
		public void forget()
		{
		}

		// Token: 0x0400084E RID: 2126
		protected Transform transform;

		// Token: 0x0400084F RID: 2127
		protected TransformDelta parentBefore;

		// Token: 0x04000850 RID: 2128
		protected TransformDelta parentAfter;
	}
}
