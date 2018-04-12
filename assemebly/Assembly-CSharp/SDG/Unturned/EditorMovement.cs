using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000495 RID: 1173
	public class EditorMovement : MonoBehaviour
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x000A827E File Offset: 0x000A667E
		public static bool isMoving
		{
			get
			{
				return EditorMovement._isMoving;
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000A8288 File Offset: 0x000A6688
		private void Update()
		{
			if (EditorInteract.isFlying)
			{
				if (Input.GetKey(ControlsSettings.left))
				{
					this.input.x = -1f;
				}
				else if (Input.GetKey(ControlsSettings.right))
				{
					this.input.x = 1f;
				}
				else
				{
					this.input.x = 0f;
				}
				if (Input.GetKey(ControlsSettings.up))
				{
					this.input.z = 1f;
				}
				else if (Input.GetKey(ControlsSettings.down))
				{
					this.input.z = -1f;
				}
				else
				{
					this.input.z = 0f;
				}
				EditorMovement._isMoving = (this.input.x != 0f || this.input.z != 0f);
				float d = 32f;
				if (Input.GetKey(ControlsSettings.modify))
				{
					d = 128f;
				}
				else if (Input.GetKey(ControlsSettings.other))
				{
					d = 8f;
				}
				float d2 = 0f;
				if (Input.GetKey(ControlsSettings.ascend))
				{
					d2 = 1f;
				}
				else if (Input.GetKey(ControlsSettings.descend))
				{
					d2 = -1f;
				}
				this.controller.Move(MainCamera.instance.transform.rotation * this.input * d * Time.deltaTime + Vector3.up * d2 * Time.deltaTime * d + d * MainCamera.instance.transform.forward * Input.GetAxis("mouse_z"));
				Vector3 position = base.transform.position;
				position.x = Mathf.Clamp(position.x, (float)(-(float)Level.size), (float)Level.size);
				position.y = Mathf.Clamp(position.y, 0f, Level.HEIGHT);
				position.z = Mathf.Clamp(position.z, (float)(-(float)Level.size), (float)Level.size);
				base.transform.position = position;
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000A84DF File Offset: 0x000A68DF
		private void Start()
		{
			this.controller = base.transform.GetComponent<CharacterController>();
		}

		// Token: 0x04001279 RID: 4729
		private static bool _isMoving;

		// Token: 0x0400127A RID: 4730
		private Vector3 input;

		// Token: 0x0400127B RID: 4731
		private CharacterController controller;
	}
}
