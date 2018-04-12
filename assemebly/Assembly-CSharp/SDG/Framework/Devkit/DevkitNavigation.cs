using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Unturned;
using UnityEngine;

namespace SDG.Framework.Devkit
{
	// Token: 0x02000130 RID: 304
	public class DevkitNavigation : MonoBehaviour
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x0004EC4B File Offset: 0x0004D04B
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x0004EC52 File Offset: 0x0004D052
		[TerminalCommandProperty("input.devkit.speed_sensitivity", "multiplier for scroll wheel speed", 2)]
		public static float speedSensitivity
		{
			get
			{
				return DevkitNavigation._speedSensitivity;
			}
			set
			{
				DevkitNavigation._speedSensitivity = value;
				TerminalUtility.printCommandPass("Set speed sensitivity to: " + DevkitNavigation.speedSensitivity);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0004EC73 File Offset: 0x0004D073
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0004EC7A File Offset: 0x0004D07A
		[TerminalCommandProperty("input.devkit.pan_sensitivity", "multiplier for panning speed", 0.1f)]
		public static float panSensitivity
		{
			get
			{
				return DevkitNavigation._panSensitivity;
			}
			set
			{
				DevkitNavigation._panSensitivity = value;
				TerminalUtility.printCommandPass("Set pan sensitivity to: " + DevkitNavigation.panSensitivity);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0004EC9B File Offset: 0x0004D09B
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0004ECA2 File Offset: 0x0004D0A2
		[TerminalCommandProperty("input.devkit.look_sensitivity", "multiplier for look speed", 1.75f)]
		public static float lookSensitivity
		{
			get
			{
				return DevkitNavigation._lookSensitivity;
			}
			set
			{
				DevkitNavigation._lookSensitivity = value;
				TerminalUtility.printCommandPass("Set look sensitivity to: " + DevkitNavigation.lookSensitivity);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0004ECC3 File Offset: 0x0004D0C3
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0004ECCA File Offset: 0x0004D0CA
		[TerminalCommandProperty("input.devkit.invert_look", "if true multiply vertical input by -1", false)]
		public static bool invertLook
		{
			get
			{
				return DevkitNavigation._invertLook;
			}
			set
			{
				DevkitNavigation._invertLook = value;
				TerminalUtility.printCommandPass("Set invert look to: " + DevkitNavigation.invertLook);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0004ECEB File Offset: 0x0004D0EB
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x0004ECF4 File Offset: 0x0004D0F4
		public static bool isNavigating
		{
			get
			{
				return DevkitNavigation._isNavigating;
			}
			protected set
			{
				if (DevkitNavigation.isNavigating == value)
				{
					return;
				}
				bool isNavigating = DevkitNavigation.isNavigating;
				DevkitNavigation._isNavigating = value;
				DevkitNavigation.triggerIsNavigatingChanged(isNavigating, DevkitNavigation.isNavigating);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0004ED24 File Offset: 0x0004D124
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x0004ED2B File Offset: 0x0004D12B
		[TerminalCommandProperty("input.devkit.focus_distance", "multiplier for focus distance", 1)]
		public static float focusDistance
		{
			get
			{
				return DevkitNavigation._focusDistance;
			}
			set
			{
				DevkitNavigation._focusDistance = value;
				TerminalUtility.printCommandPass("Set focus_distance to: " + DevkitNavigation.focusDistance);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000952 RID: 2386 RVA: 0x0004ED4C File Offset: 0x0004D14C
		// (remove) Token: 0x06000953 RID: 2387 RVA: 0x0004ED80 File Offset: 0x0004D180
		public static event DevkitIsNavigatingChangedHandler isNavigatingChanged;

		// Token: 0x06000954 RID: 2388 RVA: 0x0004EDB4 File Offset: 0x0004D1B4
		public static void focus(Bounds bounds)
		{
			float d = Mathf.Max(Mathf.Max(bounds.extents.x, bounds.extents.y), bounds.extents.z);
			DevkitNavigation.instance.transform.position = bounds.center - MainCamera.instance.transform.forward * d * DevkitNavigation.focusDistance;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0004EE33 File Offset: 0x0004D233
		protected static void triggerIsNavigatingChanged(bool oldIsNavigating, bool newIsNavigating)
		{
			if (DevkitNavigation.isNavigatingChanged != null)
			{
				DevkitNavigation.isNavigatingChanged(oldIsNavigating, newIsNavigating);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0004EE4B File Offset: 0x0004D24B
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0004EE53 File Offset: 0x0004D253
		public float pitch { get; protected set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0004EE5C File Offset: 0x0004D25C
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0004EE64 File Offset: 0x0004D264
		public float yaw { get; protected set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0004EE6D File Offset: 0x0004D26D
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0004EE75 File Offset: 0x0004D275
		public float speed { get; protected set; }

		// Token: 0x0600095C RID: 2396 RVA: 0x0004EE80 File Offset: 0x0004D280
		protected void load()
		{
			this.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Editor_" + Level.info.name + ".dat";
			if (!File.Exists(path))
			{
				return;
			}
			using (StreamReader streamReader = new StreamReader(path))
			{
				IFormattedFileReader formattedFileReader = new KeyValueTableReader(streamReader);
				base.transform.position = formattedFileReader.readValue<Vector3>("Position");
				this.pitch = formattedFileReader.readValue<float>("Pitch");
				this.yaw = formattedFileReader.readValue<float>("Yaw");
				this.speed = formattedFileReader.readValue<float>("Speed");
				this.applyRotation();
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0004EF40 File Offset: 0x0004D340
		protected void save()
		{
			if (!this.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Editor_" + Level.info.name + ".dat";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<Vector3>("Position", base.transform.position);
				formattedFileWriter.writeValue<float>("Pitch", this.pitch);
				formattedFileWriter.writeValue<float>("Yaw", this.yaw);
				formattedFileWriter.writeValue<float>("Speed", this.speed);
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0004F00C File Offset: 0x0004D40C
		protected void applyRotation()
		{
			base.transform.rotation = Quaternion.Euler(0f, this.yaw, 0f);
			base.transform.rotation *= Quaternion.Euler(this.pitch, 0f, 0f);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0004F064 File Offset: 0x0004D464
		protected void Update()
		{
			if (Input.GetKey(KeyCode.Mouse1) && DevkitInput.canEditorReceiveInput)
			{
				if (!DevkitNavigation.isNavigating)
				{
					Cursor.lockState = CursorLockMode.Locked;
					DevkitNavigation.isNavigating = true;
				}
				this.speed = Mathf.Clamp(this.speed + Input.GetAxis("mouse_z") * this.speed * DevkitNavigation.speedSensitivity, 0.5f, 2048f);
				if (Input.GetKey(KeyCode.Mouse0))
				{
					base.transform.position += base.transform.right * Input.GetAxis("mouse_x") * this.speed * DevkitNavigation.panSensitivity;
					base.transform.position += base.transform.up * Input.GetAxis("mouse_y") * this.speed * DevkitNavigation.panSensitivity;
				}
				else
				{
					this.pitch = Mathf.Clamp(this.pitch + Input.GetAxis("mouse_y") * DevkitNavigation.lookSensitivity * (float)((!DevkitNavigation.invertLook) ? -1 : 1), -90f, 90f);
					this.yaw += Input.GetAxis("mouse_x") * DevkitNavigation.lookSensitivity;
					this.yaw %= 360f;
					this.applyRotation();
					float d = 0f;
					if (Input.GetKey(KeyCode.A))
					{
						d = -1f;
					}
					else if (Input.GetKey(KeyCode.D))
					{
						d = 1f;
					}
					float d2 = 0f;
					if (Input.GetKey(KeyCode.S))
					{
						d2 = -1f;
					}
					else if (Input.GetKey(KeyCode.W))
					{
						d2 = 1f;
					}
					base.transform.position += base.transform.right * d * Time.deltaTime * this.speed;
					base.transform.position += base.transform.forward * d2 * Time.deltaTime * this.speed;
				}
				return;
			}
			if (DevkitNavigation.isNavigating)
			{
				Cursor.lockState = CursorLockMode.None;
				DevkitNavigation.isNavigating = false;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0004F2D7 File Offset: 0x0004D6D7
		private void onLevelExited()
		{
			this.save();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0004F2DF File Offset: 0x0004D6DF
		private void OnDisable()
		{
			Level.onLevelExited = (LevelExited)Delegate.Remove(Level.onLevelExited, new LevelExited(this.onLevelExited));
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0004F301 File Offset: 0x0004D701
		private void OnEnable()
		{
			Level.onLevelExited = (LevelExited)Delegate.Combine(Level.onLevelExited, new LevelExited(this.onLevelExited));
			this.load();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0004F329 File Offset: 0x0004D729
		private void Awake()
		{
			this.pitch = 0f;
			this.yaw = 0f;
			this.speed = 4f;
			DevkitNavigation.instance = this;
		}

		// Token: 0x04000714 RID: 1812
		protected static float _speedSensitivity = 2f;

		// Token: 0x04000715 RID: 1813
		protected static float _panSensitivity = 0.1f;

		// Token: 0x04000716 RID: 1814
		protected static float _lookSensitivity = 1.75f;

		// Token: 0x04000717 RID: 1815
		protected static bool _invertLook;

		// Token: 0x04000718 RID: 1816
		protected static bool _isNavigating;

		// Token: 0x04000719 RID: 1817
		protected static float _focusDistance = 1.5f;

		// Token: 0x0400071A RID: 1818
		protected static DevkitNavigation instance;

		// Token: 0x0400071F RID: 1823
		protected bool wasLoaded;
	}
}
