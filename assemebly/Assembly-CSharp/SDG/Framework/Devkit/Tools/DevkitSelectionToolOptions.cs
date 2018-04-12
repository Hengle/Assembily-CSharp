using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Unturned;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000177 RID: 375
	public class DevkitSelectionToolOptions : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000B44 RID: 2884 RVA: 0x00059E07 File Offset: 0x00058207
		static DevkitSelectionToolOptions()
		{
			DevkitSelectionToolOptions.load();
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00059E4C File Offset: 0x0005824C
		public static DevkitSelectionToolOptions instance
		{
			get
			{
				return DevkitSelectionToolOptions._instance;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00059E54 File Offset: 0x00058254
		public static void load()
		{
			DevkitSelectionToolOptions.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Selection.tool";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (!File.Exists(path))
			{
				return;
			}
			using (StreamReader streamReader = new StreamReader(path))
			{
				IFormattedFileReader reader = new KeyValueTableReader(streamReader);
				DevkitSelectionToolOptions.instance.read(reader);
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00059ED8 File Offset: 0x000582D8
		public static void save()
		{
			if (!DevkitSelectionToolOptions.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Selection.tool";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<DevkitSelectionToolOptions>(DevkitSelectionToolOptions.instance);
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00059F54 File Offset: 0x00058354
		public void read(IFormattedFileReader reader)
		{
			this.snapPosition = reader.readValue<float>("Snap_Position");
			this.snapRotation = reader.readValue<float>("Snap_Rotation");
			this.snapScale = reader.readValue<float>("Snap_Scale");
			this.surfaceOffset = reader.readValue<float>("Surface_Offset");
			this.surfaceAlign = reader.readValue<bool>("Surface_Align");
			this.localSpace = reader.readValue<bool>("Local_Space");
			this.lockHandles = reader.readValue<bool>("Lock_Handles");
			this.selectionMask = reader.readValue<ERayMask>("Selection_Mask");
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00059FEC File Offset: 0x000583EC
		public void write(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Snap_Position", this.snapPosition);
			writer.writeValue<float>("Snap_Rotation", this.snapRotation);
			writer.writeValue<float>("Snap_Scale", this.snapScale);
			writer.writeValue<float>("Surface_Offset", this.surfaceOffset);
			writer.writeValue<bool>("Surface_Align", this.surfaceAlign);
			writer.writeValue<bool>("Local_Space", this.localSpace);
			writer.writeValue<bool>("Lock_Handles", this.lockHandles);
			writer.writeValue<ERayMask>("Selection_Mask", this.selectionMask);
		}

		// Token: 0x0400082D RID: 2093
		private static DevkitSelectionToolOptions _instance = new DevkitSelectionToolOptions();

		// Token: 0x0400082E RID: 2094
		[Inspectable("#SDG::Devkit.Selection_Tool.Snap_Position", null)]
		public float snapPosition = 1f;

		// Token: 0x0400082F RID: 2095
		[Inspectable("#SDG::Devkit.Selection_Tool.Snap_Rotation", null)]
		public float snapRotation = 15f;

		// Token: 0x04000830 RID: 2096
		[Inspectable("#SDG::Devkit.Selection_Tool.Snap_Scale", null)]
		public float snapScale = 0.1f;

		// Token: 0x04000831 RID: 2097
		[Inspectable("#SDG::Devkit.Selection_Tool.Surface_Offset", null)]
		public float surfaceOffset;

		// Token: 0x04000832 RID: 2098
		[Inspectable("#SDG::Devkit.Selection_Tool.Surface_Align", null)]
		public bool surfaceAlign;

		// Token: 0x04000833 RID: 2099
		[Inspectable("#SDG::Devkit.Selection_Tool.Local_Space", null)]
		public bool localSpace;

		// Token: 0x04000834 RID: 2100
		[Inspectable("#SDG::Devkit.Selection_Tool.Lock_Handles", null)]
		public bool lockHandles;

		// Token: 0x04000835 RID: 2101
		[Inspectable("#SDG::Devkit.Foliage_Tool.Selection_Mask", null)]
		public ERayMask selectionMask = ERayMask.LARGE | ERayMask.MEDIUM | ERayMask.SMALL | ERayMask.ENVIRONMENT | ERayMask.GROUND | ERayMask.CLIP | ERayMask.TRAP;

		// Token: 0x04000836 RID: 2102
		public IDevkitSelectionToolInstantiationInfo instantiationInfo;

		// Token: 0x04000837 RID: 2103
		protected static bool wasLoaded;
	}
}
