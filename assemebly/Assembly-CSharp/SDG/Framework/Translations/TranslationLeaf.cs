using System;

namespace SDG.Framework.Translations
{
	// Token: 0x02000206 RID: 518
	public class TranslationLeaf
	{
		// Token: 0x06000F79 RID: 3961 RVA: 0x000685CB File Offset: 0x000669CB
		public TranslationLeaf(Translation newTranslation, TranslationBranch newBranch)
		{
			this.translation = newTranslation;
			this.branch = newBranch;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x000685E1 File Offset: 0x000669E1
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x000685E9 File Offset: 0x000669E9
		public Translation translation { get; protected set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x000685F2 File Offset: 0x000669F2
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x000685FA File Offset: 0x000669FA
		public TranslationBranch branch { get; protected set; }

		// Token: 0x06000F7E RID: 3966 RVA: 0x00068603 File Offset: 0x00066A03
		public TranslationReference getReferenceTo()
		{
			return this.branch.getReferenceTo();
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00068610 File Offset: 0x00066A10
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00068618 File Offset: 0x00066A18
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this.text == value)
				{
					return;
				}
				string text = this.text;
				this._text = value;
				this.triggerTextChanged(text, this.text);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00068652 File Offset: 0x00066A52
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x0006865C File Offset: 0x00066A5C
		public int version
		{
			get
			{
				return this._version;
			}
			set
			{
				if (this.version == value)
				{
					return;
				}
				int version = this.version;
				this._version = value;
				this.triggerVersionChanged(version, this.version);
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000F83 RID: 3971 RVA: 0x00068694 File Offset: 0x00066A94
		// (remove) Token: 0x06000F84 RID: 3972 RVA: 0x000686CC File Offset: 0x00066ACC
		public event TranslationLeafTextChangedHandler textChanged;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000F85 RID: 3973 RVA: 0x00068704 File Offset: 0x00066B04
		// (remove) Token: 0x06000F86 RID: 3974 RVA: 0x0006873C File Offset: 0x00066B3C
		public event TranslationLeafVersionChangedHandler versionChanged;

		// Token: 0x06000F87 RID: 3975 RVA: 0x00068772 File Offset: 0x00066B72
		protected void triggerTextChanged(string oldText, string newText)
		{
			if (this.textChanged != null)
			{
				this.textChanged(this, oldText, newText);
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0006878D File Offset: 0x00066B8D
		protected void triggerVersionChanged(int oldVersion, int newVersion)
		{
			if (this.versionChanged != null)
			{
				this.versionChanged(this, oldVersion, newVersion);
			}
		}

		// Token: 0x0400098F RID: 2447
		protected string _text;

		// Token: 0x04000990 RID: 2448
		protected int _version;
	}
}
