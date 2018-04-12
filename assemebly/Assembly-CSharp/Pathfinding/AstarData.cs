using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Pathfinding.Serialization;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class AstarData
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00006404 File Offset: 0x00004804
		public AstarPath active
		{
			get
			{
				return AstarPath.active;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000640B File Offset: 0x0000480B
		public byte[] GetData()
		{
			return this.data;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006413 File Offset: 0x00004813
		public void SetData(byte[] data, uint checksum)
		{
			this.data = data;
			this.dataChecksum = checksum;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006424 File Offset: 0x00004824
		public void Awake()
		{
			this.userConnections = new UserConnection[0];
			this.graphs = new NavGraph[0];
			if (this.cacheStartup && this.file_cachedStartup != null)
			{
				this.LoadFromCache();
			}
			else
			{
				this.DeserializeGraphs();
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006478 File Offset: 0x00004878
		public void UpdateShortcuts()
		{
			this.navmesh = (NavMeshGraph)this.FindGraphOfType(typeof(NavMeshGraph));
			this.gridGraph = (GridGraph)this.FindGraphOfType(typeof(GridGraph));
			this.pointGraph = (PointGraph)this.FindGraphOfType(typeof(PointGraph));
			this.recastGraph = (RecastGraph)this.FindGraphOfType(typeof(RecastGraph));
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000064F4 File Offset: 0x000048F4
		public void LoadFromCache()
		{
			AstarPath.active.BlockUntilPathQueueBlocked();
			if (this.file_cachedStartup != null)
			{
				byte[] bytes = this.file_cachedStartup.bytes;
				this.DeserializeGraphs(bytes);
				GraphModifier.TriggerEvent(GraphModifier.EventType.PostCacheLoad);
			}
			else
			{
				Debug.LogError("Can't load from cache since the cache is empty");
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006545 File Offset: 0x00004945
		public byte[] SerializeGraphs()
		{
			return this.SerializeGraphs(SerializeSettings.Settings);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006554 File Offset: 0x00004954
		public byte[] SerializeGraphs(SerializeSettings settings)
		{
			uint num;
			return this.SerializeGraphs(settings, out num);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000656C File Offset: 0x0000496C
		public byte[] SerializeGraphs(SerializeSettings settings, out uint checksum)
		{
			AstarPath.active.BlockUntilPathQueueBlocked();
			AstarSerializer astarSerializer = new AstarSerializer(this, settings);
			astarSerializer.OpenSerialize();
			this.SerializeGraphsPart(astarSerializer);
			byte[] result = astarSerializer.CloseSerialize();
			checksum = astarSerializer.GetChecksum();
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000065A8 File Offset: 0x000049A8
		public void SerializeGraphsPart(AstarSerializer sr)
		{
			sr.SerializeGraphs(this.graphs);
			sr.SerializeUserConnections(this.userConnections);
			sr.SerializeNodes();
			sr.SerializeExtraInfo();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000065CE File Offset: 0x000049CE
		public void DeserializeGraphs()
		{
			if (this.data != null)
			{
				this.DeserializeGraphs(this.data);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000065E8 File Offset: 0x000049E8
		private void ClearGraphs()
		{
			if (this.graphs == null)
			{
				return;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].OnDestroy();
				}
			}
			this.graphs = null;
			this.UpdateShortcuts();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006641 File Offset: 0x00004A41
		public void OnDestroy()
		{
			this.ClearGraphs();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000664C File Offset: 0x00004A4C
		public void DeserializeGraphs(byte[] bytes)
		{
			AstarPath.active.BlockUntilPathQueueBlocked();
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("Bytes should not be null when passed to DeserializeGraphs");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPart(astarSerializer);
					astarSerializer.CloseDeserialize();
					this.UpdateShortcuts();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).\nThe data is either corrupt or it was saved using a 3.0.x or earlier version of the system");
				}
				this.active.VerifyIntegrity();
			}
			catch (Exception arg)
			{
				Debug.LogWarning("Caught exception while deserializing data.\n" + arg);
				this.data_backup = bytes;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000066EC File Offset: 0x00004AEC
		public void DeserializeGraphsAdditive(byte[] bytes)
		{
			AstarPath.active.BlockUntilPathQueueBlocked();
			try
			{
				if (bytes == null)
				{
					throw new ArgumentNullException("Bytes should not be null when passed to DeserializeGraphs");
				}
				AstarSerializer astarSerializer = new AstarSerializer(this);
				if (astarSerializer.OpenDeserialize(bytes))
				{
					this.DeserializeGraphsPartAdditive(astarSerializer);
					astarSerializer.CloseDeserialize();
				}
				else
				{
					Debug.Log("Invalid data file (cannot read zip).");
				}
				this.active.VerifyIntegrity();
			}
			catch (Exception arg)
			{
				Debug.LogWarning("Caught exception while deserializing data.\n" + arg);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006780 File Offset: 0x00004B80
		public void DeserializeGraphsPart(AstarSerializer sr)
		{
			this.ClearGraphs();
			this.graphs = sr.DeserializeGraphs();
			if (this.graphs != null)
			{
				for (int j = 0; j < this.graphs.Length; j++)
				{
					if (this.graphs[j] != null)
					{
						this.graphs[j].graphIndex = (uint)j;
					}
				}
			}
			this.userConnections = sr.DeserializeUserConnections();
			sr.DeserializeExtraInfo();
			int i;
			for (i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
						return true;
					});
				}
			}
			sr.PostDeserialization();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006860 File Offset: 0x00004C60
		public void DeserializeGraphsPartAdditive(AstarSerializer sr)
		{
			if (this.graphs == null)
			{
				this.graphs = new NavGraph[0];
			}
			if (this.userConnections == null)
			{
				this.userConnections = new UserConnection[0];
			}
			List<NavGraph> list = new List<NavGraph>(this.graphs);
			list.AddRange(sr.DeserializeGraphs());
			this.graphs = list.ToArray();
			if (this.graphs != null)
			{
				for (int l = 0; l < this.graphs.Length; l++)
				{
					if (this.graphs[l] != null)
					{
						this.graphs[l].graphIndex = (uint)l;
					}
				}
			}
			List<UserConnection> list2 = new List<UserConnection>(this.userConnections);
			list2.AddRange(sr.DeserializeUserConnections());
			this.userConnections = list2.ToArray();
			sr.DeserializeNodes();
			int i;
			for (i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					this.graphs[i].GetNodes(delegate(GraphNode node)
					{
						node.GraphIndex = (uint)i;
						return true;
					});
				}
			}
			sr.DeserializeExtraInfo();
			sr.PostDeserialization();
			for (int j = 0; j < this.graphs.Length; j++)
			{
				for (int k = j + 1; k < this.graphs.Length; k++)
				{
					if (this.graphs[j] != null && this.graphs[k] != null && this.graphs[j].guid == this.graphs[k].guid)
					{
						Debug.LogWarning("Guid Conflict when importing graphs additively. Imported graph will get a new Guid.\nThis message is (relatively) harmless.");
						this.graphs[j].guid = Pathfinding.Util.Guid.NewGuid();
						break;
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006A40 File Offset: 0x00004E40
		public void FindGraphTypes()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(AstarPath));
			Type[] types = assembly.GetTypes();
			List<Type> list = new List<Type>();
			foreach (Type type in types)
			{
				for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
				{
					if (object.Equals(baseType, typeof(NavGraph)))
					{
						list.Add(type);
						break;
					}
				}
			}
			this.graphTypes = list.ToArray();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006AD8 File Offset: 0x00004ED8
		public Type GetGraphType(string type)
		{
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.graphTypes[i];
				}
			}
			return null;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00006B20 File Offset: 0x00004F20
		public NavGraph CreateGraph(string type)
		{
			Debug.Log("Creating Graph of type '" + type + "'");
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					return this.CreateGraph(this.graphTypes[i]);
				}
			}
			Debug.LogError("Graph type (" + type + ") wasn't found");
			return null;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006B98 File Offset: 0x00004F98
		public NavGraph CreateGraph(Type type)
		{
			NavGraph navGraph = Activator.CreateInstance(type) as NavGraph;
			navGraph.active = this.active;
			return navGraph;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006BC0 File Offset: 0x00004FC0
		public NavGraph AddGraph(string type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (this.graphTypes[i].Name == type)
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError("No NavGraph of type '" + type + "' could be found");
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006C34 File Offset: 0x00005034
		public NavGraph AddGraph(Type type)
		{
			NavGraph navGraph = null;
			for (int i = 0; i < this.graphTypes.Length; i++)
			{
				if (object.Equals(this.graphTypes[i], type))
				{
					navGraph = this.CreateGraph(this.graphTypes[i]);
				}
			}
			if (navGraph == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"No NavGraph of type '",
					type,
					"' could be found, ",
					this.graphTypes.Length,
					" graph types are avaliable"
				}));
				return null;
			}
			this.AddGraph(navGraph);
			return navGraph;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006CCC File Offset: 0x000050CC
		public void AddGraph(NavGraph graph)
		{
			AstarPath.active.BlockUntilPathQueueBlocked();
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == null)
				{
					this.graphs[i] = graph;
					graph.active = this.active;
					graph.Awake();
					graph.graphIndex = (uint)i;
					this.UpdateShortcuts();
					return;
				}
			}
			if (this.graphs != null && (long)this.graphs.Length >= 255L)
			{
				throw new Exception("Graph Count Limit Reached. You cannot have more than " + 255u + " graphs. Some compiler directives can change this limit, e.g ASTAR_MORE_AREAS, look under the 'Optimizations' tab in the A* Inspector");
			}
			this.graphs = new List<NavGraph>(this.graphs)
			{
				graph
			}.ToArray();
			this.UpdateShortcuts();
			graph.active = this.active;
			graph.Awake();
			graph.graphIndex = (uint)(this.graphs.Length - 1);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006DB8 File Offset: 0x000051B8
		public bool RemoveGraph(NavGraph graph)
		{
			graph.SafeOnDestroy();
			int i;
			for (i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] == graph)
				{
					break;
				}
			}
			if (i == this.graphs.Length)
			{
				return false;
			}
			this.graphs[i] = null;
			this.UpdateShortcuts();
			return true;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00006E18 File Offset: 0x00005218
		public static NavGraph GetGraph(GraphNode node)
		{
			if (node == null)
			{
				return null;
			}
			AstarPath active = AstarPath.active;
			if (active == null)
			{
				return null;
			}
			AstarData astarData = active.astarData;
			if (astarData == null)
			{
				return null;
			}
			if (astarData.graphs == null)
			{
				return null;
			}
			uint graphIndex = node.GraphIndex;
			if ((ulong)graphIndex >= (ulong)((long)astarData.graphs.Length))
			{
				return null;
			}
			return astarData.graphs[(int)graphIndex];
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00006E7E File Offset: 0x0000527E
		public GraphNode GetNode(int graphIndex, int nodeIndex)
		{
			return this.GetNode(graphIndex, nodeIndex, this.graphs);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00006E8E File Offset: 0x0000528E
		public GraphNode GetNode(int graphIndex, int nodeIndex, NavGraph[] graphs)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00006E98 File Offset: 0x00005298
		public NavGraph FindGraphOfType(Type type)
		{
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006EF8 File Offset: 0x000052F8
		public IEnumerable FindGraphsOfType(Type type)
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && object.Equals(this.graphs[i].GetType(), type))
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006F24 File Offset: 0x00005324
		public IEnumerable GetUpdateableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && this.graphs[i] is IUpdatableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006F48 File Offset: 0x00005348
		public IEnumerable GetRaycastableGraphs()
		{
			if (this.graphs == null)
			{
				yield break;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null && this.graphs[i] is IRaycastableGraph)
				{
					yield return this.graphs[i];
				}
			}
			yield break;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006F6C File Offset: 0x0000536C
		public int GetGraphIndex(NavGraph graph)
		{
			if (graph == null)
			{
				throw new ArgumentNullException("graph");
			}
			if (this.graphs != null)
			{
				for (int i = 0; i < this.graphs.Length; i++)
				{
					if (graph == this.graphs[i])
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006FC0 File Offset: 0x000053C0
		public int GuidToIndex(Pathfinding.Util.Guid guid)
		{
			if (this.graphs == null)
			{
				return -1;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					if (this.graphs[i].guid == guid)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007020 File Offset: 0x00005420
		public NavGraph GuidToGraph(Pathfinding.Util.Guid guid)
		{
			if (this.graphs == null)
			{
				return null;
			}
			for (int i = 0; i < this.graphs.Length; i++)
			{
				if (this.graphs[i] != null)
				{
					if (this.graphs[i].guid == guid)
					{
						return this.graphs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0400007E RID: 126
		[NonSerialized]
		public NavMeshGraph navmesh;

		// Token: 0x0400007F RID: 127
		[NonSerialized]
		public GridGraph gridGraph;

		// Token: 0x04000080 RID: 128
		[NonSerialized]
		public PointGraph pointGraph;

		// Token: 0x04000081 RID: 129
		[NonSerialized]
		public RecastGraph recastGraph;

		// Token: 0x04000082 RID: 130
		public Type[] graphTypes;

		// Token: 0x04000083 RID: 131
		[NonSerialized]
		public NavGraph[] graphs = new NavGraph[0];

		// Token: 0x04000084 RID: 132
		[NonSerialized]
		public UserConnection[] userConnections = new UserConnection[0];

		// Token: 0x04000085 RID: 133
		public bool hasBeenReverted;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		private byte[] data;

		// Token: 0x04000087 RID: 135
		public uint dataChecksum;

		// Token: 0x04000088 RID: 136
		public byte[] data_backup;

		// Token: 0x04000089 RID: 137
		public TextAsset file_cachedStartup;

		// Token: 0x0400008A RID: 138
		public byte[] data_cachedStartup;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		public bool cacheStartup;
	}
}
