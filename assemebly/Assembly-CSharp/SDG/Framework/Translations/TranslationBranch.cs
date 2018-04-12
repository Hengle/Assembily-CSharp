using System;
using System.Collections.Generic;
using SDG.Framework.IO.FormattedFiles;

namespace SDG.Framework.Translations
{
	// Token: 0x02000203 RID: 515
	public class TranslationBranch : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x000680D3 File Offset: 0x000664D3
		public TranslationBranch(Translation newTranslation, TranslationBranch newParentBranch, string newKey)
		{
			this.translation = newTranslation;
			this.parentBranch = newParentBranch;
			this._key = newKey;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000680F0 File Offset: 0x000664F0
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x000680F8 File Offset: 0x000664F8
		public Translation translation { get; protected set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00068101 File Offset: 0x00066501
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00068109 File Offset: 0x00066509
		public TranslationBranch parentBranch { get; protected set; }

		// Token: 0x06000F60 RID: 3936 RVA: 0x00068114 File Offset: 0x00066514
		public TranslationReference getReferenceTo()
		{
			string referenceToPath = this.getReferenceToPath(string.Empty);
			return new TranslationReference(referenceToPath);
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06000F61 RID: 3937 RVA: 0x00068134 File Offset: 0x00066534
		// (remove) Token: 0x06000F62 RID: 3938 RVA: 0x0006816C File Offset: 0x0006656C
		public event TranslationBranchKeyChangedHandler keyChanged;

		// Token: 0x06000F63 RID: 3939 RVA: 0x000681A4 File Offset: 0x000665A4
		protected string getReferenceToPath(string path)
		{
			path = this.key + path;
			if (this.parentBranch != null && this.parentBranch.parentBranch != null)
			{
				path = '.' + path;
				path = this.parentBranch.getReferenceToPath(path);
			}
			else
			{
				path = string.Concat(new object[]
				{
					'#',
					this.translation.ns,
					"::",
					path
				});
			}
			return path;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x0006822E File Offset: 0x0006662E
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x00068238 File Offset: 0x00066638
		public string key
		{
			get
			{
				return this._key;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				if (this.key == value)
				{
					return;
				}
				if (this.parentBranch.branches.ContainsKey(value))
				{
					return;
				}
				if (this.parentBranch.branches.Remove(this.key))
				{
					string key = this.key;
					this._key = value;
					this.parentBranch.branches.Add(this.key, this);
					this.recursiveTriggerKeyChanged(key, this.key);
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x000682C7 File Offset: 0x000666C7
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x000682CF File Offset: 0x000666CF
		public Dictionary<string, TranslationBranch> branches { get; protected set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x000682D8 File Offset: 0x000666D8
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x000682E0 File Offset: 0x000666E0
		public TranslationLeaf leaf { get; protected set; }

		// Token: 0x06000F6A RID: 3946 RVA: 0x000682EC File Offset: 0x000666EC
		public virtual TranslationBranch addBranch(string key)
		{
			TranslationBranch translationBranch = new TranslationBranch(this.translation, this, key);
			this.branches.Add(key, translationBranch);
			return translationBranch;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00068315 File Offset: 0x00066715
		public virtual void addLeaf()
		{
			this.leaf = new TranslationLeaf(this.translation, this);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00068329 File Offset: 0x00066729
		public virtual void addBranches()
		{
			this.branches = new Dictionary<string, TranslationBranch>();
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00068338 File Offset: 0x00066738
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject(this.key);
			if (reader == null)
			{
				return;
			}
			if (reader.containsKey("Text") && reader.containsKey("Version"))
			{
				this.addLeaf();
				reader.readKey("Text");
				this.leaf.text = reader.readValue<string>();
				reader.readKey("Version");
				this.leaf.version = reader.readValue<int>();
			}
			else
			{
				this.addBranches();
				foreach (string key in reader.getKeys())
				{
					TranslationBranch translationBranch = this.addBranch(key);
					reader.readKey(key);
					translationBranch.read(reader);
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00068420 File Offset: 0x00066820
		public void write(IFormattedFileWriter writer)
		{
			if (string.IsNullOrEmpty(this.key))
			{
				return;
			}
			if (this.leaf != null)
			{
				string text = this.leaf.text;
				if (text == null)
				{
					text = string.Empty;
				}
				writer.beginObject(this.key);
				text = text.Replace("\"", "\\\"");
				text = text.Replace("'", "\\'");
				writer.writeValue("Text", text);
				writer.writeValue<int>("Version", this.leaf.version);
				writer.endObject();
			}
			else if (this.branches != null)
			{
				writer.beginObject(this.key);
				foreach (KeyValuePair<string, TranslationBranch> keyValuePair in this.branches)
				{
					TranslationBranch value = keyValuePair.Value;
					value.write(writer);
				}
				writer.endObject();
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00068530 File Offset: 0x00066930
		protected virtual void recursiveTriggerKeyChanged(string oldKey, string newKey)
		{
			this.triggerKeyChanged(oldKey, newKey);
			if (this.branches != null)
			{
				foreach (KeyValuePair<string, TranslationBranch> keyValuePair in this.branches)
				{
					TranslationBranch value = keyValuePair.Value;
					value.triggerKeyChanged(oldKey, newKey);
				}
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x000685A8 File Offset: 0x000669A8
		protected virtual void triggerKeyChanged(string oldKey, string newKey)
		{
			TranslationBranchKeyChangedHandler translationBranchKeyChangedHandler = this.keyChanged;
			if (translationBranchKeyChangedHandler != null)
			{
				translationBranchKeyChangedHandler(this, oldKey, newKey);
			}
		}

		// Token: 0x0400098A RID: 2442
		protected string _key;
	}
}
