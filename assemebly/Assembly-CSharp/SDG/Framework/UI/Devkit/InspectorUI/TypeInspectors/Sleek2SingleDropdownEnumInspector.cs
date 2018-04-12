using System;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000271 RID: 625
	public class Sleek2SingleDropdownEnumInspector : Sleek2KeyValueInspector
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x00075404 File Offset: 0x00073804
		public Sleek2SingleDropdownEnumInspector()
		{
			base.name = "Single_Dropdown_Enum_Inspector";
			this.button = new Sleek2ToolbarButton();
			this.button.transform.reset();
			base.valuePanel.addElement(this.button);
			this.label = new Sleek2Label();
			this.label.transform.reset();
			this.label.textComponent.color = Sleek2Config.darkTextColor;
			this.button.addElement(this.label);
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0007548F File Offset: 0x0007388F
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x00075497 File Offset: 0x00073897
		public Sleek2ToolbarButton button { get; protected set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000754A0 File Offset: 0x000738A0
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x000754A8 File Offset: 0x000738A8
		public Sleek2Label label { get; protected set; }

		// Token: 0x06001253 RID: 4691 RVA: 0x000754B4 File Offset: 0x000738B4
		public override void inspect(ObjectInspectableInfo newInspectable)
		{
			base.inspect(newInspectable);
			if (base.inspectable == null)
			{
				return;
			}
			string[] names = Enum.GetNames(base.inspectable.type);
			this.enumButtons = new Sleek2DropdownEnumToolbarButton[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				Sleek2DropdownEnumToolbarButton sleek2DropdownEnumToolbarButton = new Sleek2DropdownEnumToolbarButton(names[i], i);
				sleek2DropdownEnumToolbarButton.clicked += this.handleEnumButtonClicked;
				this.button.panel.addElement(sleek2DropdownEnumToolbarButton);
				this.enumButtons[i] = sleek2DropdownEnumToolbarButton;
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00075540 File Offset: 0x00073940
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
				this.enumButtons[i].refresh(num == enumValue);
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000755E4 File Offset: 0x000739E4
		protected virtual void handleEnumButtonClicked(Sleek2ImageButton button)
		{
			int enumValue = (button as Sleek2DropdownEnumToolbarButton).enumValue;
			base.inspectable.value = enumValue;
		}

		// Token: 0x04000A9D RID: 2717
		protected Sleek2DropdownEnumToolbarButton[] enumButtons;
	}
}
