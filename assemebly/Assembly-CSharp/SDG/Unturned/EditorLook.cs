using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000494 RID: 1172
	public class EditorLook : MonoBehaviour
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x000A8065 File Offset: 0x000A6465
		public static float pitch
		{
			get
			{
				return EditorLook._pitch;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000A806C File Offset: 0x000A646C
		public static float yaw
		{
			get
			{
				return EditorLook._yaw;
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x000A8074 File Offset: 0x000A6474
		private void Update()
		{
			if (EditorInteract.isFlying)
			{
				MainCamera.instance.fieldOfView = Mathf.Lerp(MainCamera.instance.fieldOfView, OptionsSettings.view + (float)((!EditorMovement.isMoving || !Input.GetKey(ControlsSettings.modify)) ? 0 : 10), 8f * Time.deltaTime);
				this.highlightCamera.fieldOfView = MainCamera.instance.fieldOfView;
				EditorLook._yaw += ControlsSettings.look * Input.GetAxis("mouse_x");
				if (ControlsSettings.invert)
				{
					EditorLook._pitch += ControlsSettings.look * Input.GetAxis("mouse_y");
				}
				else
				{
					EditorLook._pitch -= ControlsSettings.look * Input.GetAxis("mouse_y");
				}
				if (EditorLook.pitch > 90f)
				{
					EditorLook._pitch = 90f;
				}
				else if (EditorLook.pitch < -90f)
				{
					EditorLook._pitch = -90f;
				}
				MainCamera.instance.transform.localRotation = Quaternion.Euler(EditorLook.pitch, 0f, 0f);
				base.transform.rotation = Quaternion.Euler(0f, EditorLook.yaw, 0f);
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000A81C8 File Offset: 0x000A65C8
		private void Start()
		{
			MainCamera.instance.fieldOfView = OptionsSettings.view;
			this.highlightCamera = MainCamera.instance.transform.FindChild("HighlightCamera").GetComponent<Camera>();
			this.highlightCamera.fieldOfView = OptionsSettings.view;
			EditorLook._pitch = MainCamera.instance.transform.localRotation.eulerAngles.x;
			if (EditorLook.pitch > 90f)
			{
				EditorLook._pitch = -360f + EditorLook.pitch;
			}
			EditorLook._yaw = base.transform.rotation.eulerAngles.y;
		}

		// Token: 0x04001276 RID: 4726
		private static float _pitch;

		// Token: 0x04001277 RID: 4727
		private static float _yaw;

		// Token: 0x04001278 RID: 4728
		private Camera highlightCamera;
	}
}
