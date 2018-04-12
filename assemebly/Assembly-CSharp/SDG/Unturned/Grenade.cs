using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000720 RID: 1824
	public class Grenade : MonoBehaviour, IExplodableThrowable
	{
		// Token: 0x060033AE RID: 13230 RVA: 0x0014F378 File Offset: 0x0014D778
		public void Explode()
		{
			List<EPlayerKill> list;
			DamageTool.explode(base.transform.position, this.range, EDeathCause.GRENADE, this.killer, this.playerDamage, this.zombieDamage, this.animalDamage, this.barricadeDamage, this.structureDamage, this.vehicleDamage, this.resourceDamage, this.objectDamage, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
			EffectManager.sendEffect(this.explosion, EffectManager.LARGE, base.transform.position);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x0014F403 File Offset: 0x0014D803
		private void Start()
		{
			base.Invoke("Explode", this.fuseLength);
		}

		// Token: 0x0400230E RID: 8974
		public CSteamID killer;

		// Token: 0x0400230F RID: 8975
		public float range;

		// Token: 0x04002310 RID: 8976
		public float playerDamage;

		// Token: 0x04002311 RID: 8977
		public float zombieDamage;

		// Token: 0x04002312 RID: 8978
		public float animalDamage;

		// Token: 0x04002313 RID: 8979
		public float barricadeDamage;

		// Token: 0x04002314 RID: 8980
		public float structureDamage;

		// Token: 0x04002315 RID: 8981
		public float vehicleDamage;

		// Token: 0x04002316 RID: 8982
		public float resourceDamage;

		// Token: 0x04002317 RID: 8983
		public float objectDamage;

		// Token: 0x04002318 RID: 8984
		public ushort explosion;

		// Token: 0x04002319 RID: 8985
		public float fuseLength = 2.5f;
	}
}
