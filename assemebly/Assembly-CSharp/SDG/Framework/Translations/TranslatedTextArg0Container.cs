using System;

namespace SDG.Framework.Translations
{
	// Token: 0x020001FE RID: 510
	public class TranslatedTextArg0Container : ITranslatedTextArgContainer
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x00067D00 File Offset: 0x00066100
		public TranslatedTextArg0Container(object newArg0)
		{
			this.arg0 = newArg0;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00067D0F File Offset: 0x0006610F
		public string format(string text)
		{
			return string.Format(text, this.arg0);
		}

		// Token: 0x0400097E RID: 2430
		public object arg0;
	}
}
