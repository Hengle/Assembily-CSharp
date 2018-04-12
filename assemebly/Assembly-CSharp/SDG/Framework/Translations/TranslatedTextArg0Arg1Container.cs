using System;

namespace SDG.Framework.Translations
{
	// Token: 0x020001FD RID: 509
	public class TranslatedTextArg0Arg1Container : ITranslatedTextArgContainer
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x00067CD6 File Offset: 0x000660D6
		public TranslatedTextArg0Arg1Container(object newArg0, object newArg1)
		{
			this.arg0 = newArg0;
			this.arg1 = newArg1;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00067CEC File Offset: 0x000660EC
		public string format(string text)
		{
			return string.Format(text, this.arg0, this.arg1);
		}

		// Token: 0x0400097C RID: 2428
		public object arg0;

		// Token: 0x0400097D RID: 2429
		public object arg1;
	}
}
