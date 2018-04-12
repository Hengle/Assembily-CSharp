using System;
using System.Text;
using SDG.Framework.UI.Sleek2;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000716 RID: 1814
	public class SleekWindow : Sleek
	{
		// Token: 0x06003389 RID: 13193 RVA: 0x0014DEB4 File Offset: 0x0014C2B4
		public SleekWindow()
		{
			SleekWindow._active = this;
			Cursor.visible = false;
			SleekWindow.freeStyle = (GUISkin)Resources.Load("UI/Free/Skin");
			SleekWindow.proStyle = (GUISkin)Resources.Load("UI/Pro/Skin");
			this.cursor = (Texture2D)Resources.Load("UI/Cursor");
			this.showCursor = true;
			this.isEnabled = true;
			this.drawCursorWhileDisabled = false;
			this.cursorRect = new Rect(0f, 0f, 20f, 20f);
			this.tooltipRect = new Rect(0f, 0f, 400f, 60f);
			this.debugRect = new Rect(0f, 0f, 800f, 30f);
			base.init();
			base.sizeScale_X = 1f;
			base.sizeScale_Y = 1f;
			this.totalFrames = 0;
			this.totalTime = 0f;
			this.fpsMin = int.MaxValue;
			this.fpsMax = int.MinValue;
			this.fps = 0;
			this.frames = 0;
			this.lastFrame = Time.realtimeSinceStartup;
			this.debugBuilder = new StringBuilder(512);
			SleekRender.allowInput = true;
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600338A RID: 13194 RVA: 0x0014DFF5 File Offset: 0x0014C3F5
		public static SleekWindow active
		{
			get
			{
				return SleekWindow._active;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x0014DFFC File Offset: 0x0014C3FC
		public float mouse_x
		{
			get
			{
				return this._mouse_x;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600338C RID: 13196 RVA: 0x0014E004 File Offset: 0x0014C404
		public float mouse_y
		{
			get
			{
				return (float)Screen.height - this._mouse_y;
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x0014E014 File Offset: 0x0014C414
		public void updateDebug()
		{
			this.frames++;
			if (Time.realtimeSinceStartup - this.lastFrame > 1f)
			{
				this.fps = (int)((float)this.frames / (Time.realtimeSinceStartup - this.lastFrame));
				if (this.fps > 0)
				{
					this.fpsMin = Mathf.Min(this.fpsMin, this.fps);
					this.fpsMax = Mathf.Max(this.fpsMax, this.fps);
					this.totalFrames += this.frames;
					this.totalTime += Time.realtimeSinceStartup - this.lastFrame;
				}
				this.lastFrame = Time.realtimeSinceStartup;
				this.frames = 0;
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0014E0D8 File Offset: 0x0014C4D8
		public override void draw(bool ignoreCulling)
		{
			if (Screen.width != this.cachedScreenWidth || Screen.height != this.cachedScreenHeight)
			{
				this.cachedScreenWidth = Screen.width;
				this.cachedScreenHeight = Screen.height;
				base.build();
			}
			Cursor.visible = false;
			bool proUI = OptionsSettings.proUI;
			if (!proUI)
			{
				if (!proUI)
				{
					GUI.skin = SleekWindow.freeStyle;
				}
			}
			else
			{
				GUI.skin = SleekWindow.proStyle;
			}
			if (this.isEnabled)
			{
				if (Input.mousePosition.x != this._mouse_x || Input.mousePosition.y != this._mouse_y)
				{
					this._mouse_x = Input.mousePosition.x - base.frame.x;
					this._mouse_y = Input.mousePosition.y - base.frame.y;
					if (this.onMovedMouse != null)
					{
						this.onMovedMouse(this.mouse_x, this.mouse_y);
					}
				}
				base.update();
				base.drawChildren(ignoreCulling);
				if (OptionsSettings.debug)
				{
					Color color = Color.green;
					this.debugBuilder.Length = 0;
					if (Provider.isConnected)
					{
						if (!Provider.isServer && Time.realtimeSinceStartup - Provider.lastNet > 3f)
						{
							color = Color.red;
							this.debugBuilder.Append("Server not responded in: ");
							this.debugBuilder.Append((int)(Time.realtimeSinceStartup - Provider.lastNet));
							this.debugBuilder.Append("s Automatically disconnecting in: ");
							this.debugBuilder.Append(Provider.CLIENT_TIMEOUT - (int)(Time.realtimeSinceStartup - Provider.lastNet));
							this.debugBuilder.Append("s");
						}
						else
						{
							this.debugBuilder.Append(this.fps);
							this.debugBuilder.Append("/s ");
							this.debugBuilder.Append((int)(Provider.ping * 1000f));
							this.debugBuilder.Append("ms ");
							this.debugBuilder.Append(Provider.APP_VERSION);
							if (Player.player != null && Player.player.channel.owner.isAdmin)
							{
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.look.isOrbiting) ? "F1" : "Orbiting");
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.look.isTracking) ? "F2" : "Tracking");
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.look.isLocking) ? "F3" : "Locking");
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.look.isFocusing) ? "F4" : "Focusing");
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.look.isSmoothing) ? "F5" : "Smoothing");
								this.debugBuilder.Append(" ");
								this.debugBuilder.Append((!Player.player.workzone.isBuilding) ? "F6" : "Building");
							}
							if (Assets.isLoading)
							{
								this.debugBuilder.Append(" Assets");
							}
							if (Provider.isLoadingInventory)
							{
								this.debugBuilder.Append(" Economy");
							}
							if (Provider.isLoadingUGC)
							{
								this.debugBuilder.Append(" Workshop");
							}
							if (Level.isLoadingContent)
							{
								this.debugBuilder.Append(" Content");
							}
							if (Level.isLoadingLighting)
							{
								this.debugBuilder.Append(" Lighting");
							}
							if (Level.isLoadingVehicles)
							{
								this.debugBuilder.Append(" Vehicles");
							}
							if (Level.isLoadingBarricades)
							{
								this.debugBuilder.Append(" Barricades");
							}
							if (Level.isLoadingStructures)
							{
								this.debugBuilder.Append(" Structures");
							}
							if (Level.isLoadingArea)
							{
								this.debugBuilder.Append(" Area");
							}
							if (Player.isLoadingInventory)
							{
								this.debugBuilder.Append(" Inventory");
							}
							if (Player.isLoadingLife)
							{
								this.debugBuilder.Append(" Life");
							}
							if (Player.isLoadingClothing)
							{
								this.debugBuilder.Append(" Clothing");
							}
						}
					}
					else
					{
						this.debugBuilder.Append(this.fps);
						this.debugBuilder.Append("/s");
					}
					SleekRender.drawLabel(this.debugRect, FontStyle.Normal, TextAnchor.UpperLeft, 12, false, color, this.debugBuilder.ToString());
				}
			}
			if (this.isEnabled || this.drawCursorWhileDisabled)
			{
				if (this.showCursor)
				{
					this.cursorRect.x = Input.mousePosition.x;
					this.cursorRect.y = (float)Screen.height - Input.mousePosition.y;
					GUI.color = OptionsSettings.cursorColor;
					if (Sleek2Pointer.cursor != null)
					{
						this.cursorRect.position = this.cursorRect.position - Sleek2Pointer.hotspot;
						GUI.DrawTexture(this.cursorRect, Sleek2Pointer.cursor);
					}
					else if (this.cursor != null)
					{
						GUI.DrawTexture(this.cursorRect, this.cursor);
					}
					GUI.color = Color.white;
					if (Event.current.type == EventType.Repaint)
					{
						if (GUI.tooltip != this.lastTooltip)
						{
							this.lastTooltip = GUI.tooltip;
							this.startedTooltip = Time.realtimeSinceStartup;
						}
						if (GUI.tooltip != string.Empty && (double)(Time.realtimeSinceStartup - this.startedTooltip) > 0.5)
						{
							this.tooltipRect.y = (float)Screen.height - Input.mousePosition.y - 30f;
							if (Input.mousePosition.x > (float)Screen.width - this.tooltipRect.width - 30f)
							{
								this.tooltipRect.x = Input.mousePosition.x - 30f - this.tooltipRect.width;
								SleekRender.drawLabel(this.tooltipRect, FontStyle.Bold, TextAnchor.MiddleRight, 12, false, SleekRender.tooltip, GUI.tooltip);
							}
							else
							{
								this.tooltipRect.x = Input.mousePosition.x + 30f;
								SleekRender.drawLabel(this.tooltipRect, FontStyle.Bold, TextAnchor.MiddleLeft, 12, false, SleekRender.tooltip, GUI.tooltip);
							}
						}
					}
					if (Cursor.lockState != CursorLockMode.None)
					{
						Cursor.lockState = CursorLockMode.None;
					}
				}
				else if (Cursor.lockState != CursorLockMode.Locked)
				{
					Cursor.lockState = CursorLockMode.Locked;
				}
			}
			if (Event.current.type == EventType.MouseDown)
			{
				if (this.onClickedMouse != null)
				{
					this.onClickedMouse();
				}
				if (this.onClickedMouseStarted != null)
				{
					this.onClickedMouseStarted();
				}
			}
			else if (Event.current.type == EventType.MouseUp && this.onClickedMouseStopped != null)
			{
				this.onClickedMouseStopped();
			}
		}

		// Token: 0x040022DA RID: 8922
		private static GUISkin freeStyle;

		// Token: 0x040022DB RID: 8923
		private static GUISkin proStyle;

		// Token: 0x040022DC RID: 8924
		private static SleekWindow _active;

		// Token: 0x040022DD RID: 8925
		public ClickedMouse onClickedMouse;

		// Token: 0x040022DE RID: 8926
		public ClickedMouseStarted onClickedMouseStarted;

		// Token: 0x040022DF RID: 8927
		public ClickedMouseStopped onClickedMouseStopped;

		// Token: 0x040022E0 RID: 8928
		public MovedMouse onMovedMouse;

		// Token: 0x040022E1 RID: 8929
		public bool showCursor;

		// Token: 0x040022E2 RID: 8930
		public bool isEnabled;

		// Token: 0x040022E3 RID: 8931
		public bool drawCursorWhileDisabled;

		// Token: 0x040022E4 RID: 8932
		private GUISkin style;

		// Token: 0x040022E5 RID: 8933
		private Rect cursorRect;

		// Token: 0x040022E6 RID: 8934
		private Rect tooltipRect;

		// Token: 0x040022E7 RID: 8935
		private Rect debugRect;

		// Token: 0x040022E8 RID: 8936
		private Texture cursor;

		// Token: 0x040022E9 RID: 8937
		private string lastTooltip;

		// Token: 0x040022EA RID: 8938
		private float startedTooltip;

		// Token: 0x040022EB RID: 8939
		private float _mouse_x;

		// Token: 0x040022EC RID: 8940
		private float _mouse_y;

		// Token: 0x040022ED RID: 8941
		public int totalFrames;

		// Token: 0x040022EE RID: 8942
		public float totalTime;

		// Token: 0x040022EF RID: 8943
		public int fpsMin;

		// Token: 0x040022F0 RID: 8944
		public int fpsMax;

		// Token: 0x040022F1 RID: 8945
		private int fps;

		// Token: 0x040022F2 RID: 8946
		private float lastFrame;

		// Token: 0x040022F3 RID: 8947
		private int frames;

		// Token: 0x040022F4 RID: 8948
		private StringBuilder debugBuilder;

		// Token: 0x040022F5 RID: 8949
		private int cachedScreenWidth;

		// Token: 0x040022F6 RID: 8950
		private int cachedScreenHeight;
	}
}
