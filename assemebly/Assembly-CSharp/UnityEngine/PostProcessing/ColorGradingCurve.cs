using System;

namespace UnityEngine.PostProcessing
{
	// Token: 0x02000843 RID: 2115
	[Serializable]
	public sealed class ColorGradingCurve
	{
		// Token: 0x06003BD0 RID: 15312 RVA: 0x001CD302 File Offset: 0x001CB702
		public ColorGradingCurve(AnimationCurve curve, float zeroValue, bool loop, Vector2 bounds)
		{
			this.curve = curve;
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			this.m_Range = bounds.magnitude;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x001CD32C File Offset: 0x001CB72C
		public void Cache()
		{
			if (!this.m_Loop)
			{
				return;
			}
			int length = this.curve.length;
			if (length < 2)
			{
				return;
			}
			if (this.m_InternalLoopingCurve == null)
			{
				this.m_InternalLoopingCurve = new AnimationCurve();
			}
			Keyframe key = this.curve[length - 1];
			key.time -= this.m_Range;
			Keyframe key2 = this.curve[0];
			key2.time += this.m_Range;
			this.m_InternalLoopingCurve.keys = this.curve.keys;
			this.m_InternalLoopingCurve.AddKey(key);
			this.m_InternalLoopingCurve.AddKey(key2);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x001CD3E4 File Offset: 0x001CB7E4
		public float Evaluate(float t)
		{
			if (this.curve.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.curve.length == 1)
			{
				return this.curve.Evaluate(t);
			}
			return this.m_InternalLoopingCurve.Evaluate(t);
		}

		// Token: 0x04003056 RID: 12374
		public AnimationCurve curve;

		// Token: 0x04003057 RID: 12375
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04003058 RID: 12376
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x04003059 RID: 12377
		[SerializeField]
		private float m_Range;

		// Token: 0x0400305A RID: 12378
		private AnimationCurve m_InternalLoopingCurve;
	}
}
