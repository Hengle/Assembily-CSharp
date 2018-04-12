using System;

namespace SDG.Framework.Translations
{
	// Token: 0x02000200 RID: 512
	public class TranslatedTextFallback : TranslatedText
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x00067D3C File Offset: 0x0006613C
		public TranslatedTextFallback(string newFallback) : base(default(TranslationReference))
		{
			this.fallback = newFallback;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00067D60 File Offset: 0x00066160
		public override string format()
		{
			if (this.args != null)
			{
				this.text = this.args.format(this.fallback);
			}
			else
			{
				this.text = this.fallback;
			}
			this.triggerTranslatedTextChanged();
			return this.text;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00067DAC File Offset: 0x000661AC
		public override void beginListening()
		{
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00067DAE File Offset: 0x000661AE
		public override void endListening()
		{
		}

		// Token: 0x04000980 RID: 2432
		protected string fallback;
	}
}
