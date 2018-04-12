using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000168 RID: 360
	public class DevkitLandscapeToolHeightmapOptions : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00055E21 File Offset: 0x00054221
		static DevkitLandscapeToolHeightmapOptions()
		{
			DevkitLandscapeToolHeightmapOptions._instance = new DevkitLandscapeToolHeightmapOptions();
			DevkitLandscapeToolHeightmapOptions.load();
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00055E77 File Offset: 0x00054277
		public static DevkitLandscapeToolHeightmapOptions instance
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions._instance;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00055E7E File Offset: 0x0005427E
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00055E85 File Offset: 0x00054285
		[TerminalCommandProperty("landscape.tool.adjust_sensitivity", "multiplier for adjust brush delta", 0.1f)]
		public static float adjustSensitivity
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions._adjustSensitivity;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions._adjustSensitivity = value;
				TerminalUtility.printCommandPass("Set adjust_sensitivity to: " + DevkitLandscapeToolHeightmapOptions.adjustSensitivity);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00055EA6 File Offset: 0x000542A6
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00055EAD File Offset: 0x000542AD
		[TerminalCommandProperty("landscape.tool.flatten_sensitivity", "multiplier for flatten brush delta", 1)]
		public static float flattenSensitivity
		{
			get
			{
				return DevkitLandscapeToolHeightmapOptions._flattenSensitivity;
			}
			set
			{
				DevkitLandscapeToolHeightmapOptions._flattenSensitivity = value;
				TerminalUtility.printCommandPass("Set flatten_sensitivity to: " + DevkitLandscapeToolHeightmapOptions.flattenSensitivity);
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00055ED0 File Offset: 0x000542D0
		public static void load()
		{
			DevkitLandscapeToolHeightmapOptions.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Heightmap.tool";
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
				DevkitLandscapeToolHeightmapOptions.instance.read(reader);
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00055F54 File Offset: 0x00054354
		public static void save()
		{
			if (!DevkitLandscapeToolHeightmapOptions.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Heightmap.tool";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<DevkitLandscapeToolHeightmapOptions>(DevkitLandscapeToolHeightmapOptions.instance);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00055FD0 File Offset: 0x000543D0
		public void read(IFormattedFileReader reader)
		{
			this.brushRadius = reader.readValue<float>("Brush_Radius");
			this.brushFalloff = reader.readValue<float>("Brush_Falloff");
			this.brushStrength = reader.readValue<float>("Brush_Strength");
			this.flattenTarget = reader.readValue<float>("Flatten_Target");
			this.maxPreviewSamples = reader.readValue<uint>("Max_Preview_Samples");
			this.smoothMethod = reader.readValue<DevkitLandscapeTool.EDevkitLandscapeToolHeightmapSmoothMethod>("Smooth_Method");
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00056044 File Offset: 0x00054444
		public void write(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Brush_Radius", this.brushRadius);
			writer.writeValue<float>("Brush_Falloff", this.brushFalloff);
			writer.writeValue<float>("Brush_Strength", this.brushStrength);
			writer.writeValue<float>("Flatten_Target", this.flattenTarget);
			writer.writeValue<uint>("Max_Preview_Samples", this.maxPreviewSamples);
			writer.writeValue<DevkitLandscapeTool.EDevkitLandscapeToolHeightmapSmoothMethod>("Smooth_Method", this.smoothMethod);
		}

		// Token: 0x040007B2 RID: 1970
		private static DevkitLandscapeToolHeightmapOptions _instance;

		// Token: 0x040007B3 RID: 1971
		protected static float _adjustSensitivity = 0.1f;

		// Token: 0x040007B4 RID: 1972
		protected static float _flattenSensitivity = 1f;

		// Token: 0x040007B5 RID: 1973
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Brush.Radius", null)]
		public float brushRadius = 16f;

		// Token: 0x040007B6 RID: 1974
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Brush.Falloff", null)]
		public float brushFalloff = 0.5f;

		// Token: 0x040007B7 RID: 1975
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Brush.Strength", null)]
		public float brushStrength = 0.05f;

		// Token: 0x040007B8 RID: 1976
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Flatten_Target", null)]
		public float flattenTarget;

		// Token: 0x040007B9 RID: 1977
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Max_Preview_Samples", null)]
		public uint maxPreviewSamples = 64u;

		// Token: 0x040007BA RID: 1978
		[Inspectable("#SDG::Devkit.Landscape_Tool.Heightmap.Smooth_Method", null)]
		public DevkitLandscapeTool.EDevkitLandscapeToolHeightmapSmoothMethod smoothMethod;

		// Token: 0x040007BB RID: 1979
		protected static bool wasLoaded;
	}
}
