using System;
using SDG.Framework.Debug;
using SDG.Framework.Devkit.Interactable;
using SDG.Framework.Rendering;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x0200016A RID: 362
	public class DevkitPositionHandle : MonoBehaviour, IDevkitHandle, IDevkitInteractableBeginHoverHandler, IDevkitInteractableBeginDragHandler, IDevkitInteractableContinueDragHandler, IDevkitInteractableEndDragHandler, IDevkitInteractableEndHoverHandler
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000564D2 File Offset: 0x000548D2
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000564D9 File Offset: 0x000548D9
		[TerminalCommandProperty("input.devkit.pivot.position.delta_sensitivity", "multiplier for position delta", 1)]
		public static float handleSensitivity
		{
			get
			{
				return DevkitPositionHandle._handleSensitivity;
			}
			set
			{
				DevkitPositionHandle._handleSensitivity = value;
				TerminalUtility.printCommandPass("Set delta_sensitivity to: " + DevkitPositionHandle.handleSensitivity);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000564FA File Offset: 0x000548FA
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00056501 File Offset: 0x00054901
		[TerminalCommandProperty("input.devkit.pivot.position.screensize", "percentage of screen size", 0.5f)]
		public static float handleScreensize
		{
			get
			{
				return DevkitPositionHandle._handleScreensize;
			}
			set
			{
				DevkitPositionHandle._handleScreensize = value;
				TerminalUtility.printCommandPass("Set screensize to: " + DevkitPositionHandle.handleScreensize);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000AE0 RID: 2784 RVA: 0x00056524 File Offset: 0x00054924
		// (remove) Token: 0x06000AE1 RID: 2785 RVA: 0x0005655C File Offset: 0x0005495C
		public event DevkitPositionHandle.DevkitPositionTransformedHandler transformed;

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00056592 File Offset: 0x00054992
		protected bool isSnapping
		{
			get
			{
				return Input.GetKey(ControlsSettings.other);
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0005659E File Offset: 0x0005499E
		public void suggestTransform(Vector3 position, Quaternion rotation)
		{
			base.transform.position = position;
			base.transform.rotation = rotation;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000565B8 File Offset: 0x000549B8
		public void beginHover(InteractionData data)
		{
			if (data.collider == this.handleAxis_X)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X;
			}
			else if (data.collider == this.handleAxis_Y)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y;
			}
			else if (data.collider == this.handleAxis_Z)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z;
			}
			else if (data.collider == this.handlePlane_X)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X;
			}
			else if (data.collider == this.handlePlane_Y)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y;
			}
			else if (data.collider == this.handlePlane_Z)
			{
				this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z;
			}
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0005668C File Offset: 0x00054A8C
		public void beginDrag(InteractionData data)
		{
			if (data.collider == this.handleAxis_X)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X;
			}
			else if (data.collider == this.handleAxis_Y)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y;
			}
			else if (data.collider == this.handleAxis_Z)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z;
			}
			else if (data.collider == this.handlePlane_X)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X;
			}
			else if (data.collider == this.handlePlane_Y)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y;
			}
			else if (data.collider == this.handlePlane_Z)
			{
				this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z;
			}
			this.transformOrigin = base.transform.position;
			this.prevPositionResult = this.transformOrigin;
			this.handleOffset = data.point - base.transform.position;
			this.mouseOrigin = Input.mousePosition;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000567A4 File Offset: 0x00054BA4
		public void continueDrag(InteractionData data)
		{
			Vector3 vector;
			Vector3 onNormal;
			Vector3 onNormal2;
			if (this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X)
			{
				vector = base.transform.right;
				onNormal = base.transform.up;
				onNormal2 = base.transform.forward;
			}
			else if (this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y)
			{
				vector = base.transform.up;
				onNormal = base.transform.right;
				onNormal2 = base.transform.forward;
			}
			else
			{
				if (this.drag != DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z && this.drag != DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z)
				{
					return;
				}
				vector = base.transform.forward;
				onNormal = base.transform.right;
				onNormal2 = base.transform.up;
			}
			if (this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z)
			{
				Vector3 b = MainCamera.instance.WorldToScreenPoint(this.transformOrigin);
				Vector3 vector2 = MainCamera.instance.WorldToScreenPoint(this.transformOrigin + vector) - b;
				Vector3 lhs = Input.mousePosition - this.mouseOrigin;
				float num = Vector3.Dot(lhs, vector2.normalized) / vector2.magnitude;
				num *= DevkitPositionHandle.handleSensitivity;
				if (Input.GetKey(ControlsSettings.snap))
				{
					num = (float)Mathf.RoundToInt(num / DevkitSelectionToolOptions.instance.snapPosition) * DevkitSelectionToolOptions.instance.snapPosition;
				}
				Vector3 a = this.transformOrigin + num * vector;
				Vector3 delta = a - this.prevPositionResult;
				this.triggerTransformed(delta);
				this.prevPositionResult = a;
			}
			else if (this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y || this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z)
			{
				Plane plane = default(Plane);
				plane.SetNormalAndPosition(vector, this.transformOrigin);
				Ray pointerToWorldRay = DevkitInput.pointerToWorldRay;
				float d;
				plane.Raycast(pointerToWorldRay, out d);
				Vector3 a2 = pointerToWorldRay.origin + pointerToWorldRay.direction * d - this.handleOffset;
				Vector3 vector3 = a2 - this.transformOrigin;
				Vector3 vector4 = Vector3.Project(vector3, onNormal);
				Vector3 vector5 = Vector3.Project(vector3, onNormal2);
				float num2 = vector4.magnitude;
				num2 *= DevkitPositionHandle.handleSensitivity;
				if (Input.GetKey(ControlsSettings.snap))
				{
					num2 = (float)Mathf.RoundToInt(num2 / DevkitSelectionToolOptions.instance.snapPosition) * DevkitSelectionToolOptions.instance.snapPosition;
				}
				float num3 = vector5.magnitude;
				num3 *= DevkitPositionHandle.handleSensitivity;
				if (Input.GetKey(ControlsSettings.snap))
				{
					num3 = (float)Mathf.RoundToInt(num3 / DevkitSelectionToolOptions.instance.snapPosition) * DevkitSelectionToolOptions.instance.snapPosition;
				}
				a2 = this.transformOrigin + vector4.normalized * num2 + vector5.normalized * num3;
				Vector3 delta2 = a2 - this.prevPositionResult;
				this.triggerTransformed(delta2);
				this.prevPositionResult = a2;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00056ACC File Offset: 0x00054ECC
		public void endDrag(InteractionData data)
		{
			this.drag = DevkitPositionHandle.EDevkitPositionHandleSelection.NONE;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00056AD5 File Offset: 0x00054ED5
		public void endHover(InteractionData data)
		{
			this.hover = DevkitPositionHandle.EDevkitPositionHandleSelection.NONE;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00056ADE File Offset: 0x00054EDE
		protected void triggerTransformed(Vector3 delta)
		{
			if (this.transformed != null)
			{
				this.transformed(this, delta);
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00056AF8 File Offset: 0x00054EF8
		protected void arrow(Vector3 normal, Vector3 view, bool isDragging, bool isHovering, Color color)
		{
			Vector3 b = Vector3.Cross(view, normal) * 0.1f;
			if (isDragging && this.isSnapping)
			{
				GL.Color(new Color(0f, 0f, 0f, 0.5f));
				float num = DevkitSelectionToolOptions.instance.snapPosition / base.transform.localScale.x;
				int num2 = Mathf.Max(2, Mathf.CeilToInt(2f / num));
				for (int i = -num2; i <= num2; i++)
				{
					Vector3 a = normal * num * (float)i;
					GLUtility.line(a - b, a + b);
				}
			}
			GL.Color((!isDragging) ? ((!isHovering) ? color : Color.yellow) : Color.white);
			GLUtility.line(Vector3.zero, normal);
			Vector3 a2 = normal * 0.75f;
			GLUtility.line(normal, a2 - b);
			GLUtility.line(normal, a2 + b);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00056C14 File Offset: 0x00055014
		protected void plane(Vector3 horizontal, Vector3 vertical, bool isDragging, bool isHovering, Color color)
		{
			if (isDragging && this.isSnapping)
			{
				GL.Color(new Color(0f, 0f, 0f, 0.5f));
				float num = DevkitSelectionToolOptions.instance.snapPosition / base.transform.localScale.x;
				int num2 = Mathf.Max(2, Mathf.CeilToInt(2f / num));
				Vector3 b = horizontal * num * (float)num2;
				Vector3 b2 = vertical * num * (float)num2;
				for (int i = -num2; i <= num2; i++)
				{
					Vector3 a = horizontal * num * (float)i;
					GLUtility.line(a - b2, a + b2);
				}
				for (int j = -num2; j <= num2; j++)
				{
					Vector3 a2 = vertical * num * (float)j;
					GLUtility.line(a2 - b, a2 + b);
				}
			}
			GL.Color((!isDragging) ? ((!isHovering) ? color : Color.yellow) : Color.white);
			Vector3 vector = horizontal * 0.25f;
			Vector3 vector2 = vertical * 0.25f;
			GLUtility.line(vector, vector + vector2);
			GLUtility.line(vector2, vector2 + vector);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00056D80 File Offset: 0x00055180
		protected void handleGLRender()
		{
			GLUtility.LINE_FLAT_COLOR.SetPass(0);
			GL.Begin(1);
			GLUtility.matrix = base.transform.localToWorldMatrix;
			this.plane(new Vector3(0f, this.inversion.y, 0f), new Vector3(0f, 0f, this.inversion.z), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_X, Color.red);
			this.plane(new Vector3(this.inversion.x, 0f, 0f), new Vector3(0f, 0f, this.inversion.z), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Y, Color.green);
			this.plane(new Vector3(this.inversion.x, 0f, 0f), new Vector3(0f, this.inversion.y, 0f), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.PLANE_Z, Color.blue);
			GLUtility.matrix = Matrix4x4.TRS(base.transform.position, Quaternion.identity, base.transform.localScale);
			this.arrow(base.transform.right * this.inversion.x, GLUtility.getDirectionFromViewToArrow(MainCamera.instance.transform.position, base.transform.position, base.transform.right * this.inversion.x), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_X, Color.red);
			this.arrow(base.transform.up * this.inversion.y, GLUtility.getDirectionFromViewToArrow(MainCamera.instance.transform.position, base.transform.position, base.transform.up * this.inversion.y), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Y, Color.green);
			this.arrow(base.transform.forward * this.inversion.z, GLUtility.getDirectionFromViewToArrow(MainCamera.instance.transform.position, base.transform.position, base.transform.forward * this.inversion.z), this.drag == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z, this.hover == DevkitPositionHandle.EDevkitPositionHandleSelection.AXIS_Z, Color.blue);
			GL.End();
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00057024 File Offset: 0x00055424
		protected void updateScale()
		{
			Vector3 position = MainCamera.instance.transform.position;
			Vector3 a = base.transform.position - position;
			float magnitude = a.magnitude;
			Vector3 lhs = a / magnitude;
			float num = magnitude;
			num *= DevkitPositionHandle.handleScreensize;
			base.transform.localScale = new Vector3(num, num, num);
			this.inversion.x = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.right) >= 0f) ? -1 : 1);
			this.inversion.y = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.up) >= 0f) ? -1 : 1);
			this.inversion.z = (float)((!DevkitSelectionToolOptions.instance.lockHandles && Vector3.Dot(lhs, base.transform.forward) >= 0f) ? -1 : 1);
			this.handleAxis_X.center = new Vector3(this.inversion.x * 0.5f, 0f, 0f);
			this.handleAxis_Y.center = new Vector3(0f, this.inversion.y * 0.5f, 0f);
			this.handleAxis_Z.center = new Vector3(0f, 0f, this.inversion.z * 0.5f);
			this.handlePlane_X.center = new Vector3(0f, this.inversion.y * 0.125f, this.inversion.z * 0.125f);
			this.handlePlane_Y.center = new Vector3(this.inversion.x * 0.125f, 0f, this.inversion.z * 0.125f);
			this.handlePlane_Z.center = new Vector3(this.inversion.x * 0.125f, this.inversion.y * 0.125f, 0f);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00057263 File Offset: 0x00055663
		protected void Update()
		{
			if (MainCamera.instance == null)
			{
				return;
			}
			this.updateScale();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0005727C File Offset: 0x0005567C
		protected void OnEnable()
		{
			GLRenderer.render += this.handleGLRender;
			base.gameObject.layer = LayerMasks.LOGIC;
			this.handleAxis_X = base.gameObject.AddComponent<BoxCollider>();
			this.handleAxis_X.size = new Vector3(1f, 0.1f, 0.1f);
			this.handleAxis_Y = base.gameObject.AddComponent<BoxCollider>();
			this.handleAxis_Y.size = new Vector3(0.1f, 1f, 0.1f);
			this.handleAxis_Z = base.gameObject.AddComponent<BoxCollider>();
			this.handleAxis_Z.size = new Vector3(0.1f, 0.1f, 1f);
			this.handlePlane_X = base.gameObject.AddComponent<BoxCollider>();
			this.handlePlane_X.size = new Vector3(0f, 0.25f, 0.25f);
			this.handlePlane_Y = base.gameObject.AddComponent<BoxCollider>();
			this.handlePlane_Y.size = new Vector3(0.25f, 0f, 0.25f);
			this.handlePlane_Z = base.gameObject.AddComponent<BoxCollider>();
			this.handlePlane_Z.size = new Vector3(0.25f, 0.25f, 0f);
			this.updateScale();
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000573D0 File Offset: 0x000557D0
		protected void OnDisable()
		{
			GLRenderer.render -= this.handleGLRender;
			UnityEngine.Object.Destroy(this.handleAxis_X);
			UnityEngine.Object.Destroy(this.handleAxis_Y);
			UnityEngine.Object.Destroy(this.handleAxis_Z);
			UnityEngine.Object.Destroy(this.handlePlane_X);
			UnityEngine.Object.Destroy(this.handlePlane_Y);
			UnityEngine.Object.Destroy(this.handlePlane_Z);
		}

		// Token: 0x040007D0 RID: 2000
		protected static float _handleSensitivity = 1f;

		// Token: 0x040007D1 RID: 2001
		protected static float _handleScreensize = 0.5f;

		// Token: 0x040007D3 RID: 2003
		protected DevkitPositionHandle.EDevkitPositionHandleSelection drag;

		// Token: 0x040007D4 RID: 2004
		protected DevkitPositionHandle.EDevkitPositionHandleSelection hover;

		// Token: 0x040007D5 RID: 2005
		protected BoxCollider handleAxis_X;

		// Token: 0x040007D6 RID: 2006
		protected BoxCollider handleAxis_Y;

		// Token: 0x040007D7 RID: 2007
		protected BoxCollider handleAxis_Z;

		// Token: 0x040007D8 RID: 2008
		protected BoxCollider handlePlane_X;

		// Token: 0x040007D9 RID: 2009
		protected BoxCollider handlePlane_Y;

		// Token: 0x040007DA RID: 2010
		protected BoxCollider handlePlane_Z;

		// Token: 0x040007DB RID: 2011
		protected Vector3 inversion;

		// Token: 0x040007DC RID: 2012
		protected Vector3 transformOrigin;

		// Token: 0x040007DD RID: 2013
		protected Vector3 mouseOrigin;

		// Token: 0x040007DE RID: 2014
		protected Vector3 handleOffset;

		// Token: 0x040007DF RID: 2015
		protected Vector3 prevPositionResult;

		// Token: 0x0200016B RID: 363
		// (Invoke) Token: 0x06000AF3 RID: 2803
		public delegate void DevkitPositionTransformedHandler(DevkitPositionHandle handle, Vector3 delta);

		// Token: 0x0200016C RID: 364
		protected enum EDevkitPositionHandleSelection
		{
			// Token: 0x040007E1 RID: 2017
			NONE,
			// Token: 0x040007E2 RID: 2018
			AXIS_X,
			// Token: 0x040007E3 RID: 2019
			AXIS_Y,
			// Token: 0x040007E4 RID: 2020
			AXIS_Z,
			// Token: 0x040007E5 RID: 2021
			PLANE_X,
			// Token: 0x040007E6 RID: 2022
			PLANE_Y,
			// Token: 0x040007E7 RID: 2023
			PLANE_Z
		}
	}
}
