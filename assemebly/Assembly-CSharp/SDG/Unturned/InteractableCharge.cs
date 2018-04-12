using System;
using System.Collections.Generic;
using HighlightingSystem;
using Steamworks;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020004C8 RID: 1224
	public class InteractableCharge : Interactable
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x000B356C File Offset: 0x000B196C
		public bool hasOwnership
		{
			get
			{
				return OwnershipTool.checkToggle(this.owner, this.group);
			}
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x000B3580 File Offset: 0x000B1980
		public override void updateState(Asset asset, byte[] state)
		{
			this.range2 = ((ItemChargeAsset)asset).range2;
			this.playerDamage = ((ItemChargeAsset)asset).playerDamage;
			this.zombieDamage = ((ItemChargeAsset)asset).zombieDamage;
			this.animalDamage = ((ItemChargeAsset)asset).animalDamage;
			this.barricadeDamage = ((ItemChargeAsset)asset).barricadeDamage;
			this.structureDamage = ((ItemChargeAsset)asset).structureDamage;
			this.vehicleDamage = ((ItemChargeAsset)asset).vehicleDamage;
			this.resourceDamage = ((ItemChargeAsset)asset).resourceDamage;
			this.objectDamage = ((ItemChargeAsset)asset).objectDamage;
			this.explosion2 = ((ItemChargeAsset)asset).explosion2;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x000B3637 File Offset: 0x000B1A37
		public override bool checkInteractable()
		{
			return false;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000B363C File Offset: 0x000B1A3C
		public void detonate(CSteamID killer)
		{
			EffectManager.sendEffect(this.explosion2, EffectManager.LARGE, base.transform.position);
			List<EPlayerKill> list;
			DamageTool.explode(base.transform.position, this.range2, EDeathCause.CHARGE, killer, this.playerDamage, this.zombieDamage, this.animalDamage, this.barricadeDamage, this.structureDamage, this.vehicleDamage, this.resourceDamage, this.objectDamage, out list, EExplosionDamageType.CONVENTIONAL, 32f, true, false);
			BarricadeManager.damage(base.transform, 5f, 1f, false);
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x000B36CD File Offset: 0x000B1ACD
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x000B36D5 File Offset: 0x000B1AD5
		public bool isSelected { get; private set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000B36DE File Offset: 0x000B1ADE
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x000B36E6 File Offset: 0x000B1AE6
		public bool isTargeted { get; private set; }

		// Token: 0x060020B9 RID: 8377 RVA: 0x000B36EF File Offset: 0x000B1AEF
		public void select()
		{
			if (this.isSelected)
			{
				return;
			}
			this.isSelected = true;
			this.updateHighlight();
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000B370A File Offset: 0x000B1B0A
		public void deselect()
		{
			if (!this.isSelected)
			{
				return;
			}
			this.isSelected = false;
			this.updateHighlight();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000B3725 File Offset: 0x000B1B25
		public void target()
		{
			if (this.isTargeted)
			{
				return;
			}
			this.isTargeted = true;
			this.updateHighlight();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000B3740 File Offset: 0x000B1B40
		public void untarget()
		{
			if (!this.isTargeted)
			{
				return;
			}
			this.isTargeted = false;
			this.updateHighlight();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x000B375B File Offset: 0x000B1B5B
		public void highlight()
		{
			if (this.highlighter != null)
			{
				return;
			}
			this.highlighter = base.gameObject.AddComponent<Highlighter>();
			this.updateHighlight();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x000B3786 File Offset: 0x000B1B86
		public void unhighlight()
		{
			if (this.highlighter == null)
			{
				return;
			}
			UnityEngine.Object.DestroyImmediate(this.highlighter);
			this.highlighter = null;
			this.isSelected = false;
			this.isTargeted = false;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000B37BC File Offset: 0x000B1BBC
		private void updateHighlight()
		{
			if (this.highlighter == null)
			{
				return;
			}
			if (this.isSelected)
			{
				if (this.isTargeted)
				{
					this.highlighter.ConstantOn(Color.cyan);
				}
				else
				{
					this.highlighter.ConstantOn(Color.green);
				}
			}
			else if (this.isTargeted)
			{
				this.highlighter.ConstantOn(Color.yellow);
			}
			else
			{
				this.highlighter.ConstantOn(Color.red);
			}
		}

		// Token: 0x04001378 RID: 4984
		public ulong owner;

		// Token: 0x04001379 RID: 4985
		public ulong group;

		// Token: 0x0400137A RID: 4986
		private float range2;

		// Token: 0x0400137B RID: 4987
		private float playerDamage;

		// Token: 0x0400137C RID: 4988
		private float zombieDamage;

		// Token: 0x0400137D RID: 4989
		private float animalDamage;

		// Token: 0x0400137E RID: 4990
		private float barricadeDamage;

		// Token: 0x0400137F RID: 4991
		private float structureDamage;

		// Token: 0x04001380 RID: 4992
		private float vehicleDamage;

		// Token: 0x04001381 RID: 4993
		private float resourceDamage;

		// Token: 0x04001382 RID: 4994
		private float objectDamage;

		// Token: 0x04001383 RID: 4995
		private ushort explosion2;

		// Token: 0x04001386 RID: 4998
		private Highlighter highlighter;
	}
}
