using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x0200026D RID: 621
	public class Sleek2MultipleDropdownEnumInspector : Sleek2KeyValueInspector
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x00074E04 File Offset: 0x00073204
		public Sleek2MultipleDropdownEnumInspector()
		{
			base.name = "Multiple_Dropdown_Enum_Inspector";
			this.button = new Sleek2ToolbarButton();
			this.button.transform.reset();
			base.valuePanel.addElement(this.button);
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.button.addElement(this.label);
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00074E8F File Offset: 0x0007328F
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00074E97 File Offset: 0x00073297
		public Sleek2ToolbarButton button { get; protected set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00074EA0 File Offset: 0x000732A0
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00074EA8 File Offset: 0x000732A8
		public Sleek2Label label { get; protected set; }

		// Token: 0x06001237 RID: 4663 RVA: 0x00074EB4 File Offset: 0x000732B4
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			string[] names = Enum.GetNames(base.inspectable.type);
			Array values = Enum.GetValues(base.inspectable.type);
			this.enumButtons = new Sleek2DropdownEnumToolbarButton[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				Sleek2DropdownEnumToolbarButton sleek2DropdownEnumToolbarButton = new Sleek2DropdownEnumToolbarButton(names[i], (int)values.GetValue(i));
				sleek2DropdownEnumToolbarButton.clicked += this.handleEnumButtonClicked;
				this.button.panel.addElement(sleek2DropdownEnumToolbarButton);
				this.enumButtons[i] = sleek2DropdownEnumToolbarButton;
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00074F5C File Offset: 0x0007335C
		public override void refresh()
		{
			if (base.inspectable == null || !base.inspectable.canRead)
			{
				return;
			}
			this.button.label.textComponent.text = base.inspectable.value.ToString();
			if (this.enumButtons == null)
			{
				return;
			}
			int num = (int)base.inspectable.value;
			for (int i = 0; i < this.enumButtons.Length; i++)
			{
				int enumValue = this.enumButtons[i].enumValue;
				this.enumButtons[i].refresh((num & enumValue) == enumValue);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00075004 File Offset: 0x00073404
		protected virtual void handleEnumButtonClicked(Sleek2ImageButton button)
		{
			int enumValue = (button as Sleek2DropdownEnumToolbarButton).enumValue;
			int num = (int)base.inspectable.value;
			if ((num & enumValue) == enumValue)
			{
				num &= ~enumValue;
			}
			else
			{
				num |= enumValue;
			}
			base.inspectable.value = num;
		}

		// Token: 0x04000A96 RID: 2710
		protected Sleek2DropdownEnumToolbarButton[] enumButtons;
	}
}
