using System;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.UI.Devkit;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D9 RID: 729
	public class Sleek2PopoutWindowContainer : Sleek2PopoutContainer
	{
		// Token: 0x06001506 RID: 5382 RVA: 0x000817D8 File Offset: 0x0007FBD8
		public Sleek2PopoutWindowContainer()
		{
			this.partition = new Sleek2WindowPartition();
			this.partition.transform.anchorMin = Vector2.zero;
			this.partition.transform.anchorMax = Vector2.one;
			this.partition.transform.pivot = new Vector2(0f, 1f);
			this.partition.transform.sizeDelta = Vector2.zero;
			this.partition.emptied += this.handleEmptied;
			base.bodyPanel.addElement(this.partition);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0008187D File Offset: 0x0007FC7D
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00081885 File Offset: 0x0007FC85
		public Sleek2WindowPartition partition { get; protected set; }

		// Token: 0x06001509 RID: 5385 RVA: 0x0008188E File Offset: 0x0007FC8E
		protected override void readContainer(IFormattedFileReader reader)
		{
			base.readContainer(reader);
			reader.readKey("Contents");
			this.partition.read(reader);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x000818AE File Offset: 0x0007FCAE
		protected override void writeContainer(IFormattedFileWriter writer)
		{
			base.writeContainer(writer);
			writer.writeKey("Contents");
			writer.writeValue<Sleek2WindowPartition>(this.partition);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x000818CE File Offset: 0x0007FCCE
		protected virtual void handleEmptied(Sleek2WindowPartition partition)
		{
			DevkitWindowManager.removeContainer(this);
		}
	}
}
