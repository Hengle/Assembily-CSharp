using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Rendering;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x0200016D RID: 365
	public class DevkitRotationHandle : MonoBehaviour, IDevkitHandle, IDevkitInteractableBeginHoverHandler, IDevkitInteractableBeginDragHandler, IDevkitInteractableContinueDragHandler, IDevkitInteractableEndDragHandler, IDevkitInteractableEndHoverHandler
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0005744E File Offset: 0x0005584E
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00057455 File Offset: 0x00055855
		[TerminalCommandProperty("input.devkit.pivot.rotation.delta_sensitivity", "multiplier for rotation delta", 1)]
		public static float handleSensitivity
		{
			get
			{
				return DevkitRotationHandle._handleSensitivity;
			}
			set
			{
				DevkitRotationHandle._handleSensitivity = value;
				TerminalUtility.printCommandPass("Set delta_sensitivity to: " + DevkitRotationHandle.handleSensitivity);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00057476 File Offset: 0x00055876
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0005747D File Offset: 0x0005587D
		[TerminalCommandProperty("input.devkit.pivot.rotation.screensize", "percentage of screen size", 0.5f)]
		public static float handleScreensize
		{
			get
			{
				return DevkitRotationHandle._handleScreensize;
			}
			set
			{
				DevkitRotationHandle._handleScreensize = value;
				TerminalUtility.printCommandPass("Set screensize to: " + DevkitRotationHandle.handleScreensize);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000AFB RID: 2811 RVA: 0x000574A0 File Offset: 0x000558A0
		// (remove) Token: 0x06000AFC RID: 2812 RVA: 0x000574D8 File Offset: 0x000558D8
		public event DevkitRotationHandle.DevkitRotationTransformedHandler transformed;

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0005750E File Offset: 0x0005590E
		protected bool isSnapping
		{
			get
			{
				return Input.GetKey(ControlsSettings.other);
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0005751A File Offset: 0x0005591A
		public void suggestTransform(Vector3 position, Quaternion rotation)
		{
			base.transform.position = position;
			this.suggestedRotation = rotation;
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.NONE)
			{
				base.transform.rotation = this.suggestedRotation;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0005754C File Offset: 0x0005594C
		public void beginHover(InteractionData data)
		{
			if (data.collider == this.handle_x)
			{
				this.hover = DevkitRotationHandle.EDevkitRotationHandleSelection.X;
			}
			else if (data.collider == this.handle_y)
			{
				this.hover = DevkitRotationHandle.EDevkitRotationHandleSelection.Y;
			}
			else if (data.collider == this.handle_z)
			{
				this.hover = DevkitRotationHandle.EDevkitRotationHandleSelection.Z;
			}
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000575BC File Offset: 0x000559BC
		public void beginDrag(InteractionData data)
		{
			Vector3 from;
			Vector3 rhs;
			if (data.collider == this.handle_x)
			{
				this.drag = DevkitRotationHandle.EDevkitRotationHandleSelection.X;
				from = base.transform.forward;
				rhs = base.transform.up;
			}
			else if (data.collider == this.handle_y)
			{
				this.drag = DevkitRotationHandle.EDevkitRotationHandleSelection.Y;
				from = base.transform.right;
				rhs = base.transform.forward;
			}
			else
			{
				if (!(data.collider == this.handle_z))
				{
					return;
				}
				this.drag = DevkitRotationHandle.EDevkitRotationHandleSelection.Z;
				from = base.transform.right;
				rhs = base.transform.up;
			}
			this.handleOffset = data.point - base.transform.position;
			this.mouseOrigin = Input.mousePosition;
			this.prevRotationResult = 0f;
			this.displayAngle = 0f;
			this.angleOrigin = Vector3.Angle(from, this.handleOffset);
			if (Vector3.Dot(this.handleOffset, rhs) < 0f)
			{
				this.angleOrigin = 360f - this.angleOrigin;
			}
			this.angleOrigin = 0.0174532924f * this.angleOrigin;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00057704 File Offset: 0x00055B04
		public void continueDrag(InteractionData data)
		{
			Vector3 rhs;
			Vector3 axis;
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.X)
			{
				rhs = base.transform.right;
				axis = new Vector3(1f, 0f, 0f);
			}
			else if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Y)
			{
				rhs = base.transform.up;
				axis = new Vector3(0f, 1f, 0f);
			}
			else
			{
				if (this.drag != DevkitRotationHandle.EDevkitRotationHandleSelection.Z)
				{
					return;
				}
				rhs = base.transform.forward;
				axis = new Vector3(0f, 0f, 1f);
			}
			Vector2 b = MainCamera.instance.WorldToScreenPoint(base.transform.position);
			Vector3 position = base.transform.position + this.handleOffset;
			Vector2 a = MainCamera.instance.WorldToScreenPoint(position);
			Vector2 normalized = (a - b).normalized;
			Vector2 lhs = new Vector2(normalized.y, -normalized.x);
			Vector2 a2 = Input.mousePosition - this.mouseOrigin;
			float magnitude = a2.magnitude;
			float num = Vector2.Dot(lhs, a2 / magnitude) * magnitude;
			float num2 = Vector3.Dot(MainCamera.instance.transform.forward, rhs);
			if (num2 > 0f)
			{
				num *= -1f;
			}
			float num3 = a2.y;
			if (Vector3.Cross(MainCamera.instance.transform.forward, rhs).y < 0f)
			{
				num3 *= -1f;
			}
			float num4 = Mathf.Lerp(num3, num, Mathf.Abs(num2));
			if (float.IsNaN(num4) || float.IsInfinity(num4))
			{
				return;
			}
			num4 *= DevkitRotationHandle.handleSensitivity;
			if (Input.GetKey(ControlsSettings.snap))
			{
				num4 = (float)Mathf.RoundToInt(num4 / DevkitSelectionToolOptions.instance.snapRotation) * DevkitSelectionToolOptions.instance.snapRotation;
			}
			float num5 = num4 - this.prevRotationResult;
			if (Mathf.Abs(num5) < 0.001f)
			{
				return;
			}
			this.triggerTransformed(axis, num5);
			this.prevRotationResult = num4;
			this.displayAngle = num4;
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.X || this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Y)
			{
				this.displayAngle = -num4;
			}
			this.displayAngle *= 0.0174532924f;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00057982 File Offset: 0x00055D82
		public void endDrag(InteractionData data)
		{
			this.drag = DevkitRotationHandle.EDevkitRotationHandleSelection.NONE;
			base.transform.rotation = this.suggestedRotation;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0005799C File Offset: 0x00055D9C
		public void endHover(InteractionData data)
		{
			this.hover = DevkitRotationHandle.EDevkitRotationHandleSelection.NONE;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000579A5 File Offset: 0x00055DA5
		protected void triggerTransformed(Vector3 axis, float delta)
		{
			if (this.transformed != null)
			{
				this.transformed(this, axis, delta);
			}
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000579C0 File Offset: 0x00055DC0
		protected void circle(Vector3 horizontalAxis, Vector3 verticalAxis, bool isDragging, bool isHovering, Color color)
		{
			if (isDragging && this.isSnapping)
			{
				GL.Color(new Color(0f, 0f, 0f, 0.5f));
				float num = this.angleOrigin + this.displayAngle;
				float num2 = 0.0174532924f * DevkitSelectionToolOptions.instance.snapRotation;
				int num3 = Mathf.Max(1, Mathf.CeilToInt(1.57079637f / num2));
				for (int i = -num3; i <= num3; i++)
				{
					float f = num + (float)i * num2;
					float d = Mathf.Cos(f);
					float d2 = Mathf.Sin(f);
					GLUtility.line(horizontalAxis * d * 0.9f + verticalAxis * d2 * 0.9f, horizontalAxis * d * 1.1f + verticalAxis * d2 * 1.1f);
				}
			}
			GL.Color((!isDragging) ? ((!isHovering) ? color : Color.yellow) : Color.white);
			float num4 = 6.28318548f;
			float num5 = 0f;
			float num6 = num4 / 32f;
			Vector3 v = GLUtility.matrix.MultiplyPoint3x4(horizontalAxis);
			while (num5 < num4)
			{
				num5 += num6;
				float f2 = Mathf.Min(num5, num4);
				float d3 = Mathf.Cos(f2);
				float d4 = Mathf.Sin(f2);
				Vector3 vector = GLUtility.matrix.MultiplyPoint3x4(horizontalAxis * d3 + verticalAxis * d4);
				GL.Vertex(v);
				GL.Vertex(vector);
				v = vector;
			}
			if (isDragging)
			{
				float f3 = this.angleOrigin;
				float d5 = Mathf.Cos(f3) * 1.5f;
				float d6 = Mathf.Sin(f3) * 1.5f;
				Vector3 end = horizontalAxis * d5 + verticalAxis * d6;
				GLUtility.line(Vector3.zero, end);
				float f4 = this.angleOrigin + this.displayAngle;
				float d7 = Mathf.Cos(f4) * 1.5f;
				float d8 = Mathf.Sin(f4) * 1.5f;
				Vector3 end2 = horizontalAxis * d7 + verticalAxis * d8;
				GLUtility.line(Vector3.zero, end2);
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00057C04 File Offset: 0x00056004
		protected void handleGLRender()
		{
			GLUtility.LINE_FLAT_COLOR.SetPass(0);
			GL.Begin(1);
			GLUtility.matrix = base.transform.localToWorldMatrix;
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.NONE || this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.X)
			{
				this.circle(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 0f), this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.X, this.hover == DevkitRotationHandle.EDevkitRotationHandleSelection.X, Color.red);
			}
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.NONE || this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Y)
			{
				this.circle(new Vector3(1f, 0f, 0f), new Vector3(0f, 0f, 1f), this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Y, this.hover == DevkitRotationHandle.EDevkitRotationHandleSelection.Y, Color.green);
			}
			if (this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.NONE || this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Z)
			{
				this.circle(new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), this.drag == DevkitRotationHandle.EDevkitRotationHandleSelection.Z, this.hover == DevkitRotationHandle.EDevkitRotationHandleSelection.Z, Color.blue);
			}
			GL.End();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00057D4C File Offset: 0x0005614C
		protected void updateScale()
		{
			if (MainCamera.instance == null)
			{
				return;
			}
			Vector3 position = MainCamera.instance.transform.position;
			float magnitude = (base.transform.position - position).magnitude;
			float num = magnitude;
			num *= DevkitRotationHandle.handleScreensize;
			base.transform.localScale = new Vector3(num, num, num);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00057DB1 File Offset: 0x000561B1
		protected void Update()
		{
			this.updateScale();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00057DBC File Offset: 0x000561BC
		protected void OnEnable()
		{
			GLRenderer.render += this.handleGLRender;
			base.gameObject.layer = LayerMasks.LOGIC;
			this.handle_x = base.gameObject.AddComponent<BoxCollider>();
			this.handle_x.size = new Vector3(0f, 2f, 2f);
			this.handle_y = base.gameObject.AddComponent<BoxCollider>();
			this.handle_y.size = new Vector3(2f, 0f, 2f);
			this.handle_z = base.gameObject.AddComponent<BoxCollider>();
			this.handle_z.size = new Vector3(2f, 2f, 0f);
			this.updateScale();
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00057E80 File Offset: 0x00056280
		protected void OnDisable()
		{
			GLRenderer.render -= this.handleGLRender;
			UnityEngine.Object.Destroy(this.handle_x);
			UnityEngine.Object.Destroy(this.handle_y);
			UnityEngine.Object.Destroy(this.handle_z);
		}

		// Token: 0x040007E8 RID: 2024
		protected static float _handleSensitivity = 1f;

		// Token: 0x040007E9 RID: 2025
		protected static float _handleScreensize = 0.5f;

		// Token: 0x040007EB RID: 2027
		protected DevkitRotationHandle.EDevkitRotationHandleSelection drag;

		// Token: 0x040007EC RID: 2028
		protected DevkitRotationHandle.EDevkitRotationHandleSelection hover;

		// Token: 0x040007ED RID: 2029
		protected BoxCollider handle_x;

		// Token: 0x040007EE RID: 2030
		protected BoxCollider handle_y;

		// Token: 0x040007EF RID: 2031
		protected BoxCollider handle_z;

		// Token: 0x040007F0 RID: 2032
		protected Vector3 mouseOrigin;

		// Token: 0x040007F1 RID: 2033
		protected Vector3 handleOffset;

		// Token: 0x040007F2 RID: 2034
		protected float angleOrigin;

		// Token: 0x040007F3 RID: 2035
		protected float prevRotationResult;

		// Token: 0x040007F4 RID: 2036
		protected float displayAngle;

		// Token: 0x040007F5 RID: 2037
		protected Quaternion suggestedRotation;

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x06000B0D RID: 2829
		public delegate void DevkitRotationTransformedHandler(DevkitRotationHandle handle, Vector3 axis, float delta);

		// Token: 0x0200016F RID: 367
		protected enum EDevkitRotationHandleSelection
		{
			// Token: 0x040007F7 RID: 2039
			NONE,
			// Token: 0x040007F8 RID: 2040
			X,
			// Token: 0x040007F9 RID: 2041
			Y,
			// Token: 0x040007FA RID: 2042
			Z
		}
	}
}
