using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000722 RID: 1826
	public class Rocket : MonoBehaviour
	{
		// Token: 0x060033B3 RID: 13235 RVA: 0x0014F4CC File Offset: 0x0014D8CC
		private void OnTriggerEnter(Collider other)
		{
			if (this.isExploded)
			{
				return;
			}
			if (other.isTrigger)
			{
				return;
			}
			if (other.transform == this.ignoreTransform)
			{
				return;
			}
			this.isExploded = true;
			if (Provider.isServer)
			{
				Vector3 point = this.lastPos;
				float damageRadius = this.range;
				EDeathCause cause = EDeathCause.MISSILE;
				CSteamID csteamID = this.killer;
				float num = this.playerDamage;
				float num2 = this.zombieDamage;
				float num3 = this.animalDamage;
				float num4 = this.barricadeDamage;
				float num5 = this.structureDamage;
				float num6 = this.vehicleDamage;
				float num7 = this.resourceDamage;
				float num8 = this.objectDamage;
				bool flag = this.penetrateBuildables;
				List<EPlayerKill> list;
				DamageTool.explode(point, damageRadius, cause, csteamID, num, num2, num3, num4, num5, num6, num7, num8, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, flag);
				EffectManager.sendEffect(this.explosion, EffectManager.LARGE, this.lastPos);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x0014F5C4 File Offset: 0x0014D9C4
		private void FixedUpdate()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x0014F5D7 File Offset: 0x0014D9D7
		private void Awake()
		{
			this.lastPos = base.transform.position;
		}

		// Token: 0x0400231B RID: 8987
		public CSteamID killer;

		// Token: 0x0400231C RID: 8988
		public float range;

		// Token: 0x0400231D RID: 8989
		public float playerDamage;

		// Token: 0x0400231E RID: 8990
		public float zombieDamage;

		// Token: 0x0400231F RID: 8991
		public float animalDamage;

		// Token: 0x04002320 RID: 8992
		public float barricadeDamage;

		// Token: 0x04002321 RID: 8993
		public float structureDamage;

		// Token: 0x04002322 RID: 8994
		public float vehicleDamage;

		// Token: 0x04002323 RID: 8995
		public float resourceDamage;

		// Token: 0x04002324 RID: 8996
		public float objectDamage;

		// Token: 0x04002325 RID: 8997
		public ushort explosion;

		// Token: 0x04002326 RID: 8998
		public bool penetrateBuildables;

		// Token: 0x04002327 RID: 8999
		public Transform ignoreTransform;

		// Token: 0x04002328 RID: 9000
		private bool isExploded;

		// Token: 0x04002329 RID: 9001
		private Vector3 lastPos;
	}
}
