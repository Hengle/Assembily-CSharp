using System;
using System.IO;
using System.Text;
using Pathfinding.Ionic.Zip;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Serialization
{
	// Token: 0x0200004C RID: 76
	public class AstarSerializer
	{
		// Token: 0x0600032F RID: 815 RVA: 0x000192C3 File Offset: 0x000176C3
		public AstarSerializer(AstarData data)
		{
			this.data = data;
			this.settings = SerializeSettings.Settings;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000192EF File Offset: 0x000176EF
		public AstarSerializer(AstarData data, SerializeSettings settings)
		{
			this.data = data;
			this.settings = settings;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00019317 File Offset: 0x00017717
		private static StringBuilder GetStringBuilder()
		{
			AstarSerializer._stringBuilder.Length = 0;
			return AstarSerializer._stringBuilder;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00019329 File Offset: 0x00017729
		public void AddChecksum(byte[] bytes)
		{
			this.checksum = Checksum.GetChecksum(bytes, this.checksum);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001933D File Offset: 0x0001773D
		public uint GetChecksum()
		{
			return this.checksum;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00019348 File Offset: 0x00017748
		public void OpenSerialize()
		{
			this.zip = new ZipFile();
			this.zip.AlternateEncoding = Encoding.UTF8;
			this.zip.AlternateEncodingUsage = ZipOption.Always;
			this.writerSettings = new JsonWriterSettings();
			this.writerSettings.AddTypeConverter(new VectorConverter());
			this.writerSettings.AddTypeConverter(new BoundsConverter());
			this.writerSettings.AddTypeConverter(new LayerMaskConverter());
			this.writerSettings.AddTypeConverter(new MatrixConverter());
			this.writerSettings.AddTypeConverter(new GuidConverter());
			this.writerSettings.AddTypeConverter(new UnityObjectConverter());
			this.writerSettings.PrettyPrint = this.settings.prettyPrint;
			this.meta = new GraphMeta();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00019408 File Offset: 0x00017808
		public byte[] CloseSerialize()
		{
			byte[] array = this.SerializeMeta();
			this.AddChecksum(array);
			this.zip.AddEntry("meta.json", array);
			MemoryStream memoryStream = new MemoryStream();
			this.zip.Save(memoryStream);
			array = memoryStream.ToArray();
			memoryStream.Dispose();
			this.zip.Dispose();
			this.zip = null;
			return array;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00019468 File Offset: 0x00017868
		public void SerializeGraphs(NavGraph[] _graphs)
		{
			if (this.graphs != null)
			{
				throw new InvalidOperationException("Cannot serialize graphs multiple times.");
			}
			this.graphs = _graphs;
			if (this.zip == null)
			{
				throw new NullReferenceException("You must not call CloseSerialize before a call to this function");
			}
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					byte[] array = this.Serialize(this.graphs[i]);
					this.AddChecksum(array);
					this.zip.AddEntry("graph" + i + ".json", array);
				}
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00019524 File Offset: 0x00017924
		public void SerializeUserConnections(UserConnection[] conns)
		{
			if (conns == null)
			{
				conns = new UserConnection[0];
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(conns);
			byte[] bytes = this.encoding.GetBytes(stringBuilder.ToString());
			if (bytes.Length <= 2)
			{
				return;
			}
			this.AddChecksum(bytes);
			this.zip.AddEntry("connections.json", bytes);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00019590 File Offset: 0x00017990
		private byte[] SerializeMeta()
		{
			this.meta.version = AstarPath.Version;
			this.meta.graphs = this.data.graphs.Length;
			this.meta.guids = new string[this.data.graphs.Length];
			this.meta.typeNames = new string[this.data.graphs.Length];
			this.meta.nodeCounts = new int[this.data.graphs.Length];
			for (int i = 0; i < this.data.graphs.Length; i++)
			{
				if (this.data.graphs[i] != null)
				{
					this.meta.guids[i] = this.data.graphs[i].guid.ToString();
					this.meta.typeNames[i] = this.data.graphs[i].GetType().FullName;
				}
			}
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(this.meta);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000196D4 File Offset: 0x00017AD4
		public byte[] Serialize(NavGraph graph)
		{
			StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
			JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
			jsonWriter.Write(graph);
			return this.encoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001970C File Offset: 0x00017B0C
		public void SerializeNodes()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			if (this.graphs == null)
			{
				throw new InvalidOperationException("Cannot serialize nodes with no serialized graphs (call SerializeGraphs first)");
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				byte[] array = this.SerializeNodes(i);
				this.AddChecksum(array);
				this.zip.AddEntry("graph" + i + "_nodes.binary", array);
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				byte[] array2 = this.SerializeNodeConnections(j);
				this.AddChecksum(array2);
				this.zip.AddEntry("graph" + j + "_conns.binary", array2);
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000197D4 File Offset: 0x00017BD4
		private byte[] SerializeNodes(int index)
		{
			return new byte[0];
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000197DC File Offset: 0x00017BDC
		public void SerializeExtraInfo()
		{
			if (!this.settings.nodes)
			{
				return;
			}
			int totCount = 0;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						totCount = Math.Max(node.NodeIndex, totCount);
						if (node.NodeIndex == -1)
						{
							Debug.LogError("Graph contains destroyed nodes. This is a bug.");
						}
						return true;
					});
				}
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter wr = new BinaryWriter(memoryStream);
			wr.Write(totCount);
			int c = 0;
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null)
				{
					this.graphs[j].GetNodes(delegate(GraphNode node)
					{
						c = Math.Max(node.NodeIndex, c);
						wr.Write(node.NodeIndex);
						return true;
					});
				}
			}
			if (c != totCount)
			{
				throw new Exception("Some graphs are not consistent in their GetNodes calls, sequential calls give different results.");
			}
			byte[] array = memoryStream.ToArray();
			wr.Close();
			this.AddChecksum(array);
			this.zip.AddEntry("graph_references.binary", array);
			for (int k = 0; k < this.graphs.Length; k++)
			{
				if (this.graphs[k] != null)
				{
					MemoryStream memoryStream2 = new MemoryStream();
					BinaryWriter binaryWriter = new BinaryWriter(memoryStream2);
					GraphSerializationContext ctx = new GraphSerializationContext(binaryWriter);
					this.graphs[k].SerializeExtraInfo(ctx);
					byte[] array2 = memoryStream2.ToArray();
					binaryWriter.Close();
					this.AddChecksum(array2);
					this.zip.AddEntry("graph" + k + "_extra.binary", array2);
					memoryStream2 = new MemoryStream();
					binaryWriter = new BinaryWriter(memoryStream2);
					ctx = new GraphSerializationContext(binaryWriter);
					this.graphs[k].GetNodes(delegate(GraphNode node)
					{
						node.SerializeReferences(ctx);
						return true;
					});
					binaryWriter.Close();
					array2 = memoryStream2.ToArray();
					this.AddChecksum(array2);
					this.zip.AddEntry("graph" + k + "_references.binary", array2);
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00019A2A File Offset: 0x00017E2A
		private byte[] SerializeNodeConnections(int index)
		{
			return new byte[0];
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00019A34 File Offset: 0x00017E34
		public void SerializeEditorSettings(GraphEditorBase[] editors)
		{
			if (editors == null || !this.settings.editorSettings)
			{
				return;
			}
			for (int i = 0; i < editors.Length; i++)
			{
				if (editors[i] == null)
				{
					return;
				}
				StringBuilder stringBuilder = AstarSerializer.GetStringBuilder();
				JsonWriter jsonWriter = new JsonWriter(stringBuilder, this.writerSettings);
				jsonWriter.Write(editors[i]);
				byte[] bytes = this.encoding.GetBytes(stringBuilder.ToString());
				if (bytes.Length > 2)
				{
					this.AddChecksum(bytes);
					this.zip.AddEntry("graph" + i + "_editor.json", bytes);
				}
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00019ADC File Offset: 0x00017EDC
		public bool OpenDeserialize(byte[] bytes)
		{
			this.readerSettings = new JsonReaderSettings();
			this.readerSettings.AddTypeConverter(new VectorConverter());
			this.readerSettings.AddTypeConverter(new BoundsConverter());
			this.readerSettings.AddTypeConverter(new LayerMaskConverter());
			this.readerSettings.AddTypeConverter(new MatrixConverter());
			this.readerSettings.AddTypeConverter(new GuidConverter());
			this.readerSettings.AddTypeConverter(new UnityObjectConverter());
			this.str = new MemoryStream();
			this.str.Write(bytes, 0, bytes.Length);
			this.str.Position = 0L;
			try
			{
				this.zip = ZipFile.Read(this.str);
			}
			catch (Exception arg)
			{
				Debug.LogWarning("Caught exception when loading from zip\n" + arg);
				this.str.Dispose();
				return false;
			}
			this.meta = this.DeserializeMeta(this.zip["meta.json"]);
			if (this.meta.version > AstarPath.Version)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from a newer version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version
				}));
			}
			else if (this.meta.version < AstarPath.Version)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Trying to load data from an older version of the A* Pathfinding Project\nCurrent version: ",
					AstarPath.Version,
					" Data version: ",
					this.meta.version,
					"\nThis is usually fine, it just means you have upgraded to a new version.\nHowever node data (not settings) can get corrupted between versions, so it is recommendedto recalculate any caches (those for faster startup) and resave any files. Even if it seems to load fine, it might cause subtle bugs.\n"
				}));
			}
			return true;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00019C94 File Offset: 0x00018094
		public void CloseDeserialize()
		{
			this.str.Dispose();
			this.zip.Dispose();
			this.zip = null;
			this.str = null;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00019CBC File Offset: 0x000180BC
		public NavGraph[] DeserializeGraphs()
		{
			this.graphs = new NavGraph[this.meta.graphs];
			int num = 0;
			for (int i = 0; i < this.meta.graphs; i++)
			{
				Type graphType = this.meta.GetGraphType(i);
				if (!object.Equals(graphType, null))
				{
					num++;
					ZipEntry zipEntry = this.zip["graph" + i + ".json"];
					if (zipEntry == null)
					{
						throw new FileNotFoundException(string.Concat(new object[]
						{
							"Could not find data for graph ",
							i,
							" in zip. Entry 'graph+",
							i,
							".json' does not exist"
						}));
					}
					NavGraph navGraph = this.data.CreateGraph(graphType);
					string @string = this.GetString(zipEntry);
					JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
					jsonReader.PopulateObject<NavGraph>(ref navGraph);
					this.graphs[i] = navGraph;
					if (this.graphs[i].guid.ToString() != this.meta.guids[i])
					{
						throw new Exception("Guid in graph file not equal to guid defined in meta file. Have you edited the data manually?\n" + this.graphs[i].guid.ToString() + " != " + this.meta.guids[i]);
					}
				}
			}
			NavGraph[] array = new NavGraph[num];
			num = 0;
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null)
				{
					array[num] = this.graphs[j];
					num++;
				}
			}
			this.graphs = array;
			return this.graphs;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00019E80 File Offset: 0x00018280
		public UserConnection[] DeserializeUserConnections()
		{
			ZipEntry zipEntry = this.zip["connections.json"];
			if (zipEntry == null)
			{
				return new UserConnection[0];
			}
			string @string = this.GetString(zipEntry);
			JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
			return (UserConnection[])jsonReader.Deserialize(typeof(UserConnection[]));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00019ED8 File Offset: 0x000182D8
		public void DeserializeNodes()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					if (this.zip.ContainsEntry("graph" + i + "_nodes.binary"))
					{
					}
				}
			}
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null)
				{
					ZipEntry zipEntry = this.zip["graph" + j + "_nodes.binary"];
					if (zipEntry != null)
					{
						MemoryStream memoryStream = new MemoryStream();
						zipEntry.Extract(memoryStream);
						memoryStream.Position = 0L;
						BinaryReader reader = new BinaryReader(memoryStream);
						this.DeserializeNodes(j, reader);
					}
				}
			}
			for (int k = 0; k < this.graphs.Length; k++)
			{
				if (this.graphs[k] != null)
				{
					ZipEntry zipEntry2 = this.zip["graph" + k + "_conns.binary"];
					if (zipEntry2 != null)
					{
						MemoryStream memoryStream2 = new MemoryStream();
						zipEntry2.Extract(memoryStream2);
						memoryStream2.Position = 0L;
						BinaryReader reader2 = new BinaryReader(memoryStream2);
						this.DeserializeNodeConnections(k, reader2);
					}
				}
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001A044 File Offset: 0x00018444
		public void DeserializeExtraInfo()
		{
			bool flag = false;
			for (int i = 0; i < this.graphs.Length; i++)
			{
				ZipEntry zipEntry = this.zip["graph" + i + "_extra.binary"];
				if (zipEntry != null)
				{
					flag = true;
					MemoryStream memoryStream = new MemoryStream();
					zipEntry.Extract(memoryStream);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					BinaryReader reader2 = new BinaryReader(memoryStream);
					GraphSerializationContext ctx2 = new GraphSerializationContext(reader2, null, i);
					this.graphs[i].DeserializeExtraInfo(ctx2);
				}
			}
			if (!flag)
			{
				return;
			}
			int totCount = 0;
			for (int j = 0; j < this.graphs.Length; j++)
			{
				if (this.graphs[j] != null)
				{
					this.graphs[j].GetNodes(delegate(GraphNode node)
					{
						totCount = Math.Max(node.NodeIndex, totCount);
						if (node.NodeIndex == -1)
						{
							Debug.LogError("Graph contains destroyed nodes. This is a bug.");
						}
						return true;
					});
				}
			}
			ZipEntry zipEntry2 = this.zip["graph_references.binary"];
			if (zipEntry2 == null)
			{
				throw new Exception("Node references not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
			}
			MemoryStream memoryStream2 = new MemoryStream();
			zipEntry2.Extract(memoryStream2);
			memoryStream2.Seek(0L, SeekOrigin.Begin);
			BinaryReader reader = new BinaryReader(memoryStream2);
			int num = reader.ReadInt32();
			GraphNode[] int2Node = new GraphNode[num + 1];
			try
			{
				for (int k = 0; k < this.graphs.Length; k++)
				{
					if (this.graphs[k] != null)
					{
						this.graphs[k].GetNodes(delegate(GraphNode node)
						{
							int2Node[reader.ReadInt32()] = node;
							return true;
						});
					}
				}
			}
			catch (Exception innerException)
			{
				throw new Exception("Some graph(s) has thrown an exception during GetNodes, or some graph(s) have deserialized more or fewer nodes than were serialized", innerException);
			}
			reader.Close();
			for (int l = 0; l < this.graphs.Length; l++)
			{
				if (this.graphs[l] != null)
				{
					zipEntry2 = this.zip["graph" + l + "_references.binary"];
					if (zipEntry2 == null)
					{
						throw new Exception("Node references for graph " + l + " not found in the data. Was this loaded from an older version of the A* Pathfinding Project?");
					}
					memoryStream2 = new MemoryStream();
					zipEntry2.Extract(memoryStream2);
					memoryStream2.Seek(0L, SeekOrigin.Begin);
					reader = new BinaryReader(memoryStream2);
					GraphSerializationContext ctx = new GraphSerializationContext(reader, int2Node, l);
					this.graphs[l].GetNodes(delegate(GraphNode node)
					{
						node.DeserializeReferences(ctx);
						return true;
					});
				}
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001A300 File Offset: 0x00018700
		public void PostDeserialization()
		{
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].PostDeserialization();
				}
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001A345 File Offset: 0x00018745
		private void DeserializeNodes(int index, BinaryReader reader)
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001A347 File Offset: 0x00018747
		private void DeserializeNodeConnections(int index, BinaryReader reader)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001A34C File Offset: 0x0001874C
		public void DeserializeEditorSettings(GraphEditorBase[] graphEditors)
		{
			if (graphEditors == null)
			{
				return;
			}
			for (int i = 0; i < graphEditors.Length; i++)
			{
				if (graphEditors[i] != null)
				{
					for (int j = 0; j < this.graphs.Length; j++)
					{
						if (this.graphs[j] != null && graphEditors[i].target == this.graphs[j])
						{
							ZipEntry zipEntry = this.zip["graph" + j + "_editor.json"];
							if (zipEntry != null)
							{
								string @string = this.GetString(zipEntry);
								JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
								GraphEditorBase graphEditorBase = graphEditors[i];
								jsonReader.PopulateObject<GraphEditorBase>(ref graphEditorBase);
								graphEditors[i] = graphEditorBase;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001A41C File Offset: 0x0001881C
		private string GetString(ZipEntry entry)
		{
			MemoryStream memoryStream = new MemoryStream();
			entry.Extract(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string result = streamReader.ReadToEnd();
			memoryStream.Position = 0L;
			streamReader.Dispose();
			return result;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001A45C File Offset: 0x0001885C
		private GraphMeta DeserializeMeta(ZipEntry entry)
		{
			if (entry == null)
			{
				throw new Exception("No metadata found in serialized data.");
			}
			string @string = this.GetString(entry);
			JsonReader jsonReader = new JsonReader(@string, this.readerSettings);
			return (GraphMeta)jsonReader.Deserialize(typeof(GraphMeta));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001A4A4 File Offset: 0x000188A4
		public static void SaveToFile(string path, byte[] data)
		{
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				fileStream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001A4E8 File Offset: 0x000188E8
		public static byte[] LoadFromFile(string path)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				byte[] array = new byte[(int)fileStream.Length];
				fileStream.Read(array, 0, (int)fileStream.Length);
				result = array;
			}
			return result;
		}

		// Token: 0x04000268 RID: 616
		private AstarData data;

		// Token: 0x04000269 RID: 617
		public JsonWriterSettings writerSettings;

		// Token: 0x0400026A RID: 618
		public JsonReaderSettings readerSettings;

		// Token: 0x0400026B RID: 619
		private ZipFile zip;

		// Token: 0x0400026C RID: 620
		private MemoryStream str;

		// Token: 0x0400026D RID: 621
		private GraphMeta meta;

		// Token: 0x0400026E RID: 622
		private SerializeSettings settings;

		// Token: 0x0400026F RID: 623
		private NavGraph[] graphs;

		// Token: 0x04000270 RID: 624
		private const string binaryExt = ".binary";

		// Token: 0x04000271 RID: 625
		private const string jsonExt = ".json";

		// Token: 0x04000272 RID: 626
		private uint checksum = uint.MaxValue;

		// Token: 0x04000273 RID: 627
		private UTF8Encoding encoding = new UTF8Encoding();

		// Token: 0x04000274 RID: 628
		private static StringBuilder _stringBuilder = new StringBuilder();
	}
}
