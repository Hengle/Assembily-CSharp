﻿using System;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002FB RID: 763
	public class Sleek2WindowPartition : Sleek2Element, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x060015DC RID: 5596 RVA: 0x000833F0 File Offset: 0x000817F0
		public Sleek2WindowPartition() : this(null)
		{
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x000833FC File Offset: 0x000817FC
		public Sleek2WindowPartition(Sleek2WindowDock dock)
		{
			base.name = "Partition";
			base.transform.pivot = new Vector2(0f, 1f);
			this.imageComponent = base.gameObject.AddComponent<Image>();
			this.imageComponent.sprite = Resources.Load<Sprite>("Sprites/UI/Background");
			this.imageComponent.type = Image.Type.Sliced;
			if (dock == null)
			{
				dock = new Sleek2WindowDock(this);
			}
			else
			{
				dock.partition = this;
			}
			this.dock = dock;
			dock.transform.anchorMin = Vector2.zero;
			dock.transform.anchorMax = Vector2.one;
			dock.transform.offsetMin = Vector2.zero;
			dock.transform.offsetMax = Vector2.zero;
			dock.dockedWindowRemoved += this.handleDockedWindowRemoved;
			this.addElement(dock);
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060015DE RID: 5598 RVA: 0x000834E4 File Offset: 0x000818E4
		// (remove) Token: 0x060015DF RID: 5599 RVA: 0x0008351C File Offset: 0x0008191C
		public event Sleek2WindowPartitionEmptied emptied;

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00083552 File Offset: 0x00081952
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0008355A File Offset: 0x0008195A
		public Image imageComponent { get; protected set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00083563 File Offset: 0x00081963
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0008356B File Offset: 0x0008196B
		public Sleek2WindowDock dock { get; protected set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00083574 File Offset: 0x00081974
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0008357C File Offset: 0x0008197C
		public Sleek2WindowPartition a { get; protected set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00083585 File Offset: 0x00081985
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0008358D File Offset: 0x0008198D
		public Sleek2WindowPartition b { get; protected set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00083596 File Offset: 0x00081996
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0008359E File Offset: 0x0008199E
		public Sleek2Separator separator { get; protected set; }

		// Token: 0x060015EA RID: 5610 RVA: 0x000835A8 File Offset: 0x000819A8
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			if (reader.containsKey("Windows"))
			{
				reader.readKey("Windows");
				int num = reader.readArrayLength();
				for (int i = 0; i < num; i++)
				{
					reader.readArrayIndex(i);
					IFormattedFileReader formattedFileReader = reader.readObject();
					if (formattedFileReader != null)
					{
						Type type = formattedFileReader.readValue<Type>("Type");
						if (type != null)
						{
							Sleek2Window sleek2Window = Activator.CreateInstance(type) as Sleek2Window;
							if (sleek2Window != null)
							{
								this.addWindow(sleek2Window);
								formattedFileReader.readKey("Window");
								sleek2Window.read(formattedFileReader);
							}
						}
					}
				}
			}
			else
			{
				reader.readKey("Direction");
				Separator.EDirection edirection = reader.readValue<Separator.EDirection>();
				reader.readKey("Split");
				float value = reader.readValue<float>();
				Sleek2WindowPartition sleek2WindowPartition;
				Sleek2WindowPartition sleek2WindowPartition2;
				this.split((edirection != Separator.EDirection.HORIZONTAL) ? ESleek2PartitionDirection.UP : ESleek2PartitionDirection.RIGHT, out sleek2WindowPartition, out sleek2WindowPartition2);
				this.separator.handle.value = value;
				reader.readKey("First");
				sleek2WindowPartition.read(reader);
				reader.readKey("Second");
				sleek2WindowPartition2.read(reader);
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000836C8 File Offset: 0x00081AC8
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			if (this.dock == null)
			{
				writer.writeKey("Direction");
				writer.writeValue<Separator.EDirection>(this.separator.handle.direction);
				writer.writeKey("Split");
				writer.writeValue<float>(this.separator.handle.value);
				writer.writeKey("First");
				writer.writeValue<Sleek2WindowPartition>(this.a);
				writer.writeKey("Second");
				writer.writeValue<Sleek2WindowPartition>(this.b);
			}
			else
			{
				writer.writeKey("Windows");
				writer.beginArray();
				for (int i = 0; i < this.dock.windows.Count; i++)
				{
					Sleek2Window sleek2Window = this.dock.windows[i];
					writer.beginObject();
					writer.writeValue<Type>("Type", sleek2Window.GetType());
					writer.writeValue<Sleek2Window>("Window", sleek2Window);
					writer.endObject();
				}
				writer.endArray();
			}
			writer.endObject();
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000837D4 File Offset: 0x00081BD4
		public bool addWindow(Sleek2Window window)
		{
			if (this.dock != null)
			{
				this.dock.addWindow(window);
				return true;
			}
			return (this.a != null && this.a.addWindow(window)) || (this.b != null && this.b.addWindow(window));
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00083838 File Offset: 0x00081C38
		public void split(ESleek2PartitionDirection direction, out Sleek2WindowPartition originalPartition, out Sleek2WindowPartition newPartition)
		{
			originalPartition = null;
			newPartition = null;
			this.separator = new Sleek2Separator();
			this.addElement(this.separator);
			this.separator.handle.value = 0.5f;
			switch (direction)
			{
			case ESleek2PartitionDirection.UP:
				this.a = new Sleek2WindowPartition(this.dock);
				this.b = new Sleek2WindowPartition();
				originalPartition = this.a;
				newPartition = this.b;
				this.separator.direction = Separator.EDirection.VERTICAL;
				break;
			case ESleek2PartitionDirection.RIGHT:
				this.a = new Sleek2WindowPartition(this.dock);
				this.b = new Sleek2WindowPartition();
				originalPartition = this.a;
				newPartition = this.b;
				this.separator.direction = Separator.EDirection.HORIZONTAL;
				break;
			case ESleek2PartitionDirection.DOWN:
				this.a = new Sleek2WindowPartition();
				this.b = new Sleek2WindowPartition(this.dock);
				newPartition = this.a;
				originalPartition = this.b;
				this.separator.direction = Separator.EDirection.VERTICAL;
				break;
			case ESleek2PartitionDirection.LEFT:
				this.a = new Sleek2WindowPartition();
				this.b = new Sleek2WindowPartition(this.dock);
				newPartition = this.a;
				originalPartition = this.b;
				this.separator.direction = Separator.EDirection.HORIZONTAL;
				break;
			}
			this.addElement(this.a);
			this.addElement(this.b);
			this.a.emptied += this.handlePartitionEmptied;
			this.b.emptied += this.handlePartitionEmptied;
			this.separator.handle.a = this.a.transform;
			this.separator.handle.b = this.b.transform;
			this.separator.handle.aActive = true;
			this.separator.handle.bActive = true;
			this.dock.dockedWindowRemoved -= this.handleDockedWindowRemoved;
			this.dock = null;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00083A48 File Offset: 0x00081E48
		protected virtual void handlePartitionEmptied(Sleek2WindowPartition partition)
		{
			Sleek2WindowPartition sleek2WindowPartition;
			if (partition == this.a)
			{
				sleek2WindowPartition = this.b;
			}
			else
			{
				sleek2WindowPartition = this.a;
			}
			this.dock = sleek2WindowPartition.dock;
			if (this.dock != null)
			{
				this.dock.partition = this;
				this.dock.transform.anchorMin = Vector2.zero;
				this.dock.transform.anchorMax = Vector2.one;
				this.dock.transform.offsetMin = Vector2.zero;
				this.dock.transform.offsetMax = Vector2.zero;
				this.dock.dockedWindowRemoved += this.handleDockedWindowRemoved;
				this.addElement(this.dock);
				this.a.emptied -= this.handlePartitionEmptied;
				this.b.emptied -= this.handlePartitionEmptied;
				this.a.destroy();
				this.b.destroy();
				this.separator.destroy();
			}
			else
			{
				this.a.emptied -= this.handlePartitionEmptied;
				this.b.emptied -= this.handlePartitionEmptied;
				partition.destroy();
				this.separator.destroy();
				this.a = sleek2WindowPartition.a;
				this.b = sleek2WindowPartition.b;
				this.addElement(this.a);
				this.addElement(this.b);
				this.a.emptied -= sleek2WindowPartition.handlePartitionEmptied;
				this.b.emptied -= sleek2WindowPartition.handlePartitionEmptied;
				this.a.emptied += this.handlePartitionEmptied;
				this.b.emptied += this.handlePartitionEmptied;
				this.separator = sleek2WindowPartition.separator;
				this.addElement(this.separator);
				sleek2WindowPartition.separator = null;
				sleek2WindowPartition.a = null;
				sleek2WindowPartition.b = null;
				sleek2WindowPartition.destroy();
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00083C69 File Offset: 0x00082069
		protected virtual void handleDockedWindowRemoved(Sleek2WindowDock dock, Sleek2Window window)
		{
			if (dock.windows.Count == 0)
			{
				this.triggerEmptied();
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00083C81 File Offset: 0x00082081
		protected virtual void triggerEmptied()
		{
			if (this.emptied != null)
			{
				this.emptied(this);
			}
		}
	}
}
