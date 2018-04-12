using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000531 RID: 1329
	public class GermanyTeleporterA : MonoBehaviour
	{
		// Token: 0x060023D7 RID: 9175 RVA: 0x000C6DA4 File Offset: 0x000C51A4
		protected virtual IEnumerator teleport()
		{
			yield return new WaitForSeconds(1f);
			if (this.target != null)
			{
				GermanyTeleporterA.nearbyPlayers.Clear();
				PlayerTool.getPlayersInRadius(base.transform.position, this.sqrRadius, GermanyTeleporterA.nearbyPlayers);
				for (int i = 0; i < GermanyTeleporterA.nearbyPlayers.Count; i++)
				{
					Player player = GermanyTeleporterA.nearbyPlayers[i];
					if (!player.life.isDead)
					{
						if (player.quests.getQuestStatus(248) != ENPCQuestStatus.COMPLETED)
						{
							player.quests.sendAddQuest(248);
						}
						player.sendTeleport(this.target.position, MeasurementTool.angleToByte(this.target.rotation.eulerAngles.y));
					}
				}
			}
			yield break;
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000C6DC0 File Offset: 0x000C51C0
		protected virtual void handleEventTriggered(string id)
		{
			if (id != this.eventID)
			{
				return;
			}
			if (Time.realtimeSinceStartup - this.lastTeleport < 5f)
			{
				return;
			}
			this.lastTeleport = Time.realtimeSinceStartup;
			EffectManager.sendEffect(this.teleportEffect, 16f, base.transform.position);
			base.StartCoroutine("teleport");
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000C6E28 File Offset: 0x000C5228
		protected virtual void OnEnable()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (!this.isListening)
			{
				NPCEventManager.eventTriggered += this.handleEventTriggered;
				this.isListening = true;
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000C6E59 File Offset: 0x000C5259
		protected virtual void OnDisable()
		{
			if (this.isListening)
			{
				NPCEventManager.eventTriggered -= this.handleEventTriggered;
				this.isListening = false;
			}
		}

		// Token: 0x040015E5 RID: 5605
		protected static List<Player> nearbyPlayers = new List<Player>();

		// Token: 0x040015E6 RID: 5606
		public Transform target;

		// Token: 0x040015E7 RID: 5607
		public float sqrRadius;

		// Token: 0x040015E8 RID: 5608
		public string eventID;

		// Token: 0x040015E9 RID: 5609
		public ushort teleportEffect;

		// Token: 0x040015EA RID: 5610
		private float lastTeleport;

		// Token: 0x040015EB RID: 5611
		private bool isListening;
	}
}
