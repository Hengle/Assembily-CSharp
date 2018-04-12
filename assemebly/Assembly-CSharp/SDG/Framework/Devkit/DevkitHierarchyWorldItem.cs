using System;
using SDG.Framework.Debug;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x0200012C RID: 300
	public class DevkitHierarchyWorldItem : DevkitHierarchyItemBase
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0004DE11 File Offset: 0x0004C211
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x0004DE1E File Offset: 0x0004C21E
		[Inspectable("#SDG::Position", null)]
		public Vector3 inspectablePosition
		{
			get
			{
				return base.transform.localPosition;
			}
			set
			{
				base.transform.position = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0004DE2C File Offset: 0x0004C22C
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0004DE39 File Offset: 0x0004C239
		[Inspectable("#SDG::Rotation", null)]
		public Quaternion inspectableRotation
		{
			get
			{
				return base.transform.localRotation;
			}
			set
			{
				base.transform.rotation = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0004DE47 File Offset: 0x0004C247
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0004DE54 File Offset: 0x0004C254
		[Inspectable("#SDG::Scale", null)]
		public Vector3 inspectableScale
		{
			get
			{
				return base.transform.localScale;
			}
			set
			{
				base.transform.localScale = value;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0004DE62 File Offset: 0x0004C262
		public override void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader == null)
			{
				return;
			}
			this.readHierarchyItem(reader);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0004DE7C File Offset: 0x0004C27C
		protected virtual void readHierarchyItem(IFormattedFileReader reader)
		{
			base.transform.position = reader.readValue<Vector3>("Position");
			base.transform.rotation = reader.readValue<Quaternion>("Rotation");
			base.transform.localScale = reader.readValue<Vector3>("Scale");
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0004DECB File Offset: 0x0004C2CB
		public override void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			this.writeHierarchyItem(writer);
			writer.endObject();
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0004DEE0 File Offset: 0x0004C2E0
		protected virtual void writeHierarchyItem(IFormattedFileWriter writer)
		{
			writer.writeValue<Vector3>("Position", base.transform.position);
			writer.writeValue<Quaternion>("Rotation", base.transform.rotation);
			writer.writeValue<Vector3>("Scale", base.transform.localScale);
		}
	}
}
