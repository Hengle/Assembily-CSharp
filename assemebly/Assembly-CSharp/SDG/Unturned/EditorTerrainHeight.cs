using System;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x0200049E RID: 1182
	public class EditorTerrainHeight : MonoBehaviour
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000AC871 File Offset: 0x000AAC71
		// (set) Token: 0x06001F24 RID: 7972 RVA: 0x000AC878 File Offset: 0x000AAC78
		public static bool isTerraforming
		{
			get
			{
				return EditorTerrainHeight._isTerraforming;
			}
			set
			{
				EditorTerrainHeight._isTerraforming = value;
				LevelGround.updateVisibility(!EditorTerrainHeight.isTerraforming);
				EditorTerrainHeight.brush.gameObject.SetActive(EditorTerrainHeight.isTerraforming);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x000AC8A1 File Offset: 0x000AACA1
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x000AC8A8 File Offset: 0x000AACA8
		public static byte brushSize
		{
			get
			{
				return EditorTerrainHeight._brushSize;
			}
			set
			{
				EditorTerrainHeight._brushSize = value;
				if (EditorTerrainHeight.brush != null)
				{
					EditorTerrainHeight.brush.localScale = new Vector3((float)EditorTerrainHeight.brushSize * 2f, (float)EditorTerrainHeight.brushSize * 2f, (float)EditorTerrainHeight.brushSize * 2f);
				}
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x000AC8FE File Offset: 0x000AACFE
		// (set) Token: 0x06001F28 RID: 7976 RVA: 0x000AC908 File Offset: 0x000AAD08
		public static float brushHeight
		{
			get
			{
				return EditorTerrainHeight._brushHeight;
			}
			set
			{
				EditorTerrainHeight._brushHeight = value;
				if (EditorTerrainHeight.brushMode == EPaintMode.FLATTEN && EditorTerrainHeight.brush != null)
				{
					EditorTerrainHeight.brush.position = new Vector3(EditorTerrainHeight.brush.position.x, EditorTerrainHeight.brushHeight * Level.TERRAIN, EditorTerrainHeight.brush.position.z);
				}
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x000AC974 File Offset: 0x000AAD74
		// (set) Token: 0x06001F2A RID: 7978 RVA: 0x000AC97C File Offset: 0x000AAD7C
		public static EPaintMode brushMode
		{
			get
			{
				return EditorTerrainHeight._brushMode;
			}
			set
			{
				EditorTerrainHeight._brushMode = value;
				if (EditorTerrainHeight.brushMode == EPaintMode.ADJUST_UP)
				{
					EditorTerrainHeight.adjustUpBrush.gameObject.SetActive(true);
					EditorTerrainHeight.adjustDownBrush.gameObject.SetActive(false);
					EditorTerrainHeight.smoothBrush.gameObject.SetActive(false);
					EditorTerrainHeight.flattenBrush.gameObject.SetActive(false);
				}
				else if (EditorTerrainHeight.brushMode == EPaintMode.ADJUST_DOWN)
				{
					EditorTerrainHeight.adjustUpBrush.gameObject.SetActive(false);
					EditorTerrainHeight.adjustDownBrush.gameObject.SetActive(true);
					EditorTerrainHeight.smoothBrush.gameObject.SetActive(false);
					EditorTerrainHeight.flattenBrush.gameObject.SetActive(false);
				}
				else if (EditorTerrainHeight.brushMode == EPaintMode.SMOOTH)
				{
					EditorTerrainHeight.adjustUpBrush.gameObject.SetActive(false);
					EditorTerrainHeight.adjustDownBrush.gameObject.SetActive(false);
					EditorTerrainHeight.smoothBrush.gameObject.SetActive(true);
					EditorTerrainHeight.flattenBrush.gameObject.SetActive(false);
				}
				else if (EditorTerrainHeight.brushMode == EPaintMode.FLATTEN)
				{
					EditorTerrainHeight.adjustUpBrush.gameObject.SetActive(false);
					EditorTerrainHeight.adjustDownBrush.gameObject.SetActive(false);
					EditorTerrainHeight.smoothBrush.gameObject.SetActive(false);
					EditorTerrainHeight.flattenBrush.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000ACACC File Offset: 0x000AAECC
		private void Update()
		{
			if (!EditorTerrainHeight.isTerraforming)
			{
				return;
			}
			if (!EditorInteract.isFlying && GUIUtility.hotControl == 0)
			{
				if (Input.GetKeyDown(ControlsSettings.tool_0))
				{
					if (EditorTerrainHeight.brushMode == EPaintMode.ADJUST_UP)
					{
						EditorTerrainHeight.brushMode = EPaintMode.ADJUST_DOWN;
					}
					else
					{
						EditorTerrainHeight.brushMode = EPaintMode.ADJUST_UP;
					}
				}
				if (Input.GetKeyDown(ControlsSettings.tool_1))
				{
					EditorTerrainHeight.brushMode = EPaintMode.SMOOTH;
				}
				if (Input.GetKeyDown(ControlsSettings.tool_2))
				{
					EditorTerrainHeight.brushMode = EPaintMode.FLATTEN;
				}
				if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftControl))
				{
					if (EditorTerrainHeight.map2)
					{
						LevelGround.undoHeight2();
					}
					else
					{
						LevelGround.undoHeight();
					}
				}
				if (Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.LeftControl))
				{
					if (EditorTerrainHeight.map2)
					{
						LevelGround.redoHeight2();
					}
					else
					{
						LevelGround.redoHeight();
					}
				}
				if (EditorInteract.groundHit.transform != null)
				{
					if (EditorTerrainHeight.brushMode == EPaintMode.FLATTEN)
					{
						EditorTerrainHeight.brush.position = new Vector3(EditorInteract.groundHit.point.x, EditorTerrainHeight.brushHeight * Level.TERRAIN, EditorInteract.groundHit.point.z);
					}
					else
					{
						EditorTerrainHeight.brush.position = EditorInteract.groundHit.point;
					}
				}
				if (Input.GetKeyUp(ControlsSettings.primary) && EditorTerrainHeight.wasTerraforming)
				{
					if (EditorTerrainHeight.map2)
					{
						LevelGround.registerHeight2();
					}
					else
					{
						LevelGround.registerHeight();
					}
				}
				if (Input.GetKey(ControlsSettings.primary) && EditorInteract.groundHit.transform != null)
				{
					if (EditorTerrainHeight.brushMode == EPaintMode.ADJUST_UP)
					{
						LevelGround.adjust(EditorInteract.groundHit.point, (int)EditorTerrainHeight.brushSize, EditorTerrainHeight.brushStrength, EditorTerrainHeight.brushNoise, EditorTerrainHeight.map2);
					}
					else if (EditorTerrainHeight.brushMode == EPaintMode.ADJUST_DOWN)
					{
						LevelGround.adjust(EditorInteract.groundHit.point, (int)EditorTerrainHeight.brushSize, -EditorTerrainHeight.brushStrength, EditorTerrainHeight.brushNoise, EditorTerrainHeight.map2);
					}
					else if (EditorTerrainHeight.brushMode == EPaintMode.SMOOTH)
					{
						LevelGround.smooth(EditorInteract.groundHit.point, (int)EditorTerrainHeight.brushSize, EditorTerrainHeight.brushStrength, EditorTerrainHeight.brushNoise, EditorTerrainHeight.map2);
					}
					else if (EditorTerrainHeight.brushMode == EPaintMode.FLATTEN)
					{
						LevelGround.flatten(EditorInteract.groundHit.point, (int)EditorTerrainHeight.brushSize, EditorTerrainHeight.brushHeight, EditorTerrainHeight.brushStrength, EditorTerrainHeight.brushNoise, EditorTerrainHeight.map2);
					}
					EditorTerrainHeight.wasTerraforming = true;
				}
				else
				{
					EditorTerrainHeight.wasTerraforming = false;
				}
				if (Input.GetKeyDown(ControlsSettings.tool_2) && EditorInteract.groundHit.transform != null)
				{
					EditorTerrainHeight.brushHeight = EditorInteract.groundHit.point.y / Level.TERRAIN;
					if (EditorTerrainHeight.brushHeight < 0f)
					{
						EditorTerrainHeight.brushHeight = 0f;
					}
					else if (EditorTerrainHeight.brushHeight > 1f)
					{
						EditorTerrainHeight.brushHeight = 1f;
					}
					EditorTerrainHeightUI.heightValue.state = EditorTerrainHeight.brushHeight;
				}
			}
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000ACE10 File Offset: 0x000AB210
		private void Start()
		{
			EditorTerrainHeight._isTerraforming = false;
			EditorTerrainHeight.brush = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Edit/Brush"))).transform;
			EditorTerrainHeight.brush.name = "Brush";
			EditorTerrainHeight.brush.parent = Level.editing;
			EditorTerrainHeight.brush.gameObject.SetActive(false);
			EditorTerrainHeight.adjustUpBrush = EditorTerrainHeight.brush.FindChild("Adjust_Up");
			EditorTerrainHeight.adjustDownBrush = EditorTerrainHeight.brush.FindChild("Adjust_Down");
			EditorTerrainHeight.smoothBrush = EditorTerrainHeight.brush.FindChild("Smooth");
			EditorTerrainHeight.flattenBrush = EditorTerrainHeight.brush.FindChild("Flatten");
			EditorTerrainHeight.brushMode = EPaintMode.ADJUST_UP;
			EditorTerrainHeight.load();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000ACECC File Offset: 0x000AB2CC
		public static void load()
		{
			if (ReadWrite.fileExists(Level.info.path + "/Editor/Height.dat", false, false))
			{
				Block block = ReadWrite.readBlock(Level.info.path + "/Editor/Height.dat", false, false, 0);
				byte b = block.readByte();
				EditorTerrainHeight.brushSize = block.readByte();
				EditorTerrainHeight.brushStrength = block.readSingle();
				EditorTerrainHeight.brushHeight = block.readSingle();
				if (b > 1)
				{
					EditorTerrainHeight.brushNoise = block.readSingle();
				}
				else
				{
					EditorTerrainHeight.brushNoise = 0f;
				}
			}
			else
			{
				EditorTerrainHeight.brushSize = EditorTerrainHeight.MIN_BRUSH_SIZE;
				EditorTerrainHeight.brushStrength = 1f;
				EditorTerrainHeight.brushHeight = 0f;
				EditorTerrainHeight.brushNoise = 0f;
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000ACF8C File Offset: 0x000AB38C
		public static void save()
		{
			Block block = new Block();
			block.writeByte(EditorTerrainHeight.SAVEDATA_VERSION);
			block.writeByte(EditorTerrainHeight.brushSize);
			block.writeSingle(EditorTerrainHeight.brushStrength);
			block.writeSingle(EditorTerrainHeight.brushHeight);
			block.writeSingle(EditorTerrainHeight.brushNoise);
			ReadWrite.writeBlock(Level.info.path + "/Editor/Height.dat", false, false, block);
		}

		// Token: 0x040012CF RID: 4815
		public static readonly byte SAVEDATA_VERSION = 2;

		// Token: 0x040012D0 RID: 4816
		public static readonly byte MIN_BRUSH_SIZE = 2;

		// Token: 0x040012D1 RID: 4817
		public static readonly byte MAX_BRUSH_SIZE = 253;

		// Token: 0x040012D2 RID: 4818
		private static bool _isTerraforming;

		// Token: 0x040012D3 RID: 4819
		private static bool wasTerraforming;

		// Token: 0x040012D4 RID: 4820
		private static Transform brush;

		// Token: 0x040012D5 RID: 4821
		private static Transform adjustUpBrush;

		// Token: 0x040012D6 RID: 4822
		private static Transform adjustDownBrush;

		// Token: 0x040012D7 RID: 4823
		private static Transform smoothBrush;

		// Token: 0x040012D8 RID: 4824
		private static Transform flattenBrush;

		// Token: 0x040012D9 RID: 4825
		private static byte _brushSize;

		// Token: 0x040012DA RID: 4826
		public static float brushNoise;

		// Token: 0x040012DB RID: 4827
		public static float brushStrength;

		// Token: 0x040012DC RID: 4828
		private static float _brushHeight;

		// Token: 0x040012DD RID: 4829
		private static EPaintMode _brushMode;

		// Token: 0x040012DE RID: 4830
		public static bool map2;
	}
}
