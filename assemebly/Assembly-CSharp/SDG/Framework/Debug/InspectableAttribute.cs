using System;
using SDG.Framework.Translations;

namespace SDG.Framework.Debug
{
	// Token: 0x02000109 RID: 265
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	public class InspectableAttribute : Attribute
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x0004CAFC File Offset: 0x0004AEFC
		public InspectableAttribute(string namePath, string tooltipPath = null)
		{
			this.name = new TranslationReference(namePath);
			this.tooltip = new TranslationReference(tooltipPath);
		}

		// Token: 0x040006C0 RID: 1728
		public TranslationReference name;

		// Token: 0x040006C1 RID: 1729
		public TranslationReference tooltip;
	}
}
