using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000270 RID: 624
	public class Sleek2SingleCycleEnumInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x00075280 File Offset: 0x00073680
		public Sleek2SingleCycleEnumInspector()
		{
			base.name = "Single_Cycle_Enum_Inspector";
			this.button = new Sleek2ImageButton();
			this.button.transform.reset();
			this.button.clicked += this.handleButtonClicked;
			base.valuePanel.addElement(this.button);
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.button.addElement(this.label);
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00075322 File Offset: 0x00073722
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0007532A File Offset: 0x0007372A
		public Sleek2ImageButton button { get; protected set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00075333 File Offset: 0x00073733
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0007533B File Offset: 0x0007373B
		public Sleek2Label label { get; protected set; }

		// Token: 0x0600124B RID: 4683 RVA: 0x00075344 File Offset: 0x00073744
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			this.button.buttonComponent.interactable = base.inspectable.canWrite;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00075374 File Offset: 0x00073774
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			this.label.textComponent.text = base.inspectable.value.ToString();
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000753B4 File Offset: 0x000737B4
		protected void handleButtonClicked(Sleek2ImageButton button)
		{
			int num = (int)base.inspectable.value;
			num++;
			if (num >= Enum.GetValues(base.inspectable.type).Length)
			{
				num = 0;
			}
			base.inspectable.value = num;
		}
	}
}
