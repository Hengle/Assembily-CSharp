using System;
using System.IO;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;

namespace SDG.Framework.Translations
{
	// Token: 0x02000201 RID: 513
	public class Translation : IDirtyable, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x00067DB0 File Offset: 0x000661B0
		public Translation(string newPath, string newLanguage, string newNamespace)
		{
			this.path = newPath;
			this.language = newLanguage;
			this.ns = newNamespace;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00067DCD File Offset: 0x000661CD
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00067DD5 File Offset: 0x000661D5
		public bool isDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				if (this.isDirty == value)
				{
					return;
				}
				this._isDirty = value;
				if (this.isDirty)
				{
					DirtyManager.markDirty(this);
				}
				else
				{
					DirtyManager.markClean(this);
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00067E07 File Offset: 0x00066207
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x00067E0F File Offset: 0x0006620F
		public string path { get; protected set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00067E18 File Offset: 0x00066218
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00067E20 File Offset: 0x00066220
		public string language { get; protected set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00067E29 File Offset: 0x00066229
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00067E31 File Offset: 0x00066231
		public string ns { get; protected set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00067E3A File Offset: 0x0006623A
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00067E42 File Offset: 0x00066242
		public TranslationBranch tree { get; protected set; }

		// Token: 0x06000F4E RID: 3918 RVA: 0x00067E4C File Offset: 0x0006624C
		public virtual TranslationLeaf addLeaf(string token)
		{
			TranslationBranch translationBranch = this.tree;
			string[] array = token.Split(Translation.DELIMITERS);
			for (int i = 0; i < array.Length; i++)
			{
				TranslationBranch translationBranch2;
				if (!translationBranch.branches.TryGetValue(array[i], out translationBranch2))
				{
					translationBranch2 = translationBranch.addBranch(array[i]);
				}
				translationBranch = translationBranch2;
				if (i == array.Length - 1)
				{
					if (translationBranch.leaf == null)
					{
						translationBranch.addLeaf();
					}
				}
				else
				{
					if (translationBranch.leaf != null)
					{
						return null;
					}
					if (translationBranch.branches == null)
					{
						translationBranch.addBranches();
					}
				}
			}
			if (translationBranch != null && translationBranch.leaf != null)
			{
				return translationBranch.leaf;
			}
			return null;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00067EFC File Offset: 0x000662FC
		public virtual TranslationLeaf getLeaf(string token)
		{
			TranslationBranch tree = this.tree;
			string[] array = token.Split(Translation.DELIMITERS);
			for (int i = 0; i < array.Length; i++)
			{
				if (tree == null || tree.branches == null)
				{
					break;
				}
				tree.branches.TryGetValue(array[i], out tree);
			}
			if (tree != null && tree.leaf != null)
			{
				return tree.leaf;
			}
			return null;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00067F70 File Offset: 0x00066370
		public virtual string translate(string token)
		{
			TranslationLeaf leaf = this.getLeaf(token);
			if (leaf != null)
			{
				return leaf.text;
			}
			return null;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00067F94 File Offset: 0x00066394
		public virtual void load()
		{
			this.tree = new TranslationBranch(this, null, "Translation");
			using (StreamReader streamReader = new StreamReader(this.path))
			{
				IFormattedFileReader reader = new KeyValueTableReader(streamReader);
				this.read(reader);
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00067FF0 File Offset: 0x000663F0
		public virtual void unload()
		{
			this.tree = null;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00067FFC File Offset: 0x000663FC
		public virtual void save()
		{
			string path = this.path;
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter writer = new KeyValueTableWriter(streamWriter);
				this.write(writer);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00068060 File Offset: 0x00066460
		public virtual void read(IFormattedFileReader reader)
		{
			this.tree.read(reader);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00068070 File Offset: 0x00066470
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject("Metadata");
			writer.writeValue("Language", this.language);
			writer.writeValue("Namespace", this.ns);
			writer.endObject();
			this.tree.write(writer);
		}

		// Token: 0x04000981 RID: 2433
		protected static readonly char[] DELIMITERS = new char[]
		{
			'/',
			'.'
		};

		// Token: 0x04000982 RID: 2434
		protected bool _isDirty;
	}
}
