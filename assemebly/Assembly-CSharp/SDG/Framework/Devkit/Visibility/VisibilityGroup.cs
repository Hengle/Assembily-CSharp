using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.Translations;

namespace SDG.Framework.Devkit.Visibility
{
	// Token: 0x0200018B RID: 395
	public class VisibilityGroup : IVisibilityGroup, IInspectableListElement, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0005AD29 File Offset: 0x00059129
		public VisibilityGroup()
		{
			this.internalName = null;
			this.displayName = TranslationReference.invalid;
			this._isVisible = true;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0005AD4A File Offset: 0x0005914A
		public VisibilityGroup(string newInternalName, TranslationReference newDisplayName, bool newIsVisible)
		{
			this.internalName = newInternalName;
			this.displayName = newDisplayName;
			this._isVisible = newIsVisible;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0005AD67 File Offset: 0x00059167
		public string inspectableListIndexInternalName
		{
			get
			{
				return this.internalName;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0005AD6F File Offset: 0x0005916F
		public TranslationReference inspectableListIndexDisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0005AD77 File Offset: 0x00059177
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0005AD7F File Offset: 0x0005917F
		public string internalName { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0005AD88 File Offset: 0x00059188
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0005AD90 File Offset: 0x00059190
		public TranslationReference displayName { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0005AD99 File Offset: 0x00059199
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0005ADA1 File Offset: 0x000591A1
		[Inspectable("#SDG::Devkit.Visibility.Group.Base.Is_Visible", null)]
		public bool isVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (this.isVisible == value)
				{
					return;
				}
				this._isVisible = value;
				this.triggerIsVisibleChanged();
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000BD0 RID: 3024 RVA: 0x0005ADC0 File Offset: 0x000591C0
		// (remove) Token: 0x06000BD1 RID: 3025 RVA: 0x0005ADF8 File Offset: 0x000591F8
		public event VisibilityGroupIsVisibleChangedHandler isVisibleChanged;

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0005AE2E File Offset: 0x0005922E
		public void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.readVisibilityGroup(reader);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0005AE46 File Offset: 0x00059246
		protected virtual void readVisibilityGroup(IFormattedFileReader reader)
		{
			this._isVisible = reader.readValue<bool>("Is_Visible");
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0005AE59 File Offset: 0x00059259
		public void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			this.writeVisibilityGroup(writer);
			writer.endObject();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0005AE6E File Offset: 0x0005926E
		protected virtual void writeVisibilityGroup(IFormattedFileWriter writer)
		{
			writer.writeValue<bool>("Is_Visible", this.isVisible);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0005AE84 File Offset: 0x00059284
		protected virtual void triggerIsVisibleChanged()
		{
			VisibilityGroupIsVisibleChangedHandler visibilityGroupIsVisibleChangedHandler = this.isVisibleChanged;
			if (visibilityGroupIsVisibleChangedHandler != null)
			{
				visibilityGroupIsVisibleChangedHandler(this);
			}
		}

		// Token: 0x0400085E RID: 2142
		protected bool _isVisible;
	}
}
