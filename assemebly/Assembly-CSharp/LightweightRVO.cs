using System;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;

// Token: 0x0200006C RID: 108
[RequireComponent(typeof(MeshFilter))]
public class LightweightRVO : MonoBehaviour
{
	// Token: 0x060003B4 RID: 948 RVA: 0x0001BC24 File Offset: 0x0001A024
	public void Start()
	{
		this.mesh = new Mesh();
		this.mesh.name = "LightweightRVO_Mesh";
		RVOSimulator rvosimulator = UnityEngine.Object.FindObjectOfType(typeof(RVOSimulator)) as RVOSimulator;
		if (rvosimulator == null)
		{
			Debug.LogError("No RVOSimulator could be found in the scene. Please add a RVOSimulator component to any GameObject");
			return;
		}
		this.sim = rvosimulator.GetSimulator();
		base.GetComponent<MeshFilter>().mesh = this.mesh;
		this.CreateAgents(this.agentCount);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0001BCA4 File Offset: 0x0001A0A4
	public void OnGUI()
	{
		if (GUILayout.Button("2", new GUILayoutOption[0]))
		{
			this.CreateAgents(2);
		}
		if (GUILayout.Button("10", new GUILayoutOption[0]))
		{
			this.CreateAgents(10);
		}
		if (GUILayout.Button("100", new GUILayoutOption[0]))
		{
			this.CreateAgents(100);
		}
		if (GUILayout.Button("500", new GUILayoutOption[0]))
		{
			this.CreateAgents(500);
		}
		if (GUILayout.Button("1000", new GUILayoutOption[0]))
		{
			this.CreateAgents(1000);
		}
		if (GUILayout.Button("5000", new GUILayoutOption[0]))
		{
			this.CreateAgents(5000);
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Random Streams", new GUILayoutOption[0]))
		{
			this.type = LightweightRVO.RVOExampleType.RandomStreams;
			this.CreateAgents((this.agents == null) ? 100 : this.agents.Count);
		}
		if (GUILayout.Button("Line", new GUILayoutOption[0]))
		{
			this.type = LightweightRVO.RVOExampleType.Line;
			this.CreateAgents((this.agents == null) ? 10 : Mathf.Min(this.agents.Count, 100));
		}
		if (GUILayout.Button("Circle", new GUILayoutOption[0]))
		{
			this.type = LightweightRVO.RVOExampleType.Circle;
			this.CreateAgents((this.agents == null) ? 100 : this.agents.Count);
		}
		if (GUILayout.Button("Point", new GUILayoutOption[0]))
		{
			this.type = LightweightRVO.RVOExampleType.Point;
			this.CreateAgents((this.agents == null) ? 100 : this.agents.Count);
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001BE74 File Offset: 0x0001A274
	private float uniformDistance(float radius)
	{
		float num = UnityEngine.Random.value + UnityEngine.Random.value;
		if (num > 1f)
		{
			return radius * (2f - num);
		}
		return radius * num;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001BEA8 File Offset: 0x0001A2A8
	public void CreateAgents(int num)
	{
		this.agentCount = num;
		this.agents = new List<IAgent>(this.agentCount);
		this.goals = new List<Vector3>(this.agentCount);
		this.colors = new List<Color>(this.agentCount);
		this.sim.ClearAgents();
		if (this.type == LightweightRVO.RVOExampleType.Circle)
		{
			float d = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.14159274f) * this.exampleScale * 0.05f;
			for (int i = 0; i < this.agentCount; i++)
			{
				Vector3 vector = new Vector3(Mathf.Cos((float)i * 3.14159274f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)i * 3.14159274f * 2f / (float)this.agentCount)) * d;
				IAgent item = this.sim.AddAgent(vector);
				this.agents.Add(item);
				this.goals.Add(-vector);
				this.colors.Add(LightweightRVO.HSVToRGB((float)i * 360f / (float)this.agentCount, 0.8f, 0.6f));
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.Line)
		{
			for (int j = 0; j < this.agentCount; j++)
			{
				Vector3 position = new Vector3((float)((j % 2 != 0) ? -1 : 1) * this.exampleScale, 0f, (float)(j / 2) * this.radius * 2.5f);
				IAgent item2 = this.sim.AddAgent(position);
				this.agents.Add(item2);
				this.goals.Add(new Vector3(-position.x, position.y, position.z));
				this.colors.Add((j % 2 != 0) ? Color.blue : Color.red);
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.Point)
		{
			for (int k = 0; k < this.agentCount; k++)
			{
				Vector3 position2 = new Vector3(Mathf.Cos((float)k * 3.14159274f * 2f / (float)this.agentCount), 0f, Mathf.Sin((float)k * 3.14159274f * 2f / (float)this.agentCount)) * this.exampleScale;
				IAgent item3 = this.sim.AddAgent(position2);
				this.agents.Add(item3);
				this.goals.Add(new Vector3(0f, position2.y, 0f));
				this.colors.Add(LightweightRVO.HSVToRGB((float)k * 360f / (float)this.agentCount, 0.8f, 0.6f));
			}
		}
		else if (this.type == LightweightRVO.RVOExampleType.RandomStreams)
		{
			float num2 = Mathf.Sqrt((float)this.agentCount * this.radius * this.radius * 4f / 3.14159274f) * this.exampleScale * 0.05f;
			for (int l = 0; l < this.agentCount; l++)
			{
				float f = UnityEngine.Random.value * 3.14159274f * 2f;
				float num3 = UnityEngine.Random.value * 3.14159274f * 2f;
				Vector3 position3 = new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * this.uniformDistance(num2);
				IAgent item4 = this.sim.AddAgent(position3);
				this.agents.Add(item4);
				this.goals.Add(new Vector3(Mathf.Cos(num3), 0f, Mathf.Sin(num3)) * this.uniformDistance(num2));
				this.colors.Add(LightweightRVO.HSVToRGB(num3 * 57.29578f, 0.8f, 0.6f));
			}
		}
		for (int m = 0; m < this.agents.Count; m++)
		{
			IAgent agent = this.agents[m];
			agent.Radius = this.radius;
			agent.MaxSpeed = this.maxSpeed;
			agent.AgentTimeHorizon = this.agentTimeHorizon;
			agent.ObstacleTimeHorizon = this.obstacleTimeHorizon;
			agent.MaxNeighbours = this.maxNeighbours;
			agent.NeighbourDist = this.neighbourDist;
			agent.DebugDraw = (m == 0 && this.debug);
		}
		this.verts = new Vector3[4 * this.agents.Count];
		this.uv = new Vector2[this.verts.Length];
		this.tris = new int[this.agents.Count * 2 * 3];
		this.meshColors = new Color[this.verts.Length];
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001C3A0 File Offset: 0x0001A7A0
	public void Update()
	{
		if (this.agents == null || this.mesh == null)
		{
			return;
		}
		if (this.agents.Count != this.goals.Count)
		{
			Debug.LogError("Agent count does not match goal count");
			return;
		}
		for (int i = 0; i < this.agents.Count; i++)
		{
			Vector3 interpolatedPosition = this.agents[i].InterpolatedPosition;
			Vector3 vector = this.goals[i] - interpolatedPosition;
			vector = Vector3.ClampMagnitude(vector, 1f);
			this.agents[i].DesiredVelocity = vector * this.agents[i].MaxSpeed;
		}
		if (this.interpolatedVelocities == null || this.interpolatedVelocities.Length < this.agents.Count)
		{
			Vector3[] array = new Vector3[this.agents.Count];
			if (this.interpolatedVelocities != null)
			{
				for (int j = 0; j < this.interpolatedVelocities.Length; j++)
				{
					array[j] = this.interpolatedVelocities[j];
				}
			}
			this.interpolatedVelocities = array;
		}
		for (int k = 0; k < this.agents.Count; k++)
		{
			IAgent agent = this.agents[k];
			this.interpolatedVelocities[k] = Vector3.Lerp(this.interpolatedVelocities[k], agent.Velocity, agent.Velocity.magnitude * Time.deltaTime * 4f);
			Vector3 vector2 = this.interpolatedVelocities[k].normalized * agent.Radius;
			if (vector2 == Vector3.zero)
			{
				vector2 = new Vector3(0f, 0f, agent.Radius);
			}
			Vector3 b = Vector3.Cross(Vector3.up, vector2);
			Vector3 a = agent.InterpolatedPosition + this.renderingOffset;
			int num = 4 * k;
			int num2 = 6 * k;
			this.verts[num] = a + vector2 - b;
			this.verts[num + 1] = a + vector2 + b;
			this.verts[num + 2] = a - vector2 + b;
			this.verts[num + 3] = a - vector2 - b;
			this.uv[num] = new Vector2(0f, 1f);
			this.uv[num + 1] = new Vector2(1f, 1f);
			this.uv[num + 2] = new Vector2(1f, 0f);
			this.uv[num + 3] = new Vector2(0f, 0f);
			this.meshColors[num] = this.colors[k];
			this.meshColors[num + 1] = this.colors[k];
			this.meshColors[num + 2] = this.colors[k];
			this.meshColors[num + 3] = this.colors[k];
			this.tris[num2] = num;
			this.tris[num2 + 1] = num + 1;
			this.tris[num2 + 2] = num + 2;
			this.tris[num2 + 3] = num;
			this.tris[num2 + 4] = num + 2;
			this.tris[num2 + 5] = num + 3;
		}
		this.mesh.Clear();
		this.mesh.vertices = this.verts;
		this.mesh.uv = this.uv;
		this.mesh.colors = this.meshColors;
		this.mesh.triangles = this.tris;
		this.mesh.RecalculateNormals();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001C824 File Offset: 0x0001AC24
	private static Color HSVToRGB(float h, float s, float v)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = s * v;
		float num5 = h / 60f;
		float num6 = num4 * (1f - Math.Abs(num5 % 2f - 1f));
		if (num5 < 1f)
		{
			num = num4;
			num2 = num6;
		}
		else if (num5 < 2f)
		{
			num = num6;
			num2 = num4;
		}
		else if (num5 < 3f)
		{
			num2 = num4;
			num3 = num6;
		}
		else if (num5 < 4f)
		{
			num2 = num6;
			num3 = num4;
		}
		else if (num5 < 5f)
		{
			num = num6;
			num3 = num4;
		}
		else if (num5 < 6f)
		{
			num = num4;
			num3 = num6;
		}
		float num7 = v - num4;
		num += num7;
		num2 += num7;
		num3 += num7;
		return new Color(num, num2, num3);
	}

	// Token: 0x04000301 RID: 769
	public int agentCount = 100;

	// Token: 0x04000302 RID: 770
	public float exampleScale = 100f;

	// Token: 0x04000303 RID: 771
	public LightweightRVO.RVOExampleType type;

	// Token: 0x04000304 RID: 772
	public float radius = 3f;

	// Token: 0x04000305 RID: 773
	public float maxSpeed = 2f;

	// Token: 0x04000306 RID: 774
	public float agentTimeHorizon = 10f;

	// Token: 0x04000307 RID: 775
	[HideInInspector]
	public float obstacleTimeHorizon = 10f;

	// Token: 0x04000308 RID: 776
	public int maxNeighbours = 10;

	// Token: 0x04000309 RID: 777
	public float neighbourDist = 15f;

	// Token: 0x0400030A RID: 778
	public Vector3 renderingOffset = Vector3.up * 0.1f;

	// Token: 0x0400030B RID: 779
	public bool debug;

	// Token: 0x0400030C RID: 780
	private Mesh mesh;

	// Token: 0x0400030D RID: 781
	private Simulator sim;

	// Token: 0x0400030E RID: 782
	private List<IAgent> agents;

	// Token: 0x0400030F RID: 783
	private List<Vector3> goals;

	// Token: 0x04000310 RID: 784
	private List<Color> colors;

	// Token: 0x04000311 RID: 785
	private Vector3[] verts;

	// Token: 0x04000312 RID: 786
	private Vector2[] uv;

	// Token: 0x04000313 RID: 787
	private int[] tris;

	// Token: 0x04000314 RID: 788
	private Color[] meshColors;

	// Token: 0x04000315 RID: 789
	private Vector3[] interpolatedVelocities;

	// Token: 0x0200006D RID: 109
	public enum RVOExampleType
	{
		// Token: 0x04000317 RID: 791
		Circle,
		// Token: 0x04000318 RID: 792
		Line,
		// Token: 0x04000319 RID: 793
		Point,
		// Token: 0x0400031A RID: 794
		RandomStreams
	}
}
