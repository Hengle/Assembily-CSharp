using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Framework.Modules;

namespace SDG.Framework.Devkit.Visibility
{
	// Token: 0x0200018D RID: 397
	public static class VisibilityManager
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0005AEA8 File Offset: 0x000592A8
		static VisibilityManager()
		{
			VisibilityManager.groups.canInspectorAdd = false;
			VisibilityManager.groups.canInspectorRemove = false;
			VisibilityManager.savedGroups = new Dictionary<string, IVisibilityGroup>();
			if (VisibilityManager.<>f__mg$cache0 == null)
			{
				VisibilityManager.<>f__mg$cache0 = new ModulesInitializedHandler(VisibilityManager.handleModulesInitialized);
			}
			ModuleHook.onModulesInitialized += VisibilityManager.<>f__mg$cache0;
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0005AF01 File Offset: 0x00059301
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0005AF08 File Offset: 0x00059308
		[Inspectable("#SDG::Groups", null)]
		public static InspectableList<IVisibilityGroup> groups { get; private set; } = new InspectableList<IVisibilityGroup>();

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000BDE RID: 3038 RVA: 0x0005AF10 File Offset: 0x00059310
		// (remove) Token: 0x06000BDF RID: 3039 RVA: 0x0005AF44 File Offset: 0x00059344
		public static event VisibilityManagerGroupRegisteredHandler groupRegistered;

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0005AF78 File Offset: 0x00059378
		public static T registerVisibilityGroup<T>(T defaultGroup) where T : IVisibilityGroup
		{
			foreach (IVisibilityGroup visibilityGroup in VisibilityManager.groups)
			{
				if (visibilityGroup.internalName == defaultGroup.internalName)
				{
					VisibilityManager.triggerGroupRegistered(visibilityGroup);
					return (T)((object)visibilityGroup);
				}
			}
			IVisibilityGroup visibilityGroup2;
			if (VisibilityManager.savedGroups.TryGetValue(defaultGroup.internalName, out visibilityGroup2))
			{
				visibilityGroup2.displayName = defaultGroup.displayName;
				if (visibilityGroup2.GetType() == defaultGroup.GetType())
				{
					defaultGroup = (T)((object)visibilityGroup2);
				}
			}
			VisibilityManager.groups.Add(defaultGroup);
			VisibilityManager.triggerGroupRegistered(defaultGroup);
			return defaultGroup;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0005B06C File Offset: 0x0005946C
		public static void load()
		{
			VisibilityManager.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Visibility.config";
			if (!File.Exists(path))
			{
				return;
			}
			using (StreamReader streamReader = new StreamReader(path))
			{
				IFormattedFileReader formattedFileReader = new KeyValueTableReader(streamReader);
				int num = formattedFileReader.readArrayLength("Groups");
				for (int i = 0; i < num; i++)
				{
					formattedFileReader.readArrayIndex(i);
					IFormattedFileReader formattedFileReader2 = formattedFileReader.readObject();
					string text = formattedFileReader2.readValue<string>("Name");
					Type type = formattedFileReader2.readValue<Type>("Type");
					if (type != null)
					{
						IVisibilityGroup visibilityGroup = formattedFileReader2.readValue(type, "Group") as IVisibilityGroup;
						visibilityGroup.internalName = text;
						VisibilityManager.savedGroups.Add(text, visibilityGroup);
					}
				}
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0005B14C File Offset: 0x0005954C
		public static void save()
		{
			if (!VisibilityManager.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Visibility.config";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeKey("Groups");
				formattedFileWriter.beginArray();
				foreach (IVisibilityGroup visibilityGroup in VisibilityManager.groups)
				{
					formattedFileWriter.beginObject();
					formattedFileWriter.writeValue("Name", visibilityGroup.internalName);
					formattedFileWriter.writeValue<Type>("Type", visibilityGroup.GetType());
					formattedFileWriter.writeValue<IVisibilityGroup>("Group", visibilityGroup);
					formattedFileWriter.endObject();
				}
				formattedFileWriter.endArray();
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0005B258 File Offset: 0x00059658
		private static void triggerGroupRegistered(IVisibilityGroup group)
		{
			VisibilityManagerGroupRegisteredHandler visibilityManagerGroupRegisteredHandler = VisibilityManager.groupRegistered;
			if (visibilityManagerGroupRegisteredHandler != null)
			{
				visibilityManagerGroupRegisteredHandler(group);
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0005B278 File Offset: 0x00059678
		private static void handleModulesInitialized()
		{
			VisibilityManager.load();
		}

		// Token: 0x04000861 RID: 2145
		private static Dictionary<string, IVisibilityGroup> savedGroups;

		// Token: 0x04000862 RID: 2146
		public static bool wasLoaded;

		// Token: 0x04000864 RID: 2148
		[CompilerGenerated]
		private static ModulesInitializedHandler <>f__mg$cache0;
	}
}
