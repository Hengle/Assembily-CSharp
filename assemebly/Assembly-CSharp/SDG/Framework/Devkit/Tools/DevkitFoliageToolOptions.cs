using System;
using System.IO;
using SDG.Framework.Debug;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Unturned;

namespace SDG.Framework.Devkit.Tools
{
	// Token: 0x0200015E RID: 350
	public class DevkitFoliageToolOptions : IFormattedFileReadable, IFormattedFileWritable
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00052AAC File Offset: 0x00050EAC
		static DevkitFoliageToolOptions()
		{
			DevkitFoliageToolOptions._instance = new DevkitFoliageToolOptions();
			DevkitFoliageToolOptions.load();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00052B3B File Offset: 0x00050F3B
		public static DevkitFoliageToolOptions instance
		{
			get
			{
				return DevkitFoliageToolOptions._instance;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00052B42 File Offset: 0x00050F42
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x00052B49 File Offset: 0x00050F49
		[TerminalCommandProperty("foliage.tool.add_sensitivity", "multiplier for paint brush delta", 1)]
		public static float addSensitivity
		{
			get
			{
				return DevkitFoliageToolOptions._addSensitivity;
			}
			set
			{
				DevkitFoliageToolOptions._addSensitivity = value;
				TerminalUtility.printCommandPass("Set add_sensitivity to: " + DevkitFoliageToolOptions.addSensitivity);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00052B6A File Offset: 0x00050F6A
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00052B71 File Offset: 0x00050F71
		[TerminalCommandProperty("foliage.tool.remove_sensitivity", "multiplier for paint brush delta", 1)]
		public static float removeSensitivity
		{
			get
			{
				return DevkitFoliageToolOptions._removeSensitivity;
			}
			set
			{
				DevkitFoliageToolOptions._removeSensitivity = value;
				TerminalUtility.printCommandPass("Set remove_sensitivity to: " + DevkitFoliageToolOptions.removeSensitivity);
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00052B94 File Offset: 0x00050F94
		public static void load()
		{
			DevkitFoliageToolOptions.wasLoaded = true;
			string path = IOUtility.rootPath + "/Cloud/Foliage.tool";
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
				DevkitFoliageToolOptions.instance.read(reader);
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00052C18 File Offset: 0x00051018
		public static void save()
		{
			if (!DevkitFoliageToolOptions.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Foliage.tool";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeValue<DevkitFoliageToolOptions>(DevkitFoliageToolOptions.instance);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00052C94 File Offset: 0x00051094
		public void read(IFormattedFileReader reader)
		{
			this.bakeInstancedMeshes = reader.readValue<bool>("Bake_Instanced_Meshes");
			this.bakeResources = reader.readValue<bool>("Bake_Resources");
			this.bakeObjects = reader.readValue<bool>("Bake_Objects");
			this.bakeClear = reader.readValue<bool>("Bake_Clear");
			this.bakeApplyScale = reader.readValue<bool>("Bake_Apply_Scale");
			this.brushRadius = reader.readValue<float>("Brush_Radius");
			this.brushFalloff = reader.readValue<float>("Brush_Falloff");
			this.brushStrength = reader.readValue<float>("Brush_Strength");
			this.densityTarget = reader.readValue<float>("Density_Target");
			this.surfaceMask = reader.readValue<ERayMask>("Surface_Mask");
			this.maxPreviewSamples = reader.readValue<uint>("Max_Preview_Samples");
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00052D5C File Offset: 0x0005115C
		public void write(IFormattedFileWriter writer)
		{
			writer.writeValue<bool>("Bake_Instanced_Meshes", this.bakeInstancedMeshes);
			writer.writeValue<bool>("Bake_Resources", this.bakeResources);
			writer.writeValue<bool>("Bake_Objects", this.bakeObjects);
			writer.writeValue<bool>("Bake_Clear", this.bakeClear);
			writer.writeValue<bool>("Bake_Apply_Scale", this.bakeApplyScale);
			writer.writeValue<float>("Brush_Radius", this.brushRadius);
			writer.writeValue<float>("Brush_Falloff", this.brushFalloff);
			writer.writeValue<float>("Brush_Strength", this.brushStrength);
			writer.writeValue<float>("Density_Target", this.densityTarget);
			writer.writeValue<ERayMask>("Surface_Mask", this.surfaceMask);
			writer.writeValue<uint>("Max_Preview_Samples", this.maxPreviewSamples);
		}

		// Token: 0x0400076B RID: 1899
		private static DevkitFoliageToolOptions _instance;

		// Token: 0x0400076C RID: 1900
		protected static float _addSensitivity = 1f;

		// Token: 0x0400076D RID: 1901
		protected static float _removeSensitivity = 1f;

		// Token: 0x0400076E RID: 1902
		[Inspectable("#SDG::Devkit.Foliage_Tool.Bake.Instanced_Meshes", null)]
		public bool bakeInstancedMeshes = true;

		// Token: 0x0400076F RID: 1903
		[Inspectable("#SDG::Devkit.Foliage_Tool.Bake.Resources", null)]
		public bool bakeResources = true;

		// Token: 0x04000770 RID: 1904
		[Inspectable("#SDG::Devkit.Foliage_Tool.Bake.Objects", null)]
		public bool bakeObjects = true;

		// Token: 0x04000771 RID: 1905
		[Inspectable("#SDG::Devkit.Foliage_Tool.Bake.Clear", null)]
		public bool bakeClear;

		// Token: 0x04000772 RID: 1906
		[Inspectable("#SDG::Devkit.Foliage_Tool.Bake.Apply_Scale", null)]
		public bool bakeApplyScale;

		// Token: 0x04000773 RID: 1907
		[Inspectable("#SDG::Devkit.Foliage_Tool.Brush.Radius", null)]
		public float brushRadius = 16f;

		// Token: 0x04000774 RID: 1908
		[Inspectable("#SDG::Devkit.Foliage_Tool.Brush.Falloff", null)]
		public float brushFalloff = 0.5f;

		// Token: 0x04000775 RID: 1909
		[Inspectable("#SDG::Devkit.Foliage_Tool.Brush.Strength", null)]
		public float brushStrength = 0.05f;

		// Token: 0x04000776 RID: 1910
		[Inspectable("#SDG::Devkit.Foliage_Tool.Density_Target", null)]
		public float densityTarget = 1f;

		// Token: 0x04000777 RID: 1911
		[Inspectable("#SDG::Devkit.Foliage_Tool.Surface_Mask", null)]
		public ERayMask surfaceMask = ERayMask.LARGE | ERayMask.MEDIUM | ERayMask.SMALL | ERayMask.ENVIRONMENT | ERayMask.GROUND;

		// Token: 0x04000778 RID: 1912
		[Inspectable("#SDG::Devkit.Foliage_Tool.Max_Preview_Samples", null)]
		public uint maxPreviewSamples = 64u;

		// Token: 0x04000779 RID: 1913
		protected static bool wasLoaded;
	}
}
