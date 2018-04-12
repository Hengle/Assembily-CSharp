using System;

namespace SDG.Framework.Translations
{
	// Token: 0x020001FB RID: 507
	public class TranslatedText
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x00067911 File Offset: 0x00065D11
		public TranslatedText(TranslationReference newReference)
		{
			this.args = null;
			this.text = null;
			this._reference = newReference;
			this.beginListening();
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00067934 File Offset: 0x00065D34
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x0006793C File Offset: 0x00065D3C
		public TranslationReference reference
		{
			get
			{
				return this._reference;
			}
			set
			{
				if (this.reference == value)
				{
					return;
				}
				this.endListening();
				this._reference = value;
				this.beginListening();
				this.format();
				this.triggerTranslatedTextChanged();
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06000F2A RID: 3882 RVA: 0x00067970 File Offset: 0x00065D70
		// (remove) Token: 0x06000F2B RID: 3883 RVA: 0x000679A8 File Offset: 0x00065DA8
		public event TranslatedTextChangedHandler changed;

		// Token: 0x06000F2C RID: 3884 RVA: 0x000679E0 File Offset: 0x00065DE0
		public virtual string format()
		{
			if (this.args != null)
			{
				this.text = this.args.format(Translator.translate(this.reference));
			}
			else
			{
				this.text = Translator.translate(this.reference);
			}
			this.triggerTranslatedTextChanged();
			return this.text;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00067A38 File Offset: 0x00065E38
		public virtual string format(object arg0)
		{
			if (this.args == null)
			{
				this.args = new TranslatedTextArg0Container(arg0);
			}
			else
			{
				TranslatedTextArg0Container translatedTextArg0Container = this.args as TranslatedTextArg0Container;
				translatedTextArg0Container.arg0 = arg0;
			}
			return this.format();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00067A7C File Offset: 0x00065E7C
		public virtual string format(object arg0, object arg1)
		{
			if (this.args == null)
			{
				this.args = new TranslatedTextArg0Arg1Container(arg0, arg1);
			}
			else
			{
				TranslatedTextArg0Arg1Container translatedTextArg0Arg1Container = this.args as TranslatedTextArg0Arg1Container;
				translatedTextArg0Arg1Container.arg0 = arg0;
				translatedTextArg0Arg1Container.arg1 = arg1;
			}
			return this.format();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00067AC8 File Offset: 0x00065EC8
		public virtual string format(object arg0, object arg1, object arg2)
		{
			if (this.args == null)
			{
				this.args = new TranslatedTextArg0Arg1Arg2Container(arg0, arg1, arg2);
			}
			else
			{
				TranslatedTextArg0Arg1Arg2Container translatedTextArg0Arg1Arg2Container = this.args as TranslatedTextArg0Arg1Arg2Container;
				translatedTextArg0Arg1Arg2Container.arg0 = arg0;
				translatedTextArg0Arg1Arg2Container.arg1 = arg1;
				translatedTextArg0Arg1Arg2Container.arg2 = arg2;
			}
			return this.format();
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00067B1C File Offset: 0x00065F1C
		public virtual string format(params object[] args)
		{
			if (this.args == null)
			{
				this.args = new TranslatedTextArgParamsContainer(args);
			}
			else
			{
				TranslatedTextArgParamsContainer translatedTextArgParamsContainer = this.args as TranslatedTextArgParamsContainer;
				translatedTextArgParamsContainer.args = args;
			}
			return this.format();
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00067B5E File Offset: 0x00065F5E
		protected virtual void handleLeafTextChanged(TranslationLeaf leaf, string oldText, string newText)
		{
			this.format();
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00067B68 File Offset: 0x00065F68
		protected virtual void handleLanguageChanged(string oldLanguage, string newLanguage)
		{
			TranslationLeaf leaf = Translator.getLeaf(oldLanguage, this.reference);
			if (leaf != null)
			{
				leaf.textChanged -= this.handleLeafTextChanged;
			}
			TranslationLeaf leaf2 = Translator.getLeaf(newLanguage, this.reference);
			if (leaf2 != null)
			{
				leaf2.textChanged += this.handleLeafTextChanged;
			}
			this.format();
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00067BC8 File Offset: 0x00065FC8
		protected virtual void triggerTranslatedTextChanged()
		{
			if (this.changed != null)
			{
				this.changed(this, this.text);
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00067BE7 File Offset: 0x00065FE7
		public override string ToString()
		{
			return this.text;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x00067BF0 File Offset: 0x00065FF0
		public virtual void beginListening()
		{
			if (this.isListening)
			{
				return;
			}
			this.isListening = true;
			Translator.languageChanged += this.handleLanguageChanged;
			TranslationLeaf leaf = Translator.getLeaf(this.reference);
			if (leaf != null)
			{
				leaf.textChanged += this.handleLeafTextChanged;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x00067C48 File Offset: 0x00066048
		public virtual void endListening()
		{
			if (!this.isListening)
			{
				return;
			}
			this.isListening = false;
			Translator.languageChanged -= this.handleLanguageChanged;
			TranslationLeaf leaf = Translator.getLeaf(this.reference);
			if (leaf != null)
			{
				leaf.textChanged -= this.handleLeafTextChanged;
			}
		}

		// Token: 0x04000974 RID: 2420
		protected TranslationReference _reference;

		// Token: 0x04000975 RID: 2421
		protected bool isListening;

		// Token: 0x04000976 RID: 2422
		protected string text;

		// Token: 0x04000977 RID: 2423
		protected ITranslatedTextArgContainer args;
	}
}
