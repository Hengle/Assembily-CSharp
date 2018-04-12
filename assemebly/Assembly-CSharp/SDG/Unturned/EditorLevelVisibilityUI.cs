using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000757 RID: 1879
	public class EditorLevelVisibilityUI
	{
		// Token: 0x06003509 RID: 13577 RVA: 0x0015F05C File Offset: 0x0015D45C
		public EditorLevelVisibilityUI()
		{
			EditorLevelVisibilityUI.localization = Localization.read("/Editor/EditorLevelVisibility.dat");
			EditorLevelVisibilityUI.container = new Sleek();
			EditorLevelVisibilityUI.container.positionScale_X = 1f;
			EditorLevelVisibilityUI.container.sizeScale_X = 1f;
			EditorLevelVisibilityUI.container.sizeScale_Y = 1f;
			EditorUI.window.add(EditorLevelVisibilityUI.container);
			EditorLevelVisibilityUI.active = false;
			EditorLevelVisibilityUI.roadsToggle = new SleekToggle();
			EditorLevelVisibilityUI.roadsToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.roadsToggle.positionOffset_Y = 90;
			EditorLevelVisibilityUI.roadsToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.roadsToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.roadsToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.roadsToggle.state = LevelVisibility.roadsVisible;
			EditorLevelVisibilityUI.roadsToggle.addLabel(EditorLevelVisibilityUI.localization.format("Roads_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle = EditorLevelVisibilityUI.roadsToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache0 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache0 = new Toggled(EditorLevelVisibilityUI.onToggledRoadsToggle);
			}
			sleekToggle.onToggled = EditorLevelVisibilityUI.<>f__mg$cache0;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.roadsToggle);
			EditorLevelVisibilityUI.navigationToggle = new SleekToggle();
			EditorLevelVisibilityUI.navigationToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.navigationToggle.positionOffset_Y = 140;
			EditorLevelVisibilityUI.navigationToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.navigationToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.navigationToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.navigationToggle.state = LevelVisibility.navigationVisible;
			EditorLevelVisibilityUI.navigationToggle.addLabel(EditorLevelVisibilityUI.localization.format("Navigation_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle2 = EditorLevelVisibilityUI.navigationToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache1 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache1 = new Toggled(EditorLevelVisibilityUI.onToggledNavigationToggle);
			}
			sleekToggle2.onToggled = EditorLevelVisibilityUI.<>f__mg$cache1;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.navigationToggle);
			EditorLevelVisibilityUI.nodesToggle = new SleekToggle();
			EditorLevelVisibilityUI.nodesToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.nodesToggle.positionOffset_Y = 190;
			EditorLevelVisibilityUI.nodesToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.nodesToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.nodesToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.nodesToggle.state = LevelVisibility.nodesVisible;
			EditorLevelVisibilityUI.nodesToggle.addLabel(EditorLevelVisibilityUI.localization.format("Nodes_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle3 = EditorLevelVisibilityUI.nodesToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache2 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache2 = new Toggled(EditorLevelVisibilityUI.onToggledNodesToggle);
			}
			sleekToggle3.onToggled = EditorLevelVisibilityUI.<>f__mg$cache2;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.nodesToggle);
			EditorLevelVisibilityUI.itemsToggle = new SleekToggle();
			EditorLevelVisibilityUI.itemsToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.itemsToggle.positionOffset_Y = 240;
			EditorLevelVisibilityUI.itemsToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.itemsToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.itemsToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.itemsToggle.state = LevelVisibility.itemsVisible;
			EditorLevelVisibilityUI.itemsToggle.addLabel(EditorLevelVisibilityUI.localization.format("Items_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle4 = EditorLevelVisibilityUI.itemsToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache3 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache3 = new Toggled(EditorLevelVisibilityUI.onToggledItemsToggle);
			}
			sleekToggle4.onToggled = EditorLevelVisibilityUI.<>f__mg$cache3;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.itemsToggle);
			EditorLevelVisibilityUI.playersToggle = new SleekToggle();
			EditorLevelVisibilityUI.playersToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.playersToggle.positionOffset_Y = 290;
			EditorLevelVisibilityUI.playersToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.playersToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.playersToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.playersToggle.state = LevelVisibility.playersVisible;
			EditorLevelVisibilityUI.playersToggle.addLabel(EditorLevelVisibilityUI.localization.format("Players_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle5 = EditorLevelVisibilityUI.playersToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache4 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache4 = new Toggled(EditorLevelVisibilityUI.onToggledPlayersToggle);
			}
			sleekToggle5.onToggled = EditorLevelVisibilityUI.<>f__mg$cache4;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.playersToggle);
			EditorLevelVisibilityUI.zombiesToggle = new SleekToggle();
			EditorLevelVisibilityUI.zombiesToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.zombiesToggle.positionOffset_Y = 340;
			EditorLevelVisibilityUI.zombiesToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.zombiesToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.zombiesToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.zombiesToggle.state = LevelVisibility.zombiesVisible;
			EditorLevelVisibilityUI.zombiesToggle.addLabel(EditorLevelVisibilityUI.localization.format("Zombies_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle6 = EditorLevelVisibilityUI.zombiesToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache5 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache5 = new Toggled(EditorLevelVisibilityUI.onToggledZombiesToggle);
			}
			sleekToggle6.onToggled = EditorLevelVisibilityUI.<>f__mg$cache5;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.zombiesToggle);
			EditorLevelVisibilityUI.vehiclesToggle = new SleekToggle();
			EditorLevelVisibilityUI.vehiclesToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.vehiclesToggle.positionOffset_Y = 390;
			EditorLevelVisibilityUI.vehiclesToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.vehiclesToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.vehiclesToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.vehiclesToggle.state = LevelVisibility.vehiclesVisible;
			EditorLevelVisibilityUI.vehiclesToggle.addLabel(EditorLevelVisibilityUI.localization.format("Vehicles_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle7 = EditorLevelVisibilityUI.vehiclesToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache6 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache6 = new Toggled(EditorLevelVisibilityUI.onToggledVehiclesToggle);
			}
			sleekToggle7.onToggled = EditorLevelVisibilityUI.<>f__mg$cache6;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.vehiclesToggle);
			EditorLevelVisibilityUI.borderToggle = new SleekToggle();
			EditorLevelVisibilityUI.borderToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.borderToggle.positionOffset_Y = 440;
			EditorLevelVisibilityUI.borderToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.borderToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.borderToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.borderToggle.state = LevelVisibility.borderVisible;
			EditorLevelVisibilityUI.borderToggle.addLabel(EditorLevelVisibilityUI.localization.format("Border_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle8 = EditorLevelVisibilityUI.borderToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache7 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache7 = new Toggled(EditorLevelVisibilityUI.onToggledBorderToggle);
			}
			sleekToggle8.onToggled = EditorLevelVisibilityUI.<>f__mg$cache7;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.borderToggle);
			EditorLevelVisibilityUI.animalsToggle = new SleekToggle();
			EditorLevelVisibilityUI.animalsToggle.positionOffset_X = -210;
			EditorLevelVisibilityUI.animalsToggle.positionOffset_Y = 490;
			EditorLevelVisibilityUI.animalsToggle.positionScale_X = 1f;
			EditorLevelVisibilityUI.animalsToggle.sizeOffset_X = 40;
			EditorLevelVisibilityUI.animalsToggle.sizeOffset_Y = 40;
			EditorLevelVisibilityUI.animalsToggle.state = LevelVisibility.animalsVisible;
			EditorLevelVisibilityUI.animalsToggle.addLabel(EditorLevelVisibilityUI.localization.format("Animals_Label"), ESleekSide.RIGHT);
			SleekToggle sleekToggle9 = EditorLevelVisibilityUI.animalsToggle;
			if (EditorLevelVisibilityUI.<>f__mg$cache8 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache8 = new Toggled(EditorLevelVisibilityUI.onToggledAnimalsToggle);
			}
			sleekToggle9.onToggled = EditorLevelVisibilityUI.<>f__mg$cache8;
			EditorLevelVisibilityUI.container.add(EditorLevelVisibilityUI.animalsToggle);
			EditorLevelVisibilityUI.regionLabels = new SleekLabel[(int)(EditorLevelVisibilityUI.DEBUG_SIZE * EditorLevelVisibilityUI.DEBUG_SIZE)];
			for (int i = 0; i < EditorLevelVisibilityUI.regionLabels.Length; i++)
			{
				SleekLabel sleekLabel = new SleekLabel();
				sleekLabel.sizeOffset_X = 200;
				sleekLabel.sizeOffset_Y = 50;
				EditorLevelVisibilityUI.regionLabels[i] = sleekLabel;
				sleekLabel.foregroundTint = ESleekTint.NONE;
				EditorLevelVisibilityUI.container.add(sleekLabel);
			}
			EditorArea area = Editor.editor.area;
			Delegate onRegionUpdated = area.onRegionUpdated;
			if (EditorLevelVisibilityUI.<>f__mg$cache9 == null)
			{
				EditorLevelVisibilityUI.<>f__mg$cache9 = new EditorRegionUpdated(EditorLevelVisibilityUI.onRegionUpdated);
			}
			area.onRegionUpdated = (EditorRegionUpdated)Delegate.Combine(onRegionUpdated, EditorLevelVisibilityUI.<>f__mg$cache9);
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x0015F784 File Offset: 0x0015DB84
		public static void open()
		{
			if (EditorLevelVisibilityUI.active)
			{
				return;
			}
			EditorLevelVisibilityUI.active = true;
			EditorLevelVisibilityUI.update((int)Editor.editor.area.region_x, (int)Editor.editor.area.region_y);
			EditorUI.message(EEditorMessage.VISIBILITY);
			EditorLevelVisibilityUI.container.lerpPositionScale(0f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0015F7E8 File Offset: 0x0015DBE8
		public static void close()
		{
			if (!EditorLevelVisibilityUI.active)
			{
				return;
			}
			EditorLevelVisibilityUI.active = false;
			for (int i = 0; i < EditorLevelVisibilityUI.regionLabels.Length; i++)
			{
				SleekLabel sleekLabel = EditorLevelVisibilityUI.regionLabels[i];
				sleekLabel.isVisible = false;
			}
			EditorLevelVisibilityUI.container.lerpPositionScale(1f, 0f, ESleekLerp.EXPONENTIAL, 20f);
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x0015F847 File Offset: 0x0015DC47
		private static void onToggledRoadsToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.roadsVisible = state;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x0015F84F File Offset: 0x0015DC4F
		private static void onToggledNavigationToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.navigationVisible = state;
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x0015F857 File Offset: 0x0015DC57
		private static void onToggledNodesToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.nodesVisible = state;
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0015F85F File Offset: 0x0015DC5F
		private static void onToggledItemsToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.itemsVisible = state;
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x0015F867 File Offset: 0x0015DC67
		private static void onToggledPlayersToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.playersVisible = state;
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x0015F86F File Offset: 0x0015DC6F
		private static void onToggledZombiesToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.zombiesVisible = state;
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x0015F877 File Offset: 0x0015DC77
		private static void onToggledVehiclesToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.vehiclesVisible = state;
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0015F87F File Offset: 0x0015DC7F
		private static void onToggledBorderToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.borderVisible = state;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0015F887 File Offset: 0x0015DC87
		private static void onToggledAnimalsToggle(SleekToggle toggle, bool state)
		{
			LevelVisibility.animalsVisible = state;
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0015F88F File Offset: 0x0015DC8F
		private static void onRegionUpdated(byte old_x, byte old_y, byte new_x, byte new_y)
		{
			if (!EditorLevelVisibilityUI.active)
			{
				return;
			}
			EditorLevelVisibilityUI.update((int)new_x, (int)new_y);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0015F8A4 File Offset: 0x0015DCA4
		private static void update(int x, int y)
		{
			for (int i = 0; i < (int)EditorLevelVisibilityUI.DEBUG_SIZE; i++)
			{
				for (int j = 0; j < (int)EditorLevelVisibilityUI.DEBUG_SIZE; j++)
				{
					int num = i * (int)EditorLevelVisibilityUI.DEBUG_SIZE + j;
					int num2 = x - (int)(EditorLevelVisibilityUI.DEBUG_SIZE / 2) + i;
					int num3 = y - (int)(EditorLevelVisibilityUI.DEBUG_SIZE / 2) + j;
					SleekLabel sleekLabel = EditorLevelVisibilityUI.regionLabels[num];
					if (Regions.checkSafe(num2, num3))
					{
						int num4 = LevelObjects.objects[num2, num3].Count + LevelGround.trees[num2, num3].Count;
						int num5 = LevelObjects.total + LevelGround.total;
						double num6 = Math.Round((double)num4 / (double)num5 * 1000.0) / 10.0;
						int num7 = 0;
						for (int k = 0; k < LevelObjects.objects[num2, num3].Count; k++)
						{
							LevelObject levelObject = LevelObjects.objects[num2, num3][k];
							if (levelObject.transform)
							{
								levelObject.transform.GetComponents<MeshFilter>(EditorLevelVisibilityUI.meshes);
								if (EditorLevelVisibilityUI.meshes.Count == 0)
								{
									Transform transform = levelObject.transform.FindChild("Model_0");
									if (transform)
									{
										transform.GetComponentsInChildren<MeshFilter>(true, EditorLevelVisibilityUI.meshes);
									}
								}
								if (EditorLevelVisibilityUI.meshes.Count != 0)
								{
									for (int l = 0; l < EditorLevelVisibilityUI.meshes.Count; l++)
									{
										Mesh sharedMesh = EditorLevelVisibilityUI.meshes[l].sharedMesh;
										if (sharedMesh)
										{
											num7 += sharedMesh.triangles.Length;
										}
									}
								}
							}
						}
						for (int m = 0; m < LevelGround.trees[num2, num3].Count; m++)
						{
							ResourceSpawnpoint resourceSpawnpoint = LevelGround.trees[num2, num3][m];
							if (resourceSpawnpoint.model)
							{
								resourceSpawnpoint.model.GetComponents<MeshFilter>(EditorLevelVisibilityUI.meshes);
								if (EditorLevelVisibilityUI.meshes.Count == 0)
								{
									Transform transform2 = resourceSpawnpoint.model.FindChild("Model_0");
									if (transform2)
									{
										transform2.GetComponentsInChildren<MeshFilter>(true, EditorLevelVisibilityUI.meshes);
									}
								}
								if (EditorLevelVisibilityUI.meshes.Count != 0)
								{
									for (int n = 0; n < EditorLevelVisibilityUI.meshes.Count; n++)
									{
										Mesh sharedMesh2 = EditorLevelVisibilityUI.meshes[n].sharedMesh;
										if (sharedMesh2)
										{
											num7 += sharedMesh2.triangles.Length;
										}
									}
								}
							}
						}
						long num8 = (long)num4 * (long)num7;
						float quality = Mathf.Clamp01((float)(1.0 - (double)num8 / 50000000.0));
						sleekLabel.text = EditorLevelVisibilityUI.localization.format("Point", new object[]
						{
							num2,
							num3
						});
						SleekLabel sleekLabel2 = sleekLabel;
						sleekLabel2.text = sleekLabel2.text + "\n" + EditorLevelVisibilityUI.localization.format("Objects", new object[]
						{
							num4,
							num6
						});
						SleekLabel sleekLabel3 = sleekLabel;
						sleekLabel3.text = sleekLabel3.text + "\n" + EditorLevelVisibilityUI.localization.format("Triangles", new object[]
						{
							num7
						});
						if (num4 == 0 && num7 == 0)
						{
							sleekLabel.foregroundColor = Color.white;
						}
						else
						{
							sleekLabel.foregroundColor = ItemTool.getQualityColor(quality);
						}
					}
				}
			}
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0015FC74 File Offset: 0x0015E074
		public static void update()
		{
			for (int i = 0; i < (int)EditorLevelVisibilityUI.DEBUG_SIZE; i++)
			{
				for (int j = 0; j < (int)EditorLevelVisibilityUI.DEBUG_SIZE; j++)
				{
					int num = i * (int)EditorLevelVisibilityUI.DEBUG_SIZE + j;
					int x = (int)(Editor.editor.area.region_x - EditorLevelVisibilityUI.DEBUG_SIZE / 2) + i;
					int y = (int)(Editor.editor.area.region_y - EditorLevelVisibilityUI.DEBUG_SIZE / 2) + j;
					SleekLabel sleekLabel = EditorLevelVisibilityUI.regionLabels[num];
					Vector3 a;
					if (Regions.tryGetPoint(x, y, out a))
					{
						a = MainCamera.instance.WorldToScreenPoint(a + new Vector3((float)(Regions.REGION_SIZE / 2), 0f, (float)(Regions.REGION_SIZE / 2)));
						if (a.z > 0f)
						{
							sleekLabel.positionOffset_X = (int)(a.x - 100f);
							sleekLabel.positionOffset_Y = (int)((float)Screen.height - a.y - 25f);
							sleekLabel.isVisible = true;
						}
						else
						{
							sleekLabel.isVisible = false;
						}
					}
					else
					{
						sleekLabel.isVisible = false;
					}
				}
			}
		}

		// Token: 0x04002475 RID: 9333
		private static readonly byte DEBUG_SIZE = 7;

		// Token: 0x04002476 RID: 9334
		private static Local localization;

		// Token: 0x04002477 RID: 9335
		private static Sleek container;

		// Token: 0x04002478 RID: 9336
		public static bool active;

		// Token: 0x04002479 RID: 9337
		private static List<MeshFilter> meshes = new List<MeshFilter>();

		// Token: 0x0400247A RID: 9338
		public static SleekToggle roadsToggle;

		// Token: 0x0400247B RID: 9339
		public static SleekToggle navigationToggle;

		// Token: 0x0400247C RID: 9340
		public static SleekToggle nodesToggle;

		// Token: 0x0400247D RID: 9341
		public static SleekToggle itemsToggle;

		// Token: 0x0400247E RID: 9342
		public static SleekToggle playersToggle;

		// Token: 0x0400247F RID: 9343
		public static SleekToggle zombiesToggle;

		// Token: 0x04002480 RID: 9344
		public static SleekToggle vehiclesToggle;

		// Token: 0x04002481 RID: 9345
		public static SleekToggle borderToggle;

		// Token: 0x04002482 RID: 9346
		public static SleekToggle animalsToggle;

		// Token: 0x04002483 RID: 9347
		private static SleekLabel[] regionLabels;

		// Token: 0x04002484 RID: 9348
		[CompilerGenerated]
		private static Toggled <>f__mg$cache0;

		// Token: 0x04002485 RID: 9349
		[CompilerGenerated]
		private static Toggled <>f__mg$cache1;

		// Token: 0x04002486 RID: 9350
		[CompilerGenerated]
		private static Toggled <>f__mg$cache2;

		// Token: 0x04002487 RID: 9351
		[CompilerGenerated]
		private static Toggled <>f__mg$cache3;

		// Token: 0x04002488 RID: 9352
		[CompilerGenerated]
		private static Toggled <>f__mg$cache4;

		// Token: 0x04002489 RID: 9353
		[CompilerGenerated]
		private static Toggled <>f__mg$cache5;

		// Token: 0x0400248A RID: 9354
		[CompilerGenerated]
		private static Toggled <>f__mg$cache6;

		// Token: 0x0400248B RID: 9355
		[CompilerGenerated]
		private static Toggled <>f__mg$cache7;

		// Token: 0x0400248C RID: 9356
		[CompilerGenerated]
		private static Toggled <>f__mg$cache8;

		// Token: 0x0400248D RID: 9357
		[CompilerGenerated]
		private static EditorRegionUpdated <>f__mg$cache9;
	}
}
