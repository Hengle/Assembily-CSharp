using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x020006CC RID: 1740
	public class SleekButtonState : SleekButton
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x00146E7C File Offset: 0x0014527C
		public SleekButtonState(params GUIContent[] newStates)
		{
			base.init();
			this.backgroundTint = ESleekTint.BACKGROUND;
			this._state = 0;
			this.icon = new SleekImageTexture();
			this.icon.positionOffset_X = 5;
			this.icon.positionOffset_Y = 5;
			this.icon.sizeOffset_X = 20;
			this.icon.sizeOffset_Y = 20;
			base.add(this.icon);
			this.fontStyle = FontStyle.Bold;
			this.fontAlignment = TextAnchor.MiddleCenter;
			this.fontSize = SleekRender.FONT_SIZE;
			this.setContent(newStates);
			this.onClickedButton = new ClickedButton(this.onClickedState);
			this.calculateContent();
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x00146F24 File Offset: 0x00145324
		public GUIContent[] states
		{
			get
			{
				return this._states;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x00146F2C File Offset: 0x0014532C
		// (set) Token: 0x0600323D RID: 12861 RVA: 0x00146F34 File Offset: 0x00145334
		public int state
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				if (this.state < this.states.Length && this.states[this.state] != null)
				{
					base.text = this.states[this.state].text;
					this.icon.texture = (Texture2D)this.states[this.state].image;
				}
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00146FA8 File Offset: 0x001453A8
		private void onClickedState(SleekButton button)
		{
			if (Event.current.button == 0)
			{
				this._state++;
				if (this.state >= this.states.Length)
				{
					this._state = 0;
				}
			}
			else
			{
				this._state--;
				if (this.state < 0)
				{
					this._state = this.states.Length - 1;
				}
			}
			if (this.state < this.states.Length && this.states[this.state] != null)
			{
				base.text = this.states[this.state].text;
				this.icon.texture = (Texture2D)this.states[this.state].image;
				if (this.onSwappedState != null)
				{
					this.onSwappedState(this, this.state);
				}
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x00147098 File Offset: 0x00145498
		public void setContent(params GUIContent[] newStates)
		{
			this._states = newStates;
			if (this.state >= this.states.Length)
			{
				this._state = 0;
			}
			if (this.states.Length > 0 && this.states[this.state] != null)
			{
				base.text = this.states[this.state].text;
			}
			else
			{
				base.text = string.Empty;
			}
			if (this.states.Length > 0 && this.states[this.state] != null)
			{
				this.icon.texture = (Texture2D)this.states[this.state].image;
			}
			else
			{
				this.icon.texture = null;
			}
		}

		// Token: 0x04002240 RID: 8768
		private SleekImageTexture icon;

		// Token: 0x04002241 RID: 8769
		private GUIContent[] _states;

		// Token: 0x04002242 RID: 8770
		private int _state;

		// Token: 0x04002243 RID: 8771
		public SwappedState onSwappedState;
	}
}
