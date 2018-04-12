using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200049F RID: 1183
	public class EditorTerrainMaterials : MonoBehaviour
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x000AD012 File Offset: 0x000AB412
		// (set) Token: 0x06001F32 RID: 7986 RVA: 0x000AD019 File Offset: 0x000AB419
		public static bool isPainting
		{
			get
			{
				return EditorTerrainMaterials._isPainting;
			}
			set
			{
				EditorTerrainMaterials._isPainting = value;
				LevelGround.updateVisibility(!EditorTerrainMaterials.isPainting);
				EditorTerrainMaterials.brush.gameObject.SetActive(EditorTerrainMaterials.isPainting);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x000AD042 File Offset: 0x000AB442
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x000AD04C File Offset: 0x000AB44C
		public static byte brushSize
		{
			get
			{
				return EditorTerrainMaterials._brushSize;
			}
			set
			{
				EditorTerrainMaterials._brushSize = value;
				if (EditorTerrainMaterials.brush != null)
				{
					EditorTerrainMaterials.brush.localScale = new Vector3((float)EditorTerrainMaterials.brushSize * 2f, (float)EditorTerrainMaterials.brushSize * 2f, (float)EditorTerrainMaterials.brushSize * 2f);
				}
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000AD0A4 File Offset: 0x000AB4A4
		private void Update()
		{
			if (!EditorTerrainMaterials.isPainting)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (Input.GetKeyDown(KeyCode.Z) && !LevelGround.previewHQ && Input.GetKey(KeyCode.LeftControl))
				{
					if (EditorTerrainMaterials.map2)
					{
						LevelGround.undoMaterial2();
					}
					else
					{
						LevelGround.undoMaterial();
					}
				}
				if (Input.GetKeyDown(KeyCode.X) && !LevelGround.previewHQ && Input.GetKey(KeyCode.LeftControl))
				{
					if (EditorTerrainMaterials.map2)
					{
						LevelGround.redoMaterial2();
					}
					else
					{
						LevelGround.redoMaterial();
					}
				}
				if (EditorInteract.groundHit.transform != null)
				{
					EditorTerrainMaterials.brush.position = EditorInteract.groundHit.point;
				}
				if (Input.GetKeyUp(ControlsSettings.primary) && !LevelGround.previewHQ && EditorTerrainMaterials.wasPainting)
				{
					if (EditorTerrainMaterials.map2)
					{
						LevelGround.registerMaterial2();
					}
					else
					{
						LevelGround.registerMaterial();
					}
				}
				if (Input.GetKey(ControlsSettings.primary) && !LevelGround.previewHQ && EditorInteract.groundHit.transform != null)
				{
					LevelGround.paint(EditorInteract.groundHit.point, (int)EditorTerrainMaterials.brushSize, EditorTerrainMaterials.brushNoise, (int)EditorTerrainMaterials.selected, EditorTerrainMaterials.map2);
					EditorTerrainMaterials.wasPainting = true;
				}
				else
				{
					EditorTerrainMaterials.wasPainting = false;
				}
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000AD220 File Offset: 0x000AB620
		private void Start()
		{
			EditorTerrainMaterials._isPainting = false;
			EditorTerrainMaterials.brush = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Paint"))).transform;
			EditorTerrainMaterials.brush.name = "Paint";
			EditorTerrainMaterials.brush.parent = Level.editing;
			EditorTerrainMaterials.brush.gameObject.SetActive(false);
			EditorTerrainMaterials.load();
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000AD284 File Offset: 0x000AB684
		public static void load()
		{
			if (ReadWrite.fileExists(Level.info.path + "/Editor/Materials.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Editor/Materials.dat", false, false, 0);
				byte b = block.readByte();
				EditorTerrainMaterials.brushSize = block.readByte();
				if (b > 1)
				{
					EditorTerrainMaterials.brushNoise = block.readSingle();
				}
				else
				{
					EditorTerrainMaterials.brushNoise = 0f;
				}
			}
			else
			{
				EditorTerrainMaterials.brushSize = EditorTerrainMaterials.MIN_BRUSH_SIZE;
				EditorTerrainMaterials.brushNoise = 0f;
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000AD31C File Offset: 0x000AB71C
		public static void save()
		{
			Block block = new Block();
			block.writeByte(EditorTerrainMaterials.SAVEDATA_VERSION);
			block.writeByte(EditorTerrainMaterials.brushSize);
			block.writeSingle(EditorTerrainMaterials.brushNoise);
			ReadWrite.writeBlock(Level.info.path + "/Editor/Materials.dat", false, false, block);
		}

		// Token: 0x040012DF RID: 4831
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x040012E0 RID: 4832
		public static readonly byte MIN_BRUSH_SIZE = 1;

		// Token: 0x040012E1 RID: 4833
		public static readonly byte MAX_BRUSH_SIZE = 254;

		// Token: 0x040012E2 RID: 4834
		private static bool _isPainting;

		// Token: 0x040012E3 RID: 4835
		private static bool wasPainting;

		// Token: 0x040012E4 RID: 4836
		public static byte selected;

		// Token: 0x040012E5 RID: 4837
		private static Transform brush;

		// Token: 0x040012E6 RID: 4838
		private static byte _brushSize;

		// Token: 0x040012E7 RID: 4839
		public static float brushNoise;

		// Token: 0x040012E8 RID: 4840
		public static bool map2;
	}
}
