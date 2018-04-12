using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SDG.Framework.UI.Devkit.TerminalUI
{
	// Token: 0x0200028A RID: 650
	public class TerminalFilterToggle
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x000797A8 File Offset: 0x00077BA8
		public TerminalFilterToggle(string newCategory, Toggle newToggle, Button newClearButton)
		{
			this.category = newCategory;
			this.toggle = newToggle;
			this.clearButton = newClearButton;
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.onValueChanged));
			this.clearButton.onClick.AddListener(new UnityAction(this.onClearClicked));
		}

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600130C RID: 4876 RVA: 0x00079808 File Offset: 0x00077C08
		// (remove) Token: 0x0600130D RID: 4877 RVA: 0x00079840 File Offset: 0x00077C40
		public event TerminalFilterChanged onTerminalFilterChanged;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x0600130E RID: 4878 RVA: 0x00079878 File Offset: 0x00077C78
		// (remove) Token: 0x0600130F RID: 4879 RVA: 0x000798B0 File Offset: 0x00077CB0
		public event TerminalFilterCleared onTerminalFilterCleared;

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000798E6 File Offset: 0x00077CE6
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000798EE File Offset: 0x00077CEE
		public string category { get; protected set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000798F7 File Offset: 0x00077CF7
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00079904 File Offset: 0x00077D04
		public bool value
		{
			get
			{
				return this.toggle.isOn;
			}
			set
			{
				this.toggle.isOn = value;
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00079912 File Offset: 0x00077D12
		private void onValueChanged(bool value)
		{
			if (this.onTerminalFilterChanged != null)
			{
				this.onTerminalFilterChanged(this.category, value);
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00079931 File Offset: 0x00077D31
		private void onClearClicked()
		{
			if (this.onTerminalFilterCleared != null)
			{
				this.onTerminalFilterCleared(this.category);
			}
		}

		// Token: 0x04000AEE RID: 2798
		private Toggle toggle;

		// Token: 0x04000AEF RID: 2799
		private Button clearButton;
	}
}
