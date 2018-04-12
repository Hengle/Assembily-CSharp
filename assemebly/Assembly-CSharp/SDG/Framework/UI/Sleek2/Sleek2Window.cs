using System;
using SDG.Framework.IO.FormattedFiles;
using UnityEngine;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002F6 RID: 758
	public class Sleek2Window : Sleek2Element, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x060015A5 RID: 5541 RVA: 0x0006C218 File Offset: 0x0006A618
		public Sleek2Window()
		{
			this.tab = new Sleek2WindowTab(this);
			base.transform.anchorMin = Vector2.zero;
			base.transform.anchorMax = Vector2.one;
			base.transform.offsetMin = Vector2.zero;
			base.transform.offsetMax = Vector2.zero;
			this.safePanel = new Sleek2Element();
			this.safePanel.transform.anchorMin = new Vector2(0f, 0f);
			this.safePanel.transform.anchorMax = new Vector2(1f, 1f);
			this.safePanel.transform.pivot = new Vector2(0f, 1f);
			this.safePanel.transform.offsetMin = new Vector2(5f, 5f);
			this.safePanel.transform.offsetMax = new Vector2(-5f, -5f);
			this.addElement(this.safePanel);
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x0006C329 File Offset: 0x0006A729
		// (set) Token: 0x060015A7 RID: 5543 RVA: 0x0006C331 File Offset: 0x0006A731
		public Sleek2WindowTab tab { get; protected set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x0006C33A File Offset: 0x0006A73A
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x0006C342 File Offset: 0x0006A742
		public Sleek2Element safePanel { get; protected set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0006C34B File Offset: 0x0006A74B
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x0006C354 File Offset: 0x0006A754
		public virtual bool isActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this.isActive != value)
				{
					this._isActive = value;
					base.transform.gameObject.SetActive(this.isActive);
					this.tab.imageComponent.color = ((!this.isActive) ? new Color(0.75f, 0.75f, 0.75f) : new Color(1f, 1f, 1f));
					this.triggerActivityChanged();
				}
				if (this.isActive)
				{
					this.focus();
				}
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0006C3E9 File Offset: 0x0006A7E9
		protected virtual void focus()
		{
			this.triggerFocused();
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060015AD RID: 5549 RVA: 0x0006C3F4 File Offset: 0x0006A7F4
		// (remove) Token: 0x060015AE RID: 5550 RVA: 0x0006C42C File Offset: 0x0006A82C
		public event Sleek2WindowActivityChangedHandler activityChanged;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060015AF RID: 5551 RVA: 0x0006C464 File Offset: 0x0006A864
		// (remove) Token: 0x060015B0 RID: 5552 RVA: 0x0006C49C File Offset: 0x0006A89C
		public event Sleek2WindowFocusedHandler focused;

		// Token: 0x060015B1 RID: 5553 RVA: 0x0006C4D2 File Offset: 0x0006A8D2
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			this.readWindow(reader);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0006C4E3 File Offset: 0x0006A8E3
		protected virtual void readWindow(IFormattedFileReader reader)
		{
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0006C4E5 File Offset: 0x0006A8E5
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			this.writeWindow(writer);
			writer.endObject();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0006C4FA File Offset: 0x0006A8FA
		protected virtual void writeWindow(IFormattedFileWriter writer)
		{
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0006C4FC File Offset: 0x0006A8FC
		protected virtual void triggerActivityChanged()
		{
			if (this.activityChanged != null)
			{
				this.activityChanged(this);
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0006C515 File Offset: 0x0006A915
		protected virtual void triggerFocused()
		{
			if (this.focused != null)
			{
				this.focused(this);
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0006C52E File Offset: 0x0006A92E
		public override void destroy()
		{
			if (this.tab.transform.parent != base.transform)
			{
				this.tab.destroy();
			}
			base.destroy();
		}

		// Token: 0x04000C09 RID: 3081
		public Sleek2WindowDock dock;

		// Token: 0x04000C0A RID: 3082
		protected bool _isActive;
	}
}
