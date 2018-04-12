using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000530 RID: 1328
	public class FlickeringLight : MonoBehaviour
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x000C6C90 File Offset: 0x000C5090
		private void Update()
		{
			float num = UnityEngine.Random.Range(0.9f, 1f);
			if (Time.time - this.blackoutTime < 0.15f)
			{
				num = 0.15f;
			}
			else if (Time.time - this.blackoutTime > this.blackoutDelay)
			{
				this.blackoutTime = Time.time;
				this.blackoutDelay = UnityEngine.Random.Range(7.3f, 13.2f);
			}
			if (this.target != null)
			{
				this.target.intensity = num;
			}
			if (this.material != null)
			{
				this.material.SetColor("_EmissionColor", new Color(num, num, num));
			}
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000C6D4B File Offset: 0x000C514B
		private void Awake()
		{
			this.material = HighlighterTool.getMaterialInstance(base.transform);
			this.blackoutTime = Time.time;
			this.blackoutDelay = UnityEngine.Random.Range(0f, 13.2f);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000C6D7E File Offset: 0x000C517E
		private void OnDestroy()
		{
			if (this.material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.material);
			}
		}

		// Token: 0x040015E1 RID: 5601
		public Light target;

		// Token: 0x040015E2 RID: 5602
		private Material material;

		// Token: 0x040015E3 RID: 5603
		private float blackoutTime;

		// Token: 0x040015E4 RID: 5604
		private float blackoutDelay;
	}
}
