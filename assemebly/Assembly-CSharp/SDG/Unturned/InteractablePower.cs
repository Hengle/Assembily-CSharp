using System;
using System.Collections.Generic;

namespace SDG.Unturned
{
	// Token: 0x020004E2 RID: 1250
	public class InteractablePower : Interactable
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000B38DC File Offset: 0x000B1CDC
		public bool isWired
		{
			get
			{
				return this._isWired;
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000B38E4 File Offset: 0x000B1CE4
		protected virtual void updateWired()
		{
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000B38E6 File Offset: 0x000B1CE6
		public void updateWired(bool newWired)
		{
			if (newWired == this.isWired)
			{
				return;
			}
			this._isWired = newWired;
			this.updateWired();
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x000B3904 File Offset: 0x000B1D04
		private void Start()
		{
			if (Level.isEditor)
			{
				this.updateWired(true);
			}
			else
			{
				ushort maxValue = ushort.MaxValue;
				if (base.isPlant)
				{
					byte b;
					byte b2;
					BarricadeRegion barricadeRegion;
					BarricadeManager.tryGetPlant(base.transform.parent, out b, out b2, out maxValue, out barricadeRegion);
				}
				List<InteractableGenerator> list = PowerTool.checkGenerators(base.transform.position, 64f, maxValue);
				for (int i = 0; i < list.Count; i++)
				{
					InteractableGenerator interactableGenerator = list[i];
					if (interactableGenerator.isPowered && interactableGenerator.fuel > 0 && (interactableGenerator.transform.position - base.transform.position).sqrMagnitude < interactableGenerator.sqrWirerange)
					{
						this.updateWired(true);
						return;
					}
				}
			}
		}

		// Token: 0x0400140A RID: 5130
		protected bool _isWired;
	}
}
