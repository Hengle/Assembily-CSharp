using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006D3 RID: 1747
	public class SleekColorPicker : Sleek
	{
		// Token: 0x06003256 RID: 12886 RVA: 0x00147694 File Offset: 0x00145A94
		public SleekColorPicker()
		{
			base.init();
			this.color = Color.black;
			base.sizeOffset_X = 240;
			base.sizeOffset_Y = 120;
			this.colorImage = new SleekImageTexture();
			this.colorImage.sizeOffset_X = 30;
			this.colorImage.sizeOffset_Y = 30;
			this.colorImage.texture = (Texture2D)Resources.Load("Materials/Pixel");
			base.add(this.colorImage);
			this.rField = new SleekByteField();
			this.rField.positionOffset_X = 40;
			this.rField.sizeOffset_X = 60;
			this.rField.sizeOffset_Y = 30;
			this.rField.foregroundColor = Palette.COLOR_R;
			this.rField.foregroundTint = ESleekTint.NONE;
			this.rField.onTypedByte = new TypedByte(this.onTypedRField);
			base.add(this.rField);
			this.gField = new SleekByteField();
			this.gField.positionOffset_X = 110;
			this.gField.sizeOffset_X = 60;
			this.gField.sizeOffset_Y = 30;
			this.gField.foregroundColor = Palette.COLOR_G;
			this.gField.foregroundTint = ESleekTint.NONE;
			this.gField.onTypedByte = new TypedByte(this.onTypedGField);
			base.add(this.gField);
			this.bField = new SleekByteField();
			this.bField.positionOffset_X = 180;
			this.bField.sizeOffset_X = 60;
			this.bField.sizeOffset_Y = 30;
			this.bField.foregroundColor = Palette.COLOR_B;
			this.bField.foregroundTint = ESleekTint.NONE;
			this.bField.onTypedByte = new TypedByte(this.onTypedBField);
			base.add(this.bField);
			this.rSlider = new SleekSlider();
			this.rSlider.positionOffset_X = 40;
			this.rSlider.positionOffset_Y = 40;
			this.rSlider.sizeOffset_X = 200;
			this.rSlider.sizeOffset_Y = 20;
			this.rSlider.orientation = ESleekOrientation.HORIZONTAL;
			this.rSlider.addLabel("R", Palette.COLOR_R, ESleekSide.LEFT);
			this.rSlider.sideLabel.foregroundTint = ESleekTint.NONE;
			this.rSlider.onDragged = new Dragged(this.onDraggedRSlider);
			base.add(this.rSlider);
			this.gSlider = new SleekSlider();
			this.gSlider.positionOffset_X = 40;
			this.gSlider.positionOffset_Y = 70;
			this.gSlider.sizeOffset_X = 200;
			this.gSlider.sizeOffset_Y = 20;
			this.gSlider.orientation = ESleekOrientation.HORIZONTAL;
			this.gSlider.addLabel("G", Palette.COLOR_G, ESleekSide.LEFT);
			this.gSlider.sideLabel.foregroundTint = ESleekTint.NONE;
			this.gSlider.onDragged = new Dragged(this.onDraggedGSlider);
			base.add(this.gSlider);
			this.bSlider = new SleekSlider();
			this.bSlider.positionOffset_X = 40;
			this.bSlider.positionOffset_Y = 100;
			this.bSlider.sizeOffset_X = 200;
			this.bSlider.sizeOffset_Y = 20;
			this.bSlider.orientation = ESleekOrientation.HORIZONTAL;
			this.bSlider.addLabel("B", Palette.COLOR_B, ESleekSide.LEFT);
			this.bSlider.sideLabel.foregroundTint = ESleekTint.NONE;
			this.bSlider.onDragged = new Dragged(this.onDraggedBSlider);
			base.add(this.bSlider);
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x00147A34 File Offset: 0x00145E34
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x00147A3C File Offset: 0x00145E3C
		public Color state
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
				this.updateColor();
				this.updateColorText();
				this.updateColorSlider();
			}
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00147A57 File Offset: 0x00145E57
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x00147A60 File Offset: 0x00145E60
		private void updateColor()
		{
			this.colorImage.backgroundColor = this.color;
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x00147A74 File Offset: 0x00145E74
		private void updateColorText()
		{
			this.rField.state = (byte)(this.color.r * 255f);
			this.gField.state = (byte)(this.color.g * 255f);
			this.bField.state = (byte)(this.color.b * 255f);
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x00147AD8 File Offset: 0x00145ED8
		private void updateColorSlider()
		{
			this.rSlider.state = this.color.r;
			this.gSlider.state = this.color.g;
			this.bSlider.state = this.color.b;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x00147B27 File Offset: 0x00145F27
		private void onTypedRField(SleekByteField field, byte value)
		{
			this.color.r = (float)value / 255f;
			this.updateColor();
			this.updateColorSlider();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x00147B66 File Offset: 0x00145F66
		private void onTypedGField(SleekByteField field, byte value)
		{
			this.color.g = (float)value / 255f;
			this.updateColor();
			this.updateColorSlider();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00147BA5 File Offset: 0x00145FA5
		private void onTypedBField(SleekByteField field, byte value)
		{
			this.color.b = (float)value / 255f;
			this.updateColor();
			this.updateColorSlider();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x00147BE4 File Offset: 0x00145FE4
		private void onDraggedRSlider(SleekSlider slider, float state)
		{
			this.color.r = state;
			this.updateColor();
			this.updateColorText();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x00147C1B File Offset: 0x0014601B
		private void onDraggedGSlider(SleekSlider slider, float state)
		{
			this.color.g = state;
			this.updateColor();
			this.updateColorText();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00147C52 File Offset: 0x00146052
		private void onDraggedBSlider(SleekSlider slider, float state)
		{
			this.color.b = state;
			this.updateColor();
			this.updateColorText();
			if (this.onColorPicked != null)
			{
				this.onColorPicked(this, this.color);
			}
		}

		// Token: 0x0400224F RID: 8783
		public ColorPicked onColorPicked;

		// Token: 0x04002250 RID: 8784
		private SleekImageTexture colorImage;

		// Token: 0x04002251 RID: 8785
		private SleekByteField rField;

		// Token: 0x04002252 RID: 8786
		private SleekByteField gField;

		// Token: 0x04002253 RID: 8787
		private SleekByteField bField;

		// Token: 0x04002254 RID: 8788
		private SleekSlider rSlider;

		// Token: 0x04002255 RID: 8789
		private SleekSlider gSlider;

		// Token: 0x04002256 RID: 8790
		private SleekSlider bSlider;

		// Token: 0x04002257 RID: 8791
		private Color color;
	}
}
