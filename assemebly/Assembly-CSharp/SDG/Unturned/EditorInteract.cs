using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000493 RID: 1171
	public class EditorInteract : MonoBehaviour
	{
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000A7C39 File Offset: 0x000A6039
		public static bool isFlying
		{
			get
			{
				return EditorInteract._isFlying;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x000A7C40 File Offset: 0x000A6040
		public static Ray ray
		{
			get
			{
				return EditorInteract._ray;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000A7C47 File Offset: 0x000A6047
		public static RaycastHit groundHit
		{
			get
			{
				return EditorInteract._groundHit;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x000A7C4E File Offset: 0x000A604E
		public static RaycastHit worldHit
		{
			get
			{
				return EditorInteract._worldHit;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000A7C55 File Offset: 0x000A6055
		public static RaycastHit objectHit
		{
			get
			{
				return EditorInteract._objectHit;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000A7C5C File Offset: 0x000A605C
		public static RaycastHit logicHit
		{
			get
			{
				return EditorInteract._logicHit;
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000A7C64 File Offset: 0x000A6064
		private void Update()
		{
			EditorInteract._isFlying = Input.GetKey(ControlsSettings.secondary);
			EditorInteract._ray = MainCamera.instance.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(EditorInteract.ray, out EditorInteract._groundHit, 2048f, ((!EditorTerrainHeight.isTerraforming || !EditorTerrainHeight.map2) && (!EditorTerrainMaterials.isPainting || !EditorTerrainMaterials.map2 || EditorRoads.isPaving)) ? RayMasks.GROUND : RayMasks.GROUND2);
			Physics.Raycast(EditorInteract.ray, out EditorInteract._worldHit, 2048f, RayMasks.EDITOR_WORLD);
			Physics.Raycast(EditorInteract.ray, out EditorInteract._objectHit, 2048f, RayMasks.EDITOR_INTERACT);
			Physics.Raycast(EditorInteract.ray, out EditorInteract._logicHit, 2048f, RayMasks.EDITOR_LOGIC);
			if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftControl))
			{
				Level.save();
			}
			if (Input.GetKeyDown(KeyCode.F1))
			{
				LevelVisibility.roadsVisible = !LevelVisibility.roadsVisible;
				EditorLevelVisibilityUI.roadsToggle.state = LevelVisibility.roadsVisible;
			}
			if (Input.GetKeyDown(KeyCode.F2))
			{
				LevelVisibility.navigationVisible = !LevelVisibility.navigationVisible;
				EditorLevelVisibilityUI.navigationToggle.state = LevelVisibility.navigationVisible;
			}
			if (Input.GetKeyDown(KeyCode.F3))
			{
				LevelVisibility.nodesVisible = !LevelVisibility.nodesVisible;
				EditorLevelVisibilityUI.nodesToggle.state = LevelVisibility.nodesVisible;
			}
			if (Input.GetKeyDown(KeyCode.F4))
			{
				LevelVisibility.itemsVisible = !LevelVisibility.itemsVisible;
				EditorLevelVisibilityUI.itemsToggle.state = LevelVisibility.itemsVisible;
			}
			if (Input.GetKeyDown(KeyCode.F5))
			{
				LevelVisibility.playersVisible = !LevelVisibility.playersVisible;
				EditorLevelVisibilityUI.playersToggle.state = LevelVisibility.playersVisible;
			}
			if (Input.GetKeyDown(KeyCode.F6))
			{
				LevelVisibility.zombiesVisible = !LevelVisibility.zombiesVisible;
				EditorLevelVisibilityUI.zombiesToggle.state = LevelVisibility.zombiesVisible;
			}
			if (Input.GetKeyDown(KeyCode.F7))
			{
				LevelVisibility.vehiclesVisible = !LevelVisibility.vehiclesVisible;
				EditorLevelVisibilityUI.vehiclesToggle.state = LevelVisibility.vehiclesVisible;
			}
			if (Input.GetKeyDown(KeyCode.F8))
			{
				LevelVisibility.borderVisible = !LevelVisibility.borderVisible;
				EditorLevelVisibilityUI.borderToggle.state = LevelVisibility.borderVisible;
			}
			if (Input.GetKeyDown(KeyCode.F9))
			{
				LevelVisibility.animalsVisible = !LevelVisibility.animalsVisible;
				EditorLevelVisibilityUI.animalsToggle.state = LevelVisibility.animalsVisible;
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000A7EDB File Offset: 0x000A62DB
		private void Start()
		{
			EditorInteract.load();
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000A7EE4 File Offset: 0x000A62E4
		public static void load()
		{
			if (ReadWrite.fileExists(Level.info.path + "/Editor/Camera.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Editor/Camera.dat", false, false, 1);
				MainCamera.instance.transform.parent.position = block.readSingleVector3();
				MainCamera.instance.transform.localRotation = Quaternion.Euler(block.readSingle(), 0f, 0f);
				MainCamera.instance.transform.parent.rotation = Quaternion.Euler(0f, block.readSingle(), 0f);
			}
			else
			{
				MainCamera.instance.transform.parent.position = new Vector3(0f, Level.TERRAIN, 0f);
				MainCamera.instance.transform.parent.rotation = Quaternion.identity;
				MainCamera.instance.transform.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000A7FF0 File Offset: 0x000A63F0
		public static void save()
		{
			Block block = new Block();
			block.writeByte(EditorInteract.SAVEDATA_VERSION);
			block.writeSingleVector3(MainCamera.instance.transform.position);
			block.writeSingle(EditorLook.pitch);
			block.writeSingle(EditorLook.yaw);
			ReadWrite.writeBlock(Level.info.path + "/Editor/Camera.dat", false, false, block);
		}

		// Token: 0x0400126F RID: 4719
		public static readonly byte SAVEDATA_VERSION = 1;

		// Token: 0x04001270 RID: 4720
		private static bool _isFlying;

		// Token: 0x04001271 RID: 4721
		private static Ray _ray;

		// Token: 0x04001272 RID: 4722
		private static RaycastHit _groundHit;

		// Token: 0x04001273 RID: 4723
		private static RaycastHit _worldHit;

		// Token: 0x04001274 RID: 4724
		private static RaycastHit _objectHit;

		// Token: 0x04001275 RID: 4725
		private static RaycastHit _logicHit;
	}
}
