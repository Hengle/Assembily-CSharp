using System;
using System.IO;
using SDG.Framework.IO;
using SDG.Framework.IO.FormattedFiles;
using SDG.Framework.IO.FormattedFiles.KeyValueTables;
using SDG.Framework.UI.Sleek2;

namespace SDG.Framework.UI.Devkit
{
	// Token: 0x02000240 RID: 576
	public class DevkitWindowLayout
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x0006E2F8 File Offset: 0x0006C6F8
		public static void load(string name)
		{
			DevkitWindowLayout.wasLoaded = true;
			DevkitWindowManager.resetLayout();
			string path = IOUtility.rootPath + "/Cloud/Layouts/" + name + ".layout";
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
				IFormattedFileReader formattedFileReader = new KeyValueTableReader(streamReader);
				formattedFileReader.readKey("Root");
				DevkitWindowManager.partition.read(formattedFileReader);
				int num = formattedFileReader.readArrayLength("Containers");
				for (int i = 0; i < num; i++)
				{
					formattedFileReader.readArrayIndex(i);
					IFormattedFileReader formattedFileReader2 = formattedFileReader.readObject();
					if (formattedFileReader2 != null)
					{
						Type type = formattedFileReader2.readValue<Type>("Type");
						if (type != null)
						{
							Sleek2PopoutContainer sleek2PopoutContainer = DevkitWindowManager.addContainer(type);
							if (sleek2PopoutContainer != null)
							{
								formattedFileReader2.readKey("Container");
								sleek2PopoutContainer.read(formattedFileReader2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0006E40C File Offset: 0x0006C80C
		public static void save(string name)
		{
			if (!DevkitWindowLayout.wasLoaded)
			{
				return;
			}
			string path = IOUtility.rootPath + "/Cloud/Layouts/" + name + ".layout";
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				IFormattedFileWriter formattedFileWriter = new KeyValueTableWriter(streamWriter);
				formattedFileWriter.writeKey("Root");
				DevkitWindowManager.partition.write(formattedFileWriter);
				formattedFileWriter.writeKey("Containers");
				formattedFileWriter.beginArray();
				for (int i = 0; i < DevkitWindowManager.containers.Count; i++)
				{
					formattedFileWriter.beginObject();
					Sleek2PopoutContainer sleek2PopoutContainer = DevkitWindowManager.containers[i];
					formattedFileWriter.writeValue<Type>("Type", sleek2PopoutContainer.GetType());
					formattedFileWriter.writeValue<Sleek2PopoutContainer>("Container", sleek2PopoutContainer);
					formattedFileWriter.endObject();
				}
				formattedFileWriter.endArray();
			}
		}

		// Token: 0x04000A1E RID: 2590
		protected static bool wasLoaded;
	}
}
