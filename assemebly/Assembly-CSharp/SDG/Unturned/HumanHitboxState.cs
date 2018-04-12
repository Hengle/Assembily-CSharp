using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000436 RID: 1078
	public class HumanHitboxState
	{
		// Token: 0x06001DBA RID: 7610 RVA: 0x000A08BC File Offset: 0x0009ECBC
		public HumanHitboxState(int size)
		{
			this.bones = new HumanBoneState[size];
			for (int i = 0; i < size; i++)
			{
				this.bones[i] = new HumanBoneState();
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x000A08FC File Offset: 0x0009ECFC
		public void update(Transform[] newBones)
		{
			for (int i = 0; i < this.bones.Length; i++)
			{
				this.bones[i].position = newBones[i].localPosition;
				this.bones[i].rotation = newBones[i].localRotation;
			}
		}

		// Token: 0x040011CB RID: 4555
		public float angle;

		// Token: 0x040011CC RID: 4556
		public HumanBoneState[] bones;

		// Token: 0x040011CD RID: 4557
		public float net;
	}
}
