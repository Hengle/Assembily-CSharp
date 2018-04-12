using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Unturned;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x02000169 RID: 361
	public class DevkitLandscapeToolSplatmapOptions : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x000560B7 File Offset: 0x000544B7
		static DevkitLandscapeToolSplatmapOptions()
		{
			DevkitLandscapeToolSplatmapOptions._instance = new DevkitLandscapeToolSplatmapOptions();
			DevkitLandscapeToolSplatmapOptions.load();
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0005613C File Offset: 0x0005453C
		public static DevkitLandscapeToolSplatmapOptions instance
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions._instance;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00056143 File Offset: 0x00054543
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0005614A File Offset: 0x0005454A
		[TerminalCommandProperty("landscape.tool.paint_sensitivity", "multiplier for paint brush delta", 1)]
		public static float paintSensitivity
		{
			get
			{
				return DevkitLandscapeToolSplatmapOptions._paintSensitivity;
			}
			set
			{
				DevkitLandscapeToolSplatmapOptions._paintSensitivity = value;
				TerminalUtility.printCommandPass("Set paint_sensitivity to: " + DevkitLandscapeToolSplatmapOptions.paintSensitivity);
			}
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0005616C File Offset: 0x0005456C
		public static void load()
		{
			DevkitLandscapeToolSplatmapOptions.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Splatmap.tool";
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
				DevkitLandscapeToolSplatmapOptions.instance.read(reader);
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000561F0 File Offset: 0x000545F0
		public static void save()
		{
			if (!DevkitLandscapeToolSplatmapOptions.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Splatmap.tool";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<DevkitLandscapeToolSplatmapOptions>(DevkitLandscapeToolSplatmapOptions.instance);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0005626C File Offset: 0x0005466C
		public void read(IFormattedFileReader reader)
		{
			this.brushRadius = reader.readValue<float>("Brush_Radius");
			this.brushFalloff = reader.readValue<float>("Brush_Falloff");
			this.brushStrength = reader.readValue<float>("Brush_Strength");
			this.useWeightTarget = reader.readValue<bool>("Use_Weight_Target");
			this.weightTarget = reader.readValue<float>("Weight_Target");
			this.maxPreviewSamples = reader.readValue<uint>("Max_Preview_Samples");
			this.smoothMethod = reader.readValue<DevkitLandscapeTool.EDevkitLandscapeToolSplatmapSmoothMethod>("Smooth_Method");
			this.previewMethod = reader.readValue<DevkitLandscapeTool.EDevkitLandscapeToolSplatmapPreviewMethod>("Preview_Method");
			this.useAutoSlope = reader.readValue<bool>("Use_Auto_Slope");
			this.autoMinAngleBegin = reader.readValue<float>("Auto_Min_Angle_Begin");
			this.autoMinAngleEnd = reader.readValue<float>("Auto_Min_Angle_End");
			this.autoMaxAngleBegin = reader.readValue<float>("Auto_Max_Angle_Begin");
			this.autoMaxAngleEnd = reader.readValue<float>("Auto_Max_Angle_End");
			this.useAutoFoundation = reader.readValue<bool>("Use_Auto_Foundation");
			this.autoRayRadius = reader.readValue<float>("Auto_Ray_Radius");
			this.autoRayLength = reader.readValue<float>("Auto_Ray_Length");
			this.autoRayMask = reader.readValue<ERayMask>("Auto_Ray_Mask");
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0005639C File Offset: 0x0005479C
		public void write(IFormattedFileWriter writer)
		{
			writer.writeValue<float>("Brush_Radius", this.brushRadius);
			writer.writeValue<float>("Brush_Falloff", this.brushFalloff);
			writer.writeValue<float>("Brush_Strength", this.brushStrength);
			writer.writeValue<bool>("Use_Weight_Target", this.useWeightTarget);
			writer.writeValue<float>("Weight_Target", this.weightTarget);
			writer.writeValue<uint>("Max_Preview_Samples", this.maxPreviewSamples);
			writer.writeValue<DevkitLandscapeTool.EDevkitLandscapeToolSplatmapSmoothMethod>("Smooth_Method", this.smoothMethod);
			writer.writeValue<DevkitLandscapeTool.EDevkitLandscapeToolSplatmapPreviewMethod>("Preview_Method", this.previewMethod);
			writer.writeValue<bool>("Use_Auto_Slope", this.useAutoSlope);
			writer.writeValue<float>("Auto_Min_Angle_Begin", this.autoMinAngleBegin);
			writer.writeValue<float>("Auto_Min_Angle_End", this.autoMinAngleEnd);
			writer.writeValue<float>("Auto_Max_Angle_Begin", this.autoMaxAngleBegin);
			writer.writeValue<float>("Auto_Max_Angle_End", this.autoMaxAngleEnd);
			writer.writeValue<bool>("Use_Auto_Foundation", this.useAutoFoundation);
			writer.writeValue<float>("Auto_Ray_Radius", this.autoRayRadius);
			writer.writeValue<float>("Auto_Ray_Length", this.autoRayLength);
			writer.writeValue<ERayMask>("Auto_Ray_Mask", this.autoRayMask);
		}

		// Token: 0x040007BC RID: 1980
		private static DevkitLandscapeToolSplatmapOptions _instance;

		// Token: 0x040007BD RID: 1981
		protected static float _paintSensitivity = 1f;

		// Token: 0x040007BE RID: 1982
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Brush.Radius", null)]
		public float brushRadius = 16f;

		// Token: 0x040007BF RID: 1983
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Brush.Falloff", null)]
		public float brushFalloff = 0.5f;

		// Token: 0x040007C0 RID: 1984
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Brush.Strength", null)]
		public float brushStrength = 1f;

		// Token: 0x040007C1 RID: 1985
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Use_Weight_Target", null)]
		public bool useWeightTarget;

		// Token: 0x040007C2 RID: 1986
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Weight_Target", null)]
		public float weightTarget;

		// Token: 0x040007C3 RID: 1987
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Max_Preview_Samples", null)]
		public uint maxPreviewSamples = 64u;

		// Token: 0x040007C4 RID: 1988
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Smooth_Method", null)]
		public DevkitLandscapeTool.EDevkitLandscapeToolSplatmapSmoothMethod smoothMethod;

		// Token: 0x040007C5 RID: 1989
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Preview_Method", null)]
		public DevkitLandscapeTool.EDevkitLandscapeToolSplatmapPreviewMethod previewMethod;

		// Token: 0x040007C6 RID: 1990
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Use_Auto_Slope", null)]
		public bool useAutoSlope;

		// Token: 0x040007C7 RID: 1991
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Min_Angle_Begin", null)]
		public float autoMinAngleBegin = 50f;

		// Token: 0x040007C8 RID: 1992
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Min_Angle_End", null)]
		public float autoMinAngleEnd = 70f;

		// Token: 0x040007C9 RID: 1993
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Max_Angle_Begin", null)]
		public float autoMaxAngleBegin = 90f;

		// Token: 0x040007CA RID: 1994
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Max_Angle_End", null)]
		public float autoMaxAngleEnd = 90f;

		// Token: 0x040007CB RID: 1995
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Use_Auto_Foundation", null)]
		public bool useAutoFoundation;

		// Token: 0x040007CC RID: 1996
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Ray_Radius", null)]
		public float autoRayRadius;

		// Token: 0x040007CD RID: 1997
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Ray_Length", null)]
		public float autoRayLength;

		// Token: 0x040007CE RID: 1998
		[Inspectable("#SDG::Devkit.Landscape_Tool.Splatmap.Auto_Ray_Mask", null)]
		public ERayMask autoRayMask;

		// Token: 0x040007CF RID: 1999
		protected static bool wasLoaded;
	}
}
