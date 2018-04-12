using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Rendering;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000170 RID: 368
	public class DevkitScaleHandle : MonoBehaviour, IDevkitHandle, IDevkitInteractableBeginHoverHandler, IDevkitInteractableBeginDragHandler, IDevkitInteractableContinueDragHandler, IDevkitInteractableEndDragHandler, IDevkitInteractableEndHoverHandler
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00057ED2 File Offset: 0x000562D2
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x00057ED9 File Offset: 0x000562D9
		[TerminalCommandProperty("input.devkit.pivot.scale.delta_sensitivity", "multiplier for scale delta", 1)]
		public static float handleSensitivity
		{
			get
			{
				return DevkitScaleHandle._handleSensitivity;
			}
			set
			{
				DevkitScaleHandle._handleSensitivity = value;
				TerminalUtility.printCommandPass("Set delta_sensitivity to: " + DevkitScaleHandle.handleSensitivity);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00057EFA File Offset: 0x000562FA
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x00057F01 File Offset: 0x00056301
		[TerminalCommandProperty("input.devkit.scale.rotation.screensize", "percentage of screen size", 0.5f)]
		public static float handleScreensize
		{
			get
			{
				return DevkitScaleHandle._handleScreensize;
			}
			set
			{
				DevkitScaleHandle._handleScreensize = value;
				TerminalUtility.printCommandPass("Set screensize to: " + DevkitScaleHandle.handleScreensize);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B15 RID: 2837 RVA: 0x00057F24 File Offset: 0x00056324
		// (remove) Token: 0x06000B16 RID: 2838 RVA: 0x00057F5C File Offset: 0x0005635C
		public event DevkitScaleHandle.DevkitScaleTransformedHandler transformed;

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00057F92 File Offset: 0x00056392
		protected bool isSnapping
		{
			get
			{
				return Input.GetKey(ControlsSettings.other);
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00057F9E File Offset: 0x0005639E
		public void suggestTransform(Vector3 position, Quaternion rotation)
		{
			base.transform.position = position;
			base.transform.rotation = rotation;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00057FB8 File Offset: 0x000563B8
		public void beginHover(InteractionData data)
		{
			if (data.collider == this.handleVector_X)
			{
				this.hover = DevkitScaleHandle.EDevkitScaleHandleSelection.X;
			}
			else if (data.collider == this.handleVector_Y)
			{
				this.hover = DevkitScaleHandle.EDevkitScaleHandleSelection.Y;
			}
			else if (data.collider == this.handleVector_Z)
			{
				this.hover = DevkitScaleHandle.EDevkitScaleHandleSelection.Z;
			}
			else if (data.collider == this.handleUniform)
			{
				this.hover = DevkitScaleHandle.EDevkitScaleHandleSelection.UNIFORM;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00058048 File Offset: 0x00056448
		public void beginDrag(InteractionData data)
		{
			if (data.collider == this.handleVector_X)
			{
				this.drag = DevkitScaleHandle.EDevkitScaleHandleSelection.X;
			}
			else if (data.collider == this.handleVector_Y)
			{
				this.drag = DevkitScaleHandle.EDevkitScaleHandleSelection.Y;
			}
			else if (data.collider == this.handleVector_Z)
			{
				this.drag = DevkitScaleHandle.EDevkitScaleHandleSelection.Z;
			}
			else if (data.collider == this.handleUniform)
			{
				this.drag = DevkitScaleHandle.EDevkitScaleHandleSelection.UNIFORM;
			}
			this.mouseOrigin = Input.mousePosition;
			this.prevScaleResult = Vector3.zero;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000580F0 File Offset: 0x000564F0
		public void continueDrag(InteractionData data)
		{
			Vector3 b;
			Vector3 one;
			if (this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.X)
			{
				b = base.transform.right;
				one = new Vector3(this.inversion.x, 0f, 0f);
			}
			else if (this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.Y)
			{
				b = base.transform.up;
				one = new Vector3(0f, this.inversion.y, 0f);
			}
			else if (this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.Z)
			{
				b = base.transform.forward;
				one = new Vector3(0f, 0f, this.inversion.z);
			}
			else
			{
				if (this.drag != DevkitScaleHandle.EDevkitScaleHandleSelection.UNIFORM)
				{
					return;
				}
				b = (MainCamera.instance.transform.right + MainCamera.instance.transform.up).normalized;
				one = Vector3.one;
			}
			Vector3 b2 = MainCamera.instance.WorldToScreenPoint(base.transform.position);
			Vector3 vector = MainCamera.instance.WorldToScreenPoint(base.transform.position + b) - b2;
			Vector3 lhs = Input.mousePosition - this.mouseOrigin;
			float num = Vector3.Dot(lhs, vector.normalized) / vector.magnitude;
			num *= DevkitScaleHandle.handleSensitivity;
			if (Input.GetKey(ControlsSettings.snap))
			{
				num = (float)Mathf.RoundToInt(num / DevkitSelectionToolOptions.instance.snapScale) * DevkitSelectionToolOptions.instance.snapScale;
			}
			Vector3 a = num * one;
			Vector3 delta = a - this.prevScaleResult;
			this.triggerTransformed(delta);
			this.prevScaleResult = a;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000582B1 File Offset: 0x000566B1
		public void endDrag(InteractionData data)
		{
			this.drag = DevkitScaleHandle.EDevkitScaleHandleSelection.NONE;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000582BA File Offset: 0x000566BA
		public void endHover(InteractionData data)
		{
			this.hover = DevkitScaleHandle.EDevkitScaleHandleSelection.NONE;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000582C3 File Offset: 0x000566C3
		protected void triggerTransformed(Vector3 delta)
		{
			if (this.transformed != null)
			{
				this.transformed(this, delta);
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000582E0 File Offset: 0x000566E0
		protected void arrow(Vector3 normal, bool isDragging, bool isHovering, Color color)
		{
			GL.Color((!isDragging) ? ((!isHovering) ? color : Color.yellow) : Color.white);
			GLUtility.line(Vector3.zero, normal);
			GLUtility.boxWireframe(normal, new Vector3(0.1f, 0.1f, 0.1f));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0005833C File Offset: 0x0005673C
		protected void handleGLRender()
		{
			GLUtility.LINE_FLAT_COLOR.SetPass(0);
			GL.Begin(1);
			GLUtility.matrix = base.transform.localToWorldMatrix;
			this.arrow(new Vector3(this.inversion.x, 0f, 0f), this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.X, this.hover == DevkitScaleHandle.EDevkitScaleHandleSelection.X, Color.red);
			this.arrow(new Vector3(0f, this.inversion.y, 0f), this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.Y, this.hover == DevkitScaleHandle.EDevkitScaleHandleSelection.Y, Color.green);
			this.arrow(new Vector3(0f, 0f, this.inversion.z), this.drag == DevkitScaleHandle.EDevkitScaleHandleSelection.Z, this.hover == DevkitScaleHandle.EDevkitScaleHandleSelection.Z, Color.blue);
			GL.Color((this.drag != DevkitScaleHandle.EDevkitScaleHandleSelection.UNIFORM) ? ((this.hover != DevkitScaleHandle.EDevkitScaleHandleSelection.UNIFORM) ? Color.white : Color.yellow) : Color.white);
			GLUtility.boxWireframe(this.inversion * 0.125f, new Vector3(0.25f, 0.25f, 0.25f));
			GL.End();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00058474 File Offset: 0x00056874
		protected void updateScale()
		{
			if (MainCamera.instance == null)
			{
				return;
			}
			Vector3 position = MainCamera.instance.transform.position;
			Vector3 a = base.transform.position - position;
			float magnitude = a.magnitude;
			Vector3 lhs = a / magnitude;
			float num = magnitude;
			num *= DevkitScaleHandle.handleScreensize;
			base.transform.localScale = new Vector3(num, num, num);
			this.inversion.x = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.right) >= 0f) ? -1 : 1);
			this.inversion.y = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.up) >= 0f) ? -1 : 1);
			this.inversion.z = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.forward) >= 0f) ? -1 : 1);
			this.handleVector_X.center = new Vector3(this.inversion.x * 0.5f, 0f, 0f);
			this.handleVector_Y.center = new Vector3(0f, this.inversion.y * 0.5f, 0f);
			this.handleVector_Z.center = new Vector3(0f, 0f, this.inversion.z * 0.5f);
			this.handleUniform.center = this.inversion * 0.125f;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0005863A File Offset: 0x00056A3A
		protected void Update()
		{
			this.updateScale();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00058644 File Offset: 0x00056A44
		protected void OnEnable()
		{
			GLRenderer.render += this.handleGLRender;
			base.gameObject.layer = LayerMasks.LOGIC;
			this.handleVector_X = base.gameObject.AddComponent<BoxCollider>();
			this.handleVector_X.size = new Vector3(1f, 0.1f, 0.1f);
			this.handleVector_Y = base.gameObject.AddComponent<BoxCollider>();
			this.handleVector_Y.size = new Vector3(0.1f, 1f, 0.1f);
			this.handleVector_Z = base.gameObject.AddComponent<BoxCollider>();
			this.handleVector_Z.size = new Vector3(0.1f, 0.1f, 1f);
			this.handleUniform = base.gameObject.AddComponent<BoxCollider>();
			this.handleUniform.size = new Vector3(0.25f, 0.25f, 0.25f);
			this.updateScale();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00058738 File Offset: 0x00056B38
		protected void OnDisable()
		{
			GLRenderer.render -= this.handleGLRender;
			UnityEngine.Object.Destroy(this.handleVector_X);
			UnityEngine.Object.Destroy(this.handleVector_Y);
			UnityEngine.Object.Destroy(this.handleVector_Z);
			UnityEngine.Object.Destroy(this.handleUniform);
		}

		// Token: 0x040007FB RID: 2043
		protected static float _handleSensitivity = 1f;

		// Token: 0x040007FC RID: 2044
		protected static float _handleScreensize = 0.5f;

		// Token: 0x040007FE RID: 2046
		protected DevkitScaleHandle.EDevkitScaleHandleSelection drag;

		// Token: 0x040007FF RID: 2047
		protected DevkitScaleHandle.EDevkitScaleHandleSelection hover;

		// Token: 0x04000800 RID: 2048
		protected BoxCollider handleVector_X;

		// Token: 0x04000801 RID: 2049
		protected BoxCollider handleVector_Y;

		// Token: 0x04000802 RID: 2050
		protected BoxCollider handleVector_Z;

		// Token: 0x04000803 RID: 2051
		protected BoxCollider handleUniform;

		// Token: 0x04000804 RID: 2052
		protected Vector3 inversion;

		// Token: 0x04000805 RID: 2053
		protected Vector3 mouseOrigin;

		// Token: 0x04000806 RID: 2054
		protected Vector3 prevScaleResult;

		// Token: 0x02000171 RID: 369
		// (Invoke) Token: 0x06000B27 RID: 2855
		public delegate void DevkitScaleTransformedHandler(DevkitScaleHandle handle, Vector3 delta);

		// Token: 0x02000172 RID: 370
		protected enum EDevkitScaleHandleSelection
		{
			// Token: 0x04000808 RID: 2056
			NONE,
			// Token: 0x04000809 RID: 2057
			X,
			// Token: 0x0400080A RID: 2058
			Y,
			// Token: 0x0400080B RID: 2059
			Z,
			// Token: 0x0400080C RID: 2060
			UNIFORM
		}
	}
}
