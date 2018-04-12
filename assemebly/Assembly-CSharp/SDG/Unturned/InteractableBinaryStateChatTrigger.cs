using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000537 RID: 1335
	public class InteractableBinaryStateChatTrigger : MonoBehaviour
	{
		// Token: 0x060023EC RID: 9196 RVA: 0x000C7630 File Offset: 0x000C5A30
		private void onChatted(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool isRich, string text, ref bool isVisible)
		{
			if (mode != EChatMode.LOCAL || player.player == null)
			{
				return;
			}
			if ((player.player.transform.position - base.transform.position).sqrMagnitude > this.sqrRadius)
			{
				return;
			}
			if (text.IndexOf(this.phrase, StringComparison.OrdinalIgnoreCase) == -1)
			{
				return;
			}
			if (LightingManager.day < LevelLighting.bias)
			{
				return;
			}
			ObjectManager.forceObjectBinaryState(this.target.transform, !this.target.isUsed);
			isVisible = false;
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000C76D4 File Offset: 0x000C5AD4
		private void OnEnable()
		{
			if (!Provider.isServer)
			{
				return;
			}
			if (this.target != null)
			{
				return;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(base.transform.position, out b, out b2))
			{
				for (int i = 0; i < LevelObjects.objects[(int)b, (int)b2].Count; i++)
				{
					LevelObject levelObject = LevelObjects.objects[(int)b, (int)b2][i];
					if (levelObject.interactable != null && levelObject.id == this.id)
					{
						this.target = (levelObject.interactable as InteractableObjectBinaryState);
						break;
					}
				}
			}
			if (this.target == null)
			{
				return;
			}
			if (!this.isListening)
			{
				ChatManager.onChatted = (Chatted)Delegate.Combine(ChatManager.onChatted, new Chatted(this.onChatted));
				this.isListening = true;
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000C77C8 File Offset: 0x000C5BC8
		private void OnDisable()
		{
			this.target = null;
			if (this.isListening)
			{
				ChatManager.onChatted = (Chatted)Delegate.Remove(ChatManager.onChatted, new Chatted(this.onChatted));
				this.isListening = false;
			}
		}

		// Token: 0x04001618 RID: 5656
		public ushort id;

		// Token: 0x04001619 RID: 5657
		public string phrase;

		// Token: 0x0400161A RID: 5658
		public float sqrRadius;

		// Token: 0x0400161B RID: 5659
		private InteractableObjectBinaryState target;

		// Token: 0x0400161C RID: 5660
		private bool isListening;
	}
}
