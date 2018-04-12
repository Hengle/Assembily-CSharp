using System;

namespace SDG.Unturned
{
	// Token: 0x0200070F RID: 1807
	public class SleekValue : Sleek
	{
		// Token: 0x0600336E RID: 13166 RVA: 0x0014D7BC File Offset: 0x0014BBBC
		public SleekValue()
		{
			base.init();
			this.field = new SleekSingleField();
			this.field.sizeOffset_X = -5;
			this.field.sizeScale_X = 0.4f;
			this.field.sizeScale_Y = 1f;
			this.field.onTypedSingle = new TypedSingle(this.onTypedSingleField);
			base.add(this.field);
			this.slider = new SleekSlider();
			this.slider.positionOffset_X = 5;
			this.slider.positionOffset_Y = -10;
			this.slider.positionScale_X = 0.4f;
			this.slider.positionScale_Y = 0.5f;
			this.slider.sizeOffset_X = -5;
			this.slider.sizeOffset_Y = 20;
			this.slider.sizeScale_X = 0.6f;
			this.slider.orientation = ESleekOrientation.HORIZONTAL;
			this.slider.onDragged = new Dragged(this.onDraggedSlider);
			base.add(this.slider);
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x0014D8CD File Offset: 0x0014BCCD
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x0014D8D5 File Offset: 0x0014BCD5
		public float state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				this.field.state = this.state;
				this.slider.state = this.state;
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0014D900 File Offset: 0x0014BD00
		private void onTypedSingleField(SleekSingleField field, float state)
		{
			if (this.onValued != null)
			{
				this.onValued(this, state);
			}
			this._state = state;
			this.slider.state = state;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0014D92D File Offset: 0x0014BD2D
		private void onDraggedSlider(SleekSlider slider, float state)
		{
			if (this.onValued != null)
			{
				this.onValued(this, state);
			}
			this._state = state;
			this.field.state = state;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0014D95A File Offset: 0x0014BD5A
		public override void draw(bool ignoreCulling)
		{
			base.drawChildren(ignoreCulling);
		}

		// Token: 0x040022D2 RID: 8914
		public Valued onValued;

		// Token: 0x040022D3 RID: 8915
		private SleekSingleField field;

		// Token: 0x040022D4 RID: 8916
		private SleekSlider slider;

		// Token: 0x040022D5 RID: 8917
		private float _state;
	}
}
