using System;

namespace SDG.Framework.Translations
{
	// Token: 0x020001FF RID: 511
	public class TranslatedTextArgParamsContainer : ITranslatedTextArgContainer
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x00067D1D File Offset: 0x0006611D
		public TranslatedTextArgParamsContainer(object[] newArgs)
		{
			this.args = newArgs;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00067D2C File Offset: 0x0006612C
		public string format(string text)
		{
			return string.Format(text, this.args);
		}

		// Token: 0x0400097F RID: 2431
		public object[] args;
	}
}
