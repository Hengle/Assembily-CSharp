using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SDG.Framework.Devkit;
using SDG.Framework.Devkit.Transactions;
using UnityEngine;

namespace SDG.Unturned
{
	// Token: 0x02000558 RID: 1368
	public class LevelObjects : MonoBehaviour
	{
		// Token: 0x06002589 RID: 9609 RVA: 0x000DA9F4 File Offset: 0x000D8DF4
		private static uint generateUniqueInstanceID()
		{
			uint num = LevelObjects.availableInstanceID;
			LevelObjects.availableInstanceID = num + 1u;
			return num;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x000DAA10 File Offset: 0x000D8E10
		public static Transform models
		{
			get
			{
				return LevelObjects._models;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000DAA17 File Offset: 0x000D8E17
		public static List<LevelObject>[,] objects
		{
			get
			{
				return LevelObjects._objects;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x000DAA1E File Offset: 0x000D8E1E
		public static List<LevelBuildableObject>[,] buildables
		{
			get
			{
				return LevelObjects._buildables;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x000DAA25 File Offset: 0x000D8E25
		public static int total
		{
			get
			{
				return LevelObjects._total;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000DAA2C File Offset: 0x000D8E2C
		public static byte[] hash
		{
			get
			{
				return LevelObjects._hash;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000DAA33 File Offset: 0x000D8E33
		public static bool[,] regions
		{
			get
			{
				return LevelObjects._regions;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002590 RID: 9616 RVA: 0x000DAA3A File Offset: 0x000D8E3A
		public static int[,] loads
		{
			get
			{
				return LevelObjects._loads;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000DAA41 File Offset: 0x000D8E41
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x000DAA48 File Offset: 0x000D8E48
		public static bool shouldInstantlyLoad { get; private set; }

		// Token: 0x06002593 RID: 9619 RVA: 0x000DAA50 File Offset: 0x000D8E50
		public static void undo()
		{
			while (LevelObjects.frame <= LevelObjects.reun.Length - 1)
			{
				if (LevelObjects.reun[LevelObjects.frame] != null)
				{
					LevelObjects.reun[LevelObjects.frame].undo();
				}
				if (LevelObjects.frame >= LevelObjects.reun.Length - 1 || LevelObjects.reun[LevelObjects.frame + 1] == null)
				{
					break;
				}
				LevelObjects.frame++;
				if (LevelObjects.reun[LevelObjects.frame].step != LevelObjects.step)
				{
					LevelObjects.step--;
					break;
				}
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x000DAAFC File Offset: 0x000D8EFC
		public static void redo()
		{
			while (LevelObjects.frame >= 0)
			{
				if (LevelObjects.reun[LevelObjects.frame] != null)
				{
					LevelObjects.reun[LevelObjects.frame].redo();
				}
				if (LevelObjects.frame <= 0 || LevelObjects.reun[LevelObjects.frame - 1] == null)
				{
					break;
				}
				LevelObjects.frame--;
				if (LevelObjects.reun[LevelObjects.frame].step != LevelObjects.step)
				{
					LevelObjects.step++;
					break;
				}
			}
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x000DAB98 File Offset: 0x000D8F98
		public static Transform register(IReun newReun)
		{
			if (LevelObjects.frame > 0)
			{
				LevelObjects.reun = new IReun[LevelObjects.reun.Length];
				LevelObjects.frame = 0;
			}
			for (int i = LevelObjects.reun.Length - 1; i > 0; i--)
			{
				LevelObjects.reun[i] = LevelObjects.reun[i - 1];
			}
			LevelObjects.reun[0] = newReun;
			return LevelObjects.reun[0].redo();
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x000DAC08 File Offset: 0x000D9008
		public static void transformObject(Transform select, Vector3 toPosition, Quaternion toRotation, Vector3 toScale, Vector3 fromPosition, Quaternion fromRotation, Vector3 fromScale)
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(fromPosition, out b, out b2))
			{
				byte b3;
				byte b4;
				if (Regions.tryGetCoordinate(toPosition, out b3, out b4))
				{
					LevelObject levelObject = null;
					int index = -1;
					for (int i = 0; i < LevelObjects.objects[(int)b, (int)b2].Count; i++)
					{
						if (LevelObjects.objects[(int)b, (int)b2][i].transform == select)
						{
							levelObject = LevelObjects.objects[(int)b, (int)b2][i];
							index = i;
							break;
						}
					}
					if (levelObject != null)
					{
						if (b != b3 || b2 != b4)
						{
							LevelObjects.objects[(int)b, (int)b2].RemoveAt(index);
							LevelObjects.objects[(int)b3, (int)b4].Add(levelObject);
						}
						if (levelObject.transform != null)
						{
							levelObject.transform.position = toPosition;
							levelObject.transform.rotation = toRotation;
							levelObject.transform.localScale = toScale;
						}
						if (levelObject.skybox != null)
						{
							levelObject.skybox.position = toPosition;
							levelObject.skybox.rotation = toRotation;
							levelObject.skybox.localScale = toScale;
						}
					}
					else
					{
						LevelBuildableObject levelBuildableObject = null;
						int index2 = -1;
						for (int j = 0; j < LevelObjects.buildables[(int)b, (int)b2].Count; j++)
						{
							if (LevelObjects.buildables[(int)b, (int)b2][j].transform == select)
							{
								levelBuildableObject = LevelObjects.buildables[(int)b, (int)b2][j];
								index2 = j;
								break;
							}
						}
						if (levelBuildableObject != null)
						{
							if (b != b3 || b2 != b4)
							{
								LevelObjects.buildables[(int)b, (int)b2].RemoveAt(index2);
								LevelObjects.buildables[(int)b3, (int)b4].Add(levelBuildableObject);
							}
							if (levelBuildableObject.transform != null)
							{
								levelBuildableObject.transform.position = toPosition;
								levelBuildableObject.transform.rotation = toRotation;
							}
						}
						else
						{
							select.position = fromPosition;
							select.rotation = fromRotation;
							select.localScale = fromScale;
						}
					}
				}
				else
				{
					select.position = fromPosition;
					select.rotation = fromRotation;
					select.localScale = fromScale;
				}
			}
			else
			{
				select.position = fromPosition;
				select.rotation = fromRotation;
				select.localScale = fromScale;
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x000DAE7E File Offset: 0x000D927E
		public static void registerTransformObject(Transform select, Vector3 toPosition, Quaternion toRotation, Vector3 toScale, Vector3 fromPosition, Quaternion fromRotation, Vector3 fromScale)
		{
			LevelObjects.register(new ReunObjectTransform(LevelObjects.step, select, fromPosition, fromRotation, fromScale, toPosition, toRotation, toScale));
		}

		// Token: 0x06002598 RID: 9624 RVA: 0x000DAE9C File Offset: 0x000D929C
		public static DevkitHierarchyWorldObject addDevkitObject(Guid GUID, Vector3 position, Quaternion rotation, Vector3 scale, ELevelObjectPlacementOrigin placementOrigin)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = position;
			gameObject.transform.rotation = rotation;
			gameObject.transform.localScale = scale;
			DevkitTransactionUtility.recordInstantiation(gameObject);
			DevkitHierarchyWorldObject devkitHierarchyWorldObject = gameObject.AddComponent<DevkitHierarchyWorldObject>();
			LevelHierarchy.initItem(devkitHierarchyWorldObject);
			devkitHierarchyWorldObject.GUID = GUID;
			devkitHierarchyWorldObject.placementOrigin = placementOrigin;
			return devkitHierarchyWorldObject;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x000DAEF8 File Offset: 0x000D92F8
		public static void registerDevkitObject(LevelObject levelObject, out byte x, out byte y)
		{
			if (Regions.tryGetCoordinate(levelObject.transform.position, out x, out y))
			{
				LevelObjects.objects[(int)x, (int)y].Add(levelObject);
				if (LevelObjects.regions[(int)x, (int)y])
				{
					levelObject.enableCollision();
					if (!levelObject.isSpeciallyCulled)
					{
						levelObject.enableVisual();
					}
					levelObject.disableSkybox();
				}
				else
				{
					levelObject.disableCollision();
					if (!levelObject.isSpeciallyCulled)
					{
						levelObject.disableVisual();
					}
					if (levelObject.isLandmarkQualityMet)
					{
						levelObject.enableSkybox();
					}
				}
			}
			else
			{
				levelObject.enableCollision();
				if (!levelObject.isSpeciallyCulled)
				{
					levelObject.enableVisual();
				}
				levelObject.disableSkybox();
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000DAFAF File Offset: 0x000D93AF
		public static void moveDevkitObject(LevelObject levelObject, byte old_x, byte old_y, byte new_x, byte new_y)
		{
			LevelObjects.objects[(int)old_x, (int)old_y].Remove(levelObject);
			LevelObjects.objects[(int)new_x, (int)new_y].Add(levelObject);
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000DAFD7 File Offset: 0x000D93D7
		public static void unregisterDevkitObject(LevelObject levelObject, byte x, byte y)
		{
			LevelObjects.objects[(int)x, (int)y].Remove(levelObject);
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000DAFEC File Offset: 0x000D93EC
		public static Transform addObject(Vector3 position, Quaternion rotation, Vector3 scale, ushort id, string name, Guid GUID, ELevelObjectPlacementOrigin placementOrigin)
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(position, out b, out b2))
			{
				LevelObject levelObject = new LevelObject(position, rotation, scale, id, name, GUID, placementOrigin, LevelObjects.generateUniqueInstanceID());
				levelObject.enableCollision();
				levelObject.enableVisual();
				levelObject.disableSkybox();
				LevelObjects.objects[(int)b, (int)b2].Add(levelObject);
				LevelObjects._total++;
				return levelObject.transform;
			}
			return null;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000DB058 File Offset: 0x000D9458
		public static Transform addBuildable(Vector3 position, Quaternion rotation, ushort id)
		{
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(position, out b, out b2))
			{
				LevelBuildableObject levelBuildableObject = new LevelBuildableObject(position, rotation, id);
				levelBuildableObject.enable();
				LevelObjects.buildables[(int)b, (int)b2].Add(levelBuildableObject);
				LevelObjects._total++;
				return levelBuildableObject.transform;
			}
			return null;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000DB0A9 File Offset: 0x000D94A9
		public static Transform registerAddObject(Vector3 position, Quaternion rotation, Vector3 scale, ObjectAsset objectAsset, ItemAsset itemAsset)
		{
			return LevelObjects.register(new ReunObjectAdd(LevelObjects.step, objectAsset, itemAsset, position, rotation, scale));
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000DB0C0 File Offset: 0x000D94C0
		public static void removeObject(Transform select)
		{
			if (select == null)
			{
				return;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(select.position, out b, out b2))
			{
				for (int i = 0; i < LevelObjects.objects[(int)b, (int)b2].Count; i++)
				{
					if (LevelObjects.objects[(int)b, (int)b2][i].transform == select)
					{
						LevelObjects.objects[(int)b, (int)b2][i].destroy();
						LevelObjects.objects[(int)b, (int)b2].RemoveAt(i);
						LevelObjects._total--;
						break;
					}
				}
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000DB16C File Offset: 0x000D956C
		public static void removeBuildable(Transform select)
		{
			if (select == null)
			{
				return;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(select.position, out b, out b2))
			{
				for (int i = 0; i < LevelObjects.buildables[(int)b, (int)b2].Count; i++)
				{
					if (LevelObjects.buildables[(int)b, (int)b2][i].transform == select)
					{
						LevelObjects.buildables[(int)b, (int)b2][i].destroy();
						LevelObjects.buildables[(int)b, (int)b2].RemoveAt(i);
						LevelObjects._total--;
						break;
					}
				}
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000DB218 File Offset: 0x000D9618
		public static void registerRemoveObject(Transform select)
		{
			if (select == null)
			{
				return;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(select.position, out b, out b2))
			{
				if (select.CompareTag("Barricade") || select.CompareTag("Structure"))
				{
					for (int i = 0; i < LevelObjects.buildables[(int)b, (int)b2].Count; i++)
					{
						if (LevelObjects.buildables[(int)b, (int)b2][i].transform == select)
						{
							LevelObjects.register(new ReunObjectRemove(LevelObjects.step, select, null, LevelObjects.buildables[(int)b, (int)b2][i].asset, select.position, select.rotation, select.localScale));
							break;
						}
					}
				}
				else
				{
					for (int j = 0; j < LevelObjects.objects[(int)b, (int)b2].Count; j++)
					{
						if (LevelObjects.objects[(int)b, (int)b2][j].transform == select)
						{
							LevelObjects.register(new ReunObjectRemove(LevelObjects.step, select, LevelObjects.objects[(int)b, (int)b2][j].asset, null, select.position, select.rotation, select.localScale));
							break;
						}
					}
				}
			}
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000DB374 File Offset: 0x000D9774
		public static ObjectAsset getAsset(Transform select)
		{
			if (select != null)
			{
				DevkitHierarchyWorldObject component = select.GetComponent<DevkitHierarchyWorldObject>();
				if (component != null && component.levelObject != null)
				{
					return component.levelObject.asset;
				}
				byte b;
				byte b2;
				if (Regions.tryGetCoordinate(select.position, out b, out b2))
				{
					for (int i = 0; i < LevelObjects.objects[(int)b, (int)b2].Count; i++)
					{
						if (LevelObjects.objects[(int)b, (int)b2][i].transform == select)
						{
							return LevelObjects.objects[(int)b, (int)b2][i].asset;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000DB428 File Offset: 0x000D9828
		public static void getAssetEditor(Transform select, out ObjectAsset objectAsset, out ItemAsset itemAsset)
		{
			objectAsset = null;
			itemAsset = null;
			if (select == null)
			{
				return;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(select.position, out b, out b2))
			{
				if (select.CompareTag("Barricade") || select.CompareTag("Structure"))
				{
					for (int i = 0; i < LevelObjects.buildables[(int)b, (int)b2].Count; i++)
					{
						if (LevelObjects.buildables[(int)b, (int)b2][i].transform == select)
						{
							itemAsset = LevelObjects.buildables[(int)b, (int)b2][i].asset;
							return;
						}
					}
				}
				else
				{
					for (int j = 0; j < LevelObjects.objects[(int)b, (int)b2].Count; j++)
					{
						if (LevelObjects.objects[(int)b, (int)b2][j].transform == select)
						{
							objectAsset = LevelObjects.objects[(int)b, (int)b2][j].asset;
							return;
						}
					}
				}
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x000DB540 File Offset: 0x000D9940
		public static void load()
		{
			LevelObjects._models = new GameObject().transform;
			LevelObjects.models.name = "Objects";
			LevelObjects.models.parent = Level.level;
			LevelObjects.models.tag = "Logic";
			LevelObjects.models.gameObject.layer = LayerMasks.LOGIC;
			LevelObjects._objects = new List<LevelObject>[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelObjects._buildables = new List<LevelBuildableObject>[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelObjects._total = 0;
			LevelObjects._regions = new bool[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelObjects._loads = new int[(int)Regions.WORLD_SIZE, (int)Regions.WORLD_SIZE];
			LevelObjects.shouldInstantlyLoad = true;
			LevelObjects.isHierarchyReady = false;
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					LevelObjects.loads[(int)b, (int)b2] = -1;
				}
			}
			for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
			{
				for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
				{
					LevelObjects.objects[(int)b3, (int)b4] = new List<LevelObject>();
					LevelObjects.buildables[(int)b3, (int)b4] = new List<LevelBuildableObject>();
				}
			}
			if (Level.info.configData.Use_Legacy_Objects)
			{
				if (ReadWrite.fileExists(Level.info.path + "/Level/Objects.dat", false, false))
				{
					River river = new River(Level.info.path + "/Level/Objects.dat", false);
					byte b5 = river.readByte();
					bool flag = true;
					if (b5 > 0)
					{
						if (b5 > 1 && b5 < 3)
						{
							river.readSteamID();
						}
						if (b5 > 8)
						{
							LevelObjects.availableInstanceID = river.readUInt32();
						}
						else
						{
							LevelObjects.availableInstanceID = 1u;
						}
						for (byte b6 = 0; b6 < Regions.WORLD_SIZE; b6 += 1)
						{
							for (byte b7 = 0; b7 < Regions.WORLD_SIZE; b7 += 1)
							{
								ushort num = river.readUInt16();
								for (ushort num2 = 0; num2 < num; num2 += 1)
								{
									Vector3 vector = river.readSingleVector3();
									Quaternion newRotation = river.readSingleQuaternion();
									Vector3 newScale;
									if (b5 > 3)
									{
										newScale = river.readSingleVector3();
									}
									else
									{
										newScale = Vector3.one;
									}
									ushort num3 = river.readUInt16();
									string newName = string.Empty;
									if (b5 > 5)
									{
										newName = river.readString();
									}
									Guid newGUID = Guid.Empty;
									if (b5 > 7)
									{
										newGUID = river.readGUID();
									}
									ELevelObjectPlacementOrigin newPlacementOrigin = ELevelObjectPlacementOrigin.MANUAL;
									if (b5 > 6)
									{
										newPlacementOrigin = (ELevelObjectPlacementOrigin)river.readByte();
									}
									uint newInstanceID;
									if (b5 > 8)
									{
										newInstanceID = river.readUInt32();
									}
									else
									{
										newInstanceID = LevelObjects.generateUniqueInstanceID();
									}
									if (num3 != 0)
									{
										LevelObject levelObject = new LevelObject(vector, newRotation, newScale, num3, newName, newGUID, newPlacementOrigin, newInstanceID);
										if (levelObject.asset == null)
										{
											flag = false;
										}
										if (Level.isEditor)
										{
											byte b8;
											byte b9;
											Regions.tryGetCoordinate(vector, out b8, out b9);
											if (b8 != b6 || b9 != b7)
											{
												Debug.LogError(string.Concat(new object[]
												{
													num3,
													" should be in ",
													b8,
													", ",
													b9,
													" but was in ",
													b6,
													", ",
													b7,
													"!"
												}));
											}
											LevelObjects.objects[(int)b8, (int)b9].Add(levelObject);
										}
										else
										{
											LevelObjects.objects[(int)b6, (int)b7].Add(levelObject);
										}
										LevelObjects._total++;
									}
								}
							}
						}
					}
					if (flag)
					{
						LevelObjects._hash = river.getHash();
					}
					else
					{
						LevelObjects._hash = new byte[20];
					}
					river.closeRiver();
				}
				else
				{
					for (byte b10 = 0; b10 < Regions.WORLD_SIZE; b10 += 1)
					{
						for (byte b11 = 0; b11 < Regions.WORLD_SIZE; b11 += 1)
						{
							if (ReadWrite.fileExists(string.Concat(new object[]
							{
								Level.info.path,
								"/Objects/Objects_",
								b10,
								"_",
								b11,
								".dat"
							}), false, false))
							{
								River river2 = new River(string.Concat(new object[]
								{
									Level.info.path,
									"/Objects/Objects_",
									b10,
									"_",
									b11,
									".dat"
								}), false);
								byte b12 = river2.readByte();
								if (b12 > 0)
								{
									ushort num4 = river2.readUInt16();
									for (ushort num5 = 0; num5 < num4; num5 += 1)
									{
										Vector3 position = river2.readSingleVector3();
										Quaternion rotation = river2.readSingleQuaternion();
										ushort num6 = river2.readUInt16();
										string empty = string.Empty;
										Guid empty2 = Guid.Empty;
										ELevelObjectPlacementOrigin placementOrigin = ELevelObjectPlacementOrigin.MANUAL;
										if (num6 != 0)
										{
											LevelObjects.addObject(position, rotation, Vector3.one, num6, empty, empty2, placementOrigin);
										}
									}
								}
								river2.closeRiver();
							}
						}
					}
					LevelObjects._hash = new byte[20];
				}
			}
			else
			{
				LevelObjects._hash = new byte[20];
			}
			if ((Provider.isServer || Level.isEditor) && ReadWrite.fileExists(Level.info.path + "/Level/Buildables.dat", false, false))
			{
				River river3 = new River(Level.info.path + "/Level/Buildables.dat", false);
				river3.readByte();
				for (byte b13 = 0; b13 < Regions.WORLD_SIZE; b13 += 1)
				{
					for (byte b14 = 0; b14 < Regions.WORLD_SIZE; b14 += 1)
					{
						ushort num7 = river3.readUInt16();
						for (ushort num8 = 0; num8 < num7; num8 += 1)
						{
							Vector3 vector2 = river3.readSingleVector3();
							Quaternion newRotation2 = river3.readSingleQuaternion();
							ushort num9 = river3.readUInt16();
							if (num9 != 0)
							{
								LevelBuildableObject item = new LevelBuildableObject(vector2, newRotation2, num9);
								if (Level.isEditor)
								{
									byte b15;
									byte b16;
									Regions.tryGetCoordinate(vector2, out b15, out b16);
									if (b15 != b13 || b16 != b14)
									{
										Debug.LogError(string.Concat(new object[]
										{
											num9,
											" should be in ",
											b15,
											", ",
											b16,
											" but was in ",
											b13,
											", ",
											b14,
											"!"
										}));
									}
									LevelObjects.buildables[(int)b15, (int)b16].Add(item);
								}
								else
								{
									LevelObjects.buildables[(int)b13, (int)b14].Add(item);
								}
								LevelObjects._total++;
							}
						}
					}
				}
				river3.closeRiver();
			}
			if (Level.info.configData.Use_Legacy_Objects && !Dedicator.isDedicated && !Level.isEditor)
			{
				for (byte b17 = 0; b17 < Regions.WORLD_SIZE; b17 += 1)
				{
					for (byte b18 = 0; b18 < Regions.WORLD_SIZE; b18 += 1)
					{
						for (int i = 0; i < LevelObjects.objects[(int)b17, (int)b18].Count; i++)
						{
							LevelObject levelObject2 = LevelObjects.objects[(int)b17, (int)b18][i];
							if (levelObject2.asset != null && !(levelObject2.transform == null))
							{
								if (levelObject2.asset.lod != EObjectLOD.NONE)
								{
									ObjectsLOD objectsLOD = levelObject2.transform.gameObject.AddComponent<ObjectsLOD>();
									objectsLOD.lod = levelObject2.asset.lod;
									objectsLOD.bias = levelObject2.asset.lodBias;
									objectsLOD.center = levelObject2.asset.lodCenter;
									objectsLOD.size = levelObject2.asset.lodSize;
									objectsLOD.calculateBounds();
								}
							}
						}
					}
				}
			}
			if (Level.isEditor)
			{
				LevelObjects.reun = new IReun[256];
				LevelObjects.step = 0;
				LevelObjects.frame = 0;
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000DBDC4 File Offset: 0x000DA1C4
		public static void save()
		{
			if (Level.info.configData.Use_Legacy_Objects)
			{
				River river = new River(Level.info.path + "/Level/Objects.dat", false);
				river.writeByte(LevelObjects.SAVEDATA_VERSION);
				river.writeUInt32(LevelObjects.availableInstanceID);
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
						river.writeUInt16((ushort)list.Count);
						ushort num = 0;
						while ((int)num < list.Count)
						{
							LevelObject levelObject = list[(int)num];
							if (levelObject != null && levelObject.transform != null && levelObject.id != 0)
							{
								river.writeSingleVector3(levelObject.transform.position);
								river.writeSingleQuaternion(levelObject.transform.rotation);
								river.writeSingleVector3(levelObject.transform.localScale);
								river.writeUInt16(levelObject.id);
								river.writeString(levelObject.name);
								river.writeGUID(levelObject.GUID);
								river.writeByte((byte)levelObject.placementOrigin);
								river.writeUInt32(levelObject.instanceID);
							}
							else
							{
								river.writeSingleVector3(Vector3.zero);
								river.writeSingleQuaternion(Quaternion.identity);
								river.writeSingleVector3(Vector3.one);
								river.writeUInt16(0);
								river.writeString(string.Empty);
								river.writeGUID(Guid.Empty);
								river.writeByte(0);
								river.writeUInt32(0u);
								Debug.LogError(string.Concat(new object[]
								{
									"Found invalid object at ",
									b,
									", ",
									b2,
									" with model: ",
									levelObject.transform,
									" and ID: ",
									levelObject.id
								}));
							}
							num += 1;
						}
					}
				}
				river.closeRiver();
			}
			River river2 = new River(Level.info.path + "/Level/Buildables.dat", false);
			river2.writeByte(LevelObjects.SAVEDATA_VERSION);
			for (byte b3 = 0; b3 < Regions.WORLD_SIZE; b3 += 1)
			{
				for (byte b4 = 0; b4 < Regions.WORLD_SIZE; b4 += 1)
				{
					List<LevelBuildableObject> list2 = LevelObjects.buildables[(int)b3, (int)b4];
					river2.writeUInt16((ushort)list2.Count);
					ushort num2 = 0;
					while ((int)num2 < list2.Count)
					{
						LevelBuildableObject levelBuildableObject = list2[(int)num2];
						if (levelBuildableObject != null && levelBuildableObject.transform != null && levelBuildableObject.id != 0)
						{
							river2.writeSingleVector3(levelBuildableObject.transform.position);
							river2.writeSingleQuaternion(levelBuildableObject.transform.rotation);
							river2.writeUInt16(levelBuildableObject.id);
						}
						else
						{
							river2.writeSingleVector3(Vector3.zero);
							river2.writeSingleQuaternion(Quaternion.identity);
							river2.writeUInt16(0);
							Debug.LogError(string.Concat(new object[]
							{
								"Found invalid object at ",
								b3,
								", ",
								b4,
								" with model: ",
								levelBuildableObject.transform,
								" and ID: ",
								levelBuildableObject.id
							}));
						}
						num2 += 1;
					}
				}
			}
			river2.closeRiver();
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000DC15C File Offset: 0x000DA55C
		private static void onRegionUpdated(byte old_x, byte old_y, byte new_x, byte new_y)
		{
			bool flag = true;
			LevelObjects.onRegionUpdated(null, old_x, old_y, new_x, new_y, 0, ref flag);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000DC178 File Offset: 0x000DA578
		private static void onPlayerTeleported(Player player, Vector3 position)
		{
			LevelObjects.shouldInstantlyLoad = true;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000DC180 File Offset: 0x000DA580
		private static void onRegionUpdated(Player player, byte old_x, byte old_y, byte new_x, byte new_y, byte step, ref bool canIncrementIndex)
		{
			if (step != 0)
			{
				return;
			}
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					if (LevelObjects.regions[(int)b, (int)b2] && !Regions.checkArea(b, b2, new_x, new_y, LevelObjects.OBJECT_REGIONS))
					{
						LevelObjects.regions[(int)b, (int)b2] = false;
						if (LevelObjects.shouldInstantlyLoad)
						{
							List<LevelObject> list = LevelObjects.objects[(int)b, (int)b2];
							for (int i = 0; i < list.Count; i++)
							{
								list[i].disableCollision();
								if (!list[i].isSpeciallyCulled)
								{
									list[i].disableVisual();
									if (list[i].isLandmarkQualityMet)
									{
										list[i].enableSkybox();
									}
								}
							}
						}
						else
						{
							LevelObjects.loads[(int)b, (int)b2] = 0;
						}
						if (Level.isEditor)
						{
							List<LevelBuildableObject> list2 = LevelObjects.buildables[(int)b, (int)b2];
							for (int j = 0; j < list2.Count; j++)
							{
								list2[j].disable();
							}
						}
					}
				}
			}
			if (Regions.checkSafe((int)new_x, (int)new_y))
			{
				for (int k = (int)(new_x - LevelObjects.OBJECT_REGIONS); k <= (int)(new_x + LevelObjects.OBJECT_REGIONS); k++)
				{
					for (int l = (int)(new_y - LevelObjects.OBJECT_REGIONS); l <= (int)(new_y + LevelObjects.OBJECT_REGIONS); l++)
					{
						if (Regions.checkSafe((int)((byte)k), (int)((byte)l)) && !LevelObjects.regions[k, l])
						{
							LevelObjects.regions[k, l] = true;
							if (LevelObjects.shouldInstantlyLoad)
							{
								List<LevelObject> list3 = LevelObjects.objects[k, l];
								for (int m = 0; m < list3.Count; m++)
								{
									list3[m].enableCollision();
									if (!list3[m].isSpeciallyCulled)
									{
										list3[m].enableVisual();
										list3[m].disableSkybox();
									}
								}
							}
							else
							{
								LevelObjects.loads[k, l] = 0;
							}
							if (Level.isEditor)
							{
								List<LevelBuildableObject> list4 = LevelObjects.buildables[k, l];
								for (int n = 0; n < list4.Count; n++)
								{
									list4[n].enable();
								}
							}
						}
					}
				}
			}
			Level.isLoadingArea = false;
			LevelObjects.shouldInstantlyLoad = false;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000DC424 File Offset: 0x000DA824
		private static void onPlayerCreated(Player player)
		{
			if (player.channel.isOwner)
			{
				Player player2 = Player.player;
				Delegate onPlayerTeleported = player2.onPlayerTeleported;
				if (LevelObjects.<>f__mg$cache0 == null)
				{
					LevelObjects.<>f__mg$cache0 = new PlayerTeleported(LevelObjects.onPlayerTeleported);
				}
				player2.onPlayerTeleported = (PlayerTeleported)Delegate.Combine(onPlayerTeleported, LevelObjects.<>f__mg$cache0);
				PlayerMovement movement = Player.player.movement;
				Delegate onRegionUpdated = movement.onRegionUpdated;
				if (LevelObjects.<>f__mg$cache1 == null)
				{
					LevelObjects.<>f__mg$cache1 = new PlayerRegionUpdated(LevelObjects.onRegionUpdated);
				}
				movement.onRegionUpdated = (PlayerRegionUpdated)Delegate.Combine(onRegionUpdated, LevelObjects.<>f__mg$cache1);
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000DC4B4 File Offset: 0x000DA8B4
		private static void handleEditorAreaRegistered(EditorArea area)
		{
			Delegate onRegionUpdated = area.onRegionUpdated;
			if (LevelObjects.<>f__mg$cache2 == null)
			{
				LevelObjects.<>f__mg$cache2 = new EditorRegionUpdated(LevelObjects.onRegionUpdated);
			}
			area.onRegionUpdated = (EditorRegionUpdated)Delegate.Combine(onRegionUpdated, LevelObjects.<>f__mg$cache2);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000DC4EC File Offset: 0x000DA8EC
		private static void handleLevelHierarchyReady()
		{
			if (!Level.info.configData.Use_Legacy_Objects && !Dedicator.isDedicated && !Level.isEditor)
			{
				for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
				{
					for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
					{
						for (int i = 0; i < LevelObjects.objects[(int)b, (int)b2].Count; i++)
						{
							LevelObject levelObject = LevelObjects.objects[(int)b, (int)b2][i];
							if (levelObject.asset != null && !(levelObject.transform == null))
							{
								if (levelObject.asset.lod != EObjectLOD.NONE)
								{
									ObjectsLOD objectsLOD = levelObject.transform.gameObject.AddComponent<ObjectsLOD>();
									objectsLOD.lod = levelObject.asset.lod;
									objectsLOD.bias = levelObject.asset.lodBias;
									objectsLOD.center = levelObject.asset.lodCenter;
									objectsLOD.size = levelObject.asset.lodSize;
									objectsLOD.calculateBounds();
								}
							}
						}
					}
				}
			}
			LevelObjects.isHierarchyReady = true;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000DC61C File Offset: 0x000DAA1C
		private void Update()
		{
			if (!Level.isLoaded)
			{
				return;
			}
			if (Dedicator.isDedicated)
			{
				return;
			}
			if (LevelObjects.loads == null || LevelObjects.regions == null || LevelObjects.objects == null)
			{
				return;
			}
			if (!LevelObjects.isHierarchyReady)
			{
				return;
			}
			for (byte b = 0; b < Regions.WORLD_SIZE; b += 1)
			{
				for (byte b2 = 0; b2 < Regions.WORLD_SIZE; b2 += 1)
				{
					if (LevelObjects.loads[(int)b, (int)b2] != -1)
					{
						if (LevelObjects.loads[(int)b, (int)b2] >= LevelObjects.objects[(int)b, (int)b2].Count)
						{
							LevelObjects.loads[(int)b, (int)b2] = -1;
							if (LevelObjects.onRegionActivated != null)
							{
								LevelObjects.onRegionActivated(b, b2);
							}
						}
						else
						{
							if (LevelObjects.regions[(int)b, (int)b2])
							{
								if (!LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isCollisionEnabled)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].enableCollision();
								}
								if (!LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isVisualEnabled && !LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isSpeciallyCulled)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].enableVisual();
								}
								if (LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isSkyboxEnabled)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].disableSkybox();
								}
							}
							else
							{
								if (LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isCollisionEnabled)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].disableCollision();
								}
								if (LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isVisualEnabled && !LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isSpeciallyCulled)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].disableVisual();
								}
								if (!LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isSkyboxEnabled && LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].isLandmarkQualityMet)
								{
									LevelObjects.objects[(int)b, (int)b2][LevelObjects.loads[(int)b, (int)b2]].enableSkybox();
								}
							}
							LevelObjects.loads[(int)b, (int)b2]++;
						}
					}
				}
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000DC94C File Offset: 0x000DAD4C
		public void Start()
		{
			Delegate onPlayerCreated = Player.onPlayerCreated;
			if (LevelObjects.<>f__mg$cache3 == null)
			{
				LevelObjects.<>f__mg$cache3 = new PlayerCreated(LevelObjects.onPlayerCreated);
			}
			Player.onPlayerCreated = (PlayerCreated)Delegate.Combine(onPlayerCreated, LevelObjects.<>f__mg$cache3);
			if (LevelObjects.<>f__mg$cache4 == null)
			{
				LevelObjects.<>f__mg$cache4 = new EditorAreaRegisteredHandler(LevelObjects.handleEditorAreaRegistered);
			}
			EditorArea.registered += LevelObjects.<>f__mg$cache4;
			if (LevelObjects.<>f__mg$cache5 == null)
			{
				LevelObjects.<>f__mg$cache5 = new LevelHierarchyReady(LevelObjects.handleLevelHierarchyReady);
			}
			LevelHierarchy.ready += LevelObjects.<>f__mg$cache5;
		}

		// Token: 0x04001772 RID: 6002
		public static readonly byte SAVEDATA_VERSION = 9;

		// Token: 0x04001773 RID: 6003
		public static readonly byte OBJECT_REGIONS = 3;

		// Token: 0x04001774 RID: 6004
		private static uint availableInstanceID;

		// Token: 0x04001775 RID: 6005
		private static IReun[] reun;

		// Token: 0x04001776 RID: 6006
		public static int step;

		// Token: 0x04001777 RID: 6007
		private static int frame;

		// Token: 0x04001778 RID: 6008
		private static Transform _models;

		// Token: 0x04001779 RID: 6009
		private static List<LevelObject>[,] _objects;

		// Token: 0x0400177A RID: 6010
		private static List<LevelBuildableObject>[,] _buildables;

		// Token: 0x0400177B RID: 6011
		private static int _total;

		// Token: 0x0400177C RID: 6012
		private static byte[] _hash;

		// Token: 0x0400177D RID: 6013
		private static bool[,] _regions;

		// Token: 0x0400177E RID: 6014
		private static bool isHierarchyReady;

		// Token: 0x0400177F RID: 6015
		private static int[,] _loads;

		// Token: 0x04001780 RID: 6016
		public static RegionActivated onRegionActivated;

		// Token: 0x04001782 RID: 6018
		[CompilerGenerated]
		private static PlayerTeleported <>f__mg$cache0;

		// Token: 0x04001783 RID: 6019
		[CompilerGenerated]
		private static PlayerRegionUpdated <>f__mg$cache1;

		// Token: 0x04001784 RID: 6020
		[CompilerGenerated]
		private static EditorRegionUpdated <>f__mg$cache2;

		// Token: 0x04001785 RID: 6021
		[CompilerGenerated]
		private static PlayerCreated <>f__mg$cache3;

		// Token: 0x04001786 RID: 6022
		[CompilerGenerated]
		private static EditorAreaRegisteredHandler <>f__mg$cache4;

		// Token: 0x04001787 RID: 6023
		[CompilerGenerated]
		private static LevelHierarchyReady <>f__mg$cache5;
	}
}
