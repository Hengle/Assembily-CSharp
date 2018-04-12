using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200055F RID: 1375
	public class LightLOD : MonoBehaviour
	{
		// Token: 0x06002602 RID: 9730 RVA: 0x000DF2B4 File Offset: 0x000DD6B4
		private void apply()
		{
			if (this.targetLight == null || this.targetLight.type == LightType.Area || this.targetLight.type == LightType.Directional)
			{
				return;
			}
			if (MainCamera.instance == null)
			{
				return;
			}
			Vector3 vector = base.transform.position - MainCamera.instance.transform.position;
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude < this.sqrTransitionStart)
			{
				if (!this.targetLight.enabled)
				{
					this.targetLight.intensity = this.intensityStart;
					this.targetLight.enabled = true;
				}
			}
			else if (sqrMagnitude > this.sqrTransitionEnd)
			{
				if (this.targetLight.enabled)
				{
					this.targetLight.intensity = this.intensityEnd;
					this.targetLight.enabled = false;
				}
			}
			else
			{
				float magnitude = vector.magnitude;
				float t = (magnitude - this.transitionStart) / this.transitionMagnitude;
				this.targetLight.intensity = Mathf.Lerp(this.intensityStart, this.intensityEnd, t);
				if (!this.targetLight.enabled)
				{
					this.targetLight.enabled = true;
				}
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000DF3FB File Offset: 0x000DD7FB
		private void Update()
		{
			this.apply();
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000DF404 File Offset: 0x000DD804
		private void Start()
		{
			if (this.targetLight == null || this.targetLight.type == LightType.Area || this.targetLight.type == LightType.Directional)
			{
				base.enabled = false;
				return;
			}
			this.intensityStart = this.targetLight.intensity;
			this.intensityEnd = 0f;
			if (this.targetLight.type == LightType.Point)
			{
				this.transitionStart = this.targetLight.range * 13f;
				this.transitionEnd = this.targetLight.range * 15f;
			}
			else if (this.targetLight.type == LightType.Spot)
			{
				this.transitionStart = Mathf.Max(64f, this.targetLight.range) * 1.75f;
				this.transitionEnd = Mathf.Max(64f, this.targetLight.range) * 2f;
			}
			this.transitionMagnitude = this.transitionEnd - this.transitionStart;
			this.sqrTransitionStart = this.transitionStart * this.transitionStart;
			this.sqrTransitionEnd = this.transitionEnd * this.transitionEnd;
			this.apply();
		}

		// Token: 0x040017A8 RID: 6056
		public Light targetLight;

		// Token: 0x040017A9 RID: 6057
		private float intensityStart;

		// Token: 0x040017AA RID: 6058
		private float intensityEnd;

		// Token: 0x040017AB RID: 6059
		private float transitionStart;

		// Token: 0x040017AC RID: 6060
		private float transitionEnd;

		// Token: 0x040017AD RID: 6061
		private float transitionMagnitude;

		// Token: 0x040017AE RID: 6062
		private float sqrTransitionStart;

		// Token: 0x040017AF RID: 6063
		private float sqrTransitionEnd;
	}
}
