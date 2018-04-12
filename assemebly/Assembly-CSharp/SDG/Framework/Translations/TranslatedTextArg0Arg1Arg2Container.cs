using System;

namespace SDG.Framework.Translations
{
	// Token: 0x020001FC RID: 508
	public class TranslatedTextArg0Arg1Arg2Container : ITranslatedTextArgContainer
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x00067C9F File Offset: 0x0006609F
		public TranslatedTextArg0Arg1Arg2Container(object newArg0, object newArg1, object newArg2)
		{
			this.arg0 = newArg0;
			this.arg1 = newArg1;
			this.arg2 = newArg2;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00067CBC File Offset: 0x000660BC
		public string format(string text)
		{
			return string.Format(text, this.arg0, this.arg1, this.arg2);
		}

		// Token: 0x04000979 RID: 2425
		public object arg0;

		// Token: 0x0400097A RID: 2426
		public object arg1;

		// Token: 0x0400097B RID: 2427
		public object arg2;
	}
}
