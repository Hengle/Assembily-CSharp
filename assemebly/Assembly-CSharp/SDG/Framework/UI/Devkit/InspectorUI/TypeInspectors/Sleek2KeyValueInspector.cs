using System;
using SDG.Framework.Translations;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000265 RID: 613
	public abstract class Sleek2KeyValueInspector : Sleek2TypeInspector
	{
		// Token: 0x060011FF RID: 4607 RVA: 0x0007342C File Offset: 0x0007182C
		public Sleek2KeyValueInspector()
		{
			this.keyPanel = new Sleek2Element();
			this.keyPanel.transform.anchorMin = new Vector2(0f, 0f);
			this.keyPanel.transform.anchorMax = new Vector2(0f, 1f);
			this.keyPanel.transform.pivot = new Vector2(0f, 1f);
			this.keyPanel.transform.sizeDelta = new Vector2(0f, 0f);
			this.keyPanel.name = "Key_Panel";
			this.addElement(this.keyPanel);
			this.valuePanel = new Sleek2Element();
			this.valuePanel.transform.anchorMin = new Vector2(1f, 0f);
			this.valuePanel.transform.anchorMax = new Vector2(1f, 1f);
			this.valuePanel.transform.pivot = new Vector2(0f, 1f);
			this.valuePanel.transform.sizeDelta = new Vector2(0f, 0f);
			this.valuePanel.name = "Value_Panel";
			this.addElement(this.valuePanel);
			this.keyLabel = new Sleek2TranslatedLabel();
			this.keyLabel.transform.reset();
			this.keyLabel.textComponent.alignment = TextAnchor.MiddleLeft;
			this.keyLabel.translation = new TranslatedText(default(TranslationReference));
			this.keyPanel.addElement(this.keyLabel);
			this.layoutComponent.preferredHeight = 30f;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x000735EB File Offset: 0x000719EB
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x000735F3 File Offset: 0x000719F3
		public Sleek2Element keyPanel { get; protected set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000735FC File Offset: 0x000719FC
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x00073604 File Offset: 0x00071A04
		public Sleek2Element valuePanel { get; protected set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0007360D File Offset: 0x00071A0D
		// (set) Token: 0x06001205 RID: 4613 RVA: 0x00073615 File Offset: 0x00071A15
		public Sleek2TranslatedLabel keyLabel { get; protected set; }

		// Token: 0x06001206 RID: 4614 RVA: 0x0007361E File Offset: 0x00071A1E
		public override void split(float value)
		{
			this.keyPanel.transform.anchorMax = new Vector2(value, 1f);
			this.valuePanel.transform.anchorMin = new Vector2(value, 0f);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00073658 File Offset: 0x00071A58
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspectable = newInspectable;
			this.refresh();
			if (base.inspectable == null)
			{
				return;
			}
			this.keyLabel.translation.reference = newInspectable.name;
			this.keyLabel.translation.format();
			if (base.inspectable.tooltip.isValid)
			{
				if (this.keyLabel.tooltip == null)
				{
					this.keyLabel.tooltip = new TranslatedText(default(TranslationReference));
				}
				this.keyLabel.tooltip.reference = base.inspectable.tooltip;
				this.keyLabel.tooltip.format();
			}
			else
			{
				this.keyLabel.tooltip = null;
			}
		}
	}
}
