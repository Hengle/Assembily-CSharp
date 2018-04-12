using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004A4 RID: 1188
	public class ReunObjectTransform : IReun
	{
		// Token: 0x06001F50 RID: 8016 RVA: 0x000ADA5C File Offset: 0x000ABE5C
		public ReunObjectTransform(int newStep, Transform newModel, Vector3 newFromPosition, Quaternion newFromRotation, Vector3 newFromScale, Vector3 newToPosition, Quaternion newToRotation, Vector3 newToScale)
		{
			this.step = newStep;
			this.model = newModel;
			this.fromPosition = newFromPosition;
			this.fromRotation = newFromRotation;
			this.fromScale = newFromScale;
			this.toPosition = newToPosition;
			this.toRotation = newToRotation;
			this.toScale = newToScale;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x000ADAAC File Offset: 0x000ABEAC
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x000ADAB4 File Offset: 0x000ABEB4
		public int step { get; private set; }

		// Token: 0x06001F53 RID: 8019 RVA: 0x000ADAC0 File Offset: 0x000ABEC0
		public Transform redo()
		{
			if (this.model != null)
			{
				LevelObjects.transformObject(this.model, this.toPosition, this.toRotation, this.toScale, this.fromPosition, this.fromRotation, this.fromScale);
			}
			return this.model;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000ADB14 File Offset: 0x000ABF14
		public void undo()
		{
			if (this.model != null)
			{
				LevelObjects.transformObject(this.model, this.fromPosition, this.fromRotation, this.fromScale, this.toPosition, this.toRotation, this.toScale);
			}
		}

		// Token: 0x04001300 RID: 4864
		private Transform model;

		// Token: 0x04001301 RID: 4865
		private Vector3 fromPosition;

		// Token: 0x04001302 RID: 4866
		private Quaternion fromRotation;

		// Token: 0x04001303 RID: 4867
		private Vector3 fromScale;

		// Token: 0x04001304 RID: 4868
		private Vector3 toPosition;

		// Token: 0x04001305 RID: 4869
		private Quaternion toRotation;

		// Token: 0x04001306 RID: 4870
		private Vector3 toScale;
	}
}
