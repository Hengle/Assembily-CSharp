using System;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.UI.Devkit;
using UnityEngine;
using UnityEngine.UI;

namespace SDG.Framework.UI.Sleek2
{
	// Token: 0x020002D8 RID: 728
	public class Sleek2PopoutContainer : Sleek2Container, IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x0006EB3C File Offset: 0x0006CF3C
		public Sleek2PopoutContainer()
		{
			base.name = "Popout";
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Vertical"),
					type = Image.Type.Sliced
				},
				handle = 
				{
					targetTransform = base.transform,
					verticalSize = true
				},
				transform = 
				{
					anchorMin = new Vector2(0f, 1f),
					anchorMax = Vector2.one,
					pivot = new Vector2(0.5f, 1f),
					sizeDelta = new Vector2(-16f, 8f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Diagonal_45")
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalSize = true,
					verticalSize = true
				},
				transform = 
				{
					anchorMin = Vector2.one,
					anchorMax = Vector2.one,
					pivot = Vector2.one,
					sizeDelta = new Vector2(8f, 8f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Horizontal"),
					type = Image.Type.Sliced
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalSize = true
				},
				transform = 
				{
					anchorMin = new Vector2(1f, 0f),
					anchorMax = Vector2.one,
					pivot = new Vector2(1f, 0.5f),
					sizeDelta = new Vector2(8f, -16f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Diagonal_135")
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalSize = true,
					verticalPosition = true
				},
				transform = 
				{
					anchorMin = new Vector2(1f, 0f),
					anchorMax = new Vector2(1f, 0f),
					pivot = new Vector2(1f, 0f),
					sizeDelta = new Vector2(8f, 8f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Vertical"),
					type = Image.Type.Sliced
				},
				handle = 
				{
					targetTransform = base.transform,
					verticalPosition = true
				},
				transform = 
				{
					anchorMin = Vector2.zero,
					anchorMax = new Vector2(1f, 0f),
					pivot = new Vector2(0.5f, 0f),
					sizeDelta = new Vector2(-16f, 8f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Diagonal_45")
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalPosition = true,
					verticalPosition = true
				},
				transform = 
				{
					anchorMin = Vector2.zero,
					anchorMax = Vector2.zero,
					pivot = Vector2.zero,
					sizeDelta = new Vector2(8f, 8f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Horizontal"),
					type = Image.Type.Sliced
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalPosition = true
				},
				transform = 
				{
					anchorMin = Vector2.zero,
					anchorMax = new Vector2(0f, 1f),
					pivot = new Vector2(0f, 0.5f),
					sizeDelta = new Vector2(8f, -16f)
				}
			});
			this.addElement(new Sleek2Resize
			{
				image = 
				{
					sprite = Resources.Load<Sprite>("Sprites/UI/Separator_Diagonal_135")
				},
				handle = 
				{
					targetTransform = base.transform,
					horizontalPosition = true,
					verticalSize = true
				},
				transform = 
				{
					anchorMin = new Vector2(0f, 1f),
					anchorMax = new Vector2(0f, 1f),
					pivot = new Vector2(0f, 1f),
					sizeDelta = new Vector2(8f, 8f)
				}
			});
			this.centerPanel = new Sleek2Element();
			this.centerPanel.name = "Center";
			this.centerPanel.transform.anchorMin = Vector2.zero;
			this.centerPanel.transform.anchorMax = Vector2.one;
			this.centerPanel.transform.sizeDelta = new Vector2(-16f, -16f);
			this.addElement(this.centerPanel);
			this.centerPanel.addElement(base.headerPanel);
			this.centerPanel.addElement(base.bodyPanel);
			this.titlebar = new Sleek2Titlebar();
			this.titlebar.exitButton.clicked += this.handleExitClicked;
			this.titlebar.dragableComponent.target = base.transform;
			base.headerPanel.addElement(this.titlebar);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0006F197 File Offset: 0x0006D597
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0006F19F File Offset: 0x0006D59F
		public Sleek2Element centerPanel { get; protected set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006F1A8 File Offset: 0x0006D5A8
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x0006F1B0 File Offset: 0x0006D5B0
		public Sleek2Titlebar titlebar { get; protected set; }

		// Token: 0x06001501 RID: 5377 RVA: 0x0006F1BC File Offset: 0x0006D5BC
		public virtual void read(IFormattedFileReader reader)
		{
			reader = reader.readObject();
			base.transform.anchorMin = new Vector2(reader.readValue<float>("Min_X"), reader.readValue<float>("Min_Y"));
			base.transform.anchorMax = new Vector2(reader.readValue<float>("Max_X"), reader.readValue<float>("Max_Y"));
			this.readContainer(reader);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006F224 File Offset: 0x0006D624
		protected virtual void readContainer(IFormattedFileReader reader)
		{
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0006F228 File Offset: 0x0006D628
		public virtual void write(IFormattedFileWriter writer)
		{
			writer.beginObject();
			writer.writeKey("Min_X");
			writer.writeValue<float>(base.transform.anchorMin.x);
			writer.writeKey("Max_X");
			writer.writeValue<float>(base.transform.anchorMax.x);
			writer.writeKey("Min_Y");
			writer.writeValue<float>(base.transform.anchorMin.y);
			writer.writeKey("Max_Y");
			writer.writeValue<float>(base.transform.anchorMax.y);
			this.writeContainer(writer);
			writer.endObject();
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0006F2D8 File Offset: 0x0006D6D8
		protected virtual void writeContainer(IFormattedFileWriter writer)
		{
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006F2DA File Offset: 0x0006D6DA
		protected virtual void handleExitClicked(Sleek2ImageButton button)
		{
			DevkitWindowManager.removeContainer(this);
		}
	}
}
