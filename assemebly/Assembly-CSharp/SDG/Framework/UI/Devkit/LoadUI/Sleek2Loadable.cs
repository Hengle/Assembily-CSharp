using System;
using SDG.Framework.UI.Sleek2;
using SDG.Unturned;

namespace SDG.Framework.UI.Devkit.LoadUI
{
	// Token: 0x02000281 RID: 641
	public class Sleek2Loadable : Sleek2ImageLabelButton
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x000788EF File Offset: 0x00076CEF
		public Sleek2Loadable(LevelInfo newLevelInfo)
		{
			this.levelInfo = newLevelInfo;
			base.label.textComponent.text = this.levelInfo.name;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00078919 File Offset: 0x00076D19
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00078921 File Offset: 0x00076D21
		public LevelInfo levelInfo { get; protected set; }
	}
}
