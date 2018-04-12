using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020007BC RID: 1980
	public class UseableOptic : Useable
	{
		// Token: 0x060039DE RID: 14814 RVA: 0x001BA16A File Offset: 0x001B856A
		public override void startSecondary()
		{
			if (base.channel.isOwner && !this.isZoomed && base.player.look.perspective == EPlayerPerspective.FIRST)
			{
				this.isZoomed = true;
				this.startZoom();
			}
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x001BA1A9 File Offset: 0x001B85A9
		public override void stopSecondary()
		{
			if (base.channel.isOwner && this.isZoomed)
			{
				this.isZoomed = false;
				this.stopZoom();
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x001BA1D4 File Offset: 0x001B85D4
		private void startZoom()
		{
			base.player.animator.viewOffset = Vector3.up;
			base.player.animator.multiplier = 0f;
			base.player.look.enableZoom(((ItemOpticAsset)base.player.equipment.asset).zoom);
			base.player.look.sensitivity = ((ItemOpticAsset)base.player.equipment.asset).zoom / 90f;
			PlayerUI.updateBinoculars(true);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x001BA26C File Offset: 0x001B866C
		private void stopZoom()
		{
			base.player.animator.viewOffset = Vector3.zero;
			base.player.animator.multiplier = 1f;
			base.player.look.disableZoom();
			base.player.look.sensitivity = 1f;
			PlayerUI.updateBinoculars(false);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x001BA2CE File Offset: 0x001B86CE
		private void onPerspectiveUpdated(EPlayerPerspective newPerspective)
		{
			if (this.isZoomed && newPerspective == EPlayerPerspective.THIRD)
			{
				this.stopZoom();
			}
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x001BA2E8 File Offset: 0x001B86E8
		public override void equip()
		{
			base.player.animator.play("Equip", true);
			if (base.channel.isOwner)
			{
				PlayerLook look = base.player.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Combine(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x001BA348 File Offset: 0x001B8748
		public override void dequip()
		{
			if (base.channel.isOwner)
			{
				if (this.isZoomed)
				{
					this.stopZoom();
				}
				PlayerLook look = base.player.look;
				look.onPerspectiveUpdated = (PerspectiveUpdated)Delegate.Remove(look.onPerspectiveUpdated, new PerspectiveUpdated(this.onPerspectiveUpdated));
			}
		}

		// Token: 0x04002CE1 RID: 11489
		private bool isZoomed;
	}
}
