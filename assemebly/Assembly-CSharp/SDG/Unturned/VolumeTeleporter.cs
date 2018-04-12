using System;
using System.Collections;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000577 RID: 1399
	public class VolumeTeleporter : MonoBehaviour
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x000E4A44 File Offset: 0x000E2E44
		private IEnumerator teleport()
		{
			yield return new WaitForSeconds(3f);
			if (this.target != null && this.playerTeleported != null && !this.playerTeleported.life.isDead)
			{
				this.playerTeleported.sendTeleport(this.target.position, MeasurementTool.angleToByte(this.target.rotation.eulerAngles.y));
				if (this.playerTeleported.equipment.isSelected)
				{
					this.playerTeleported.equipment.dequip();
				}
				this.playerTeleported.equipment.canEquip = true;
			}
			this.playerTeleported = null;
			yield break;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000E4A60 File Offset: 0x000E2E60
		private void OnTriggerEnter(Collider other)
		{
			bool flag;
			if (!Dedicator.isDedicated && !string.IsNullOrEmpty(this.achievement) && other.transform.CompareTag("Player") && other.transform == Player.player.transform && Provider.provider.achievementsService.getAchievement(this.achievement, out flag) && !flag)
			{
				Provider.provider.achievementsService.setAchievement(this.achievement);
			}
			if (Provider.isServer && other.transform.CompareTag("Player") && this.playerTeleported == null)
			{
				EffectManager.sendEffect(this.teleportEffect, 16f, this.effectHook.position);
				this.playerTeleported = DamageTool.getPlayer(other.transform);
				base.StartCoroutine("teleport");
			}
		}

		// Token: 0x04001829 RID: 6185
		public string achievement;

		// Token: 0x0400182A RID: 6186
		public Transform target;

		// Token: 0x0400182B RID: 6187
		public ushort teleportEffect;

		// Token: 0x0400182C RID: 6188
		public Transform effectHook;

		// Token: 0x0400182D RID: 6189
		private Player playerTeleported;
	}
}
