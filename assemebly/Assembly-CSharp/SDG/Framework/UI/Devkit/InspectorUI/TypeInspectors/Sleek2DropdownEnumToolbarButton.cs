using System;

namespace SDG.Framework.UI.Devkit.InspectorUI.TypeInspectors
{
	// Token: 0x02000269 RID: 617
	public class Sleek2DropdownEnumToolbarButton : Sleek2ToolbarLabelButton
	{
		// Token: 0x0600121A RID: 4634 RVA: 0x00074A44 File Offset: 0x00072E44
		public Sleek2DropdownEnumToolbarButton(string newEnumName, int newEnumValue)
		{
			this.enumName = newEnumName;
			this.enumValue = newEnumValue;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00074A5A File Offset: 0x00072E5A
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00074A62 File Offset: 0x00072E62
		public string enumName { get; protected set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00074A6B File Offset: 0x00072E6B
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x00074A73 File Offset: 0x00072E73
		public int enumValue { get; protected set; }

		// Token: 0x0600121F RID: 4639 RVA: 0x00074A7C File Offset: 0x00072E7C
		public void refresh(bool isFlagged)
		{
			base.label.textComponent.text = ((!isFlagged) ? this.enumName : ("[[ " + this.enumName + " ]]"));
		}
	}
}
