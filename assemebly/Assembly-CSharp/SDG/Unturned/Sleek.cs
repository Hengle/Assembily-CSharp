using System;
using System.Collections.Generic;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006C0 RID: 1728
	public class Sleek
	{
		// Token: 0x060031E3 RID: 12771 RVA: 0x00144343 File Offset: 0x00142743
		public Sleek()
		{
			this.init();
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x00144374 File Offset: 0x00142774
		// (set) Token: 0x060031E5 RID: 12773 RVA: 0x001443C1 File Offset: 0x001427C1
		public Color backgroundColor
		{
			get
			{
				switch (this.backgroundTint)
				{
				case ESleekTint.NONE:
					return this._backgroundColor;
				case ESleekTint.BACKGROUND:
					return OptionsSettings.backgroundColor;
				case ESleekTint.FOREGROUND:
					return OptionsSettings.foregroundColor;
				case ESleekTint.FONT:
					return OptionsSettings.fontColor;
				default:
					return Color.white;
				}
			}
			set
			{
				this._backgroundColor = value;
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060031E6 RID: 12774 RVA: 0x001443CC File Offset: 0x001427CC
		// (set) Token: 0x060031E7 RID: 12775 RVA: 0x0014442F File Offset: 0x0014282F
		public Color foregroundColor
		{
			get
			{
				switch (this.foregroundTint)
				{
				case ESleekTint.NONE:
					return (!this.isRich) ? this._foregroundColor : this.richOverrideColor;
				case ESleekTint.BACKGROUND:
					return OptionsSettings.backgroundColor;
				case ESleekTint.FOREGROUND:
					return OptionsSettings.foregroundColor;
				case ESleekTint.FONT:
					return OptionsSettings.fontColor;
				default:
					return Color.white;
				}
			}
			set
			{
				this._foregroundColor = value;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x00144438 File Offset: 0x00142838
		public Sleek parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x00144440 File Offset: 0x00142840
		public List<Sleek> children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x00144448 File Offset: 0x00142848
		public Rect frame
		{
			get
			{
				return this._frame;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x00144450 File Offset: 0x00142850
		// (set) Token: 0x060031EC RID: 12780 RVA: 0x00144458 File Offset: 0x00142858
		public ESleekConstraint constraint
		{
			get
			{
				return this._constraint;
			}
			set
			{
				this._constraint = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x00144468 File Offset: 0x00142868
		// (set) Token: 0x060031EE RID: 12782 RVA: 0x00144470 File Offset: 0x00142870
		public int constrain_X
		{
			get
			{
				return this._constrain_X;
			}
			set
			{
				this._constrain_X = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x00144480 File Offset: 0x00142880
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x00144488 File Offset: 0x00142888
		public int constrain_Y
		{
			get
			{
				return this._constrain_Y;
			}
			set
			{
				this._constrain_Y = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x00144498 File Offset: 0x00142898
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x001444A0 File Offset: 0x001428A0
		public int positionOffset_X
		{
			get
			{
				return this._positionOffset_X;
			}
			set
			{
				this._positionOffset_X = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x001444B0 File Offset: 0x001428B0
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x001444B8 File Offset: 0x001428B8
		public int positionOffset_Y
		{
			get
			{
				return this._positionOffset_Y;
			}
			set
			{
				this._positionOffset_Y = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x001444C8 File Offset: 0x001428C8
		// (set) Token: 0x060031F6 RID: 12790 RVA: 0x001444D0 File Offset: 0x001428D0
		public float positionScale_X
		{
			get
			{
				return this._positionScale_X;
			}
			set
			{
				this._positionScale_X = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x001444E0 File Offset: 0x001428E0
		// (set) Token: 0x060031F8 RID: 12792 RVA: 0x001444E8 File Offset: 0x001428E8
		public float positionScale_Y
		{
			get
			{
				return this._positionScale_Y;
			}
			set
			{
				this._positionScale_Y = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x001444F8 File Offset: 0x001428F8
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x00144500 File Offset: 0x00142900
		public int sizeOffset_X
		{
			get
			{
				return this._sizeOffset_X;
			}
			set
			{
				this._sizeOffset_X = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x00144510 File Offset: 0x00142910
		// (set) Token: 0x060031FC RID: 12796 RVA: 0x00144518 File Offset: 0x00142918
		public int sizeOffset_Y
		{
			get
			{
				return this._sizeOffset_Y;
			}
			set
			{
				this._sizeOffset_Y = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x00144528 File Offset: 0x00142928
		// (set) Token: 0x060031FE RID: 12798 RVA: 0x00144530 File Offset: 0x00142930
		public float sizeScale_X
		{
			get
			{
				return this._sizeScale_X;
			}
			set
			{
				this._sizeScale_X = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x00144540 File Offset: 0x00142940
		// (set) Token: 0x06003200 RID: 12800 RVA: 0x00144548 File Offset: 0x00142948
		public float sizeScale_Y
		{
			get
			{
				return this._sizeScale_Y;
			}
			set
			{
				this._sizeScale_Y = value;
				this.needsFrame = true;
			}
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00144558 File Offset: 0x00142958
		public void drawAnyway(bool ignoreCulling)
		{
			this.drawChildren(ignoreCulling);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00144561 File Offset: 0x00142961
		public virtual void draw(bool ignoreCulling)
		{
			this.drawChildren(ignoreCulling);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x0014456C File Offset: 0x0014296C
		protected void drawChildren(bool ignoreCulling)
		{
			if (this.hideTooltip)
			{
				GUI.tooltip = string.Empty;
			}
			if (!this.isInputable)
			{
				GUI.enabled = false;
			}
			if (this.local)
			{
				ignoreCulling = true;
				Sleek.cullingRect = this.getCullingRect();
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].update();
				if (this.children[i].isVisible)
				{
					if (this.children[i].isOnScreen(ignoreCulling, Sleek.cullingRect))
					{
						this.children[i].draw(ignoreCulling);
					}
					else if (ignoreCulling)
					{
						this.children[i].drawAnyway(ignoreCulling);
					}
				}
			}
			if (!this.isInputable)
			{
				GUI.enabled = true;
			}
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00144657 File Offset: 0x00142A57
		public virtual void destroy()
		{
			this.destroyChildren();
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00144660 File Offset: 0x00142A60
		protected void destroyChildren()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].destroy();
			}
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x0014469C File Offset: 0x00142A9C
		protected Rect calculate()
		{
			if (this._parent != null)
			{
				float uiLayoutScale = GraphicsSettings.uiLayoutScale;
				Rect result = this._parent.calculate();
				if (this._parent.local)
				{
					result.x = (float)this.positionOffset_X * uiLayoutScale;
					result.y = (float)this.positionOffset_Y * uiLayoutScale;
				}
				else
				{
					result.x += (float)this.positionOffset_X * uiLayoutScale + result.width * this.positionScale_X;
					result.y += (float)this.positionOffset_Y * uiLayoutScale + result.height * this.positionScale_Y;
				}
				result.width = (float)this.sizeOffset_X * uiLayoutScale + result.width * this.sizeScale_X;
				result.height = (float)this.sizeOffset_Y * uiLayoutScale + result.height * this.sizeScale_Y;
				if (this.constrain_X != 0 && result.width > (float)this.constrain_X)
				{
					result.x += (result.width - (float)this.constrain_X) / 2f;
					result.width = (float)this.constrain_X;
				}
				if (this.constrain_Y != 0 && result.height > (float)this.constrain_Y)
				{
					result.y += (result.height - (float)this.constrain_Y) / 2f;
					result.height = (float)this.constrain_Y;
				}
				if (this.constraint == ESleekConstraint.X)
				{
					result.x += (result.width - result.height) / 2f;
					result.width = result.height;
				}
				else if (this.constraint == ESleekConstraint.Y)
				{
					result.y += (result.height - result.width) / 2f;
					result.height = result.width;
				}
				else if (this.constraint == ESleekConstraint.XY)
				{
					if (result.width < result.height)
					{
						result.y += (result.height - result.width) / 2f;
						result.height = result.width;
					}
					else
					{
						result.x += (result.width - result.height) / 2f;
						result.width = result.height;
					}
				}
				return result;
			}
			if (Screen.width == 5760 && Screen.height == 1080)
			{
				return new Rect(1920f, 0f, 1920f, 1080f);
			}
			return new Rect((float)this.positionOffset_X, (float)this.positionOffset_Y, (float)Screen.width, (float)Screen.height);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00144988 File Offset: 0x00142D88
		public void lerpPositionOffset(int newPositionOffset_X, int newPositionOffset_Y, ESleekLerp lerp, float time)
		{
			this.isLerpingPositionOffset = true;
			this.positionOffsetLerp = lerp;
			this.positionOffsetLerpTime = time;
			this.positionOffsetLerpTicked = Time.realtimeSinceStartup;
			this.fromPositionOffset_X = this.positionOffset_X;
			this.fromPositionOffset_Y = this.positionOffset_Y;
			this.toPositionOffset_X = newPositionOffset_X;
			this.toPositionOffset_Y = newPositionOffset_Y;
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x001449DC File Offset: 0x00142DDC
		public void lerpPositionScale(float newPositionScale_X, float newPositionScale_Y, ESleekLerp lerp, float time)
		{
			this.isLerpingPositionScale = true;
			this.positionScaleLerp = lerp;
			this.positionScaleLerpTime = time;
			this.positionScaleLerpTicked = Time.realtimeSinceStartup;
			this.fromPositionScale_X = this.positionScale_X;
			this.fromPositionScale_Y = this.positionScale_Y;
			this.toPositionScale_X = newPositionScale_X;
			this.toPositionScale_Y = newPositionScale_Y;
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00144A30 File Offset: 0x00142E30
		public void lerpSizeOffset(int newSizeOffset_X, int newSizeOffset_Y, ESleekLerp lerp, float time)
		{
			this.isLerpingSizeOffset = true;
			this.sizeOffsetLerp = lerp;
			this.sizeOffsetLerpTime = time;
			this.sizeOffsetLerpTicked = Time.realtimeSinceStartup;
			this.fromSizeOffset_X = this.sizeOffset_X;
			this.fromSizeOffset_Y = this.sizeOffset_Y;
			this.toSizeOffset_X = newSizeOffset_X;
			this.toSizeOffset_Y = newSizeOffset_Y;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00144A84 File Offset: 0x00142E84
		public void lerpSizeScale(float newSizeScale_X, float newSizeScale_Y, ESleekLerp lerp, float time)
		{
			this.isLerpingSizeScale = true;
			this.sizeScaleLerp = lerp;
			this.sizeScaleLerpTime = time;
			this.sizeScaleLerpTicked = Time.realtimeSinceStartup;
			this.fromSizeScale_X = this.sizeScale_X;
			this.fromSizeScale_Y = this.sizeScale_Y;
			this.toSizeScale_X = newSizeScale_X;
			this.toSizeScale_Y = newSizeScale_Y;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00144AD8 File Offset: 0x00142ED8
		public virtual Rect getCullingRect()
		{
			return default(Rect);
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00144AF0 File Offset: 0x00142EF0
		public bool isOnScreen(bool ignoreCulling, Rect rect)
		{
			if (this.parent == null)
			{
				return true;
			}
			if (ignoreCulling)
			{
				if (this.frame.xMax < rect.xMin || this.frame.yMax < rect.yMin || this.frame.xMin > rect.xMax || this.frame.yMin > rect.yMax)
				{
					return false;
				}
			}
			else if (Screen.width == 5760 && Screen.height == 1080)
			{
				if (this.frame.xMax < 1920f || this.frame.yMax < 0f || this.frame.xMin > 3840f || this.frame.yMin > 1080f)
				{
					return false;
				}
			}
			else if (this.frame.xMax < 0f || this.frame.yMax < 0f || this.frame.xMin > (float)Screen.width || this.frame.yMin > (float)Screen.height)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x00144C74 File Offset: 0x00143074
		public void build()
		{
			this._frame = this.calculate();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].build();
			}
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x00144CBA File Offset: 0x001430BA
		public void add(Sleek sleek)
		{
			this.children.Add(sleek);
			sleek._parent = this;
			sleek.build();
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00144CD5 File Offset: 0x001430D5
		public void addLabel(string text, ESleekSide side)
		{
			this.addLabel(text, Color.white, side);
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x00144CE4 File Offset: 0x001430E4
		public void addLabel(string text, Color color, ESleekSide side)
		{
			this.sideLabel = new SleekLabel();
			if (side == ESleekSide.LEFT)
			{
				this.sideLabel.positionOffset_X = -205;
				this.sideLabel.fontAlignment = TextAnchor.MiddleRight;
			}
			else if (side == ESleekSide.RIGHT)
			{
				this.sideLabel.positionOffset_X = 5;
				this.sideLabel.positionScale_X = 1f;
				this.sideLabel.fontAlignment = TextAnchor.MiddleLeft;
			}
			this.sideLabel.positionOffset_Y = -20;
			this.sideLabel.positionScale_Y = 0.5f;
			this.sideLabel.sizeOffset_X = 200;
			this.sideLabel.sizeOffset_Y = 40;
			if (color != Color.white)
			{
				this.sideLabel.foregroundTint = ESleekTint.NONE;
				this.sideLabel.foregroundColor = color;
			}
			this.sideLabel.text = text;
			this.add(this.sideLabel);
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x00144DCC File Offset: 0x001431CC
		public void updateLabel(string text)
		{
			this.sideLabel.text = text;
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x00144DDA File Offset: 0x001431DA
		public int search(Sleek sleek)
		{
			return this.children.IndexOf(sleek);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x00144DE8 File Offset: 0x001431E8
		public void remove(Sleek sleek)
		{
			sleek._parent = null;
			sleek.destroy();
			this.children.Remove(sleek);
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00144E04 File Offset: 0x00143204
		public void remove()
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i]._parent = null;
				this.children[i].destroy();
			}
			this.children.Clear();
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00144E5C File Offset: 0x0014325C
		protected void update()
		{
			if (Event.current.type == EventType.Repaint)
			{
				if (this.isLerpingPositionOffset)
				{
					if (this.positionOffsetLerp == ESleekLerp.LINEAR)
					{
						if (Time.realtimeSinceStartup - this.positionOffsetLerpTicked > this.positionOffsetLerpTime)
						{
							this.isLerpingPositionOffset = false;
							this.positionOffset_X = this.toPositionOffset_X;
							this.positionOffset_Y = this.toPositionOffset_Y;
						}
						else
						{
							this.positionOffset_X = (int)Mathf.Lerp((float)this.fromPositionOffset_X, (float)this.toPositionOffset_X, (Time.realtimeSinceStartup - this.positionOffsetLerpTicked) / this.positionOffsetLerpTime);
							this.positionOffset_Y = (int)Mathf.Lerp((float)this.fromPositionOffset_Y, (float)this.toPositionOffset_Y, (Time.realtimeSinceStartup - this.positionOffsetLerpTicked) / this.positionOffsetLerpTime);
						}
					}
					else if (this.positionOffsetLerp == ESleekLerp.EXPONENTIAL)
					{
						if (Mathf.Abs(this.toPositionOffset_X - this.positionOffset_X) < 1 && Mathf.Abs(this.toPositionOffset_Y - this.positionOffset_Y) < 1)
						{
							this.isLerpingPositionOffset = false;
							this.positionOffset_X = this.toPositionOffset_X;
							this.positionOffset_Y = this.toPositionOffset_Y;
						}
						else
						{
							this.positionOffset_X = (int)Mathf.Lerp((float)this.positionOffset_X, (float)this.toPositionOffset_X, (Time.realtimeSinceStartup - this.positionOffsetLerpTicked) * this.positionOffsetLerpTime);
							this.positionOffset_Y = (int)Mathf.Lerp((float)this.positionOffset_Y, (float)this.toPositionOffset_Y, (Time.realtimeSinceStartup - this.positionOffsetLerpTicked) * this.positionOffsetLerpTime);
							this.positionOffsetLerpTicked = Time.realtimeSinceStartup;
						}
					}
				}
				if (this.isLerpingPositionScale)
				{
					if (this.positionScaleLerp == ESleekLerp.LINEAR)
					{
						if (Time.realtimeSinceStartup - this.positionScaleLerpTicked > this.positionScaleLerpTime)
						{
							this.isLerpingPositionScale = false;
							this.positionScale_X = this.toPositionScale_X;
							this.positionScale_Y = this.toPositionScale_Y;
						}
						else
						{
							this.positionScale_X = Mathf.Lerp(this.fromPositionScale_X, this.toPositionScale_X, (Time.realtimeSinceStartup - this.positionScaleLerpTicked) / this.positionScaleLerpTime);
							this.positionScale_Y = Mathf.Lerp(this.fromPositionScale_Y, this.toPositionScale_Y, (Time.realtimeSinceStartup - this.positionScaleLerpTicked) / this.positionScaleLerpTime);
						}
					}
					else if (this.positionScaleLerp == ESleekLerp.EXPONENTIAL)
					{
						if (Mathf.Abs(this.toPositionScale_X - this.positionScale_X) < 0.01f && Mathf.Abs(this.toPositionScale_Y - this.positionScale_Y) < 0.01f)
						{
							this.isLerpingPositionScale = false;
							this.positionScale_X = this.toPositionScale_X;
							this.positionScale_Y = this.toPositionScale_Y;
						}
						else
						{
							this.positionScale_X = Mathf.Lerp(this.positionScale_X, this.toPositionScale_X, (Time.realtimeSinceStartup - this.positionScaleLerpTicked) * this.positionScaleLerpTime);
							this.positionScale_Y = Mathf.Lerp(this.positionScale_Y, this.toPositionScale_Y, (Time.realtimeSinceStartup - this.positionScaleLerpTicked) * this.positionScaleLerpTime);
							this.positionScaleLerpTicked = Time.realtimeSinceStartup;
						}
					}
				}
				if (this.isLerpingSizeOffset)
				{
					if (this.sizeOffsetLerp == ESleekLerp.LINEAR)
					{
						if (Time.realtimeSinceStartup - this.sizeOffsetLerpTicked > this.sizeOffsetLerpTime)
						{
							this.isLerpingSizeOffset = false;
							this.sizeOffset_X = this.toSizeOffset_X;
							this.sizeOffset_Y = this.toSizeOffset_Y;
						}
						else
						{
							this.sizeOffset_X = (int)Mathf.Lerp((float)this.fromSizeOffset_X, (float)this.toSizeOffset_X, (Time.realtimeSinceStartup - this.sizeOffsetLerpTicked) / this.sizeOffsetLerpTime);
							this.sizeOffset_Y = (int)Mathf.Lerp((float)this.fromSizeOffset_Y, (float)this.toSizeOffset_Y, (Time.realtimeSinceStartup - this.sizeOffsetLerpTicked) / this.sizeOffsetLerpTime);
						}
					}
					else if (this.sizeOffsetLerp == ESleekLerp.EXPONENTIAL)
					{
						if (Mathf.Abs(this.toSizeOffset_X - this.sizeOffset_X) < 1 && Mathf.Abs(this.toSizeOffset_Y - this.sizeOffset_Y) < 1)
						{
							this.isLerpingSizeOffset = false;
							this.sizeOffset_X = this.toSizeOffset_X;
							this.sizeOffset_Y = this.toSizeOffset_Y;
						}
						else
						{
							this.sizeOffset_X = (int)Mathf.Lerp((float)this.sizeOffset_X, (float)this.toSizeOffset_X, (Time.realtimeSinceStartup - this.sizeOffsetLerpTicked) * this.sizeOffsetLerpTime);
							this.sizeOffset_Y = (int)Mathf.Lerp((float)this.sizeOffset_Y, (float)this.toSizeOffset_Y, (Time.realtimeSinceStartup - this.sizeOffsetLerpTicked) * this.sizeOffsetLerpTime);
							this.sizeOffsetLerpTicked = Time.realtimeSinceStartup;
						}
					}
				}
				if (this.isLerpingSizeScale)
				{
					if (this.sizeScaleLerp == ESleekLerp.LINEAR)
					{
						if (Time.realtimeSinceStartup - this.sizeScaleLerpTicked > this.sizeScaleLerpTime)
						{
							this.isLerpingSizeScale = false;
							this.sizeScale_X = this.toSizeScale_X;
							this.sizeScale_Y = this.toSizeScale_Y;
						}
						else
						{
							this.sizeScale_X = Mathf.Lerp(this.fromSizeScale_X, this.toSizeScale_X, (Time.realtimeSinceStartup - this.sizeScaleLerpTicked) / this.sizeScaleLerpTime);
							this.sizeScale_Y = Mathf.Lerp(this.fromSizeScale_Y, this.toSizeScale_Y, (Time.realtimeSinceStartup - this.sizeScaleLerpTicked) / this.sizeScaleLerpTime);
						}
					}
					else if (this.sizeScaleLerp == ESleekLerp.EXPONENTIAL)
					{
						if (Mathf.Abs(this.toSizeScale_X - this.sizeScale_X) < 0.01f && Mathf.Abs(this.toSizeScale_Y - this.sizeScale_Y) < 0.01f)
						{
							this.isLerpingSizeScale = false;
							this.sizeScale_X = this.toSizeScale_X;
							this.sizeScale_Y = this.toSizeScale_Y;
						}
						else
						{
							this.sizeScale_X = Mathf.Lerp(this.sizeScale_X, this.toSizeScale_X, (Time.realtimeSinceStartup - this.sizeScaleLerpTicked) * this.sizeScaleLerpTime);
							this.sizeScale_Y = Mathf.Lerp(this.sizeScale_Y, this.toSizeScale_Y, (Time.realtimeSinceStartup - this.sizeScaleLerpTicked) * this.sizeScaleLerpTime);
							this.sizeScaleLerpTicked = Time.realtimeSinceStartup;
						}
					}
				}
				if (this.needsFrame)
				{
					this.needsFrame = false;
					this.build();
				}
			}
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x0014545D File Offset: 0x0014385D
		protected void init()
		{
			this.isVisible = true;
			this.isInputable = true;
			this.hideTooltip = false;
			this._children = new List<Sleek>();
			this.build();
		}

		// Token: 0x040021F5 RID: 8693
		private static Rect cullingRect;

		// Token: 0x040021F6 RID: 8694
		public ESleekTint backgroundTint;

		// Token: 0x040021F7 RID: 8695
		private Color _backgroundColor = Color.white;

		// Token: 0x040021F8 RID: 8696
		public ESleekTint foregroundTint;

		// Token: 0x040021F9 RID: 8697
		private Color _foregroundColor = Color.white;

		// Token: 0x040021FA RID: 8698
		public Color richOverrideColor = Palette.COLOR_W;

		// Token: 0x040021FB RID: 8699
		public bool isVisible;

		// Token: 0x040021FC RID: 8700
		public bool isHidden;

		// Token: 0x040021FD RID: 8701
		public bool isRich;

		// Token: 0x040021FE RID: 8702
		public bool isInputable;

		// Token: 0x040021FF RID: 8703
		public bool hideTooltip;

		// Token: 0x04002200 RID: 8704
		private Sleek _parent;

		// Token: 0x04002201 RID: 8705
		private List<Sleek> _children;

		// Token: 0x04002202 RID: 8706
		private int fromPositionOffset_X;

		// Token: 0x04002203 RID: 8707
		private int fromPositionOffset_Y;

		// Token: 0x04002204 RID: 8708
		private int toPositionOffset_X;

		// Token: 0x04002205 RID: 8709
		private int toPositionOffset_Y;

		// Token: 0x04002206 RID: 8710
		private float fromPositionScale_X;

		// Token: 0x04002207 RID: 8711
		private float fromPositionScale_Y;

		// Token: 0x04002208 RID: 8712
		private float toPositionScale_X;

		// Token: 0x04002209 RID: 8713
		private float toPositionScale_Y;

		// Token: 0x0400220A RID: 8714
		private int fromSizeOffset_X;

		// Token: 0x0400220B RID: 8715
		private int fromSizeOffset_Y;

		// Token: 0x0400220C RID: 8716
		private int toSizeOffset_X;

		// Token: 0x0400220D RID: 8717
		private int toSizeOffset_Y;

		// Token: 0x0400220E RID: 8718
		private float fromSizeScale_X;

		// Token: 0x0400220F RID: 8719
		private float fromSizeScale_Y;

		// Token: 0x04002210 RID: 8720
		private float toSizeScale_X;

		// Token: 0x04002211 RID: 8721
		private float toSizeScale_Y;

		// Token: 0x04002212 RID: 8722
		private ESleekLerp positionOffsetLerp;

		// Token: 0x04002213 RID: 8723
		private float positionOffsetLerpTime;

		// Token: 0x04002214 RID: 8724
		private float positionOffsetLerpTicked;

		// Token: 0x04002215 RID: 8725
		private bool isLerpingPositionOffset;

		// Token: 0x04002216 RID: 8726
		private ESleekLerp positionScaleLerp;

		// Token: 0x04002217 RID: 8727
		private float positionScaleLerpTime;

		// Token: 0x04002218 RID: 8728
		private float positionScaleLerpTicked;

		// Token: 0x04002219 RID: 8729
		private bool isLerpingPositionScale;

		// Token: 0x0400221A RID: 8730
		private ESleekLerp sizeOffsetLerp;

		// Token: 0x0400221B RID: 8731
		private float sizeOffsetLerpTime;

		// Token: 0x0400221C RID: 8732
		private float sizeOffsetLerpTicked;

		// Token: 0x0400221D RID: 8733
		private bool isLerpingSizeOffset;

		// Token: 0x0400221E RID: 8734
		private ESleekLerp sizeScaleLerp;

		// Token: 0x0400221F RID: 8735
		private float sizeScaleLerpTime;

		// Token: 0x04002220 RID: 8736
		private float sizeScaleLerpTicked;

		// Token: 0x04002221 RID: 8737
		private bool isLerpingSizeScale;

		// Token: 0x04002222 RID: 8738
		private bool needsFrame;

		// Token: 0x04002223 RID: 8739
		protected Rect _frame;

		// Token: 0x04002224 RID: 8740
		protected bool local;

		// Token: 0x04002225 RID: 8741
		public SleekLabel sideLabel;

		// Token: 0x04002226 RID: 8742
		private ESleekConstraint _constraint;

		// Token: 0x04002227 RID: 8743
		private int _constrain_X;

		// Token: 0x04002228 RID: 8744
		private int _constrain_Y;

		// Token: 0x04002229 RID: 8745
		private int _positionOffset_X;

		// Token: 0x0400222A RID: 8746
		private int _positionOffset_Y;

		// Token: 0x0400222B RID: 8747
		private float _positionScale_X;

		// Token: 0x0400222C RID: 8748
		private float _positionScale_Y;

		// Token: 0x0400222D RID: 8749
		private int _sizeOffset_X;

		// Token: 0x0400222E RID: 8750
		private int _sizeOffset_Y;

		// Token: 0x0400222F RID: 8751
		private float _sizeScale_X;

		// Token: 0x04002230 RID: 8752
		private float _sizeScale_Y;
	}
}
