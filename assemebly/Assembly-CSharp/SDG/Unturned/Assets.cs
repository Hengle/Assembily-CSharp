using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SDG.Framework.Devkit;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDG.Unturned
{
	// Token: 0x0200038C RID: 908
	public class Assets : MonoBehaviour
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x0008D396 File Offset: 0x0008B796
		public static int assetsToLoadPerStep
		{
			get
			{
				return (!LoadingUI.isDonePlayingSplashscreen) ? 1 : Assets.ASSETS_PER_STEP;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0008D3AD File Offset: 0x0008B7AD
		public static TypeRegistryDictionary assetTypes
		{
			get
			{
				return Assets._assetTypes;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x0008D3B4 File Offset: 0x0008B7B4
		public static TypeRegistryDictionary useableTypes
		{
			get
			{
				return Assets._useableTypes;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x0008D3BB File Offset: 0x0008B7BB
		public static bool isLoading
		{
			get
			{
				return Assets._isLoading;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x0008D3C2 File Offset: 0x0008B7C2
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x0008D3C9 File Offset: 0x0008B7C9
		public static List<RootAssetDirectory> rootAssetDirectories { get; protected set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0008D3D1 File Offset: 0x0008B7D1
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x0008D3D8 File Offset: 0x0008B7D8
		public static Dictionary<string, RootContentDirectory> rootContentDirectories { get; protected set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x0008D3E0 File Offset: 0x0008B7E0
		public static List<string> errors
		{
			get
			{
				return Assets._errors;
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0008D3E8 File Offset: 0x0008B7E8
		public static void rename(Asset asset, string newName)
		{
			if (string.IsNullOrEmpty(newName))
			{
				return;
			}
			if (asset.name == newName)
			{
				return;
			}
			string filePath = asset.getFilePath();
			asset.name = newName;
			string filePath2 = asset.getFilePath();
			if (!File.Exists(filePath))
			{
				return;
			}
			File.Move(filePath, filePath2);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0008D43C File Offset: 0x0008B83C
		public static Asset find(EAssetType type, ushort id)
		{
			if (type == EAssetType.NONE)
			{
				return null;
			}
			if (id == 0)
			{
				return null;
			}
			Asset result = null;
			Assets.assets[type].TryGetValue(id, out result);
			return result;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0008D470 File Offset: 0x0008B870
		public static Asset find(EAssetType type, string name)
		{
			if (type == EAssetType.NONE)
			{
				return null;
			}
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			Asset result = null;
			Assets.namedAssets[type].TryGetValue(name.ToLower(), out result);
			return result;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x0008D4B0 File Offset: 0x0008B8B0
		public static T find<T>(AssetReference<T> reference) where T : Asset
		{
			if (!reference.isValid)
			{
				return (T)((object)null);
			}
			Asset asset = Assets.find(reference.GUID);
			return asset as T;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0008D4E8 File Offset: 0x0008B8E8
		public static ContentFile find<T>(ContentReference<T> reference) where T : UnityEngine.Object
		{
			if (!reference.isValid)
			{
				return null;
			}
			reference.path = reference.path.ToLower();
			RootContentDirectory rootContentDirectory;
			if (Assets.rootContentDirectories.TryGetValue(reference.name, out rootContentDirectory))
			{
				ContentDirectory contentDirectory = rootContentDirectory;
				string[] array = reference.path.Split(new char[]
				{
					'/'
				});
				for (int i = 0; i < array.Length; i++)
				{
					if (i == array.Length - 1)
					{
						foreach (ContentFile contentFile in contentDirectory.files)
						{
							if (reference.isReferenceTo(contentFile))
							{
								return contentFile;
							}
						}
						return null;
					}
					string key = array[i];
					ContentDirectory contentDirectory2;
					if (!contentDirectory.directories.TryGetValue(key, out contentDirectory2))
					{
						return null;
					}
					contentDirectory = contentDirectory2;
				}
				return null;
			}
			return null;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0008D5F0 File Offset: 0x0008B9F0
		public static T load<T>(ContentReference<T> reference) where T : UnityEngine.Object
		{
			if (!reference.isValid)
			{
				return (T)((object)null);
			}
			if (Application.isEditor && reference.name == "core.content")
			{
				string path = reference.path.Substring(17, reference.path.LastIndexOf('.') - 17);
				return Resources.Load<T>(path);
			}
			RootContentDirectory rootContentDirectory;
			if (Assets.rootContentDirectories.TryGetValue(reference.name, out rootContentDirectory))
			{
				return rootContentDirectory.assetBundle.LoadAsset<T>(reference.path);
			}
			return (T)((object)null);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0008D688 File Offset: 0x0008BA88
		public static Asset find(Guid GUID)
		{
			Asset result;
			Assets.assetDictionary.TryGetValue(GUID, out result);
			return result;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0008D6A4 File Offset: 0x0008BAA4
		public static Asset[] find(EAssetType type)
		{
			if (type == EAssetType.NONE)
			{
				return null;
			}
			if (type == EAssetType.OBJECT)
			{
				Asset[] array = new Asset[Assets.namedAssets[type].Values.Count];
				int num = 0;
				foreach (KeyValuePair<string, Asset> keyValuePair in Assets.namedAssets[type])
				{
					array[num] = keyValuePair.Value;
					num++;
				}
				return array;
			}
			Asset[] array2 = new Asset[Assets.assets[type].Values.Count];
			int num2 = 0;
			foreach (KeyValuePair<ushort, Asset> keyValuePair2 in Assets.assets[type])
			{
				array2[num2] = keyValuePair2.Value;
				num2++;
			}
			return array2;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0008D7B8 File Offset: 0x0008BBB8
		public static void find<T>(List<T> results) where T : Asset
		{
			for (int i = 0; i < Assets.assetList.Count; i++)
			{
				Asset asset = Assets.assetList[i];
				if (asset != null)
				{
					if (typeof(Asset).IsAssignableFrom(asset.GetType()))
					{
						T t = asset as T;
						if (t != null)
						{
							results.Add(t);
						}
					}
				}
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0008D834 File Offset: 0x0008BC34
		public static void runtimeCreate(Type type, AssetDirectory directory)
		{
			try
			{
				Asset asset = Activator.CreateInstance(type) as Asset;
				if (asset != null)
				{
					asset.GUID = Guid.NewGuid();
					Assets.add(asset, false);
					asset.directory = directory;
					directory.assets.Add(asset);
					if (asset is IDevkitAssetSpawnable)
					{
						(asset as IDevkitAssetSpawnable).devkitAssetSpawn();
					}
					if (asset != null)
					{
						((IDirtyable)asset).isDirty = true;
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0008D8BC File Offset: 0x0008BCBC
		public static void add(Asset asset, bool overrideExistingID)
		{
			if (asset == null)
			{
				return;
			}
			overrideExistingID = true;
			EAssetType assetCategory = asset.assetCategory;
			if (assetCategory == EAssetType.OBJECT)
			{
				if (overrideExistingID)
				{
					Assets.assets[assetCategory].Remove(asset.id);
					Assets.assets[assetCategory].Add(asset.id, asset);
				}
				else if (!Assets.assets[assetCategory].ContainsKey(asset.id))
				{
					Assets.assets[assetCategory].Add(asset.id, asset);
				}
				string key = asset.name.ToLower();
				if (Assets.namedAssets[assetCategory].ContainsKey(key))
				{
					Asset asset2;
					Assets.namedAssets[assetCategory].TryGetValue(key, out asset2);
					Assets.errors.Add(string.Concat(new object[]
					{
						"The name ",
						asset.name,
						" for ",
						asset.id,
						" is already taken by ",
						asset2.id,
						"!"
					}));
					return;
				}
				Assets.namedAssets[assetCategory].Add(key, asset);
			}
			else if (assetCategory != EAssetType.NONE)
			{
				if (overrideExistingID)
				{
					Assets.assets[assetCategory].Remove(asset.id);
				}
				else if (Assets.assets[assetCategory].ContainsKey(asset.id))
				{
					Asset asset3;
					Assets.assets[assetCategory].TryGetValue(asset.id, out asset3);
					Assets.errors.Add(string.Concat(new object[]
					{
						"The ID ",
						asset.id,
						" for ",
						asset.name,
						" is already taken by ",
						asset3.name,
						"!"
					}));
					return;
				}
				Assets.assets[assetCategory].Add(asset.id, asset);
			}
			if (asset.GUID != Guid.Empty)
			{
				if (Assets.assetDictionary.ContainsKey(asset.GUID))
				{
					Asset asset4;
					Assets.assetDictionary.TryGetValue(asset.GUID, out asset4);
					Assets.errors.Add(string.Concat(new string[]
					{
						"The GUID ",
						asset.GUID.ToString("N"),
						" for ",
						asset.name,
						" is somehow already taken by ",
						asset4.name,
						"! Have we reached the heat death of the universe?"
					}));
					asset.GUID = Guid.NewGuid();
					if (asset != null)
					{
						((IDirtyable)asset).isDirty = true;
					}
				}
				Assets.assetDictionary.Add(asset.GUID, asset);
				Assets.assetList.Add(asset);
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0008DB8D File Offset: 0x0008BF8D
		public static void remove(Asset asset)
		{
			if (asset.GUID != Guid.Empty)
			{
				Assets.assetDictionary.Remove(asset.GUID);
				Assets.assetList.RemoveFast(asset);
			}
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0008DBC0 File Offset: 0x0008BFC0
		public static void refresh()
		{
			Assets.asset.StartCoroutine("init");
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0008DBD4 File Offset: 0x0008BFD4
		private static void scanFolder(string path, bool usePath, bool loadFromResources, bool canUse, AssetDirectory directory, EAssetOrigin origin, bool overrideExistingIDs)
		{
			string fileName = Path.GetFileName(path);
			if (ReadWrite.fileExists(path + "/" + fileName + ".asset", false, usePath))
			{
				Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, string.Concat(new string[]
				{
					(!usePath) ? string.Empty : ReadWrite.PATH,
					path,
					"/",
					fileName,
					".asset"
				}), path, path, usePath, usePath, loadFromResources, canUse, directory, origin, overrideExistingIDs));
			}
			else if (ReadWrite.fileExists(path + "/" + fileName + ".dat", false, usePath))
			{
				Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, path, path, usePath, usePath, loadFromResources, canUse, directory, origin, overrideExistingIDs));
			}
			else if (ReadWrite.fileExists(path + "/Asset.dat", false, usePath))
			{
				Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, path, path, usePath, usePath, loadFromResources, canUse, directory, origin, overrideExistingIDs));
			}
			else
			{
				string text = path;
				if (usePath)
				{
					text = ReadWrite.PATH + text;
				}
				string[] files = Directory.GetFiles(text, "*.asset");
				for (int i = 0; i < files.Length; i++)
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(Path.GetFileNameWithoutExtension(files[i]), files[i], path, path, usePath, usePath, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
			}
			string[] folders = ReadWrite.getFolders(path, usePath);
			for (int j = 0; j < folders.Length; j++)
			{
				string fileName2 = Path.GetFileName(folders[j]);
				AssetDirectory assetDirectory = new AssetDirectory(fileName2, directory);
				directory.directories.Add(assetDirectory);
				Assets.scanFolder(path + "/" + fileName2, usePath, loadFromResources, canUse, assetDirectory, origin, overrideExistingIDs);
			}
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0008DD98 File Offset: 0x0008C198
		private static void scanFolder(string root, string path, bool loadFromResources, bool canUse, AssetDirectory directory, EAssetOrigin origin, bool overrideExistingIDs)
		{
			string text = root + path;
			string fileName = Path.GetFileName(path);
			if (ReadWrite.fileExists(text + "/" + fileName + ".asset", false, false))
			{
				if (!loadFromResources && ReadWrite.fileExists(text + "/" + fileName + ".unity3d", false, false))
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, text + "/" + fileName + ".asset", text, text, false, false, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
				else
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, text + "/" + fileName + ".asset", text, path, false, true, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
			}
			else if (ReadWrite.fileExists(text + "/" + fileName + ".dat", false, false))
			{
				if (!loadFromResources && ReadWrite.fileExists(text + "/" + fileName + ".unity3d", false, false))
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, text, text, false, false, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
				else
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, text, path, false, true, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
			}
			else if (ReadWrite.fileExists(text + "/Asset.dat", false, false))
			{
				if (!loadFromResources && ReadWrite.fileExists(text + "/" + fileName + ".unity3d", false, false))
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, text, text, false, false, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
				else
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(fileName, null, text, path, false, true, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
			}
			else
			{
				string[] files = Directory.GetFiles(text, "*.asset");
				for (int i = 0; i < files.Length; i++)
				{
					Assets.filesScanned.Enqueue(new ScannedFileInfo(Path.GetFileNameWithoutExtension(files[i]), files[i], path, path, false, false, loadFromResources, canUse, directory, origin, overrideExistingIDs));
				}
			}
			string[] folders = ReadWrite.getFolders(text, false);
			for (int j = 0; j < folders.Length; j++)
			{
				string fileName2 = Path.GetFileName(folders[j]);
				AssetDirectory assetDirectory = new AssetDirectory(fileName2, directory);
				directory.directories.Add(assetDirectory);
				Assets.scanFolder(root, path + "/" + Path.GetFileName(folders[j]), loadFromResources, canUse, assetDirectory, origin, overrideExistingIDs);
			}
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0008E00C File Offset: 0x0008C40C
		private static void loadFile(ScannedFileInfo file)
		{
			if (!string.IsNullOrEmpty(file.assetPath))
			{
				using (StreamReader streamReader = new StreamReader(file.assetPath))
				{
					IFormattedFileReader formattedFileReader = new KeyValueTableReader(streamReader);
					IFormattedFileReader formattedFileReader2 = formattedFileReader.readObject("Metadata");
					Guid guid = formattedFileReader2.readValue<Guid>("GUID");
					Type type = formattedFileReader2.readValue<Type>("Type");
					Bundle bundle = new Bundle(file.bundlePath + "/" + file.name + ".unity3d", file.bundleUsePath, file.loadFromResources);
					Local local = Localization.tryRead(file.dataPath, file.dataUsePath);
					try
					{
						byte[] array = Hash.SHA1(streamReader.BaseStream);
						Asset asset = Activator.CreateInstance(type, new object[]
						{
							bundle,
							local,
							array
						}) as Asset;
						if (asset != null)
						{
							asset.GUID = guid;
							asset.assetOrigin = file.origin;
							asset.absoluteOriginFilePath = file.assetPath;
							asset.canUse = file.canUse;
							formattedFileReader.readKey("Asset");
							asset.read(formattedFileReader);
							Assets.add(asset, file.overrideExistingIDs);
							asset.directory = file.directory;
							file.directory.assets.Add(asset);
						}
						else
						{
							Assets.errors.Add(string.Concat(new object[]
							{
								"Failed to instantiate ",
								file.name,
								".asset of type ",
								type,
								"!"
							}));
						}
					}
					catch (Exception ex)
					{
						Assets.errors.Add("Failed to analyze " + file.name + ".asset: " + ex.Message);
						Debug.LogError("Failed to analyze " + file.name + ".asset!");
						Debug.LogException(ex);
						bundle.unload();
					}
				}
			}
			else
			{
				Data data = null;
				string text = file.dataPath + "/" + file.name + ".dat";
				try
				{
					if (ReadWrite.fileExists(text, false, file.dataUsePath))
					{
						data = ReadWrite.readData(text, false, file.dataUsePath);
					}
					else
					{
						text = file.dataPath + "/Asset.dat";
						data = ReadWrite.readData(text, false, file.dataUsePath);
					}
					if (data.hasError)
					{
						Assets.errors.Add("Errored on import " + file.name + ".dat!");
					}
				}
				catch (Exception ex2)
				{
					Assets.errors.Add("Failed to import " + file.name + ".dat: " + ex2.Message);
					Debug.LogError("Failed to import " + file.name + ".dat!");
					Debug.LogException(ex2);
					return;
				}
				byte b = data.readByte("Asset_Bundle_Version");
				if (b < 1)
				{
					b = 1;
				}
				Bundle bundle2 = new Bundle(file.bundlePath + "/" + file.name + ".unity3d", file.bundleUsePath, file.loadFromResources);
				bundle2.convertShadersToStandard = (b < 2);
				Local local2 = Localization.tryRead(file.dataPath, file.dataUsePath);
				string text2 = data.readString("Type");
				if (!string.IsNullOrEmpty(text2))
				{
					Type type2 = Assets.assetTypes.getType(text2);
					if (type2 != null && typeof(Asset).IsAssignableFrom(type2))
					{
						ushort num = data.readUInt16("ID");
						try
						{
							Asset asset2 = Activator.CreateInstance(type2, new object[]
							{
								bundle2,
								data,
								local2,
								num
							}) as Asset;
							if (asset2 != null)
							{
								if (file.dataUsePath)
								{
									text = ReadWrite.PATH + text;
								}
								if (data.has("GUID"))
								{
									asset2.GUID = new Guid(data.readString("GUID"));
								}
								else
								{
									asset2.GUID = Guid.NewGuid();
									string text3 = File.ReadAllText(text);
									text3 = "GUID " + asset2.GUID.ToString("N") + "\n" + text3;
									File.WriteAllText(text, text3);
								}
								asset2.assetOrigin = file.origin;
								asset2.absoluteOriginFilePath = text;
								asset2.canUse = file.canUse;
								Assets.add(asset2, file.overrideExistingIDs);
								asset2.directory = file.directory;
								file.directory.assets.Add(asset2);
							}
						}
						catch (Exception ex3)
						{
							Assets.errors.Add("Failed to analyze " + file.name + ".dat: " + ex3.Message);
							Debug.LogError("Failed to analyze " + file.name + ".dat!");
							Debug.LogException(ex3);
							bundle2.unload();
						}
					}
					else
					{
						Assets.errors.Add(string.Concat(new string[]
						{
							"The asset type ",
							text2,
							" in ",
							file.name,
							".dat is not handled!"
						}));
						bundle2.unload();
					}
				}
				else
				{
					Assets.errors.Add(file.name + ".dat is missing an asset type!");
					bundle2.unload();
				}
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0008E5FC File Offset: 0x0008C9FC
		public static void load(string path, bool usePath, bool loadFromResources, bool canUse, EAssetOrigin origin, bool overrideExistingIDs)
		{
			string text = path;
			if (usePath)
			{
				text = ReadWrite.PATH + text;
			}
			RootAssetDirectory rootAssetDirectory = new RootAssetDirectory(text, Path.GetFileName(Path.GetDirectoryName(path)));
			Assets.rootAssetDirectories.Add(rootAssetDirectory);
			Assets.scanFolder(path, usePath, loadFromResources, canUse, rootAssetDirectory, origin, overrideExistingIDs);
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file = Assets.filesScanned.Dequeue();
				Assets.loadFile(file);
			}
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0008E670 File Offset: 0x0008CA70
		public static void searchForAndLoadContent(string absolutePath)
		{
			string[] files = Directory.GetFiles(absolutePath, "*.content");
			for (int i = 0; i < files.Length; i++)
			{
				AssetBundle assetBundle = AssetBundle.LoadFromFile(files[i]);
				if (!(assetBundle == null))
				{
					RootContentDirectory rootContentDirectory = new RootContentDirectory(assetBundle, assetBundle.name);
					Assets.rootContentDirectories.Add(rootContentDirectory.name, rootContentDirectory);
				}
			}
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0008E6D8 File Offset: 0x0008CAD8
		public IEnumerator init()
		{
			Assets._isLoading = true;
			if (Assets.errors == null)
			{
				Assets._errors = new List<string>();
			}
			else
			{
				Assets.errors.Clear();
			}
			Assets.assets = new Dictionary<EAssetType, Dictionary<ushort, Asset>>();
			Assets.assets.Add(EAssetType.ITEM, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.EFFECT, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.OBJECT, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.RESOURCE, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.VEHICLE, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.ANIMAL, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.MYTHIC, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.SKIN, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.SPAWN, new Dictionary<ushort, Asset>());
			Assets.assets.Add(EAssetType.NPC, new Dictionary<ushort, Asset>());
			Assets.namedAssets = new Dictionary<EAssetType, Dictionary<string, Asset>>();
			Assets.namedAssets.Add(EAssetType.ITEM, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.EFFECT, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.OBJECT, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.RESOURCE, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.VEHICLE, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.ANIMAL, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.MYTHIC, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.SKIN, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.SPAWN, new Dictionary<string, Asset>());
			Assets.namedAssets.Add(EAssetType.NPC, new Dictionary<string, Asset>());
			Assets.assetDictionary = new Dictionary<Guid, Asset>();
			Assets.assetList = new List<Asset>();
			Assets.rootAssetDirectories = new List<RootAssetDirectory>();
			if (Assets.rootContentDirectories != null)
			{
				foreach (KeyValuePair<string, RootContentDirectory> keyValuePair in Assets.rootContentDirectories)
				{
					RootContentDirectory value = keyValuePair.Value;
					value.assetBundle.Unload(false);
				}
			}
			Assets.rootContentDirectories = new Dictionary<string, RootContentDirectory>();
			Assets.filesScanned = new Queue<ScannedFileInfo>();
			while (!LoadingUI.isDoneCoreSplashscreen)
			{
				yield return null;
			}
			yield return null;
			RootAssetDirectory coreDirectory = new RootAssetDirectory(ReadWrite.PATH + "/Bundles", "Core");
			Assets.rootAssetDirectories.Add(coreDirectory);
			Assets.searchForAndLoadContent(ReadWrite.PATH + "/Content");
			AssetDirectory assetsDirectory = new AssetDirectory("Assets", coreDirectory);
			coreDirectory.directories.Add(assetsDirectory);
			Assets.scanFolder("/Bundles/Assets", true, false, true, assetsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Asset", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file = Assets.filesScanned.Dequeue();
				Assets.loadFile(file);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Asset", Assets.filesScanned.Count, 0f, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory itemsDirectory = new AssetDirectory("Items", coreDirectory);
			coreDirectory.directories.Add(itemsDirectory);
			Assets.scanFolder("/Bundles/Items", true, false, true, itemsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Item", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file2 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file2);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Item", Assets.filesScanned.Count, 1f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory effectsDirectory = new AssetDirectory("Effects", coreDirectory);
			coreDirectory.directories.Add(effectsDirectory);
			Assets.scanFolder("/Bundles/Effects", true, false, true, effectsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Effect", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file3 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file3);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Effect", Assets.filesScanned.Count, 2f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory objectsDirectory = new AssetDirectory("Objects", coreDirectory);
			coreDirectory.directories.Add(objectsDirectory);
			Assets.scanFolder("/Bundles/Objects", true, false, true, objectsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Object", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file4 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file4);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Object", Assets.filesScanned.Count, 3f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory resourcesDirectory = new AssetDirectory("Resources", coreDirectory);
			coreDirectory.directories.Add(resourcesDirectory);
			Assets.scanFolder("/Bundles/Resources", true, false, true, resourcesDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Resource", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file5 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file5);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Resource", Assets.filesScanned.Count, 4f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory vehiclesDirectory = new AssetDirectory("Vehicles", coreDirectory);
			coreDirectory.directories.Add(vehiclesDirectory);
			Assets.scanFolder("/Bundles/Vehicles", true, false, true, vehiclesDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Vehicle", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file6 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file6);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Vehicle", Assets.filesScanned.Count, 5f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory animalsDirectory = new AssetDirectory("Animals", coreDirectory);
			coreDirectory.directories.Add(animalsDirectory);
			Assets.scanFolder("/Bundles/Animals", true, false, true, animalsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Animal", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file7 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file7);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Animal", Assets.filesScanned.Count, 6f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory mythicsDirectory = new AssetDirectory("Mythics", coreDirectory);
			coreDirectory.directories.Add(mythicsDirectory);
			Assets.scanFolder("/Bundles/Mythics", true, false, true, mythicsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Mythic", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file8 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file8);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Mythic", Assets.filesScanned.Count, 7f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory skinsDirectory = new AssetDirectory("Skins", coreDirectory);
			coreDirectory.directories.Add(skinsDirectory);
			Assets.scanFolder("/Bundles/Skins", true, false, true, skinsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Skin", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file9 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file9);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Skin", Assets.filesScanned.Count, 8f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory spawnsDirectory = new AssetDirectory("Spawns", coreDirectory);
			coreDirectory.directories.Add(spawnsDirectory);
			Assets.scanFolder("/Bundles/Spawns", true, false, true, spawnsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("Spawn", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file10 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file10);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Spawn", Assets.filesScanned.Count, 9f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			AssetDirectory npcsDirectory = new AssetDirectory("NPCs", coreDirectory);
			coreDirectory.directories.Add(npcsDirectory);
			Assets.scanFolder("/Bundles/NPCs", true, false, true, npcsDirectory, EAssetOrigin.OFFICIAL, false);
			LoadingUI.assetsScan("NPC", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file11 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file11);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("NPC", Assets.filesScanned.Count, 10f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			if (Dedicator.isDedicated)
			{
				if (!ReadWrite.folderExists("/Bundles/Workshop/Content", true))
				{
					ReadWrite.createFolder("/Bundles/Workshop/Content", true);
				}
				RootAssetDirectory sharedWorkshopDirectory = new RootAssetDirectory(ReadWrite.PATH + "/Bundles/Workshop/Content", "Workshop_Shared");
				Assets.rootAssetDirectories.Add(sharedWorkshopDirectory);
				Assets.scanFolder("/Bundles/Workshop/Content", true, false, true, sharedWorkshopDirectory, EAssetOrigin.WORKSHOP, false);
				LoadingUI.assetsScan("Workshop_Shared", Assets.filesScanned.Count);
				yield return null;
				while (Assets.filesScanned.Count > 0)
				{
					ScannedFileInfo file12 = Assets.filesScanned.Dequeue();
					Assets.loadFile(file12);
					if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
					{
						LoadingUI.assetsLoad("Workshop_Shared", Assets.filesScanned.Count, 11f / Assets.STEPS, 1f / Assets.STEPS);
						yield return null;
					}
				}
				if (!ReadWrite.folderExists(ServerSavedata.directory + "/" + Provider.serverID + "/Workshop/Content", true))
				{
					ReadWrite.createFolder(ServerSavedata.directory + "/" + Provider.serverID + "/Workshop/Content", true);
				}
				RootAssetDirectory serverWorkshopDirectory = new RootAssetDirectory(ServerSavedata.directory + "/" + Provider.serverID + "/Workshop/Content", "Workshop_Server");
				Assets.rootAssetDirectories.Add(serverWorkshopDirectory);
				Assets.scanFolder(ServerSavedata.directory + "/" + Provider.serverID + "/Workshop/Content", true, false, true, serverWorkshopDirectory, EAssetOrigin.WORKSHOP, false);
				LoadingUI.assetsScan("Workshop_Server", Assets.filesScanned.Count);
				yield return null;
				while (Assets.filesScanned.Count > 0)
				{
					ScannedFileInfo file13 = Assets.filesScanned.Dequeue();
					Assets.loadFile(file13);
					if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
					{
						LoadingUI.assetsLoad("Workshop_Server", Assets.filesScanned.Count, 12f / Assets.STEPS, 1f / Assets.STEPS);
						yield return null;
					}
				}
				RootAssetDirectory serverBundlesDirectory = new RootAssetDirectory(ServerSavedata.directory + "/" + Provider.serverID + "/Bundles", "Bundles_Server");
				Assets.rootAssetDirectories.Add(serverBundlesDirectory);
				Assets.scanFolder(ServerSavedata.directory + "/" + Provider.serverID + "/Bundles", true, false, true, serverBundlesDirectory, EAssetOrigin.MISC, false);
				LoadingUI.assetsScan("Bundles_Server", Assets.filesScanned.Count);
				yield return null;
				while (Assets.filesScanned.Count > 0)
				{
					ScannedFileInfo file14 = Assets.filesScanned.Dequeue();
					Assets.loadFile(file14);
					if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
					{
						LoadingUI.assetsLoad("Bundles_Server", Assets.filesScanned.Count, 13f / Assets.STEPS, 1f / Assets.STEPS);
						yield return null;
					}
				}
			}
			else
			{
				if (Provider.provider.workshopService.ugc != null)
				{
					for (int index = 0; index < Provider.provider.workshopService.ugc.Count; index++)
					{
						SteamContent content = Provider.provider.workshopService.ugc[index];
						if (content.type == ESteamUGCType.OBJECT || content.type == ESteamUGCType.ITEM || content.type == ESteamUGCType.VEHICLE)
						{
							RootAssetDirectory directory = new RootAssetDirectory(content.path, Path.GetFileName(Path.GetDirectoryName(content.path)));
							Assets.rootAssetDirectories.Add(directory);
							Assets.scanFolder(content.path, false, false, true, directory, EAssetOrigin.WORKSHOP, false);
							LoadingUI.assetsScan("Workshop_Client", Assets.filesScanned.Count);
							yield return null;
						}
					}
				}
				while (Assets.filesScanned.Count > 0)
				{
					ScannedFileInfo file15 = Assets.filesScanned.Dequeue();
					Assets.loadFile(file15);
					if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
					{
						LoadingUI.assetsLoad("Workshop_Client", Assets.filesScanned.Count, 14f / Assets.STEPS, 1f / Assets.STEPS);
						yield return null;
					}
				}
			}
			foreach (LevelInfo levelInfo in Level.getLevels(ESingleplayerMapCategory.ALL))
			{
				if (levelInfo != null)
				{
					if (ReadWrite.folderExists(levelInfo.path + "/Bundles", false))
					{
						RootAssetDirectory rootAssetDirectory = new RootAssetDirectory(levelInfo.path + "/Bundles", Path.GetFileName(levelInfo.path));
						Assets.rootAssetDirectories.Add(rootAssetDirectory);
						EAssetOrigin origin = EAssetOrigin.MISC;
						ESingleplayerMapCategory category = levelInfo.configData.Category;
						if (category != ESingleplayerMapCategory.OFFICIAL)
						{
							if (category != ESingleplayerMapCategory.CURATED)
							{
								if (category == ESingleplayerMapCategory.WORKSHOP)
								{
									origin = EAssetOrigin.WORKSHOP;
								}
							}
							else
							{
								origin = EAssetOrigin.CURATED;
							}
						}
						else
						{
							origin = EAssetOrigin.OFFICIAL;
						}
						Assets.scanFolder(levelInfo.path, "/Bundles", levelInfo.configData.Load_From_Resources, levelInfo.configData.Can_Use_Bundles, rootAssetDirectory, origin, false);
					}
					if (ReadWrite.folderExists(levelInfo.path + "/Content", false))
					{
						Assets.searchForAndLoadContent(levelInfo.path + "/Content");
					}
				}
			}
			LoadingUI.assetsScan("Map", Assets.filesScanned.Count);
			yield return null;
			while (Assets.filesScanned.Count > 0)
			{
				ScannedFileInfo file16 = Assets.filesScanned.Dequeue();
				Assets.loadFile(file16);
				if (Assets.filesScanned.Count % Assets.assetsToLoadPerStep == 0)
				{
					LoadingUI.assetsLoad("Map", Assets.filesScanned.Count, 15f / Assets.STEPS, 1f / Assets.STEPS);
					yield return null;
				}
			}
			LoadingUI.updateKey("Loading_Clean");
			yield return null;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			LoadingUI.updateKey("Loading_Blueprints");
			yield return null;
			Asset[] itemAssets = Assets.find(EAssetType.ITEM);
			if (itemAssets != null)
			{
				for (int j = 0; j < itemAssets.Length; j++)
				{
					ItemAsset itemAsset = (ItemAsset)itemAssets[j];
					byte b = 0;
					while ((int)b < itemAsset.blueprints.Count)
					{
						Blueprint blueprint = itemAsset.blueprints[(int)b];
						byte b2 = 0;
						while ((int)b2 < itemAsset.blueprints.Count)
						{
							if (b2 != b)
							{
								Blueprint blueprint2 = itemAsset.blueprints[(int)b2];
								if (blueprint.type == blueprint2.type)
								{
									if (blueprint.outputs.Length == blueprint2.outputs.Length)
									{
										bool flag = true;
										byte b3 = 0;
										while ((int)b3 < blueprint.outputs.Length)
										{
											if (blueprint.outputs[(int)b3].id != blueprint2.outputs[(int)b3].id)
											{
												flag = false;
											}
											b3 += 1;
										}
										if (flag)
										{
											if (blueprint.supplies.Length == blueprint2.supplies.Length)
											{
												bool flag2 = true;
												byte b4 = 0;
												while ((int)b4 < blueprint.supplies.Length)
												{
													if (blueprint.supplies[(int)b4].id != blueprint2.supplies[(int)b4].id)
													{
														flag2 = false;
													}
													b4 += 1;
												}
												if (flag2)
												{
													Assets.errors.Add(itemAsset.itemName + " has an identical blueprint: " + blueprint);
												}
											}
										}
									}
								}
							}
							b2 += 1;
						}
						b += 1;
					}
					for (int k = 0; k < itemAssets.Length; k++)
					{
						if (k != j)
						{
							ItemAsset itemAsset2 = (ItemAsset)itemAssets[k];
							byte b5 = 0;
							while ((int)b5 < itemAsset.blueprints.Count)
							{
								Blueprint blueprint3 = itemAsset.blueprints[(int)b5];
								byte b6 = 0;
								while ((int)b6 < itemAsset2.blueprints.Count)
								{
									Blueprint blueprint4 = itemAsset2.blueprints[(int)b6];
									if (blueprint3.type == blueprint4.type)
									{
										if (blueprint3.outputs.Length == blueprint4.outputs.Length)
										{
											bool flag3 = true;
											byte b7 = 0;
											while ((int)b7 < blueprint3.outputs.Length)
											{
												if (blueprint3.outputs[(int)b7].id != blueprint4.outputs[(int)b7].id)
												{
													flag3 = false;
												}
												b7 += 1;
											}
											if (flag3)
											{
												if (blueprint3.supplies.Length == blueprint4.supplies.Length)
												{
													bool flag4 = true;
													byte b8 = 0;
													while ((int)b8 < blueprint3.supplies.Length)
													{
														if (blueprint3.supplies[(int)b8].id != blueprint4.supplies[(int)b8].id)
														{
															flag4 = false;
														}
														b8 += 1;
													}
													if (flag4)
													{
														Assets.errors.Add(string.Concat(new object[]
														{
															itemAsset.itemName,
															" shares an identical blueprint with ",
															itemAsset2.itemName,
															": ",
															blueprint3
														}));
													}
												}
											}
										}
									}
									b6 += 1;
								}
								b5 += 1;
							}
						}
					}
				}
			}
			LoadingUI.updateKey("Loading_Spawns");
			yield return null;
			Asset[] spawnAssets = Assets.find(EAssetType.SPAWN);
			if (spawnAssets != null)
			{
				foreach (SpawnAsset spawnAsset in spawnAssets)
				{
					for (int m = 0; m < spawnAsset.roots.Count; m++)
					{
						SpawnTable spawnTable = spawnAsset.roots[m];
						if (spawnTable.spawnID != 0)
						{
							SpawnAsset spawnAsset2 = (SpawnAsset)Assets.find(EAssetType.SPAWN, spawnTable.spawnID);
							if (spawnAsset2 != null)
							{
								spawnTable.spawnID = spawnAsset.id;
								spawnTable.isLink = true;
								spawnAsset2.tables.Add(spawnTable);
							}
						}
					}
					spawnAsset.roots.Clear();
				}
				foreach (SpawnAsset spawnAsset3 in spawnAssets)
				{
					spawnAsset3.prepare();
				}
				foreach (SpawnAsset spawnAsset4 in spawnAssets)
				{
					for (int num2 = 0; num2 < spawnAsset4.tables.Count; num2++)
					{
						SpawnTable spawnTable2 = spawnAsset4.tables[num2];
						if (spawnTable2.spawnID != 0)
						{
							SpawnAsset spawnAsset5 = (SpawnAsset)Assets.find(EAssetType.SPAWN, spawnTable2.spawnID);
							if (spawnAsset5 != null)
							{
								SpawnTable spawnTable3 = new SpawnTable();
								spawnTable3.assetID = 0;
								spawnTable3.spawnID = spawnAsset4.id;
								spawnTable3.weight = spawnTable2.weight;
								spawnTable3.chance = spawnTable2.chance;
								if (num2 > 0)
								{
									spawnTable3.chance -= spawnAsset4.tables[num2 - 1].chance;
								}
								spawnTable3.isLink = spawnTable2.isLink;
								spawnAsset5.roots.Add(spawnTable3);
							}
						}
					}
				}
			}
			LoadingUI.updateKey("Loading_Misc");
			yield return null;
			if (Assets.onAssetsRefreshed != null)
			{
				Assets.onAssetsRefreshed();
			}
			yield return null;
			Assets._isLoading = false;
			if (!Assets.hasLoaded)
			{
				Assets.hasLoaded = true;
				if (Dedicator.isDedicated)
				{
					Provider.host();
				}
				else
				{
					SceneManager.LoadScene("Menu");
				}
			}
			yield break;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0008E6EC File Offset: 0x0008CAEC
		private void Start()
		{
			Assets.refresh();
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0008E6F3 File Offset: 0x0008CAF3
		private void Awake()
		{
			Assets.asset = this;
		}

		// Token: 0x04000D9D RID: 3485
		private static readonly float STEPS = 16f;

		// Token: 0x04000D9E RID: 3486
		private static readonly int ASSETS_PER_STEP = 25;

		// Token: 0x04000D9F RID: 3487
		private static TypeRegistryDictionary _assetTypes = new TypeRegistryDictionary(typeof(Asset));

		// Token: 0x04000DA0 RID: 3488
		private static TypeRegistryDictionary _useableTypes = new TypeRegistryDictionary(typeof(Useable));

		// Token: 0x04000DA1 RID: 3489
		private static Assets asset;

		// Token: 0x04000DA2 RID: 3490
		private static bool hasLoaded;

		// Token: 0x04000DA3 RID: 3491
		private static bool _isLoading;

		// Token: 0x04000DA4 RID: 3492
		public static AssetsRefreshed onAssetsRefreshed;

		// Token: 0x04000DA5 RID: 3493
		private static Dictionary<EAssetType, Dictionary<ushort, Asset>> assets;

		// Token: 0x04000DA6 RID: 3494
		private static Dictionary<EAssetType, Dictionary<string, Asset>> namedAssets;

		// Token: 0x04000DA7 RID: 3495
		private static Dictionary<Guid, Asset> assetDictionary;

		// Token: 0x04000DA8 RID: 3496
		private static List<Asset> assetList;

		// Token: 0x04000DA9 RID: 3497
		private static Queue<ScannedFileInfo> filesScanned;

		// Token: 0x04000DAC RID: 3500
		private static List<string> _errors;
	}
}
